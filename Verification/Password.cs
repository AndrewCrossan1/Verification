using System;
using System.Security.Cryptography;
using System.Text;
using System.Linq;
using System.Threading;
using System.Text.RegularExpressions;

namespace Verification
{
    /// <summary>
    /// Class for verifying passwords, and comparing hashes and returning true or false
    /// </summary>
    public class Password
    {
        #region Variable Declaration
        //Public property for value attribute
        public string? Value { get { return value; } set { this.value = value; } }
        //Private attribute for value
        private string? value = default;
        //Private Variable for ErrorMessage
        /// <summary>
        /// Saves error message if present
        /// </summary>
        private static string? errormessage;
        //Public property for ErrorMessage attribute
        public static string? ErrorMessage => errormessage;
        #endregion

        //Constructor Method
        /// <summary>
        /// <para>Initialises an object of the Password class</para>
        /// </summary>
        /// <param name="Password"></param>
        public Password(string Password)
        {
            this.value = Password;
        }

        #region Method Declaration
        /// <summary>
        /// <para>Used to verify that password meets suitable criteria</para>
        /// </summary>
        private bool Verify() 
        {
            //Check if password is set
            if (string.IsNullOrWhiteSpace(this.value)) 
            {
                errormessage = "Password should not be null or contain whitespace";
                return false;
            }
            //Setting RegEx statements
            //Must contain 1 or more numbers
            var HasNumber = new Regex(@"[0-9]+");
            //Must contain 1 or more uppercase letter
            var HasUpperChar = new Regex(@"[A-Z]+");
            //Must be between 7 and 21 characters
            var IsCorrectLength = new Regex(@".{7,21}");
            //Must contain one or more lower characters
            var HasLowerChar = new Regex(@"[a-z]+");
            //Must contain at least one special character (!#@$£?_-)
            var HasSymbols = new Regex(@"[!#@$£?_-]+");
            if (!HasNumber.IsMatch(this.value))
            {
                errormessage = "Password should contain at least one digit";
                return false;
            }
            else if (!HasUpperChar.IsMatch(this.value))
            {
                errormessage = "Password should contain at least one uppercase character";
                return false;
            }
            else if (!IsCorrectLength.IsMatch(this.value))
            {
                errormessage = "Password must be between 7 and 21 characters";
                return false;
            }
            else if (!HasLowerChar.IsMatch(this.value))
            {
                errormessage = "Password must contain at least one lower character";
                return false;
            }
            else if (!HasSymbols.IsMatch(this.value))
            {
                errormessage = "Password must contain at least one special character";
                return false;
            }
            else 
            {
                return true;
            }
        }
        /// <summary>
        /// <para>Used to hash previously set password value</para>
        /// <para>Can return hashed value, or null if password is not valid</para>
        /// <para>If null is returned, an ErrorMessage will be present</para>
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Hash"></param>
        public string? CreatePasswordHash() 
        {
            if (!Verify()) 
            {
                return null;
            }
            //Create SHA256 object only when it is needed
            using (SHA256 sha256Hash = SHA256.Create())
            {
                //Convert entered value to bytes
                #pragma warning disable CS8604 // Possible null reference argument.
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(value));
                #pragma warning restore CS8604 // Possible null reference argument.

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();  
            }
        }
        #endregion
    }
}