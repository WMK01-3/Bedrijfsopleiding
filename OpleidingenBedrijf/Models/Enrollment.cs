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
    public class Enrollment
    {
        [Key]
        public int EnrollmentID { get; set; }
        public DateTime Timestamp;
        public bool Payed;


        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
