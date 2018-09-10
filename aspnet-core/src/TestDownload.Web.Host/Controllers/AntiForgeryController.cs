using Microsoft.AspNetCore.Antiforgery;
using TestDownload.Controllers;

namespace TestDownload.Web.Host.Controllers
{
    public class AntiForgeryController : TestDownloadControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
