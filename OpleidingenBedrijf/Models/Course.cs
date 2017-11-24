using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    [Table("Course")]
    public class Course
    {
        public enum DifficultyEnum { Beginner, Moderate, Expert}

        [Key]
        public int CourseID { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxParticipants { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<DateTime> Dates { get; set; }        // all of the active course dates
        public int Duration{ get; set; }                // the duration of the lessons


        public virtual Location Location { get; set; }
        public virtual User Teacher { get; set; }
        // Dit zijn de inschrijvingen
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
