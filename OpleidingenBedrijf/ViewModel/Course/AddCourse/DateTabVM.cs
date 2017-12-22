using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using BedrijfsOpleiding.View.CourseView.AddCourse;
using WpfUIPickerLib;

namespace BedrijfsOpleiding.ViewModel.Course.AddCourse
{
    public class DateTabVM : BaseViewModel
    {
        private AddCourseView _addCourseView;
        private DateTab _view;

        private DateTime _nearestFirstDayOfWeek;
        private int _weekMultiplier;
        public int WeekMultiplier
        {
            get => _weekMultiplier * 7;
            set => _weekMultiplier = value;
        }

        public SelectedInfoClass SelectedInfo { get; set; }
        public bool[] IsDaySelected { get; set; }
        public string CalendarCurrentWeekLabel =>
                    $"{_nearestFirstDayOfWeek.AddDays(WeekMultiplier).ToString("MMMM", CultureInfo.InvariantCulture)} {_nearestFirstDayOfWeek.AddDays(WeekMultiplier).Year}";

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

        public DummyDataGridCalendarItem DummyCalendarItem =>
                   new DummyDataGridCalendarItem();

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
                SelectedInfo.ClassRoom = $"Lokaal: {value}";
                OnPropertyChanged(nameof(SelectedInfo));
                OnPropertyChanged(nameof(CurrentClassRoom));
                SetCalendar();
            }
        }

        #endregion

        public DateTabVM(MainWindowVM vm, DateTab view, AddCourseView addCourseView) : base(vm)
        {
            _addCourseView = addCourseView;
            _view = view;
            SelectedInfo = new SelectedInfoClass();
            IsDaySelected = new bool[7];

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

            SelectedInfo.Date = $"Datum: {_nearestFirstDayOfWeek.AddDays(WeekMultiplier + i)}";

            SetCalendar();
            OnPropertyChanged(nameof(SelectedInfo));
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
                SelectedInfo.Time = $"Tijd: {_timeTumblers[0].SelectedValue}:{_timeTumblers[1].SelectedValue}";
                OnPropertyChanged(nameof(SelectedInfo));
            }
            SetCalendar();
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

    public class DummyDataGridCalendarItem
    {
        public Canvas Monday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(50, 40));
                return canvas;
            }
        }
        public Canvas Tuesday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(80, 0));
                return canvas;
            }
        }
        public Canvas Wednesday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(10, 70));
                return canvas;
            }
        }
        public Canvas Thursday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(30, 90));
                return canvas;
            }
        }
        public Canvas Friday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(50, 40));
                return canvas;
            }
        }
        public Canvas Saturday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(120, 30));
                return canvas;
            }
        }
        public Canvas Sunday
        {
            get
            {
                Canvas canvas = new Canvas();
                canvas.Children.Add(GetRect(0, 288));
                return canvas;
            }
        }

        private static Rectangle GetRect(int height, int top)
        {
            Rectangle rect = new Rectangle
            {
                Width = 20,
                Height = height,
                Fill = new SolidColorBrush(Colors.LightSalmon)
            };
            Canvas.SetTop(rect, top);
            return rect;
        }
    }

    public class SelectedInfoClass
    {
        public string Day { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string ClassRoom { get; set; }
    }
}
