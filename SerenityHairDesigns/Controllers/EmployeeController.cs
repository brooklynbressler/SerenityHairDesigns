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
            return RedirectToAction("Edit", new { @employee = selectedEmployee});
        }

        public ActionResult Edit(Employee employee)
        {
            return View(employee);
        }
    }
}