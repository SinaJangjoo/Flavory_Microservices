using System.ComponentModel.DataAnnotations;

namespace Flavory.Web.Models
{
	public class LoginRequestDto
	{
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
