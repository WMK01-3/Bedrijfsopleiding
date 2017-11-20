using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    public class Address
    {
        public int AddressID;
        public string Street { get; set; }
        public string City { get; set; }
        public string Zipcode { get; set; }

    }
}
