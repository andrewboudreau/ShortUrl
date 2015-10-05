using System.Web.Mvc;

using ShortUrl.Services;

namespace ShortUrl.Controllers
{
    public class ShortenController : Controller
    {
        private readonly IShortUrlService service;

        public ShortenController(IShortUrlService service)
        {
            this.service = service;
        }

        [HttpPost]
        public ActionResult Index(string url)
        {
            var id = this.service.Create(url);
            return this.Content($"Shortening '{url}' to {id}");
        }
    }
}