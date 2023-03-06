using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Careers
        public ActionResult Careers() {
            return View();
        }

        // GET: Services
        public ActionResult Services()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection col) {

            if (col["btnCareers"].ToString() == "submit") return RedirectToAction("Careers");

            if (col["btnServices"].ToString() == "submit") return RedirectToAction("Services");

            return View();
        }

        [HttpPost]
        public ActionResult Careers(FormCollection col) {

            if (col["btnHome"].ToString() == "submit") return RedirectToAction("Index");


            return View();
        }

        [HttpPost]
        public ActionResult Services(FormCollection col)
        {

            if (col["btnHome"].ToString() == "submit") return RedirectToAction("Index");

            return View();
        }
    }
}