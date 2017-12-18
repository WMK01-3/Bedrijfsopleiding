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
        public bool Read { get; set; }
        public DateTime Timestamp;
        public string MessageText { get; set; }
        public string Title { get; set; }
    }
}