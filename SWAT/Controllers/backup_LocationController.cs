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
    public class backup_LocationController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /Location/
        public ActionResult Index()
        {
            var tblswatlocations = db.tblswatlocations.Include(t => t.lkpcountry).Include(t => t.lkpregion).Include(t => t.lkpsubnational);
            return View(tblswatlocations.ToList());
        }

        // GET: /Location/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatlocation tblswatlocation = db.tblswatlocations.Find(id);
            if (tblswatlocation == null)
            {
                return HttpNotFound();
            }
            return View(tblswatlocation);
        }

        // GET: /Location/Create
        public ActionResult Create(int? uid)
        {
            ViewBag.countryID = new SelectList(db.lkpcountries, "ID", "Name");
            ViewBag.regionID = new SelectList(db.lkpregions, "ID", "Name");
            ViewBag.subnationalID = new SelectList(db.lkpsubnationals, "ID", "Name");
            ViewBag.uid = uid;
            return View();
        }

        // POST: /Location/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,latitude,longitude,countryID,name,regionID,subnationalID")] tblswatlocation tblswatlocation)
        {
            if (ModelState.IsValid)
            {
                db.tblswatlocations.Add(tblswatlocation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.countryID = new SelectList(db.lkpcountries, "ID", "Name", tblswatlocation.countryID);
            ViewBag.regionID = new SelectList(db.lkpregions, "ID", "Name", tblswatlocation.regionID);
            ViewBag.subnationalID = new SelectList(db.lkpsubnationals, "ID", "Name", tblswatlocation.subnationalID);
            return View(tblswatlocation);
        }

        // GET: /Location/Edit/5
        public ActionResult Edit(int? id, int? uid)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatlocation tblswatlocation = db.tblswatlocations.Find(id);
            if (tblswatlocation == null)
            {
                return HttpNotFound();
            }

            ViewBag.countryID = new SelectList(db.lkpcountries, "ID", "Name", tblswatlocation.countryID);
            ViewBag.regionID = new SelectList(db.lkpregions, "ID", "Name", tblswatlocation.regionID);
            ViewBag.subnationalID = new SelectList(db.lkpsubnationals, "ID", "Name", tblswatlocation.subnationalID);
            ViewBag.uid = uid;
            return View(tblswatlocation);
        }

        // POST: /Location/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,latitude,longitude,countryID,name,regionID,subnationalID")] tblswatlocation tblswatlocation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatlocation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.countryID = new SelectList(db.lkpcountries, "ID", "Name", tblswatlocation.countryID);
            ViewBag.regionID = new SelectList(db.lkpregions, "ID", "Name", tblswatlocation.regionID);
            ViewBag.subnationalID = new SelectList(db.lkpsubnationals, "ID", "Name", tblswatlocation.subnationalID);
            return View(tblswatlocation);
        }

        // GET: /Location/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatlocation tblswatlocation = db.tblswatlocations.Find(id);
            if (tblswatlocation == null)
            {
                return HttpNotFound();
            }
            return View(tblswatlocation);
        }

        // POST: /Location/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblswatlocation tblswatlocation = db.tblswatlocations.Find(id);
            db.tblswatlocations.Remove(tblswatlocation);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
