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
        public enum DurationEnum { Eendaagse, Tweedaagse, Driedaagse, Vierdaagse, Wekelijkse }

        [Key]
        public int CourseID { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxParticipants { get; set; }
        public int Duration { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Archived { get; set; }
        public DateTime Created_at = DateTime.Now;
        public int UserID { get; set; }
        public int LocationID { get; set; }
        [ForeignKey("LocationID")]
        public virtual Location Location { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
        //[ForeignKey("LocationID")]
        //public virtual Location Location { get; set; }
        //[ForeignKey("UserID")]
        //public virtual User User { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
    