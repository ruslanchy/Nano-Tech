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
        public ActionResult login(user user1/*, adminn adm*/)
        {   
            user us1 = db.users.Where(x => x.useremail == user1.useremail && x.userpass == user1.userpass).FirstOrDefault();
            //adminn ad1 = db.adminns.Where(x=> x.adname == adm.adname && x.adpass == adm.adpass).FirstOrDefault();
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
        public ActionResult UserLogout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult viewprofile()
        {
            var userlist = db.users.ToList();
            return View(userlist);
        }
        public ActionResult userdash()
        {
            return View();
        }
        public ActionResult editprofile(user user2)
        {   
            return View(user2);
        }
        [HttpPost]
        public ActionResult editprofilesave(user user2, HttpPostedFileBase file)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["myconnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(mainconn);
            //string sqlquery1 = "SELECT * from [dbo].[product] where proname=@proname ";
            //SqlCommand sqlcomm1 = new SqlCommand(sqlquery1, sqlconn);
            //sqlconn.Open();

            //var proid = pro2.proid.ToString();
            //sqlconn.Close();
            string sqlquery = "update [dbo].[users] set username=@username ,userpass =@userpass ,usercontact = @usercontact , useremail =@useremail where userid=@userid ";
            SqlCommand sqlcomm = new SqlCommand(sqlquery, sqlconn);
            sqlconn.Open();
            sqlcomm.Parameters.AddWithValue("@userid", user2.userid);
            sqlcomm.Parameters.AddWithValue("@username", user2.username);
            //if (file != null && file.ContentLength > 0)
            //{
            //    string filename = Path.GetFileName(file.FileName);
            //    string imgpath = Path.Combine(Server.MapPath("~/productimages/"), filename);
            //    file.SaveAs(imgpath);
            //}
            
            sqlcomm.Parameters.AddWithValue("@userpass", user2.userpass);
            sqlcomm.Parameters.AddWithValue("@usercontact", user2.usercontact);
            sqlcomm.Parameters.AddWithValue("@useremail", user2.useremail);

            sqlcomm.ExecuteNonQuery();
            sqlconn.Close();
            return RedirectToAction("Index", "Home");
        }

    }
}