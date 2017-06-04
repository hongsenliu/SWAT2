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
    public class HPPComController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /HPPCom/

        //public ActionResult Index()
        //{
        //    var tblswathppcoms = db.tblSWATHPPcoms.Include(t => t.lkpSWAT5rankLU).Include(t => t.lkpSWATmedicalCostLU).Include(t => t.lkpSWATmedicalTimeLU).Include(t => t.lkpSWATsurvivorshipLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswathppcoms.ToList());
        //}

        //
        // GET: /HPPCom/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblSWATHPPcom tblswathppcom = db.tblSWATHPPcoms.Find(id);
        //    if (tblswathppcom == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswathppcom);
        //}

        //
        // GET: /HPPCom/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.diarrhea = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.medicalCost = new SelectList(db.lkpswatmedicalcostlus, "id", "Description");
            ViewBag.medicalTime = new SelectList(db.lkpswatmedicaltimelus, "id", "Description");
            ViewBag.survivorship = new SelectList(db.lkpswatsurvivorshiplus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "diarrheaSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "survivorshipSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalTimeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalCostSCORE").Description;

            return View();
        }

        private void updateScores(tblswathppcom tblswathppcom)
        {
            if (tblswathppcom.diarrhea != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswathppcom.diarrhea).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "diarrheaSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "diarrheaSCORE").Value = null;
            }

            if (tblswathppcom.survivorship != null)
            {
                int intorder = (int)db.lkpswatsurvivorshiplus.Find(tblswathppcom.survivorship).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_survivorship.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "survivorshipSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "survivorshipSCORE").Value = null;
            }

            if (tblswathppcom.medicalTime != null)
            {
                int intorder = (int)db.lkpswatmedicaltimelus.Find(tblswathppcom.medicalTime).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_medicaltime.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "medicalTimeSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "medicalTimeSCORE").Value = null;
            }

            if (tblswathppcom.medicalCost != null)
            {
                int intorder = (int)db.lkpswatmedicalcostlus.Find(tblswathppcom.medicalCost).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_medicalcost.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "medicalCostSCORE").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswathppcom.SurveyID && e.VarName == "medicalCostSCORE").Value = null;
            }

            db.SaveChanges();
        }

        //
        // POST: /HPPCom/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswathppcom tblswathppcom, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswathppcoms.Where(e => e.SurveyID == tblswathppcom.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswathppcom.ID = recordId;
                    db.Entry(tblswathppcom).State = EntityState.Modified;
                }
                else
                {
                    db.tblswathppcoms.Add(tblswathppcom);
                }
                db.SaveChanges();
                updateScores(tblswathppcom);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "HPPKhp", new { SurveyID = tblswathppcom.SurveyID });
                }
            }

            ViewBag.diarrhea = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppcom.diarrhea);
            ViewBag.medicalCost = new SelectList(db.lkpswatmedicalcostlus, "id", "Description", tblswathppcom.medicalCost);
            ViewBag.medicalTime = new SelectList(db.lkpswatmedicaltimelus, "id", "Description", tblswathppcom.medicalTime);
            ViewBag.survivorship = new SelectList(db.lkpswatsurvivorshiplus, "id", "Description", tblswathppcom.survivorship);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "diarrheaSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "survivorshipSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalTimeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalCostSCORE").Description;

            return View(tblswathppcom);
        }

        //
        // GET: /HPPCom/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswathppcom tblswathppcom = db.tblswathppcoms.Find(id);
            if (tblswathppcom == null)
            {
                return HttpNotFound();
            }
            ViewBag.diarrhea = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppcom.diarrhea);
            ViewBag.medicalCost = new SelectList(db.lkpswatmedicalcostlus, "id", "Description", tblswathppcom.medicalCost);
            ViewBag.medicalTime = new SelectList(db.lkpswatmedicaltimelus, "id", "Description", tblswathppcom.medicalTime);
            ViewBag.survivorship = new SelectList(db.lkpswatsurvivorshiplus, "id", "Description", tblswathppcom.survivorship);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "diarrheaSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "survivorshipSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalTimeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalCostSCORE").Description;

            return View(tblswathppcom);
        }

        //
        // POST: /HPPCom/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswathppcom tblswathppcom, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswathppcom).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswathppcom);

                if (submitBtn.Equals("Next"))
                {
                    var records = db.tblswathppkhps.Where(e => e.SurveyID == tblswathppcom.SurveyID);
                    if (!records.Any())
                    {
                        tblswathppkhp newEntry = new tblswathppkhp();
                        newEntry.SurveyID = tblswathppcom.SurveyID;
                        db.tblswathppkhps.Add(newEntry);
                        db.SaveChanges();

                        int newId = newEntry.ID;
                        return RedirectToAction("Edit", "HPPKhp", new { id = newId, SurveyID = newEntry.SurveyID });
                    }

                    return RedirectToAction("Edit", "HPPKhp", new { id = records.First(e => e.SurveyID == tblswathppcom.SurveyID).ID, SurveyID = tblswathppcom.SurveyID });
                }
            }
            ViewBag.diarrhea = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswathppcom.diarrhea);
            ViewBag.medicalCost = new SelectList(db.lkpswatmedicalcostlus, "id", "Description", tblswathppcom.medicalCost);
            ViewBag.medicalTime = new SelectList(db.lkpswatmedicaltimelus, "id", "Description", tblswathppcom.medicalTime);
            ViewBag.survivorship = new SelectList(db.lkpswatsurvivorshiplus, "id", "Description", tblswathppcom.survivorship);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "diarrheaSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "survivorshipSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalTimeSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "medicalCostSCORE").Description;

            return View(tblswathppcom);
        }

        //
        // GET: /HPPCom/Delete/5

        public ActionResult Delete(int id = 0)
        {
            tblswathppcom tblswathppcom = db.tblswathppcoms.Find(id);
            if (tblswathppcom == null)
            {
                return HttpNotFound();
            }
            return View(tblswathppcom);
        }

        //
        // POST: /HPPCom/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblswathppcom tblswathppcom = db.tblswathppcoms.Find(id);
            db.tblswathppcoms.Remove(tblswathppcom);
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