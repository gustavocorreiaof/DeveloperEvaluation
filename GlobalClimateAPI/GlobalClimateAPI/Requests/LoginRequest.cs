using Core.Domain.Msgs;
using GlobalClimateAPI.Requests.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace GlobalClimateAPI.Requests
{
    public class LoginRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestsMsgs), ErrorMessageResourceName = "INF001")]
        public required string UserName { get; set; }
        
        [PasswordValidation]
        public required string Password { get; set; }
    }
}
