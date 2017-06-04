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
    public class WPSupplyController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /WPSupply/

        //public ActionResult Index()
        //{
        //    var tblswatwpsupplies = db.tblswatwpsupplies.Include(t => t.lkpswat5ranklu).Include(t => t.lkpswat5ranklu1).Include(t => t.lkpswat5ranklu2).Include(t => t.lkpswat5ranklu3).Include(t => t.lkpswat5ranklu4).Include(t => t.lkpswat5ranklu5).Include(t => t.lkpswat5ranklu6).Include(t => t.lkpswat5ranklu7).Include(t => t.lkpswat5ranklu8).Include(t => t.lkpswat5ranklu9).Include(t => t.lkpswat5ranklu10).Include(t => t.lkpswat5ranklu11).Include(t => t.lkpswat5ranklu12).Include(t => t.lkpswat5ranklu13).Include(t => t.lkpSWATcollectDangerLU).Include(t => t.lkpSWATcollectDangerLU1).Include(t => t.lkpSWATdomWaterUsesLU).Include(t => t.lkpSWATwaterCollectTimeLU).Include(t => t.lkpSWATwaterCollectTimeLU1).Include(t => t.lkpSWATwaterEffortLU).Include(t => t.lkpSWATwaterEffortLU1).Include(t => t.lkpSWATwpaReliabilityMonthLU).Include(t => t.lkpSWATwpaReliabilityMonthLU1).Include(t => t.lkpSWATwpaReliabilityMonthLU2).Include(t => t.lkpSWATwpaReliabilityMonthLU3).Include(t => t.lkpSWATwpaReliabilityMonthLU4).Include(t => t.lkpSWATwpaReliabilityMonthLU5).Include(t => t.lkpSWATwpaReliabilityMonthLU6).Include(t => t.lkpSWATwpaReliabilityMonthLU7).Include(t => t.lkpSWATwpaReliabilityMonthLU8).Include(t => t.lkpSWATwpaReliabilityMonthLU9).Include(t => t.lkpSWATwpaReliabilityMonthLU10).Include(t => t.lkpSWATwpaReliabilityMonthLU11).Include(t => t.tblswatwpoverview);
        //    return View(tblswatwpsupplies.ToList());
        //}

        //
        // GET: /WPSupply/Details/5

        //public ActionResult Details(long id = 0)
        //{
        //    tblswatwpsupply tblswatwpsupply = db.tblswatwpsupplies.Find(id);
        //    if (tblswatwpsupply == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpsupply);
        //}

        private void getQuestion()
        {
            ViewBag.Question1 = db.lkpswatwpscorelus.First(e => e.ScoreName == "conflictWaterSCORE").Description;
            ViewBag.Question2 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domPercentUseSCORE").Description;
            ViewBag.Question3 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domDemandDrySCORE1").Description;
            ViewBag.Question4 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domWaterUsesSCORE").Description;
            ViewBag.Question5 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domwaterCollectTimeSCORE").Description;
            ViewBag.Question6 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domwaterEffortSCORE").Description;
            ViewBag.Question7 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domcollectDangerSCORE").Description;
            ViewBag.Question8 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domdangerTOTALSCORE").Description;
            ViewBag.Question9 = db.lkpswatwpscorelus.First(e => e.ScoreName == "domdemoWaterFetchSCORE").Description;
            ViewBag.Question10 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInstitutionPeopleServedDrySCORE").Description;
            ViewBag.Question11 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInstitutionPeopleServedWetSCORE").Description;
            ViewBag.Question12 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInstitutionPeopleServedAvgSCORE").Description;
            ViewBag.Question13 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInstitutionSupplySCORE").Description;
            ViewBag.Question14 = db.lkpswatwpscorelus.First(e => e.ScoreName == "inwaterCollectTimeSCORE").Description;
            ViewBag.Question15 = db.lkpswatwpscorelus.First(e => e.ScoreName == "inwaterEffortSCORE").Description;
            ViewBag.Question16 = db.lkpswatwpscorelus.First(e => e.ScoreName == "incollectDangerSCORE").Description;
            ViewBag.Question17 = db.lkpswatwpscorelus.First(e => e.ScoreName == "indangerTOTALSCORE").Description;
            ViewBag.Question18 = db.lkpswatwpscorelus.First(e => e.ScoreName == "indemoWaterFetchSCORE").Description;
            ViewBag.Question19 = db.lkpswatwpscorelus.First(e => e.ScoreName == "ecWaterUseSCORE").Description;
            ViewBag.Question20 = db.lkpswatwpscorelus.First(e => e.ScoreName == "ecSupplySCORE").Description;
            ViewBag.Question21 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInterruptionFreqSCORE").Description;
            ViewBag.Question22 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaInterruptionSCORE").Description;
            ViewBag.Question23 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilitySCORETOTAL").Description;
            ViewBag.Month1 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE1").Description;
            ViewBag.Month2 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE2").Description;
            ViewBag.Month3 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE3").Description;
            ViewBag.Month4 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE4").Description;
            ViewBag.Month5 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE5").Description;
            ViewBag.Month6 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE6").Description;
            ViewBag.Month7 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE7").Description;
            ViewBag.Month8 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE8").Description;
            ViewBag.Month9 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE9").Description;
            ViewBag.Month10 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE10").Description;
            ViewBag.Month11 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE11").Description;
            ViewBag.Month12 = db.lkpswatwpscorelus.First(e => e.ScoreName == "wpaReliabilityMonthSCORE12").Description;

        }

        private void updateScores(tblswatwpsupply tblswatwpsupply)
        {
            if (tblswatwpsupply.conflictWater != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatwpsupply.conflictWater).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "conflictWaterSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "conflictWaterSCORE").Value = null;
            }

            if (tblswatwpsupply.domPercentUse != null)
            {
                double score = (double)tblswatwpsupply.domPercentUse / 100;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domPercentUseSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domPercentUseSCORE").Value = null;
            }

            var wpsurvey = db.tblswatwpsupplies.Include(t => t.tblswatwpoverview);
            int sid = wpsurvey.First(e => e.wpID == tblswatwpsupply.wpID).tblswatwpoverview.SurveyID;
            int hh = (int)db.tblswatbackgroundinfoes.First(e => e.SurveyID == sid).numHouseholds;
            /*
            if (tblswatwpsupply.domCollectDry1 != null && tblswatwpsupply.domCollectDryAmount1 != null)
            {
                double score = (double)tblswatwpsupply.domCollectDry1 * (double)tblswatwpsupply.domCollectDryAmount1 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE1").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE1").Value = null;
            }

            if (tblswatwpsupply.domCollectDry2 != null && tblswatwpsupply.domCollectDryAmount2 != null)
            {
                double score = (double)tblswatwpsupply.domCollectDry2 * (double)tblswatwpsupply.domCollectDryAmount2 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE2").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE2").Value = null;
            }

            if (tblswatwpsupply.domCollectDry3 != null && tblswatwpsupply.domCollectDryAmount3 != null)
            {
                double score = (double)tblswatwpsupply.domCollectDry3 * (double)tblswatwpsupply.domCollectDryAmount3 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE3").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE3").Value = null;
            }

            if (tblswatwpsupply.domCollectWet1 != null && tblswatwpsupply.domCollectWetAmount1 != null)
            {
                double score = (double)tblswatwpsupply.domCollectWet1 * (double)tblswatwpsupply.domCollectWetAmount1 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE1").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE1").Value = null;
            }

            if (tblswatwpsupply.domCollectWet2 != null && tblswatwpsupply.domCollectWetAmount2 != null)
            {
                double score = (double)tblswatwpsupply.domCollectWet2 * (double)tblswatwpsupply.domCollectWetAmount2 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE2").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE2").Value = null;
            }

            if (tblswatwpsupply.domCollectWet3 != null && tblswatwpsupply.domCollectWetAmount3 != null)
            {
                double score = (double)tblswatwpsupply.domCollectWet3 * (double)tblswatwpsupply.domCollectWetAmount3 * hh;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE3").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE3").Value = null;
            }
             */
            // New algorithms for domDemandScores

            
            if (tblswatwpsupply.domCollectDry1 != null && tblswatwpsupply.domCollectDryAmount1 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectDryAmount1);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE1").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE1").Value = null;
            }

            if (tblswatwpsupply.domCollectDry2 != null && tblswatwpsupply.domCollectDryAmount2 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectDryAmount2);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE2").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE2").Value = null;
            }

            if (tblswatwpsupply.domCollectDry3 != null && tblswatwpsupply.domCollectDryAmount3 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectDryAmount3);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE3").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandDrySCORE3").Value = null;
            }

            if (tblswatwpsupply.domCollectWet1 != null && tblswatwpsupply.domCollectWetAmount1 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectWetAmount1);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE1").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE1").Value = null;
            }

            if (tblswatwpsupply.domCollectWet2 != null && tblswatwpsupply.domCollectWetAmount2 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectWetAmount2);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE2").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE2").Value = null;
            }

            if (tblswatwpsupply.domCollectWet3 != null && tblswatwpsupply.domCollectWetAmount3 != null)
            {
                double score = getDemandScore(tblswatwpsupply.domCollectWetAmount3);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE3").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domDemandWetSCORE3").Value = null;
            }

            if (tblswatwpsupply.domWaterUses != null)
            {
                int intorder = (int)db.lkpswatdomwateruseslus.Find(tblswatwpsupply.domWaterUses).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_domwateruses.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domWaterUsesSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domWaterUsesSCORE").Value = null;
            }

            if (tblswatwpsupply.tblswatwpoverview.wpaType != null)
            {
                int intorder = (int)db.lkpswateaseuselus.First(e => e.Description == tblswatwpsupply.tblswatwpoverview.wpaType).intorder;
                //double score = Convert.ToDouble(db.lkpSWATscores_easeUse.First(e => e.intorder == intorder).Description);
                double? score = null;
                if (intorder == 1)
                {
                    score = 1;
                }
                else if (intorder == 2)
                {
                    score = 0.75;
                }
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "easeUseSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "easeUseSCORE").Value = null;
            }

            if (tblswatwpsupply.domwaterCollectTime != null)
            {
                int intorder = (int)db.lkpswatwatercollecttimelus.Find(tblswatwpsupply.domwaterCollectTime).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watercollecttime.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domwaterCollectTimeSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domwaterCollectTimeSCORE").Value = null;
            }

            if (tblswatwpsupply.domwaterEffort != null)
            {
                int intorder = (int)db.lkpswatwatereffortlus.Find(tblswatwpsupply.domwaterEffort).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watereffort.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domwaterEffortSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domwaterEffortSCORE").Value = null;
            }

            if (tblswatwpsupply.domcollectDanger != null)
            {
                int intorder = (int)db.lkpswatcollectdangerlus.Find(tblswatwpsupply.domcollectDanger).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_collectdanger.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domcollectDangerSCORE").Value = score;

                if (score > 0)
                {
                    bool[] dangers = { tblswatwpsupply.domdangerType1, tblswatwpsupply.domdangerType2, tblswatwpsupply.domdangerType3 };
                    int dangerCounter = 0;
                    foreach (bool item in dangers)
                    {
                        if (item)
                        {
                            dangerCounter++;
                        }
                    }
                    double dangerScore = 1 - (double)dangerCounter / 3;
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domdangerTOTALSCORE").Value = dangerScore;
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domdangerTOTALSCORE").Value = null;
                }
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domcollectDangerSCORE").Value = null;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "domdangerTOTALSCORE").Value = null;
            }

            if (tblswatwpsupply.wpaInstitutionSupply != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatwpsupply.wpaInstitutionSupply).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaInstitutionSupplySCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaInstitutionSupplySCORE").Value = null;
            }

            if (tblswatwpsupply.inwaterCollectTime != null)
            {
                int intorder = (int)db.lkpswatwatercollecttimelus.Find(tblswatwpsupply.inwaterCollectTime).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watercollecttime.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "inwaterCollectTimeSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "inwaterCollectTimeSCORE").Value = null;
            }

            if (tblswatwpsupply.inwaterEffort != null)
            {
                int intorder = (int)db.lkpswatwatereffortlus.Find(tblswatwpsupply.inwaterEffort).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_watereffort.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "inwaterEffortSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "inwaterEffortSCORE").Value = null;
            }

            if (tblswatwpsupply.incollectDanger != null)
            {
                int intorder = (int)db.lkpswatcollectdangerlus.Find(tblswatwpsupply.incollectDanger).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_collectdanger.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "incollectDangerSCORE").Value = score;

                if (score > 0)
                {
                    bool[] dangers = { tblswatwpsupply.indangerType1, tblswatwpsupply.indangerType2, tblswatwpsupply.indangerType3 };
                    int dangerCounter = 0;
                    foreach (bool item in dangers)
                    {
                        if (item)
                        {
                            dangerCounter++;
                        }
                    }
                    double dangerScore = 1 - (double)dangerCounter / 3;
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "indangerTOTALSCORE").Value = dangerScore;
                }
                else
                {
                    db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "indangerTOTALSCORE").Value = null;
                }
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "incollectDangerSCORE").Value = null;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "indangerTOTALSCORE").Value = null;
            }

            if (tblswatwpsupply.ecSupply != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatwpsupply.ecSupply).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "ecSupplySCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "ecSupplySCORE").Value = null;
            }

            if (tblswatwpsupply.wpaInterruptionFreq != null)
            {
                int intorder = (int)db.lkpswat5ranklu.Find(tblswatwpsupply.wpaInterruptionFreq).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_5rankalwaysbad.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaInterruptionFreqSCORE").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaInterruptionFreqSCORE").Value = null;
            }

            double? wpaReliability = null;
            int wpaRcounter = 0;

            if (tblswatwpsupply.wpaReliabilityMonth1 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth1).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE1").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE1").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth2 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth2).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE2").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE2").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth3 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth3).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE3").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE3").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth4 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth4).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE4").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE4").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth5 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth5).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE5").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE5").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth6 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth6).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE6").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE6").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth7 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth7).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE7").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE7").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth8 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth8).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE8").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE8").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth9 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth9).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE9").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE9").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth10 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth10).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE10").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE10").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth11 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth11).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE11").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE11").Value = null;
            }

            if (tblswatwpsupply.wpaReliabilityMonth12 != null)
            {
                int intorder = (int)db.lkpswatwpareliabilitymonthlus.Find(tblswatwpsupply.wpaReliabilityMonth12).intorder;
                double score = Convert.ToDouble(db.lkpswatscores_wpareliabilitymonth.First(e => e.intorder == intorder).Description);
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE12").Value = score;
                wpaReliability = wpaReliability.GetValueOrDefault(0) + score;
                wpaRcounter++;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilityMonthSCORE12").Value = null;
            }

            if (wpaRcounter > 0)
            {
                double score = (double)wpaReliability / wpaRcounter;
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilitySCORETOTAL").Value = score;
            }
            else
            {
                db.tblswatwpscores.First(e => e.wpID == tblswatwpsupply.wpID && e.ScoreName == "wpaReliabilitySCORETOTAL").Value = null;
            }

            db.SaveChanges();
        }

        private double getDemandScore(double? demandAmount)
        {
            double score;
            if (demandAmount > 150)
            {
                score = 0.25;
            }
            else if (demandAmount > 60)
            {
                score = 0.1;
            }
            else if (demandAmount > 20)
            {
                score = 0.75;
            }
            else if (demandAmount == 20)
            {
                score = 0.5;
            }
            else
            {
                score = 0;
            }
            return score;
        }

        private void showForm(long wpID)
        {
            tblswatwpoverview wpt = db.tblswatwpoverviews.Find(wpID);
            ViewBag.notPB = !(wpt.wpaType.Equals("piped") || wpt.wpaType.Equals("bottled"));
            ViewBag.dom = wpt.wpaUseDom;
            ViewBag.inst = wpt.wpaUseInst;
            ViewBag.eco = wpt.wpaUseEc;
        }

        //
        // GET: /WPSupply/Create

        public ActionResult Create(long? wpID)
        {
            if (wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.conflictWater = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domdemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domdemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domdemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domdemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domdemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.wpaInstitutionSupply = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.indemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.indemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.indemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.indemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.indemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.ecSupply = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.wpaInterruptionFreq = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.domcollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description");
            ViewBag.incollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description");
            ViewBag.domWaterUses = new SelectList(db.lkpswatdomwateruseslus, "id", "Description");
            ViewBag.domwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description");
            ViewBag.inwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description");
            ViewBag.domwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description");
            ViewBag.inwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description");
            ViewBag.wpaReliabilityMonth1 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth2 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth3 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth4 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth5 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth6 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth7 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth8 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth9 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth10 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth11 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");
            ViewBag.wpaReliabilityMonth12 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description");

            getQuestion();

            showForm((long)wpID);

            return View();
        }

        //
        // POST: /WPSupply/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatwpsupply tblswatwpsupply, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var recordIDs = db.tblswatwpsupplies.Where(e => e.wpID == tblswatwpsupply.wpID).Select(e => e.ID);
                if (recordIDs.Any())
                {
                    long recordId = recordIDs.First();
                    tblswatwpsupply.ID = recordId;
                    db.Entry(tblswatwpsupply).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatwpsupplies.Add(tblswatwpsupply);
                }
                db.SaveChanges();
                updateScores(tblswatwpsupply);

                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WPQuality", new { wpID = tblswatwpsupply.ID });
                }
            }

            ViewBag.conflictWater = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.conflictWater);
            ViewBag.domdemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch1);
            ViewBag.domdemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch2);
            ViewBag.domdemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch3);
            ViewBag.domdemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch4);
            ViewBag.domdemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch5);
            ViewBag.wpaInstitutionSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInstitutionSupply);
            ViewBag.indemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch1);
            ViewBag.indemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch2);
            ViewBag.indemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch3);
            ViewBag.indemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch4);
            ViewBag.indemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch5);
            ViewBag.ecSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.ecSupply);
            ViewBag.wpaInterruptionFreq = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInterruptionFreq);
            ViewBag.domcollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.domcollectDanger);
            ViewBag.incollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.incollectDanger);
            ViewBag.domWaterUses = new SelectList(db.lkpswatdomwateruseslus, "id", "Description", tblswatwpsupply.domWaterUses);
            ViewBag.domwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.domwaterCollectTime);
            ViewBag.inwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.inwaterCollectTime);
            ViewBag.domwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.domwaterEffort);
            ViewBag.inwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.inwaterEffort);
            ViewBag.wpaReliabilityMonth1 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth1);
            ViewBag.wpaReliabilityMonth2 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth2);
            ViewBag.wpaReliabilityMonth3 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth3);
            ViewBag.wpaReliabilityMonth4 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth4);
            ViewBag.wpaReliabilityMonth5 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth5);
            ViewBag.wpaReliabilityMonth6 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth6);
            ViewBag.wpaReliabilityMonth7 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth7);
            ViewBag.wpaReliabilityMonth8 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth8);
            ViewBag.wpaReliabilityMonth9 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth9);
            ViewBag.wpaReliabilityMonth10 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth10);
            ViewBag.wpaReliabilityMonth11 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth11);
            ViewBag.wpaReliabilityMonth12 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth12);

            getQuestion();

            showForm(tblswatwpsupply.wpID);

            return View(tblswatwpsupply);
        }

        //
        // GET: /WPSupply/Edit/5

        public ActionResult Edit(long? id, long? wpID)
        {
            if (id == null || wpID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatwpsupply tblswatwpsupply = db.tblswatwpsupplies.Find(id);
            if (tblswatwpsupply == null)
            {
                return HttpNotFound();
            }
            ViewBag.conflictWater = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.conflictWater);
            ViewBag.domdemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch1);
            ViewBag.domdemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch2);
            ViewBag.domdemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch3);
            ViewBag.domdemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch4);
            ViewBag.domdemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch5);
            ViewBag.wpaInstitutionSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInstitutionSupply);
            ViewBag.indemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch1);
            ViewBag.indemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch2);
            ViewBag.indemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch3);
            ViewBag.indemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch4);
            ViewBag.indemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch5);
            ViewBag.ecSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.ecSupply);
            ViewBag.wpaInterruptionFreq = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInterruptionFreq);
            ViewBag.domcollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.domcollectDanger);
            ViewBag.incollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.incollectDanger);
            ViewBag.domWaterUses = new SelectList(db.lkpswatdomwateruseslus, "id", "Description", tblswatwpsupply.domWaterUses);
            ViewBag.domwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.domwaterCollectTime);
            ViewBag.inwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.inwaterCollectTime);
            ViewBag.domwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.domwaterEffort);
            ViewBag.inwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.inwaterEffort);
            ViewBag.wpaReliabilityMonth1 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth1);
            ViewBag.wpaReliabilityMonth2 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth2);
            ViewBag.wpaReliabilityMonth3 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth3);
            ViewBag.wpaReliabilityMonth4 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth4);
            ViewBag.wpaReliabilityMonth5 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth5);
            ViewBag.wpaReliabilityMonth6 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth6);
            ViewBag.wpaReliabilityMonth7 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth7);
            ViewBag.wpaReliabilityMonth8 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth8);
            ViewBag.wpaReliabilityMonth9 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth9);
            ViewBag.wpaReliabilityMonth10 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth10);
            ViewBag.wpaReliabilityMonth11 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth11);
            ViewBag.wpaReliabilityMonth12 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth12);

            getQuestion();
            showForm((long)wpID);

            return View(tblswatwpsupply);
        }

        //
        // POST: /WPSupply/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatwpsupply tblswatwpsupply, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatwpsupply).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatwpsupply);

                if (submitBtn.Equals("Next"))
                {
                    var records = db.tblswatwpqualities.Where(e => e.wpID == tblswatwpsupply.wpID);
                    if (!records.Any())
                    {
                        tblswatwpquality newEntry = new tblswatwpquality();
                        newEntry.wpID = tblswatwpsupply.wpID;
                        db.tblswatwpqualities.Add(newEntry);
                        db.SaveChanges();

                        long newId = newEntry.ID;
                        return RedirectToAction("Edit", "WPQuality", new { id = newId, wpID = newEntry.wpID });
                    }

                    return RedirectToAction("Edit", "WPQuality", new { id = records.First(e => e.wpID == tblswatwpsupply.wpID).ID, wpID = tblswatwpsupply.wpID });
                }
            }
            ViewBag.conflictWater = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.conflictWater);
            ViewBag.domdemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch1);
            ViewBag.domdemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch2);
            ViewBag.domdemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch3);
            ViewBag.domdemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch4);
            ViewBag.domdemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.domdemoWaterFetch5);
            ViewBag.wpaInstitutionSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInstitutionSupply);
            ViewBag.indemoWaterFetch1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch1);
            ViewBag.indemoWaterFetch2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch2);
            ViewBag.indemoWaterFetch3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch3);
            ViewBag.indemoWaterFetch4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch4);
            ViewBag.indemoWaterFetch5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.indemoWaterFetch5);
            ViewBag.ecSupply = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.ecSupply);
            ViewBag.wpaInterruptionFreq = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatwpsupply.wpaInterruptionFreq);
            ViewBag.domcollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.domcollectDanger);
            ViewBag.incollectDanger = new SelectList(db.lkpswatcollectdangerlus, "id", "Description", tblswatwpsupply.incollectDanger);
            ViewBag.domWaterUses = new SelectList(db.lkpswatdomwateruseslus, "id", "Description", tblswatwpsupply.domWaterUses);
            ViewBag.domwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.domwaterCollectTime);
            ViewBag.inwaterCollectTime = new SelectList(db.lkpswatwatercollecttimelus, "id", "Description", tblswatwpsupply.inwaterCollectTime);
            ViewBag.domwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.domwaterEffort);
            ViewBag.inwaterEffort = new SelectList(db.lkpswatwatereffortlus, "id", "Description", tblswatwpsupply.inwaterEffort);
            ViewBag.wpaReliabilityMonth1 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth1);
            ViewBag.wpaReliabilityMonth2 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth2);
            ViewBag.wpaReliabilityMonth3 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth3);
            ViewBag.wpaReliabilityMonth4 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth4);
            ViewBag.wpaReliabilityMonth5 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth5);
            ViewBag.wpaReliabilityMonth6 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth6);
            ViewBag.wpaReliabilityMonth7 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth7);
            ViewBag.wpaReliabilityMonth8 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth8);
            ViewBag.wpaReliabilityMonth9 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth9);
            ViewBag.wpaReliabilityMonth10 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth10);
            ViewBag.wpaReliabilityMonth11 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth11);
            ViewBag.wpaReliabilityMonth12 = new SelectList(db.lkpswatwpareliabilitymonthlus, "id", "Description", tblswatwpsupply.wpaReliabilityMonth12);

            getQuestion();
            showForm(tblswatwpsupply.wpID);
            return View(tblswatwpsupply);
        }

        //
        // GET: /WPSupply/Delete/5

        //public ActionResult Delete(long id = 0)
        //{
        //    tblswatwpsupply tblswatwpsupply = db.tblswatwpsupplies.Find(id);
        //    if (tblswatwpsupply == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatwpsupply);
        //}

        //
        // POST: /WPSupply/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(long id)
        //{
        //    tblswatwpsupply tblswatwpsupply = db.tblswatwpsupplies.Find(id);
        //    db.tblswatwpsupplies.Remove(tblswatwpsupply);
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