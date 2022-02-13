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
        nanotechfinalEntities db = new nanotechfinalEntities();
        // GET: login
        [HttpGet]
        public ActionResult login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult login(user user1)
        {   
            user us1 = db.users.Where(x => x.username == user1.username && x.userpass == user1.userpass).FirstOrDefault();
            if(us1!= null)
            {
                Session["username"]= user1.username;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.msg = "Invalid id or password";
            }
            return View();
        }
        
    }
}