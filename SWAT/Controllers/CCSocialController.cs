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
    public class CCSocialController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /CCSocial/

        //public ActionResult Index()
        //{
        //    var tblswatccsocials = db.tblSWATCCsocials.Include(t => t.lkpSWAT5rankLU).Include(t => t.lkpSWAT5rankLU1).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccsocials.ToList());
        //}

        //
        // GET: /CCSocial/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblSWATCCsocial tblswatccsocial = db.tblSWATCCsocials.Find(id);
        //    if (tblswatccsocial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccsocial);
        //}

        //
        // GET: /CCSocial/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.socHelp = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.socClique = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.socCBO = new SelectList(db.lkpswatsoccbolus, "id", "Description");
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "socHelpSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCliqueSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCBOSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "socAttendSCORE").Description;
            return View();
        }

        private void updateScores(tblswatccsocial tblswatccsocial)
        {
            if (tblswatccsocial.socHelp != null)
            {
                int socHelpOrder = Convert.ToInt32(db.lkpswat5ranklu.Find(tblswatccsocial.socHelp).intorder);
                double socHelpScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.Single(e => e.intorder == socHelpOrder).Description);
                db.tblswatscores.Single(e => e.VarName == "socHelpSCORE" && e.SurveyID == tblswatccsocial.SurveyID).Value = socHelpScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.VarName == "socHelpSCORE" && e.SurveyID == tblswatccsocial.SurveyID).Value = null;
            }

            if (tblswatccsocial.socClique != null)
            {
                int socCliqueOrder = Convert.ToInt32(db.lkpswat5ranklu.Find(tblswatccsocial.socClique).intorder);
                double socCliqueScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.Single(e => e.intorder == socCliqueOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socCliqueSCORE").Value = socCliqueScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socCliqueSCORE").Value = null;
            }

            if (tblswatccsocial.socCBO != null)
            {
                int socCboOrder = Convert.ToInt32(db.lkpswatsoccbolus.Find(tblswatccsocial.socCBO).intorder);
                double socCboScore = Convert.ToDouble(db.lkpswatscores_soccbo.Single(e => e.intorder == socCboOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socCBOSCORE").Value = socCboScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socCBOSCORE").Value = null;
            }

            if (tblswatccsocial.socAttend != null)
            {
                double socAttendScore = (double)tblswatccsocial.socAttend / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socAttendSCORE").Value = socAttendScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccsocial.SurveyID && e.VarName == "socAttendSCORE").Value = null;
            }

            db.SaveChanges();
        }

        //
        // POST: /CCSocial/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatccsocial tblswatccsocial)
        {
            if (ModelState.IsValid)
            {
                var socialIDs = db.tblswatccsocials.Where(e => e.SurveyID == tblswatccsocial.SurveyID).Select(e => e.ID);
                if (socialIDs.Any())
                {
                    int socialID = socialIDs.First();
                    tblswatccsocial.ID = socialID;
                    db.Entry(tblswatccsocial).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatccsocial);

                    // return RedirectToAction("Index");
                    // return RedirectToAction("Report", "Survey", new { id = tblswatccsocial.SurveyID });
                    return RedirectToAction("Create", "CCCom", new { SurveyID = tblswatccsocial.SurveyID });
                }

                db.tblswatccsocials.Add(tblswatccsocial);
                db.SaveChanges();
                updateScores(tblswatccsocial);

                // return RedirectToAction("Index");
                // return RedirectToAction("Report", "Survey", new { id = tblswatccsocial.SurveyID });
                return RedirectToAction("Create", "CCCom", new { SurveyID = tblswatccsocial.SurveyID });
            }

            ViewBag.socHelp = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socHelp);
            ViewBag.socClique = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socClique);
            ViewBag.socCBO = new SelectList(db.lkpswatsoccbolus, "id", "Description", tblswatccsocial.socCBO);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "socHelpSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCliqueSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCBOSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "socAttendSCORE").Description;
            return View(tblswatccsocial);
        }

        //
        // GET: /CCSocial/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccsocial tblswatccsocial = db.tblswatccsocials.Find(id);
            if (tblswatccsocial == null)
            {
                return HttpNotFound();
            }
            ViewBag.socHelp = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socHelp);
            ViewBag.socClique = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socClique);
            ViewBag.socCBO = new SelectList(db.lkpswatsoccbolus, "id", "Description", tblswatccsocial.socCBO);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "socHelpSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCliqueSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCBOSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "socAttendSCORE").Description;
            return View(tblswatccsocial);
        }

        //
        // POST: /CCSocial/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatccsocial tblswatccsocial)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccsocial).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccsocial);

                // If there is not any CCCom with the current survey (SurveyID) then create one and redirect to its edit link.
                var coms = db.tblswatcccoms.Where(e => e.SurveyID == tblswatccsocial.SurveyID);
                if (!coms.Any())
                {
                    tblswatcccom tblswatcccom = new tblswatcccom();
                    tblswatcccom.SurveyID = tblswatccsocial.SurveyID;
                    db.tblswatcccoms.Add(tblswatcccom);
                    db.SaveChanges();

                    int newCCcomId = tblswatcccom.ID;
                    return RedirectToAction("Edit", "CCCom", new { id = newCCcomId, SurveyID = tblswatcccom.SurveyID});
                }

                return RedirectToAction("Edit", "CCCom", new { id = coms.Single(e => e.SurveyID == tblswatccsocial.SurveyID).ID, SurveyID = tblswatccsocial.SurveyID});
            }
            ViewBag.socHelp = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socHelp);
            ViewBag.socClique = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccsocial.socClique);
            ViewBag.socCBO = new SelectList(db.lkpswatsoccbolus, "id", "Description", tblswatccsocial.socCBO);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "socHelpSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCliqueSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "socCBOSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "socAttendSCORE").Description;
            return View(tblswatccsocial);
        }

        //
        // GET: /CCSocial/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblSWATCCsocial tblswatccsocial = db.tblSWATCCsocials.Find(id);
        //    if (tblswatccsocial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccsocial);
        //}

        //
        // POST: /CCSocial/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblSWATCCsocial tblswatccsocial = db.tblSWATCCsocials.Find(id);
        //    db.tblSWATCCsocials.Remove(tblswatccsocial);
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