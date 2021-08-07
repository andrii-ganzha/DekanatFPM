using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using DekanatFPM.Models;

namespace DekanatFPM.Controllers
{
    public class GroupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Groups
        public ActionResult Index()
        {
            var groups = db.Groups.Include(g => g.Specialization);
            return View(groups.ToList());
        }

        // GET: Groups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            var students = db.Students.Where(s => s.GroupID == id);
            ViewBag.Students = students.ToList();

            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.SpecializationID = new SelectList(db.Specializations, "SpecializationID", "Name");
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GroupID,SpecializationID,Type")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SpecializationID = new SelectList(db.Specializations, "SpecializationID", "Name", group.SpecializationID);
            return View(group);
        }

        private int? ControlTextParser(string text)
        {
            if (text.Length == 0)
            {
                return null;
            }
            return int.Parse(text[0].ToString());
        }

        //GET: Groups/AddIndividualPlan
        public ActionResult AddIndividualPlan(int groupID)
        {
            ViewBag.GroupID = groupID;
            return View();
        }
        
        //POST: Groups/AddIndividualPlan
        [HttpPost]
        public ActionResult AddIndividualPlan(HttpPostedFileBase file, YearIndividualPlan plan, int groupID)
        {
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream, XLEventTracking.Disabled))
            {
                IXLWorksheet workSheet = workBook.Worksheets.First();
                string text = workSheet.Cell(5, "o").Value.ToString();
                plan.Year = int.Parse(text[0].ToString());

                List<Subject> subjects = new List<Subject>();
                int numberRow = 10;

                while(workSheet.Cell(numberRow,"A").Value.ToString() != "")
                {
                    Subject subject = new Subject();
                    subject.GroupID = 1;
                    subject.Name = workSheet.Cell(numberRow, "B").Value.ToString();
                    subject.Year = plan.Year;
                    
                    text = workSheet.Cell(numberRow, "G").Value.ToString();
                    subject.ControlExam = ControlTextParser(text);
                    
                    text = workSheet.Cell(numberRow, "H").Value.ToString();
                    subject.ControlCredit = ControlTextParser(text);
                    
                    text = workSheet.Cell(numberRow, "I").Value.ToString();
                    subject.ControlCourseWork = ControlTextParser(text);
                    
                    text = workSheet.Cell(numberRow, "J").Value.ToString();
                    subject.ControlIndividual = ControlTextParser(text);

                    subjects.Add(subject);
                    numberRow++;
                }
            }

            return View(plan);
        }

        // GET: Groups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "SpecializationID", "Name", group.SpecializationID);
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GroupID,SpecializationID,Type")] Group group)
        {
            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SpecializationID = new SelectList(db.Specializations, "SpecializationID", "Name", group.SpecializationID);
            return View(group);
        }

        // GET: Groups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = db.Groups.Find(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = db.Groups.Find(id);
            db.Groups.Remove(group);
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
