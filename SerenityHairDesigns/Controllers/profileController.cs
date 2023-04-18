using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Controllers
{
    public class ProfileController : Controller
    {
        // GET: profile
        public ActionResult ScheduleLogin()
        {
			Models.Customer u = new Models.Customer();
			return View(u);
        }

        public ActionResult Employeelogin()
        {
            Models.Customer u = new Models.Customer();
            return View();
        }

        public ActionResult ScheduleNowLoggedIn()
        {
            Models.Customer c = new Models.Customer();
            return View(c);
        }

        public ActionResult EmployeeLoggedIn()
        {
            Models.Employee e = new Models.Employee();
            return View(e);
        }

        public ActionResult AdminLoggedIn()
        {
            Models.Employee e = new Models.Employee();
            return View(e);
        }

        public ActionResult ManageEmployees()
        {
            Models.Employee u = new Models.Employee();
            return View(u);
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
                items.Add(new SelectListItem { Text = item.product.strProductName + ", \t Quantity =  " + item.intProductInventory, Value = item.intEmployeeProductID.ToString()});
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

                    return View();
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

                    return View();
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

                    return View();
                }

                if (col["btnSubmit"] == "btnAddItems")
                {
                    int intNewProductID = int.Parse(col["AddExistingItem"]);

                    int intNewProductAmount = int.Parse(col["newitemquantitychange"]);

                    db.AddEmployeeProduct(lngEmployeeID, intNewProductID, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return View();
                }

                if (col["btnSubmit"] == "btnAddNewProduct")
                {
                    string strNewProduct = col["NewProduct"];

                    int intNewProductAmount = int.Parse(col["NewProductQuantity"]);

                    db.AddNewProduct(lngEmployeeID, strNewProduct, intNewProductAmount);

                    col["btnSubmit"] = "";

                    return View();
                }



            }
            catch (Exception ex)
            {

            }

			return View();
		}

		[HttpPost]
		public ActionResult ScheduleLogin(FormCollection col) {
			try {
				Models.Customer u = new Models.Customer();

				u.strFirstName = col["strFirstName"];
				u.strLastName = col["strLastName"];
				u.strEmailAddress = col["strEmailAddress"];
				u.strPhoneNumber = col["strPhoneNumber"];
				u.strPassword = col["strPassword"];
				u.strGender = col["strGender"];

				if (col["btnSubmit"] == "signin") {
					u.strEmailAddress = col["strEmailAddress"];
					u.strPassword = col["strPassword"];

					u = u.LoginCustomer();
					if (u != null && u.intCustomerID > 0) {
						u.SaveCustomerSession();
                        return RedirectToAction("ScheduleNowLoggedIn");
                    }
					else {
						u = new Models.Customer();
						u.ActionType = Models.Customer.ActionTypes.LoginFailed;
					}
				}
				else if (col["btnSubmit"] == "signup") { //sign up button pressed
					Models.Customer.ActionTypes at = Models.Customer.ActionTypes.NoType;
						at = u.SaveCustomer();
						switch (at) {
							case Models.Customer.ActionTypes.InsertSuccessful:
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
			catch (Exception) {
				Models.Customer u = new Models.Customer();
				return View(u);
			}
		}

        [HttpPost]
        public ActionResult Employeelogin(FormCollection col)
        {
            try
            {
                Models.Employee e = new Models.Employee();

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
						else {
							e.SaveEmployeeSession();
							return RedirectToAction("AdminLoggedIn");
						}
                        
                    }
                    else
                    {
                        e = new Models.Employee();
                        e.ActionType = Models.Employee.ActionTypes.LoginFailed;
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


        public ActionResult SignOut() {
			Models.Customer c = new Models.Customer();
			c.RemoveCustomerSession();

			Models.Employee e = new Models.Employee();
			e.RemoveEmployeeSession();

			return RedirectToAction("Index", "Home");
		}


	}
}