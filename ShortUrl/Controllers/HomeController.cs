using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShortUrl.Services;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortUrlService service;

        public HomeController(IShortUrlService service)
        {
            this.service = service;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.Recent = await service.RecentShortenedUrls().ToListAsync();
            return View();
        }
    }
}