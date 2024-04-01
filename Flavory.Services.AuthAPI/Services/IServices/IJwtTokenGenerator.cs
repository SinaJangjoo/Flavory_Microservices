using Flavory.Services.AuthAPI.Models;

namespace Flavory.Services.AuthAPI.Services.IServices
{
	public interface IJwtTokenGenerator
	{
		string GenerateToken(ApplicationUser applicationUser);
	}
}
