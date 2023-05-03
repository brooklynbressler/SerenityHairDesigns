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
            Customer C = new Customer();
            C = C.GetCustomerSession();
            List<Appointments> lstAppointments = new List<Appointments>();
            Database db = new Database();

            lstAppointments = db.GetAppointments(C.intCustomerID);

            //List<Appointments> sortedList = lstAppointments.OrderByDescending(x => x.intAppointmentID).ToList();

            ViewBag.lstAppointments = sortedList;
    
            return View(C); 
            //ViewBag.lstAppointments = sortedList;



            return View(C);
        }
        // GET: profile
        public ActionResult ScheduleLogin()
        {
            Customer u = new Customer();
            return View(u);
        }

        public ActionResult Employeelogin()
        {
            return View();
        }

        public ActionResult EmployeeScheduleAppointment()
        {
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

            Models.Employee e = new Models.Employee();

            e = e.GetEmployeeSession();

            long lngEmployeeID = e.intEmployeeID;  //employees ID here

            Customer customer = new Customer();

            customer.strFirstName = col["firstname"];
            customer.strLastName = col["lastname"];
            customer.strPhoneNumber = col["Number"];
            int intGender = int.Parse(col["Gender"]);

            db.InsertCustomerManually(customer, intGender, lngEmployeeID );

            customer = db.GetLastCustomer();

            db.InsertAppointment(appointment, customer, lngEmployeeID, intServiceID);

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
            List<AppointmentTypes> at = new List<AppointmentTypes>();
            List<Services> s = new List<Services>();

            Database db = new Database();

			at = db.GetAppointmentTypes();
			s = db.GetServiceTypes();

            ViewBag.AppointmentTypes = at;
            ViewBag.ServiceTypes = s;

            return View();

        }


        //[HttpPost]
        //public JsonResult GetEmployeeById(int id)
        //{
        //    Database db = new Database();
        //    //var employees = db.GetEmployees();
        //    //var employee = employees.Find((e) => e.intEmployeeID == id);
        //    //return Json(employee);
        //}

        public ActionResult AdminLoggedIn()
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();
            if (e.IsEmployeeAuthenticated)
            {
                Database db = new Database();
                List<Image> images = new List<Image>();
                images = db.GetEmployeeImages(e.intEmployeeID, 0, true);
                e.UserImage = new Image();
                if (images.Count > 0) e.UserImage = images[0];
            }
            return View(e);
        }

        [HttpPost]
        public ActionResult AdminLoggedIn(HttpPostedFileBase UserImage, FormCollection col)
        {
            try
            {
                Employee e = new Employee();
                e = e.GetEmployeeSession();

                e.strFirstName = col["strFirstName"];
                e.strLastName = col["strLastName"];
                e.strPassword = col["strPassword"];
                e.strPhoneNumber = col["strPhoneNumber"];
                e.strEmailAddress = col["strEmailAddress"];
                e.strGender = col["strGender"];
                e.strYearsOfExperience = col["strYearsOfExperience"];


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

                    else if (col["btnSubmit"] == "EditAvailability")
                    {



                        string dteStartTime = col["dteStartDate"];


                        DateTime dteStartDate;

                        DateTime.TryParse(dteStartTime, out dteStartDate);


                        string dteEndTime = col["dteEndDate"];

                        DateTime dteEndDate;

                        DateTime.TryParse(dteEndTime, out dteEndDate);



                        List<DateTime> TimeSlots = new List<DateTime>();

                        DateTime interval = dteStartDate;

                        while (interval <= dteEndDate)
                        {
                            TimeSlots.Add(interval);
                            interval = interval.AddMinutes(30);

                        }

                        Models.Employee emp = new Models.Employee();

                        emp = emp.GetEmployeeSession();

                        long lngEmployeeID = emp.intEmployeeID;  //employees ID here

                        foreach (DateTime item in TimeSlots)
						{
                            

						}


                        e.SaveEmployeeSession();
                        return RedirectToAction("AdminLoggedIn", "Profile");

                    }
                    return View(e);
                }
            }
            catch (Exception)
            {
                Employee e = new Employee();
                return View(e);
            }
        }

        public ActionResult EmployeeLoggedIn()
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();
            if (e.IsEmployeeAuthenticated)
            {
                Database db = new Database();
                List<Image> images = new List<Image>();
                images = db.GetEmployeeImages(e.intEmployeeID, 0, true);
                e.UserImage = new Image();
                if (images.Count > 0) e.UserImage = images[0];

                // Get the list of skills for the employee
                List<string> skills = db.SelectEmployeeSkill(e);

                ViewBag.skills = skills;

            }

            return View(e);
        }


        [HttpPost]
        public ActionResult EmployeeLoggedIn(HttpPostedFileBase UserImage, FormCollection col)
        {
            try
            {
                Employee e = new Employee();
                e = e.GetEmployeeSession();

				Database db = new Database();
				// Get the list of skills for the employee
				List<string> skills = db.SelectEmployeeSkill(e);
				ViewBag.skills = skills;

				// Update employee details
				e.strFirstName = col["strFirstName"];
                e.strLastName = col["strLastName"];
                e.strPassword = col["strPassword"];
                e.strPhoneNumber = col["strPhoneNumber"];
                e.strEmailAddress = col["strEmailAddress"];
                e.strGender = col["strGender"];
                e.strYearsOfExperience = col["strYearsOfExperience"];
                e.strSkillName = col["strSkillName"];

                string startdate = col["dtmStartTime"];
                DateTime dteStartDate;
                DateTime.TryParse(startdate, out dteStartDate);
                e.dtmStartTime = dteStartDate;

                string enddate = col["dtmEndTime"];
                DateTime dteEndDate;
                DateTime.TryParse(enddate, out dteEndDate);
                e.dtmEndTime = dteEndDate;

                  if (e.strFirstName.Length == 0 || e.strLastName.Length == 0 || e.strEmailAddress.Length == 0 || e.strPassword.Length == 0)
                   {
                        e.ActionType = Models.Employee.ActionTypes.RequiredFieldsMissing;
                        return View(e);
                    }
                    else
                    {
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
                            db.InsertAvailability(e);

                            e.SaveEmployeeSession();
                            return RedirectToAction("EmployeeLoggedIn", "Profile");
                        }

                        return View(e);
                    }
                }

            catch (Exception)
            {
                Employee e = new Employee();
                return View(e);
            }
        }


		public ActionResult EmployeesInfo()
        {

            Models.Employee e = new Models.Employee();

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
                Models.Employee e = new Models.Employee();

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

            Models.Employee e = new Models.Employee();

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
                Models.Employee e = new Models.Employee();

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

                e.strEmailAddress = col["strEmailAddress"];
                e.strPassword = col["strPassword"];

                if (col["btnSubmit"] == "signin")
                {
                    e.strEmailAddress = col["strEmailAddress"];
                    e.strPassword = col["strPassword"];

                    e = e.LoginEmployee();
                    if (e != null && e.intEmployeeID > 0)
                    {
                        e.SelectEmployeeRole();

                        if (e.strRole == "Employee")
                        {
                            e.SaveEmployeeSession();
                            return RedirectToAction("EmployeeLoggedIn");
                        }
                        else
                        {
                            e.SaveEmployeeSession();
                            return RedirectToAction("AdminLoggedIn");
                        }

                    }
                    else
                    {
                        e = new Employee();
                        e.ActionType = Employee.ActionTypes.LoginFailed;
                    }
                }
                return View(e);
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
