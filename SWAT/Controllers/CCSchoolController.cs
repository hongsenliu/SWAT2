using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWAT.Models;

namespace SWAT.Controllers
{
    public class CCSchoolController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /CCSchool/
        //public ActionResult Index()
        //{
        //    var tblswatccschools = db.tblswatccschools.Include(t => t.lkpSWAT5rankLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccschools.ToList());
        //}

        // GET: /CCSchool/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccschool tblswatccschool = db.tblswatccschools.Find(id);
        //    if (tblswatccschool == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccschool);
        //}

        // GET: /CCSchool/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.schoolHHAccess = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolAttendSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolInstitutionSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolHHAccessSCORE").Description;
            return View();
        }

        private void updateScores(tblswatccschool tblswatccschool)
        {
            if (tblswatccschool.schoolAttend != null)
            {
                double schoolAttendScore = (double)tblswatccschool.schoolAttend / 100.0;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccschool.SurveyID && e.VarName == "schoolAttendSCORE").Value = schoolAttendScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccschool.SurveyID && e.VarName == "schoolAttendSCORE").Value = null;
            }

            bool[] schools = { tblswatccschool.schoolInstitution1, tblswatccschool.schoolInstitution2, tblswatccschool.schoolInstitution3, tblswatccschool.schoolInstitution4 };

            int numSchool = 0;
            foreach (bool item in schools)
            {
                if (item)
                {
                    numSchool++;
                }
            }
            double schoolInstitutionScore = (double)numSchool / 4.0;
            db.tblswatscores.Single(e => e.SurveyID == tblswatccschool.SurveyID && e.VarName == "schoolInstitutionSCORE").Value = schoolInstitutionScore;

            if (tblswatccschool.schoolHHAccess != null)
            {
                int? hhAccessOrder = db.lkpswat5ranklu.Find(tblswatccschool.schoolHHAccess).intorder;
                double hhAccessScore = Double.Parse(db.lkpswatscores_5rankalwaysgood.Single(e => e.intorder == hhAccessOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccschool.SurveyID && e.VarName == "schoolHHAccessSCORE").Value = hhAccessScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccschool.SurveyID && e.VarName == "schoolHHAccessSCORE").Value = null;
            }

            db.SaveChanges();
        }

        // POST: /CCSchool/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,schoolAttend,schoolInstitution1,schoolInstitution2,schoolInstitution3,schoolInstitution4,schoolHHAccess")] tblswatccschool tblswatccschool)
        {
            if (ModelState.IsValid)
            {
                var schoolIDs = db.tblswatccschools.Where(e => e.SurveyID == tblswatccschool.SurveyID).Select(e => e.ID);
                if (schoolIDs.Any())
                {
                    int schoolId = schoolIDs.First();
                    tblswatccschool.ID = schoolId;
                    db.Entry(tblswatccschool).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatccschool);

                    // return RedirectToAction("Index");
                    return RedirectToAction("Create", "CCIndig", new { SurveyID = tblswatccschool.SurveyID });
                }

                db.tblswatccschools.Add(tblswatccschool);
                db.SaveChanges();
                updateScores(tblswatccschool);

                // return RedirectToAction("Index");
                return RedirectToAction("Create", "CCIndig", new { SurveyID = tblswatccschool.SurveyID });
            }

            ViewBag.schoolHHAccess = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccschool.schoolHHAccess);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolAttendSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolInstitutionSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolHHAccessSCORE").Description;
            return View(tblswatccschool);
        }

        // GET: /CCSchool/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccschool tblswatccschool = db.tblswatccschools.Find(id);
            if (tblswatccschool == null)
            {
                return HttpNotFound();
            }
            ViewBag.schoolHHAccess = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccschool.schoolHHAccess);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolAttendSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolInstitutionSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolHHAccessSCORE").Description;
            return View(tblswatccschool);
        }

        // POST: /CCSchool/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,schoolAttend,schoolInstitution1,schoolInstitution2,schoolInstitution3,schoolInstitution4,schoolHHAccess")] tblswatccschool tblswatccschool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccschool).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccschool);

                // If there is not any CCIndig with the current survey (SurveyID) then create one and redirect to its edit link.
                var indigs = db.tblswatccindigs.Where(e => e.SurveyID == tblswatccschool.SurveyID);
                if (!indigs.Any())
                {
                    tblswatccindig tblswatccindig = new tblswatccindig();
                    tblswatccindig.SurveyID = tblswatccschool.SurveyID;
                    db.tblswatccindigs.Add(tblswatccindig);
                    db.SaveChanges();
                    int newIndigID = tblswatccindig.ID;

                    return RedirectToAction("Edit", "CCIndig", new { id = newIndigID, SurveyID = tblswatccindig.SurveyID });
                }
                else
                {
                    return RedirectToAction("Edit", "CCIndig", new { id = indigs.Single(e => e.SurveyID == tblswatccschool.SurveyID).ID, SurveyID = tblswatccschool.SurveyID });
                }

                // return RedirectToAction("Index");
            }
            ViewBag.schoolHHAccess = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccschool.schoolHHAccess);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolAttendSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolInstitutionSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "schoolHHAccessSCORE").Description;
            return View(tblswatccschool);
        }

        // GET: /CCSchool/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccschool tblswatccschool = db.tblswatccschools.Find(id);
        //    if (tblswatccschool == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccschool);
        //}

        // POST: /CCSchool/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccschool tblswatccschool = db.tblswatccschools.Find(id);
        //    db.tblswatccschools.Remove(tblswatccschool);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
