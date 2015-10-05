using System;
using System.Collections.Generic;
using System.Web.Mvc;
using ShortUrl.Models;

namespace ShortUrl.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Recent = new List<ShortenedUrl>();
            ViewBag.Recent.Add(new ShortenedUrl { Id = 1, Url = "http://www.google.com", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 2, Url = "http://www.google.com?2", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 3, Url = "http://www.google.com?3", Created = DateTime.Now });
            ViewBag.Recent.Add(new ShortenedUrl { Id = 4, Url = "http://stackoverflow.com/questions/3809401/what-is-a-good-regular-expression-to-match-a-url", Created = DateTime.Now });

            return View();
        }
    }
}