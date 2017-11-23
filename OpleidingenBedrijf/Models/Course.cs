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
        public enum DurationEnum { Eendaagse, Tweedaagse, Driedaagse, Vierdaagse, Wekelijkse}

        [Key]
        public int CourseID { get; set; }
        public string Name { get; set; }
        public DifficultyEnum Difficulty { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime StartDate { get; set; }
        public DurationEnum Duration { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<DateTime> Dates { get; set; }        // all of the active course dates
        public DateTime Created_at = DateTime.Now;

        //public virtual Location Location { get; set; }
        //public virtual User Teacher { get; set; }
        
        public int UserID { get; set; }
        public int LocationID { get; set; }

        // Dit zijn de inschrijvingen
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
