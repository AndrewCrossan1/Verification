using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using Verification;

namespace VerificationTest
{
    [TestClass]
    public class EmailVerification
    {
        [Description("A syntactically valid, domain valid email"), TestCategory("Email Testing"), TestMethod]
        public async Task ValidEmail()
        {
            Email email = new Email("Andrew.crossan23@outlook.com");
            bool valid = await email.VerifyAsync();
            if (!valid)
            {
                Console.WriteLine(Email.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Valid email");
                Assert.AreEqual(valid, true);
            }
        }
        [Description("A syntactically valid, domain invalid email"), TestCategory("Email Testing"), TestMethod]
        public async Task InvalidDomain()
        {
            Email email = new Email("Andrew.crossan23@tigtogdfsadf.com");
            bool valid = await email.VerifyAsync();
            if (!valid)
            {
                Console.WriteLine(Email.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Invalid email");
                Assert.AreEqual(valid, false);
            }
        }
        [Description("A syntactically invalid, domain valid email"), TestCategory("Email Testing"), TestMethod]
        public async Task InvalidSyntax()
        {
            Email email = new Email("1!||@:;'w.#rossan23@outlook.com");
            bool valid = await email.VerifyAsync();
            if (!valid)
            {
                Console.WriteLine(Email.ErrorMessage);
            }
            else
            {
                Console.WriteLine("Invalid email");
                Assert.AreEqual(valid, false);
            }
        }
        [Description("Validate whether email is sent or not (Check email)"), TestCategory("Email Testing"), TestMethod]
        public async Task SendEmail()
        {
            bool result = await Email.SendConfirmationAsync("", "", "");
            if (result)
            {
                Assert.AreEqual(result, true);
            }
            else
            {
                Console.WriteLine(Email.ErrorMessage);
            }
        }
    }
}
