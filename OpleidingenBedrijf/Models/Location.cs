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

        // Address stuff
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public Location()
        {
            
        }

        public Location(string classroom, string street, string city, string zipcode)
        {
            this.Classroom = classroom;
            this.Street = street;
            this.City = city;
            this.Country = zipcode;
        }

        public override string ToString()
        {
            return $"[{Classroom}] {Street}, {City}";
        }
    }
}
