using System;
using System.ComponentModel.DataAnnotations;

namespace BedrijfsOpleiding.Models
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public int CourseID { get; set; }
        public int UserID { get; set; }
        public DateTime Timestamp;      
        public Message() { }
    }
}
