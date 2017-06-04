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
    public class CCIndigController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /CCIndig/
        //public ActionResult Index()
        //{
        //    var tblswatccindigs = db.tblswatccindigs.Include(t => t.tblSWATSurvey);
        //    return View(tblswatccindigs.ToList());
        //}

        // GET: /CCIndig/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccindig tblswatccindig = db.tblswatccindigs.Find(id);
        //    if (tblswatccindig == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccindig);
        //}

        // GET: /CCIndig/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "indigPopSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "longtermPopSCORE").Description;
            return View();
        }

        private void updateScores(tblswatccindig tblswatccindig)
        {
            long pop = (long)db.tblswatbackgroundinfoes.Single(e => e.SurveyID == tblswatccindig.SurveyID).Population;
            if (tblswatccindig.indigPop != null)
            {
                double indigPopPerCapita = ((double)tblswatccindig.indigPop / pop) * 1000;
                tblswatccindig.indigPopPerCapita = indigPopPerCapita;

                int? indigOrder = null;
                foreach (var item in db.lkpswatindigpoplus.OrderByDescending(e => e.Description))
                {
                    if (Double.Parse(item.Description) <= indigPopPerCapita)
                    {
                        indigOrder = item.intorder;
                        break;
                    }
                }
                if (indigOrder != null)
                {
                    double indigPopScore = Double.Parse(db.lkpswatscores_indigpop.Single(e => e.intorder == indigOrder).Description);
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "indigPopSCORE").Value = indigPopScore;
                }
                else
                {
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "indigPopSCORE").Value = null;
                }

            }
            else
            {
                tblswatccindig.indigPopPerCapita = null;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "indigPopSCORE").Value = null;
            }

            if (tblswatccindig.longtermPop != null)
            {
                double longtermPopPerCapita = ((double)tblswatccindig.longtermPop / pop) * 1000;
                tblswatccindig.longtermPopPerCapita = longtermPopPerCapita;

                int? longtermOrder = null;
                foreach (var item in db.lkpswatlongtermpoplus.OrderByDescending(e => e.Description))
                {
                    if (Double.Parse(item.Description) <= longtermPopPerCapita)
                    {
                        longtermOrder = item.intorder;
                        break;
                    }
                }
                if (longtermOrder != null)
                {
                    double longtermScore = Double.Parse(db.lkpswatscores_longtermpop.Single(e => e.intorder == longtermOrder).Description);
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "longtermPopSCORE").Value = longtermScore;
                }
                else
                {
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "longtermPopSCORE").Value = null;
                }

            }
            else
            {
                tblswatccindig.longtermPopPerCapita = null;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccindig.SurveyID && e.VarName == "longtermPopSCORE").Value = null;
            }

            db.SaveChanges();
        }

        private void checkPopulation(tblswatccindig tblswatccindig)
        {
            int comPop = (int)db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatccindig.SurveyID).Population;

            if (tblswatccindig.indigPop > comPop)
            {
                ModelState.AddModelError("indigPop", "The popluation of community is " + comPop);
            }
            if (tblswatccindig.longtermPop > comPop)
            {
                ModelState.AddModelError("longtermPop", "The popluation of community is " + comPop);
            }
        }

        // POST: /CCIndig/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,indigPop,longtermPop")] tblswatccindig tblswatccindig, string submitBtn)
        {
            checkPopulation(tblswatccindig);
            if (ModelState.IsValid)
            {
                var indigIDs = db.tblswatccindigs.Where(e => e.SurveyID == tblswatccindig.SurveyID).Select(e => e.ID);

                if (indigIDs.Any())
                {
                    int indigId = indigIDs.First();
                    tblswatccindig.ID = indigId;
                    db.Entry(tblswatccindig).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatccindigs.Add(tblswatccindig);
                }
                db.SaveChanges();
                updateScores(tblswatccindig);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "CCFinancial", new { SurveyID = tblswatccindig.SurveyID });
                }
            }

            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "indigPopSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "longtermPopSCORE").Description;
            return View(tblswatccindig);
        }

        // GET: /CCIndig/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccindig tblswatccindig = db.tblswatccindigs.Find(id);
            if (tblswatccindig == null)
            {
                return HttpNotFound();
            }
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "indigPopSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "longtermPopSCORE").Description;
            return View(tblswatccindig);
        }

        // POST: /CCIndig/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,indigPop,indigPopPerCapita,longtermPop,longtermPopPerCapita")] tblswatccindig tblswatccindig, string submitBtn)
        {
            checkPopulation(tblswatccindig);
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccindig).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccindig);

                if (submitBtn.Equals("Next"))
                {
                    // If there is not any CCFinancial with the current survey (SurveyID) then create one and redirect to its edit link.
                    var financials = db.tblswatccfinancials.Where(e => e.SurveyID == tblswatccindig.SurveyID);
                    if (!financials.Any())
                    {
                        tblswatccfinancial tblswatccfinancial = new tblswatccfinancial();
                        tblswatccfinancial.SurveyID = tblswatccindig.SurveyID;
                        db.tblswatccfinancials.Add(tblswatccfinancial);
                        db.SaveChanges();
                        int newFinancialId = tblswatccfinancial.ID;

                        return RedirectToAction("Edit", "CCFinancial", new { id = newFinancialId, SurveyID = tblswatccfinancial.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "CCFinancial", new { id = financials.Single(e => e.SurveyID == tblswatccindig.SurveyID).ID, SurveyID = tblswatccindig.SurveyID });
                    }
                }
            }
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "indigPopSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "longtermPopSCORE").Description;
            return View(tblswatccindig);
        }

        // GET: /CCIndig/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccindig tblswatccindig = db.tblswatccindigs.Find(id);
        //    if (tblswatccindig == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccindig);
        //}

        // POST: /CCIndig/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccindig tblswatccindig = db.tblswatccindigs.Find(id);
        //    db.tblswatccindigs.Remove(tblswatccindig);
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
