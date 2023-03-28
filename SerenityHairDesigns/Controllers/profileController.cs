using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        [HttpPost]
		public ActionResult ScheduleLogin(FormCollection col) {
			try {
				Models.Customer u = new Models.Customer();

				u.strFirstName = col["strFirstName"];
				u.strLastName = col["strLastName"];
				u.strEmailAddress = col["strEmailAddress"];
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
                        e.SaveEmployeeSession();
                        return RedirectToAction("EmployeeLoggedIn");
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

        public ActionResult ScheduleNowLoggedIn() {
			Models.Customer u = new Models.Customer();
			return View(u);
		}

        public ActionResult EmployeeLoggedIn()
        {
            Models.Customer u = new Models.Customer();
            return View(u);
        }

        public ActionResult SignOut() {
			Models.Customer u = new Models.Customer();
			u.RemoveCustomerSession();
			return RedirectToAction("Index", "Home");
		}
	}
}