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
    public class CCFinancialController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /CCFinancial/
        //public ActionResult Index()
        //{
        //    var tblswatccfinancials = db.tblswatccfinancials.Include(t => t.lkpSWATroscaLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccfinancials.ToList());
        //}

        // GET: /CCFinancial/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccfinancial tblswatccfinancial = db.tblswatccfinancials.Find(id);
        //    if (tblswatccfinancial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccfinancial);
        //}

        // GET: /CCFinancial/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.rosca = new SelectList(db.lkpswatroscalus, "id", "Description");
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "incomeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "roscaSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsComSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsIndSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View();
        }

        private void updateScores(tblswatccfinancial tblswatccfinancial)
        {
            if (tblswatccfinancial.income != null)
            {
                double? incomeScore = tblswatccfinancial.income / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "incomeSCORE").Value = incomeScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "incomeSCORE").Value = null;
            }

            if (tblswatccfinancial.rosca != null)
            {
                int? roscaOrder = db.lkpswatroscalus.Find(tblswatccfinancial.rosca).intorder;
                double roscaScore = Double.Parse(db.lkpswatscores_rosca.Single(e => e.intorder == roscaOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "roscaSCORE").Value = roscaScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "roscaSCORE").Value = null;
            }

            bool[] comArr = { tblswatccfinancial.assetsCom1, tblswatccfinancial.assetsCom2, tblswatccfinancial.assetsCom3, tblswatccfinancial.assetsCom4 };

            int numComTrue = 0;
            foreach (bool item in comArr)
            {
                if (item)
                {
                    numComTrue++;
                }
            }
            double comScore = (double)numComTrue / 4.0;
            db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "assetsComSCORE").Value = comScore;

            bool[] indArr = { tblswatccfinancial.assetsInd1, tblswatccfinancial.assetsInd2, tblswatccfinancial.assetsInd3, tblswatccfinancial.assetsInd4};

            int numIndTrue = 0;
            foreach (bool item in indArr)
            {
                if (item)
                {
                    numIndTrue++;
                }
            }

            double indScore = (double)numIndTrue / 4.0;
            db.tblswatscores.Single(e => e.SurveyID == tblswatccfinancial.SurveyID && e.VarName == "assetsIndSCORE").Value = indScore;

            db.SaveChanges();
        }

        // POST: /CCFinancial/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,income,rosca,assetsCom1,assetsCom2,assetsCom3,assetsCom4,assetsInd1,assetsInd2,assetsInd3,assetsInd4")] tblswatccfinancial tblswatccfinancial, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var financialIDs = db.tblswatccfinancials.Where(e => e.SurveyID == tblswatccfinancial.SurveyID).Select(e => e.ID);

                if (financialIDs.Any())
                {
                    int financialId = financialIDs.First();
                    tblswatccfinancial.ID = financialId;
                    db.Entry(tblswatccfinancial).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatccfinancials.Add(tblswatccfinancial);
                }
                db.SaveChanges();
                updateScores(tblswatccfinancial);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "CCGender", new { SurveyID = tblswatccfinancial.SurveyID });
                }
            }

            ViewBag.rosca = new SelectList(db.lkpswatroscalus, "id", "Description", tblswatccfinancial.rosca);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "incomeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "roscaSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsComSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsIndSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatccfinancial.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatccfinancial.SurveyID).ID;
            ViewBag.SurveyID = tblswatccfinancial.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccfinancial);
        }

        // GET: /CCFinancial/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblswatccfinancial tblswatccfinancial = db.tblswatccfinancials.Find(id);
            if (tblswatccfinancial == null)
            {
                return HttpNotFound();
            }
            ViewBag.rosca = new SelectList(db.lkpswatroscalus, "id", "Description", tblswatccfinancial.rosca);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "incomeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "roscaSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsComSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsIndSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccfinancial);
        }

        // POST: /CCFinancial/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,income,rosca,assetsCom1,assetsCom2,assetsCom3,assetsCom4,assetsInd1,assetsInd2,assetsInd3,assetsInd4")] tblswatccfinancial tblswatccfinancial, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccfinancial).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccfinancial);

                if (submitBtn.Equals("Next"))
                {
                    // If there is not any CCGender with the current survey (SurveyID) then create one and redirect to its edit link.
                    var genders = db.tblswatccgenders.Where(e => e.SurveyID == tblswatccfinancial.SurveyID);
                    if (!genders.Any())
                    {
                        tblswatccgender tblswatccgender = new tblswatccgender();
                        tblswatccgender.SurveyID = tblswatccfinancial.SurveyID;
                        db.tblswatccgenders.Add(tblswatccgender);
                        db.SaveChanges();
                        int newGenderId = tblswatccgender.ID;

                        return RedirectToAction("Edit", "CCGender", new { id = newGenderId, SurveyID = tblswatccgender.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "CCGender", new { id = genders.Single(e => e.SurveyID == tblswatccfinancial.SurveyID).ID, SurveyID = tblswatccfinancial.SurveyID });
                    }
                }
            }
            ViewBag.rosca = new SelectList(db.lkpswatroscalus, "id", "Description", tblswatccfinancial.rosca);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "incomeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "roscaSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsComSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "assetsIndSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatccfinancial.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatccfinancial.SurveyID).ID;
            ViewBag.SurveyID = tblswatccfinancial.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccfinancial);
        }

        // GET: /CCFinancial/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccfinancial tblswatccfinancial = db.tblswatccfinancials.Find(id);
        //    if (tblswatccfinancial == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccfinancial);
        //}

        // POST: /CCFinancial/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccfinancial tblswatccfinancial = db.tblswatccfinancials.Find(id);
        //    db.tblswatccfinancials.Remove(tblswatccfinancial);
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
