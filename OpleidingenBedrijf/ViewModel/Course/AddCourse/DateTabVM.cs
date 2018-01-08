using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BedrijfsOpleiding.Annotations;
using BedrijfsOpleiding.Database;
using BedrijfsOpleiding.View.CourseView.AddCourse;
using WpfUIPickerLib;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class DateTabVM : BaseViewModel
    {
        private int _currentDateElementIndex = 0;
        private readonly AddCourseView _addCourseView;
        private DateTab _view;
        private DateTime _nearestFirstDayOfWeek;

        public SelectedInfoClass SelectedInfo { get; set; }
        public bool[] IsDaySelected { get; set; }
        public string CalendarCurrentWeekLabel =>
                    $"{_nearestFirstDayOfWeek.AddDays(WeekMultiplier).ToString("MMMM", CultureInfo.InvariantCulture)} {_nearestFirstDayOfWeek.AddDays(WeekMultiplier).Year}";

        public List<TimeLines> TimeLines
        {
            get
            {
                var timeList = new List<TimeLines>();

                for (var i = 0; i < 24; i++)
                    timeList.Add(new TimeLines { X2 = 60, Y1 = 24 * i + 1, Y2 = 24 * i + 1, Text = $"{i}:00", Margin = $"0 {24 * i - 12} 0 0" });

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
                UpdateCalendar();
            }
        }

        #endregion

        #region DateItemList : ObservableCollection<SelectedInfoClass>
        //List of the final date items
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        #endregion

        public DateTabVM(MainWindowVM vm, DateTab view, AddCourseView addCourseView) : base(vm)
        {
            _addCourseView = addCourseView;
            _view = view;
            SelectedInfo = new SelectedInfoClass(_currentDateElementIndex);
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

            UpdateCalendar();
        }

        /// <summary>
        /// Called when one of the Carets is clicked. Moves the calendar by 'i' week
        /// </summary>
        /// <param name="i"></param>
        public void SetCalendar(int i)
        {
            if (_weekMultiplier + i > 0)
            {
                _weekMultiplier += i;
                UpdateCalendar();
                OnPropertyChanged(nameof(CalendarCurrentWeekLabel));
            }
        }

        public void UpdateCalendar()
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
            Canvas canvas = new Canvas();
            DateTime date = _nearestFirstDayOfWeek.AddDays(WeekMultiplier + day);
            var filledTimeSpans = new List<TimeSpan>();

            //See if the classroom has any other courses, so it cannot be overbooked
            using (CustomDbContext context = new CustomDbContext())
            {
                if (_view.classRoom != null)
                {
                    string classroomText = _view.classRoom.Text;

                    LocationTabVM locTab = _addCourseView.ViewModel.LocationTab.ViewModel;
                    string[] locStr = locTab.GetLocationArray();
                    string street = locStr[0];
                    string city = locStr[1];
                    string country = locStr[2];

                    var items = from i in context.CourseDates
                                join j in context.Courses on i.CourseID equals j.CourseID
                                where i.Date.Year == date.Year && i.Date.Month == date.Month && i.Date.Day == date.Day && i.ClassRoom == classroomText && j.Location.Street == street && j.Location.City == city && j.Location.Country == country
                                select new { i.Date, j.Duration };

                    if (items.Any())
                    {
                        foreach (var item in items)
                        {
                            Rectangle rect = new Rectangle
                            {
                                Width = 10,
                                Height = item.Duration * 0.4,
                                Fill = new SolidColorBrush(Colors.Orange)
                            };

                            int dist = (item.Date.Hour - 2) * 24 + item.Date.Minute * 2;
                            Canvas.SetTop(rect, dist);

                            filledTimeSpans.AddRange(GetTimeSpanList(item.Date, item.Duration));

                            canvas.Children.Add(rect);
                        }
                    }
                }
            }

            //If it is the day that is currently selected
            if (_currentSelectedDay == day)
            {
                string duration = _addCourseView.ViewModel.MainTab.Duration.Text;
                Color color = Colors.YellowGreen;

                if (_timeTumblers != null && duration.IsEmpty() == false)
                {
                    List<TimeSpan> newTimeSpans = GetTimeSpanList(new DateTime(1, 1, 1, TimeTumblers[0].SelectedValueIndex, TimeTumblers[1].SelectedValueIndex * 5, 0), int.Parse(duration));

                    foreach (TimeSpan timeSpan in newTimeSpans)
                    {
                        //Cannot add date because classroom is already booked
                        if (filledTimeSpans.Contains(timeSpan))
                            color = Colors.Firebrick;
                    }
                }

                Rectangle rect = new Rectangle
                {
                    Width = 10,
                    Height = 0,
                    Fill = new SolidColorBrush(color)
                };

                if (duration.IsEmpty() == false)
                    rect.Height = int.Parse(duration) * 0.4;

                if (_timeTumblers != null)
                    Canvas.SetTop(rect, 1 + _timeTumblers[0].SelectedValueIndex * 24 + _timeTumblers[1].SelectedValueIndex * 2);
                else
                    Canvas.SetTop(rect, 1);

                Canvas.SetLeft(rect, 30);

                canvas.Children.Add(rect);
            }

            return canvas;
        }

        private static List<TimeSpan> GetTimeSpanList(DateTime date, int duration)
        {
            var timeSpans = new List<TimeSpan>();
            var hourOffset = 0;
            var minute = 0;

            for (var i = 0; i < duration; i++)
            {
                if (i == 60)
                {
                    minute = 0;
                    hourOffset++;
                }

                TimeSpan timeSpan = new TimeSpan(date.Hour + hourOffset, date.Minute + minute, 0);
                if (timeSpans.Contains(timeSpan) == false)
                    timeSpans.Add(timeSpan);

                minute++;
            }
            return timeSpans;
        }

        private int _currentSelectedDay;
        public void SelectDay(int i)
        {
            if (_nearestFirstDayOfWeek.AddDays(WeekMultiplier + i) < DateTime.Now) return;

            IsDaySelected[_currentSelectedDay] = false;
            _currentSelectedDay = i;
            IsDaySelected[_currentSelectedDay] = true;

            SelectedInfo.Date = ChangeDate(SelectedInfo.Date, _nearestFirstDayOfWeek.AddDays(WeekMultiplier + i).Date);

            UpdateCalendar();
            OnPropertyChanged(nameof(IsDaySelected));
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
            UpdateCalendar();
        }

        /// <summary>
        /// Add a date to the list of dates if possible
        /// </summary>
        public void AddDate()
        {
            ErrorMessage = "";

            if (SelectedInfo.ClassRoom.IsEmpty() || SelectedInfo.Date.Day == 0)
            {
                ErrorMessage = "Selecteer een datum en klas";
            }
            else
            {
                ObservableCollection<SelectedInfoClass> tempDateList = DateItemList;
                tempDateList.Add(new SelectedInfoClass(_currentDateElementIndex += 1) { Date = SelectedInfo.Date, ClassRoom = SelectedInfo.ClassRoom });
                DateItemList = tempDateList;

                SelectedInfo.Date = new DateTime(1, 1, 1, 0, 0, 0);
                SelectedInfo.ClassRoom = "";
                _view.classRoom.Text = "";
            }
        }

        /// <summary>
        /// Changes the time of a date because DateTime is stupid
        /// </summary>
        /// <param name="oldDateTime"></param>
        /// <param name="hour"></param>
        /// <param name="minute"></param>
        /// <returns></returns>
        private static DateTime ChangeTime(DateTime oldDateTime, int hour, int minute) =>
            new DateTime(oldDateTime.Year, oldDateTime.Month, oldDateTime.Day, hour, minute, 0);

        private static DateTime ChangeDate(DateTime oldDateTime, DateTime newDateTime) =>
            new DateTime(newDateTime.Year, newDateTime.Month, newDateTime.Day, oldDateTime.Hour, oldDateTime.Minute, 0);

        /// <summary>
        /// Checks if dates are selected, if so create the course
        /// </summary>
        public void CheckData()
        {
            if (DateItemList.Count > 0)
                _addCourseView.ViewModel.AddCourse();
        }
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

    public class TimeLines
    {
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
        public string Text { get; set; }
        public string Margin { get; set; }
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
            Date.ToString("dddd", new CultureInfo("nl-NL"));

        public int ElementIndex { get; set; }

        #endregion

        public SelectedInfoClass(int index)
        {
            Date = new DateTime(1, 1, 1, 0, 0, 0);
            ClassRoom = "";
            ElementIndex = index;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
