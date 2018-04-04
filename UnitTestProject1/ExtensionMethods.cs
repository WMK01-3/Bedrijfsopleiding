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

        /// <summary>
        /// Checks if a string containing rubbish is not valid
        /// </summary>
        [TestMethod]
        public void IsEmailWithRubbish()
        {
            Assert.IsFalse("8732y78y 8723rh f48u fhu8hf ufh84h 4 @@@ @@ @U@(*@@JI @UI@ IUNu.f.f.f.ewf e.f".IsEmail());
        }

        /// <summary>
        /// Checks if a string containing more characters that are allowed in single
        /// </summary>
        [TestMethod]
        public void IsEmailWithMultiples()
        {
            Assert.IsFalse("test@@email.com".IsEmail());
        }

        #endregion

        #region IsName() : bool

        /// <summary>
        /// Checks if a perfectly normal name passes
        /// </summary>
        [TestMethod]
        public void IsName()
        {
            Assert.IsTrue("Meneer Meneer".IsName());
        }

        /// <summary>
        /// Checks if a string containing numbers does not passes
        /// </summary>
        [TestMethod]
        public void IsNameNumber()
        {
            Assert.IsFalse("Meneer 23897".IsName());
        }

        /// <summary>
        /// Checks if a string which is to long (24 character) does not pass
        /// </summary>
        [TestMethod]
        public void IsNameTooLong()
        {
            Assert.IsFalse("Meneer Meneer Meneer Meneer".IsName());
        }

        /// <summary>
        /// Checks if a string which is to short (shorter than 2 character) does not pass
        /// </summary>
        [TestMethod]
        public void IsNameTooShort()
        {
            Assert.IsFalse("M".IsName());
        }

        #endregion

        #region IsMoney() : bool

        [TestMethod]
        public void IsMoney()
        {
            Assert.IsTrue("12,00".IsMoney());
        }

        [TestMethod]
        public void IsMoneyThreeComma()
        {
            Assert.IsFalse("12,000".IsMoney());
        }

        /// <summary>
        /// Checks if money with 2 comma's 
        /// </summary>
        [TestMethod]
        public void IsMoneyMultipleComma()
        {
            Assert.IsFalse("12,00,0".IsMoney());
        }

        [TestMethod]
        public void IsMoneyNoComma()
        {
            Assert.IsTrue("12000".IsMoney());
        }

        #endregion

        #region IsPassword() : bool

        /// <summary>
        /// Checks if a valid password passes
        /// </summary>
        [TestMethod]
        public void IsPassword()
        {
            Assert.IsTrue("password1!".IsPassword());
        }

        /// <summary>
        /// Checks if a password without number does not pass
        /// </summary>
        [TestMethod]
        public void IsPasswordNoNumber()
        {
            Assert.IsFalse("password!".IsPassword());
        }

        /// <summary>
        /// Checks if a password without symbol does not pass
        /// </summary>
        [TestMethod]
        public void IsPasswordNoSymbol()
        {
            Assert.IsFalse("password1".IsPassword());
        }

        /// <summary>
        /// Checks if a password without any letters does not pass
        /// </summary>
        [TestMethod]
        public void IsPasswordNoLetters()
        {
            Assert.IsFalse("1283901!".IsPassword());
        }

        /// <summary>
        /// Checks if a password shorter than 8 characters does not pass
        /// </summary>
        [TestMethod]
        public void IsPasswordTooShort()
        {
            Assert.IsFalse("hi1!".IsPassword());
        }

        /// <summary>
        /// Checks if a password longer than 24 characters does not pass
        /// </summary>
        [TestMethod]
        public void IsPasswordTooLong()
        {
            Assert.IsFalse("ThisIsAVeryLongPassword1!".IsPassword());
        }

        #endregion

        #region IsEmpty() : bool

        /// <summary>
        /// Checks if an empty string passes
        /// </summary>
        [TestMethod]
        public void IsEmpty()
        {
            Assert.IsTrue("".IsEmpty());
        }

        /// <summary>
        /// Checks if an non empty string does not pass
        /// </summary>
        [TestMethod]
        public void IsNotEmpty()
        {
            Assert.IsFalse("joijiojio".IsEmpty());
        }

        #endregion
    }
}
