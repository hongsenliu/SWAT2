using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using SWAT.Models;

namespace SWAT.Controllers
{
    public class CCGenderController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /CCGender/
        //public ActionResult Index()
        //{
        //    var tblswatccgenders = db.tblswatccgenders.Include(t => t.lkpSWATgenderLU).Include(t => t.lkpSWATgenderLU1).Include(t => t.lkpSWATgenderLU2).Include(t => t.lkpSWATgenderLU3).Include(t => t.lkpSWATgenderLU4).Include(t => t.lkpSWATgenderLU5).Include(t => t.lkpSWATgWomenEngagedLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccgenders.ToList());
        //}

        // GET: /CCGender/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccgender tblswatccgender = db.tblswatccgenders.Find(id);
        //    if (tblswatccgender == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccgender);
        //}

        // GET: /CCGender/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.gRole1 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gRole2 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gRole3 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gRole4 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gRole5 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gRole6 = new SelectList(db.lkpswatgenderlus, "id", "Description");
            ViewBag.gWomenEngage = new SelectList(db.lkpswatgwomenengagedlus, "id", "Description");
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "gRoleWomenSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenEngageSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenRepSCORE").Description;
            return View();
        }

        private double getRoleScore(int genderID)
        {
            double roleScore = 0;
            int? genderOrder = db.lkpswatgenderlus.Find(genderID).intorder;
            roleScore = Double.Parse(db.lkpswatscores_gender.Single(e => e.intorder == genderOrder).Description);
            return roleScore;
        }

        private void updateScores(tblswatccgender tblswatccgender)
        {
            int?[] gRoles = { tblswatccgender.gRole1, tblswatccgender.gRole2, tblswatccgender.gRole3,
                             tblswatccgender.gRole4, tblswatccgender.gRole5, tblswatccgender.gRole6};
            double? sumRoleScore = null;
            foreach (int? item in gRoles)
            {
                if (item != null)
                {
                    sumRoleScore = sumRoleScore.GetValueOrDefault(0) + getRoleScore((int)item);
                }
            }
            if (sumRoleScore != null)
            {
                double gRoleWomen = (double)sumRoleScore / 6;
                tblswatccgender.gRoleWomen = gRoleWomen;
                int? gRoleWomenOrder = null;
                foreach (var item in db.lkpswatgrolewomenlus.OrderByDescending(e => e.Description))
                {
                    if (Double.Parse(item.Description) <= gRoleWomen)
                    {
                        gRoleWomenOrder = item.intorder;
                        break;
                    }
                }
                if (gRoleWomenOrder != null)
                {
                    double gRoleWomenScore = Double.Parse(db.lkpswatscores_grolewomen.Single(e => e.intorder == gRoleWomenOrder).Description);
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gRoleWomenSCORE").Value = gRoleWomenScore;
                }
                else
                {
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gRoleWomenSCORE").Value = null;
                }

            }
            else
            {
                tblswatccgender.gRoleWomen = null;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gRoleWomenSCORE").Value = null;
            }

            if (tblswatccgender.gWomenEngage != null)
            {
                int? gWomenEngageOrder = db.lkpswatgwomenengagedlus.Find(tblswatccgender.gWomenEngage).intorder;
                double gWomenEngageScore = Double.Parse(db.lkpswatscores_gwomenengaged.Single(e => e.intorder == gWomenEngageOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gWomenEngageSCORE").Value = gWomenEngageScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gWomenEngageSCORE").Value = null;
            }

            if (tblswatccgender.gWomenRep != null)
            {
                double gWomenRepScore = (double)tblswatccgender.gWomenRep / 100;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gWomenRepSCORE").Value = gWomenRepScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccgender.SurveyID && e.VarName == "gWomenRepSCORE").Value = null;
            }

            db.SaveChanges();
        }

        // POST: /CCGender/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,gRole1,gRole2,gRole3,gRole4,gRole5,gRole6,gRoleWomen,gWomenEngage,gWomenRep")] tblswatccgender tblswatccgender)
        {
            if (ModelState.IsValid)
            {
                var genderIDs = db.tblswatccgenders.Where(e => e.SurveyID == tblswatccgender.SurveyID).Select(e => e.ID);
                if (genderIDs.Any())
                {
                    int genderId = genderIDs.First();
                    tblswatccgender.ID = genderId;
                    db.Entry(tblswatccgender).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatccgender);

                    // return RedirectToAction("Index");
                    // return RedirectToAction("Report", "Survey", new { id = tblswatccgender.SurveyID });
                    return RedirectToAction("Create", "CCSocial", new { SurveyID = tblswatccgender.SurveyID });
                }

                db.tblswatccgenders.Add(tblswatccgender);
                db.SaveChanges();
                updateScores(tblswatccgender);

                // return RedirectToAction("Index");
                // return RedirectToAction("Report", "Survey", new { id = tblswatccgender.SurveyID });
                return RedirectToAction("Create", "CCSocial", new { SurveyID = tblswatccgender.SurveyID });
            }

            ViewBag.gRole1 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole1);
            ViewBag.gRole2 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole2);
            ViewBag.gRole3 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole3);
            ViewBag.gRole4 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole4);
            ViewBag.gRole5 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole5);
            ViewBag.gRole6 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole6);
            ViewBag.gWomenEngage = new SelectList(db.lkpswatgwomenengagedlus, "id", "Description", tblswatccgender.gWomenEngage);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "gRoleWomenSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenEngageSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenRepSCORE").Description;
            return View(tblswatccgender);
        }

        // GET: /CCGender/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccgender tblswatccgender = db.tblswatccgenders.Find(id);
            if (tblswatccgender == null)
            {
                return HttpNotFound();
            }
            ViewBag.gRole1 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole1);
            ViewBag.gRole2 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole2);
            ViewBag.gRole3 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole3);
            ViewBag.gRole4 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole4);
            ViewBag.gRole5 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole5);
            ViewBag.gRole6 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole6);
            ViewBag.gWomenEngage = new SelectList(db.lkpswatgwomenengagedlus, "id", "Description", tblswatccgender.gWomenEngage);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "gRoleWomenSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenEngageSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenRepSCORE").Description;
            return View(tblswatccgender);
        }

        // POST: /CCGender/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,gRole1,gRole2,gRole3,gRole4,gRole5,gRole6,gRoleWomen,gWomenEngage,gWomenRep")] tblswatccgender tblswatccgender)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccgender).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccgender);

                // If there is not any CCSocial with the current survey (SurveyID) then create one and redirect to its edit link.
                var socials = db.tblswatccsocials.Where(e => e.SurveyID == tblswatccgender.SurveyID);
                if (!socials.Any())
                {
                    tblswatccsocial tblswatccsocial = new tblswatccsocial();
                    tblswatccsocial.SurveyID = tblswatccgender.SurveyID;
                    db.tblswatccsocials.Add(tblswatccsocial);
                    db.SaveChanges();

                    int newSocialID = tblswatccsocial.ID;

                    return RedirectToAction("Edit", "CCSocial", new { id = newSocialID, SurveyID = tblswatccsocial.SurveyID });
                }

                return RedirectToAction("Edit", "CCSocial", new { id = socials.Single(e => e.SurveyID == tblswatccgender.SurveyID).ID, SurveyID = tblswatccgender.SurveyID });
                // return RedirectToAction("Index");
            }
            ViewBag.gRole1 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole1);
            ViewBag.gRole2 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole2);
            ViewBag.gRole3 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole3);
            ViewBag.gRole4 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole4);
            ViewBag.gRole5 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole5);
            ViewBag.gRole6 = new SelectList(db.lkpswatgenderlus, "id", "Description", tblswatccgender.gRole6);
            ViewBag.gWomenEngage = new SelectList(db.lkpswatgwomenengagedlus, "id", "Description", tblswatccgender.gWomenEngage);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "gRoleWomenSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenEngageSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "gWomenRepSCORE").Description;
            return View(tblswatccgender);
        }

        // GET: /CCGender/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatccgender tblswatccgender = db.tblswatccgenders.Find(id);
        //    if (tblswatccgender == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccgender);
        //}

        // POST: /CCGender/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccgender tblswatccgender = db.tblswatccgenders.Find(id);
        //    db.tblswatccgenders.Remove(tblswatccgender);
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
