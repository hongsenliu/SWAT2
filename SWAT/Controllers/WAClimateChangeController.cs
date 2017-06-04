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
    public class WAClimateChangeController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /WAClimateChange/
        //public ActionResult Index()
        //{
        //    var tblswatwaclimatechanges = db.tblswatwaclimatechanges.Include(t => t.tblSWATSurvey);
        //    return View(tblswatwaclimatechanges.ToList());
        //}

        // GET: /WAClimateChange/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwaclimatechange tblswatwaclimatechange = db.tblswatwaclimatechanges.Find(id);
        //    if (tblswatwaclimatechange == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwaclimatechange);
        //}

        // GET: /WAClimateChange/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        private void updateScores(tblswatwaclimatechange tblswatwaclimatechange)
        {
            double climateDiffScore = 0;
            double climateChangeTrues = 0;

            bool[] climateChanges = {tblswatwaclimatechange.climateDryer, tblswatwaclimatechange.climateWetter, 
                                     tblswatwaclimatechange.climateColder, tblswatwaclimatechange.climateHotter,
                                     tblswatwaclimatechange.climateSeasons};
            foreach (bool climateChange in climateChanges)
            {
                if (climateChange)
                {
                    climateChangeTrues++;
                }
            }
            
            climateDiffScore = climateChangeTrues / 5.0;
            db.tblswatscores.Single(e => e.SurveyID == tblswatwaclimatechange.SurveyID && e.VarName == "climateDiffSCORE").Value = climateDiffScore;
            db.SaveChanges();
        }

        // POST: /WAClimateChange/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,climateDryer,climateWetter,climateColder,climateHotter,climateSeasons")] tblswatwaclimatechange tblswatwaclimatechange, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var climateChangeIDs = db.tblswatwaclimatechanges.Where(e => e.SurveyID == tblswatwaclimatechange.SurveyID).Select(e => e.ID);
                if (climateChangeIDs.Any())
                {
                    int climateChangeId = climateChangeIDs.First();
                    tblswatwaclimatechange.ID = climateChangeId;
                    db.Entry(tblswatwaclimatechange).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatwaclimatechanges.Add(tblswatwaclimatechange);
                }
                
                db.SaveChanges();
                updateScores(tblswatwaclimatechange);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WAExtremeEvent", new { SurveyID = tblswatwaclimatechange.SurveyID });
                }
            }

            return View(tblswatwaclimatechange);
        }

        // GET: /WAClimateChange/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatwaclimatechange tblswatwaclimatechange = db.tblswatwaclimatechanges.Find(id);
            if (tblswatwaclimatechange == null)
            {
                return HttpNotFound();
            }
            return View(tblswatwaclimatechange);
        }

        // POST: /WAClimateChange/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,climateDryer,climateWetter,climateColder,climateHotter,climateSeasons")] tblswatwaclimatechange tblswatwaclimatechange, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatwaclimatechange).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwaclimatechange);
                if (submitBtn.Equals("Next"))
                {
                    // If there is not any WAExtremeEvent with the current survey (SurveyID) then create one and redirect to its edit link.
                    var extremeEvents = db.tblswatwaextremeevents.Where(e => e.SurveyID == tblswatwaclimatechange.SurveyID);
                    if (!extremeEvents.Any())
                    {
                        tblswatwaextremeevent tblswatwaextremeevent = new tblswatwaextremeevent();
                        tblswatwaextremeevent.SurveyID = tblswatwaclimatechange.SurveyID;
                        db.tblswatwaextremeevents.Add(tblswatwaextremeevent);
                        db.SaveChanges();

                        int newExtremeEventID = tblswatwaextremeevent.ID;
                        return RedirectToAction("Edit", "WAExtremeEvent", new { id = newExtremeEventID, SurveyID = tblswatwaextremeevent.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "WAExtremeEvent", new { id = extremeEvents.Single(e => e.SurveyID == tblswatwaclimatechange.SurveyID).ID, SurveyID = tblswatwaclimatechange.SurveyID });
                    }

                }
            }
            return View(tblswatwaclimatechange);
        }

        // GET: /WAClimateChange/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwaclimatechange tblswatwaclimatechange = db.tblswatwaclimatechanges.Find(id);
        //    if (tblswatwaclimatechange == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwaclimatechange);
        //}

        // POST: /WAClimateChange/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatwaclimatechange tblswatwaclimatechange = db.tblswatwaclimatechanges.Find(id);
        //    db.tblswatwaclimatechanges.Remove(tblswatwaclimatechange);
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
