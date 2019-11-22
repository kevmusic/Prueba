using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class SexosController : Controller
    {
        private readonly ElContext db = new ElContext();

        // GET: Sexos
        public ActionResult Index()
        {
            return View(db.Sexos.ToList());
        }

        // GET: Sexos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexos.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // GET: Sexos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sexos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SexoID,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Sexos.Add(sexo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sexo);
        }

        // GET: Sexos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexos.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Sexos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SexoID,Descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sexo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sexo);
        }

        // GET: Sexos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = db.Sexos.Find(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Sexos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sexo sexo = db.Sexos.Find(id);
            db.Sexos.Remove(sexo);
            db.SaveChanges();
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
