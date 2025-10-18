namespace Domain
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            if (email is null) 
            {
                return false;
            }

            return true;
        }
    }
}
