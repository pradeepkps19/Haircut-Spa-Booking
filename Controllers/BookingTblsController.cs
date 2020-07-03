using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseFirstApp.Models;
using System.Web.UI.WebControls;
using System.Xml;

namespace DatabaseFirstApp.Controllers
{
    public class BookingTblsController : Controller
    {
        private DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2();

        // GET: BookingTbls
        public ActionResult Index()
        {
            return View(db.BookingTbls.ToList());
        }

        // GET: BookingTbls/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTbl bookingTbl = db.BookingTbls.Find(id);
            if (bookingTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookingTbl);
        }

        // GET: BookingTbls/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookingTbls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookingId,ServiceName,CustomerName,Mobile,BookingDate,BookingTime,UserId")] BookingTbl bookingTbl)
        {
            if (ModelState.IsValid)
            {
                db.BookingTbls.Add(bookingTbl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookingTbl);
        }

        // GET: BookingTbls/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTbl bookingTbl = db.BookingTbls.Find(id);
            if (bookingTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookingTbl);
        }

        // POST: BookingTbls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingId,ServiceName,CustomerName,Mobile,BookingDate,BookingTime,UserId")] BookingTbl bookingTbl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookingTbl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookingTbl);
        }

        // GET: BookingTbls/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookingTbl bookingTbl = db.BookingTbls.Find(id);
            if (bookingTbl == null)
            {
                return HttpNotFound();
            }
            return View(bookingTbl);
        }

        // POST: BookingTbls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookingTbl bookingTbl = db.BookingTbls.Find(id);
            db.BookingTbls.Remove(bookingTbl);
            db.SaveChanges();
            return RedirectToAction("Profile","Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
