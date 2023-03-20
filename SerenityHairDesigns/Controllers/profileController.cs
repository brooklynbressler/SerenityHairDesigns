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
			Models.User u = new Models.User();
			return View(u);
        }

		[HttpPost]
		public ActionResult ScheduleLogin(FormCollection col) {
			try {
				Models.User u = new Models.User();

				u.strFirstName = col["strFirstName"];
				u.strLastName = col["strLastName"];
				u.strEmailAddress = col["strEmailAddress"];
				u.strPassword = col["strPassword"];
				u.strGender = col["strGender"];

				if (col["btnSubmit"] == "signin") {
					u.strEmailAddress = col["strEmailAddress"];
					u.strPassword = col["strPassword"];

					u = u.Login();
					if (u != null && u.intCustomerID > 0) {
						u.SaveUserSession();
						return RedirectToAction("ScheduleNowLoggedIn");
					}
					else {
						u = new Models.User();
						u.ActionType = Models.User.ActionTypes.LoginFailed;
					}
				}
				else if (col["btnSubmit"] == "signup") { //sign up button pressed
					Models.User.ActionTypes at = Models.User.ActionTypes.NoType;
						at = u.Save();
						switch (at) {
							case Models.User.ActionTypes.InsertSuccessful:
								u.SaveUserSession();
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
				Models.User u = new Models.User();
				return View(u);
			}
		}

		public ActionResult ScheduleNowLoggedIn() {
			Models.User u = new Models.User();
			return View(u);
		}

		public ActionResult SignOut() {
			Models.User u = new Models.User();
			u.RemoveUserSession();
			return RedirectToAction("Index", "Home");
		}
	}
}