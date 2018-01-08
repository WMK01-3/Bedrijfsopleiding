using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BedrijfsOpleiding.Models
{
    [Table("Course")]
    public class Course
    {
        public enum DifficultyEnum { Beginner, Moderate, Expert }

        [Key]
        public int CourseID { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxParticipants { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        
        public DateTime CreatedAt = DateTime.Now;
        
        //The location of the course
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }

        //The Teacher
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        public int UserID { get; set; }

        //A list of the people who entered the Course
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
