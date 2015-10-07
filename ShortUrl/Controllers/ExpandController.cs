using System.Threading.Tasks;
using System.Web.Mvc;
using ShortUrl.Services;

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
            if (url.Url.StartsWith("http"))
            {
                return Redirect(url.Url);
            }

            return Redirect("http://" + url.Url);
        }

        public ActionResult Debug(int id)
        {
            return Content(id.ToString());
        }
    }
}