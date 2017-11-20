using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{

    public class User
    {
        public enum RoleEnum { Employee, Teacher, Customer };

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Gender { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }


        public virtual Address Address { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }
}
