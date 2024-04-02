namespace Flavory.Web.Utility
{
	public class SD
	{
        public static string CouponAPIBase { get; set; }  //variable to store the base url of our coupon API in our web project used in "appsettings.json"
		public static string AuthAPIBase { get; set; }

        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public const string TokenCookie = "JWTToken";

        public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}
