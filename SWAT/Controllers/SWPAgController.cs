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
    public class SWPAgController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SWPAg/

        //public ActionResult Index()
        //{
        //    var tblswatswpags = db.tblswatswpags.Include(t => t.lkpswat5ranklu).Include(t => t.lkpswat5ranklu1).Include(t => t.lkpSWATbestManagementLU).Include(t => t.lkpSWATcropLossLU).Include(t => t.lkpSWATerosionLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatswpags.ToList());
        //}

        //
        // GET: /SWPAg/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatswpag tblswatswpag = db.tblswatswpags.Find(id);
        //    if (tblswatswpag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpag);
        //}

        //
        // GET: /SWPAg/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int? agId = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID).isEconAg;
            if (agId != 1511)
            {
                // return RedirectToAction("Index");
                return RedirectToAction("Create", "SWPDev", new { SurveyID = SurveyID });
            }

            ViewBag.fertilizer = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.pesticide = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.bestManagement = new SelectList(db.lkpswatbestmanagementlus, "id", "Description");
            ViewBag.cropLoss = new SelectList(db.lkpswatcroplosslus, "id", "Description");
            ViewBag.erosion = new SelectList(db.lkpswaterosionlus, "id", "Description");

            @ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "agTypeSCORE").Description;
            @ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "fertilizerSCORE").Description;
            @ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "pesticideSCORE").Description;
            @ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "erosionSCORE").Description;
            @ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManagementSCORE").Description;
            @ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "cropLossSCORE").Description;

            return View();
        }

        private void updateScores(tblswatswpag tblswatswpag)
        {
            if (tblswatswpag.agType1)
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "agTypeSCORE").Value = 1;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "agTypeSCORE").Value = 0;
            }

            if (tblswatswpag.fertilizer != null)
            {
                int fertilizerOrder = (int)db.lkpswat5ranklu.Find(tblswatswpag.fertilizer).intorder;
                double fertilizerScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.First(e => e.intorder == fertilizerOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "fertilizerSCORE").Value = fertilizerScore;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "fertilizerSCORE").Value = null;
            }

            if (tblswatswpag.pesticide != null)
            {
                int pesticideOrder = (int)db.lkpswat5ranklu.Find(tblswatswpag.pesticide).intorder;
                double pesticideScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.First(e => e.intorder == pesticideOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "pesticideSCORE").Value = pesticideScore;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "pesticideSCORE").Value = null;
            }

            if (tblswatswpag.erosion != null)
            {
                int intOrder = (int)db.lkpswaterosionlus.Find(tblswatswpag.erosion).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_erosion.First(e => e.intorder == intOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "erosionSCORE").Value = score;
            }
            else 
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "erosionSCORE").Value = null;
            }

            if (tblswatswpag.bestManagement != null)
            {
                int intOrder = (int)db.lkpswatbestmanagementlus.Find(tblswatswpag.bestManagement).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_bestmanagement.First(e => e.intorder == intOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "bestManagementSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "bestManagementSCORE").Value = null;
            }

            if (tblswatswpag.cropLoss != null)
            {
                int intOrder = (int)db.lkpswatcroplosslus.Find(tblswatswpag.cropLoss).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_croploss.First(e => e.intorder == intOrder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "cropLossSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatswpag.SurveyID && e.VarName == "cropLossSCORE").Value = null;
            }

            db.SaveChanges();
        }

        //
        // POST: /SWPAg/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatswpag tblswatswpag)
        {
            if (ModelState.IsValid)
            {
                var agIDs = db.tblswatswpags.Where(e => e.SurveyID == tblswatswpag.SurveyID).Select(e => e.ID);
                if (agIDs.Any())
                {
                    int agId = agIDs.First();
                    tblswatswpag.ID = agId;
                    db.Entry(tblswatswpag).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatswpag);

                    // return RedirectToAction("Index");
                    return RedirectToAction("Create", "SWPDev", new { SurveyID = tblswatswpag.SurveyID });
                }

                db.tblswatswpags.Add(tblswatswpag);
                db.SaveChanges();
                updateScores(tblswatswpag);

                // return RedirectToAction("Index");
                return RedirectToAction("Create", "SWPDev", new { SurveyID = tblswatswpag.SurveyID });
            }

            ViewBag.fertilizer = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.fertilizer);
            ViewBag.pesticide = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.pesticide);
            ViewBag.bestManagement = new SelectList(db.lkpswatbestmanagementlus, "id", "Description", tblswatswpag.bestManagement);
            ViewBag.cropLoss = new SelectList(db.lkpswatcroplosslus, "id", "Description", tblswatswpag.cropLoss);
            ViewBag.erosion = new SelectList(db.lkpswaterosionlus, "id", "Description", tblswatswpag.erosion);

            @ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "agTypeSCORE").Description;
            @ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "fertilizerSCORE").Description;
            @ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "pesticideSCORE").Description;
            @ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "erosionSCORE").Description;
            @ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManagementSCORE").Description;
            @ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "cropLossSCORE").Description;

            return View(tblswatswpag);
        }

        //
        // GET: /SWPAg/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatswpag tblswatswpag = db.tblswatswpags.Find(id);
            if (tblswatswpag == null)
            {
                return HttpNotFound();
            }
            ViewBag.fertilizer = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.fertilizer);
            ViewBag.pesticide = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.pesticide);
            ViewBag.bestManagement = new SelectList(db.lkpswatbestmanagementlus, "id", "Description", tblswatswpag.bestManagement);
            ViewBag.cropLoss = new SelectList(db.lkpswatcroplosslus, "id", "Description", tblswatswpag.cropLoss);
            ViewBag.erosion = new SelectList(db.lkpswaterosionlus, "id", "Description", tblswatswpag.erosion);

            @ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "agTypeSCORE").Description;
            @ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "fertilizerSCORE").Description;
            @ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "pesticideSCORE").Description;
            @ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "erosionSCORE").Description;
            @ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManagementSCORE").Description;
            @ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "cropLossSCORE").Description;

            return View(tblswatswpag);
        }

        //
        // POST: /SWPAg/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatswpag tblswatswpag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatswpag).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatswpag);

                var background = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatswpag.SurveyID);
                if (background != null)
                {
                    // id = 1511 is the "Yes" option in database
                    if (background.isEconDev == 1511)
                    {
                        // TODO redirect to dev form
                        var swpdev = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatswpag.SurveyID);
                        if (!swpdev.Any())
                        {
                            tblswatswpdev tblswatswpdev = new tblswatswpdev();
                            tblswatswpdev.SurveyID = tblswatswpag.SurveyID;
                            db.tblswatswpdevs.Add(tblswatswpdev);
                            db.SaveChanges();

                            int newSWPdevID = tblswatswpdev.ID;
                            return RedirectToAction("Edit", "SWPDev", new { id = newSWPdevID, SurveyID = tblswatswpdev.SurveyID });
                        }
                        return RedirectToAction("Edit", "SWPDev", new { id = swpdev.First(e => e.SurveyID == tblswatswpag.SurveyID).ID, SurveyID = tblswatswpag.SurveyID });
                    }
                }
                // TODO redirect to health form
                var hppcom = db.tblswathppcoms.Where(e => e.SurveyID == tblswatswpag.SurveyID);
                if (!hppcom.Any())
                {
                    tblswathppcom tblswathppcom = new tblswathppcom();
                    tblswathppcom.SurveyID = tblswatswpag.SurveyID;
                    db.tblswathppcoms.Add(tblswathppcom);
                    db.SaveChanges();

                    int newHPPcomID = tblswathppcom.ID;
                    return RedirectToAction("Edit", "HPPCom", new { id = newHPPcomID, SurveyID = tblswathppcom.SurveyID });
                }
                return RedirectToAction("Edit", "HPPCom", new { id = hppcom.First(e => e.SurveyID == tblswatswpag.SurveyID).ID, SurveyID = tblswatswpag.SurveyID });
            
            }
            ViewBag.fertilizer = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.fertilizer);
            ViewBag.pesticide = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatswpag.pesticide);
            ViewBag.bestManagement = new SelectList(db.lkpswatbestmanagementlus, "id", "Description", tblswatswpag.bestManagement);
            ViewBag.cropLoss = new SelectList(db.lkpswatcroplosslus, "id", "Description", tblswatswpag.cropLoss);
            ViewBag.erosion = new SelectList(db.lkpswaterosionlus, "id", "Description", tblswatswpag.erosion);

            @ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "agTypeSCORE").Description;
            @ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "fertilizerSCORE").Description;
            @ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "pesticideSCORE").Description;
            @ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "erosionSCORE").Description;
            @ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "bestManagementSCORE").Description;
            @ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "cropLossSCORE").Description;

            return View(tblswatswpag);
        }

        //
        // GET: /SWPAg/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatswpag tblswatswpag = db.tblswatswpags.Find(id);
        //    if (tblswatswpag == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatswpag);
        //}

        //
        // POST: /SWPAg/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatswpag tblswatswpag = db.tblswatswpags.Find(id);
        //    db.tblswatswpags.Remove(tblswatswpag);
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