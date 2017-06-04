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
    public class WPSwprController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /WPSwpr/

        //public ActionResult Index()
        //{
        //    var tblswatwpswprs = db.tblswatwpswprs.Include(t => t.lkpSWATpipedAgeLU).Include(t => t.lkpswatpipedcost1lu).Include(t => t.lkpswatpipedcost2lu).Include(t => t.lkpswatpipedcost3lu).Include(t => t.lkpSWATpipedSuitabilityLU).Include(t => t.lkpSWATYesNoLU).Include(t => t.lkpSWATYesNoLU1).Include(t => t.lkpSWATYesNoLU2).Include(t => t.lkpSWATYesNoLU3).Include(t => t.lkpSWATYesNoLU4).Include(t => t.lkpSWATYesNoLU5).Include(t => t.lkpSWATYesNoLU6).Include(t => t.lkpSWATYesNoLU7).Include(t => t.lkpSWATYesNoLU8).Include(t => t.lkpSWATYesNoLU9).Include(t => t.lkpSWATYesNoLU10).Include(t => t.lkpSWATYesNoLU11).Include(t => t.lkpSWATYesNoLU12).Include(t => t.lkpSWATYesNoLU13).Include(t => t.lkpSWATYesNoLU14).Include(t => t.lkpSWATYesNoLU15).Include(t => t.lkpSWATYesNoLU16).Include(t => t.lkpSWATYesNoLU17).Include(t => t.lkpSWATYesNoLU18).Include(t => t.lkpSWATYesNoLU19).Include(t => t.lkpSWATYesNoLU20).Include(t => t.lkpSWATYesNoLU21).Include(t => t.lkpSWATYesNoLU22).Include(t => t.lkpSWATYesNoLU23).Include(t => t.lkpSWATYesNoLU24).Include(t => t.lkpSWATYesNoLU25).Include(t => t.lkpSWATYesNoLU26).Include(t => t.lkpSWATYesNoLU27).Include(t => t.lkpSWATYesNoLU28).Include(t => t.lkpSWATYesNoLU29).Include(t => t.lkpSWATYesNoLU30).Include(t => t.lkpSWATYesNoLU31).Include(t => t.lkpSWATYesNoLU32).Include(t => t.lkpSWATYesNoLU33).Include(t => t.lkpSWATYesNoLU34).Include(t => t.lkpSWATYesNoLU35).Include(t => t.lkpSWATYesNoLU36).Include(t => t.tblswatwpoverview);
        //    return View(tblswatwpswprs.ToList());
        //}

        //
        // GET: /WPSwpr/Details/5

        //public ActionResult Details(long id = 0)
        //{
        //    tblswatwpswpr tblswatwpswpr = db.tblswatwpswprs.Find(id);
        //    if (tblswatwpswpr == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpswpr);
        //}

        private void getQuestions()
        {
            ViewBag.Question1 = db.lkpswatwpscorelus.First(e => e.ScoreName == "surfaceFencedSCORE").Description;
            ViewBag.Question2 = db.lkpswatwpscorelus.First(e => e.ScoreName == "surfaceUphillSCORE").Description;
            ViewBag.Question3 = db.lkpswatwpscorelus.First(e => e.ScoreName == "surfaceHealthSCORE").Description;
            ViewBag.Question4 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springBoxSCORE").Description;
            ViewBag.Question5 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springBackfillSCORE").Description;
            ViewBag.Question6 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springCoverSCORE").Description;
            ViewBag.Question7 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springWaterPoolSCORE").Description;
            ViewBag.Question8 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springAnimalsSCORE").Description;
            ViewBag.Question9 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springLatrineSCORE").Description;
            ViewBag.Question10 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springUphillSCORE").Description;
            ViewBag.Question11 = db.lkpswatwpscorelus.First(e => e.ScoreName == "springDiversionSCORE").Description;
            ViewBag.Question12 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholeLatrineSCORE").Description;
            ViewBag.Question13 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholeUphillSCORE").Description;
            ViewBag.Question14 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholeWellNearSCORE").Description;
            ViewBag.Question15 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholeAnimalSCORE").Description;
            ViewBag.Question16 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholePlatformSCORE").Description;
            ViewBag.Question17 = db.lkpswatwpscorelus.First(e => e.ScoreName == "boreholeSealSCORE").Description;
            ViewBag.Question18 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wellLatrineSCORE").Description;
            ViewBag.Question19 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wellUphillSCORE").Description;
            ViewBag.Question20 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wellAnimalsSCORE").Description;
            ViewBag.Question21 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wellPlatformSCORE").Description;
            ViewBag.Question22 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wellCoverSCORE").Description;
            ViewBag.Question23 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwFirstFlushSCORE").Description;
            ViewBag.Question24 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwOpenSCORE").Description;
            ViewBag.Question25 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwRoofSCORE").Description;
            ViewBag.Question26 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwGuttersSCORE").Description;
            ViewBag.Question27 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwCrackedSCORE").Description;
            ViewBag.Question28 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwDipperSCORE").Description;
            ViewBag.Question29 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwLeakSCORE").Description;
            ViewBag.Question30 = db.lkpswatwpscorelus.First(e => e.ScoreName == "rwDirtySCORE").Description;
            ViewBag.Question31 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedStoreSCORE").Description;
            ViewBag.Question32 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedLeakSCORE").Description;
            ViewBag.Question33 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedShareSCORE").Description;
            ViewBag.Question34 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedAnimalsSCORE").Description;
            ViewBag.Question35 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedBreakSCORE").Description;
            ViewBag.Question36 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedIllegalSCORE").Description;
            ViewBag.Question37 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedSuitabilitySCORE").Description;
            ViewBag.Question38 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedCostsSCORE1").Description;
            ViewBag.Question39 = "What is the average revenue generated in a month (in local currency)?";
            ViewBag.Question40 = "What is the average monthly expenditure (in local currency)?";
            ViewBag.Question41 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedCapitalSCORE").Description;
            ViewBag.Question42 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedAgeSCORE").Description;
            ViewBag.Question43 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedFutureSCORE").Description;
            ViewBag.Question44 = db.lkpswatwpscorelus.First(e => e.ScoreName == "pipedFuturePopSCORE").Description;
        }

        private void updateScores(tblswatwpswpr tblswatwpswpr)
        {
            if (tblswatwpswpr.surfaceFenced != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.surfaceFenced).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnoluyesgood.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceFencedSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceFencedSCORE").Value = null;
            }

            if (tblswatwpswpr.surfaceUphill != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.surfaceUphill).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceUphillSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceUphillSCORE").Value = null;
            }

            if (tblswatwpswpr.surfaceHealth != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.surfaceHealth).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceHealthSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "surfaceHealthSCORE").Value = null;
            }

            if (tblswatwpswpr.springBox != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springBox).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnoluyesgood.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springBoxSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springBoxSCORE").Value = null;
            }

            if (tblswatwpswpr.springBackfill != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springBackfill).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springBackfillSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springBackfillSCORE").Value = null;
            }

            if (tblswatwpswpr.springCover != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springCover).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springCoverSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springCoverSCORE").Value = null;
            }

            if (tblswatwpswpr.springWaterPool != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springWaterPool).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springWaterPoolSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springWaterPoolSCORE").Value = null;
            }

            if (tblswatwpswpr.springAnimals != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springAnimals).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springAnimalsSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springAnimalsSCORE").Value = null;
            }

            if (tblswatwpswpr.springLatrine != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springLatrine).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springLatrineSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springLatrineSCORE").Value = null;
            }

            if (tblswatwpswpr.springUphill != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springUphill).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springUphillSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springUphillSCORE").Value = null;
            }

            if (tblswatwpswpr.springDiversion != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.springDiversion).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springDiversionSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "springDiversionSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholeLatrine != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholeLatrine).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeLatrineSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeLatrineSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholeUphill != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholeUphill).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeUphillSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeUphillSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholeWellNear != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholeWellNear).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeWellNearSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeWellNearSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholeAnimals != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholeAnimals).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeAnimalSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeAnimalSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholePlatform != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholePlatform).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholePlatformSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholePlatformSCORE").Value = null;
            }

            if (tblswatwpswpr.boreholeSeal != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.boreholeSeal).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeSealSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "boreholeSealSCORE").Value = null;
            }

            if (tblswatwpswpr.wellLatrine != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.wellLatrine).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellLatrineSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellLatrineSCORE").Value = null;
            }

            if (tblswatwpswpr.wellUphill != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.wellUphill).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellUphillSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellUphillSCORE").Value = null;
            }

            if (tblswatwpswpr.wellAnimals != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.wellAnimals).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellAnimalsSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellAnimalsSCORE").Value = null;
            }

            if (tblswatwpswpr.wellPlatform != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.wellPlatform).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellPlatformSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellPlatformSCORE").Value = null;
            }

            if (tblswatwpswpr.wellCover != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.wellCover).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellCoverSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "wellCoverSCORE").Value = null;
            }

            if (tblswatwpswpr.rwFirstFlush != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwFirstFlush).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwFirstFlushSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwFirstFlushSCORE").Value = null;
            }

            if (tblswatwpswpr.rwOpen != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwOpen).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwOpenSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwOpenSCORE").Value = null;
            }

            if (tblswatwpswpr.rwRoof != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwRoof).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwRoofSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwRoofSCORE").Value = null;
            }

            if (tblswatwpswpr.rwGutters != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwGutters).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwGuttersSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwGuttersSCORE").Value = null;
            }

            if (tblswatwpswpr.rwCracked != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwCracked).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwCrackedSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwCrackedSCORE").Value = null;
            }

            if (tblswatwpswpr.rwDipper != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwDipper).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwDipperSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwDipperSCORE").Value = null;
            }

            if (tblswatwpswpr.rwLeak != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwLeak).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwLeakSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwLeakSCORE").Value = null;
            }

            if (tblswatwpswpr.rwDirty != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.rwDirty).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwDirtySCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "rwDirtySCORE").Value = null;
            }

            if (tblswatwpswpr.pipedStore != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedStore).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedStoreSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedStoreSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedLeak != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedLeak).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedLeakSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedLeakSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedShare != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedShare).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedShareSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedShareSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedAnimals != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedAnimals).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedAnimalsSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedAnimalsSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedBreak != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedBreak).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedBreakSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedBreakSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedIllegal != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedIllegal).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnolu_yesbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedIllegalSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedIllegalSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedSuitability != null)
            {
                int intorder = (int)db.lkpswatpipedsuitabilitylus.Find(tblswatwpswpr.pipedSuitability).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pipedsuitability.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedSuitabilitySCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedSuitabilitySCORE").Value = null;
            }

            if (tblswatwpswpr.pipedCosts1 != null)
            {
                int intorder = (int)db.lkpswatpipedcost1lu.Find(tblswatwpswpr.pipedCosts1).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pipedcost1.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE1").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE1").Value = null;
            }

            if (tblswatwpswpr.pipedCosts2 != null)
            {
                int intorder = (int)db.lkpswatpipedcost2lu.Find(tblswatwpswpr.pipedCosts2).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pipedcost2.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE2").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE2").Value = null;
            }

            if (tblswatwpswpr.pipedCosts3 != null)
            {
                int intorder = (int)db.lkpswatpipedcost3lu.Find(tblswatwpswpr.pipedCosts3).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pipedcost3.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE3").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCostsSCORE3").Value = null;
            }

            if (tblswatwpswpr.pipedRevenue != null && tblswatwpswpr.pipedExpenditure != null)
            {
                double revenue = (double)tblswatwpswpr.pipedRevenue;
                double exp = (double)tblswatwpswpr.pipedExpenditure;
                double balance = (revenue - exp) / revenue;
                tblswatwpswpr.pipedBalance = balance;
                int intorder = 0;
                foreach (var item in db.lkpswatpipedbalancelus.OrderByDescending(e => e.Description))
                {
                    if (balance >= Convert.ToDouble(item.Description))
                    {
                        intorder = (int)item.intorder;
                        break;
                    }
                }


                if (intorder > 0 && intorder < 7)
                {
                    double score = Convert.ToDouble(db.lkpswatscores_pipedbalance.First(e => e.intorder == intorder).Description);
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedBalanceSCORE").Value = score;
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedBalanceSCORE").Value = null;
                }
            }
            else
            {
                tblswatwpswpr.pipedBalance = null;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedBalanceSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedCapital != null)
            {
                int intorder = (int)db.lkpswatyesnolus.Find(tblswatwpswpr.pipedCapital).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_yesnoluyesgood.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCapitalSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedCapitalSCORE").Value = null;
            }

            if (tblswatwpswpr.pipedAge != null)
            {
                int intorder = (int)db.lkpswatpipedagelus.Find(tblswatwpswpr.pipedAge).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_pipedage.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedAgeSCORE").Value = score;

            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedAgeSCORE").Value = null;
            }

            if (db.tblswatwpoverviews.Find(tblswatwpswpr.wpID).wpaType == "piped")
            {
                bool[] pipedFutures = { tblswatwpswpr.pipedFuture1, tblswatwpswpr.pipedFuture2, tblswatwpswpr.pipedFuture3 };
                int checkedPF = 0;
                foreach (bool item in pipedFutures)
                {
                    if (item)
                    {
                        checkedPF++;
                    }
                }
                double pfScore = (double)checkedPF / 3.0;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedFutureSCORE").Value = pfScore;

                bool[] pipedPops = { tblswatwpswpr.pipedFuturePop1, tblswatwpswpr.pipedFuturePop2, tblswatwpswpr.pipedFuturePop3 };
                int checkedPP = 0;
                foreach (bool item in pipedPops)
                {
                    if (item)
                    {
                        checkedPP++;
                    }
                }
                double ppScore = (double)checkedPP / 3.0;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedFuturePopSCORE").Value = ppScore;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedFutureSCORE").Value = null;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpswpr.wpID && e.ScoreName == "pipedFuturePopSCORE").Value = null;
            }

            db.SaveChanges();
        }

        private void checkRevenue(tblswatwpswpr tblswatwpswpr)
        {
            if (tblswatwpswpr.pipedRevenue != null)
            {
                if (tblswatwpswpr.pipedExpenditure == null)
                {
                    ModelState.AddModelError("pipedExpenditure", "Expenditure cannot be empty.");
                }
            }

            if (tblswatwpswpr.pipedExpenditure != null)
            {
                if (tblswatwpswpr.pipedRevenue == null)
                {
                    ModelState.AddModelError("pipedRevenue", "Revenue cannot be empty.");
                }
            }
        }

        //
        // GET: /WPSwpr/Create

        public ActionResult Create(long? wpID)
        {
            if (wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.pipedAge = new SelectList(db.lkpswatpipedagelus, "id", "Description");
            ViewBag.pipedCosts1 = new SelectList(db.lkpswatpipedcost1lu, "id", "Description");
            ViewBag.pipedCosts2 = new SelectList(db.lkpswatpipedcost2lu, "id", "Description");
            ViewBag.pipedCosts3 = new SelectList(db.lkpswatpipedcost3lu, "id", "Description");
            ViewBag.pipedSuitability = new SelectList(db.lkpswatpipedsuitabilitylus, "id", "Description");
            ViewBag.surfaceFenced = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.surfaceUphill = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.surfaceHealth = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springBox = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springBackfill = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springCover = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springWaterPool = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springUphill = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.springDiversion = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholeLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholeUphill = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholeWellNear = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholeAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholePlatform = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.boreholeSeal = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.wellLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.wellUphill = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.wellAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.wellPlatform = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.wellCover = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwFirstFlush = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwOpen = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwRoof = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwGutters = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwCracked = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwDipper = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwLeak = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.rwDirty = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedStore = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedLeak = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedShare = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedBreak = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedIllegal = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.pipedCapital = new SelectList(db.lkpswatyesnolus, "id", "Description");
            getQuestions();
            ViewBag.watType = db.tblswatwpoverviews.Find(wpID).wpaType;
            return View();
        }

        //
        // POST: /WPSwpr/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatwpswpr tblswatwpswpr)
        {
            checkRevenue(tblswatwpswpr);
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatwpswprs.Where(e => e.wpID == tblswatwpswpr.wpID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    long recordId = recordIDs.First();
                    tblswatwpswpr.ID = recordId;
                    db.Entry(tblswatwpswpr).State = EntityState.Modified;
                    db.SaveChanges();
                    updateScores(tblswatwpswpr);

                    return RedirectToAction("WaterPoints", "Survey", new { id = db.tblswatwpswprs.Include(t => t.tblswatwpoverview).First(e => e.wpID == tblswatwpswpr.wpID).tblswatwpoverview.SurveyID });
                }
                db.tblswatwpswprs.Add(tblswatwpswpr);
                db.SaveChanges();
                updateScores(tblswatwpswpr);
                
                // return RedirectToAction("Index");
                return RedirectToAction("WaterPoints", "Survey", new { id = db.tblswatwpswprs.Include(t => t.tblswatwpoverview).First(e => e.wpID == tblswatwpswpr.wpID).tblswatwpoverview.SurveyID });
            }

            ViewBag.pipedAge = new SelectList(db.lkpswatpipedagelus, "id", "Description", tblswatwpswpr.pipedAge);
            ViewBag.pipedCosts1 = new SelectList(db.lkpswatpipedcost1lu, "id", "Description", tblswatwpswpr.pipedCosts1);
            ViewBag.pipedCosts2 = new SelectList(db.lkpswatpipedcost2lu, "id", "Description", tblswatwpswpr.pipedCosts2);
            ViewBag.pipedCosts3 = new SelectList(db.lkpswatpipedcost3lu, "id", "Description", tblswatwpswpr.pipedCosts3);
            ViewBag.pipedSuitability = new SelectList(db.lkpswatpipedsuitabilitylus, "id", "Description", tblswatwpswpr.pipedSuitability);
            ViewBag.surfaceFenced = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceFenced);
            ViewBag.surfaceUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceUphill);
            ViewBag.surfaceHealth = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceHealth);
            ViewBag.springBox = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBox);
            ViewBag.springBackfill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBackfill);
            ViewBag.springCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springCover);
            ViewBag.springWaterPool = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springWaterPool);
            ViewBag.springAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springAnimals);
            ViewBag.springLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springLatrine);
            ViewBag.springUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springUphill);
            ViewBag.springDiversion = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springDiversion);
            ViewBag.boreholeLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeLatrine);
            ViewBag.boreholeUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeUphill);
            ViewBag.boreholeWellNear = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeWellNear);
            ViewBag.boreholeAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeAnimals);
            ViewBag.boreholePlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholePlatform);
            ViewBag.boreholeSeal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeSeal);
            ViewBag.wellLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellLatrine);
            ViewBag.wellUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellUphill);
            ViewBag.wellAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellAnimals);
            ViewBag.wellPlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellPlatform);
            ViewBag.wellCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellCover);
            ViewBag.rwFirstFlush = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwFirstFlush);
            ViewBag.rwOpen = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwOpen);
            ViewBag.rwRoof = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwRoof);
            ViewBag.rwGutters = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwGutters);
            ViewBag.rwCracked = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwCracked);
            ViewBag.rwDipper = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDipper);
            ViewBag.rwLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwLeak);
            ViewBag.rwDirty = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDirty);
            ViewBag.pipedStore = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedStore);
            ViewBag.pipedLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedLeak);
            ViewBag.pipedShare = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedShare);
            ViewBag.pipedAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedAnimals);
            ViewBag.pipedBreak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedBreak);
            ViewBag.pipedIllegal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedIllegal);
            ViewBag.pipedCapital = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedCapital);
            getQuestions();
            ViewBag.watType = db.tblswatwpoverviews.Find(tblswatwpswpr.wpID).wpaType;
            return View(tblswatwpswpr);
        }

        //
        // GET: /WPSwpr/Edit/5

        public ActionResult Edit(long? id, long? wpID)
        {
            if (id == null || wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            tblswatwpswpr tblswatwpswpr = db.tblswatwpswprs.Find(id);
            if (tblswatwpswpr == null)
            {
                return HttpNotFound();
            }
            ViewBag.pipedAge = new SelectList(db.lkpswatpipedagelus, "id", "Description", tblswatwpswpr.pipedAge);
            ViewBag.pipedCosts1 = new SelectList(db.lkpswatpipedcost1lu, "id", "Description", tblswatwpswpr.pipedCosts1);
            ViewBag.pipedCosts2 = new SelectList(db.lkpswatpipedcost2lu, "id", "Description", tblswatwpswpr.pipedCosts2);
            ViewBag.pipedCosts3 = new SelectList(db.lkpswatpipedcost3lu, "id", "Description", tblswatwpswpr.pipedCosts3);
            ViewBag.pipedSuitability = new SelectList(db.lkpswatpipedsuitabilitylus, "id", "Description", tblswatwpswpr.pipedSuitability);
            ViewBag.surfaceFenced = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceFenced);
            ViewBag.surfaceUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceUphill);
            ViewBag.surfaceHealth = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceHealth);
            ViewBag.springBox = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBox);
            ViewBag.springBackfill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBackfill);
            ViewBag.springCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springCover);
            ViewBag.springWaterPool = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springWaterPool);
            ViewBag.springAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springAnimals);
            ViewBag.springLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springLatrine);
            ViewBag.springUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springUphill);
            ViewBag.springDiversion = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springDiversion);
            ViewBag.boreholeLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeLatrine);
            ViewBag.boreholeUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeUphill);
            ViewBag.boreholeWellNear = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeWellNear);
            ViewBag.boreholeAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeAnimals);
            ViewBag.boreholePlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholePlatform);
            ViewBag.boreholeSeal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeSeal);
            ViewBag.wellLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellLatrine);
            ViewBag.wellUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellUphill);
            ViewBag.wellAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellAnimals);
            ViewBag.wellPlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellPlatform);
            ViewBag.wellCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellCover);
            ViewBag.rwFirstFlush = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwFirstFlush);
            ViewBag.rwOpen = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwOpen);
            ViewBag.rwRoof = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwRoof);
            ViewBag.rwGutters = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwGutters);
            ViewBag.rwCracked = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwCracked);
            ViewBag.rwDipper = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDipper);
            ViewBag.rwLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwLeak);
            ViewBag.rwDirty = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDirty);
            ViewBag.pipedStore = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedStore);
            ViewBag.pipedLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedLeak);
            ViewBag.pipedShare = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedShare);
            ViewBag.pipedAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedAnimals);
            ViewBag.pipedBreak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedBreak);
            ViewBag.pipedIllegal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedIllegal);
            ViewBag.pipedCapital = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedCapital);
            getQuestions();
            ViewBag.watType = db.tblswatwpoverviews.Find(wpID).wpaType;

            return View(tblswatwpswpr);
        }

        //
        // POST: /WPSwpr/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatwpswpr tblswatwpswpr)
        {
            checkRevenue(tblswatwpswpr);

            if (ModelState.IsValid)
            {
                db.Entry(tblswatwpswpr).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwpswpr);

                // return RedirectToAction("Index");
                return RedirectToAction("WaterPoints", "Survey", new { id = db.tblswatwpswprs.Include(t => t.tblswatwpoverview).First(e => e.wpID == tblswatwpswpr.wpID).tblswatwpoverview.SurveyID });
            }
            ViewBag.pipedAge = new SelectList(db.lkpswatpipedagelus, "id", "Description", tblswatwpswpr.pipedAge);
            ViewBag.pipedCosts1 = new SelectList(db.lkpswatpipedcost1lu, "id", "Description", tblswatwpswpr.pipedCosts1);
            ViewBag.pipedCosts2 = new SelectList(db.lkpswatpipedcost2lu, "id", "Description", tblswatwpswpr.pipedCosts2);
            ViewBag.pipedCosts3 = new SelectList(db.lkpswatpipedcost3lu, "id", "Description", tblswatwpswpr.pipedCosts3);
            ViewBag.pipedSuitability = new SelectList(db.lkpswatpipedsuitabilitylus, "id", "Description", tblswatwpswpr.pipedSuitability);
            ViewBag.surfaceFenced = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceFenced);
            ViewBag.surfaceUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceUphill);
            ViewBag.surfaceHealth = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.surfaceHealth);
            ViewBag.springBox = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBox);
            ViewBag.springBackfill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springBackfill);
            ViewBag.springCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springCover);
            ViewBag.springWaterPool = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springWaterPool);
            ViewBag.springAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springAnimals);
            ViewBag.springLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springLatrine);
            ViewBag.springUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springUphill);
            ViewBag.springDiversion = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.springDiversion);
            ViewBag.boreholeLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeLatrine);
            ViewBag.boreholeUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeUphill);
            ViewBag.boreholeWellNear = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeWellNear);
            ViewBag.boreholeAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeAnimals);
            ViewBag.boreholePlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholePlatform);
            ViewBag.boreholeSeal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.boreholeSeal);
            ViewBag.wellLatrine = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellLatrine);
            ViewBag.wellUphill = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellUphill);
            ViewBag.wellAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellAnimals);
            ViewBag.wellPlatform = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellPlatform);
            ViewBag.wellCover = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.wellCover);
            ViewBag.rwFirstFlush = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwFirstFlush);
            ViewBag.rwOpen = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwOpen);
            ViewBag.rwRoof = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwRoof);
            ViewBag.rwGutters = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwGutters);
            ViewBag.rwCracked = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwCracked);
            ViewBag.rwDipper = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDipper);
            ViewBag.rwLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwLeak);
            ViewBag.rwDirty = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.rwDirty);
            ViewBag.pipedStore = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedStore);
            ViewBag.pipedLeak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedLeak);
            ViewBag.pipedShare = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedShare);
            ViewBag.pipedAnimals = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedAnimals);
            ViewBag.pipedBreak = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedBreak);
            ViewBag.pipedIllegal = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedIllegal);
            ViewBag.pipedCapital = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatwpswpr.pipedCapital);
            getQuestions();
            ViewBag.watType = db.tblswatwpoverviews.Find(tblswatwpswpr.wpID).wpaType;
            return View(tblswatwpswpr);
        }

        //
        // GET: /WPSwpr/Delete/5

        //public ActionResult Delete(long id = 0)
        //{
        //    tblswatwpswpr tblswatwpswpr = db.tblswatwpswprs.Find(id);
        //    if (tblswatwpswpr == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpswpr);
        //}

        //
        // POST: /WPSwpr/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    tblswatwpswpr tblswatwpswpr = db.tblswatwpswprs.Find(id);
        //    db.tblswatwpswprs.Remove(tblswatwpswpr);
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