﻿using System;
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
    public class WAExtremeEventController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /WAExtremeEvent/
        //public ActionResult Index()
        //{
        //    var tblswatwaextremeevents = db.tblswatwaextremeevents.Include(t => t.lkpSWATextremeEventsLU).Include(t => t.lkpSWATextremeEventsLU1).Include(t => t.lkpSWATextremeEventsLU2).Include(t => t.tblSWATSurvey);
        //    return View(tblswatwaextremeevents.ToList());
        //}

        // GET: /WAExtremeEvent/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwaextremeevent tblswatwaextremeevent = db.tblswatwaextremeevents.Find(id);
        //    if (tblswatwaextremeevent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwaextremeevent);
        //}

        // GET: /WAExtremeEvent/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.extremeDry = new SelectList(db.lkpswatextremeeventslus, "id", "Description");
            ViewBag.extremeFlood = new SelectList(db.lkpswatextremeeventslus, "id", "Description");
            ViewBag.extremeOther = new SelectList(db.lkpswatextremeeventslus, "id", "Description");
            ViewBag.currentSectionID = 2;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View();
        }

        public void updateScores( tblswatwaextremeevent tblswatwaextremeevent)
        {
            int? extremeDryId = tblswatwaextremeevent.extremeDry;
            if (extremeDryId != null)
            {
                int? extremeDryIntorder = db.lkpswatextremeeventslus.Find(extremeDryId).intorder;
                double? extremeDryScore = Double.Parse(db.lkpswatscores_extremeevents.Single(e => e.intorder == extremeDryIntorder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeDrySCORE").Value = extremeDryScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeDrySCORE").Value = null;
            }

            int? extremeFloodId = tblswatwaextremeevent.extremeFlood;
            if (extremeFloodId != null)
            {
                int? extremeFloodIntorder = db.lkpswatextremeeventslus.Find(extremeFloodId).intorder;
                double? extremeFloodScore = Double.Parse(db.lkpswatscores_extremeevents.Single(e => e.intorder == extremeFloodIntorder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeFloodSCORE").Value = extremeFloodScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeFloodSCORE").Value = null;
            }

            int? extremeOtherId = tblswatwaextremeevent.extremeOther;
            if (extremeOtherId != null)
            {
                int? extremeOtherIntorder = db.lkpswatextremeeventslus.Find(extremeOtherId).intorder;
                double? extremeOtherScore = Double.Parse(db.lkpswatscores_extremeevents.Single(e => e.intorder == extremeOtherIntorder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeOtherSCORE").Value = extremeOtherScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID && e.VarName == "extremeOtherSCORE").Value = null;
            }
            db.SaveChanges();
        }

        // POST: /WAExtremeEvent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,extremeDry,extremeFlood,extremeOther,extremeOtherComment")] tblswatwaextremeevent tblswatwaextremeevent, string submitBtn)
        {
            if (tblswatwaextremeevent.extremeOther != null && tblswatwaextremeevent.extremeOther != 251)
            {
                if (String.IsNullOrWhiteSpace(tblswatwaextremeevent.extremeOtherComment))
                {
                    ModelState.AddModelError("extremeOtherComment", "Please specify the extreme other.");
                }
            }
            if (ModelState.IsValid)
            {
                var extremeEventIDs = db.tblswatwaextremeevents.Where(e => e.SurveyID == tblswatwaextremeevent.SurveyID).Select(e => e.ID);
                if (extremeEventIDs.Any())
                {
                    int extremeEventId = extremeEventIDs.First();
                    tblswatwaextremeevent.ID = extremeEventId;
                    db.Entry(tblswatwaextremeevent).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatwaextremeevents.Add(tblswatwaextremeevent);
                }

                db.SaveChanges();
                updateScores(tblswatwaextremeevent);
                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WARiskPrep", new { SurveyID = tblswatwaextremeevent.SurveyID });
                }
            }

            ViewBag.extremeDry = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeDry);
            ViewBag.extremeFlood = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeFlood);
            ViewBag.extremeOther = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeOther);
            ViewBag.currentSectionID = 2;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatwaextremeevent.SurveyID).LocationID;
            ViewBag.SurveyID = tblswatwaextremeevent.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatwaextremeevent);
        }

        // GET: /WAExtremeEvent/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatwaextremeevent tblswatwaextremeevent = db.tblswatwaextremeevents.Find(id);
            if (tblswatwaextremeevent == null)
            {
                return HttpNotFound();
            }
            ViewBag.extremeDry = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeDry);
            ViewBag.extremeFlood = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeFlood);
            ViewBag.extremeOther = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeOther);
            ViewBag.currentSectionID = 2;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View(tblswatwaextremeevent);
        }

        // POST: /WAExtremeEvent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,extremeDry,extremeFlood,extremeOther,extremeOtherComment")] tblswatwaextremeevent tblswatwaextremeevent, string submitBtn)
        {
            if (tblswatwaextremeevent.extremeOther != null && tblswatwaextremeevent.extremeOther != 251)
            {
                if (String.IsNullOrWhiteSpace(tblswatwaextremeevent.extremeOtherComment))
                {
                    ModelState.AddModelError("extremeOtherComment", "Please specify the extreme other.");
                }
            }

            if (ModelState.IsValid)
            {
                db.Entry(tblswatwaextremeevent).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwaextremeevent);

                if (submitBtn.Equals("Next"))
                {
                    // If there is not any WARiskPrep with the current survey (SurveyID) then create one and redirect to its edit link.
                    var riskPreps = db.tblswatwariskpreps.Where(e => e.SurveyID == tblswatwaextremeevent.SurveyID);
                    if (!riskPreps.Any())
                    {
                        tblswatwariskprep tblswatwariskprep = new tblswatwariskprep();
                        tblswatwariskprep.SurveyID = tblswatwaextremeevent.SurveyID;
                        db.tblswatwariskpreps.Add(tblswatwariskprep);
                        db.SaveChanges();

                        int newRiskPrepID = tblswatwariskprep.ID;
                        return RedirectToAction("Edit", "WARiskPrep", new { id = newRiskPrepID, SurveyID = tblswatwariskprep.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "WARiskPrep", new { id = riskPreps.Single(e => e.SurveyID == tblswatwaextremeevent.SurveyID).ID, SurveyID = tblswatwaextremeevent.SurveyID });
                    }
                }
            }
            ViewBag.extremeDry = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeDry);
            ViewBag.extremeFlood = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeFlood);
            ViewBag.extremeOther = new SelectList(db.lkpswatextremeeventslus, "id", "Description", tblswatwaextremeevent.extremeOther);
            ViewBag.currentSectionID = 2;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatwaextremeevent.SurveyID).LocationID;
            ViewBag.SurveyID = tblswatwaextremeevent.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatwaextremeevent);
        }

        // GET: /WAExtremeEvent/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwaextremeevent tblswatwaextremeevent = db.tblswatwaextremeevents.Find(id);
        //    if (tblswatwaextremeevent == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwaextremeevent);
        //}

        // POST: /WAExtremeEvent/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatwaextremeevent tblswatwaextremeevent = db.tblswatwaextremeevents.Find(id);
        //    db.tblswatwaextremeevents.Remove(tblswatwaextremeevent);
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
