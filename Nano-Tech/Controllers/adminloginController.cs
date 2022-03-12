using Nano_Tech.Models;
using Nano_Tech.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.Entity;

namespace Nano_Tech.Controllers
{
    public class adminloginController : Controller
    {
        nanotechfinalEntities1 db = new nanotechfinalEntities1();
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
        public ActionResult AdminLogout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult addadmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult adminadd(adminn ad1)
        {
            if (ModelState.IsValid)
            {
                adminn object1 = new adminn();
                object1.adname = ad1.adname;
                object1.adpass = ad1.adpass;
                db.adminns.Add(object1);
                db.SaveChanges();
            }
            ModelState.Clear();
            return RedirectToAction("addadmin", "adminlogin");
        }
        
        public ActionResult addproduct()
        {
            return View();
        }
        [HttpPost]
        public ActionResult productadd(product pro1,HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            string sqlquery = "insert into [dbo].[product] (proname,proimage,proprice,prodesc,catid) values (@proname,@proimage,@proprice,@prodesc,@catid)";
            SqlCommand sqlcomm = new SqlCommand(sqlquery,sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@proname", pro1.proname);
            //if(file != null && file.ContentLength > 0)
            //{
            //    string filename = Path.GetFileName(file.FileName);
            //    string imgpath = Path.Combine(Server.MapPath("~/proimages/"),filename);
            //    file.SaveAs(imgpath);
            //}
            //sqlcomm.Parameters.AddWithValue("@proimage", "~/proimages/"+file.FileName );
            sqlcomm.Parameters.AddWithValue("@proimage", pro1.proimage);
            sqlcomm.Parameters.AddWithValue("@proprice", pro1.proprice);
            sqlcomm.Parameters.AddWithValue("@prodesc", pro1.prodesc);
            sqlcomm.Parameters.AddWithValue("@catid", pro1.catid);

            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("addproduct","adminlogin");
        }
        public ActionResult viewproduct()
        {   
            var prolist = db.products.ToList();
            return View(prolist);
        }
        public ActionResult deleteproduct(int proid)
        {   var res = db.products.Where(x=>x.proid == proid).First();
            db.products.Remove(res);
            db.SaveChanges();
            
            var prolist1 = db.products.ToList();
            return View("viewproduct",prolist1);
        }
        public ActionResult viewuser()
        {   var userlist = db.users.ToList();
            return View(userlist);
        }
        public ActionResult deleteuser(int userid)
        {
            var res = db.users.Where(x => x.userid == userid).First();
            db.users.Remove(res);
            db.SaveChanges();

            var prolist1 = db.users.ToList();
            return View("viewuser", prolist1);
        }
        public ActionResult editproduct(product pro2)
        {   
            return View(pro2);
        }
        public ActionResult editprodsave(product pro2 , HttpPostedFileBase file)
        {
            //int proidxyz = pro2.proid;
            string mainconn = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            //string sqlquery1 = "SELECT * from [dbo].[product] where proname=@proname ";
            //SqlCommand sqlcomm1 = new SqlCommand(sqlquery1, sqlconn);
            //sqlconn.Open();

            //var proid = pro2.proid.ToString();
            //sqlconn.Close();
            string sqlquery = "update [dbo].[product] set proname=@proname ,proimage =@proimage ,proprice = @proprice , prodesc =@prodesc where proid=@proid ";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@proid", pro2.proid);
            sqlcomm.Parameters.AddWithValue("@proname", pro2.proname);
            if (file != null && file.ContentLength > 0)
            {
                string filename = Path.GetFileName(file.FileName);
                string imgpath = Path.Combine(Server.MapPath("~/productimages/"), filename);
                file.SaveAs(imgpath);
            }
            sqlcomm.Parameters.AddWithValue("@proimage", "~/productimages/");
            sqlcomm.Parameters.AddWithValue("@proprice", pro2.proprice);
            sqlcomm.Parameters.AddWithValue("@prodesc", pro2.prodesc);

            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("viewproduct","adminlogin");
        }

    }
}