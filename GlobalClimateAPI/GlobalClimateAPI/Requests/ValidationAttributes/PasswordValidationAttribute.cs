using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GlobalClimateAPI.Requests.ValidationAttributes
{
    public class PasswordValidationAttribute : ValidationAttribute
    {
        public PasswordValidationAttribute() : base(RequestsMsgs.INF002) { }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var password = value.ToString();

            var passwordRegex = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{6,}$";

            return Regex.IsMatch(password, passwordRegex);
        }
    }
}
