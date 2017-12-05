using System.Text.RegularExpressions;

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

        /// <summary>
        /// Check if string is a name
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsName(this string str) =>
            Regex.IsMatch(str, @"^[a-z A-Z]{2,24}\b\z$");

        /// <summary>
        /// Check if string is a password
        /// At least 1 number, 1 letter, 1 special character, between 8 and 24 characters
        ///
        /// regex source:
        /// https://stackoverflow.com/questions/19605150/regex-for-password-must-contain-at-least-eight-characters-at-least-one-number-a
        /// Srinivas
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsPassword(this string str) =>
            Regex.IsMatch(str, @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,24}$");

        /// <summary>
        /// Check if string is empty e.g. ""
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string str) =>
            str.Length == 0;

        /// <summary>
        /// Check if string is money : XXXX,XX
        /// Edited for own use
        /// 
        /// regex source:
        /// https://stackoverflow.com/questions/1028221/regex-for-money
        /// balpha
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsMoney(this string str) =>
            Regex.IsMatch(str, @"^(\d+([,.]\d{1,2})?)$");

    }
}
