using System;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace Verification
{
    /// <summary>
    /// Provides email verification and email confirmation if necessary
    /// </summary>
    public class Email
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

        public Email(string Email)
        {
            this.value = Email;
        }

        #region Method Declaration
        /// <summary>
        /// Check if email is syntactically valid (Certain minimum length, and contains correct characters.
        /// </summary>
        /// <returns>True or False</returns>
        private bool IsSyntacticallyValid() 
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(this.value))
            {
                return true;
            }
            else
            {
                errormessage = "Email is invalid.";
                return false;
            }
        }
        /// <summary>
        /// Verify email domain exists using SMTP network connection
        /// </summary>
        /// <returns>Asynchronous task that returns True or False</returns>
        private async Task<bool> IsDomainValidAsync()
        {
            try
            {
                //Split email provider from address
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string[]? host = value.Split('@');
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                //Set hostname to the second part of the split (The bit after the @ sign)
                string domain = host[1];
                IPHostEntry IPHst = await Dns.GetHostEntryAsync(domain);
                IPEndPoint endPt = new IPEndPoint(IPHst.AddressList[0], 25);
                Socket s = new Socket(endPt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                await s.ConnectAsync(endPt);
                return true;
            }
            catch (Exception ex) 
            {
                errormessage = "Please use a valid email provider";
                return false;
            }
        }
        /// <summary>
        /// Verify email domain exists using SMTP network connection
        /// </summary>
        /// <returns>Synchronous task that returns True or False</returns>
        public bool IsDomainValid()
        {
            try
            {
                //Split email provider from address
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                string[]? host = value.Split('@');
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                //Set hostname to the second part of the split (The bit after the @ sign)
                string domain = host[1];
                IPHostEntry IPHst = Dns.GetHostEntry(domain);
                IPEndPoint endPt = new IPEndPoint(IPHst.AddressList[0], 25);
                Socket s = new Socket(endPt.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                s.Connect(endPt);
                return true;
            }
            catch (Exception ex)
            {
                errormessage = "Please use a valid email provider";
                return false;
            }
        }
        /// <summary>
        /// Verify email domain and email syntax are both correct and alert user
        /// </summary>
        /// <returns>synchronous task that returns true or false</returns>
        public bool Verify() 
        {
            if (!IsSyntacticallyValid()) 
            {
                return false;
            } else if (!IsDomainValid())
            {
                return false;
            } else 
            {
                return true;
            }
        }
        /// <summary>
        /// Verify email domain and email syntax are both correct and alert user
        /// </summary>
        /// <returns>Asynchronous task that returns true or false</returns>
        public async Task<bool> VerifyAsync()
        {
            bool domainvalid = await Task.Run(IsDomainValidAsync);
            if (!IsSyntacticallyValid())
            {
                return false;
            }
            else if (!domainvalid)
            {
                return false;
            } 
            else
            {
                return true;
            }
        }
        #endregion
    }
}
