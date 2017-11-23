using System;
using System.Collections.Generic;

namespace BedrijfsOpleiding.Models
{
    public class Invoice
    {
        public DateTime Date { get; }
        public List<Enrollment> Enrollments { get; }
        public User Customer { get; }

        public Invoice(DateTime date, User customer)
        {
            Date = date;
            Enrollments = new List<Enrollment>();
            Customer = customer;
        }

        public void Add(Enrollment enrollment)
        {
            Enrollments.Add(enrollment);
        }
    }
}
