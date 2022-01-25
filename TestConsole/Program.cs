using System;
using Verification;

namespace TestConsole
{
    public class Program 
    {
        static void Main(string[] args) 
        {
            bool valid = Email.SendConfirmation("andrew.crossan23@gmail.com", "Bruichladdich23!", "andrew.crossan23@gmail.com");
            Console.Write(valid);
        }
    }
}
