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
    public class SFSanitationController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SFSanitation/

        //public ActionResult Index()
        //{
        //    var tblswatsfsanitations = db.tblswatsfsanitations.Include(t => t.lkpSWATtoiletsAllLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatsfsanitations.ToList());
        //}

        //
        // GET: /SFSanitation/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatsfsanitation tblswatsfsanitation = db.tblswatsfsanitations.Find(id);
        //    if (tblswatsfsanitation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfsanitation);
        //}

        //
        // GET: /SFSanitation/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.toiletsAll = new SelectList(db.lkpswattoiletsalllus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletsAllSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletDistancesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View();
        }

        private void checkSumHHsan(tblswatsfsanitation tblswatsfsanitation)
        {
            double?[] hhSan = { tblswatsfsanitation.toiletHome, tblswatsfsanitation.toiletYard, tblswatsfsanitation.toilet100,
                                tblswatsfsanitation.toilet500, tblswatsfsanitation.toiletFar};
            double? total = null;
            foreach (double? item in hhSan)
            {
                total = total.GetValueOrDefault(0) + item.GetValueOrDefault(0);
            }
            if (total > 100)
            {
                ModelState.AddModelError("toiletHome", "The total percentage of households cannot exceed 100.");
                ModelState.AddModelError("toiletYard", "The total percentage of households cannot exceed 100.");
                ModelState.AddModelError("toilet100", "The total percentage of households cannot exceed 100.");
                ModelState.AddModelError("toilet500", "The total percentage of households cannot exceed 100.");
                ModelState.AddModelError("toiletFar", "The total percentage of households cannot exceed 100.");
            }
        }

        private void updateScores(tblswatsfsanitation tblswatsfsanitation)
        {
            if (tblswatsfsanitation.toiletsAll != null)
            {
                int intorder = (int)db.lkpswattoiletsalllus.Find(tblswatsfsanitation.toiletsAll).intorder;
                if (intorder != 1)
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletsAllSCORE").Value = null;
                }
                else
                {
                    double score = Convert.ToDouble(db.lkpswatscores_toiletsall.First(e => e.intorder == intorder).Description);
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletsAllSCORE").Value = score;
                }
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletsAllSCORE").Value = null;
            }

            double? total = null;
            if (tblswatsfsanitation.toiletHome != null)
            {
                double score = (double)tblswatsfsanitation.toiletHome / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletHomeSCORE").Value = score;
                total = total.GetValueOrDefault(0) + score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletHomeSCORE").Value = null;
            }

            if (tblswatsfsanitation.toiletYard != null)
            {
                double score = (double)tblswatsfsanitation.toiletYard / 100;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletYardSCORE").Value = score;
                total = total.GetValueOrDefault(0) + score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletYardSCORE").Value = null;
            }

            if (tblswatsfsanitation.toilet100 != null)
            {
                double score = (double)tblswatsfsanitation.toilet100 / 100 * 0.75;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toilet100SCORE").Value = score;
                total = total.GetValueOrDefault(0) + score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toilet100SCORE").Value = null;
            }

            if (tblswatsfsanitation.toilet500 != null)
            {
                double score = (double)tblswatsfsanitation.toilet500 / 100 * 0.5;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toilet500SCORE").Value = score;
                total = total.GetValueOrDefault(0) + score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toilet500SCORE").Value = null;
            }

            if (tblswatsfsanitation.toiletFar != null)
            {
                double score = (double)tblswatsfsanitation.toiletFar / 100 * 0.25;
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletFarSCORE").Value = score;
                total = total.GetValueOrDefault(0) + score;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletFarSCORE").Value = null;
            }

            db.tblswatscores.First(e => e.SurveyID == tblswatsfsanitation.SurveyID && e.VarName == "toiletDistancesSCORE").Value = total;

            db.SaveChanges();
        }

        private void deleteSFod(tblswatsfsanitation tblswatsfsanitation)
        {
            var sfOds = db.tblswatsfods.Where(e => e.SurveyID == tblswatsfsanitation.SurveyID).ToList();

            foreach (tblswatsfod item in sfOds)
            {
                db.tblswatsfods.Remove(item);
            }

            db.SaveChanges();
        }

        //
        // POST: /SFSanitation/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatsfsanitation tblswatsfsanitation, string submitBtn)
        {
            checkSumHHsan(tblswatsfsanitation);
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatsfsanitations.Where(e => e.SurveyID == tblswatsfsanitation.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswatsfsanitation.ID = recordId;
                    db.Entry(tblswatsfsanitation).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatsfsanitations.Add(tblswatsfsanitation);
                }
                db.SaveChanges();
                updateScores(tblswatsfsanitation);

                if (submitBtn.Equals("Next"))
                {
                    if (tblswatsfsanitation.toiletsAll == 981) // 981 means all homes have toilet
                    {
                        return RedirectToAction("Create", "SFPoint", new { SurveyID = tblswatsfsanitation.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Create", "SFOd", new { SurveyID = tblswatsfsanitation.SurveyID });
                    }
                }
            }

            ViewBag.toiletsAll = new SelectList(db.lkpswattoiletsalllus, "id", "Description", tblswatsfsanitation.toiletsAll);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletsAllSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletDistancesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfsanitation.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfsanitation);
        }

        //
        // GET: /SFSanitation/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsfsanitation tblswatsfsanitation = db.tblswatsfsanitations.Find(id);
            if (tblswatsfsanitation == null)
            {
                return HttpNotFound();
            }
            ViewBag.toiletsAll = new SelectList(db.lkpswattoiletsalllus, "id", "Description", tblswatsfsanitation.toiletsAll);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletsAllSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletDistancesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfsanitation);
        }

        //
        // POST: /SFSanitation/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatsfsanitation tblswatsfsanitation, string submitBtn)
        {
            checkSumHHsan(tblswatsfsanitation);
            if (ModelState.IsValid)
            {
                db.Entry(tblswatsfsanitation).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatsfsanitation);


                if (submitBtn.Equals("Next"))
                {
                    if (tblswatsfsanitation.toiletsAll == 981) // 981 means all homes have toilet
                    {
                        deleteSFod(tblswatsfsanitation);
                        var records = db.tblswatsfpoints.Where(e => e.SurveyID == tblswatsfsanitation.SurveyID);
                        if (!records.Any())
                        {
                            tblswatsfpoint newEntry = new tblswatsfpoint();
                            newEntry.SurveyID = tblswatsfsanitation.SurveyID;
                            db.tblswatsfpoints.Add(newEntry);
                            db.SaveChanges();

                            int newId = newEntry.ID;
                            return RedirectToAction("Edit", "SFPoint", new { id = newId, SurveyID = newEntry.SurveyID });
                        }

                        return RedirectToAction("Edit", "SFPoint", new { id = records.First(e => e.SurveyID == tblswatsfsanitation.SurveyID).ID, SurveyID = tblswatsfsanitation.SurveyID });
                    }
                    else
                    {
                        var records = db.tblswatsfods.Where(e => e.SurveyID == tblswatsfsanitation.SurveyID);
                        if (!records.Any())
                        {
                            tblswatsfod newEntry = new tblswatsfod();
                            newEntry.SurveyID = tblswatsfsanitation.SurveyID;
                            db.tblswatsfods.Add(newEntry);
                            db.SaveChanges();

                            int newId = newEntry.ID;
                            return RedirectToAction("Edit", "SFOd", new { id = newId, SurveyID = newEntry.SurveyID });
                        }

                        return RedirectToAction("Edit", "SFOd", new { id = records.First(e => e.SurveyID == tblswatsfsanitation.SurveyID).ID, SurveyID = tblswatsfsanitation.SurveyID });
                    }
                }
                
            }
            ViewBag.toiletsAll = new SelectList(db.lkpswattoiletsalllus, "id", "Description", tblswatsfsanitation.toiletsAll);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletsAllSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "toiletDistancesSCORE").Description;
            tblswatbackgroundinfo bg = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfsanitation.SurveyID);
            int? ls = bg.isEconLs;
            int? ag = bg.isEconAg;
            int? dev = bg.isEconDev;
            Boolean hasSection4 = true;
            ViewBag.hasSection4 = hasSection4 && ((ls == 1511) || (ag == 1511) || (dev == 1511)) ? "true" : "false";
            ViewBag.currentSectionID = 6;
            return View(tblswatsfsanitation);
        }

        //
        // GET: /SFSanitation/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatsfsanitation tblswatsfsanitation = db.tblswatsfsanitations.Find(id);
        //    if (tblswatsfsanitation == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfsanitation);
        //}

        //
        // POST: /SFSanitation/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatsfsanitation tblswatsfsanitation = db.tblswatsfsanitations.Find(id);
        //    db.tblswatsfsanitations.Remove(tblswatsfsanitation);
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