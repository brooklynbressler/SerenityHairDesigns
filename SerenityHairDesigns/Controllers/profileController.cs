using SerenityHairDesigns.Models;
using System;
using System.Web.Mvc;
using System.IO;
using System.Web;
using System.Collections.Generic;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Controllers
{
    public class ProfileController : Controller
    {
        // GET: profile
        public ActionResult ScheduleLogin()
        {
            Customer u = new Customer();
            return View(u);
        }

        public ActionResult Employeelogin()
        {
            Models.Customer u = new Models.Customer();
            return View();
        }

        public ActionResult ScheduleNowLoggedIn()
        {
            Customer c = new Customer();
            return View(c);
        }

        public ActionResult EmployeeLoggedIn()
        {
            Employee e = new Employee();
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
                        return RedirectToAction("ScheduleNowLoggedIn");
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
                            return RedirectToAction("ScheduleNowLoggedIn");
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
