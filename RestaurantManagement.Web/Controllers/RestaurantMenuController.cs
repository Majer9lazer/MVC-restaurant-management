using System;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using NLog.Fluent;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class RestaurantMenuController : Controller
    {
        private readonly RestaurantManagement_DB _restaurantManagementDb = new RestaurantManagement_DB();
        public ActionResult Index(int state = -1, string message = "")
        {
            ViewBag.state = state;
            ViewBag.message = message;
            return View();
        }

        public ActionResult SaveMenu(Item menu)
        {
            if (!ModelState.IsValid)
                return RedirectToAction("Index", "RestaurantMenu", new { state = 1, message = "Вы заполнили не все поля." });
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
            return View(_restaurantManagementDb.Items.OrderBy(o => o.Itemid).ToList());
        }

        public ActionResult DeleteMenu(int menuItemId = 0)
        {
            if (menuItemId == 0)
                return RedirectToAction("ListMenu", "RestaurantMenu", new
                {
                    state = 1,
                    message = "Даные пришли пустыми"
                });
            try
            {
                Item findedItem = _restaurantManagementDb.Items.Find(menuItemId);
                if (findedItem == null)
                    return RedirectToAction("ListMenu", "RestaurantMenu", new { state = 1, message = "В базе данных нет такой записи." });

                _restaurantManagementDb.Items.Remove(findedItem);
                _restaurantManagementDb.SaveChanges();
                return RedirectToAction("ListMenu", "RestaurantMenu", new
                {
                    state = 0,
                    message = "Данные были удалены успешно."
                });
            }
            catch (Exception e)
            {
                return RedirectToAction("ListMenu", "RestaurantMenu", new
                {
                    state = 1,
                    message = e.ToString()
                });
            }

        }

        public ActionResult ReserveTable(Reservation reservation, string email)
        {
            if (ModelState.IsValid)
            {
                reservation.Guestid = Guid.NewGuid();
                try
                {
                    _restaurantManagementDb.Reservations.Add(reservation);
                    _restaurantManagementDb.SaveChanges();
                    return RedirectToAction("Index", "Home", new { message = "Ваша заявка принята, ждите ответа от смс" });
                }
                catch (Exception e)
                {
                    return RedirectToAction("Index", "Home", new { message = e.ToString() });
                }
            }
            ViewData["typeid"] = _restaurantManagementDb.EatTypes.Select(s => new SelectListItem
            {
                Value = s.TypeId.ToString(),
                Text = s.EatTypeName
            }).ToList();


            return View();
        }

    }
}