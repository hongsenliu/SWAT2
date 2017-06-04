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
    public class CCComController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /CCCom/

        //public ActionResult Index()
        //{
        //    var tblswatcccoms = db.tblswatcccoms.Include(t => t.tblSWATSurvey);
        //    return View(tblswatcccoms.ToList());
        //}

        //
        // GET: /CCCom/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatcccom tblswatcccom = db.tblswatcccoms.Find(id);
        //    if (tblswatcccom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatcccom);
        //}

        //
        // GET: /CCCom/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Question = db.lkpswatscorevarslus.Single(e => e.VarName == "comResourcesSCORE").Description;
            return View();
        }

        private void updateScores(tblswatcccom tblswatcccom)
        {
            bool[] comChecked = { tblswatcccom.comResource1, tblswatcccom.comResource2, tblswatcccom.comResource3,
                                  tblswatcccom.comResource4, tblswatcccom.comResource5};
            double checkedCounter = 0;
            foreach (bool item in comChecked)
            {
                if (item)
                {
                    checkedCounter++;
                }
            }

            double comScore = checkedCounter / 5;

            db.tblswatscores.Single(e => e.SurveyID == tblswatcccom.SurveyID && e.VarName == "comResourcesSCORE").Value = comScore;
            db.SaveChanges();
        }

        //
        // POST: /CCCom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatcccom tblswatcccom)
        {
            if (ModelState.IsValid)
            {
                var comIDs = db.tblswatcccoms.Where(e => e.SurveyID == tblswatcccom.SurveyID).Select(e => e.ID);
                if (comIDs.Any())
                {
                    tblswatcccom.ID = comIDs.First();
                    db.Entry(tblswatcccom).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatcccom);

                    return RedirectToAction("Create", "CCExternal", new { SurveyID = tblswatcccom.SurveyID});
                }

                db.tblswatcccoms.Add(tblswatcccom);
                db.SaveChanges();
                updateScores(tblswatcccom);

                return RedirectToAction("Create", "CCExternal", new { SurveyID = tblswatcccom.SurveyID});
            }

            ViewBag.Question = db.lkpswatscorevarslus.Single(e => e.VarName == "comResourcesSCORE").Description;
            return View(tblswatcccom);
        }

        //
        // GET: /CCCom/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatcccom tblswatcccom = db.tblswatcccoms.Find(id);
            if (tblswatcccom == null)
            {
                return HttpNotFound();
            }

            ViewBag.Question = db.lkpswatscorevarslus.Single(e => e.VarName == "comResourcesSCORE").Description;
            return View(tblswatcccom);
        }

        //
        // POST: /CCCom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatcccom tblswatcccom)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatcccom).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatcccom);

                var ccexternal = db.tblswatccexternalsupports.Where(e => e.SurveyID == tblswatcccom.SurveyID);
                if (!ccexternal.Any())
                {
                    tblswatccexternalsupport tblswatccexternal = new tblswatccexternalsupport();
                    tblswatccexternal.SurveyID = tblswatcccom.SurveyID;
                    db.tblswatccexternalsupports.Add(tblswatccexternal);
                    db.SaveChanges();

                    int newExternalID = tblswatccexternal.ID;

                    return RedirectToAction("Edit", "CCExternal", new { id = newExternalID, SurveyID = tblswatccexternal.SurveyID });
                }

                return RedirectToAction("Edit", "CCExternal", new { id = ccexternal.Single(e => e.SurveyID == tblswatcccom.SurveyID).ID, SurveyID = tblswatcccom.SurveyID });
            }

            ViewBag.Question = db.lkpswatscorevarslus.Single(e => e.VarName == "comResourcesSCORE").Description;
            return View(tblswatcccom);
        }

        //
        // GET: /CCCom/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatcccom tblswatcccom = db.tblswatcccoms.Find(id);
        //    if (tblswatcccom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatcccom);
        //}

        //
        // POST: /CCCom/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatcccom tblswatcccom = db.tblswatcccoms.Find(id);
        //    db.tblswatcccoms.Remove(tblswatcccom);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}