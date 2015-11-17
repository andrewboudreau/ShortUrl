using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Coordinator.Client;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using ShortUrl.Models;
using ShortUrl.Service;

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
            ViewBag.Recent = await service.RecentShortenedUrls();
            return View();
        }

        [HttpGet]
        public ActionResult Import(HttpPostedFileBase file)
        {
            return View();
        }

        [HttpPost, ActionName("Import")]
        public async Task<ActionResult> ImportPost(HttpPostedFileBase import)
        {
            var urls = DeserializeFromStream(import.InputStream);
            await service.ImportAsync(urls);

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Export()
        {
            var urls = await service.ExportAsync();

            var stream = SerializeToStream(urls);
            return File(stream, MimeMapping.GetMimeMapping("file.bson"), "shorturl_export.json");
        }

        private static List<ShortenedUrl> DeserializeFromStream(Stream stream)
        {
            var serializer = new JsonSerializer();

            using (var sr = new StreamReader(stream))
            using (var jsonTextReader = new JsonTextReader(sr))
            {
                return serializer.Deserialize<List<ShortenedUrl>>(jsonTextReader);
            }
        }

        private static Stream SerializeToStream(List<ShortenedUrl> urls)
        {
            var serializer = new JsonSerializer();

            var stream = new MemoryStream();
            var streamWriter = new StreamWriter(stream);
            var jsonWriter = new JsonTextWriter(streamWriter);

            serializer.Serialize(jsonWriter, urls);
            streamWriter.Flush();
            stream.Position = 0;

            return stream;
        }
    }
}