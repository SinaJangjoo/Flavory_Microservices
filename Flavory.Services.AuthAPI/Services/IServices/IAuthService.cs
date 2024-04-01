using Flavory.Services.AuthAPI.Models.Dto;

namespace Flavory.Services.AuthAPI.Services.IServices
{

	//In this page we define those endpoints that we declare in controller

	public interface IAuthService
	{
		//In register the return is those columns in UserDto and recieve those columns in RegistrationRequestDto
		Task<string> Register(RegistrationRequestDto registrationRequestDto);


		//In register the return is those columns in LoginResponseDto and recieve those columns in LoginRequestDto
		Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
	}
}
