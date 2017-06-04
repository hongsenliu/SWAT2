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
    public class CCExternalController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /CCExternal/

        //public ActionResult Index()
        //{
        //    var tblswatccexternalsupports = db.tblswatccexternalsupports.Include(t => t.lkpSWATextVisitLU).Include(t => t.lkpSWATextVisitLU1).Include(t => t.lkpSWATfundAppLU).Include(t => t.lkpSWATfundAppSuccessLU).Include(t => t.lkpSWATgovRightsLU).Include(t => t.lkpSWATgovWatAnalLU).Include(t => t.lkpSWATgovWatPolLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccexternalsupports.ToList());
        //}

        //
        // GET: /CCExternal/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblswatccexternalsupport tblswatccexternalsupport = db.tblswatccexternalsupports.Find(id);
        //    if (tblswatccexternalsupport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccexternalsupport);
        //}

        //
        // GET: /CCExternal/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.extVisitTech = new SelectList(db.lkpswatextvisitlus, "id", "Description");
            ViewBag.extVisitAdmin = new SelectList(db.lkpswatextvisitlus, "id", "Description");
            ViewBag.fundApp = new SelectList(db.lkpswatfundapplus, "id", "Description");
            ViewBag.fundAppSuccess = new SelectList(db.lkpswatfundappsuccesslus, "id", "Description");
            ViewBag.govRights = new SelectList(db.lkpswatgovrightslus, "id", "Description");
            ViewBag.govWatAnal = new SelectList(db.lkpswatgovwatanallus, "id", "Description");
            ViewBag.govWatPol = new SelectList(db.lkpswatgovwatpollus, "ID", "Description");
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundApp_FUNDSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundAppSuccessSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "govRightsSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatPolSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatAnalSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.Single(e => e.VarName == "extVisitTechSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.Single(e => e.VarName == "expertAccessSCORE").Description;
            return View();
        }

        private void updateScores(tblswatccexternalsupport tblswatccexternalsupport)
        {
            if (tblswatccexternalsupport.fundApp != null)
            {
                int fundAppOrder = (int)db.lkpswatfundapplus.Find(tblswatccexternalsupport.fundApp).intorder;
                double fundAppScore = Double.Parse(db.lkpswatscores_fundappfund.Single(e => e.intorder == fundAppOrder).Description);
                double fundAppLinkScore = Double.Parse(db.lkpswatscores_fundapplink.Single(e => e.intorder == fundAppOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundApp_FUNDSCORE").Value = fundAppScore;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundApp_LINKSCORE").Value = fundAppLinkScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundApp_FUNDSCORE").Value = null;
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundApp_LINKSCORE").Value = null;
            }

            if (tblswatccexternalsupport.fundAppSuccess != null)
            {
                int fundAppSuccessOrder = (int)db.lkpswatfundappsuccesslus.Find(tblswatccexternalsupport.fundAppSuccess).intorder;
                double fundAppSuccessScore = Double.Parse(db.lkpswatscores_fundappsuccess.Single(e => e.intorder == fundAppSuccessOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundAppSuccessSCORE").Value = fundAppSuccessScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "fundAppSuccessSCORE").Value = null;
            }

            if (tblswatccexternalsupport.govRights != null)
            {
                int govRightOrder = (int)db.lkpswatgovrightslus.Find(tblswatccexternalsupport.govRights).intorder;
                if (govRightOrder == 3)
                {
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govRightsSCORE").Value = null;
                }
                else
                {
                    double govRightScore = Double.Parse(db.lkpswatscores_govrights.Single(e => e.intorder == govRightOrder).Description);
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govRightsSCORE").Value = govRightScore;
                }
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govRightsSCORE").Value = null;
            }

            if (tblswatccexternalsupport.govWatPol != null)
            {
                int govWatPolOrder = (int)db.lkpswatgovwatpollus.Find(tblswatccexternalsupport.govWatPol).intorder;
                double govWatPolScore = Double.Parse(db.lkpswatscores_govwatpol.Single(e => e.intorder == govWatPolOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govWatPolSCORE").Value = govWatPolScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govWatPolSCORE").Value = null;
            }

            if (tblswatccexternalsupport.govWatAnal != null)
            {
                int govWatAnalOrder = (int)db.lkpswatgovwatanallus.Find(tblswatccexternalsupport.govWatAnal).intorder;
                if (govWatAnalOrder == 4)
                {
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govWatAnalSCORE").Value = null;
                }
                else
                {
                    double govWatAnalScore = Double.Parse(db.lkpswatscores_govwatanal.Single(e => e.intorder == govWatAnalOrder).Description);
                    db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govWatAnalSCORE").Value = govWatAnalScore;
                }
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "govWatAnalSCORE").Value = null;
            }

            if (tblswatccexternalsupport.extVisitTech != null)
            {
                int extVisitTechOrder = (int)db.lkpswatextvisitlus.Find(tblswatccexternalsupport.extVisitTech).intorder;
                double extVTScore = Double.Parse(db.lkpswatscores_extvisit.Single(e => e.intorder == extVisitTechOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "extVisitTechSCORE").Value = extVTScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "extVisitTechSCORE").Value = null;
            }

            if (tblswatccexternalsupport.extVisitAdmin != null)
            {
                int extVAOrder = (int)db.lkpswatextvisitlus.Find(tblswatccexternalsupport.extVisitAdmin).intorder;
                double extVAScore = Double.Parse(db.lkpswatscores_extvisit.Single(e => e.intorder == extVAOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "extVisitAdminSCORE").Value = extVAScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "extVisitAdminSCORE").Value = null;
            }

            bool[] expertAccess = {tblswatccexternalsupport.expertAccess1, tblswatccexternalsupport.expertAccess2, tblswatccexternalsupport.expertAccess3,
                                     tblswatccexternalsupport.expertAccess4, tblswatccexternalsupport.expertAccess5};
            double checkedCounter = 0;
            foreach (bool item in expertAccess)
            {
                if (item)
                {
                    checkedCounter++;
                }
            }
            double expertAccessScore = checkedCounter / 5;
            db.tblswatscores.Single(e => e.SurveyID == tblswatccexternalsupport.SurveyID && e.VarName == "expertAccessSCORE").Value = expertAccessScore;
            db.SaveChanges();
        }
       
        //
        // POST: /CCExternal/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatccexternalsupport tblswatccexternalsupport)
        {
            if (ModelState.IsValid)
            {
                var externalIDs = db.tblswatccexternalsupports.Where(e => e.SurveyID == tblswatccexternalsupport.SurveyID).Select(e => e.ID);
                if (externalIDs.Any())
                {
                    int externalID = externalIDs.First();
                    tblswatccexternalsupport.ID = externalID;
                    db.Entry(tblswatccexternalsupport).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatccexternalsupport);

                    return RedirectToAction("Create", "CCWaterManagement", new { SurveyID = tblswatccexternalsupport.SurveyID});
                }

                db.tblswatccexternalsupports.Add(tblswatccexternalsupport);
                db.SaveChanges();
                updateScores(tblswatccexternalsupport);

                return RedirectToAction("Create", "CCWaterManagement", new { SurveyID = tblswatccexternalsupport.SurveyID});
            }

            ViewBag.extVisitTech = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitTech);
            ViewBag.extVisitAdmin = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitAdmin);
            ViewBag.fundApp = new SelectList(db.lkpswatfundapplus, "id", "Description", tblswatccexternalsupport.fundApp);
            ViewBag.fundAppSuccess = new SelectList(db.lkpswatfundappsuccesslus, "id", "Description", tblswatccexternalsupport.fundAppSuccess);
            ViewBag.govRights = new SelectList(db.lkpswatgovrightslus, "id", "Description", tblswatccexternalsupport.govRights);
            ViewBag.govWatAnal = new SelectList(db.lkpswatgovwatanallus, "id", "Description", tblswatccexternalsupport.govWatAnal);
            ViewBag.govWatPol = new SelectList(db.lkpswatgovwatpollus, "ID", "Description", tblswatccexternalsupport.govWatPol);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundApp_FUNDSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundAppSuccessSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "govRightsSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatPolSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatAnalSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.Single(e => e.VarName == "extVisitTechSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.Single(e => e.VarName == "expertAccessSCORE").Description;
            return View(tblswatccexternalsupport);
        }

        //
        // GET: /CCExternal/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccexternalsupport tblswatccexternalsupport = db.tblswatccexternalsupports.Find(id);
            if (tblswatccexternalsupport == null)
            {
                return HttpNotFound();
            }
            ViewBag.extVisitTech = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitTech);
            ViewBag.extVisitAdmin = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitAdmin);
            ViewBag.fundApp = new SelectList(db.lkpswatfundapplus, "id", "Description", tblswatccexternalsupport.fundApp);
            ViewBag.fundAppSuccess = new SelectList(db.lkpswatfundappsuccesslus, "id", "Description", tblswatccexternalsupport.fundAppSuccess);
            ViewBag.govRights = new SelectList(db.lkpswatgovrightslus, "id", "Description", tblswatccexternalsupport.govRights);
            ViewBag.govWatAnal = new SelectList(db.lkpswatgovwatanallus, "id", "Description", tblswatccexternalsupport.govWatAnal);
            ViewBag.govWatPol = new SelectList(db.lkpswatgovwatpollus, "ID", "Description", tblswatccexternalsupport.govWatPol);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundApp_FUNDSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundAppSuccessSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "govRightsSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatPolSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatAnalSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.Single(e => e.VarName == "extVisitTechSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.Single(e => e.VarName == "expertAccessSCORE").Description;
            return View(tblswatccexternalsupport);
        }

        //
        // POST: /CCExternal/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatccexternalsupport tblswatccexternalsupport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccexternalsupport).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccexternalsupport);

                var ccwatermanagement = db.tblswatccwatermanagements.Where(e => e.SurveyID == tblswatccexternalsupport.SurveyID);
                if (!ccwatermanagement.Any())
                {
                    tblswatccwatermanagement tblswatccwatermanagement = new tblswatccwatermanagement();
                    tblswatccwatermanagement.SurveyID = tblswatccexternalsupport.SurveyID;
                    db.tblswatccwatermanagements.Add(tblswatccwatermanagement);
                    db.SaveChanges();

                    int newExternalID = tblswatccwatermanagement.ID;

                    return RedirectToAction("Edit", "CCWaterManagement", new { id = newExternalID, SurveyID = tblswatccwatermanagement.SurveyID });
                }

                return RedirectToAction("Edit", "CCWaterManagement", new { id = ccwatermanagement.First(e => e.SurveyID == tblswatccexternalsupport.SurveyID).ID, SurveyID = tblswatccexternalsupport.SurveyID });
            }
            ViewBag.extVisitTech = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitTech);
            ViewBag.extVisitAdmin = new SelectList(db.lkpswatextvisitlus, "id", "Description", tblswatccexternalsupport.extVisitAdmin);
            ViewBag.fundApp = new SelectList(db.lkpswatfundapplus, "id", "Description", tblswatccexternalsupport.fundApp);
            ViewBag.fundAppSuccess = new SelectList(db.lkpswatfundappsuccesslus, "id", "Description", tblswatccexternalsupport.fundAppSuccess);
            ViewBag.govRights = new SelectList(db.lkpswatgovrightslus, "id", "Description", tblswatccexternalsupport.govRights);
            ViewBag.govWatAnal = new SelectList(db.lkpswatgovwatanallus, "id", "Description", tblswatccexternalsupport.govWatAnal);
            ViewBag.govWatPol = new SelectList(db.lkpswatgovwatpollus, "ID", "Description", tblswatccexternalsupport.govWatPol);
            ViewBag.Question1 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundApp_FUNDSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.Single(e => e.VarName == "fundAppSuccessSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.Single(e => e.VarName == "govRightsSCORE").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatPolSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.Single(e => e.VarName == "govWatAnalSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.Single(e => e.VarName == "extVisitTechSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.Single(e => e.VarName == "expertAccessSCORE").Description;
            return View(tblswatccexternalsupport);
        }

        //
        // GET: /CCExternal/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblswatccexternalsupport tblswatccexternalsupport = db.tblswatccexternalsupports.Find(id);
        //    if (tblswatccexternalsupport == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccexternalsupport);
        //}

        //
        // POST: /CCExternal/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatccexternalsupport tblswatccexternalsupport = db.tblswatccexternalsupports.Find(id);
        //    db.tblswatccexternalsupports.Remove(tblswatccexternalsupport);
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