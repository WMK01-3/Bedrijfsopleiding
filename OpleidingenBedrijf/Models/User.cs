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

        public int ID;
        public RoleEnum Role;
        public Name Name;
        public string Email;
        public Address Address;


        public User(RoleEnum role)
        {
            this.Role = role;
        }
    }
}
