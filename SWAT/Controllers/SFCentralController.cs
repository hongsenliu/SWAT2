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
    public class SFCentralController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SFCentral/

        //public ActionResult Index()
        //{
        //    var tblswatsfcentrals = db.tblSWATSFcentrals.Include(t => t.lkpSWAT5rankLU).Include(t => t.lkpSWATcentralConditionLU).Include(t => t.lkpSWATcentralTreatmentTypeLU).Include(t => t.lkpSWAThhCleanLU).Include(t => t.lkpSWATpubCleanLU).Include(t => t.lkpSWATYesNoLU).Include(t => t.lkpSWATYesNoLU1).Include(t => t.lkpSWATYesNoLU2).Include(t => t.tblSWATSurvey);
        //    return View(tblswatsfcentrals.ToList());
        //}

        //
        // GET: /SFCentral/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblSWATSFcentral tblswatsfcentral = db.tblSWATSFcentrals.Find(id);
        //    if (tblswatsfcentral == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfcentral);
        //}

        private void removeOthers(int SurveyID)
        { 
            var septics = db.tblswatsfseptics.Where(e => e.SurveyID == SurveyID);
            foreach (tblswatsfseptic s in septics)
            {
                db.tblswatsfseptics.Remove(s);
            }

            var lats = db.tblswatsflats.Where(e => e.SurveyID == SurveyID);
            foreach (tblswatsflat l in lats)
            {
                db.tblswatsflats.Remove(l);
            }


            var scores = db.tblswatscores.Include(t => t.lkpswatscorevarslu).Where(e => e.SurveyID == SurveyID && e.lkpswatscorevarslu.SectionID == 10 || e.lkpswatscorevarslu.SectionID == 11);

            foreach (tblswatscore s in scores)
            {
                    s.Value = null;
            }

            db.SaveChanges();
        }

        private void showForm(int SurveyID)
        {
            tblswatsfpoint sfpoint = db.tblswatsfpoints.First(e => e.SurveyID == SurveyID);
            ViewBag.pub = (sfpoint.SanUseCom || sfpoint.sanUsePub);
            ViewBag.ind = sfpoint.sanUseInd;
        }

        //
        // GET: /SFCentral/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            removeOthers((int)SurveyID);

            ViewBag.centralSludge = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.centralCondition = new SelectList(db.lkpswatcentralconditionlus, "id", "Description");
            ViewBag.centralTreatmentType = new SelectList(db.lkpswatcentraltreatmenttypelus, "id", "Description");
            ViewBag.hhCentralClean = new SelectList(db.lkpswathhcleanlus, "id", "Description");
            ViewBag.centralPubClean = new SelectList(db.lkpswatpubcleanlus, "id", "Description");
            ViewBag.centralfeesCharged = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.centralFeesLimitAccess = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.centralFeesEnsureClean = new SelectList(db.lkpswatyesnolus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "centralToiletsPSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "centralTreatmentTypeSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "centralSludgeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "centralConditionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "centralPubCleanSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "centralAccessSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "centralfeesChargedSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesLimitAccessSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesEnsureCleanSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "hhCentralCleanSCORE").Description;

            showForm((int)SurveyID);

            return View();
        }

        private void updateScores(tblswatsfcentral tblswatsfcentral)
        {
            if (tblswatsfcentral.centralToilets != null)
            {
                int toilets = (int)tblswatsfcentral.centralToilets;
                double score = (double)toilets / (int)db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfcentral.SurveyID).numHouseholds;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralToiletsPSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralToiletsPSCORE").Value = null;
            }

            if (tblswatsfcentral.centralTreatmentType != null)
            {
                int intorder = (int)db.lkpswatcentraltreatmenttypelus.Find(tblswatsfcentral.centralTreatmentType).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_centraltreatment.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralTreatmentTypeSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralTreatmentTypeSCORE").Value = null;
            }

            if (tblswatsfcentral.centralSludge != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatsfcentral.centralSludge).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralSludgeSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralSludgeSCORE").Value = null;
            }

            if (tblswatsfcentral.centralCondition != null)
            {
                int intorder = (int)db.lkpswatcentralconditionlus.Find(tblswatsfcentral.centralCondition).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_centralcondition.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralConditionSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralConditionSCORE").Value = null;
            }

            if (tblswatsfcentral.centralPubClean != null)
            {
                int intorder = (int)db.lkpswatpubcleanlus.Find(tblswatsfcentral.centralPubClean).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pubclean.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralPubCleanSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralPubCleanSCORE").Value = null;
            }

            bool[] access = { tblswatsfcentral.centralAccessGroup1, tblswatsfcentral.centralAccessGroup2, tblswatsfcentral.centralAccessGroup3};
            int accessCounter = 0;
            foreach (bool item in access)
            {
                if (item)
                {
                    accessCounter++;
                }
            }
            double accessScore = (double)accessCounter / 3;
            db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralAccessSCORE").Value = accessScore;

            if (tblswatsfcentral.centralfeesCharged != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatsfcentral.centralfeesCharged).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnoluyesgood.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralfeesChargedSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralfeesChargedSCORE").Value = null;
            }

            if (tblswatsfcentral.centralFeesLimitAccess != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatsfcentral.centralFeesLimitAccess).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralFeesLimitAccessSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralFeesLimitAccessSCORE").Value = null;
            }

            if (tblswatsfcentral.centralFeesEnsureClean != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatsfcentral.centralFeesEnsureClean).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnoluyesgood.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralFeesEnsureCleanSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "centralFeesEnsureCleanSCORE").Value = null;
            }

            if (tblswatsfcentral.hhCentralClean != null)
            {
                int intorder = (int)db.lkpswathhcleanlus.Find(tblswatsfcentral.hhCentralClean).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_hhclean.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "hhCentralCleanSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfcentral.SurveyID && e.VarName == "hhCentralCleanSCORE").Value = null;
            }

            db.SaveChanges();
        }

        //
        // POST: /SFCentral/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatsfcentral tblswatsfcentral, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatsfcentrals.Where(e => e.SurveyID == tblswatsfcentral.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswatsfcentral.ID = recordId;
                    db.Entry(tblswatsfcentral).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatsfcentrals.Add(tblswatsfcentral);
                }
                db.SaveChanges();
                updateScores(tblswatsfcentral);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("WaterPoints", "Survey", new { id = tblswatsfcentral.SurveyID });
                }
            }

            ViewBag.centralSludge = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatsfcentral.centralSludge);
            ViewBag.centralCondition = new SelectList(db.lkpswatcentralconditionlus, "id", "Description", tblswatsfcentral.centralCondition);
            ViewBag.centralTreatmentType = new SelectList(db.lkpswatcentraltreatmenttypelus, "id", "Description", tblswatsfcentral.centralTreatmentType);
            ViewBag.hhCentralClean = new SelectList(db.lkpswathhcleanlus, "id", "Description", tblswatsfcentral.hhCentralClean);
            ViewBag.centralPubClean = new SelectList(db.lkpswatpubcleanlus, "id", "Description", tblswatsfcentral.centralPubClean);
            ViewBag.centralfeesCharged = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralfeesCharged);
            ViewBag.centralFeesLimitAccess = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesLimitAccess);
            ViewBag.centralFeesEnsureClean = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesEnsureClean);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "centralToiletsPSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "centralTreatmentTypeSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "centralSludgeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "centralConditionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "centralPubCleanSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "centralAccessSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "centralfeesChargedSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesLimitAccessSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesEnsureCleanSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "hhCentralCleanSCORE").Description;

            showForm((int)tblswatsfcentral.SurveyID);

            return View(tblswatsfcentral);
        }

        //
        // GET: /SFCentral/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsfcentral tblswatsfcentral = db.tblswatsfcentrals.Find(id);
            if (tblswatsfcentral == null)
            {
                return HttpNotFound();
            }

            removeOthers((int)SurveyID);

            ViewBag.centralSludge = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatsfcentral.centralSludge);
            ViewBag.centralCondition = new SelectList(db.lkpswatcentralconditionlus, "id", "Description", tblswatsfcentral.centralCondition);
            ViewBag.centralTreatmentType = new SelectList(db.lkpswatcentraltreatmenttypelus, "id", "Description", tblswatsfcentral.centralTreatmentType);
            ViewBag.hhCentralClean = new SelectList(db.lkpswathhcleanlus, "id", "Description", tblswatsfcentral.hhCentralClean);
            ViewBag.centralPubClean = new SelectList(db.lkpswatpubcleanlus, "id", "Description", tblswatsfcentral.centralPubClean);
            ViewBag.centralfeesCharged = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralfeesCharged);
            ViewBag.centralFeesLimitAccess = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesLimitAccess);
            ViewBag.centralFeesEnsureClean = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesEnsureClean);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "centralToiletsPSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "centralTreatmentTypeSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "centralSludgeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "centralConditionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "centralPubCleanSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "centralAccessSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "centralfeesChargedSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesLimitAccessSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesEnsureCleanSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "hhCentralCleanSCORE").Description;

            showForm((int)tblswatsfcentral.SurveyID);
            
            return View(tblswatsfcentral);
        }

        //
        // POST: /SFCentral/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatsfcentral tblswatsfcentral, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatsfcentral).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatsfcentral);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("WaterPoints", "Survey", new { id = tblswatsfcentral.SurveyID });
                }
            }
            ViewBag.centralSludge = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatsfcentral.centralSludge);
            ViewBag.centralCondition = new SelectList(db.lkpswatcentralconditionlus, "id", "Description", tblswatsfcentral.centralCondition);
            ViewBag.centralTreatmentType = new SelectList(db.lkpswatcentraltreatmenttypelus, "id", "Description", tblswatsfcentral.centralTreatmentType);
            ViewBag.hhCentralClean = new SelectList(db.lkpswathhcleanlus, "id", "Description", tblswatsfcentral.hhCentralClean);
            ViewBag.centralPubClean = new SelectList(db.lkpswatpubcleanlus, "id", "Description", tblswatsfcentral.centralPubClean);
            ViewBag.centralfeesCharged = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralfeesCharged);
            ViewBag.centralFeesLimitAccess = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesLimitAccess);
            ViewBag.centralFeesEnsureClean = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatsfcentral.centralFeesEnsureClean);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "centralToiletsPSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "centralTreatmentTypeSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "centralSludgeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "centralConditionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "centralPubCleanSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "centralAccessSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "centralfeesChargedSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesLimitAccessSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "centralFeesEnsureCleanSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "hhCentralCleanSCORE").Description;

            showForm((int)tblswatsfcentral.SurveyID);

            return View(tblswatsfcentral);
        }

        //
        // GET: /SFCentral/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblSWATSFcentral tblswatsfcentral = db.tblSWATSFcentrals.Find(id);
        //    if (tblswatsfcentral == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfcentral);
        //}

        //
        // POST: /SFCentral/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblSWATSFcentral tblswatsfcentral = db.tblSWATSFcentrals.Find(id);
        //    db.tblSWATSFcentrals.Remove(tblswatsfcentral);
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