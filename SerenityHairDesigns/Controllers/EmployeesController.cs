using SerenityHairDesigns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace SerenityHairDesigns.Controllers
{
    public class EmployeesController : Controller
    {
        // GET: Employees
        public ActionResult Index()
        {
            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employees/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employees/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


//            // GET: Employee
//            public ActionResult Index()
//            {
//                Database db = new Database();
//                var employeesSelectList = new List<SelectListItem>();
//                var lstEmployees = db.GetEmployees();
//                foreach (var item in lstEmployees)
//                {
//                    employeesSelectList.Add(new SelectListItem { Text = $"{item.strFirstName} {item.strLastName}", Value = $"{item.intEmployeeID}" });
//                }
//                var model = new Employee
//                {
//                    EmployeeList = employeesSelectList
//                };

//                return View(model);
//            }

//            [HttpPost]
//            public ActionResult Index(FormCollection formCollection)
//            {
//                Database db = new Database();
//                var lstEmployees = db.GetEmployees();
//                var id = formCollection["SelectedEmployee"];
//                var selectedEmployee = lstEmployees.Find((e) => e.intEmployeeID == Int32.Parse(id));
//                return RedirectToAction("Edit", new { @employee = selectedEmployee });
//            }

//            public ActionResult Edit(Employee employee)
//            {
//                return View(employee);
//            }
//        }
//    }
    }
}
