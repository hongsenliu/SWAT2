﻿using System;
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
    public class SFOdController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /SFOd/

        //public ActionResult Index()
        //{
        //    var tblswatsfods = db.tblswatsfods.Include(t => t.tblSWATSurvey);
        //    return View(tblswatsfods.ToList());
        //}

        //
        // GET: /SFOd/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatsfod tblswatsfod = db.tblswatsfods.Find(id);
        //    if (tblswatsfod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfod);
        //}

        //
        // GET: /SFOd/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemographicSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "hhNoToiletSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "ODPercentSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemoGenderSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "ODfacilitatorSCORE").Description;

            return View();
        }

        private void updateScores(tblswatsfod tblswatsfod)
        {
            bool[] graphics = { tblswatsfod.ODdemographic1, tblswatsfod.ODdemographic2, tblswatsfod.ODdemographic3, tblswatsfod.ODdemographic4};
            int checkedCounter = 0;
            foreach (bool item in graphics)
            {
                if (item)
                {
                    checkedCounter++;
                }
            }
            double score = 1 - (double)checkedCounter / 4;
            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemographicSCORE").Value = score;
            if (tblswatsfod.ODdemographicNONE)
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODnoneSCORE").Value = 1;
            }
            else
            {
                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODnoneSCORE").Value = 0;
                if (checkedCounter > 0)
                {
                    if (tblswatsfod.hhNoToilet != null)
                    {
                        int hh = (int)db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfod.SurveyID).numHouseholds;
                        double surveyscore = (double)tblswatsfod.hhNoToilet / hh;
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "hhNoToiletSCORE").Value = surveyscore;
                        if (surveyscore > 0)
                        {
                            if (tblswatsfod.ODPercent != null)
                            {
                                // TODO check --> double ODPercentscore = 1 - (double)tblswatsfod.ODPercent / 100;
                                double ODPercentscore = (double)tblswatsfod.ODPercent / 100;
                                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODPercentSCORE").Value = ODPercentscore;
                                if (ODPercentscore > 0)
                                {
                                    bool[] genders = { tblswatsfod.ODdemoGender1, tblswatsfod.ODdemoGender2, tblswatsfod.ODdemoGender3, tblswatsfod.ODdemoGender4 };
                                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = 1;
                                    foreach (bool item in genders)
                                    {
                                        if (item)
                                        {
                                            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = 0;
                                            break;
                                        }
                                    }

                                    if (tblswatsfod.ODdemoGenderNONE)
                                    {
                                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = 1;
                                    }
                                    else
                                    {
                                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = 0;
                                    }

                                    bool[] odFac = {tblswatsfod.ODfacilitator1, tblswatsfod.ODfacilitator2, tblswatsfod.ODfacilitator3,
                                                    tblswatsfod.ODfacilitator4, tblswatsfod.ODfacilitator5, tblswatsfod.ODfacilitator6, 
                                                    tblswatsfod.ODfacilitator7, tblswatsfod.ODfacilitator8, tblswatsfod.ODfacilitator9, 
                                                    tblswatsfod.ODfacilitator10, tblswatsfod.ODfacilitator11};
                                    int odFacCounter = 0;
                                    foreach (bool item in odFac)
                                    {
                                        if (item)
                                        {
                                            odFacCounter++;
                                        }
                                    }

                                    if (odFacCounter >= 5)
                                    {
                                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = 1;
                                    }
                                    else
                                    {
                                        double odFacScore = 0;
                                        odFacScore = (double)odFacCounter / 5;
                                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = odFacScore;
                                    }
                                }
                                else
                                {
                                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = null;
                                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = null;
                                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = null;
                                }
                            }
                            else
                            {
                                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODPercentSCORE").Value = null;
                                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = null;
                                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = null;
                                db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = null;
                            }

                            
                        }
                        else
                        {
                            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODPercentSCORE").Value = null;
                            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = null;
                            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = null;
                            db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = null;
                        }
                    }
                    else
                    {
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "hhNoToiletSCORE").Value = null;
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODPercentSCORE").Value = null;
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = null;
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = null;
                        db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = null;
                    }

                    
                }
                else
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "hhNoToiletSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODPercentSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODdemoGenderNONESCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatsfod.SurveyID && e.VarName == "ODfacilitatorSCORE").Value = null;
                }
            }

            db.SaveChanges();

        }

        private void checkHouseholds(tblswatsfod tblswatsfod)
        {
            int comhh = (int)db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatsfod.SurveyID).numHouseholds;

            if (tblswatsfod.hhNoToilet > comhh)
            {
                ModelState.AddModelError("hhNoToilet", "The community has " + comhh + " households.");
            }
        }

        //
        // POST: /SFOd/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatsfod tblswatsfod, string submitBtn)
        {
            checkHouseholds(tblswatsfod);
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatsfods.Where(e => e.SurveyID == tblswatsfod.SurveyID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    int recordId = recordIDs.First();
                    tblswatsfod.ID = recordId;
                    db.Entry(tblswatsfod).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatsfods.Add(tblswatsfod);
                }
                db.SaveChanges();
                updateScores(tblswatsfod);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "SFPoint", new { SurveyID = tblswatsfod.SurveyID });
                }
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemographicSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "hhNoToiletSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "ODPercentSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemoGenderSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "ODfacilitatorSCORE").Description;

            return View(tblswatsfod);
        }

        //
        // GET: /SFOd/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatsfod tblswatsfod = db.tblswatsfods.Find(id);
            if (tblswatsfod == null)
            {
                return HttpNotFound();
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemographicSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "hhNoToiletSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "ODPercentSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemoGenderSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "ODfacilitatorSCORE").Description;

            return View(tblswatsfod);
        }

        //
        // POST: /SFOd/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatsfod tblswatsfod, string submitBtn)
        {
            checkHouseholds(tblswatsfod);
            if (ModelState.IsValid)
            {
                db.Entry(tblswatsfod).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatsfod);

                if (submitBtn.Equals("Next"))
                {
                    var records = db.tblswatsfpoints.Where(e => e.SurveyID == tblswatsfod.SurveyID);
                    if (!records.Any())
                    {
                        tblswatsfpoint newEntry = new tblswatsfpoint();
                        newEntry.SurveyID = tblswatsfod.SurveyID;
                        db.tblswatsfpoints.Add(newEntry);
                        db.SaveChanges();

                        int newId = newEntry.ID;
                        return RedirectToAction("Edit", "SFPoint", new { id = newId, SurveyID = newEntry.SurveyID });
                    }

                    return RedirectToAction("Edit", "SFPoint", new { id = records.First(e => e.SurveyID == tblswatsfod.SurveyID).ID, SurveyID = tblswatsfod.SurveyID });
                }
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemographicSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "hhNoToiletSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "ODPercentSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "ODdemoGenderSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "ODfacilitatorSCORE").Description;

            return View(tblswatsfod);
        }

        //
        // GET: /SFOd/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatsfod tblswatsfod = db.tblswatsfods.Find(id);
        //    if (tblswatsfod == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatsfod);
        //}

        //
        // POST: /SFOd/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatsfod tblswatsfod = db.tblswatsfods.Find(id);
        //    db.tblswatsfods.Remove(tblswatsfod);
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