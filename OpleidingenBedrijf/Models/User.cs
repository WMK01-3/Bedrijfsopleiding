using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    [Table("Users")]
    public class User
    {
        public enum RoleEnum { Employee, Teacher, Customer }
        public enum GenderEnum { Male, Female }

        [Key]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public GenderEnum Gender { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }

        // Address stuff
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }

        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
