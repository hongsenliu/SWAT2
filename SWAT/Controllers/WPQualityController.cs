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
    public class WPQualityController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /WPQuality/

        //public ActionResult Index()
        //{
        //    var tblswatwpqualities = db.tblswatwpqualities.Include(t => t.lkpSWATfaecalPathogensLU).Include(t => t.lkpSWATqualTreatedLU).Include(t => t.lkpSWATuserTreatedLU).Include(t => t.lkpSWATuserTreatmentMethodLU).Include(t => t.lkpSWATwaterTasteOdourLU).Include(t => t.lkpSWATwaterTasteOdourLU1).Include(t => t.lkpSWATwaterTurbidityLU).Include(t => t.tblSWATWPoverview);
        //    return View(tblswatwpqualities.ToList());
        //}

        //
        // GET: /WPQuality/Details/5

        //public ActionResult Details(long id = 0)
        //{
        //    tblswatwpquality tblswatwpquality = db.tblswatwpqualities.Find(id);
        //    if (tblswatwpquality == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpquality);
        //}

        private void getQuestions()
        {
            ViewBag.Question1 = db.lkpswatwpscorelus.First(e => e.ScoreName == "qualTreatedSCORE").Description;
            ViewBag.Question2 = db.lkpswatwpscorelus.First(e => e.ScoreName == "userTreatedSCORE").Description;
            ViewBag.Question3 = db.lkpswatwpscorelus.First(e => e.ScoreName == "userTreatmentMethodSCORE").Description;
            ViewBag.Question4 = db.lkpswatwpscorelus.First(e => e.ScoreName == "faecalPathogensSCORE").Description;
            ViewBag.Question5 = db.lkpswatwpscorelus.First(e => e.ScoreName == "waterTasteSCORE").Description;
            ViewBag.Question6 = db.lkpswatwpscorelus.First(e => e.ScoreName == "waterOdourSCORE").Description;
            ViewBag.Question7 = db.lkpswatwpscorelus.First(e => e.ScoreName == "waterTurbiditySCORE").Description;
        }

        private void updateScores(tblswatwpquality tblswatwpquality)
        {
            if (tblswatwpquality.qualTreated != null)
            {
                int intorder = (int)db.lkpswatqualtreatedlus.Find(tblswatwpquality.qualTreated).intorder;
                if (intorder < 5)
                {
                    double score = Convert.ToDouble(db.lkpswatscores_qualtreated.First(e => e.intorder == intorder).Description);
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "qualTreatedSCORE").Value = score;
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "qualTreatedSCORE").Value = null;
                }
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "qualTreatedSCORE").Value = null;
            }

            if (tblswatwpquality.userTreated != null)
            {
                int intorder = (int)db.lkpswatusertreatedlus.Find(tblswatwpquality.userTreated).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_usertreatedifuntreatedbefore.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatedSCORE").Value = score;
                
            }
            else
            {
                if (tblswatwpquality.qualTreated != null)
                {
                    if (tblswatwpquality.qualTreated > 1261 && tblswatwpquality.qualTreated < 1265)
                    {
                        db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatedSCORE").Value = 1;
                    }
                    else
                    {
                        db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatedSCORE").Value = null;
                    }
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatedSCORE").Value = null;
                }
            }

            if (tblswatwpquality.userTreatmentMethod != null)
            {
                int intorder = (int)db.lkpswatusertreatmentmethodlus.Find(tblswatwpquality.userTreatmentMethod).intorder;
                if (intorder < 8)
                {
                    double score = Convert.ToDouble(db.lkpswatscores_usertreatmentmethod.First(e => e.intorder == intorder).Description);
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatmentMethodSCORE").Value = score;
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatmentMethodSCORE").Value = null;
                }
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "userTreatmentMethodSCORE").Value = null;
            }

            if (tblswatwpquality.faecalPathogens != null)
            {
                int intorder = (int)db.lkpswatfaecalpathogenslus.Find(tblswatwpquality.faecalPathogens).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_faecalpathogens.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "faecalPathogensSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "faecalPathogensSCORE").Value = null;
            }

            if (tblswatwpquality.waterTaste != null)
            {
                int intorder = (int)db.lkpswatwatertasteodourlus.Find(tblswatwpquality.waterTaste).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watertasteodour.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterTasteSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterTasteSCORE").Value = null;
            }

            if (tblswatwpquality.waterOdour != null)
            {
                int intorder = (int)db.lkpswatwatertasteodourlus.Find(tblswatwpquality.waterOdour).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watertasteodour.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterOdourSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterOdourSCORE").Value = null;
            }

            if (tblswatwpquality.waterTurbidity != null)
            {
                int intorder = (int)db.lkpswatwaterturbiditylus.Find(tblswatwpquality.waterTurbidity).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_waterturbidity.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterTurbiditySCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpquality.wpID && e.ScoreName == "waterTurbiditySCORE").Value = null;
            }
            db.SaveChanges();
        }

        //
        // GET: /WPQuality/Create

        public ActionResult Create(long? wpID)
        {
            if (wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.faecalPathogens = new SelectList(db.lkpswatfaecalpathogenslus, "id", "Description");
            ViewBag.qualTreated = new SelectList(db.lkpswatqualtreatedlus, "id", "Description");
            ViewBag.userTreated = new SelectList(db.lkpswatusertreatedlus, "id", "Description");
            ViewBag.userTreatmentMethod = new SelectList(db.lkpswatusertreatmentmethodlus, "id", "Description");
            ViewBag.waterOdour = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description");
            ViewBag.waterTaste = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description");
            ViewBag.waterTurbidity = new SelectList(db.lkpswatwaterturbiditylus, "id", "Description");
            getQuestions();
            return View();
        }

        //
        // POST: /WPQuality/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatwpquality tblswatwpquality, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatwpqualities.Where(e => e.wpID == tblswatwpquality.wpID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    long recordId = recordIDs.First();
                    tblswatwpquality.ID = recordId;
                    db.Entry(tblswatwpquality).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatwpqualities.Add(tblswatwpquality);
                }
                db.SaveChanges();
                updateScores(tblswatwpquality);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WPSwpr", new { wpID = tblswatwpquality.wpID });
                }
            }

            ViewBag.faecalPathogens = new SelectList(db.lkpswatfaecalpathogenslus, "id", "Description", tblswatwpquality.faecalPathogens);
            ViewBag.qualTreated = new SelectList(db.lkpswatqualtreatedlus, "id", "Description", tblswatwpquality.qualTreated);
            ViewBag.userTreated = new SelectList(db.lkpswatusertreatedlus, "id", "Description", tblswatwpquality.userTreated);
            ViewBag.userTreatmentMethod = new SelectList(db.lkpswatusertreatmentmethodlus, "id", "Description", tblswatwpquality.userTreatmentMethod);
            ViewBag.waterOdour = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterOdour);
            ViewBag.waterTaste = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterTaste);
            ViewBag.waterTurbidity = new SelectList(db.lkpswatwaterturbiditylus, "id", "Description", tblswatwpquality.waterTurbidity);
            getQuestions();
            return View(tblswatwpquality);
        }

        //
        // GET: /WPQuality/Edit/5

        public ActionResult Edit(long? id, long? wpID)
        {
            if (id == null || wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatwpquality tblswatwpquality = db.tblswatwpqualities.Find(id);
            if (tblswatwpquality == null)
            {
                return HttpNotFound();
            }
            ViewBag.faecalPathogens = new SelectList(db.lkpswatfaecalpathogenslus, "id", "Description", tblswatwpquality.faecalPathogens);
            ViewBag.qualTreated = new SelectList(db.lkpswatqualtreatedlus, "id", "Description", tblswatwpquality.qualTreated);
            ViewBag.userTreated = new SelectList(db.lkpswatusertreatedlus, "id", "Description", tblswatwpquality.userTreated);
            ViewBag.userTreatmentMethod = new SelectList(db.lkpswatusertreatmentmethodlus, "id", "Description", tblswatwpquality.userTreatmentMethod);
            ViewBag.waterOdour = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterOdour);
            ViewBag.waterTaste = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterTaste);
            ViewBag.waterTurbidity = new SelectList(db.lkpswatwaterturbiditylus, "id", "Description", tblswatwpquality.waterTurbidity);
            getQuestions();
            return View(tblswatwpquality);
        }

        //
        // POST: /WPQuality/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatwpquality tblswatwpquality, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatwpquality).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwpquality);

                if (submitBtn.Equals("Next"))
                {
                    var records = db.tblswatwpswprs.Where(e => e.wpID == tblswatwpquality.wpID);
                    if (!records.Any())
                    {
                        tblswatwpswpr newEntry = new tblswatwpswpr();
                        newEntry.wpID = tblswatwpquality.wpID;
                        db.tblswatwpswprs.Add(newEntry);
                        db.SaveChanges();

                        long newId = newEntry.ID;
                        return RedirectToAction("Edit", "WPSwpr", new { id = newId, wpID = newEntry.wpID });
                    }

                    return RedirectToAction("Edit", "WPSwpr", new { id = records.First(e => e.wpID == tblswatwpquality.wpID).ID, wpID = tblswatwpquality.wpID });
                }
            }
            ViewBag.faecalPathogens = new SelectList(db.lkpswatfaecalpathogenslus, "id", "Description", tblswatwpquality.faecalPathogens);
            ViewBag.qualTreated = new SelectList(db.lkpswatqualtreatedlus, "id", "Description", tblswatwpquality.qualTreated);
            ViewBag.userTreated = new SelectList(db.lkpswatusertreatedlus, "id", "Description", tblswatwpquality.userTreated);
            ViewBag.userTreatmentMethod = new SelectList(db.lkpswatusertreatmentmethodlus, "id", "Description", tblswatwpquality.userTreatmentMethod);
            ViewBag.waterOdour = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterOdour);
            ViewBag.waterTaste = new SelectList(db.lkpswatwatertasteodourlus, "id", "Description", tblswatwpquality.waterTaste);
            ViewBag.waterTurbidity = new SelectList(db.lkpswatwaterturbiditylus, "id", "Description", tblswatwpquality.waterTurbidity);
            getQuestions();
            return View(tblswatwpquality);
        }

        //
        // GET: /WPQuality/Delete/5

        //public ActionResult Delete(long id = 0)
        //{
        //    tblswatwpquality tblswatwpquality = db.tblswatwpqualities.Find(id);
        //    if (tblswatwpquality == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpquality);
        //}

        //
        // POST: /WPQuality/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    tblswatwpquality tblswatwpquality = db.tblswatwpqualities.Find(id);
        //    db.tblswatwpqualities.Remove(tblswatwpquality);
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