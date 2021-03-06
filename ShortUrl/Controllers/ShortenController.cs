﻿using System.Threading.Tasks;
using System.Web.Mvc;
using ShortUrl.Service;

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
        [Route("Shorten")]
        public async Task<ActionResult> Index(string url)
        {
            await service.ShortenUrlAsync(url);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Delete/{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            await service.DeleteUrlByIdAsync(id);
            return RedirectToAction("Index", "Home");
        }
    }
}