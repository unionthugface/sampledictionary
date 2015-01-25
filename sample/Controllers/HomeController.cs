using sample.Models;
using sample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Glossary()
        {
            GlossaryViewModel g = new GlossaryViewModel();

            g.GlossaryOfTerms = TermService.GetTerms();

            GlossaryViewModel h = new GlossaryViewModel();

            if (g.GlossaryOfTerms != null)
            {
                h.GlossaryOfTerms = g.GlossaryOfTerms.OrderBy(x => x.Term).ToList();
            }

            return View(h);
        }
    }
}