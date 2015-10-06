using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShortUrl.Models;
using ShortUrl.Services.EntityFramework;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var context = new ShortUrlService();

            var id = await context.CreateAsync("http://www.google.com");

            ViewBag.Recent = await context.RecentShortenedUrls();
            ViewBag.Recent.Add(new ShortenedUrl { Id = 1, Url = "http://www.google.com", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 2, Url = "http://www.google.com?2", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 3, Url = "http://www.google.com?3", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 4, Url = "http://stackoverflow.com/questions/3809401/what-is-a-good-regular-expression-to-match-a-url", Created = DateTime.Now });

            return View();
        }
    }
}