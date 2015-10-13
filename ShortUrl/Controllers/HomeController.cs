using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using Coordinator.Client;
using ShortUrl.Services;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        private readonly IShortUrlService service;

        private readonly CoordinatorClient coordinator;

        public HomeController(IShortUrlService service, CoordinatorClient coordinator)
        {
            this.service = service;
            this.coordinator = coordinator;
        }

        public async Task<ActionResult> Index()
        {
            if (Request.Url == null)
            {
                throw new NullReferenceException("Request.Url cannot be null");
            }

            ViewBag.RegisteredSites = await coordinator.RegisterSiteAsync(new Coordinator.Models.Site()
            {
                Id = "shorturl",
                Name = "ShortUrl",
                Url =
                    Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host +
                    (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)

            });

            ViewBag.Recent = await service.RecentShortenedUrls().ToListAsync();
            return View();
        }
    }
}