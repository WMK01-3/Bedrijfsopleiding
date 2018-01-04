using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BedrijfsOpleiding.Models
{
    [Table("CourseDate")]
    public class CourseDate
    {
        [Key]
        public virtual int DateID { get; set; }
        public virtual int CourseID { get; set;  }
        [ForeignKey("CourseID")]
        public virtual Course Course { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string ClassRoom { get; set; }
    }
}
