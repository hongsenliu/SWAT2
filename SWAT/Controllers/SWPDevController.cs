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
    public class SWPDevController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SWPDev/

        //public ActionResult Index()
        //{
        //    var tblswatswpdevs = db.tblswatswpdevs.Include(t => t.lkpSWAT5rankLU).Include(t => t.lkpSWATbestManIndLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatswpdevs.ToList());
        //}

        //
        // GET: /SWPDev/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatswpdev tblswatswpdev = db.tblswatswpdevs.Find(id);
        //    if (tblswatswpdev == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpdev);
        //}

        //
        // GET: /SWPDev/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int? devId = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID).isEconDev;
            if (devId != 1511)
            {
                // return RedirectToAction("Index");
                return RedirectToAction("Create", "HPPCom", new { SurveyID = SurveyID });
            }

            ViewBag.wwTreatInd = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.bestManInd = new SelectList(db.lkpswatbestmanindlus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "devSiteSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManIndSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "wwTreatIndSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View();
        }

        private void updateScores(tblswatswpdev tblswatswpdev)
        {
            int? devSiteTotal = null;
            int?[] devSites = { tblswatswpdev.devSite1, tblswatswpdev.devSite2, tblswatswpdev.devSite3, tblswatswpdev.devSite4 };

            foreach (int? item in devSites)
            {
                if (item != null)
                {
                    devSiteTotal = devSiteTotal.GetValueOrDefault(0) + item;
                }
            }
            tblswatswpdev.devSiteTOTAL = devSiteTotal;

            if (devSiteTotal != null)
            {
                int devSiteOrder = 0;
                foreach (var item in db.lkpswatdevsitelus.OrderByDescending(e => e.Description))
                {
                    if (item.Description <= devSiteTotal)
                    {
                        devSiteOrder = item.intorder;
                        break;
                    }
                }
                if (devSiteOrder > 0)
                {
                    double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == devSiteOrder).Description);
                    db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "devSiteSCORE").Value = score;
                }
                else
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "devSiteSCORE").Value = null;
                }
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "devSiteSCORE").Value = null;
            }

            if (tblswatswpdev.bestManInd != null)
            {
                int intorder = (int)db.lkpswatbestmanindlus.Find(tblswatswpdev.bestManInd).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_bestmanind.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "bestManIndSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "bestManIndSCORE").Value = null;
            }

            if (tblswatswpdev.wwTreatInd != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatswpdev.wwTreatInd).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "wwTreatIndSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpdev.SurveyID && e.VarName == "wwTreatIndSCORE").Value = null;
            }

            db.SaveChanges();
        }

        //
        // POST: /SWPDev/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatswpdev tblswatswpdev, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var devIDs = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatswpdev.SurveyID).Select(e => e.ID);
                if (devIDs.Any())
                {
                    int devId = devIDs.First();
                    tblswatswpdev.ID = devId;
                    db.Entry(tblswatswpdev).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatswpdevs.Add(tblswatswpdev);
                }
                db.SaveChanges();
                updateScores(tblswatswpdev);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "HPPCom", new { SurveyID = tblswatswpdev.SurveyID });
                }
            }

            ViewBag.wwTreatInd = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpdev.wwTreatInd);
            ViewBag.bestManInd = new SelectList(db.lkpswatbestmanindlus, "id", "Description", tblswatswpdev.bestManInd);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "devSiteSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManIndSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "wwTreatIndSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatswpdev.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatswpdev.SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == tblswatswpdev.SurveyID).ID;
            ViewBag.SurveyID = tblswatswpdev.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatswpdev);
        }

        //
        // GET: /SWPDev/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatswpdev tblswatswpdev = db.tblswatswpdevs.Find(id);
            if (tblswatswpdev == null)
            {
                return HttpNotFound();
            }
            ViewBag.wwTreatInd = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpdev.wwTreatInd);
            ViewBag.bestManInd = new SelectList(db.lkpswatbestmanindlus, "id", "Description", tblswatswpdev.bestManInd);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "devSiteSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManIndSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "wwTreatIndSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatswpdev.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatswpdev.SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == tblswatswpdev.SurveyID).ID;
            ViewBag.SurveyID = tblswatswpdev.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatswpdev);
        }

        //
        // POST: /SWPDev/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatswpdev tblswatswpdev, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatswpdev).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatswpdev);

                if (submitBtn.Equals("Next"))
                {
                    var hppcom = db.tblswathppcoms.Where(e => e.SurveyID == tblswatswpdev.SurveyID);
                    if (!hppcom.Any())
                    {
                        tblswathppcom tblswathppcom = new tblswathppcom();
                        tblswathppcom.SurveyID = tblswatswpdev.SurveyID;
                        db.tblswathppcoms.Add(tblswathppcom);
                        db.SaveChanges();

                        int newHPPcomID = tblswathppcom.ID;
                        return RedirectToAction("Edit", "HPPCom", new { id = newHPPcomID, SurveyID = tblswathppcom.SurveyID });
                    }
                    return RedirectToAction("Edit", "HPPCom", new { id = hppcom.First(e => e.SurveyID == tblswatswpdev.SurveyID).ID, SurveyID = tblswatswpdev.SurveyID });

                }
            }
            ViewBag.wwTreatInd = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpdev.wwTreatInd);
            ViewBag.bestManInd = new SelectList(db.lkpswatbestmanindlus, "id", "Description", tblswatswpdev.bestManInd);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "devSiteSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManIndSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "wwTreatIndSCORE").Description;
            ViewBag.currentSectionID = 4;
            return View(tblswatswpdev);
        }

        //
        // GET: /SWPDev/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatswpdev tblswatswpdev = db.tblswatswpdevs.Find(id);
        //    if (tblswatswpdev == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpdev);
        //}

        //
        // POST: /SWPDev/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatswpdev tblswatswpdev = db.tblswatswpdevs.Find(id);
        //    db.tblswatswpdevs.Remove(tblswatswpdev);
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