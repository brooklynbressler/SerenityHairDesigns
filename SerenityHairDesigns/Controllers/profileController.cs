using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

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
            Models.Employee u = new Models.Employee();
            return View(u);
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

			e = e.GetEmployeeSession();

			if(e.IsEmployeeAuthenticated) {
				Models.Database db = new Models.Database();
				List<Models.Image> images = new List<Models.Image>();
				images = db.GetEmployeeImages(e.intEmployeeID, 0, true);
				e.UserImage = new Models.Image();
				if (images.Count > 0) e.UserImage = images[0];
			}

			return View(e);
		}

		[HttpPost]
		public ActionResult AdminLoggedIn(HttpPostedFileBase UserImage, FormCollection col) {

			try {
				Models.Employee e = new Models.Employee();
				e = e.GetEmployeeSession();

				e.strFirstName = col["strFirstName"];
				e.strLastName = col["strLastName"];
				e.strPassword = col["strPassword"];
				e.strPhoneNumber = col["strPhoneNumber"];
				e.strEmailAddress = col["strEmailAddress"];
				e.strGender = col["strGender"];
				

				if (e.strFirstName.Length == 0 || e.strLastName.Length == 0 || e.strEmailAddress.Length == 0 || e.strPassword.Length == 0) {
					e.ActionType = Models.Employee.ActionTypes.RequiredFieldsMissing;
					return View(e);
				}
				else {
					if (col["btnSubmit"] == "update") { //update button pressed
						e.Save();

						e.UserImage = new Models.Image();
						e.UserImage.ImageID = System.Convert.ToInt32(col["UserImage.ImageID"]);

						if (UserImage != null) {
							e.UserImage = new Models.Image();
							e.UserImage.ImageID = Convert.ToInt32(col["UserImage.ImageID"]);
							e.UserImage.Primary = true;
							e.UserImage.FileName = Path.GetFileName(UserImage.FileName);
							if (e.UserImage.IsImageFile()) {
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
			catch (Exception) {
				Models.Employee e = new Models.Employee();
				return View(e);
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