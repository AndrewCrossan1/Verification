using Microsoft.VisualStudio.TestTools.UnitTesting;
using Verification;

namespace VerificationTest
{
    [TestClass]
    public class PasswordVerification
    {
        [TestMethod]
        public void NormalValidPassword()
        {
            Password p = new Password("Burnside23!");
        }
    }
}