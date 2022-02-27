using Nano_Tech.Models;
using System;
using System.Collections.Generic;
using Nano_Tech.Controllers;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Nano_Tech.Controllers
{
    public class loginController : Controller
    {
        nanotechfinalEntities1 db = new nanotechfinalEntities1();
        // GET: login
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user user1, adminn adm)
        {   
            user us1 = db.users.Where(x => x.useremail == user1.useremail && x.userpass == user1.userpass).FirstOrDefault();
            adminn ad1 = db.adminns.Where(x=> x.adname == adm.adname && x.adpass == adm.adpass).FirstOrDefault();
            if (us1!= null)
            {
                Session["useremail"]= user1.useremail;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.msg = "Invalid email or password";
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult editprofile()
        {   
            return View();
        }

    }
}