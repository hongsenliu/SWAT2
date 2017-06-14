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
    public class SWPLivestockController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SWPLivestock/

        //public ActionResult Index()
        //{
        //    var tblswatswpls = db.tblswatswpls.Include(t => t.lkpSWAT5rankLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatswpls.ToList());
        //}

        //
        // GET: /SWPLivestock/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatswpl tblswatswpl = db.tblswatswpls.Find(id);
        //    if (tblswatswpl == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpl);
        //}

        //
        // GET: /SWPLivestock/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int? lsId = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID).isEconLs;
            if (lsId != 1511)
            {
                // return RedirectToAction("Index");
                return RedirectToAction("Create", "SWPAg", new { SurveyID = SurveyID});
            }

            ViewBag.livestockEffluent = new SelectList(db.lkpswat5ranklu, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "waterFencedSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockEffluentSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View();
        }

        private void updateScores(tblswatswpl tblswatswpl)
        {
            if (tblswatswpl.livestock1 || tblswatswpl.livestock2)
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "livestockSCORE").Value = 1;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "livestockSCORE").Value = 0;
            }

            if (tblswatswpl.waterFenced != null)
            {
                double waterFencedScore = (double)tblswatswpl.waterFenced / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "waterFencedSCORE").Value = waterFencedScore;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "waterFencedSCORE").Value = null;
            }

            if (tblswatswpl.livestockEffluent != null)
            {
                int lsOrder = (int)db.lkpswat5ranklu.Find(tblswatswpl.livestockEffluent).intorder;
                double lsScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == lsOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "livestockEffluentSCORE").Value = lsScore;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpl.SurveyID && e.VarName == "livestockEffluentSCORE").Value = null;
            }
            db.SaveChanges();

        }

        //
        // POST: /SWPLivestock/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatswpl tblswatswpl, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var lsIDs = db.tblswatswpls.Where(e => e.SurveyID == tblswatswpl.SurveyID).Select(e => e.ID);
                if (lsIDs.Any())
                {
                    int lsId = lsIDs.First();
                    tblswatswpl.ID = lsId;
                    db.Entry(tblswatswpl).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatswpls.Add(tblswatswpl);
                }
                db.SaveChanges();
                updateScores(tblswatswpl);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "SWPAg", new { SurveyID = tblswatswpl.SurveyID });
                }
            }

            ViewBag.livestockEffluent = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpl.livestockEffluent);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "waterFencedSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockEffluentSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatswpl.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.SurveyID = tblswatswpl.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatswpl);
        }

        //
        // GET: /SWPLivestock/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatswpl tblswatswpl = db.tblswatswpls.Find(id);
            if (tblswatswpl == null)
            {
                return HttpNotFound();
            }
            ViewBag.livestockEffluent = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpl.livestockEffluent);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "waterFencedSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockEffluentSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatswpl.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.SurveyID = tblswatswpl.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatswpl);
        }

        //
        // POST: /SWPLivestock/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatswpl tblswatswpl, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatswpl).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatswpl);

                if (submitBtn.Equals("Next"))
                {
                    var background = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatswpl.SurveyID);
                    if (background != null)
                    {
                        // id = 1511 is the "Yes" option in database
                        if (background.isEconAg == 1511)
                        {
                            // TODO redirect to ag form
                            var swpag = db.tblswatswpags.Where(e => e.SurveyID == tblswatswpl.SurveyID);
                            if (!swpag.Any())
                            {
                                tblswatswpag tblswatswpag = new tblswatswpag();
                                tblswatswpag.SurveyID = tblswatswpl.SurveyID;
                                db.tblswatswpags.Add(tblswatswpag);
                                db.SaveChanges();

                                int newSWPagID = tblswatswpag.ID;
                                return RedirectToAction("Edit", "SWPAg", new { id = newSWPagID, SurveyID = tblswatswpag.SurveyID });
                            }
                            return RedirectToAction("Edit", "SWPAg", new { id = swpag.First(e => e.SurveyID == tblswatswpl.SurveyID).ID, SurveyID = tblswatswpl.SurveyID });
                        }
                        else if (background.isEconDev == 1511)
                        {
                            // TODO redirect to dev form
                            var swpdev = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatswpl.SurveyID);
                            if (!swpdev.Any())
                            {
                                tblswatswpdev tblswatswpdev = new tblswatswpdev();
                                tblswatswpdev.SurveyID = tblswatswpl.SurveyID;
                                db.tblswatswpdevs.Add(tblswatswpdev);
                                db.SaveChanges();

                                int newSWPdevID = tblswatswpdev.ID;
                                return RedirectToAction("Edit", "SWPDev", new { id = newSWPdevID, SurveyID = tblswatswpdev.SurveyID });
                            }
                            return RedirectToAction("Edit", "SWPDev", new { id = swpdev.First(e => e.SurveyID == tblswatswpl.SurveyID).ID, SurveyID = tblswatswpl.SurveyID });
                        }
                    }
                    // TODO redirect to health form
                    var hppcom = db.tblswathppcoms.Where(e => e.SurveyID == tblswatswpl.SurveyID);
                    if (!hppcom.Any())
                    {
                        tblswathppcom tblswathppcom = new tblswathppcom();
                        tblswathppcom.SurveyID = tblswatswpl.SurveyID;
                        db.tblswathppcoms.Add(tblswathppcom);
                        db.SaveChanges();

                        int newHPPcomID = tblswathppcom.ID;
                        return RedirectToAction("Edit", "HPPCom", new { id = newHPPcomID, SurveyID = tblswathppcom.SurveyID });
                    }
                    return RedirectToAction("Edit", "HPPCom", new { id = hppcom.First(e => e.SurveyID == tblswatswpl.SurveyID).ID, SurveyID = tblswatswpl.SurveyID });
                }
            }
            ViewBag.livestockEffluent = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpl.livestockEffluent);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "waterFencedSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "livestockEffluentSCORE").Description;
            ViewBag.currentSectionID = 4;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatswpl.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.cceduID = db.tblswatccedus.First(e => e.SurveyID == tblswatswpl.SurveyID).ID;
            ViewBag.SurveyID = tblswatswpl.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatswpl);
        }

        //
        // GET: /SWPLivestock/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatswpl tblswatswpl = db.tblswatswpls.Find(id);
        //    if (tblswatswpl == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpl);
        //}

        //
        // POST: /SWPLivestock/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatswpl tblswatswpl = db.tblswatswpls.Find(id);
        //    db.tblswatswpls.Remove(tblswatswpl);
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