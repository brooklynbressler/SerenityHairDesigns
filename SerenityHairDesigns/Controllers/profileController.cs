using SerenityHairDesigns.Models;
using System;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Linq;


namespace SerenityHairDesigns.Controllers
{
    public class ProfileController : Controller
    {
        public ActionResult CustomerIndex() {

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

            Customer C = new Customer();
            C = C.GetCustomerSession();
            List<Appointments> lstAppointments = new List<Appointments>();
            Database db = new Database();

            lstAppointments = db.GetAppointments(C.intCustomerID);

            List<Appointments> sortedList = lstAppointments.OrderByDescending(x => x.intAppointmentID).ToList();

            ViewBag.lstAppointments = sortedList;
    
            return View(C); 
        }
        // GET: profile
        public ActionResult ScheduleLogin()
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

            Customer u = new Customer();
            return View(u);
        }

        public ActionResult Employeelogin()
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

        public ActionResult EmployeeScheduleAppointment()
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

            List<Genders> Genders = new List<Genders>();

            Genders = db.GetGenders();

            ViewBag.Genders = Genders;

            return View();
        }

        [HttpPost]
        public ActionResult EmployeeScheduleAppointment(FormCollection col)
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

            Appointments appointment = new Appointments();

            string date = col["AppointmentDateTime"];

            DateTime dteDate;

            DateTime.TryParse(date, out dteDate);

            appointment.dtmAppointmentDate = dteDate;

            int intServiceID = int.Parse(col["Services"]);

            Services service = new Services();

            Database db = new Database();

            service = db.GetSelectedServices(intServiceID);

            appointment.monAppointmentCost = service.decServiceCost;

            appointment.intEstTimeInMins = service.intMinutes;

            appointment.monAppointmentTip = 0;

            appointment.strAppointmentName = service.strServiceName;

            Models.Employee e2 = new Models.Employee();

            e2 = e2.GetEmployeeSession();

            long lngEmployeeID = e2.intEmployeeID;  //employees ID here

            Customer customer = new Customer();

            customer.strFirstName = col["firstname"];
            customer.strLastName = col["lastname"];
            customer.strPhoneNumber = col["Number"];
            int intGender = int.Parse(col["Gender"]);

            db.InsertCustomerManually(customer, intGender, lngEmployeeID );

            customer = db.GetLastCustomer();

            db.InsertAppointmentNoPic(appointment, customer, lngEmployeeID, intServiceID);

            List<Services> lstServices = new List<Services>();

            lstServices = db.GetAllServices();

            ViewBag.Services = lstServices;

            List<Genders> Genders = new List<Genders>();

            Genders = db.GetGenders();

            ViewBag.Genders = Genders;


            return View();
        }


        public ActionResult ScheduleNowLoggedIn()
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

            List<AppointmentTypes> at = new List<AppointmentTypes>();
            List<Services> s = new List<Services>();

            Database db = new Database();

			at = db.GetAppointmentTypes();
			s = db.GetServiceTypes();

            ViewBag.AppointmentTypes = at;
            ViewBag.ServiceTypes = s;
            DateTime dteCurrentDate = DateTime.Now;

            DateTime CurrentDate; 
            CurrentDate = new DateTime(dteCurrentDate.Year, dteCurrentDate.Month, dteCurrentDate.Day, 0, 0, 0);
            string CurrentDate2 = CurrentDate.ToString("yyyy-MM-dd");
            ViewBag.CurrentDate = CurrentDate2;
            
            DateTime midnightDate;
            DateTime midnightDate2;

            midnightDate = new DateTime(dteCurrentDate.Year, dteCurrentDate.Month, dteCurrentDate.Day, 0, 0, 0);
            midnightDate2 = new DateTime(dteCurrentDate.Year, dteCurrentDate.Month, dteCurrentDate.Day, 0, 0, 0);
            midnightDate2 = midnightDate2.AddDays(1);

            string begindate;
            string enddate;

            begindate = midnightDate.ToString("yyyy-MM-dd");
            enddate = midnightDate2.ToString("yyyy-MM-dd");

            long lngID = Convert.ToInt64(RouteData.Values["id"]);

            List<StylistAvailability> stylistAvailabilities = new List<StylistAvailability>();

            stylistAvailabilities = db.GetTimesByDate(lngID, begindate, enddate);


            foreach(var item in stylistAvailabilities)
			{
                item.strStartTimeString = item.dteStartTime.ToString("h:mm tt");
			}


            ViewBag.StylistAvailabilities = stylistAvailabilities;




            return View();

        }

		[HttpPost]
		public ActionResult ScheduleNowLoggedIn(HttpPostedFileBase UserImage, FormCollection col)
		{
			try
			{
                Employee e = new Employee();
                e = e.GetEmployeeSession();

                if (e.intEmployeeID == 0)
                {

                    ViewBag.blnIsEmployee = 0;
                    Customer c2 = new Customer();
                    c2 = c2.GetCustomerSession();

                    if (c2.intCustomerID > 0)
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



                List<AppointmentTypes> at = new List<AppointmentTypes>();
                List<Services> s = new List<Services>();

                Database db = new Database();

                at = db.GetAppointmentTypes();
                s = db.GetServiceTypes();

                ViewBag.AppointmentTypes = at;
                ViewBag.ServiceTypes = s;
                //Employee e = new Employee();
                Appointments a = new Appointments();
				List<StylistAvailability> sty = new List<StylistAvailability>();
				string dteSelectedDate = col["date"];
                ViewBag.CurrentDate = dteSelectedDate;

                DateTime dteSelectedDate2;

				DateTime.TryParse(dteSelectedDate, out dteSelectedDate2);

                DateTime dteSelectedDateafter;
                dteSelectedDateafter = dteSelectedDate2.AddDays(1);

                string seconddate = dteSelectedDateafter.ToString();

                long lngID = Convert.ToInt64(RouteData.Values["id"]);


                if (col["btnSubmit"] == "ViewTimes")
				{
					sty = db.GetTimesByDate(lngID, dteSelectedDate, seconddate);
                    foreach (var item in sty)
                    {
                        item.strStartTimeString = item.dteStartTime.ToString("h:mm tt");
                    }
                    ViewBag.StylistAvailabilities = sty;

                }

                if (col["btnSubmit"] == "Schedule-Appointment-Btn")
				{
                    

                    int intTimeID = int.Parse(col["Times"]);

                    a.intAppointmentType = int.Parse(col["AptTypes"]);                    

                    int intServiceType = int.Parse(col["ServiceType"]);

                    Customer c = new Customer();

                    c = c.GetCustomerSession();

                    db.InsertAppointmentNoPic(a, c, lngID, intServiceType, intTimeID);



					//if (UserImage == null)
					//{
					//    c.UserImage1 = new Models.Image();
					//    if (col["EventImage.ImageID"].ToString() == "")
					//    {
					//        c.UserImage1.ImageID = 0;
					//    }
					//    else
					//    {
					//        c.UserImage1.ImageID = Convert.ToInt32(col["EventImage.ImageID"]);
					//    }

					//    c.UserImage1.Primary = true;
					//    c.UserImage1.FileName = Path.GetFileName(UserImage.FileName);
					//    if (c.UserImage.IsImageFile())
					//    {
					//        c.UserImage1.Size = UserImage.ContentLength;
					//        Stream stream = UserImage.InputStream;
					//        BinaryReader binaryReader = new BinaryReader(stream);
					//        c.UserImage1.ImageData = binaryReader.ReadBytes((int)stream.Length);

					//        c.UpdatePrimaryImage();
					//    }
					//}


					//c.UserImage = new Image();
					//c.UserImage.ImageID = Convert.ToInt32(col["UserImage2.ImageID"]);

					//if (UserImage != null)
					//{
					//    c.UserImage = new Image();
					//    c.UserImage.ImageID = Convert.ToInt32(col["UserImage2.ImageID"]);
					//    c.UserImage.Primary = true;
					//    c.UserImage.FileName = Path.GetFileName(UserImage.FileName);
					//    if (c.UserImage.IsImageFile())
					//    {
					//        c.UserImage.Size = UserImage.ContentLength;
					//        Stream stream = UserImage.InputStream;
					//        BinaryReader binaryReader = new BinaryReader(stream);
					//        c.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
					//        db.InsertCustomerImage(c);
					//    }
					//}




					//HttpPostedFileBase UserCurrentImage =

					//HttpPostedFileBase UserDesiredImage = 



				}


                return View();

            }
            catch (Exception ex)
			{
				Customer c = new Customer();
				return View(c);
			}

		}

		public ActionResult EmployeeLoggedIn()
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

            return View(e);
        }

        [HttpPost]
        public JsonResult GetEmployeeById(int id)
        {


            Database db = new Database();
            var employees = db.GetEmployees();
            var employee = employees.Find((e) => e.intEmployeeID == id);
            return Json(employee);
        }

        public ActionResult AdminLoggedIn()
        {
            Database db = new Database();

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

           // Employee e = new Employee();
            e = e.GetEmployeeSession();
            if (e.IsEmployeeAuthenticated)
            {
                List<Image> images = new List<Image>();
                images = db.GetEmployeeImages(e.intEmployeeID, 0, true);
                e.UserImage = new Image();
                if (images.Count > 0) e.UserImage = images[0];
            }

            List<Schedules> Schedules = new List<Schedules>();

            DateTime CurrentDate = DateTime.Now;


            //  Schedules = db.GetEmployeesSchedule(e.intEmployeeID, CurrentDate);
            List<SelectListItem> items = new List<SelectListItem>();

            items = GetSchedules(e.intEmployeeID);
            
            ViewBag.Schedules = items;



            return View(e);
        }


        public List<SelectListItem> GetSchedules(long intEmployeeID)
        {
            List<Models.Schedules> schedules2 = new List<Models.Schedules>();

            DateTime CurrentDate = DateTime.Now;
            Database db = new Database();

            schedules2 = db.GetEmployeesSchedule(intEmployeeID, CurrentDate);


            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in schedules2)
            {
                string Date = item.dteEndTime.ToString("h:mm tt");
                string date2 = item.dteStartTime.ToString("h:mm tt");
                string date3 = item.dteStartTime.ToString("MM/dd");
                items.Add(new SelectListItem { Text = date3 + " -- " + date2 + " to " + Date, Value = item.intScheduleID.ToString() });
            }

            return items;

        }


        [HttpPost]
        public ActionResult AdminLoggedIn(HttpPostedFileBase UserImage, FormCollection col, string Schedules)
        {
            try
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

               // Employee e = new Employee();
                e = e.GetEmployeeSession();

                e.strFirstName = col["strFirstName"];
                e.strLastName = col["strLastName"];
                e.strPassword = col["strPassword"];
                e.strPhoneNumber = col["strPhoneNumber"];
                e.strEmailAddress = col["strEmailAddress"];
                e.strGender = col["strGender"];

                if (e.strGender == "Female")
                {
                    e.intGenderID = 1;
                }
                else if (e.strGender == "Male")
                {
                    e.intGenderID = 2;

                }
                else if (e.strGender == "General")
                {
                    e.intGenderID = 3;

                }


                if (e.strFirstName.Length == 0 || e.strLastName.Length == 0 || e.strEmailAddress.Length == 0 || e.strPassword.Length == 0)
                {
                    e.ActionType = Models.Employee.ActionTypes.RequiredFieldsMissing;
                    return View(e);
                }
                else
                {
                    if (col["btnSubmit"] == "update")
                    { //update button pressed
                        e.Save();

                        e.UserImage = new Image();
                        e.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);

                        if (UserImage != null)
                        {
                            e.UserImage = new Image();
                            e.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);
                            e.UserImage.Primary = true;
                            e.UserImage.FileName = Path.GetFileName(UserImage.FileName);
                            if (e.UserImage.IsImageFile())
                            {
                                e.UserImage.Size = UserImage.ContentLength;
                                Stream stream = UserImage.InputStream;
                                BinaryReader binaryReader = new BinaryReader(stream);
                                e.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
                                e.UpdatePrimaryImage();
                            }
                        }


                        e.SaveEmployeeSession();
                        return RedirectToAction("AdminLoggedIn", "Profile");
                    }

                    else if (col["btnSubmit"] == "DeleteSchedule")
					{

                        int intScheduleID = int.Parse(Schedules);

                        Database db = new Database();

                        db.DeleteSchedule(intScheduleID);

                        col["btnSubmit"] = "";

                        return RedirectToAction("AdminLoggedIn", "Profile");
                    }

                    else if (col["btnSubmit"] == "EditAvailability")
                    {



                        string dteStartTime = col["dteStartDate"];


                        DateTime dteStartDate;

                        DateTime.TryParse(dteStartTime, out dteStartDate);


                        string dteEndTime = col["dteEndDate"];

                        DateTime dteEndDate;

                        DateTime.TryParse(dteEndTime, out dteEndDate);

                        Models.Employee emp = new Models.Employee();

                        emp = emp.GetEmployeeSession();

                        long lngEmployeeID = emp.intEmployeeID;  //employees ID here

                        Database db = new Database();

                        db.InsertAvailabilitySchedule(dteStartDate, dteEndDate, lngEmployeeID);


                        List<DateTime> TimeSlots = new List<DateTime>();

                        DateTime interval = dteStartDate;

                        while (interval <= dteEndDate)
                        {
                            TimeSlots.Add(interval);
                            interval = interval.AddMinutes(30);

                        }

                        foreach (var item in TimeSlots)
                        {

                            db.InsertAvailability(item, item.AddMinutes(29), lngEmployeeID);

                        }

                        col["btnSubmit"] = "";


                        e.SaveEmployeeSession();
                        return RedirectToAction("AdminLoggedIn", "Profile");

                    }
                    return View(e);
                }
            }
            catch (Exception ex)
            {
                Employee e = new Employee();
                return View(e);
            }
        }




        [HttpPost]
        public ActionResult CustomerIndex(HttpPostedFileBase UserImage, FormCollection col)
        {

            try
            {
                Employee e = new Employee();
                e = e.GetEmployeeSession();

                if (e.intEmployeeID == 0)
                {

                    ViewBag.blnIsEmployee = 0;
                    Customer c2 = new Customer();
                    c2 = c2.GetCustomerSession();

                    if (c2.intCustomerID > 0)
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

                Customer c = new Customer();
                c = c.GetCustomerSession();

                c.strFirstName = col["strFirstName"];
                c.strLastName = col["strLastName"];
                c.strPassword = col["strPassword"];
                c.strPhoneNumber = col["strPhoneNumber"];
                c.strEmailAddress = col["strEmailAddress"];
                c.strGender = col["strGender"];


                if (c.strFirstName.Length == 0 || c.strLastName.Length == 0 || c.strEmailAddress.Length == 0 || c.strPassword.Length == 0)
                {
                    c.ActionType = Models.Customer.ActionTypes.RequiredFieldsMissing;
                    return View(c);
                }
                else
                {
                    if (col["btnSubmit"] == "update")
                    { //update button pressed
                        c.SaveCustomer();

                        c.UserImage = new Image();
                        c.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);

                        if (UserImage != null)
                        {
                            c.UserImage = new Image();
                            c.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);
                            c.UserImage.Primary = true;
                            c.UserImage.FileName = Path.GetFileName(UserImage.FileName);
                            if (c.UserImage.IsImageFile())
                            {
                                c.UserImage.Size = UserImage.ContentLength;
                                Stream stream = UserImage.InputStream;
                                BinaryReader binaryReader = new BinaryReader(stream);
                                c.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
                                c.UpdatePrimaryImage();
                            }
                        }

                        c.SaveCustomerSession();
                        return RedirectToAction("CustomerIndex", "Profile");

                    }
                    return View(c);
                    
                }
                
            }
            catch (Exception)
            {
                Customer c = new Customer();
                return View(c);
            }
        }

        public ActionResult EmployeesInfo()
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

            //Models.Employee e = new Models.Employee();

            e = e.GetEmployeeSession();

            long lngEmployeeID = e.intEmployeeID;  //employees ID here

            List<SelectListItem> items = new List<SelectListItem>();

            items = GetEmployeesProducts(lngEmployeeID);

            ViewBag.EmployeesProducts = items;

            List<Products> AllProducts = new List<Products>();

            Models.Database db = new Models.Database();

            AllProducts = db.GetAllProducts();

            ViewBag.AllProducts = AllProducts;

            return View();
        }

        public List<SelectListItem> GetEmployeesProducts(long lngEmployeeID)
        {
            List<Models.EmployeeProducts> lstProducts = new List<Models.EmployeeProducts>();

            Models.Database db = new Models.Database();

            lstProducts = db.GetEmployeesProducts(lngEmployeeID);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in lstProducts)
            {
                items.Add(new SelectListItem { Text = item.product.strProductName + ", \t Quantity =  " + item.intProductInventory, Value = item.intEmployeeProductID.ToString() });
            }

            return items;

        }


        [HttpPost]
        public ActionResult EmployeesInfo(FormCollection col, string EmployeesProducts)
        {
            try
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

               // Models.Employee e = new Models.Employee();

                e = e.GetEmployeeSession();

                long lngEmployeeID = e.intEmployeeID;

                List<SelectListItem> items = new List<SelectListItem>();

                items = GetEmployeesProducts(lngEmployeeID);

                ViewBag.EmployeesProducts = items;

                Models.Employee employee = new Models.Employee();
                employee.intEmployeeID = lngEmployeeID;

                List<Products> AllProducts = new List<Products>();

                Models.Database db = new Models.Database();

                AllProducts = db.GetAllProducts();

                ViewBag.AllProducts = AllProducts;


                if (col["btnSubmit"] == "EmployeeCosts")
                {
                    string startdatecosts = col["startdatecosts"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdatecosts, out dteStartDate);

                    string enddatecosts = col["enddatecosts"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddatecosts, out dteEndDate);

                    int intBoothRental = int.Parse(col["boothrentalcosts"]);

                    bool blnInserted;

                    blnInserted = db.EnterEmployeeCost(lngEmployeeID, dteStartDate, dteEndDate, intBoothRental);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }

                if (col["btnSubmit"] == "EmployeeEarnings")
                {
                    string startdateearnings = col["startdateearnings"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdateearnings, out dteStartDate);

                    string enddateearnings = col["enddateearnings"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddateearnings, out dteEndDate);

                    int intappointmentpay = int.Parse(col["appointmentpay"]);

                    int intTipPay = int.Parse(col["tip"]);


                    bool blnInserted;

                    blnInserted = db.EnterEmployeeEarning(lngEmployeeID, dteStartDate, dteEndDate, intappointmentpay, intTipPay);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }


                if (col["btnSubmit"] == "btnInfo")
                {
                    string startdateEarningsinfo = col["startdateEarningsinfo"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdateEarningsinfo, out dteStartDate);

                    string enddateEarningsinfo = col["enddateEarningsinfo"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddateEarningsinfo, out dteEndDate);


                    bool blnInserted;

                    EmployeesMoneyInfo EmployeeInfo = new EmployeesMoneyInfo();

                    EmployeeInfo = db.EmployeeInfo(lngEmployeeID, dteStartDate, dteEndDate);

                    ViewBag.EmployeeInfo = EmployeeInfo;

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnChangeItemQuantity")
                {
                    int intItemQuantityChange = int.Parse(col["itemquantitychange"]);

                    int EmployeeProducts = int.Parse(EmployeesProducts);

                    db.UpdateEmployeeItemInventory(EmployeeProducts, intItemQuantityChange);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnAddItems")
                {
                    int intNewProductID = int.Parse(col["AddExistingItem"]);

                    int intNewProductAmount = int.Parse(col["newitemquantitychange"]);

                    db.AddEmployeeProduct(lngEmployeeID, intNewProductID, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnAddNewProduct")
                {
                    string strNewProduct = col["NewProduct"];

                    int intNewProductAmount = int.Parse(col["NewProductQuantity"]);

                    db.AddNewProduct(lngEmployeeID, strNewProduct, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeesInfo", "Profile");
                }



            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("EmployeesInfo", "Profile");
        }


        public ActionResult AdminInfo()
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


           // Models.Employee e = new Models.Employee();

            e = e.GetEmployeeSession();

            long lngEmployeeID = e.intEmployeeID;  //employees ID here

            List<SelectListItem> items = new List<SelectListItem>();

            items = GetEmployeesProducts(lngEmployeeID);

            ViewBag.EmployeesProducts = items;

            List<Products> AllProducts = new List<Products>();

            Models.Database db = new Models.Database();

            AllProducts = db.GetAllProducts();

            ViewBag.AllProducts = AllProducts;

            return View();
        }

        public List<SelectListItem> GetAdminProducts(long lngEmployeeID)
        {
            List<Models.EmployeeProducts> lstProducts = new List<Models.EmployeeProducts>();

            Models.Database db = new Models.Database();

            lstProducts = db.GetEmployeesProducts(lngEmployeeID);

            List<SelectListItem> items = new List<SelectListItem>();

            foreach (var item in lstProducts)
            {
                items.Add(new SelectListItem { Text = item.product.strProductName + ", \t Quantity =  " + item.intProductInventory, Value = item.intEmployeeProductID.ToString() });
            }

            return items;

        }


        [HttpPost]
        public ActionResult AdminInfo(FormCollection col, string EmployeesProducts)
        {
            try
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

                //Models.Employee e = new Models.Employee();

                e = e.GetEmployeeSession();

                long lngEmployeeID = e.intEmployeeID;

                List<SelectListItem> items = new List<SelectListItem>();

                items = GetEmployeesProducts(lngEmployeeID);

                ViewBag.EmployeesProducts = items;

                Models.Employee employee = new Models.Employee();
                employee.intEmployeeID = lngEmployeeID;

                List<Products> AllProducts = new List<Products>();

                Models.Database db = new Models.Database();

                AllProducts = db.GetAllProducts();

                ViewBag.AllProducts = AllProducts;


                if (col["btnSubmit"] == "btnAdminCosts")
                {
                    string startdatecosts = col["startdatecosts"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdatecosts, out dteStartDate);

                    string enddatecosts = col["enddatecosts"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddatecosts, out dteEndDate);

                    int intBoothRental = int.Parse(col["boothrentalcosts"]);

                    int intBuildingRental = int.Parse(col["buildingrental"]);

                    int intBuildingUtilities = int.Parse(col["buildingutilities"]);

                    bool blnInserted;

                    blnInserted = db.EnterAdminCosts(lngEmployeeID, dteStartDate, dteEndDate, intBoothRental, intBuildingRental, intBuildingUtilities);

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnAdminEarnings")
                {
                    string startdateearnings = col["startdateearnings"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdateearnings, out dteStartDate);

                    string enddateearnings = col["enddateearnings"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddateearnings, out dteEndDate);

                    int intappointmentpay = int.Parse(col["appointmentpay"]);

                    int intTipPay = int.Parse(col["tip"]);


                    bool blnInserted;

                    blnInserted = db.EnterEmployeeEarning(lngEmployeeID, dteStartDate, dteEndDate, intappointmentpay, intTipPay);

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }


                if (col["btnSubmit"] == "btnInfo")
                {
                    string startdateEarningsinfo = col["startdateEarningsinfo"];

                    DateTime dteStartDate;

                    DateTime.TryParse(startdateEarningsinfo, out dteStartDate);

                    string enddateEarningsinfo = col["enddateEarningsinfo"];

                    DateTime dteEndDate;

                    DateTime.TryParse(enddateEarningsinfo, out dteEndDate);


                    bool blnInserted;

                    EmployeesMoneyInfo EmployeeInfo = new EmployeesMoneyInfo();

                    EmployeeInfo = db.EmployeeInfo(lngEmployeeID, dteStartDate, dteEndDate);

                    ViewBag.EmployeeInfo = EmployeeInfo;

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnChangeItemQuantity")
                {
                    int intItemQuantityChange = int.Parse(col["itemquantitychange"]);

                    int EmployeeProducts = int.Parse(EmployeesProducts);

                    db.UpdateEmployeeItemInventory(EmployeeProducts, intItemQuantityChange);

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnAddItems")
                {
                    int intNewProductID = int.Parse(col["AddExistingItem"]);

                    int intNewProductAmount = int.Parse(col["newitemquantitychange"]);

                    db.AddEmployeeProduct(lngEmployeeID, intNewProductID, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }

                if (col["btnSubmit"] == "btnAddNewProduct")
                {
                    string strNewProduct = col["NewProduct"];

                    int intNewProductAmount = int.Parse(col["NewProductQuantity"]);

                    db.AddNewProduct(lngEmployeeID, strNewProduct, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return RedirectToAction("AdminInfo", "Profile");
                }



            }
            catch (Exception ex)
            {

            }

            return RedirectToAction("AdminInfo", "Profile");
        }

        [HttpPost]
        public ActionResult ScheduleLogin(FormCollection col)
        {
            try
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

                Customer u = new Customer();

                u.strFirstName = col["strFirstName"];
                u.strLastName = col["strLastName"];
                u.strEmailAddress = col["strEmailAddress"];
                u.strPhoneNumber = col["strPhoneNumber"];
                u.strPassword = col["strPassword"];
                u.strGender = col["strGender"];

                if (col["btnSubmit"] == "signin")
                {
                    u.strEmailAddress = col["strEmailAddress"];
                    u.strPassword = col["strPassword"];

                    u = u.LoginCustomer();
                    if (u != null && u.intCustomerID > 0)
                    {
                        u.SaveCustomerSession();
                        return RedirectToAction("CustomerIndex");
                    }
                    else
                    {
                        u = new Customer();
                        u.ActionType = Customer.ActionTypes.LoginFailed;
                    }
                }
                else if (col["btnSubmit"] == "signup")
                { //sign up button pressed
                    Customer.ActionTypes at = Customer.ActionTypes.NoType;
                    at = u.SaveCustomer();
                    switch (at)
                    {
                        case Customer.ActionTypes.InsertSuccessful:
                            u.SaveCustomerSession();
                            return RedirectToAction("CustomerIndex");
                        //break;
                        default:
                            return View(u);
                            //break;
                    }
                }
                return View(u);
            }
            catch (Exception)
            {
                Customer u = new Customer();
                return View(u);
            }
        }

        [HttpPost]
        public ActionResult Employeelogin(FormCollection col)
        {
            try
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

                Employee e2 = new Employee();

                e2.strEmailAddress = col["strEmailAddress"];
                e2.strPassword = col["strPassword"];

                if (col["btnSubmit"] == "signin")
                {
                    e2.strEmailAddress = col["strEmailAddress"];
                    e2.strPassword = col["strPassword"];

                    e2 = e2.LoginEmployee();
                    if (e2 != null && e2.intEmployeeID > 0)
                    {
                        e2.SelectEmployeeRole();

                        if (e2.strRole == "Employee")
                        {
                            e2.SaveEmployeeSession();
                            return RedirectToAction("EmployeeLoggedIn");
                        }
                        else
                        {
                            e2.SaveEmployeeSession();
                            return RedirectToAction("AdminLoggedIn");
                        }

                    }
                    else
                    {
                        e2 = new Employee();
                        e2.ActionType = Employee.ActionTypes.LoginFailed;
                    }
                }
                return View(e2);
            }
            catch (Exception)
            {
                Models.Employee e = new Models.Employee();
                return View(e);
            }
        }


        [HttpPost]
        public ActionResult ManageEmployees(FormCollection col)
        {
            try
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

                Models.Employee u = new Models.Employee();
                u.strFirstName = col["strFirstName"];
                u.strLastName = col["strLastName"];
                u.strEmailAddress = col["strEmailAddress"];
                u.strPhoneNumber = col["strPhoneNumber"];
                u.strPassword = col["strPassword"];
                if (col["btnSubmit"] == "Create Employee")
                { //Create button pressed
                    Models.Employee.ActionTypes at = Models.Employee.ActionTypes.NoType;
                    at = u.SaveEmployee();
                    switch (at)
                    {
                        case Models.Employee.ActionTypes.InsertSuccessful:
                            u.SaveEmployeeSession();
                            return RedirectToAction("ManageEmployees");
                        //break;
                        default:
                            return View(u);
                            //break;
                    }
                }
                return View(u);
            }
            catch (Exception)
            {
                Models.Employee u = new Models.Employee();
                return View(u);
            }
        }


        public ActionResult SignOut()
        {
            Models.Customer c = new Models.Customer();
            c.RemoveCustomerSession();

            Employee e = new Employee();
            e.RemoveEmployeeSession();

            return RedirectToAction("Index", "Home");
        }



    }
}
