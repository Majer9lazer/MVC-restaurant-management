using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
        private readonly Logger _logger = LogManager.GetCurrentClassLogger();
        public ActionResult Index(string message, bool? isLogOff)
        {
            string databaseGenerationMessage = _db.SetStandartData();
            if (databaseGenerationMessage == "0")
            {
                if (string.IsNullOrEmpty(message))
                    message = "Так как база была пустой я добавил немного данных.\n" +
                              "Чтобы не было так одиноко.";
            }
            else if (databaseGenerationMessage != "1")
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, databaseGenerationMessage);


            ViewBag.Title = "RestaurantManagement - Home Page";
            if (isLogOff != null && (bool)isLogOff)
                EmploeesController.IsLoggedIn = false;
            ViewBag.Message = message;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Мои контакты";

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
        public ActionResult EatTypes()
        {
            return View();
        }
        public ActionResult AddEatType(EatType eatType)
        {
            if (string.IsNullOrEmpty(eatType.EatTypeName))
                return RedirectToAction("EatTypeList", "Home",
                    new { message = "Вы не заполнили имя." });
            try
            {
                if (eatType.TypeId == 0)
                {
                    _db.EatTypes.Add(eatType);
                    _db.SaveChanges();
                    return RedirectToAction("EatTypeList", "Home",
                        new { message = "Данные были добавлены успешно." });
                }
                EatType findedElement = _db.EatTypes.Find(eatType.TypeId);

                if (findedElement == null)
                    return RedirectToAction("EatTypeList", "Home",
                        new { message = "Данные пришли пустыми." });

                findedElement.EatTypeName = eatType.EatTypeName;
                _db.Entry(findedElement).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("EatTypeList", "Home",
                    new { message = "Данные были изменены успешно." });

            }
            catch (Exception e)
            {
                _logger.Error(e.ToString);
                return RedirectToAction("EatTypeList", "Home", new { message = e.ToString() });
            }
        }

        public ActionResult EditEatType(int eatTypeId)
        {
            EatType findedElement = _db.EatTypes.Find(eatTypeId);
            if (findedElement != null)
                return View("EatTypes", findedElement);
            else
                return RedirectToAction("EatTypeList", "Home", new { message = "Данные пришли пустыми" });
        }
        public ActionResult EatTypeList(string message)
        {
            ViewBag.Message = message;
            return View(_db.EatTypes.ToList());
        }

        public ActionResult DeleteEatType(int eatTypeId = 0)
        {
            EatType elementToDelete = _db.EatTypes.Find(eatTypeId);
            if (elementToDelete != null)
            {
                try
                {
                    _db.EatTypes.Remove(elementToDelete);
                    _db.SaveChanges();
                    return RedirectToAction("EatTypeList", "Home", new { message = "Удалено успешено." });
                }
                catch (Exception e)
                {
                    return RedirectToAction("EatTypeList", "Home", new { message = e.ToString() });
                }

            }

            return RedirectToAction("EatTypeList", "Home", new { message = "Данные пришли пустыми." });
        }

    }
}