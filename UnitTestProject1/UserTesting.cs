using BedrijfsOpleiding;
using BedrijfsOpleiding.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    class UserTesting
    {

        public void Test()
        {
            User user = new User
            {
                UserID = 1,
                Blocked = false,
                Email = "lol@lol.lol",
                FirstName = "Adolf Musollini",
                LastName = "Rammstein",
                UserName = "MeineFuhrer69"
            };

        }
    }
}
