using System.Text.RegularExpressions;

namespace Backend.Api.Helpers
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;
            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public static bool IsStrongPassword(string password)
        {
            // Al menos 8 caracteres, una mayúscula, una minúscula, un número
            return password.Length >= 8 &&
                   Regex.IsMatch(password, @"[a-z]") &&
                   Regex.IsMatch(password, @"[A-Z]") &&
                   Regex.IsMatch(password, @"[0-9]");
        }
    }
}
