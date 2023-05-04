using SerenityHairDesigns.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;

namespace SerenityHairDesigns.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            Database db = new Database();
            var lstEmployees = db.GetEmployees();
            var id = formCollection["SelectedEmployee"];
            var selectedEmployee = lstEmployees.Find((e2) => e2.intEmployeeID == Int32.Parse(id));
            var employee = new Employee
            {
                intEmployeeID = selectedEmployee.intEmployeeID,
                strFirstName= selectedEmployee.strFirstName,
                strLastName= selectedEmployee.strLastName,  
                strEmailAddress= selectedEmployee.strEmailAddress,  
                strPassword= selectedEmployee.strPassword,
                strPhoneNumber= selectedEmployee.strPhoneNumber,
            };
            return RedirectToAction("Edit", employee);
        }

        public ActionResult Edit(Employee employee)
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection col)
        {

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            var employee = new Employee
            {
                intEmployeeID = Int32.Parse(col["intEmployeeID"]),
                strFirstName = col["strFirstName"],
                strLastName = col["strLastName"],
                strEmailAddress = col["strEmailAddress"],
                strPhoneNumber = col["strPhoneNumber"],
                strPassword = col["strPassword"],
            };
            employee.SaveEmployee();
            return RedirectToAction("Index");
        }

        public ActionResult CreateEmployee()
        {

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            return View();
        }

        public ActionResult ManageEmployees()
        {
            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();
             
            lstEmployees = db.GetEmployees();

            ViewBag.lstEmployees = lstEmployees;


            return View();
        }


        [HttpPost]
        public ActionResult ManageEmployees(FormCollection col, Employee employee)
        {

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            List<Employee> lstEmployees = new List<Employee>();
            Database db = new Database();

            lstEmployees = db.GetEmployees();


            ViewBag.lstEmployees = lstEmployees;



            employee.strFirstName = col["strFirstName"];
            employee.strLastName = col["strLastName"];
            employee.strGender = col["strGender"];
            if (employee.strGender == "Female")
            {
                employee.intGenderID = 1;
            }
            else if (employee.strGender == "Male")
            {
                employee.intGenderID = 2;

            }
            else if (employee.strGender == "General")
            {
                employee.intGenderID = 3;

            }
            employee.strEmailAddress = col["strEmailAddress"];
            employee.strPhoneNumber = col["strPhoneNumber"];
            employee.strPassword = col["strPassword"];


            try
            {
                if (col["btnSubmit"] == "edit")
                {
                    string strEmployeeID = col["Employees"];
                    int intEmployeeID = Convert.ToInt32(strEmployeeID);

                    employee.intEmployeeID = intEmployeeID;

                    employee = db.SelectEmployeeDropDownList(employee, intEmployeeID);



                    return View(employee);
                }
                else if (col["btnSubmit"] == "update")
                {
                    employee.Save();


                    lstEmployees = db.GetEmployees();

                    ViewBag.lstEmployees = lstEmployees;


                    return View(employee);
                }
                else    if (col["btnSubmit"] == "Create")
                {


                    db.InsertEmployee(employee);

                    lstEmployees = db.GetEmployees();



                    ViewBag.lstEmployees = lstEmployees;


                    RedirectToAction("ManageEmployees");
                    return View(employee);
                }
            }

            catch
            {
            }

            return View();
        }
    

            [HttpPost]
            //this works
        public ActionResult CreateEmployee(FormCollection col)
        {

            Employee e = new Employee();
            e = e.GetEmployeeSession();

            if (e.intEmployeeID == 0)
            {

                ViewBag.blnIsEmployee = 0;
                Customer c = new Customer();
                c = c.GetCustomerSession();

                if (c.intCustomerID > 0)
                {
                    ViewBag.blnIsCustomer = 1;
                }
                else
                {
                    ViewBag.blnIsCustomer = 0;
                }

            }
            else
            {
                e = e.SelectEmployeeRole();
                ViewBag.blnIsEmployee = 1;
                if (e.strRole == "Admin")
                {
                    ViewBag.IsAdmin = 1;
                }
                else
                {
                    ViewBag.IsAdmin = 0;
                }
            }

            var employee = new Employee
            {
                strFirstName = col["strFirstName"],
                strLastName = col["strLastName"],
                strEmailAddress = col["strEmailAddress"],
                strPhoneNumber = col["strPhoneNumber"],
                strPassword = col["strPassword"],
            };
            employee.SaveEmployee();
            return RedirectToAction("Index");
        }
    }
}