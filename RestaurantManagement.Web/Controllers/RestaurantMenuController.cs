using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class RestaurantMenuController : Controller
    {
        private RestaurantManagement_DB _restaurantManagementDb = new RestaurantManagement_DB();
        // GET: RestaurantMenu
        public ActionResult Index(int state = -1, string message = "")
        {
            ViewBag.state = state;
            ViewBag.message = message;
            return View();
        }

        public ActionResult SaveMenu(Item menu)
        {
            try
            {
                _restaurantManagementDb.Items.Add(menu);
                _restaurantManagementDb.SaveChanges();
                return RedirectToAction("ListMenu", "RestaurantMenu", new { state = 0, message = "Данные добавлены успешно." });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "RestaurantMenu", new { state = 1, message = e.ToString() });
            }
        }

        public ActionResult ListMenu(int state = -1, string message = "")
        {
            ViewBag.state = state;
            ViewBag.message = message;
            return View(_restaurantManagementDb.Items.OrderBy(o => o.itemid).ToList());
        }
    }
}