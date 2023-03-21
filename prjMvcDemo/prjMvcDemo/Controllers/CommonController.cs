using prjMvcDemo.Models;
using prjMvcDemo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace prjMvcDemo.Controllers
{
    public class CommonController : Controller
    {
        // GET: Common
        public ActionResult Home()
        {
            if (Session["SK_LOGINED_USER"] == null)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginVM vm)
        {
            Customer user = new CustomerFactory().QueryByEmail(vm.txtAccount);
            if (user != null)
            {
                if (vm.txtPassword.Equals(user.fPassword))
                {
                    // Session 可存放物件
                    Session["SK_LOGINED_USER"] = user;
                    RedirectToAction("Home");
                }
            }

            return View();
        }
    }
}