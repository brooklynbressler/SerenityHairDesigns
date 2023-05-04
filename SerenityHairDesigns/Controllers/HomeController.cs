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

            Employee u = new Employee();
            return View();
        }

        // GET: Careers
        public ActionResult Careers() {

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

            return View(new Resume());
        }


        [HttpPost]
        public ActionResult Careers(Resume model, HttpPostedFileBase File )
        {

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

            EmailSender sender = new EmailSender();

			sender.SendEmail(model, File);

            return View();
        }

        public ActionResult AddService()
        {

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

            Database db = new Database();
            List<Genders> Genders = new List<Genders>();

            Genders = db.GetGenders();

            ViewBag.Genders = Genders;

            return View();
        }

        [HttpPost]
        public ActionResult AddService(FormCollection col)
        {

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

            Database db = new Database();

            List<Services> lstServices = new List<Services>();

            lstServices = db.GetAllServices();

            ViewBag.Services = lstServices;

            return View();
        }

        [HttpPost]
        public ActionResult RemoveService(FormCollection col)
        {

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
                if(e.strRole == "Admin")
				{
                    ViewBag.IsAdmin = 1;
				}
                else
				{
                    ViewBag.IsAdmin = 0;
				}
			}

            ViewBag.EmployeeRole = e.strRole;

            return View();
        }

        public ActionResult AboutUs()
        {
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

            List<AboutUs> lstReviews = new List<AboutUs>();
            Database db = new Database();

            lstReviews = db.GetReviews();

            List<AboutUs> sortedList = lstReviews.OrderByDescending(x => x.intRating).ToList();

            ViewBag.lstReviews = sortedList;

            return View();
        }

        public ActionResult ContactUs()
        {
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


            return View();
        }
        
        [HttpPost]
        public ActionResult ContactUs(ContactUs model)
        {
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

            Database db = new Database();

            db.InsertReview(model);

            return View();
        }

        public ActionResult BookNow()
        {
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

            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();

            lstEmployees = db.GetEmployees();

            ViewBag.lstEmployees = lstEmployees;

           //         List<Skill> lstSkills = new List<Skill>();

			        //foreach (var item in lstEmployees) {
           //             Skill skill = new Skill();
           //             lstSkills = db.SelectEmployeeSkills();                 
           //         }            

           //         ViewBag.lstSkills = lstSkills;

            DateTime dteStartTime = new DateTime(2023, 5, 1, 8, 0, 0);
            DateTime dteEndTime = new DateTime(2023, 5, 1, 17, 0, 0);

            List<DateTime> TimeSlots = new List<DateTime>();

            DateTime interval = dteStartTime;

            while (interval <= dteEndTime)
			{
                TimeSlots.Add(interval);
                interval = interval.AddMinutes(30);

            }


            return View();
        }

        [HttpPost]
        public ActionResult BookNow(FormCollection col) {

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
            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();

            lstEmployees = db.GetEmployees();

            ViewBag.lstEmployees = lstEmployees;            

            long lngID = Convert.ToInt64(RouteData.Values["id"]);
            return RedirectToAction("Stylist", "BookNow", new { @id = lngID });
        }
    }
}