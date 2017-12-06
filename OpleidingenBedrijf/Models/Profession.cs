using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BedrijfsOpleiding.Models
{
    [Table("Profession")]
    public class Profession
    {
        [Key]
        public virtual int ProfessionID { get; set; }

        public virtual int UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        public virtual string ProfessionName { get; set; }
    }
}
