using sample.Models;
using sample.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace sample.Controllers
{
    [RoutePrefix("Terms")]
    public class TermsController : Controller
    {
        [Route("Edit/{term?}")]
        [HttpGet]
        public ActionResult Edit(string term = null)
        {
            EditTermViewModel e = new EditTermViewModel();

            if (term == null)
            {
                e.HtmlTitle = "Add a Term";
                e.HtmlHeading = "Add";

            }
            else
            {
                e.HtmlTitle = "Edit Term";
                e.HtmlHeading = "Edit";
                e.GlossaryTerm = TermService.GetTerms(term).SingleOrDefault();
            }

            return View(e);
        }

    }
}