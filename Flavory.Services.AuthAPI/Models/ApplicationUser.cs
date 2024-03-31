using Microsoft.AspNetCore.Identity;

namespace Flavory.Services.AuthAPI.Models
{
	public class ApplicationUser : IdentityUser
	{
        public string Name { get; set; }
    }
}
