using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Controllers
{
    public class profileController : Controller
    {
        // GET: profile
        public ActionResult ScheduleLogin()
        {
            return View();
        }

		public ActionResult SignIn() {
			web2.Models.User u = new web2.Models.User();
			return View(u);
		}

		[HttpPost]
		public ActionResult SignIn(FormCollection col) {
			try {
				web2.Models.User u = new web2.Models.User();

				if (col["btnSubmit"] == "signin") {
					u.UserID = col["UserID"];
					u.Password = col["Password"];

					u = u.Login();
					if (u != null && u.UID > 0) {
						u.SaveUserSession();
						return RedirectToAction("Index");
					}
					else {
						u = new web2.Models.User();
						u.UserID = col["UserID"];
						u.ActionType = web2.Models.User.ActionTypes.LoginFailed;
					}
				}
				return View(u);
			}
			catch (Exception) {
				web2.Models.User u = new web2.Models.User();
				return View(u);
			}
		}

		public ActionResult SignUp() {
			web2.Models.User u = new web2.Models.User();
			return View(u);
		}

		[HttpPost]
		public ActionResult SignUp(FormCollection col) {
			try {
				web2.Models.User u = new web2.Models.User();

				u.FirstName = col["FirstName"];
				u.LastName = col["LastName"];
				u.Email = col["Email"];
				u.UserID = col["UserID"];
				u.Password = col["Password"];
				u.Gender = col["Genders"];

				if (u.FirstName.Length == 0 || u.LastName.Length == 0 || u.Email.Length == 0 || u.UserID.Length == 0 || u.Password.Length == 0) {
					u.ActionType = web2.Models.User.ActionTypes.RequiredFieldsMissing;
					return View(u);
				}
				else {
					if (col["btnSubmit"] == "signup") { //sign up button pressed
						web2.Models.User.ActionTypes at = web2.Models.User.ActionTypes.NoType;
						at = u.Save();
						switch (at) {
							case web2.Models.User.ActionTypes.InsertSuccessful:
								u.SaveUserSession();
								return RedirectToAction("Index");
							//break;
							default:
								return View(u);
								//break;
						}
					}
					else {
						return View(u);
					}
				}
			}
			catch (Exception) {
				web2.Models.User u = new web2.Models.User();
				return View(u);
			}
		}

		public ActionResult SignOut() {
			web2.Models.User u = new web2.Models.User();
			u.RemoveUserSession();
			return RedirectToAction("Index", "Home");
		}
	}
}