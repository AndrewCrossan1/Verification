using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Verification;

namespace VerificationTest
{
    [TestClass]
    public class PasswordVerification
    {
        [Description("Valid"), TestCategory("Password Testing"), TestMethod]
        //Normal data for a valid password - expected return is a hashed version of the constructor parameter
        public void NormalValidPassword()
        {
            Password p = new Password("SevenDuckling!3");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("Length Boundary"), TestCategory("Password Testing"), TestMethod]
        //Extreme data for a valid password (7 Characters) - expected return is a hashed version of the constructor parameter
        public void ExtremeValidPassword()
        {
            Password p = new Password("FourS2!");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("Too short"), TestCategory("Password Testing"), TestMethod]
        //Normal data for an invalid password (4 characters) - expected error message as return
        public void NormalInvalidPassword()
        {
            Password p = new Password("Fo4!");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("No Digit"), TestCategory("Password Testing"), TestMethod]
        //Normal data for an invalid password (No digit) - expected error message as return
        public void NoDigit()
        {
            Password p = new Password("FourSimonChicken!");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("No Special Character"), TestCategory("Password Testing"), TestMethod]
        //Normal data for an invalid password (No special character) - expected error message as return
        public void NoSpecialCharacter()
        {
            Password p = new Password("FourSi2");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("No Capital letter"), TestCategory("Password Testing"), TestMethod]
        //Normal data for an invalid password (No capital letter) - expected error message as return
        public void NoCapitalLetter() 
        {
            Password p = new Password("foursi2!");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
        [Description("No lowercase characters"), TestCategory("Password Testing"), TestMethod]
        //Normal data for an invalid password (No lowercase letter) - expected error message as return
        public void NoLowercaseLetter()
        {
            Password p = new Password("CHICKENDIPPER3553#");
            if (p.CreatePasswordHash() == null)
            {
                Console.WriteLine(Password.ErrorMessage);
            }
            else
            {
                Console.WriteLine(p.CreatePasswordHash());
            }
        }
    }
}