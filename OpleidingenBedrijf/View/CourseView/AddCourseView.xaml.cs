using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using static System.Enum;

namespace BedrijfsOpleiding.View.CursusView
{
    public partial class CursusView
    {
        public CursusView(BaseViewModel parent) : base(parent)
        {
            InitializeComponent();
            OwnViewModel = new CourseVM(this);
        }

        private void MaxParticipants_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MaxParticipantsLabel.Content = Math.Round(MaxParticipants.Value, 0);
        }

        private void Teacher_Loaded(object sender, RoutedEventArgs e)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                var data = new List<string>();

                foreach (User user in context.Users.Where(u => u.Role == User.RoleEnum.Teacher))
                    data.Add(user.FirstName + " " + user.LastName);

                Teacher.ItemsSource = data;
                Teacher.SelectedIndex = 0;
            }
        }

        private void Teacher_DropDownClosed(object sender, EventArgs e)
        {
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
                                where u.Role == User.RoleEnum.Teacher && (u.FirstName == firstname) && (u.LastName == lastname)
                                select u.UserID).First();

                    TeacherID.Text = user.ToString();
                }
            }
        }

        private void Duration_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DurationEnum> data = GetValues(typeof(Course.DurationEnum)).Cast<Course.DurationEnum>().ToList();

            Duration.ItemsSource = data;
            Duration.SelectedIndex = 0;
        }

        private void SaveCourse_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(@"TeacherID:" + TeacherID.Text);
            Console.WriteLine(@"LocationID:" + LocationID.Text);

            if (StartDate.SelectedDate == null) return;

            Course course = new Course
            {
                Name = CourseName.Text,
                Difficulty = (Course.DifficultyEnum)Difficulty.SelectedItem,
                MaxParticipants = (int)MaxParticipants.Value,
                Duration = (Course.DurationEnum)Duration.SelectedItem,
                Price = int.Parse(Price.Text),
                Description = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text,

                UserID = short.Parse(TeacherID.Text),
                LocationID = int.Parse(TeacherID.Text)
            };
            
        }

        private void Difficulty_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DifficultyEnum> data = GetValues(typeof(Course.DifficultyEnum)).Cast<Course.DifficultyEnum>().ToList();

            Difficulty.ItemsSource = data;
            Difficulty.SelectedIndex = 0;
        }

        private void Location_Loaded(object sender, RoutedEventArgs e)
        {
            using (CustomDbContext context = new CustomDbContext())
            {
                List<string> data = context.Locations.Select(location => location.Classroom).ToList();
                Location.ItemsSource = data;
                Location.SelectedIndex = 0;
            }
        }

        private void Location_DropDownClosed_1(object sender, EventArgs e)
        {
            //LocationID in hidden input stoppen
            using (CustomDbContext context = new CustomDbContext())
            {
                int location = (from l in context.Locations
                                where l.Classroom == Location.Text
                                select l.LocationID).First();
                LocationID.Text = location.ToString();
            }
        }
    }
}
