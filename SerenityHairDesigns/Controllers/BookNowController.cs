using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Controllers
{
    public class BookNowController : Controller
    {
        public ActionResult Stylist() {
            List<string> lstSkills = new List<string>();
            Database db = new Database();
            Employee e = new Employee();
            long lngID = Convert.ToInt64(RouteData.Values["id"]);

            lstSkills = db.SelectSkills(lngID);

            ViewBag.lstSkills = lstSkills;            

            e = e.SelectEmployee(lngID);

            return View(e);
        }

        public ActionResult ScheduleLogin()
        {
            return View();
        }

    }
}