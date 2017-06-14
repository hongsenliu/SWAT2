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
    public class HPPKhpController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /HPPKhp/

        public ActionResult Index()
        {
            var tblswathppkhps = db.tblswathppkhps.Include(t => t.lkpswat5ranklu).Include(t => t.tblswatsurvey);
            return View(tblswathppkhps.ToList());
        }

        //
        // GET: /HPPKhp/Details/5

        public ActionResult Details(int id = 0)
        {
            tblswathppkhp tblswathppkhp = db.tblswathppkhps.Find(id);
            if (tblswathppkhp == null)
            {
                return HttpNotFound();
            }
            return View(tblswathppkhp);
        }

        //
        // GET: /HPPKhp/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.promotion = new SelectList(db.lkpswat5ranklu, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "knowledgeQualSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "trainingAccessTOTALSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "promotionSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "handWashSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "childFaecesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 5;

            return View();
        }

        private void checkSumChildFaces(tblswathppkhp tblswathppkhp)
        {
            double?[] childfaces = { tblswathppkhp.childFaeces1, tblswathppkhp.childFaeces2, tblswathppkhp.childFaeces3,
                                     tblswathppkhp.childFaeces4, tblswathppkhp.childFaeces5, tblswathppkhp.childFaeces6,
                                     tblswathppkhp.childFaeces7};
            double? total = null;
            foreach (double? item in childfaces)
            {
                total = total.GetValueOrDefault(0) + item.GetValueOrDefault(0);
            }
            if (total > 100)
            {
                ModelState.AddModelError("childFaeces1", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces2", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces3", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces4", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces5", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces6", "The total percentage of dispose cannot exceed 100.");
                ModelState.AddModelError("childFaeces7", "The total percentage of dispose cannot exceed 100.");
            }
        }

        private void updateScores(tblswathppkhp tblswathppkhp)
        {
            if (tblswathppkhp.knowledgeQual != null)
            {
                double score = (double)tblswathppkhp.knowledgeQual / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "knowledgeQualSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "knowledgeQualSCORE").Value = null;
            }

            if (tblswathppkhp.trainingAccess1 != null)
            {
                double score = (double)tblswathppkhp.trainingAccess1 / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess1SCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess1SCORE").Value = null;
            }

            if (tblswathppkhp.trainingAccess2 != null)
            {
                double score = (double)tblswathppkhp.trainingAccess2 / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess2SCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess2SCORE").Value = null;
            }

            if (tblswathppkhp.trainingAccess3 != null)
            {
                double score = (double)tblswathppkhp.trainingAccess3 / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess3SCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess3SCORE").Value = null;
            }

            if (tblswathppkhp.trainingAccess4 != null)
            {
                double score = (double)tblswathppkhp.trainingAccess4 / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess4SCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess4SCORE").Value = null;
            }

            if (tblswathppkhp.trainingAccess5 != null)
            {
                double score = (double)tblswathppkhp.trainingAccess5 / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess5SCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccess5SCORE").Value = null;
            }

            double?[] trainingAccess = { tblswathppkhp.trainingAccess1, tblswathppkhp.trainingAccess2, tblswathppkhp.trainingAccess3,
                                         tblswathppkhp.trainingAccess4, tblswathppkhp.trainingAccess5};

            int trainingAccessCounter = 0;
            double? total = null;
            foreach (double? item in trainingAccess)
            {
                if (item != null)
                {
                    total = total.GetValueOrDefault(0) + item.GetValueOrDefault(0);
                    trainingAccessCounter++;
                }
            }
            if (trainingAccessCounter > 0)
            {
                double score = total.GetValueOrDefault(0) / trainingAccessCounter;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccessTOTALSCORE").Value = score / 100;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "trainingAccessTOTALSCORE").Value = null;
            }

            if (tblswathppkhp.promotion != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswathppkhp.promotion).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "promotionSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "promotionSCORE").Value = null;
            }

            if (tblswathppkhp.handWash != null)
            {
                double score = (double)tblswathppkhp.handWash / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "handWashSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "handWashSCORE").Value = null;
            }

            double?[] childfaces = { tblswathppkhp.childFaeces1, tblswathppkhp.childFaeces2, tblswathppkhp.childFaeces3,
                                     tblswathppkhp.childFaeces4, tblswathppkhp.childFaeces5, tblswathppkhp.childFaeces6,
                                     tblswathppkhp.childFaeces7};
            int topCounter = 0;
            double? cfscore = null;
            foreach (double? item in childfaces.OrderByDescending(i => i))
            {
                if (topCounter < 3)
                {
                    if (item != null)
                    {
                        cfscore = cfscore.GetValueOrDefault(0) + item;
                    }
                    topCounter++;
                }
            }
            db.tblswatscores.First(e => e.SurveyID == tblswathppkhp.SurveyID && e.VarName == "childFaecesSCORE").Value = cfscore;
            db.SaveChanges();
        }

        //
        // POST: /HPPKhp/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswathppkhp tblswathppkhp, string submitBtn)
        {
            checkSumChildFaces(tblswathppkhp);
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswathppkhps.Where(e => e.SurveyID == tblswathppkhp.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswathppkhp.ID = recordId;
                    db.Entry(tblswathppkhp).State = EntityState.Modified;
                }
                else
                {
                    db.tblswathppkhps.Add(tblswathppkhp);
                }
                db.SaveChanges();
                updateScores(tblswathppkhp);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "SFSanitation", new { SurveyID = tblswathppkhp.SurveyID });
                }
            }

            ViewBag.promotion = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppkhp.promotion);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "knowledgeQualSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "trainingAccessTOTALSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "promotionSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "handWashSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "childFaecesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswathppkhp.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 5;

            return View(tblswathppkhp);
        }

        //
        // GET: /HPPKhp/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswathppkhp tblswathppkhp = db.tblswathppkhps.Find(id);
            if (tblswathppkhp == null)
            {
                return HttpNotFound();
            }
            ViewBag.promotion = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppkhp.promotion);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "knowledgeQualSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "trainingAccessTOTALSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "promotionSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "handWashSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "childFaecesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 5;

            return View(tblswathppkhp);
        }

        //
        // POST: /HPPKhp/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswathppkhp tblswathppkhp, string submitBtn)
        {
            checkSumChildFaces(tblswathppkhp);
            if (ModelState.IsValid)
            {
                db.Entry(tblswathppkhp).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswathppkhp);

                if (submitBtn.Equals("Next"))
                {
                    var records = db.tblswatsfsanitations.Where(e => e.SurveyID == tblswathppkhp.SurveyID);
                    if (!records.Any())
                    {
                        tblswatsfsanitation newEntry = new tblswatsfsanitation();
                        newEntry.SurveyID = tblswathppkhp.SurveyID;
                        db.tblswatsfsanitations.Add(newEntry);
                        db.SaveChanges();

                        int newId = newEntry.ID;
                        return RedirectToAction("Edit", "SFSanitation", new { id = newId, SurveyID = newEntry.SurveyID });
                    }

                    return RedirectToAction("Edit", "SFSanitation", new { id = records.First(e => e.SurveyID == tblswathppkhp.SurveyID).ID, SurveyID = tblswathppkhp.SurveyID });
                }
            }
            ViewBag.promotion = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppkhp.promotion);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "knowledgeQualSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "trainingAccessTOTALSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "promotionSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "handWashSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "childFaecesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswathppkhp.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 5;

            return View(tblswathppkhp);
        }

        //
        // GET: /HPPKhp/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblswathppkhp tblswathppkhp = db.tblswathppkhps.Find(id);
            if (tblswathppkhp == null)
            {
                return HttpNotFound();
            }
            return View(tblswathppkhp);
        }

        //
        // POST: /HPPKhp/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblswathppkhp tblswathppkhp = db.tblswathppkhps.Find(id);
            db.tblswathppkhps.Remove(tblswathppkhp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}