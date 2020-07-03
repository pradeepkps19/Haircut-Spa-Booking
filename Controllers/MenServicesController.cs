using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseFirstApp.Models;

namespace DatabaseFirstApp.Controllers
{
    public class MenServicesController : Controller
    {
        private DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2();

        // GET: MenServices
        public ActionResult Index()
        {
            return View(db.MServices.ToList());
        }

        // GET: MenServices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MService mService = db.MServices.Find(id);
            if (mService == null)
            {
                return HttpNotFound();
            }
            return View(mService);
        }

        // GET: MenServices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MenServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ServiceId,ServiceName,ServiceDescription,Cost,Image")] MService mService)
        {
            if (ModelState.IsValid)
            {
                db.MServices.Add(mService);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mService);
        }

        // GET: MenServices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MService mService = db.MServices.Find(id);
            if (mService == null)
            {
                return HttpNotFound();
            }
            return View(mService);
        }

        // POST: MenServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ServiceId,ServiceName,ServiceDescription,Cost,Image")] MService mService)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mService).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mService);
        }

        // GET: MenServices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MService mService = db.MServices.Find(id);
            if (mService == null)
            {
                return HttpNotFound();
            }
            return View(mService);
        }

        // POST: MenServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MService mService = db.MServices.Find(id);
            db.MServices.Remove(mService);
            db.SaveChanges();
            return RedirectToAction("Index");
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
