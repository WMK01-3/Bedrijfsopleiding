using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    public class Course
    {
        public enum DifficultyEnum { Beginner, Moderate, Expert}

        public int CourseID;
        public DifficultyEnum Difficulty;
        public int MaxParticipants;
        public decimal Price;
        public string Description;
        public User Teacher;
        public Location Location;
        public List<DateTime> Date;         // all of the active course dates
        public int Duration;                // the duration of the lessons
        //public List<User> 
    }
}
