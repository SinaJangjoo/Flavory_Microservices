namespace Flavory.Web.Utility
{
	public class SD
	{
        public static string CouponAPIBase { get; set; }  //variable to store the base url of our coupon API in our web project used in "appsettings.json"
        public enum ApiType
		{
			GET,
			POST,
			PUT,
			DELETE
		}
	}
}
