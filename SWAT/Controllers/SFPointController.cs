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
    public class SFPointController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SFPoint/

        //public ActionResult Index()
        //{
        //    var tblswatsfpoints = db.tblswatsfpoints.Include(t => t.lkpSWATsanTypeLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatsfpoints.ToList());
        //}

        //
        // GET: /SFPoint/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatsfpoint tblswatsfpoint = db.tblswatsfpoints.Find(id);
        //    if (tblswatsfpoint == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfpoint);
        //}

        //
        // GET: /SFPoint/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.sanType = new SelectList(db.lkpswatsantypelus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "sanTypeVAL").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "SanUSeSUMMARY").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View();
        }

        private void removeCSL(tblswatsfpoint tblswatsfpoint)
        {
            // TODO remove CSL SF
            // type = [1001, 1002, 1003]
            
            var centrals = db.tblswatsfcentrals.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
            foreach (tblswatsfcentral c in centrals)
            {
                db.tblswatsfcentrals.Remove(c);
            }

            var septics = db.tblswatsfseptics.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
            foreach (tblswatsfseptic s in septics)
            {
                db.tblswatsfseptics.Remove(s);
            }

            var lats = db.tblswatsflats.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
            foreach (tblswatsflat l in lats)
            {
                db.tblswatsflats.Remove(l);
            }

            var scores = db.tblswatscores.Include(t => t.lkpswatscorevarslu).Where(e => e.SurveyID == tblswatsfpoint.SurveyID && (e.lkpswatscorevarslu.SectionID == 9 || e.lkpswatscorevarslu.SectionID == 10 || e.lkpswatscorevarslu.SectionID == 11));
            
            foreach (tblswatscore s in scores)
            {
                s.Value = null;
            }
            db.SaveChanges();
        }

        private void updateScores(tblswatsfpoint tblswatsfpoint)
        {
            if (tblswatsfpoint.sanType != null)
            {
                int intorder = (int)db.lkpswatsantypelus.Find(tblswatsfpoint.sanType).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_santype.First(e => e.intorder == intorder).Description);
                db.tblswatscores.First(e => e.SurveyID == tblswatsfpoint.SurveyID && e.VarName == "sanTypeVAL").Value = score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfpoint.SurveyID && e.VarName == "sanTypeVAL").Value = null;
                removeCSL(tblswatsfpoint);
            }

            bool[] sanUse = { tblswatsfpoint.sanUseInd, tblswatsfpoint.SanUseCom, tblswatsfpoint.sanUsePub};
            int sanUseCounter = 0;

            foreach (bool item in sanUse)
            {
                if (item)
                {
                    sanUseCounter++;
                }
            }
            double sanUseScore = (double)sanUseCounter / 3;
            db.tblswatscores.First(e => e.SurveyID == tblswatsfpoint.SurveyID && e.VarName == "SanUSeSUMMARY").Value = sanUseScore;
            db.SaveChanges();
            
        }

        //
        // POST: /SFPoint/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatsfpoint tblswatsfpoint, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatsfpoints.Where(e => e.SurveyID == tblswatsfpoint.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswatsfpoint.ID = recordId;
                    db.Entry(tblswatsfpoint).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatsfpoints.Add(tblswatsfpoint);
                }
                db.SaveChanges();
                updateScores(tblswatsfpoint);

                if (submitBtn.Equals("Next"))
                {
                    // sanType = [1001, 1002, 1003]
                    if (tblswatsfpoint.sanType == 1001)
                    {
                        return RedirectToAction("Create", "SFCentral", new { SurveyID = tblswatsfpoint.SurveyID });
                    }
                    else if (tblswatsfpoint.sanType == 1002)
                    {
                        return RedirectToAction("Create", "SFSeptic", new { SurveyID = tblswatsfpoint.SurveyID });
                    }
                    else if (tblswatsfpoint.sanType == 1003)
                    {
                        return RedirectToAction("Create", "SFLat", new { SurveyID = tblswatsfpoint.SurveyID });
                    }
                    return RedirectToAction("WaterPoints", "Survey", new { id = tblswatsfpoint.SurveyID });
                }
            }

            ViewBag.sanType = new SelectList(db.lkpswatsantypelus, "id", "Description", tblswatsfpoint.sanType);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "sanTypeVAL").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "SanUSeSUMMARY").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfpoint.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfpoint);
        }

        //
        // GET: /SFPoint/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsfpoint tblswatsfpoint = db.tblswatsfpoints.Find(id);
            if (tblswatsfpoint == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanType = new SelectList(db.lkpswatsantypelus, "id", "Description", tblswatsfpoint.sanType);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "sanTypeVAL").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "SanUSeSUMMARY").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfpoint.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfpoint);
        }

        //
        // POST: /SFPoint/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatsfpoint tblswatsfpoint, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatsfpoint).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatsfpoint);

                if (submitBtn.Equals("Next"))
                {
                    // sanType = [1001, 1002, 1003]
                    if (tblswatsfpoint.sanType == 1001)
                    {
                        var records = db.tblswatsfcentrals.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
                        if (!records.Any())
                        {
                            tblswatsfcentral newEntry = new tblswatsfcentral();
                            newEntry.SurveyID = tblswatsfpoint.SurveyID;
                            db.tblswatsfcentrals.Add(newEntry);
                            db.SaveChanges();

                            int newId = newEntry.ID;
                            return RedirectToAction("Edit", "SFCentral", new { id = newId, SurveyID = newEntry.SurveyID });
                        }

                        return RedirectToAction("Edit", "SFCentral", new { id = records.First(e => e.SurveyID == tblswatsfpoint.SurveyID).ID, SurveyID = tblswatsfpoint.SurveyID });


                    }
                    else if (tblswatsfpoint.sanType == 1002)
                    {
                        var records = db.tblswatsfseptics.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
                        if (!records.Any())
                        {
                            tblswatsfseptic newEntry = new tblswatsfseptic();
                            newEntry.SurveyID = tblswatsfpoint.SurveyID;
                            db.tblswatsfseptics.Add(newEntry);
                            db.SaveChanges();

                            int newId = newEntry.ID;
                            return RedirectToAction("Edit", "SFSeptic", new { id = newId, SurveyID = newEntry.SurveyID });
                        }

                        return RedirectToAction("Edit", "SFSeptic", new { id = records.First(e => e.SurveyID == tblswatsfpoint.SurveyID).ID, SurveyID = tblswatsfpoint.SurveyID });
                    }
                    else if (tblswatsfpoint.sanType == 1003)
                    {
                        var records = db.tblswatsflats.Where(e => e.SurveyID == tblswatsfpoint.SurveyID);
                        if (!records.Any())
                        {
                            tblswatsflat newEntry = new tblswatsflat();
                            newEntry.SurveyID = tblswatsfpoint.SurveyID;
                            db.tblswatsflats.Add(newEntry);
                            db.SaveChanges();

                            int newId = newEntry.ID;
                            return RedirectToAction("Edit", "SFLat", new { id = newId, SurveyID = newEntry.SurveyID });
                        }

                        return RedirectToAction("Edit", "SFLat", new { id = records.First(e => e.SurveyID == tblswatsfpoint.SurveyID).ID, SurveyID = tblswatsfpoint.SurveyID });
                    }

                    return RedirectToAction("WaterPoints", "Survey", new { id = tblswatsfpoint.SurveyID });
                }
            }
            ViewBag.sanType = new SelectList(db.lkpswatsantypelus, "id", "Description", tblswatsfpoint.sanType);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "sanTypeVAL").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "SanUSeSUMMARY").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfpoint.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfpoint);
        }

        //
        // GET: /SFPoint/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatsfpoint tblswatsfpoint = db.tblswatsfpoints.Find(id);
        //    if (tblswatsfpoint == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfpoint);
        //}

        //
        // POST: /SFPoint/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatsfpoint tblswatsfpoint = db.tblswatsfpoints.Find(id);
        //    db.tblswatsfpoints.Remove(tblswatsfpoint);
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