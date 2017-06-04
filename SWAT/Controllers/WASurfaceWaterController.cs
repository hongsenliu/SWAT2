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
    public class WASurfaceWaterController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /WASurfaceWater/
        //public ActionResult Index()
        //{
        //    var tblswatwasurfacewaters = db.tblswatwasurfacewaters.Include(t => t.lkpSWATrunoffLU).Include(t => t.lkpSWATsurfaceVarLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatwasurfacewaters.ToList());
        //}

        // GET: /WASurfaceWater/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwasurfacewater tblswatwasurfacewater = db.tblswatwasurfacewaters.Find(id);
        //    if (tblswatwasurfacewater == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwasurfacewater);
        //}

        // GET: /WASurfaceWater/Create
        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.runoff = new SelectList(db.lkpswatrunofflus, "ID", "Description");
            ViewBag.surfaceVar = new SelectList(db.lkpswatsurfacevarlus, "id", "Description");
            return View();
        }

        private void updateScores(tblswatwasurfacewater tblswatwasurfacewater)
        {
            if (tblswatwasurfacewater.runoff != null)
            {
                int? runoffOrder = db.lkpswatrunofflus.Find(tblswatwasurfacewater.runoff).intorder;
                double? runoffScore = db.lkpswatscores_runoff.Single(e => e.intorder == runoffOrder).Description;
                db.tblswatscores.Single(e => e.SurveyID == tblswatwasurfacewater.SurveyID && e.VarName == "swAvailabilitySCORE").Value = runoffScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatwasurfacewater.SurveyID && e.VarName == "swAvailabilitySCORE").Value = null;
            }

            if (tblswatwasurfacewater.surfaceVar != null)
            {
                int? surfaceVarOrder = db.lkpswatsurfacevarlus.Find(tblswatwasurfacewater.surfaceVar).intorder;
                double? surfaceVarScore = Double.Parse(db.lkpswatscores_surfacevar.Single(e => e.intorder == surfaceVarOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatwasurfacewater.SurveyID && e.VarName == "surfaceVarSCORE").Value = surfaceVarScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatwasurfacewater.SurveyID && e.VarName == "surfaceVarSCORE").Value = null;
            }

            db.SaveChanges();
        }

        // POST: /WASurfaceWater/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,runoff,surfaceVar")] tblswatwasurfacewater tblswatwasurfacewater, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var surfaceWaterIDs = db.tblswatwasurfacewaters.Where(e => e.SurveyID == tblswatwasurfacewater.SurveyID).Select(e => e.ID);
                if (surfaceWaterIDs.Any())
                {
                    int surfaceWaterId = surfaceWaterIDs.First();
                    tblswatwasurfacewater.ID = surfaceWaterId;
                    db.Entry(tblswatwasurfacewater).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatwasurfacewaters.Add(tblswatwasurfacewater);
                }
                db.SaveChanges();
                updateScores(tblswatwasurfacewater);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WAGroundWater", new { SurveyID = tblswatwasurfacewater.SurveyID });
                }
            }

            ViewBag.runoff = new SelectList(db.lkpswatrunofflus, "ID", "Description", tblswatwasurfacewater.runoff);
            ViewBag.surfaceVar = new SelectList(db.lkpswatsurfacevarlus, "id", "Description", tblswatwasurfacewater.surfaceVar);
            return View(tblswatwasurfacewater);
        }

        // GET: /WASurfaceWater/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatwasurfacewater tblswatwasurfacewater = db.tblswatwasurfacewaters.Find(id);
            if (tblswatwasurfacewater == null)
            {
                return HttpNotFound();
            }
            ViewBag.runoff = new SelectList(db.lkpswatrunofflus, "ID", "Description", tblswatwasurfacewater.runoff);
            ViewBag.surfaceVar = new SelectList(db.lkpswatsurfacevarlus, "id", "Description", tblswatwasurfacewater.surfaceVar);
            return View(tblswatwasurfacewater);
        }

        // POST: /WASurfaceWater/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,runoff,surfaceVar")] tblswatwasurfacewater tblswatwasurfacewater, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatwasurfacewater).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwasurfacewater);

                if (submitBtn.Equals("Next"))
                {
                    // If there is not any WAGroundWater with the current survey (SurveyID) then create one and redirect to its edit link.
                    var groundWaters = db.tblswatwagroundwaters.Where(e => e.SurveyID == tblswatwasurfacewater.SurveyID);
                    if (!groundWaters.Any())
                    {
                        tblswatwagroundwater tblswatwagroundwater = new tblswatwagroundwater();
                        tblswatwagroundwater.SurveyID = tblswatwasurfacewater.SurveyID;
                        db.tblswatwagroundwaters.Add(tblswatwagroundwater);
                        db.SaveChanges();

                        int newGroundWaterID = tblswatwagroundwater.ID;
                        return RedirectToAction("Edit", "WAGroundWater", new { id = newGroundWaterID, SurveyID = tblswatwagroundwater.SurveyID });
                    }
                    else
                    {
                        return RedirectToAction("Edit", "WAGroundWater", new { id = groundWaters.Single(e => e.SurveyID == tblswatwasurfacewater.SurveyID).ID, SurveyID = tblswatwasurfacewater.SurveyID });
                    }
                }
            }
            ViewBag.runoff = new SelectList(db.lkpswatrunofflus, "ID", "Description", tblswatwasurfacewater.runoff);
            ViewBag.surfaceVar = new SelectList(db.lkpswatsurfacevarlus, "id", "Description", tblswatwasurfacewater.surfaceVar);
            return View(tblswatwasurfacewater);
        }

        // GET: /WASurfaceWater/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblswatwasurfacewater tblswatwasurfacewater = db.tblswatwasurfacewaters.Find(id);
        //    if (tblswatwasurfacewater == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwasurfacewater);
        //}

        // POST: /WASurfaceWater/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblswatwasurfacewater tblswatwasurfacewater = db.tblswatwasurfacewaters.Find(id);
        //    db.tblswatwasurfacewaters.Remove(tblswatwasurfacewater);
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
