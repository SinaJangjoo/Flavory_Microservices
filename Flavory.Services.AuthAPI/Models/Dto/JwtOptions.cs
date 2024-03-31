namespace Flavory.Services.AuthAPI.Models.Dto
{
	//The name of this class should be the same with "JwtOptions" in appsettings.json page!
	//And all the properties should be the same name with those properties in that page!
	public class JwtOptions
	{
		public string Issuer { get; set; } = string.Empty;
		public string Audience { get; set; } = string.Empty;
		public string Secret { get; set; } = string.Empty;
	}
}

