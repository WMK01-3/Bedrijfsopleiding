using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    public class Location
    {
        public int LocationId;
        public string Classroom { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}
