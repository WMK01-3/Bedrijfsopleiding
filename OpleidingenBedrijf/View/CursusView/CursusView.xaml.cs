using BedrijfsOpleiding.Models;
using BedrijfsOpleiding.ViewModel;
using BedrijfsOpleiding.ViewModel.Course;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BedrijfsOpleiding.View.CursusView
{
    /// <summary>
    /// Interaction logic for CursusView.xaml
    /// </summary>
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
            using (var context = new CustomDbContext())
            {
                List<string>data = new List<string>();

                foreach (var user in context.Users.Where(u => u.Role == User.RoleEnum.Teacher))
                {
                   data.Add(user.FirstName + " " + user.LastName);
                }

                Teacher.ItemsSource = data;
                Teacher.SelectedIndex = 0;

            }

        }

        private void Teacher_DropDownClosed(object sender, EventArgs e)
        {
            //TeacherID in hidden input stoppen
            using (var context = new CustomDbContext())
            {
                //Voor wanneer een docent een tussenvoegsel heeft.
                if (Teacher.Text.Split(' ').Count() == 3)
                {
                    string firstname = Teacher.Text.Split(' ')[0];
                    string tussenvoegsel = Teacher.Text.Split(' ')[1];
                    string lastname = Teacher.Text.Split(' ')[2];

                    var user = (from u in context.Users
                                where (u.Role == User.RoleEnum.Teacher) && 
                                (u.FirstName == firstname) && 
                                (u.LastName == tussenvoegsel + " " + lastname)
                                select u.UserID).First();

                    TeacherID.Text = user.ToString();

                }
                //Voor wanneer een docent GEEN tussenvoegsel heeft.
                else if(Teacher.Text.Split(' ').Count() == 2)
                {
                    string firstname = Teacher.Text.Split(' ')[0];
                    string lastname = Teacher.Text.Split(' ')[1];

                    var user = (from u in context.Users
                                where (u.Role == User.RoleEnum.Teacher) && (u.FirstName == firstname) && (u.LastName == lastname)
                                select u.UserID).First();

                    TeacherID.Text = user.ToString();

                }
            };

        }

        private void Duration_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DurationEnum> data = new List<Course.DurationEnum>();

            foreach(Course.DurationEnum duration in Course.DurationEnum.GetValues(typeof(Course.DurationEnum)))
                data.Add(duration);
            
            Duration.ItemsSource = data;
            Duration.SelectedIndex = 0;
        }

        private void SaveCourse_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("TeacherID:" + TeacherID.Text);
            Console.WriteLine("LocationID:" + LocationID.Text);

            var course = new Models.Course()
            {
                Name = CourseName.Text,
                Difficulty = (Course.DifficultyEnum)Difficulty.SelectedItem,
                MaxParticipants = (int)MaxParticipants.Value,
                StartDate = (DateTime)StartDate.SelectedDate.Value.Date,
                Duration = (Course.DurationEnum)Duration.SelectedItem,
                Price = Int32.Parse(Price.Text),
                Description = new TextRange(Description.Document.ContentStart, Description.Document.ContentEnd).Text,

                UserID = Int16.Parse(TeacherID.Text),
                LocationID = Int32.Parse(TeacherID.Text)
                //Teacher = User.getUserByID(Int16.Parse(TeacherID.Text)),
                //Location = Models.Location.getLocationByID(Int16.Parse(LocationID.Text))
            };

            ((CourseVM)OwnViewModel).AddCourse(course);


        }

        private void Difficulty_Loaded(object sender, RoutedEventArgs e)
        {
            List<Course.DifficultyEnum> data = new List<Course.DifficultyEnum>();

            foreach (Course.DifficultyEnum difficulty in Course.DifficultyEnum.GetValues(typeof(Course.DifficultyEnum)))
                data.Add(difficulty);

            Difficulty.ItemsSource = data;
            Difficulty.SelectedIndex = 0;
        }

        private void Location_Loaded(object sender, RoutedEventArgs e)
        {
            using (var context = new CustomDbContext())
            {
                List<string> data = new List<string>();

                foreach (var location in context.Locations)
                    data.Add(location.Classroom);



                Location.ItemsSource = data;
                Location.SelectedIndex = 0;

            }
        }

        private void Location_DropDownClosed_1(object sender, EventArgs e)
        {
            //LocationID in hidden input stoppen
            using (var context = new CustomDbContext())
            {
                var location = (from l in context.Locations
                                where l.Classroom == Location.Text
                                select l.LocationID).First();
                LocationID.Text = location.ToString();
                
            };
        }
    }
}
