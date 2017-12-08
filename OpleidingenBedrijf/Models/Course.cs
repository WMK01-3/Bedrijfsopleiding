using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BedrijfsOpleiding.Models
{
    [Table("Course")]
    public class Course
    {
        public enum DifficultyEnum { Beginner, Moderate, Expert }

        [Key]
        public int CourseID { get; set; }

        //The Difficulty of the course
        public DifficultyEnum Difficulty { get; set; }

        //How many people can join the course ( this amount always needs to be less than the amount of people able to fit in a classroom
        public int MaxParticipants { get; set; }

        //The amount of minutes a course is per lesson
        public int Duration { get; set; }

        //The cost people need to pay for a course ( total )
        public decimal Price { get; set; }

        //The name of the course
        public string Title { get; set; }

        //A Description
        public string Description { get; set; }

        //The dates a course is given ( the location needs to be free )
        public List<DateTime> Dates { get; set; }

        //The date and time the course is added to the list
        public DateTime CreatedAt = DateTime.Now;

        //The categories of the course, also used for teacher proffessions
        public ICollection<string> Categories { get; set; }

        //The userid of the teacher who gives the course
        public int UserID { get; set; }

        //The ID of the location
        public int LocationID { get; set; }

        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        // All the enrollments : the people who signed up for the course
        public virtual ICollection<Enrollment> Enrollments { get; set; }

        public virtual ICollection<CourseDate> CourseDates { get; set; }
    }
}
