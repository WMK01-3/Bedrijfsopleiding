using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace BedrijfsOpleiding.Models
{
    [Table("Users")]
    public class User
    {
        public enum RoleEnum { Employee, Teacher, Customer }
        [Key]
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleEnum Role { get; set; }
        public virtual ICollection<int> EnrollMentID { get; set; }

        public User()
        {

        }

        public User(string firstname, string lastname, string email)
        {
            FirstName = firstname;
            LastName = lastname;
            Email = email;
        }

        public string FullName => $"{FirstName} {LastName}";

        public static Expression<Func<User, bool>> ContainsName(string letters) =>
            user => (user.FirstName + " " + user.LastName).Contains(letters);
    }
}
