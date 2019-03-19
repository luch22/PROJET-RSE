﻿using _WebApp.Infrastructure;
using System.Web.Mvc;

namespace _WebApp.Controllers {
    public class HomeController : Controller
    {
        [AnonymousRequired]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}