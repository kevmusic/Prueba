using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    [Authorize]
    public class EntesController : Controller
    {
        private readonly ElContext db = new ElContext();

        // GET: Entes
        public ActionResult Index()
        {
            IQueryable<Ente> entes = db.Entes.Include(e => e.Sexo);
            return View(entes.ToList());
        }

        // GET: Entes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ente ente = db.Entes.Find(id);
            if (ente == null)
            {
                return HttpNotFound();
            }
            return View(ente);
        }

        // GET: Entes/Create
        public ActionResult Create()
        {
            ViewBag.SexoID = new SelectList(db.Sexos, "SexoID", "Descripcion");
            ViewBag.Sexos = db.Sexos.ToList();
            return View();
        }

        // POST: Entes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EnteID,NombreCompleto,FechaNacimiento,SexoID")] Ente ente)
        {
            if (ModelState.IsValid)
            {
                db.Entes.Add(ente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SexoID = new SelectList(db.Sexos, "SexoID", "Descripcion", ente.SexoID);
            ViewBag.Sexos = db.Sexos.ToList();
            return View(ente);
        }

        // GET: Entes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ente ente = db.Entes.Include(e => e.Polizas).FirstOrDefault(f => f.EnteID == id);
            if (ente == null)
            {
                return HttpNotFound();
            }
            ViewBag.SexoID = new SelectList(db.Sexos, "SexoID", "Descripcion", ente.SexoID);
            return View(ente);
        }

        // POST: Entes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EnteID,NombreCompleto,FechaNacimiento,SexoID,Polizas")] Ente ente)
        {
            if (ModelState.IsValid)
            {
                foreach (Poliza poliza in ente.Polizas)
                {
                    if (poliza.PolizaID == 0)
                    {
                        poliza.AseguradoID = ente.EnteID;
                        db.Entry(poliza).State = EntityState.Added;
                    }

                    else
                        db.Entry(poliza).State = EntityState.Modified;
                }

                db.Entry(ente).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SexoID = new SelectList(db.Sexos, "SexoID", "Descripcion", ente.SexoID);
            return View(ente);
        }

        // GET: Entes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ente ente = db.Entes.Find(id);
            if (ente == null)
            {
                return HttpNotFound();
            }
            return View(ente);
        }

        // POST: Entes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ente ente = db.Entes.Find(id);
            db.Entes.Remove(ente);
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



        [HttpGet]
        public ActionResult DeletePolicy(int? enteID, int? polizaID)
        {
            if (enteID == null || polizaID == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Poliza poliza = db.Polizas.FirstOrDefault(f => f.AseguradoID == enteID && f.PolizaID == polizaID);

            if (poliza == null)
                return HttpNotFound();

            db.Entry(poliza).State = EntityState.Deleted;
            db.SaveChanges();

            return RedirectToAction($"Edit/{enteID}");
        }
    }
}
