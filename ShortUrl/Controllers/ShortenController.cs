using System.Threading.Tasks;
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
        public async Task<ActionResult> Index(string url)
        {
            var id = await service.CreateAsync(url);
            return Content($"Shortening '{url}' to {id}");
        }
    }
}