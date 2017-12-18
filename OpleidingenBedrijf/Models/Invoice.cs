using System;
using System.Collections.Generic;

namespace BedrijfsOpleiding.Models
{
    public class Invoice
    {
        public DateTime Date { get; }
        public List<Course> Courses { get; }
        public User Customer { get; }

        public Invoice(DateTime date, User customer)
        {
            Date = date;
            Courses = new List<Course>();
            Customer = customer;
        }

        public void Add(Course course)
        {
            Courses.Add(course);
        }
    }
}
