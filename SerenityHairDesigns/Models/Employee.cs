using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.ComponentModel.DataAnnotations;
using SerenityHairDesigns.Models;

namespace SerenityHairDesigns.Models {
	public class Employee {

        public long intEmployeeID = 0;
        public string strFirstName = string.Empty;
        public string strLastName = string.Empty;
        public string strGender = string.Empty;
        public string strPassword = string.Empty;

        //[DataType(DataType.EmailAddress)]
        public string strEmailAddress = string.Empty;

        //[DataType(DataType.PhoneNumber)]
        public string strPhoneNumber = string.Empty;

        public ActionTypes ActionType = ActionTypes.NoType;
        public Image UserImage;
        public List<Image> Images;

		public Employee LoginEmployee() {
            try {
                Database db = new Database();
                return db.LoginEmployee(this);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool IsEmployeeAuthenticated {
            get {
                if (intEmployeeID > 0) return true;
                return false;
            }
        }

        public Employee.ActionTypes SaveEmployee() {
            try {
                Database db = new Database();
                if (intEmployeeID == 0) { //insert new user
                    this.ActionType = db.InsertEmployee(this);
                }
                else {
                    this.ActionType = db.UpdateEmployee(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool RemoveEmployeeSession() {
            try {
                HttpContext.Current.Session["CurrentUser"] = null;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Customer GetEmployeeSession() {
            try {
                Customer u = new Customer();
                if (HttpContext.Current.Session["CurrentUser"] == null) {
                    return u;
                }
                u = (Customer)HttpContext.Current.Session["CurrentUser"];
                return u;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool SaveEmployeeSession() {
            try {
                HttpContext.Current.Session["CurrentUser"] = this;
                return true;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public enum ActionTypes {
            NoType = 0,
            InsertSuccessful = 1,
            UpdateSuccessful = 2,
            DuplicateEmail = 3,
            DuplicateUserID = 4,
            Unknown = 5,
            RequiredFieldsMissing = 6,
            LoginFailed = 7
        }
    }
}