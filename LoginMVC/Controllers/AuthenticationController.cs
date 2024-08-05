using LoginMVC.DAL;
using LoginMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMVC.Controllers
{
    public class AuthenticationController : Controller
    {

        // GET: Authentication
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginViewModel model) {

            ViewBag.Error = "Algo ha fallado";

            if (ModelState.IsValid)
            {
                UsuarioDAL usuario = new UsuarioDAL();

                List<LoginViewModel> list = usuario.GetUserListFromDB();

                foreach (var item in list)
                {
                    if(item.Email.Trim() == model.Email && item.Password.Trim() == model.Password)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            return View(model);
        }


    }
}