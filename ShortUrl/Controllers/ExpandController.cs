using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShortUrl.Controllers
{
    public class ExpandController : Controller
    {
        public ActionResult Index(int id)
        {
            return Content(id.ToString());
        }

        public ActionResult Debug(int id)
        {
            return Content(id.ToString());
        }
    }
}