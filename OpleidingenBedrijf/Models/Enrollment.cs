using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    [Table("Enrollments")]
    public sealed class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public DateTime Timestamp;
        public bool Payed;

        public Course Course { get; set; }
        public User User { get; set; }

        public Enrollment()
        {
            
        }

        public Enrollment(User user, Course course)
        {
            User = user;
            Course = course;
        }
    }
}
