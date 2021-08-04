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
    public class SpecialtiesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Specialties
        public ActionResult Index()
        {
            var specialties = db.Specialties.Include(s => s.TrainingDirection);
            return View(specialties.ToList());
        }

        // GET: Specialties/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // GET: Specialties/Create
        public ActionResult Create()
        {
            ViewBag.TrainingDirectionID = new SelectList(db.TrainingDirections, "TrainingDirectionID", "Name");
            return View();
        }

        // POST: Specialties/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SpecialtyID,Name,TrainingDirectionID")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                db.Specialties.Add(specialty);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TrainingDirectionID = new SelectList(db.TrainingDirections, "TrainingDirectionID", "Name", specialty.TrainingDirectionID);
            return View(specialty);
        }

        // GET: Specialties/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainingDirectionID = new SelectList(db.TrainingDirections, "TrainingDirectionID", "Name", specialty.TrainingDirectionID);
            return View(specialty);
        }

        // POST: Specialties/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SpecialtyID,Name,TrainingDirectionID")] Specialty specialty)
        {
            if (ModelState.IsValid)
            {
                db.Entry(specialty).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainingDirectionID = new SelectList(db.TrainingDirections, "TrainingDirectionID", "Name", specialty.TrainingDirectionID);
            return View(specialty);
        }

        // GET: Specialties/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Specialty specialty = db.Specialties.Find(id);
            if (specialty == null)
            {
                return HttpNotFound();
            }
            return View(specialty);
        }

        // POST: Specialties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Specialty specialty = db.Specialties.Find(id);
            db.Specialties.Remove(specialty);
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
