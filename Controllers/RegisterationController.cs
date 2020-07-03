using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DatabaseFirstApp.Models;
using System.Data.Entity.Validation;

namespace DatabaseFirstApp.Controllers
{
    public class RegisterationController : Controller
    {
        private DatabaseFirstDbEntities2 db = new DatabaseFirstDbEntities2();

        // GET: Registeration
        public ActionResult Index()
        {
            return View(db.Registerations.ToList());
        }

        // GET: Registeration/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registeration registeration = db.Registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // GET: Registeration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registeration/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,UserName,EmailID,Mobile,Password,Gender")] Registeration registeration)
        {
            if (ModelState.IsValid)
            {
                db.Registerations.Add(registeration);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(registeration);
        }

        // GET: Registeration/Edit/5
        public ActionResult Edit(int? id)
        {

            Registeration registeration = db.Registerations.Find(id);
            return View(registeration);
        }

        // POST: Registeration/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserName,EmailID,Mobile,Password,Gender")] Registeration registeration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   
                    db.Entry(registeration).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("About", "Home");

        }

        // GET: Registeration/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registeration registeration = db.Registerations.Find(id);
            if (registeration == null)
            {
                return HttpNotFound();
            }
            return View(registeration);
        }

        // POST: Registeration/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Registeration registeration = db.Registerations.Find(id);
            db.Registerations.Remove(registeration);
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
