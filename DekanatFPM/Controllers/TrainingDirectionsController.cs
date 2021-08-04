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
    public class TrainingDirectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TrainingDirections
        public ActionResult Index()
        {
            return View(db.TrainingDirections.ToList());
        }

        // GET: TrainingDirections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingDirection trainingDirection = db.TrainingDirections.Find(id);
            if (trainingDirection == null)
            {
                return HttpNotFound();
            }
            return View(trainingDirection);
        }

        // GET: TrainingDirections/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TrainingDirections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TrainingDirectionID,Name")] TrainingDirection trainingDirection)
        {
            if (ModelState.IsValid)
            {
                db.TrainingDirections.Add(trainingDirection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trainingDirection);
        }

        // GET: TrainingDirections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingDirection trainingDirection = db.TrainingDirections.Find(id);
            if (trainingDirection == null)
            {
                return HttpNotFound();
            }
            return View(trainingDirection);
        }

        // POST: TrainingDirections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TrainingDirectionID,Name")] TrainingDirection trainingDirection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainingDirection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trainingDirection);
        }

        // GET: TrainingDirections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrainingDirection trainingDirection = db.TrainingDirections.Find(id);
            if (trainingDirection == null)
            {
                return HttpNotFound();
            }
            return View(trainingDirection);
        }

        // POST: TrainingDirections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TrainingDirection trainingDirection = db.TrainingDirections.Find(id);
            db.TrainingDirections.Remove(trainingDirection);
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
