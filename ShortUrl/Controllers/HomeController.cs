using System;
using System.Threading.Tasks;
using System.Web.Mvc;

using Coordinator.Client;
using ShortUrl.Service;

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

            var site = new Coordinator.Models.Site
            {
                Id = GetType().Assembly.GetName().Name,
                Name = "ShortUrl",
                Url = Request.Url.Scheme + Uri.SchemeDelimiter + Request.Url.Host + (Request.Url.IsDefaultPort ? "" : ":" + Request.Url.Port)
            };

            try
            {
                ViewBag.RegisteredSites = await coordinator.RegisterSiteAsync(site);
                ViewBag.Recent = await service.RecentShortenedUrls();
            }
            catch (Exception)
            {
                // ignored
            }

            return View();
        }
    }
}