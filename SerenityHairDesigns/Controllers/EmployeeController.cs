using SerenityHairDesigns.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SerenityHairDesigns.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            Database db = new Database();
            var employeesSelectList = new List<SelectListItem>();
            var lstEmployees = db.GetEmployees();
            foreach (var item in lstEmployees)
            {
                employeesSelectList.Add(new SelectListItem { Text = $"{item.strFirstName} {item.strLastName}", Value = $"{item.intEmployeeID}" });
            }
            var model = new Employee
            {
                EmployeeList = employeesSelectList
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(FormCollection formCollection)
        {
            Database db = new Database();
            var lstEmployees = db.GetEmployees();
            var id = formCollection["SelectedEmployee"];
            var selectedEmployee = lstEmployees.Find((e) => e.intEmployeeID == Int32.Parse(id));
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
            return View(employee);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection col)
        {
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
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmployee(FormCollection col)
        {
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