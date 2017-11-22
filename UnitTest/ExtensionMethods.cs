using BedrijfsOpleiding;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ExtensionMethods
    {
        #region IsEmail() : bool

        /// <summary>
        /// Checks if a correct email does pass
        /// </summary>
        [TestMethod]
        public void IsEmailCorrect()
        {
            Assert.IsTrue("test@email.com".IsEmail());
        }

        /// <summary>
        /// Checks if a string without domain does not pass
        /// </summary>
        [TestMethod]
        public void IsEmailWithoutDomain()
        {
            Assert.IsFalse("test@email".IsEmail());
        }

        /// <summary>
        /// Checks if a string without the '@' does not pass
        /// </summary>
        [TestMethod]
        public void IsEmailWithoutAt()
        {
            Assert.IsFalse("testemail.com".IsEmail());
        }

        /// <summary>
        /// Checks if a string containing an empty space does not pass
        /// </summary>
        [TestMethod]
        public void IsEmailWithSpace()
        {
            Assert.IsFalse("test @email.com".IsEmail());
        }

        #endregion

        #region IsName() : bool
        
       

        #endregion
    }
}
