using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DekanatFPM.Models;

namespace DekanatFPM.Controllers
{
    public class SpecializationsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specializations
        public ActionResult Index()
        {
            var specializations = db.Specializations.Include(s => s.Specialty);
            return View(specializations.ToList());
        }

        // GET: Specializations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialization specialization = db.Specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // GET: Specializations/Create
        public ActionResult Create()
        {
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Name");
            return View();
        }

        // POST: Specializations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecializationID,Name,SpecialtyID")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.Specializations.Add(specialization);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Name", specialization.SpecialtyID);
            return View(specialization);
        }

        // GET: Specializations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialization specialization = db.Specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Name", specialization.SpecialtyID);
            return View(specialization);
        }

        // POST: Specializations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecializationID,Name,SpecialtyID")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialization).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecialtyID = new SelectList(db.Specialties, "SpecialtyID", "Name", specialization.SpecialtyID);
            return View(specialization);
        }

        // GET: Specializations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialization specialization = db.Specializations.Find(id);
            if (specialization == null)
            {
                return HttpNotFound();
            }
            return View(specialization);
        }

        // POST: Specializations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialization specialization = db.Specializations.Find(id);
            db.Specializations.Remove(specialization);
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
    }
}
