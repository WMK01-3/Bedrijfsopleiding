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
        private DateTime _calendarDateTime;
        private DateTab _view;

        public bool[] IsDaySelected { get; set; }

        public List<TumblerData> TimeTumblers
        {
            get
            {
                var tumblerDatas = new List<TumblerData>();

                var hours = new string[13];
                var minutes = new string[60];
                var timePeriod = new[] { "AM", "PM" };

                for (var h = 0; h < hours.Length; h++)
                    hours[h] = $"{h}";

                for (var m = 0; m < minutes.Length; m++)
                    minutes[m] = $"{m}";

                tumblerDatas.Add(new TumblerData(hours, 0, ""));
                tumblerDatas.Add(new TumblerData(minutes, 0, ""));
                tumblerDatas.Add(new TumblerData(timePeriod, 0, ""));

                return tumblerDatas;
            }
        }

        #region CalendarCurrentWeek : string

        private string _calendarCurrentWeekLabel;
        public string CalendarCurrentWeekLabel
        {
            get => _calendarCurrentWeekLabel ?? "10-16 December 2017";
            set
            {
                _calendarCurrentWeekLabel = value;
                OnPropertyChanged(nameof(CalendarCurrentWeekLabel));
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

        public DummyDataGridCalendarItem DummyCalendarItem =>
            new DummyDataGridCalendarItem();

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

        public SelectedInfoClass SelectedInfo { get; set; }

        public DateTabVM(MainWindowVM vm, DateTab view) : base(vm)
        {
            _view = view;
            SelectedInfo = new SelectedInfoClass();
            IsDaySelected = new bool[7];

            for (var i = 0; i < 7; i++)
                IsDaySelected[i] = false;

            DateTime dateNow = DateTime.Now;
            _calendarDateTime = dateNow;
            if (DateTime.Now.DayOfWeek > DayOfWeek.Monday)
            {
                _calendarDateTime = dateNow.AddDays((int)DateTime.Now.DayOfWeek -
                                          ((int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday));
            }

            SetCalendar();
        }

        public void SetCalendar(int i)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            if (dfi == null) return;

            DateTime dateNow = DateTime.Now;
            if (DateTime.Now.DayOfWeek > DayOfWeek.Monday)
            {
                dateNow = dateNow.AddDays((int)DateTime.Now.DayOfWeek -
                                          ((int)DateTime.Now.DayOfWeek - (int)DayOfWeek.Monday));
            }

            if (dateNow >= _calendarDateTime.AddDays(i * 7)) return;

            _calendarDateTime = _calendarDateTime.AddDays(i * 7);

            SetCalendar();
        }

        private void SetCalendar()
        {
            CalendarItem = new DataGridCalendarItem
            {
                Monday = GetDaySchedule(_calendarDateTime),
                Tuesday = GetDaySchedule(_calendarDateTime.AddDays(1)),
                Wednesday = GetDaySchedule(_calendarDateTime.AddDays(2)),
                Thursday = GetDaySchedule(_calendarDateTime.AddDays(3)),
                Friday = GetDaySchedule(_calendarDateTime.AddDays(4)),
                Saturday = GetDaySchedule(_calendarDateTime.AddDays(5)),
                Sunday = GetDaySchedule(_calendarDateTime.AddDays(6))
            };
        }

        private static Canvas GetDaySchedule(DateTime date)
        {
            Canvas canvas = new Canvas();

            using (CustomDbContext context = new CustomDbContext())
            {
                var items = from i in context.CourseDates
                            join j in context.Courses on i.CourseID equals j.CourseID
                            where i.Date == date
                            select new { i.Date, j.Duration };

                if (items.Any() == false) return canvas;

                foreach (var item in items)
                {
                    //420 pixels is the total day: 24 hours
                    Rectangle rect = new Rectangle
                    {
                        Width = 10,
                        Height = item.Duration / 60 * 10
                    };

                    int dist = item.Date.Hour * 10 + item.Date.Minute / 6;
                    Canvas.SetTop(rect, dist);

                    canvas.Children.Add(rect);
                }
            }

            return canvas;
        }

        private int _currentSelectedDay = 0;
        public void SelectDay(int i)
        {
            IsDaySelected[_currentSelectedDay] = false;
            _currentSelectedDay = i;
            IsDaySelected[_currentSelectedDay] = true;
            SelectedInfo.Date = $"Datum: {_calendarDateTime.AddDays(i).ToString("dd/mm/yyyy")}";
            OnPropertyChanged(nameof(SelectedInfo));
            OnPropertyChanged(nameof(IsDaySelected));
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
                canvas.Children.Add(GetRect(50, 40));
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
