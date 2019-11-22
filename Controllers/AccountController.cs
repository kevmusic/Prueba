using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using University.Models;
using University.Models.ViewModel;

namespace University.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            Login login = new Login();
            return View(login);
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login([Bind(Include = "Username,Password")] Login login)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario()
                {
                    Username = login.Username,
                    Password = login.Password
                };

                if (Usuario.AutenticarUsuario(usuario).Autenticado)
                {
                    FormsAuthentication.SetAuthCookie(usuario.Username, false);
                    return RedirectToAction("Index", "Entes");
                }
            }

            return View(login);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Logout");
        }
    }
}