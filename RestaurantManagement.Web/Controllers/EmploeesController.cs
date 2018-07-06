using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class EmploeesController : Controller
    {
        private RestaurantManagement_DB db = new RestaurantManagement_DB();

        // GET: Emploees
        public async Task<ActionResult> Index()
        {
            var emploees = db.Emploees.Include(e => e.Job);
            return View(await emploees.ToListAsync());
        }

        // GET: Emploees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await db.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }

        // GET: Emploees/Create
        public ActionResult Create()
        {
            ViewBag.Jobid = new SelectList(db.Jobs, "Jobid", "Jobtype");
            return View();
        }

        // POST: Emploees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Empid,Fullname,Dob,Education,Experience,Phonenumber,Address,Skills,Username,Emppassword,Jobid")] Emploee emploee)
        {
            if (ModelState.IsValid)
            {
                db.Emploees.Add(emploee);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Jobid = new SelectList(db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
            return View(emploee);
        }

        // GET: Emploees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await db.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Jobid = new SelectList(db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
            return View(emploee);
        }

        // POST: Emploees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Empid,Fullname,Dob,Education,Experience,Phonenumber,Address,Skills,Username,Emppassword,Jobid")] Emploee emploee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emploee).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Jobid = new SelectList(db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
            return View(emploee);
        }

        // GET: Emploees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await db.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }

        // POST: Emploees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Emploee emploee = await db.Emploees.FindAsync(id);
            db.Emploees.Remove(emploee);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
