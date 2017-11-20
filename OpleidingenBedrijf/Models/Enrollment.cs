using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    public class Enrollment
    {
        public int EnrollmentId;
        public DateTime Timestamp;
        public bool Payed;


        public virtual Course Course { get; set; }
        public virtual User User { get; set; }
    }
}
