using DatabaseFirstApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;

namespace DatabaseFirstApp.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2();
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Register(int id=0)
        {
            Registeration reg = new Registeration();
            return View(reg);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Registeration reg)
        {
            using (DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2())
            {
                if (db.Registerations.Any(x => x.UserName == reg.UserName))
                {
                    ViewBag.DuplicateMessage = "Username Already Exist";
                    return View("Register", reg);
                }
                else
                {
                    db.Registerations.Add(reg);
                    db.SaveChanges();

                }
            }
           
                return RedirectToAction("Login");

         }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Registeration reg)
        {

            var details = (from list in db.Registerations
                           where list.UserName.Equals(reg.UserName) && list.Password.Equals(reg.Password)
                           select new
                           {
                               list.UserId,
                               list.UserName,
                               list.Gender,
                               list.Password
                           }).ToList();

            if (details.FirstOrDefault() != null)
            {
                Session["UserID"] = details.FirstOrDefault().UserId.ToString();
                Session["UserName"] = details.FirstOrDefault().UserName;
                var gender = details.FirstOrDefault().Gender.ToString();


                if (Session["UserName"].ToString() == "admin")
                {
                    if(details.FirstOrDefault().Password.ToString() == "123456")
                    {
                        return RedirectToAction("Admin", "Admin");
                    }
                    return View();
                }
                else
                {


                    if (gender == "Female" || gender == "F")
                    {
                        return RedirectToAction("Service", "Home");
                    }
                    else
                    {
                        return RedirectToAction("MService", "Home");
                    }
                }
            }
            else
            {
                ViewBag.DuplicateMessage = "Invalid Credential";
                return View("Login", reg);
            }

          

            
        }
        
        
        public ActionResult Service()
        {
            return View(from Service in db.Services select Service);
        }

        public ActionResult MService()
        {
            return View(from MService in db.MServices select MService); ;
        }
        
        public ActionResult Book()
        {
            var l = new List<string>() { "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM" };
            ViewBag.l = l;
            if (Session["UserId"] != null)
            {
                int sid = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                int uid = int.Parse(Session["UserId"].ToString());
                var srvc = db.Services.Where(s => s.ServiceId == sid).FirstOrDefault();
                var sname = srvc.ServiceName.ToString();
                ViewBag.list = sname;
                ViewBag.id = uid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book(BookingTbl bt)
        {
            using (DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2())
            {
                if (db.BookingTbls.Any(x => (x.ServiceName == bt.ServiceName) && (x.BookingDate == bt.BookingDate) && (x.BookingTime == bt.BookingTime)))
                {
                    var l = new List<string>() { "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM" };
                    ViewBag.l = l;
                    int sid = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                    int uid = int.Parse(Session["UserId"].ToString());
                    var srvc = db.Services.Where(s => s.ServiceId == sid).FirstOrDefault();
                    var sname = srvc.ServiceName.ToString();
                    ViewBag.list = sname;
                    ViewBag.id = uid;
                    ViewBag.DuplicateMessage = "Slot Unavailable";
                    return View("Book", bt);
                }
                else
                {
                    db.BookingTbls.Add(bt);
                    db.SaveChanges();
                    return RedirectToAction("Profile");

                }
            }

            

        }


        public ActionResult MBook()
        {

            var l=new List<string>() { "10:00 AM","11:00 AM","12:00 PM","01:00 PM","02:00 PM","03:00 PM","04:00 PM","05:00 PM","06:00 PM"};
            ViewBag.l = l;
            if (Session["UserId"] != null)
            {
                int sid = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                int uid = int.Parse(Session["UserId"].ToString());

                var srvc = db.MServices.Where(s => s.ServiceId == sid).FirstOrDefault();
                var sname = srvc.ServiceName.ToString();
                ViewBag.list = sname;
                ViewBag.id = uid;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MBook(BookingTbl bt)
        {
            if (db.BookingTbls.Any(x => (x.ServiceName == bt.ServiceName) && (x.BookingDate == bt.BookingDate) && (x.BookingTime == bt.BookingTime)))
            {
                var l = new List<string>() { "10:00 AM", "11:00 AM", "12:00 PM", "01:00 PM", "02:00 PM", "03:00 PM", "04:00 PM", "05:00 PM", "06:00 PM" };
                ViewBag.l = l;
                int sid = int.Parse(Url.RequestContext.RouteData.Values["id"].ToString());
                int uid = int.Parse(Session["UserId"].ToString());
                var srvc = db.MServices.Where(s => s.ServiceId == sid).FirstOrDefault();
                var sname = srvc.ServiceName.ToString();
                ViewBag.list = sname;
                ViewBag.id = uid;
                ViewBag.DuplicateMessage = "Slot Unavailable";
                return View("MBook", bt);
            }
            else
            {

                db.BookingTbls.Add(bt);
                db.SaveChanges();
                return RedirectToAction("MProfile");

            }
        }

        public ActionResult Profile()
        {
            int uid = int.Parse(Session["UserId"].ToString());
            return View(from BookingTbl in db.BookingTbls where BookingTbl.UserId == uid select BookingTbl ); ;
        }

        public ActionResult MProfile()
        {
            return View(from BookingTbl in db.BookingTbls select BookingTbl); ;
        }
        public ActionResult About()
        {

            return View();
        }

        
       
        public ActionResult Delete(int? id)
        {
            
            Service service = db.Services.Find(id);
            
            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Service service = db.Services.Find(id);
            db.Services.Remove(service);
            db.SaveChanges();
            return RedirectToAction("Profile");
        }
        public ActionResult Logout()
        {
            Session["UserId"] = null;
            Session["UserName"] = null;
            Session.Abandon();
            return RedirectToAction("Login", "Home");

        }
    }
}