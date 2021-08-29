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
    public class SubjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Subjects
        public ActionResult Index()
        {
            var subjects = db.Subjects.Include(s => s.Group);
            return View(subjects.ToList());
        }

        // GET: Subjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // GET: Subjects/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name");
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubjectID,Name,GroupID,StudentID,Year,ControlExam,ControlCredit,ControlCourseWork,ControlIndividual")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Subjects.Add(subject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", subject.GroupID);
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", subject.GroupID);
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubjectID,Name,GroupID,StudentID,Year,ControlExam,ControlCredit,ControlCourseWork,ControlIndividual")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(subject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "Name", subject.GroupID);
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Subject subject = db.Subjects.Find(id);
            if (subject == null)
            {
                return HttpNotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Subject subject = db.Subjects.Find(id);
            db.Subjects.Remove(subject);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //GET: Subjects/AddStatement
        public ActionResult AddStatement(int subjectID)
        {
            ViewBag.SubjectID = subjectID;
            return View();
        }

        //POST: Subjects/AddStatement
        [HttpPost]
        public ActionResult AddStatement(HttpPostedFileBase file, int subjectID)
        {
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream, XLEventTracking.Disabled))
            {
                IXLWorksheet workSheet = workBook.Worksheets.First();

                List<Statement> statements = new List<Statement>();
                int numberRow = 24;
                int recordbook = 0;
                int semester = int.Parse(workSheet.Cell(12, "H").Value.ToString());
                while (workSheet.Cell(numberRow, "A").Value.ToString() != "")
                {
                    Statement statement = new Statement();
                    statement.SubjectID = subjectID;

                    recordbook = int.Parse(workSheet.Cell(numberRow, "L").Value.ToString());
                    statement.StudentID = db.Students.Where(s => s.RecordBook == recordbook).First().StudentID;
                    statement.Grade = int.Parse(workSheet.Cell(numberRow, "S").Value.ToString());
                    statement.Semester = semester;

                    statements.Add(statement);
                    numberRow++;
                }
                db.Statements.AddRange(statements);
                db.SaveChanges();
            }
            return View();
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
