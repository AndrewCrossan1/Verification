using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Resources;
using System.Globalization;

namespace Verification
{
    /// <summary>
    /// Provides email verification and email confirmation if necessary
    /// </summary>
    public class Email
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
        /// <summary>
        /// Intialises a new instance of the email class
        /// </summary>
        /// <param name="Email"></param>
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
#pragma warning disable CS8604 // Dereference of a possibly null reference.
            if (re.IsMatch(this.value))
#pragma warning restore CS8604 // Dereference of a possibly null reference
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
#pragma warning disable 
            catch (Exception ex)
            {
#pragma warning restore
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
            catch (Exception)
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
            }
            else if (!IsDomainValid())
            {
                return false;
            }
            else
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
        /// <summary>
        /// Send html message to user email to verify account using randomly generated phrase
        /// </summary>
        /// <returns>True or false</returns>
        public static async Task<bool> SendConfirmationAsync(string Sender, string SenderPassword, string Recepient)
        {
            try
            {
                string vcode = new Random().Next(100000, 999999).ToString();
                //Setting SmtpClient variables.
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(Sender, SenderPassword),
                    EnableSsl = true
                };
                //Set up email message
                MailMessage message = new MailMessage();
                message.To.Add(Recepient);
                message.From = new MailAddress(Sender);
                message.Subject = "Email verification";
                message.IsBodyHtml = true;
                StringBuilder stringbuilder = new StringBuilder();
                ResourceManager rm = new ResourceManager("UsingRESX.Resource1", Assembly.GetExecutingAssembly());
                string strwebsite = Resource1.emailtemplate.ToString();
                string line = strwebsite;
                line = line.Replace("{ vcode }", vcode.ToString());
                message.Body = $"{line}";
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception)
            {
                errormessage = "Email not sent, internal error occurred";
                return false;
            }
        }
        public static bool SendConfirmation(string Sender, string SenderPassword, string Recepient)
        {
            try
            {
                string vcode = new Random().Next(100000, 999999).ToString();
                //Setting SmtpClient variables.
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(Sender, SenderPassword),
                    EnableSsl = true
                };
                //Set up email message
                MailMessage message = new MailMessage();
                message.To.Add(Recepient);
                message.From = new MailAddress(Sender);
                message.Subject = "Email verification";
                message.IsBodyHtml = true;
                StringBuilder stringbuilder = new StringBuilder();
                string line = File.ReadAllText(@"Resources/emailtemplate.html");
                line = line.Replace("{ vcode }", vcode.ToString());
                message.Body = $"{line}";
                client.Send(message);
                return true;
            }
            catch (Exception)
            {
                errormessage = "Email not sent, internal error occurred";
                return false;
            }
        }
    }
}
