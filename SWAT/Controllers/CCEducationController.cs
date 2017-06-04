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
    public class CCEducationController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /CCEducation/
        //public ActionResult Index()
        //{
        //    var tblswatccedus = db.tblswatccedus.Include(t => t.tblSWATSurvey);
        //    return View(tblswatccedus.ToList());
        //}

        // GET: /CCEducation/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccedu tblswatccedu = db.tblswatccedus.Find(id);
        //    if (tblswatccedu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccedu);
        //}

        // GET: /CCEducation/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduPrimeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduSecSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradWomenSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradMenSCORE").Description;
            return View();
        }

        private void updateScores(tblswatccedu tblswatccedu)
        {
            if (tblswatccedu.eduPrim != null)
            {
                double? eduPrimeScore = tblswatccedu.eduPrim / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduPrimeSCORE").Value = eduPrimeScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduPrimeSCORE").Value = null;
            }

            if (tblswatccedu.eduSec != null)
            {
                double? eduSecScore = tblswatccedu.eduSec / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduSecSCORE").Value = eduSecScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduSecSCORE").Value = null;
            }

            if (tblswatccedu.eduGradWomen != null)
            {
                double? eduGradWomenScore = tblswatccedu.eduGradWomen / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradWomenSCORE").Value = eduGradWomenScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradWomenSCORE").Value = null;
            }

            if (tblswatccedu.eduGradMen != null)
            {
                double? eduGradMenScore = tblswatccedu.eduGradMen / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradMenSCORE").Value = eduGradMenScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradMenSCORE").Value = null;
            }

            if (tblswatccedu.eduGradMen != null && tblswatccedu.eduGradWomen != null)
            {
                double? eduGradDiffMenWomen = 2 * (tblswatccedu.eduGradMen - tblswatccedu.eduGradWomen) / (tblswatccedu.eduGradMen + tblswatccedu.eduGradWomen);
                int? eduGradDiffOrder = null;
                foreach (var item in db.lkpswatedugraddifflus.OrderByDescending(e => e.Description))
                {
                    if (Double.Parse(item.Description) <= eduGradDiffMenWomen)
                    {
                        eduGradDiffOrder = item.intorder;
                        break;
                    }
                }
                double? eduGradDiffScore = Double.Parse(db.lkpswatscores_edugraddiff.Single(e => e.intorder == eduGradDiffOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradDiffMenWomen").Value = eduGradDiffMenWomen;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradDiffMenWomenSCORE").Value = eduGradDiffScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradDiffMenWomen").Value = null;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccedu.SurveyID && e.VarName == "eduGradDiffMenWomenSCORE").Value = null;
            }

            db.SaveChanges();

        }

        // POST: /CCEducation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,eduPrim,eduSec,eduGradWomen,eduGradMen")] tblswatccedu tblswatccedu, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var educationIDs = db.tblswatccedus.Where(e => e.SurveyID == tblswatccedu.SurveyID).Select(e => e.ID);
                if (educationIDs.Any())
                {
                    int educationId = educationIDs.First();
                    tblswatccedu.ID = educationId;
                    db.Entry(tblswatccedu).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatccedus.Add(tblswatccedu);
                }
                db.SaveChanges();
                updateScores(tblswatccedu);
                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "CCTrain", new { SurveyID = tblswatccedu.SurveyID });
                }
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduPrimeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduSecSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradWomenSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradMenSCORE").Description;
            return View(tblswatccedu);
        }

        // GET: /CCEducation/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccedu tblswatccedu = db.tblswatccedus.Find(id);
            if (tblswatccedu == null)
            {
                return HttpNotFound();
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduPrimeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduSecSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradWomenSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradMenSCORE").Description;
            return View(tblswatccedu);
        }

        // POST: /CCEducation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,eduPrim,eduSec,eduGradWomen,eduGradMen")] tblswatccedu tblswatccedu, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccedu).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccedu);

                if (submitBtn.Equals("Next"))
                {
                    // If there is not any CCTrain with the current survey (SurveyID) then create one and redirect to its edit link.
                    var trains = db.tblswatcctrains.Where(e => e.SurveyID == tblswatccedu.SurveyID);
                    if (!trains.Any())
                    {
                        tblswatcctrain tblswatcctrain = new tblswatcctrain();
                        tblswatcctrain.SurveyID = tblswatccedu.SurveyID;
                        db.tblswatcctrains.Add(tblswatcctrain);
                        db.SaveChanges();

                        int newTrainID = tblswatcctrain.ID;
                        return RedirectToAction("Edit", "CCTrain", new { id = newTrainID, SurveyID = tblswatcctrain.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "CCTrain", new { id = trains.Single(e => e.SurveyID == tblswatccedu.SurveyID).ID, SurveyID = tblswatccedu.SurveyID });
                    }
                }
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduPrimeSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduSecSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradWomenSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "eduGradMenSCORE").Description;
            return View(tblswatccedu);
        }

        // GET: /CCEducation/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccedu tblswatccedu = db.tblswatccedus.Find(id);
        //    if (tblswatccedu == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccedu);
        //}

        // POST: /CCEducation/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccedu tblswatccedu = db.tblswatccedus.Find(id);
        //    db.tblswatccedus.Remove(tblswatccedu);
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
