using Microsoft.AspNetCore.Http;
using SerenityHairDesigns.Models;
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
            Models.Employee u = new Models.Employee();
            return View();
        }

        [HttpPost]
        public ActionResult Index(System.Web.Mvc.FormCollection col) {
            try {
                Models.Employee e = new Models.Employee();

                if (col["btnSubmit"] == "signin") {
                    e.strEmailAddress = col["strEmailAddress"];
                    e.strPassword = col["strPassword"];

                    e = e.LoginEmployee();
                    if (e != null && e.intEmployeeID > 0) {
                        e.SaveEmployeeSession();
                        return RedirectToAction("EmployeeLoggedIn", "Profile");
                    }
                    else {
                        e = new Models.Employee();
                        e.ActionType = Models.Employee.ActionTypes.LoginFailed;
                    }
                }
                return View(e);
            }
            catch (Exception) {
                Models.Employee u = new Models.Employee();
                return View(u);
            }
        }

        //public ActionResult EmployeeLoggedIn() {
        //    Models.User u = new Models.User();
        //    return View(u);
        //}

        // GET: Careers
        public ActionResult Careers() { 


            return View(new Resume());
        }


        [HttpPost]
        public ActionResult Careers(Resume model, IFormFile File )
        {
            EmailSender sender = new EmailSender();

			sender.SendEmail(model, File);

            return View();
        }


        // GET: Services
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            List<AboutUs> lstReviews = new List<AboutUs>();
            Database db = new Database();

            lstReviews = db.GetReviews();

            return View(lstReviews);
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ContactUs(ContactUs model)
        {
            Database db = new Database();

            db.InsertReview(model);

            return View();
        }

        public ActionResult BookNow()
        {
            return View();
        }





	}
}