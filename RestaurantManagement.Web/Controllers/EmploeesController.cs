using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using RestaurantManagement.DAL.Model;

namespace RestaurantManagement.Web.Controllers
{
    public class EmploeesController : Controller
    {
        private readonly RestaurantManagement_DB _db = new RestaurantManagement_DB();
        public static Emploee StaticEmployee = new Emploee();
        public static bool IsLoggedIn;
        // GET: Emploees
        public async Task<ActionResult> Index(string message)
        {
            ViewBag.Message = message;
            return View(await _db.Emploees.Include(e => e.Job).ToListAsync());
        }

        // GET: Emploees/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await _db.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            return View(emploee);
        }

        // GET: Emploees/Create
        public ActionResult Create()
        {
            ViewBag.Jobid = new SelectList(_db.Jobs, "Jobid", "Jobtype");
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
                if (_db.Emploees.Any(f => f.Username == emploee.Username))
                {
                    emploee = _db.Emploees.FirstOrDefault(f =>
                        f.Username == emploee.Username && f.Emppassword == emploee.Emppassword);
                    string mailMessage = await _db.SendMessage(emploee?.Username, $"Ваш логин : {emploee?.Username}\n" +
                                                                                 $"Пароль : {emploee?.Emppassword}");
                    return RedirectToAction("Index", "Home", new
                    {
                        message = $"Уважаемый(ая) {emploee?.Fullname}.\n" +
                                  $"Такой аккаунт с логином ({emploee?.Username}) уже существует\n" +
                                  $"Попробуйте войти снова\n" +
                                  $"На вашу почту был выслан ваш пароль\n" +
                                  $"Информация по состоянию отправки сообщения - {mailMessage}"
                    });
                }
                _db.Emploees.Add(emploee);
                await _db.SaveChangesAsync();
                string message = await _db.SendMessage(emploee.Username, $"Уважаемый(ая) {emploee.Fullname}. Вы хотели у нас работать? \n" +
                                                                        $"Тогда попытайтесь войти в нашу систему , и получите некотрые привелегии\n" +
                                                                        $"Ваш логин : {emploee.Username}\n" +
                                                                        $"Ваш пароль : {emploee.Emppassword}");
                return RedirectToAction("Index", "Home", new
                {
                    message = $"Уважаемый(ая) {emploee.Fullname}. Ваша заявка принята , ожидайте ответа по смс.\n" +
                                                                         $"Информация по состоянию отправки сообщения - {message}"
                });
            }

            ViewBag.Jobid = new SelectList(_db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
            return View(emploee);
        }

        // GET: Emploees/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await _db.Emploees.FindAsync(id);
            if (emploee == null)
            {
                return HttpNotFound();
            }
            ViewBag.Jobid = new SelectList(_db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
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
                _db.Entry(emploee).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await _db.SendMessage(emploee.Username, $"Уважаемый(ая) {emploee.Fullname}\n" +
                                                        $"Ваши данные были изменены\n" +
                                                        $"Ваш логин - {emploee.Username}\n" +
                                                        $"Ваш пароль - {emploee.Emppassword}");
                return RedirectToAction("Index");
            }
            ViewBag.Jobid = new SelectList(_db.Jobs, "Jobid", "Jobtype", emploee.Jobid);
            return View(emploee);
        }

        // GET: Emploees/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emploee emploee = await _db.Emploees.FindAsync(id);
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
            Emploee emploee = await _db.Emploees.FindAsync(id);
            if (emploee == null)
                return HttpNotFound();

            _db.Emploees.Remove(emploee);
            await _db.SaveChangesAsync();
            string message;
            if (StaticEmployee.Username == emploee.Username)
            {
                message = await _db.SendMessage(emploee.Username, $"Уважаемый(ая) {emploee.Fullname}\n" +
                                                                  "Вы решили уволиться по собственному желанию\n" +
                                                                  "Тем самым вы не имеете доступа к нашему сервису. ");
                return RedirectToAction("Index", new
                {
                    message = $"Вы сами себя удадили тем самым уволили))\n" +
                              $"Состояние доставки сообщения - {message} "
                });
            }
            message = await _db.SendMessage(emploee.Username, $"Вас уволил человек - {StaticEmployee.Fullname} который вошел в нашу систему\n" +
                                                              $"Напишите ему на его почту. Его почта : {StaticEmployee.Username}");
            return RedirectToAction("Index", new
            {
                message = "Вы либо кого-то уволили.\n" + "Либо просто удалили.\n" +
                          "И, то, что, вы сделали будет отправлено этому пользователю на почту.\n" +
                          $"Состояние доставки сообщения - {message} "
            });
        }
        [HttpPost]
        public async Task<ActionResult> Login(string login, string password)
        {
            Emploee emploee = await _db.Emploees.FirstOrDefaultAsync(w => w.Username == login && w.Emppassword == password);
            if (emploee != null)
            {
                IsLoggedIn = true;
                 await _db.SendMessage(emploee.Username, $"Вы зашли в систему. Пользуйтесь сервисом.\n" +
                                                         $"Ну я надеюсь , что вы всё правильно сделали.\n" +
                                                         $"Точнее не я , а \"наша команда\". ");
                StaticEmployee = emploee;
                return View("Details", emploee);
            }
            else
            {
                return RedirectToAction("Index","Home", new { message = "Скорее всего пароль не верный или логин" });
            }

        }

        public ActionResult LoginIndex()
        {
            return View();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
