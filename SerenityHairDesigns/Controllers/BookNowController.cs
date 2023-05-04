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

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            List<string> lstSkills = new List<string>();
            Database db = new Database();
            //Employee e = new Employee();
            long lngID = Convert.ToInt64(RouteData.Values["id"]);

            lstSkills = db.SelectSkills(lngID);

            ViewBag.lstSkills = lstSkills;            

            e = e.SelectEmployee(lngID);

            return View(e);
        }

        [HttpPost]
        public ActionResult Stylist(FormCollection col)
        {

            Employee e = new Employee();
            e = e.GetEmployeeSession();
            Customer c = new Customer();
            c = c.GetCustomerSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;


                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }
            if (e.intEmployeeID == 0 && c.intCustomerID == 0)
			{
                return RedirectToAction("ScheduleLogin", "profile");
			}


                List<string> lstSkills = new List<string>();
            Database db = new Database();
          //  Employee e = new Employee();
            long lngID = Convert.ToInt64(RouteData.Values["id"]);

            lstSkills = db.SelectSkills(lngID);

            ViewBag.lstSkills = lstSkills;

            e = e.SelectEmployee(lngID);


            return RedirectToAction("ScheduleNowLoggedIn", "profile", new { @id = lngID });
        }

        public ActionResult ScheduleLogin()
        {
            return View();
        }

    }
}