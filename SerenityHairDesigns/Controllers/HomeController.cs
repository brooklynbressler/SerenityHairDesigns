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
            Employee u = new Employee();
            return View();
        }

        // GET: Careers
        public ActionResult Careers() { 


            return View(new Resume());
        }


        [HttpPost]
        public ActionResult Careers(Resume model, HttpPostedFileBase File )
        {
            EmailSender sender = new EmailSender();

			sender.SendEmail(model, File);

            return View();
        }

        public ActionResult AddService()
        {
            Database db = new Database();
            List<Genders> Genders = new List<Genders>();

            Genders = db.GetGenders();

            ViewBag.Genders = Genders;

            return View();
        }

        [HttpPost]
        public ActionResult AddService(FormCollection col)
        {
            Database db = new Database();
            List<Genders> Genders = new List<Genders>();

            Genders = db.GetGenders();

            ViewBag.Genders = Genders;

            Services service = new Services();

            service.strServiceName = col["strServiceName"];
            service.decServiceCost = decimal.Parse(col["decServiceCost"]);
            service.intMinutes = int.Parse(col["intMinutes"]);
            service.intGenderID = int.Parse(col["Gender"]);

            db.InsertService(service);

            return RedirectToAction("Services", "Home");
        }


        public ActionResult RemoveService()
        {

            Database db = new Database();

            List<Services> lstServices = new List<Services>();

            lstServices = db.GetAllServices();

            ViewBag.Services = lstServices;

            return View();
        }

        [HttpPost]
        public ActionResult RemoveService(FormCollection col)
        {
            Database db = new Database();

            List<Services> lstServices = new List<Services>();

            lstServices = db.GetAllServices();

            ViewBag.Services = lstServices;

            int intServiceID = int.Parse(col["Services"]);

            db.RemoveService(intServiceID);

            return RedirectToAction("Services", "Home");
        }


        [HttpPost]
        public ActionResult Services(FormCollection col)
        {
            if (col["btnSubmit"] == "btnAddService")
			{
                return RedirectToAction("AddService", "Home");
            }
            else 
			{
                return RedirectToAction("RemoveService", "Home");
            }
        }

        // GET: Services
        public ActionResult Services()
        {

            Database db = new Database();

            List<Services> lstServices = new List<Services>();

            lstServices = db.GetAllServices();

            List<Services> MaleServices = new List<Services>();
            List<Services> FemaleServices = new List<Services>();
            List<Services> BothServices = new List<Services>();

            foreach (var item in lstServices)
			{
                if (item.intGenderID == 2)
				{
                    MaleServices.Add(item);
				}
                else if (item.intGenderID == 1)
				{
                    FemaleServices.Add(item);
				}
                else
				{
                    BothServices.Add(item);
				}
			}

            ViewBag.MaleServices = MaleServices;
            ViewBag.FemaleServices = FemaleServices;
            ViewBag.BothServices = BothServices;

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            ViewBag.EmployeeRole = e.strRole;

            return View();
        }

        public ActionResult AboutUs()
        {
            List<AboutUs> lstReviews = new List<AboutUs>();
            Database db = new Database();

            lstReviews = db.GetReviews();

            List<AboutUs> sortedList = lstReviews.OrderByDescending(x => x.intRating).ToList();

            ViewBag.lstReviews = sortedList;

            return View();
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
            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();

            lstEmployees = db.GetEmployees();

            ViewBag.lstEmployees = lstEmployees;

            return View();
        }

        [HttpPost]
        public ActionResult BookNow(FormCollection col) {
            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();

            lstEmployees = db.GetEmployees();

            ViewBag.lstEmployees = lstEmployees;            

            long lngID = Convert.ToInt64(RouteData.Values["id"]);
            return RedirectToAction("Stylist", "BookNow", new { @id = lngID });
        }
    }
}