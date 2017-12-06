using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BedrijfsOpleiding.Models
{
    [Table("Teacher")]
    public class Teacher : User
    {
        public ICollection<string> Professions { get; set; }

        public Teacher()
        {
            Role = RoleEnum.Teacher;
        }
    }
}
