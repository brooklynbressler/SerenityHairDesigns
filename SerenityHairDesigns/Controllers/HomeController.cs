﻿using SerenityHairDesigns.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;



namespace SerenityHairDesigns.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        // GET: Careers
        public ActionResult Careers() { 


            return View(new Resume());
        }


        [HttpPost]
        public ActionResult Careers(Resume model, IFormFile File )
        {
            EmailSender sender = new EmailSender();

			sender.SendEmail(model, File);

            return View();
        }


        // GET: Services
        public ActionResult Services()
        {
            return View();
        }

        public ActionResult AboutUs()
        {
            List<AboutUs> lstReviews = new List<AboutUs>();
            Database db = new Database();

            lstReviews = db.GetReviews();

            return View(lstReviews);
        }

        public ActionResult ContactUs()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult ContactUs(ContactUs model)
        {
            Database db = new Database();

            db.InsertReview(model);

            return View();
        }

        public ActionResult BookNow()
        {
            return View();
        }

        public ActionResult login()
        {
            return View();
        }



	}
}