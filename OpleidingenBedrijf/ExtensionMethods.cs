using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BedrijfsOpleiding
{
    public static class ExtensionMethods
    {
        /// <summary>
        /// Checks if string is an email address
        /// 
        /// regex source:
        /// https://stackoverflow.com/questions/33882173/email-address-input-validation
        /// Justin
        /// </summary>
        /// <returns>boolean if string is an email</returns>
        public static bool IsEmail(this string str) =>
            Regex.IsMatch(str, @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
    }
}
