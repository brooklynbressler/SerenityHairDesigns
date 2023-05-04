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
            //ViewBag.lstAppointments = sortedList;
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

            e = e.GetEmployeeSession();

            long lngEmployeeID = e.intEmployeeID;

            List<Appointments> a = new List<Appointments>();

            a = db.GetEmployeeAppointments(lngEmployeeID);

            ViewBag.Appointments = a;

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

            List<Appointments> a = new List<Appointments>();

            string date = col["AppointmentDateTime"];

            DateTime dteDate;

            DateTime.TryParse(date, out dteDate);

            appointment.dtmAppointmentDate = dteDate;

            string strServiceID = "";
            int intServiceID = 0;

            strServiceID = col["Services"];

            intServiceID = Convert.ToInt32(strServiceID);

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

            a = db.GetEmployeeAppointments(lngEmployeeID);

            ViewBag.Appointments = a;

            Customer customer = new Customer();

            if(col["btnSubmit"] == "btnScheduleAppointment") 
            {
                customer.strFirstName = col["firstname"];
                customer.strLastName = col["lastname"];
                customer.strPhoneNumber = col["Number"];
                int intGender = int.Parse(col["Gender"]);

                db.InsertCustomerManually(customer, intGender, lngEmployeeID);

                customer = db.GetLastCustomer();

                db.InsertAppointmentNoPic(appointment, customer, lngEmployeeID, intServiceID);

                a = db.GetEmployeeAppointments(lngEmployeeID);

                ViewBag.Appointments = a;
            } else if (col["btnSubmit"] == "btnCancelAppointment") 
            {
                appointment.intAppointmentID = Convert.ToInt32(col["Appointment"]);

                db.CancelAppointment(appointment.intAppointmentID);

                a = db.GetEmployeeAppointments(lngEmployeeID);

                ViewBag.Appointments = a;

                RedirectToAction("EmployeeScheduleAppointment", "Profile");
            }

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

        //[HttpPost]
        //public JsonResult GetEmployeeById(int id)
        //{
        //    Database db = new Database();
        //    //var employees = db.GetEmployees();
        //    //var employee = employees.Find((e) => e.intEmployeeID == id);
        //    //return Json(employee);
        //}
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
                    return RedirectToAction("CustomerIndex", "profile");


                }


                return View();

            }
            catch (Exception ex)
			{
				Customer c = new Customer();
				return View(c);
			}

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

            // Get the list of skills for the employee
            List<string> skills = db.SelectEmployeeSkill(e);

            ViewBag.skills = skills;

            List<Schedules> Schedules = new List<Schedules>();

            DateTime CurrentDate = DateTime.Now;

            Schedules = db.GetEmployeesSchedule(e.intEmployeeID, CurrentDate);
            
            ViewBag.Schedules = Schedules;



            return View(e);
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

                List<Schedules> Schedules2 = new List<Schedules>();
                Database db = new Database();
                DateTime CurrentDate = DateTime.Now;

                // Get the list of skills for the employee
                List<string> skills = db.SelectEmployeeSkill(e);

                ViewBag.skills = skills;


                Schedules2 = db.GetEmployeesSchedule(e.intEmployeeID, CurrentDate);

                ViewBag.Schedules = Schedules;

                Employee e2 = new Employee();
                e2 = e2.GetEmployeeSession();

                e2.strFirstName = col["strFirstName"];
                e2.strLastName = col["strLastName"];
                e2.strPassword = col["strPassword"];
                e2.strPhoneNumber = col["strPhoneNumber"];
                e2.strEmailAddress = col["strEmailAddress"];
                e2.strGender = col["strGender"];
                e2.strYearsOfExperience = col["strYearsOfExperience"];

                if (e2.strGender == "Female")
                {
                    e2.intGenderID = 1;
                }
                else if (e2.strGender == "Male")
                {
                    e2.intGenderID = 2;

                }
                else if (e2.strGender == "General")
                {
                    e2.intGenderID = 3;

                }


                if (e2.strFirstName.Length == 0 || e2.strLastName.Length == 0 || e2.strEmailAddress.Length == 0 || e2.strPassword.Length == 0)
                {
                    e2.ActionType = Models.Employee.ActionTypes.RequiredFieldsMissing;
                    return View(e2);
                }
                else
                {
                    if (col["btnSubmit"] == "update")
                    { //update button pressed
                        e2.Save();

                        e2.UserImage = new Image();
                        e2.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);

                        if (UserImage != null)
                        {
                            e2.UserImage = new Image();
                            e2.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);
                            e2.UserImage.Primary = true;
                            e2.UserImage.FileName = Path.GetFileName(UserImage.FileName);
                            if (e2.UserImage.IsImageFile())
                            {
                                e2.UserImage.Size = UserImage.ContentLength;
                                Stream stream = UserImage.InputStream;
                                BinaryReader binaryReader = new BinaryReader(stream);
                                e2.UserImage.ImageData = binaryReader.ReadBytes((int)stream.Length);
                                e2.UpdatePrimaryImage();
                            }
                        }


                        e2.SaveEmployeeSession();
                        return RedirectToAction("AdminLoggedIn", "Profile");
                    }

                    else if (col["btnSubmit"] == "btnCancelSchedule")
					{

                        int intScheduleID = Convert.ToInt32(col["Schedules"]);

                        db.DeleteSchedule(intScheduleID);

                        col["btnSubmit"] = "";

                        return RedirectToAction("AdminLoggedIn", "Profile");


                    }

                    else if (col["btnSubmit"] == "EditAvailability")
                    {
                        string dteStartTime = col["dtmStartTime"];


                        DateTime dteStartDate;

                        DateTime.TryParse(dteStartTime, out dteStartDate);


                        string dteEndTime = col["dtmEndTime"];

                        DateTime dteEndDate;

                        DateTime.TryParse(dteEndTime, out dteEndDate);

                        Models.Employee emp = new Models.Employee();

                        emp = emp.GetEmployeeSession();

                        long lngEmployeeID = emp.intEmployeeID;  //employees ID here

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


                        db.InsertAvailability(dteStartDate, dteEndDate, lngEmployeeID);

                        col["btnSubmit"] = "";


                        e2.SaveEmployeeSession();
                        return RedirectToAction("AdminLoggedIn", "Profile");

                    }
                    return View(e2);
                }
            }
            catch (Exception ex)
            {
                Employee e = new Employee();
                return View(e);
            }
        }

        public ActionResult EmployeeLoggedIn()
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

            Employee e2 = new Employee();
            e2 = e2.GetEmployeeSession();

            Database db = new Database();

            if (e2.IsEmployeeAuthenticated)
            {
                List<Image> images = new List<Image>();
                images = db.GetEmployeeImages(e.intEmployeeID, 0, true);
                e2.UserImage = new Image();
                if (images.Count > 0) e2.UserImage = images[0];

                // Get the list of skills for the employee
                List<string> skills = db.SelectEmployeeSkill(e);

                ViewBag.skills = skills;

            }

            List<Schedules> Schedules = new List<Schedules>();

            DateTime CurrentDate = DateTime.Now;

            Schedules = db.GetEmployeesSchedule(e.intEmployeeID, CurrentDate);

            ViewBag.Schedules = Schedules;

            return View(e);
        }





        [HttpPost]
        public ActionResult EmployeeLoggedIn(HttpPostedFileBase UserImage, FormCollection col, Employee Employee)
        {
            try
            {
                Employee e = new Employee();
                e = e.GetEmployeeSession();

                Employee = Employee.GetEmployeeSession();

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


				//Employee e = new Employee();
				//e = e.GetEmployeeSession();

				Database db = new Database();
                // Get the list of skills for the employee
                List<string> skills = db.SelectEmployeeSkill(e);
                ViewBag.skills = skills;


                if (col["btnSubmit"] == "btnCancelSchedule")
                {

                    int intScheduleID = Convert.ToInt32(col["Schedules"]);

                    db.DeleteSchedule(intScheduleID);

                    col["btnSubmit"] = "";

                    return RedirectToAction("EmployeeLoggedIn", "Profile");


                }

                // Update employee details
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
				e.strYearsOfExperience = col["strYearsOfExperience"];
                e.strSkillName = col["strSkillName"];
                e = e.GetEmployeeSession();

                

                //Customer c = new Customer();
                //c = c.GetCustomerSession();

                string startdate = col["dtmStartTime"];
                DateTime dteStartDate;
                DateTime.TryParse(startdate, out dteStartDate);
                e.dtmStartTime = dteStartDate;

                string enddate = col["dtmEndTime"];
                DateTime dteEndDate;
                DateTime.TryParse(enddate, out dteEndDate);
                e.dtmEndTime = dteEndDate;

                string strAddSkill = col["txtAddSkill"];
                string strDeleteSkill = col["Skills"];

                  if (e.strFirstName.Length == 0 || e.strLastName.Length == 0 || e.strEmailAddress.Length == 0 || e.strPassword.Length == 0)
                   {
                        e.ActionType = Models.Employee.ActionTypes.RequiredFieldsMissing;
                        return View(e);
                    }
                  else
                    {
                        if (col["btnSubmit"] == "AddSkills")
                        {
                            db.InsertSkill(e, strAddSkill);
						    skills = db.SelectEmployeeSkill(e);
						    ViewBag.skills = skills;
						   
                            e.SaveEmployeeSession();
                            return RedirectToAction("EmployeeLoggedIn", "Profile");
                    
					    }

                    if (col["btnSubmit"] == "DeleteSkills")
                    {
                        db.DeleteSkill(strDeleteSkill, e);
						skills = db.SelectEmployeeSkill(e);
						ViewBag.skills = skills;
						RedirectToAction("EmployeeLoggedIn", "Profile");

					}

                       if (col["btnSubmit"] == "update")
                       {
                            // Update employee details in the database
                            e.Save();

                            //// Update employee skills in the database
                            //db.UpdateEmployeeSkills(e);

                            // Update employee image if provided
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
                        return RedirectToAction("EmployeeLoggedIn", "Profile");
                        }




                    if (col["btnSubmit"] == "EditAvailability")
                        {

                            db.InsertAvailabilitySchedule(e.dtmStartTime, e.dtmEndTime, e.intEmployeeID);

                            List<DateTime> TimeSlots = new List<DateTime>();

                            DateTime interval = dteStartDate;

                            while (interval <= e.dtmEndTime)
                            {
                                TimeSlots.Add(interval);
                                interval = interval.AddMinutes(30);

                            }

                            foreach (var item in TimeSlots)
                            {

                                db.InsertAvailability(item, item.AddMinutes(29), e.intEmployeeID);

                            }

                        e.SaveEmployeeSession();


                            return RedirectToAction("EmployeeLoggedIn", "Profile");
                        }

                        return View(e);
                    }
                }

            catch (Exception ex )
            {
                Employee e = new Employee();
                return View(e);
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

                    return View();
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
                Customer C = new Customer();
                C = C.GetCustomerSession();
                List<Appointments> lstAppointments = new List<Appointments>();
                Database db = new Database();

                lstAppointments = db.GetAppointments(C.intCustomerID);

                List<Appointments> sortedList = lstAppointments.OrderByDescending(x => x.intAppointmentID).ToList();

                ViewBag.lstAppointments = sortedList;

                Customer c = new Customer();
                c = c.GetCustomerSession();

                c.strFirstName = col["strFirstName"];
                c.strLastName = col["strLastName"];
                c.strPassword = col["strPassword"];
                c.strPhoneNumber = col["strPhoneNumber"];
                c.strEmailAddress = col["strEmailAddress"];
                c.strGender = col["strGender"];
                if (c.strGender == "Female")
                {
                    c.intGenderID = 1;
                }
                else if (c.strGender == "Male")
                {
                    c.intGenderID = 2;

                }
                else if (c.strGender == "General")
                {
                    c.intGenderID = 3;

                }

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

            return RedirectToAction("AboutUs", "Home");
        }



    }
}
