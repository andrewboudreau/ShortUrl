using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using ShortUrl.Service;

namespace ShortUrl.Controllers
{
    public class ExpandController : Controller
    {
        private readonly IShortUrlService service;

        public ExpandController(IShortUrlService service)
        {
            this.service = service;
        }

        public async Task<ActionResult> Index(int id)
        {
            var url = await service.FindUrlByIdAsync(id);
            if (url == null)
            {
                return HttpNotFound();
            }

            await service.AddVisitorAsync(id, Request.Headers, Request.UserAgent);
            return Redirect(url.Url);
        }

        public async Task<ActionResult> Debug(int id)
        {
            var url = await service.FindUrlByIdAsync(id);
            if (url == null)
            {
                return HttpNotFound();
            }

            return Content(JsonConvert.SerializeObject(url));
        }
    }
}