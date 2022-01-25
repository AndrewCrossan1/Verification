using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification
{
    /// <summary>
    /// Used to verify usernames, validate user names against an array of other usernames to ensure unique and exclusivity.
    /// </summary>
    public class Username
    {
        #region Variable Declaration
        //Public property for value attribute
        public string Value { get { return value; } set { this.value = value; } }
        //Private attribute for value
        private string value = default;
        //Private Variable for ErrorMessage
        /// <summary>
        /// Saves error message if present
        /// </summary>
        private static string? errormessage;
        //Public property for ErrorMessage attribute
        public static string? ErrorMessage => errormessage;
        #endregion

        //Constructor Method
        public Username(string Username)
        {
            this.value = Username;
        }

        #region Method Declaration

        #endregion
    }
}
