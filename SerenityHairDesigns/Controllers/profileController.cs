using SerenityHairDesigns.Models;
using System;
using System.Web.Mvc;
using System.IO;

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
            return View(e);
        }

        [HttpPost]
		public ActionResult ScheduleLogin(FormCollection col) {
			try {
                Customer u = new Customer();

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
						u = new Customer();
						u.ActionType = Customer.ActionTypes.LoginFailed;
					}
				}
				else if (col["btnSubmit"] == "signup") { //sign up button pressed
                    Customer.ActionTypes at = Customer.ActionTypes.NoType;
						at = u.SaveCustomer();
						switch (at) {
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
			catch (Exception) {
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
						else {
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

        public ActionResult ScheduleNowLoggedIn() {
			Models.Customer c = new Models.Customer();
			return View(c);
		}

        public ActionResult EmployeeLoggedIn()
        {
            Models.Employee e = new Models.Employee();
            return View(e);
        }

		public ActionResult AdminLoggedIn() {
			Models.Employee e = new Models.Employee();
			return View(e);
		}

		public ActionResult SignOut() {
			Models.Customer c = new Models.Customer();
			c.RemoveCustomerSession();

            Employee e = new Employee();
			e.RemoveEmployeeSession();

			return RedirectToAction("Index", "Home");
		}


	}
}