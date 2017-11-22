using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        public int LocationID { get; set; }
        public string Classroom { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}
