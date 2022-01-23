namespace Verification
{
    /// <summary>
    /// Class for verifying Date of Births, returns true or false.
    /// </summary>
    public class DateOfBirth
    {
        #region Variable Declaration
        //Public property for value attribute
        public DateTime Value { get { return value; } set { this.value = value; } }
        //Private attribute for value
        private DateTime value = default;
        //Private Variable for ErrorMessage
        /// <summary>
        /// Saves error message if present
        /// </summary>
        private static string? errormessage;
        //Public property for ErrorMessage attribute
        public static string? ErrorMessage => errormessage;
        #endregion

        public DateOfBirth(DateTime date)
        {
            value = date;
        }

        #region Method Declaration
        /// <summary>
        /// Check if inputted date is valid (Is date in the future?)
        /// </summary>
        /// <returns>True or false</returns>
        public bool ValidateDate()
        {
            try
            {
                if (DateOnly.FromDateTime(this.value) > DateOnly.FromDateTime(DateTime.Today))
                {
                    errormessage = "Date must be not in the future!";
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                errormessage = "An internal error has occurred";
                return false;
            }
        }
        /// <summary>
        /// Check if user is older than or equal to 18
        /// </summary>
        /// <returns>True or false</returns>
        public bool IsUserOldEnough() 
        {
            //Check if year difference is 18
            if (DateOnly.FromDateTime(DateTime.Today).Year - DateOnly.FromDateTime(this.value).Year < 18)
            {
                errormessage = "User is not old enough.";
                return false;
            } 
            else if (DateOnly.FromDateTime(DateTime.Today).Year - DateOnly.FromDateTime(this.value).Year > 18) 
            {
                return true; 
            }
            else
            { 
                if (value.DayOfYear < DateTime.Today.DayOfYear) 
                {
                    errormessage = "User is not old enough.";
                    return false;
                } 
                else if (value.DayOfYear > DateTime.Today.DayOfYear) 
                {
                    return false;
                }
                else 
                {
                    return true;
                }
            }
        }
        #endregion
    }
}
