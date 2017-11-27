using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;

namespace BedrijfsOpleiding.View.CourseView
{
    public partial class AddCourseView
    {
        public int CourseId = 0;

        public AddCourseView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new AddCourseVM(this);

            #region hideControls
            ecCourseName.Visibility = Visibility.Hidden;
            ecMaxParticipants.Visibility = Visibility.Hidden;
            ecStartDate.Visibility = Visibility.Hidden;
            ecPrice.Visibility = Visibility.Hidden;
            ecDescription.Visibility = Visibility.Hidden;
            ErrorMessage.Visibility = Visibility.Hidden;


            #endregion
        }

        public AddCourseView(BaseViewModel parentViewModel, int id) : this(parentViewModel)
        {
            CourseId = id;

            using (CustomDbContext context = new CustomDbContext())
            {
                Course x = (from u in context.Courses
                            where u.CourseID == id
                            select u).First();

                CourseName.Text = x.Title;
                Difficulty.Text = x.Difficulty.ToString();
                Duration.Text = x.Duration.ToString();
                Price.Text = x.Price.ToString(CultureInfo.InvariantCulture);
                MaxParticipants.Value = x.MaxParticipants;
                Teacher.Text = x.UserID.ToString();
                Description.Document.Blocks.Add(new Paragraph(new Run(x.Description)));
                Location.Text = x.LocationID.ToString();
            }
        }

        private void MaxParticipants_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxParticipantsLabel.Content = Math.Round(MaxParticipants.Value, 0);
        }

        private void Teacher_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            using (CustomDbContext context = new CustomDbContext())
            {
                var data = new List<string>();

                foreach (User user in context.Users.Where(u => u.Role == User.RoleEnum.Teacher))
                    data.Add(user.FirstName + " " + user.LastName);

                Teacher.ItemsSource = data;
                Teacher.SelectedIndex = 0;
            }
            */
        }

        private void Teacher_DropDownClosed(object sender, EventArgs e)
        {
            /*
            //TeacherID in hidden input stoppen
            using (CustomDbContext context = new CustomDbContext())
            {
                //Voor wanneer een docent een tussenvoegsel heeft.
                if (Teacher.Text.Split(' ').Length == 3)
                {
                    string firstname = Teacher.Text.Split(' ')[0];
                    string tussenvoegsel = Teacher.Text.Split(' ')[1];
                    string lastname = Teacher.Text.Split(' ')[2];

                    int user = (from u in context.Users
                                where u.Role == User.RoleEnum.Teacher &&
                                      u.FirstName == firstname &&
                                      u.LastName == tussenvoegsel + " " + lastname
                                select u.UserID).First();

                    TeacherID.Text = user.ToString();
                }
                else if (Teacher.Text.Split(' ').Length == 2)
                {
                    string firstname = Teacher.Text.Split(' ')[0];
                    string lastname = Teacher.Text.Split(' ')[1];

                    int user = (from u in context.Users
                                where u.Role == User.RoleEnum.Teacher && u.FirstName == firstname && u.LastName == lastname
                                select u.UserID).First();

                    TeacherID.Text = user.ToString();
                }
            }
            */
        }

        private void Duration_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DurationEnum> data = Enum.GetValues(typeof(Course.DurationEnum)).Cast<Course.DurationEnum>().ToList();

            Duration.ItemsSource = data;
            Duration.SelectedIndex = 0;
        }

        private void SaveCourse_Click(object sender, RoutedEventArgs e)
        {
            /*  if (StartDate.SelectedDate == null) return;

              Course course = new Course
              {
                  Title = CourseName.Text,
                  Difficulty = (Course.DifficultyEnum)Difficulty.SelectedItem,
                  MaxParticipants = (int)MaxParticipants.Value,
                  Duration = (Course.DurationEnum)Duration.SelectedItem,
                  Price = int.Parse(Price.Text),
                  Description = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text,

                  UserID = short.Parse(TeacherID.Text),
                  LocationID = int.Parse(TeacherID.Text)
              };*/

            if (CourseId > 0)
                ((AddCourseVM)OwnViewModel).SaveCourse();
            else
                ((AddCourseVM)OwnViewModel).AddCourse();
        }

        private void Difficulty_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DifficultyEnum> data = Enum.GetValues(typeof(Course.DifficultyEnum)).Cast<Course.DifficultyEnum>().ToList();

            Difficulty.ItemsSource = data;
            Difficulty.SelectedIndex = 0;
        }

        private void Location_Loaded(object sender, RoutedEventArgs e)
        {
            /*
            using (CustomDbContext context = new CustomDbContext())
            {
                List<string> data = context.Locations.Select(location => location.Classroom).ToList();
                Location.ItemsSource = data;
                Location.SelectedIndex = 0;
            }
            */
        }

        private void Location_DropDownClosed_1(object sender, EventArgs e)
        {
            /*
            //LocationID in hidden input stoppen
            using (CustomDbContext context = new CustomDbContext())
            {
                int location = (from l in context.Locations
                                where l.Classroom == Location.Text
                                select l.LocationID).First();
                LocationID.Text = location.ToString();
            }
            */

        }
    }
}
