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
        public ActionResult Create([Bind(Include = "GroupID,SpecializationID,Type,Number,StartYear")] Group group)
        {
            if (ModelState.IsValid)
            {
                var specialization = db.Specializations.Find(group.SpecializationID);
                string year = group.StartYear.ToString();
                year = year[2].ToString() + year[3].ToString();
                string name = specialization.ShortName + "-" + year;
                if(group.Type==TypeGroup.School)
                {
                    group.Duration = 4;
                }
                if(group.Type==TypeGroup.College)
                {
                    name += "у";
                    group.Duration = 3;
                }
                if(group.Type==TypeGroup.Master)
                {
                    name += "м";
                    group.Duration = 2;
                }
                if(group.Type==TypeGroup.Postgraduate)
                {
                    name += "а";
                    group.Duration = 4;
                }
                name += "-" + group.Number;
                group.Name = name;
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
                    subject.GroupID = groupID;
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
                db.Subjects.AddRange(subjects);
                db.SaveChanges();
                var students = db.Students.Where(g => g.GroupID == groupID).ToList();
                for(int i=0; i<students.Count; i++)
                {
                    plan.StudentID = i + 1;
                    var planToAdd = new YearIndividualPlan(plan);
                    db.YearIndividualPlans.Add(planToAdd);
                    db.SaveChanges();
                }
            }

            return View(plan);
        }

        public ActionResult EditIndividualPlan(int groupID)
        {
            ViewBag.GroupId = groupID;
            return View();
        }
        
        //POST: Groups/EditIndividualPlan
        [HttpPost]
        public ActionResult EditIndividualPlan(HttpPostedFileBase file, YearIndividualPlan plan, int groupID)
        {
            using (XLWorkbook workBook = new XLWorkbook(file.InputStream, XLEventTracking.Disabled))
            {
                IXLWorksheet workSheet = workBook.Worksheets.First();
                string text = workSheet.Cell(5, "o").Value.ToString();
                plan.Year = int.Parse(text[0].ToString());

                //List<Subject> subjects = new List<Subject>();
                int numberRow = 10;

                var currentSubjects = db.Subjects.Where(s => s.GroupID == groupID).ToList();
                int subjectCount = 0;

                while (workSheet.Cell(numberRow, "A").Value.ToString() != "")
                {
                    if (subjectCount < currentSubjects.Count())
                    {
                        //Subject subject = new Subject();
                        currentSubjects[subjectCount].GroupID = groupID;
                        currentSubjects[subjectCount].Name = workSheet.Cell(numberRow, "B").Value.ToString();
                        currentSubjects[subjectCount].Year = plan.Year;

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
                        subject.Year = plan.Year;

                        text = workSheet.Cell(numberRow, "G").Value.ToString();
                        subject.ControlExam = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "H").Value.ToString();
                        subject.ControlCredit = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "I").Value.ToString();
                        subject.ControlCourseWork = ControlTextParser(text);

                        text = workSheet.Cell(numberRow, "J").Value.ToString();
                        subject.ControlIndividual = ControlTextParser(text);

                        currentSubjects.Add(subject);
                        db.Subjects.Add(subject);
                        db.SaveChanges();
                    }

                    numberRow++;
                    subjectCount++;
                }
                if(subjectCount<currentSubjects.Count())
                {
                    //currentSubjects.RemoveRange(subjectCount, currentSubjects.Count() - subjectCount);
                    for(int i=subjectCount; i<currentSubjects.Count(); i++)
                    {
                        //db.Entry(currentSubjects[i]).State = EntityState.Deleted;
                        db.Subjects.Remove(currentSubjects[i]);
                        db.SaveChanges();
                    }
                }
                var students = db.Students.Where(g => g.GroupID == groupID).ToList();
                var plans = db.YearIndividualPlans.Include(p => p.Student).Where(p => p.Student.GroupID == groupID).ToList();
                for (int i = 0; i < students.Count; i++)
                {
                    var planToDelit = plans.Where(p => p.StudentID == students[i].StudentID).Where(p=>p.Year==plan.Year).First();
                    if (planToDelit != null)
                    {
                        db.YearIndividualPlans.Remove(planToDelit);
                        db.SaveChanges();
                    }
                    plan.StudentID = i + 1;
                    var planToAdd = new YearIndividualPlan(plan);
                    db.YearIndividualPlans.Add(planToAdd);
                    db.SaveChanges();
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
        public ActionResult Edit([Bind(Include = "GroupID,SpecializationID,Type,Number,StartYear")] Group group)
        {
            if (ModelState.IsValid)
            {
                var specialization = db.Specializations.Find(group.SpecializationID);
                string year = group.StartYear.ToString();
                year = year[2].ToString() + year[3].ToString();
                string name = specialization.ShortName + "-" + year;
                if (group.Type == TypeGroup.School)
                {
                    group.Duration = 4;
                }
                if (group.Type == TypeGroup.College)
                {
                    name += "у";
                    group.Duration = 3;
                }
                if (group.Type == TypeGroup.Master)
                {
                    name += "м";
                    group.Duration = 2;
                }
                if (group.Type == TypeGroup.Postgraduate)
                {
                    name += "а";
                    group.Duration = 4;
                }
                name += "-" + group.Number;
                group.Name = name;
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
