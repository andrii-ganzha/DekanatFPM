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
    public class StudentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Students
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.Group);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Where(s=>s.StudentID==id).Include(s=>s.Group).ToList().First();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create()
        {
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupID");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,GroupID,Name,Surname,Birthday,Gender")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupID", student.GroupID);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupID", student.GroupID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,GroupID,Name,Surname,Birthday,Gender")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GroupID = new SelectList(db.Groups, "GroupID", "GroupID", student.GroupID);
            return View(student);
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        private int? ControlTextParser(string text)
        {
            if (text.Length == 0)
            {
                return null;
            }
            return int.Parse(text[0].ToString());
        }

        //GET: Students/EditIndividualPlan
        public ActionResult EditIndividualPlan (int studentID, int year)
        {
            ViewBag.StudentID = studentID;
            ViewBag.Year = year;
            return View();
        }

        //POST: Students/EditindividualPlan
        [HttpPost]
        public ActionResult EditIndividualPlan (HttpPostedFileBase file, YearIndividualPlan plan, int studentID, int year)
        {
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream, XLEventTracking.Disabled))
            {
                IXLWorksheet workSheet = workBook.Worksheets.First();
                string text = workSheet.Cell(5, "o").Value.ToString();
                plan.Year = year;
                var student = db.Students.Find(studentID);
                int groupID = student.GroupID;

                int numberRow = 10;
                var currentSubjects = db.Subjects.Where(s => s.StudentID == studentID)
                                          .Where(s => s.GroupID == student.GroupID)
                                          .Where(s => s.Year == year)
                                          .ToList();
                int subjectCount = 0;
                while (workSheet.Cell(numberRow, "A").Value.ToString() != "")
                {
                    if (subjectCount < currentSubjects.Count())
                    {
                        //Subject subject = new Subject();
                        currentSubjects[subjectCount].GroupID = groupID;
                        currentSubjects[subjectCount].Name = workSheet.Cell(numberRow, "B").Value.ToString();
                        currentSubjects[subjectCount].Year = year;

                        text = workSheet.Cell(numberRow, "G").Value.ToString();
                        currentSubjects[subjectCount].ControlExam = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "H").Value.ToString();
                        currentSubjects[subjectCount].ControlCredit = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "I").Value.ToString();
                        currentSubjects[subjectCount].ControlCourseWork = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "J").Value.ToString();
                        currentSubjects[subjectCount].ControlIndividual = ControlTextParser(text);

                        db.Entry(currentSubjects[subjectCount]).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        Subject subject = new Subject();
                        subject.GroupID = groupID;
                        subject.Name = workSheet.Cell(numberRow, "B").Value.ToString();
                        subject.Year = year;

                        text = workSheet.Cell(numberRow, "G").Value.ToString();
                        subject.ControlExam = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "H").Value.ToString();
                        subject.ControlCredit = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "I").Value.ToString();
                        subject.ControlCourseWork = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "J").Value.ToString();
                        subject.ControlIndividual = ControlTextParser(text);

                        subject.StudentID = studentID;

                        currentSubjects.Add(subject);
                        db.Subjects.Add(subject);
                        db.SaveChanges();
                    }

                    numberRow++;
                    subjectCount++;
                }
                if (subjectCount < currentSubjects.Count())
                {
                    //currentSubjects.RemoveRange(subjectCount, currentSubjects.Count() - subjectCount);
                    for (int i = subjectCount; i < currentSubjects.Count(); i++)
                    {
                        //db.Entry(currentSubjects[i]).State = EntityState.Deleted;
                        db.Subjects.Remove(currentSubjects[i]);
                        db.SaveChanges();
                    }
                }

                var planToDelit = db.YearIndividualPlans.Include(p => p.Student)
                                                        .Where(p => p.StudentID == studentID)
                                                        .Where(p => p.Year == year)
                                                        .FirstOrDefault();
                if(planToDelit != null)
                {
                    db.YearIndividualPlans.Remove(planToDelit);
                    db.SaveChanges();
                }

                plan.StudentID = student.StudentID;
                var planToAdd = new YearIndividualPlan(plan);
                db.YearIndividualPlans.Add(planToAdd);
                db.SaveChanges();
                student.PlanWithGroup = false;
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();

            }
            return View(plan);
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
