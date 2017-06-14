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
    public class CCWaterManagementController : Controller
    {
        private SWATEntities db = new SWATEntities();

        //
        // GET: /CCWaterManagement/

        //public ActionResult Index()
        //{
        //    var tblswatccwatermanagements = db.tblSWATCCwaterManagements.Include(t => t.lkpSWAT5rankLU).Include(t => t.lkpSWAT5rankLU1).Include(t => t.lkpSWAT5rankLU2).Include(t => t.lkpSWAT5rankLU3).Include(t => t.lkpSWAT5rankLU4).Include(t => t.lkpSWATcomSatisfactionLU).Include(t => t.lkpSWATproPoorLU).Include(t => t.lkpSWATwatActionPlanLU).Include(t => t.lkpSWATwatBudgetLU).Include(t => t.lkpSWATwatClassRepLU).Include(t => t.lkpSWATwatComConcernsLU).Include(t => t.lkpSWATwatComLU).Include(t => t.lkpSWATwatTechStaffLU).Include(t => t.lkpSWATwatTechTrainingLU).Include(t => t.tblSWATSurvey);
        //    return View(tblswatccwatermanagements.ToList());
        //}

        //
        // GET: /CCWaterManagement/Details/5

        //public ActionResult Details(int id = 0)
        //{
        //    tblSWATCCwaterManagement tblswatccwatermanagement = db.tblSWATCCwaterManagements.Find(id);
        //    if (tblswatccwatermanagement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccwatermanagement);
        //}

        //
        // GET: /CCWaterManagement/Create

        public ActionResult Create(int? SurveyID)
        {
            if (SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.watRecords1 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.watRecords2 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.watRecords3 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.watRecords4 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.watRecords5 = new SelectList(db.lkpswat5ranklu, "id", "Description");
            ViewBag.comSatisfaction = new SelectList(db.lkpswatcomsatisfactionlus, "id", "Description");
            ViewBag.proPoor = new SelectList(db.lkpswatpropoorlus, "id", "Description");
            ViewBag.watActionPlan = new SelectList(db.lkpswatwatactionplanlus, "id", "Description");
            ViewBag.watBudget = new SelectList(db.lkpswatwatbudgetlus, "id", "Description");
            ViewBag.watClassRep = new SelectList(db.lkpswatwatclassreplus, "id", "Description");
            ViewBag.watComConcerns = new SelectList(db.lkpswatwatcomconcernslus, "id", "Description");
            ViewBag.watCom = new SelectList(db.lkpswatwatcomlus, "id", "Description");
            ViewBag.watTechStaff = new SelectList(db.lkpswatwattechstafflus, "id", "Description");
            ViewBag.watTechTraining = new SelectList(db.lkpswatwattechtraininglus, "id", "Description");

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "watComSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "watActionPlanSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "watRecordsSCORETOTAL").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "comSatisfactionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "watClassRepSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "watComConcernsSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechStaffSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechTrainingSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "watBudgetSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "watFinPlanSCORE").Description;
            ViewBag.Question11 = db.lkpswatscorevarslus.First(e => e.VarName == "proPoorSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View();
        }

        private void updateScores(tblswatccwatermanagement tblswatccwatermanagement)
        {
            if (tblswatccwatermanagement.watCom != null)
            {
                int watComOrder = (int)db.lkpswatwatcomlus.Find(tblswatccwatermanagement.watCom).intorder;
                double watComScore = Convert.ToDouble(db.lkpswatscores_watcom.First(e => e.intorder == watComOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watComSCORE").Value = watComScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watComSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watActionPlan != null)
            {
                int watActionOrder = (int)db.lkpswatwatactionplanlus.Find(tblswatccwatermanagement.watActionPlan).intorder;
                double watActionScore = Convert.ToDouble(db.lkpswatscores_watactionplan.First(e => e.intorder == watActionOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watActionPlanSCORE").Value = watActionScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watActionPlanSCORE").Value = null;
            }

            int recordCounter = 0;
            double recordScore = 0;

            if (tblswatccwatermanagement.watRecords1 != null)
            {
                int watRecordOrder = (int)db.lkpswat5ranklu.Find(tblswatccwatermanagement.watRecords1).intorder;
                double watRecordScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == watRecordOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE1").Value = watRecordScore;
                recordCounter++;
                recordScore = recordScore + watRecordScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE1").Value = null;
            }

            if (tblswatccwatermanagement.watRecords2 != null)
            {
                int watRecordOrder = (int)db.lkpswat5ranklu.Find(tblswatccwatermanagement.watRecords2).intorder;
                double watRecordScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == watRecordOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE2").Value = watRecordScore;
                recordCounter++;
                recordScore = recordScore + watRecordScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE2").Value = null;
            }

            if (tblswatccwatermanagement.watRecords3 != null)
            {
                int watRecordOrder = (int)db.lkpswat5ranklu.Find(tblswatccwatermanagement.watRecords3).intorder;
                double watRecordScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == watRecordOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE3").Value = watRecordScore;
                recordCounter++;
                recordScore = recordScore + watRecordScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE3").Value = null;
            }

            if (tblswatccwatermanagement.watRecords4 != null)
            {
                int watRecordOrder = (int)db.lkpswat5ranklu.Find(tblswatccwatermanagement.watRecords4).intorder;
                double watRecordScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == watRecordOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE4").Value = watRecordScore;
                recordCounter++;
                recordScore = recordScore + watRecordScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE4").Value = null;
            }

            if (tblswatccwatermanagement.watRecords5 != null)
            {
                int watRecordOrder = (int)db.lkpswat5ranklu.Find(tblswatccwatermanagement.watRecords5).intorder;
                double watRecordScore = Convert.ToDouble(db.lkpswatscores_5rankalwaysgood.First(e => e.intorder == watRecordOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE5").Value = watRecordScore;
                recordCounter++;
                recordScore = recordScore + watRecordScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORE5").Value = null;
            }

            if (recordCounter > 0)
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORETOTAL").Value = recordScore / recordCounter;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watRecordsSCORETOTAL").Value = null;
            }

            if (tblswatccwatermanagement.comSatisfaction != null)
            {
                int comOrder = (int)db.lkpswatcomsatisfactionlus.Find(tblswatccwatermanagement.comSatisfaction).intorder;
                double comScore = Convert.ToDouble(db.lkpswatscores_comsatisfaction.First(e => e.intorder == comOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "comSatisfactionSCORE").Value = comScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "comSatisfactionSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watClassRep != null)
            {
                int repOrder = (int)db.lkpswatwatclassreplus.Find(tblswatccwatermanagement.watClassRep).intorder;
                double repScore = Convert.ToDouble(db.lkpswatscores_watclassrep.First(e => e.intorder == repOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watClassRepSCORE").Value = repScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watClassRepSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watComConcerns != null)
            {
                int concerOrder = (int)db.lkpswatwatcomconcernslus.Find(tblswatccwatermanagement.watComConcerns).intorder;
                double concerScore = Convert.ToDouble(db.lkpswatscores_watcomconcerns.First(e => e.intorder == concerOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watComConcernsSCORE").Value = concerScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watComConcernsSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watTechStaff != null)
            {
                int techOrder = (int)db.lkpswatwattechstafflus.Find(tblswatccwatermanagement.watTechStaff).intorder;
                double techScore = Convert.ToDouble(db.lkpswatscores_wattechstaff.First(e => e.intorder == techOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watTechStaffSCORE").Value = techScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watTechStaffSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watTechTraining != null)
            {
                int trainOrder = (int)db.lkpswatwattechtraininglus.Find(tblswatccwatermanagement.watTechTraining).intorder;
                double trainScore = Convert.ToDouble(db.lkpswatscores_wattechtraining.First(e => e.intorder == trainOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watTechTrainingSCORE").Value = trainScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watTechTrainingSCORE").Value = null;
            }

            if (tblswatccwatermanagement.watBudget != null)
            {
                int budgetOrder = (int)db.lkpswatwatbudgetlus.Find(tblswatccwatermanagement.watBudget).intorder;
                double budgetScore = Convert.ToDouble(db.lkpswatscores_watbudget.First(e => e.intorder == budgetOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watBudgetSCORE").Value = budgetScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watBudgetSCORE").Value = null;
            }

            bool[] watFin = { tblswatccwatermanagement.watFinPlan1, tblswatccwatermanagement.watFinPlan2};
            int watFinCounter = 0;

            foreach (bool item in watFin)
            {
                if (item)
                {
                    watFinCounter++;
                }
            }
            double watFinScore = (double)watFinCounter / 2.0;
            db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "watFinPlanSCORE").Value = watFinScore;

            if (tblswatccwatermanagement.proPoor != null)
            {
                int proOrder = (int)db.lkpswatpropoorlus.Find(tblswatccwatermanagement.proPoor).intorder;
                double proScore = Convert.ToDouble(db.lkpswatscores_propoor.First(e => e.intorder == proOrder).Description);
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "proPoorSCORE").Value = proScore;
            }
            else
            {
                db.tblswatscores.Single(e => e.SurveyID == tblswatccwatermanagement.SurveyID && e.VarName == "proPoorSCORE").Value = null;
            }

            db.SaveChanges();
            
        }

        //
        // POST: /CCWaterManagement/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblswatccwatermanagement tblswatccwatermanagement, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                var waterManagementIDs = db.tblswatccwatermanagements.Where(e => e.SurveyID == tblswatccwatermanagement.SurveyID).Select(e => e.ID);
                if (waterManagementIDs.Any())
                {
                    int waterManagementID = waterManagementIDs.First();
                    tblswatccwatermanagement.ID = waterManagementID;
                    db.Entry(tblswatccwatermanagement).State = EntityState.Modified;
                }
                else
                {
                    db.tblswatccwatermanagements.Add(tblswatccwatermanagement);
                }
                db.SaveChanges();
                updateScores(tblswatccwatermanagement);
                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "SWPLivestock", new { SurveyID = tblswatccwatermanagement.SurveyID });
                }
            }

            ViewBag.watRecords1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords1);
            ViewBag.watRecords2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords2);
            ViewBag.watRecords3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords3);
            ViewBag.watRecords4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords4);
            ViewBag.watRecords5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords5);
            ViewBag.comSatisfaction = new SelectList(db.lkpswatcomsatisfactionlus, "id", "Description", tblswatccwatermanagement.comSatisfaction);
            ViewBag.proPoor = new SelectList(db.lkpswatpropoorlus, "id", "Description", tblswatccwatermanagement.proPoor);
            ViewBag.watActionPlan = new SelectList(db.lkpswatwatactionplanlus, "id", "Description", tblswatccwatermanagement.watActionPlan);
            ViewBag.watBudget = new SelectList(db.lkpswatwatbudgetlus, "id", "Description", tblswatccwatermanagement.watBudget);
            ViewBag.watClassRep = new SelectList(db.lkpswatwatclassreplus, "id", "Description", tblswatccwatermanagement.watClassRep);
            ViewBag.watComConcerns = new SelectList(db.lkpswatwatcomconcernslus, "id", "Description", tblswatccwatermanagement.watComConcerns);
            ViewBag.watCom = new SelectList(db.lkpswatwatcomlus, "id", "Description", tblswatccwatermanagement.watCom);
            ViewBag.watTechStaff = new SelectList(db.lkpswatwattechstafflus, "id", "Description", tblswatccwatermanagement.watTechStaff);
            ViewBag.watTechTraining = new SelectList(db.lkpswatwattechtraininglus, "id", "Description", tblswatccwatermanagement.watTechTraining);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "watComSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "watActionPlanSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "watRecordsSCORETOTAL").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "comSatisfactionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "watClassRepSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "watComConcernsSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechStaffSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechTrainingSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "watBudgetSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "watFinPlanSCORE").Description;
            ViewBag.Question11 = db.lkpswatscorevarslus.First(e => e.VarName == "proPoorSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatccwatermanagement.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID;
            ViewBag.SurveyID = tblswatccwatermanagement.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccwatermanagement);
        }

        //
        // GET: /CCWaterManagement/Edit/5

        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatccwatermanagement tblswatccwatermanagement = db.tblswatccwatermanagements.Find(id);
            if (tblswatccwatermanagement == null)
            {
                return HttpNotFound();
            }
            ViewBag.watRecords1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords1);
            ViewBag.watRecords2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords2);
            ViewBag.watRecords3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords3);
            ViewBag.watRecords4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords4);
            ViewBag.watRecords5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords5);
            ViewBag.comSatisfaction = new SelectList(db.lkpswatcomsatisfactionlus, "id", "Description", tblswatccwatermanagement.comSatisfaction);
            ViewBag.proPoor = new SelectList(db.lkpswatpropoorlus, "id", "Description", tblswatccwatermanagement.proPoor);
            ViewBag.watActionPlan = new SelectList(db.lkpswatwatactionplanlus, "id", "Description", tblswatccwatermanagement.watActionPlan);
            ViewBag.watBudget = new SelectList(db.lkpswatwatbudgetlus, "id", "Description", tblswatccwatermanagement.watBudget);
            ViewBag.watClassRep = new SelectList(db.lkpswatwatclassreplus, "id", "Description", tblswatccwatermanagement.watClassRep);
            ViewBag.watComConcerns = new SelectList(db.lkpswatwatcomconcernslus, "id", "Description", tblswatccwatermanagement.watComConcerns);
            ViewBag.watCom = new SelectList(db.lkpswatwatcomlus, "id", "Description", tblswatccwatermanagement.watCom);
            ViewBag.watTechStaff = new SelectList(db.lkpswatwattechstafflus, "id", "Description", tblswatccwatermanagement.watTechStaff);
            ViewBag.watTechTraining = new SelectList(db.lkpswatwattechtraininglus, "id", "Description", tblswatccwatermanagement.watTechTraining);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "watComSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "watActionPlanSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "watRecordsSCORETOTAL").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "comSatisfactionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "watClassRepSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "watComConcernsSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechStaffSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechTrainingSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "watBudgetSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "watFinPlanSCORE").Description;
            ViewBag.Question11 = db.lkpswatscorevarslus.First(e => e.VarName == "proPoorSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == SurveyID).ID;
            ViewBag.SurveyID = SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccwatermanagement);
        }

        //
        // POST: /CCWaterManagement/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(tblswatccwatermanagement tblswatccwatermanagement, string submitBtn)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblswatccwatermanagement).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatccwatermanagement);

                if (submitBtn.Equals("Next"))
                {
                    var background = db.tblswatbackgroundinfoes.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID);
                    if (background != null)
                    {
                        // id = 1511 is the "Yes" option in database
                        if (background.isEconLs == 1511)
                        {
                            // TODO redirect to livestock form
                            var swpls = db.tblswatswpls.Where(e => e.SurveyID == tblswatccwatermanagement.SurveyID);
                            if (!swpls.Any())
                            {
                                tblswatswpl tblswatswpls = new tblswatswpl();
                                tblswatswpls.SurveyID = tblswatccwatermanagement.SurveyID;
                                db.tblswatswpls.Add(tblswatswpls);
                                db.SaveChanges();

                                int newSWPlsID = tblswatswpls.ID;
                                return RedirectToAction("Edit", "SWPLivestock", new { id = newSWPlsID, SurveyID = tblswatswpls.SurveyID });
                            }
                            return RedirectToAction("Edit", "SWPLivestock", new { id = swpls.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID, SurveyID = tblswatccwatermanagement.SurveyID });
                        }
                        else if (background.isEconAg == 1511)
                        {
                            // TODO redirect to ag form
                            var swpag = db.tblswatswpags.Where(e => e.SurveyID == tblswatccwatermanagement.SurveyID);
                            if (!swpag.Any())
                            {
                                tblswatswpag tblswatswpag = new tblswatswpag();
                                tblswatswpag.SurveyID = tblswatccwatermanagement.SurveyID;
                                db.tblswatswpags.Add(tblswatswpag);
                                db.SaveChanges();

                                int newSWPagID = tblswatswpag.ID;
                                return RedirectToAction("Edit", "SWPAg", new { id = newSWPagID, SurveyID = tblswatswpag.SurveyID });
                            }
                            return RedirectToAction("Edit", "SWPAg", new { id = swpag.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID, SurveyID = tblswatccwatermanagement.SurveyID });
                        }
                        else if (background.isEconDev == 1511)
                        {
                            // TODO redirect to dev form
                            var swpdev = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatccwatermanagement.SurveyID);
                            if (!swpdev.Any())
                            {
                                tblswatswpdev tblswatswpdev = new tblswatswpdev();
                                tblswatswpdev.SurveyID = tblswatccwatermanagement.SurveyID;
                                db.tblswatswpdevs.Add(tblswatswpdev);
                                db.SaveChanges();

                                int newSWPdevID = tblswatswpdev.ID;
                                return RedirectToAction("Edit", "SWPDev", new { id = newSWPdevID, SurveyID = tblswatswpdev.SurveyID });
                            }
                            return RedirectToAction("Edit", "SWPDev", new { id = swpdev.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID, SurveyID = tblswatccwatermanagement.SurveyID });
                        }
                    }
                    // TODO redirect to health form
                    var hppcom = db.tblswathppcoms.Where(e => e.SurveyID == tblswatccwatermanagement.SurveyID);
                    if (!hppcom.Any())
                    {
                        tblswathppcom tblswathppcom = new tblswathppcom();
                        tblswathppcom.SurveyID = tblswatccwatermanagement.SurveyID;
                        db.tblswathppcoms.Add(tblswathppcom);
                        db.SaveChanges();

                        int newHPPcomID = tblswathppcom.ID;
                        return RedirectToAction("Edit", "HPPCom", new { id = newHPPcomID, SurveyID = tblswathppcom.SurveyID });
                    }
                    return RedirectToAction("Edit", "HPPCom", new { id = hppcom.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID, SurveyID = tblswatccwatermanagement.SurveyID });
                }
            }
            ViewBag.watRecords1 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords1);
            ViewBag.watRecords2 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords2);
            ViewBag.watRecords3 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords3);
            ViewBag.watRecords4 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords4);
            ViewBag.watRecords5 = new SelectList(db.lkpswat5ranklu, "id", "Description", tblswatccwatermanagement.watRecords5);
            ViewBag.comSatisfaction = new SelectList(db.lkpswatcomsatisfactionlus, "id", "Description", tblswatccwatermanagement.comSatisfaction);
            ViewBag.proPoor = new SelectList(db.lkpswatpropoorlus, "id", "Description", tblswatccwatermanagement.proPoor);
            ViewBag.watActionPlan = new SelectList(db.lkpswatwatactionplanlus, "id", "Description", tblswatccwatermanagement.watActionPlan);
            ViewBag.watBudget = new SelectList(db.lkpswatwatbudgetlus, "id", "Description", tblswatccwatermanagement.watBudget);
            ViewBag.watClassRep = new SelectList(db.lkpswatwatclassreplus, "id", "Description", tblswatccwatermanagement.watClassRep);
            ViewBag.watComConcerns = new SelectList(db.lkpswatwatcomconcernslus, "id", "Description", tblswatccwatermanagement.watComConcerns);
            ViewBag.watCom = new SelectList(db.lkpswatwatcomlus, "id", "Description", tblswatccwatermanagement.watCom);
            ViewBag.watTechStaff = new SelectList(db.lkpswatwattechstafflus, "id", "Description", tblswatccwatermanagement.watTechStaff);
            ViewBag.watTechTraining = new SelectList(db.lkpswatwattechtraininglus, "id", "Description", tblswatccwatermanagement.watTechTraining);

            ViewBag.Question1 = db.lkpswatscorevarslus.First(e => e.VarName == "watComSCORE").Description;
            ViewBag.Question2 = db.lkpswatscorevarslus.First(e => e.VarName == "watActionPlanSCORE").Description;
            ViewBag.Question3 = db.lkpswatscorevarslus.First(e => e.VarName == "watRecordsSCORETOTAL").Description;
            ViewBag.Question4 = db.lkpswatscorevarslus.First(e => e.VarName == "comSatisfactionSCORE").Description;
            ViewBag.Question5 = db.lkpswatscorevarslus.First(e => e.VarName == "watClassRepSCORE").Description;
            ViewBag.Question6 = db.lkpswatscorevarslus.First(e => e.VarName == "watComConcernsSCORE").Description;
            ViewBag.Question7 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechStaffSCORE").Description;
            ViewBag.Question8 = db.lkpswatscorevarslus.First(e => e.VarName == "watTechTrainingSCORE").Description;
            ViewBag.Question9 = db.lkpswatscorevarslus.First(e => e.VarName == "watBudgetSCORE").Description;
            ViewBag.Question10 = db.lkpswatscorevarslus.First(e => e.VarName == "watFinPlanSCORE").Description;
            ViewBag.Question11 = db.lkpswatscorevarslus.First(e => e.VarName == "proPoorSCORE").Description;
            ViewBag.currentSectionID = 3;
            ViewBag.locID = db.tblswatsurveys.Find(tblswatccwatermanagement.SurveyID).LocationID;
            ViewBag.waprecID = db.tblswatwaprecipitations.First(e => e.SurveyID == tblswatccwatermanagement.SurveyID).ID;
            ViewBag.SurveyID = tblswatccwatermanagement.SurveyID;
            ViewBag.uid = 191;
            return View(tblswatccwatermanagement);
        }

        //
        // GET: /CCWaterManagement/Delete/5

        //public ActionResult Delete(int id = 0)
        //{
        //    tblSWATCCwaterManagement tblswatccwatermanagement = db.tblSWATCCwaterManagements.Find(id);
        //    if (tblswatccwatermanagement == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatccwatermanagement);
        //}

        //
        // POST: /CCWaterManagement/Delete/5

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblSWATCCwaterManagement tblswatccwatermanagement = db.tblSWATCCwaterManagements.Find(id);
        //    db.tblSWATCCwaterManagements.Remove(tblswatccwatermanagement);
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