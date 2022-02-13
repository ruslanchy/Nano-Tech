using Nano_Tech.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Nano_Tech.Controllers
{
    public class adminloginController : Controller
    {
        nanotechfinalEntities db = new nanotechfinalEntities();
        // GET: adminlogin
        [HttpGet]
        public ActionResult adminlogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminlogin(adminn  adm)
        {   
            adminn ad = db.adminns.Where(x=>x.adname == adm.adname && x.adpass == adm.adpass).FirstOrDefault();
            if (ad != null)
            {
                Session["adname"] = adm.adname;
                return RedirectToAction("admindash");
            }
            else
            {
                ViewBag.msg = "Invalid id or password";
                //return RedirectToAction("adminlogin");
            }
            return View();
        }
        public ActionResult admindash()
        {
            return View();
        }
    }
}