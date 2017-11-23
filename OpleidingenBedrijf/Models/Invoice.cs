using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    class Invoice
    {
        public DateTime Date { get; private set; } 
        public List<Enrollment> Enrollments { get; private set; }
        public User Customer { get; private set; }


        public Invoice(DateTime date, User customer)
        {
            this.Date = date;
            this.Enrollments = new List<Enrollment>();
            this.Customer = customer;
        }

        public void Add(Enrollment enrollment)
        {
            this.Enrollments.Add(enrollment);
        }
    }
}
