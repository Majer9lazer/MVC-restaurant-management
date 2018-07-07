using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class ReservationController : Controller
    {
        private readonly RestaurantManagement_DB _db = new RestaurantManagement_DB();
        public async Task<ActionResult> Index()
        {
            return View(await _db.Reservations.ToListAsync());
        }
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation emploee = await _db.Reservations.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }
    }
}