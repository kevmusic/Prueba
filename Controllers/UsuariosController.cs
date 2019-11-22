using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using University.Models;

namespace University.Controllers
{
    public class UsuariosController : Controller
    {
        private ElContext db = new ElContext();

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Rol);
            return View(usuarios.ToList());
        }
        
        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.RolID = new SelectList(db.Roles, "RolID", "Descripcion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,Password,RolID")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                if (Usuario.GuardarUsuario(usuario).Guardado)
                    return RedirectToAction("Index");
            }

            ViewBag.RolID = new SelectList(db.Roles, "RolID", "Descripcion", usuario.RolID);
            return View(usuario);
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
