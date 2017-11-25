using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GPSwood_online.Models;

namespace GPSwood_online.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(GPSwood_online.Models.User userModel)
        {
            using (GPSmapEntities db = new GPSmapEntities())
            {
                var userDetalis = db.Users.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
                if (userDetalis == null)
                {
                    userModel.LoginErrorMessage = "İstifadəçi adı və ya parol səfdir.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userId"] = userDetalis.UserId;
                    Session["userName"] = userDetalis.UserName;
                    return RedirectToAction("Index", "Map");
                }
            }
        }
        public ActionResult LogOut()
        {
            int userid = (int)Session["userId"];
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}   
