using Microsoft.AspNetCore.Identity;

namespace Flavory.Web.Models
{
	public class LoginResponseDto
	{
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
