using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NLog;
using NLog.Fluent;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantManagement_DB _db = new RestaurantManagement_DB();
        private Logger _logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index()
        {
            ViewBag.Title = "RestaurantManagement - Home Page";
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

        public ActionResult ChangeCulture(string lang)
        {
            var langCookie = new HttpCookie("lang", lang)
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMonths(100)
            };
            Response.AppendCookie(langCookie);

            return Redirect(Request.UrlReferrer?.ToString());
        }

        public ActionResult ReservationTable()
        {
            return View();
        }
        public ActionResult EatTypes(EatType eatType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.EatTypes.Add(eatType);
                    _db.SaveChanges();
                }
                catch (Exception e)
                {
                    _logger.Error(e.ToString);
                }
            }
            return View();
        }
    }
}