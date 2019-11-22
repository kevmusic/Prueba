using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class PolizasController : Controller
    {
        private readonly ElContext db = new ElContext();

        // GET: Polizas
        public ActionResult Index()
        {
            IQueryable<Poliza> polizas = db.Polizas.Include(p => p.Asegurado);
            return View(polizas.ToList());
        }

        // GET: Polizas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poliza poliza = db.Polizas.Find(id);
            if (poliza == null)
            {
                return HttpNotFound();
            }
            return View(poliza);
        }

        // GET: Polizas/Create
        public ActionResult Create()
        {
            ViewBag.AseguradoID = new SelectList(db.Entes, "EnteID", "NombreCompleto");
            return View();
        }

        // POST: Polizas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PolizaID,NumeroPoliza,AseguradoID")] Poliza poliza)
        {
            if (ModelState.IsValid)
            {
                db.Polizas.Add(poliza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AseguradoID = new SelectList(db.Entes, "EnteID", "NombreCompleto", poliza.AseguradoID);
            return View(poliza);
        }

        // GET: Polizas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poliza poliza = db.Polizas.Find(id);
            if (poliza == null)
            {
                return HttpNotFound();
            }
            ViewBag.AseguradoID = new SelectList(db.Entes, "EnteID", "NombreCompleto", poliza.AseguradoID);
            return View(poliza);
        }

        // POST: Polizas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PolizaID,NumeroPoliza,AseguradoID")] Poliza poliza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poliza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AseguradoID = new SelectList(db.Entes, "EnteID", "NombreCompleto", poliza.AseguradoID);
            return View(poliza);
        }

        // GET: Polizas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Poliza poliza = db.Polizas.Find(id);
            if (poliza == null)
            {
                return HttpNotFound();
            }
            return View(poliza);
        }

        // POST: Polizas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Poliza poliza = db.Polizas.Find(id);
            db.Polizas.Remove(poliza);
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
