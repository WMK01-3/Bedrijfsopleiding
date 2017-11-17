using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BedrijfsOpleiding.Models
{
    public class Name
    {
        public string FirstName;
        public string LastName;

        public string GetReverse() => LastName + ", " + FirstName;
    }
}
