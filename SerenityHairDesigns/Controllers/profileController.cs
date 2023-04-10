using SerenityHairDesigns.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

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
            Customer u = new Customer();
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
            var employee = employees.Find((e) => e.EmployeeId == id);
            return Json(employee);
        }

        public ActionResult AdminLoggedIn()
        {
            Employee e = new Employee();
            return View(e);
        }

        public ActionResult ManageEmployees()
        {
           Database db = new Database();
            var employeesSelectList = new List<SelectListItem>();
            var lstEmployees = db.GetEmployees();
            foreach (var item in lstEmployees)
            {
                employeesSelectList.Add(new SelectListItem { Text = $"{item.strFirstName} {item.strLastName}", Value = $"{item.EmployeeId}" });
            }
            ViewBag.lstEmployees = lstEmployees;
            var model = new Employee
            {
                EmployeeList = employeesSelectList
            };


            return View(model);

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
                Employee e = new Employee();
                return View(e);
            }
        }

        [HttpPost]
        public ActionResult ManageEmployees(FormCollection col)
        {
            var fuckingSelectedItemMaybe = col["SelectedEmployee"];

            try
            {
                Employee u = new Employee
                {
                    strFirstName = col["strFirstName"],
                    strLastName = col["strLastName"],
                    strEmailAddress = col["strEmailAddress"],
                    strPhoneNumber = col["strPhoneNumber"],
                    strPassword = col["strPassword"]
                };
                if (col["btnSubmit"] == "Create Employee")
                { //Create button pressed
                    Employee.ActionTypes at = Employee.ActionTypes.NoType;
                    at = u.SaveEmployee();
                    switch (at)
                    {
                        case Employee.ActionTypes.InsertSuccessful:
                            u.SaveEmployeeSession();
                            return RedirectToAction("ManageEmployees");
                        //break;
                        default:
                            return View(u);
                            //break;
                    }
                }
                if (col["Employees"] == "intEmployeeID")
                {
                    Employee objEmployee = new Employee();
                    Database db = new Database();
                    //make method that pulls id from the drop down
                    //objEmployee = db.GetEmployees();

                    ViewBag.objEmployee = objEmployee;

                    return View();
                }
                return View();
            }
            

                catch (Exception)
                {
                Employee u = new Employee();
                    return View(u);
                }
            
        }

		public ActionResult AdminLoggedIn() {
			Models.Employee e = new Models.Employee();
			return View(e);
		}



        public ActionResult SignOut() {
            Customer c = new Customer();
			c.RemoveCustomerSession();

            Employee e = new Employee();
			e.RemoveEmployeeSession();

			return RedirectToAction("Index", "Home");
		}


	}
}