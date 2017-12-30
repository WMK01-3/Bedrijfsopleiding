using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Shapes;
using BedrijfsOpleiding.Annotations;
using BedrijfsOpleiding.View.CourseView.AddCourse;
using WpfUIPickerLib;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class DateTabVM : BaseViewModel
    {
        private readonly AddCourseView _addCourseView;
        private DateTab _view;
        private DateTime _nearestFirstDayOfWeek;

        public SelectedInfoClass SelectedInfo { get; set; }
        public bool[] IsDaySelected { get; set; }
        public string CalendarCurrentWeekLabel =>
                    $"{_nearestFirstDayOfWeek.AddDays(WeekMultiplier).ToString("MMMM", CultureInfo.InvariantCulture)} {_nearestFirstDayOfWeek.AddDays(WeekMultiplier).Year}";

        public List<string> TimeLabels
        {
            get
            {
                var timeList = new List<string>();

                for (var i = 0; i < 24; i++)
                    timeList.Add($"{i}:00");

                return timeList;
            }
        }

        #region WeekMultiplier : int

        private int _weekMultiplier;
        public int WeekMultiplier
        {
            get => _weekMultiplier * 7;
            set => _weekMultiplier = value;
        }

        #endregion

        #region DayDateStrings : string[]

        private string[] _dayDateStrings = new string[7];
        public string[] DayDateStrings
        {
            get => _dayDateStrings;
            set
            {
                _dayDateStrings = value;
                OnPropertyChanged(nameof(DayDateStrings));
            }
        }

        #endregion

        #region TimeTumblers : List<TumblerData>

        private List<TumblerData> _timeTumblers;
        public List<TumblerData> TimeTumblers
        {
            get => _timeTumblers;
            set
            {
                _timeTumblers = value;
                OnPropertyChanged(nameof(TimeTumblers));
            }
        }

        #endregion

        #region CalendarItem : DataGridCalendarItem

        private DataGridCalendarItem _calendarItem;
        public DataGridCalendarItem CalendarItem
        {
            get => _calendarItem;
            set
            {
                _calendarItem = value;
                OnPropertyChanged(nameof(CalendarItem));
            }
        }

        #endregion

        #region CurrentClassRoom : string

        private string _currentClassRoom;
        public string CurrentClassRoom
        {
            get => _currentClassRoom;
            set
            {
                _currentClassRoom = value;
                SelectedInfo.ClassRoom = value;
                OnPropertyChanged(nameof(CurrentClassRoom));
                SetCalendar();
            }
        }

        #endregion

        #region DateItemList : ObservableCollection<SelectedInfoClass>

        private ObservableCollection<SelectedInfoClass> _dateItemList;
        public ObservableCollection<SelectedInfoClass> DateItemList
        {
            get => _dateItemList;
            set
            {
                _dateItemList = value;
                OnPropertyChanged(nameof(DateItemList));
            }
        }

        #endregion

        public DateTabVM(MainWindowVM vm, DateTab view, AddCourseView addCourseView) : base(vm)
        {
            _addCourseView = addCourseView;
            _view = view;
            SelectedInfo = new SelectedInfoClass();
            IsDaySelected = new bool[7];
            DateItemList = new ObservableCollection<SelectedInfoClass>();

            //Set the tumblerDatas
            var tumblerDatas = new List<TumblerData>();

            var hours = new string[24];
            var minutes = new string[12];

            for (var h = 0; h < hours.Length; h++)
                hours[h] = $"{h}";

            for (var m = 0; m < minutes.Length; m++)
                minutes[m] = m < 2 ? $"0{m * 5}" : $"{m * 5}";

            tumblerDatas.Add(new TumblerData(hours, 0, "", OnSelectionChanged));
            tumblerDatas.Add(new TumblerData(minutes, 0, "", OnSelectionChanged));

            TimeTumblers = tumblerDatas;

            //Set the selection
            for (var i = 0; i < 7; i++)
                IsDaySelected[i] = false;


            //Initialize the Calendar
            DateTime dateNow = DateTime.Now;
            _nearestFirstDayOfWeek = dateNow;
            if (DateTime.Now.DayOfWeek > DayOfWeek.Monday)
                _nearestFirstDayOfWeek = dateNow.AddDays((int)DayOfWeek.Monday - (int)DateTime.Now.DayOfWeek);
            _weekMultiplier = 0;

            SetCalendar();
        }

        /// <summary>
        /// Called when one of the Carets is clicked. Moves the calendar by 'i' week
        /// </summary>
        /// <param name="i"></param>
        public void SetCalendar(int i)
        {
            _weekMultiplier += i;
            SetCalendar();
            OnPropertyChanged(nameof(CalendarCurrentWeekLabel));
        }

        private void SetCalendar()
        {
            var strings = new string[7];
            for (var j = 0; j < 7; j++)
                strings[j] = _nearestFirstDayOfWeek.AddDays(WeekMultiplier + j).Day.ToString();
            DayDateStrings = strings;

            CalendarItem = new DataGridCalendarItem
            {
                Monday = GetDaySchedule(0),
                Tuesday = GetDaySchedule(1),
                Wednesday = GetDaySchedule(2),
                Thursday = GetDaySchedule(3),
                Friday = GetDaySchedule(4),
                Saturday = GetDaySchedule(5),
                Sunday = GetDaySchedule(6)
            };
        }

        /// <summary>
        /// Gets all courses planned for that day
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private Canvas GetDaySchedule(int day)
        {
            const int barSize = 24;

            Canvas canvas = new Canvas();
            DateTime date = _nearestFirstDayOfWeek.AddDays(WeekMultiplier + day);

            using (CustomDbContext context = new CustomDbContext())
            {
                var items = from i in context.CourseDates
                            join j in context.Courses on i.CourseID equals j.CourseID
                            where i.Date == date
                            select new { i.Date, j.Duration };

                if (items.Any())
                {
                    foreach (var item in items)
                    {
                        //1 pixel = 5 minutes
                        //24 hours = 288 pixels
                        Rectangle rect = new Rectangle
                        {
                            Width = 10,
                            Height = item.Duration / (60 / barSize)
                        };

                        int dist = item.Date.Hour * 24 + item.Date.Minute / 5 * 2;
                        Canvas.SetTop(rect, dist);

                        canvas.Children.Add(rect);
                    }
                }
            }

            //If it is the day that is currently selected
            if (_currentSelectedDay == day)
            {
                Rectangle rect = new Rectangle
                {
                    Width = 10,
                    Height = 0,
                    Fill = new SolidColorBrush(Colors.YellowGreen)
                };

                string text = _addCourseView.ViewModel.MainTab.Duration.Text;

                if (text.IsEmpty() == false)
                    rect.Height = int.Parse(text) / (60 / barSize);

                if (_timeTumblers != null)
                    Canvas.SetTop(rect, _timeTumblers[0].SelectedValueIndex * barSize + _timeTumblers[1].SelectedValueIndex / (60 / barSize));
                else
                    Canvas.SetTop(rect, 0);

                canvas.Children.Add(rect);
            }

            return canvas;
        }

        private int _currentSelectedDay;
        public void SelectDay(int i)
        {
            IsDaySelected[_currentSelectedDay] = false;
            _currentSelectedDay = i;
            IsDaySelected[_currentSelectedDay] = true;

            SelectedInfo.Date = _nearestFirstDayOfWeek.AddDays(WeekMultiplier + i).Date;

            SetCalendar();
            //OnPropertyChanged(nameof(SelectedInfo));
            OnPropertyChanged(nameof(IsDaySelected));
        }

        public void DeSelectDay()
        {

        }

        /// <summary>
        /// Gets called when a value in the timepicker changed
        /// </summary>
        public void OnSelectionChanged()
        {
            if (_timeTumblers != null)
            {
                int hours = int.Parse((string)_timeTumblers[0].SelectedValue);
                int minutes = int.Parse((string)_timeTumblers[1].SelectedValue);
                SelectedInfo.Date = ChangeTime(SelectedInfo.Date, hours, minutes);
            }
            SetCalendar();
        }

        /// <summary>
        /// Add a date to the list of dates if possible
        /// </summary>
        public void AddDate()
        {
            if (SelectedInfo.ClassRoom.IsEmpty() || SelectedInfo.Date.Day == 0)
            {
                //TODO Show Error
                return;
            }
            else
            {
                ObservableCollection<SelectedInfoClass> tempDateList = DateItemList;
                tempDateList.Add(SelectedInfo);
                DateItemList = tempDateList;
            }


            SelectedInfo = new SelectedInfoClass();
        }

        /// <summary>
        /// Changes the time of a date because DateTime is stupid
        /// </summary>
        /// <param name="orgDateTime"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private DateTime ChangeTime(DateTime orgDateTime, int hour, int minute) =>
            new DateTime(orgDateTime.Year, orgDateTime.Month, orgDateTime.Day, hour, minute, 0);
    }

    public class DataGridCalendarItem
    {
        public Canvas Monday { get; set; }
        public Canvas Tuesday { get; set; }
        public Canvas Wednesday { get; set; }
        public Canvas Thursday { get; set; }
        public Canvas Friday { get; set; }
        public Canvas Saturday { get; set; }
        public Canvas Sunday { get; set; }
    }

    public class SelectedInfoClass : INotifyPropertyChanged
    {
        #region Properties

        #region Date : string

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }

        }

        #endregion

        #region ClassRoom : string

        private string _classRoom;
        public string ClassRoom
        {
            get => _classRoom;
            set
            {
                _classRoom = value;
                OnPropertyChanged(nameof(ClassRoom));
            }
        }

        #endregion

        public string NLDate => 
            Date.ToString("D", new CultureInfo("nl-NL"));

        #endregion

        public SelectedInfoClass()
        {
            Date = new DateTime(1, 1, 1, 0, 0, 0);
            ClassRoom = "";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
