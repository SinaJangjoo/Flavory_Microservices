using Microsoft.AspNetCore.Identity;

namespace Flavory.Services.AuthAPI.Models.Dto
{
	public class LoginResponseDto
	{
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
