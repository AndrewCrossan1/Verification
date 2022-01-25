using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Verification;
namespace VerificationTest
{
    [TestClass]
    public class DOBTest
    {
        [Description("Testing a valid 18 year old DOB"), TestCategory("Date of Birth Testing"), TestMethod()]
        public void Over18() 
        {
            DateOfBirth dob = new DateOfBirth(Convert.ToDateTime("23-01-2004"));
            bool result1 = dob.IsUserOldEnough();
            Assert.AreEqual(true, result1);
        }
        //[Description("Testing an under 18 year olds DOB"), TestCategory("Date of Birth Testing"), TestMethod()]
        public void Under18()
        {
            DateOfBirth dob = new DateOfBirth(Convert.ToDateTime("19-10-2005"));
            bool result1 = dob.IsUserOldEnough();
            bool result2 = dob.ValidateDate();
            if (result1 == false || result2 == false)
            {
                Console.WriteLine(DateOfBirth.ErrorMessage);
                Assert.AreEqual(false, result1);
            }
            else
            {
                Assert.AreEqual(true, result1);
                Assert.AreEqual(true, result2);
            }
        }
//[Description("Testing an under 18 year olds DOB"), TestCategory("Date of Birth Testing"), TestMethod()]
        public void InFuture()
        {
            DateOfBirth dob = new DateOfBirth(Convert.ToDateTime("19-10-2025"));
            bool result1 = dob.IsUserOldEnough();
            bool result2 = dob.ValidateDate();
            if (result1 == false || result2 == false)
            {
                Console.WriteLine(DateOfBirth.ErrorMessage);
                Assert.AreEqual(false, result1);
                Assert.AreEqual(false, result2);
            }
            else
            {
                Assert.AreEqual(true, result1);
                Assert.AreEqual(true, result2);
            }
        }
    }
}
