using Flavory.Web.Service.IService;
using Flavory.Web.Utility;
namespace Flavory.Web.Service
{
    public class TokenProvider : ITokenProvider
    {
        //We use " Cookies " to store our JWT token!
        //When we want to use cookies we have to use IHttpContextAccessor

        private readonly IHttpContextAccessor _contextAccessor;
        public TokenProvider(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }
        public void ClearToken()
        {
            _contextAccessor.HttpContext?.Response.Cookies.Delete(SD.TokenCookie);
        }

        public string? GetToken()
        {
            string? token = null;
            bool? hasToken=_contextAccessor.HttpContext?.Request.Cookies.TryGetValue(SD.TokenCookie, out token);

            return hasToken is true ? token : null;
        }

        public void SetToken(string token)
        {
            _contextAccessor.HttpContext?.Response.Cookies.Append(SD.TokenCookie,token);
        }
    }
}
