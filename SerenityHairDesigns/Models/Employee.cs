using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace SerenityHairDesigns.Models {
    public class Employee {
        public int intEmployeeID { get; set; }
        public string strFirstName  { get; set; }
        public string strLastName  { get; set; }
        public string strGender  { get; set; }
        public string strPassword  { get; set; }
        public string strRole { get; set; }

        //[DataType(DataType.EmailAddress)]
        public string strEmailAddress  { get; set; }

        //[DataType(DataType.PhoneNumber)]
        public string strPhoneNumber  { get; set; }

        public ActionTypes ActionType = ActionTypes.NoType;
        public Image UserImage;
        public List<Image> Images;
        public IEnumerable<SelectListItem> EmployeeList { get; set; }
        public Employee SelectedEmployee { get; set; }

        public Employee LoginEmployee() {
            try {
                Database db = new Database();
                return db.LoginEmployee(this);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public Employee SelectEmployeeRole() {
            try {
                Database db = new Database();
                return db.SelectEmployeeRole(this);
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public bool IsEmployeeAuthenticated {
            get {
                if (intEmployeeID > 0) return true;
                return false;
            }
        }

        public ActionTypes SaveEmployee() {
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

        public Employee GetEmployeeSession() {
            try {
                Employee e = new Employee();
                if (HttpContext.Current.Session["CurrentUser"] == null) {
                    return e;
                }
                e = (Employee)HttpContext.Current.Session["CurrentUser"];
                return e;
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

        public Employee.ActionTypes Save() {
            try {
                Database db = new Database();
                if (intEmployeeID == 0) { //insert new employee
                    this.ActionType = db.InsertEmployee(this);
                }
                else {
                    this.ActionType = db.UpdateEmployee(this);
                }
                return this.ActionType;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public sbyte UpdatePrimaryImage() {
            try {
                Models.Database db = new Database();
                long NewUID;
                if (this.UserImage.ImageID == 0) {
                    NewUID = db.InsertEmployeeImage(this);
                    if (NewUID > 0) UserImage.ImageID = NewUID;
                }
                else {
                    db.UpdateEmployeeImage(this);
                }
                return 0;
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