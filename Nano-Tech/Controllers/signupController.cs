using System;
using Nano_Tech.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nano_Tech.Controllers;
using System.Web.Mvc;

namespace Nano_Tech.Controllers
{
    public class signupController : Controller
    {
        nanotechfinalEntities1 db = new nanotechfinalEntities1();
        // GET: signup
        public ActionResult signup()
        {   
            return View();
        }
        [HttpPost]
        public ActionResult adduser(user user1)
        {
            if (ModelState.IsValid)
            {
                user obj = new user();
                obj.username = user1.username;
                obj.userpass = user1.userpass;
                obj.usercontact = user1.usercontact;
                obj.useremail = user1.useremail;

                db.users.Add(obj);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("login","login");
        }
       
    }
    
}