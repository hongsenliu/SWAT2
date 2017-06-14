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
    public class BackgroundController : Controller
    {
        private SWATEntities db = new SWATEntities();

        // GET: /Background/
        //public ActionResult Index()
        //{
        //    var tblswatbackgroundinfoes = db.tblSWATBackgroundinfoes.Include(t => t.lkpBiome).Include(t => t.lkpClimateClassification).Include(t => t.lkpSoil).Include(t => t.lkpSWATareaProtLU).Include(t => t.lkpSWATmapAridity).Include(t => t.lkpSWATurbanDistanceLU).Include(t => t.lkpSWATWatershedsLU).Include(t => t.tblSWATSurvey).Include(t => t.lkpSWATareaBMLU).Include(t => t.lkpSWATeconPrisLU).Include(t => t.lkpSWATpriorLU).Include(t => t.lkpSWATpriorLU1).Include(t => t.lkpSWATpriorLU2).Include(t => t.lkpSWATpriorLU3).Include(t => t.lkpSWATpriorLU4).Include(t => t.lkpSWATpriorLU5).Include(t => t.lkpSWATpriorLU6).Include(t => t.lkpSWATpriorLU7).Include(t => t.lkpSWATYesNoLU).Include(t => t.lkpSWATYesNoLU1).Include(t => t.lkpSWATYesNoLU2);
        //    return View(tblswatbackgroundinfoes.ToList());
        //}

        // GET: /Background/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblSWATBackgroundinfo tblswatbackgroundinfo = db.tblSWATBackgroundinfoes.Find(id);
        //    if (tblswatbackgroundinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatbackgroundinfo);
        //}

        // GET: /Background/Create
        public ActionResult Create(int? SurveyID)
        {
            if ( SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.EcoregionID = new SelectList(db.lkpbiomes, "ID", "Description");
            ViewBag.ClimateID = new SelectList(db.lkpclimateclassifications, "ID", "CCType");
            ViewBag.SoilID = new SelectList(db.lkpsoils, "ID", "Name");
            ViewBag.AreaProtID = new SelectList(db.lkpswatareaprotlus, "id", "Description");
            ViewBag.AridityID = new SelectList(db.lkpswatmaparidities, "id", "Description");
            ViewBag.UrbanDistanceID = new SelectList(db.lkpswaturbandistancelus, "id", "Description");
            ViewBag.WatershedID = new SelectList(db.lkpswatwatershedslus, "ID", "Description");
            //ViewBag.SurveyID = new SelectList(db.tblSWATSurveys, "ID", "ID");
            ViewBag.AreaBmID = new SelectList(db.lkpswatareabmlus, "id", "Description");
            ViewBag.isEconPris = new SelectList(db.lkpswateconprislus, "id", "Description");
            ViewBag.PriorQuality = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorQuan = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorSeasonal = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorPolitics = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorHealth = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorFinances = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorAccessible = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.PriorEquity = new SelectList(db.lkpswatpriorlus, "id", "Description");
            ViewBag.isEconAg = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.isEconLs = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.isEconDev = new SelectList(db.lkpswatyesnolus, "id", "Description");
            ViewBag.SurveyID = SurveyID;
            ViewBag.currentSectionID = 1;
            return View();
        }

        private void updateSWPrecords(tblswatbackgroundinfo tblswatbackgroundinfo)
        {
            // TODO remove ls, ag and dev if there are some records and isEconls, isEconAg, isEcondev are not 1511
            if (tblswatbackgroundinfo.isEconLs != 1511)
            {
                var livestocks = db.tblswatswpls.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                foreach (tblswatswpl item in livestocks)
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "livestockSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "waterFencedSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "livestockEffluentSCORE").Value = null;
                    
                    db.tblswatswpls.Remove(item);
                }
            }
            else
            { 
                var livestocks = db.tblswatswpls.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                if (!livestocks.Any())
                {
                    tblswatswpl tblswatswpl = new tblswatswpl();
                    tblswatswpl.SurveyID = tblswatbackgroundinfo.SurveyID;
                    db.tblswatswpls.Add(tblswatswpl);
                }
            }

            if (tblswatbackgroundinfo.isEconAg != 1511)
            {
                var records = db.tblswatswpags.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                foreach (tblswatswpag item in records)
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "agTypeSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "fertilizerSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "pesticideSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "erosionSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "bestManagementSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "cropLossSCORE").Value = null;
                    
                    db.tblswatswpags.Remove(item);
                }
            }
            else
            {
                var ags = db.tblswatswpags.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                if (!ags.Any())
                {
                    tblswatswpag tblswatswpag = new tblswatswpag();
                    tblswatswpag.SurveyID = tblswatbackgroundinfo.SurveyID;
                    db.tblswatswpags.Add(tblswatswpag);
                }
            }

            if (tblswatbackgroundinfo.isEconDev != 1511)
            {
                var records = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                foreach (tblswatswpdev item in records)
                {
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "devSiteSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "bestManIndSCORE").Value = null;
                    db.tblswatscores.First(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VarName == "wwTreatIndSCORE").Value = null;
                    db.tblswatswpdevs.Remove(item);
                }
            }
            else
            {
                var devs = db.tblswatswpdevs.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ToList();
                if (!devs.Any())
                {
                    tblswatswpdev tblswatswpdev = new tblswatswpdev();
                    tblswatswpdev.SurveyID = tblswatbackgroundinfo.SurveyID;
                    db.tblswatswpdevs.Add(tblswatswpdev);
                }
            }

            db.SaveChanges();
        }

        // Helper method to update the scores that occur in the background infomation section
        private void updateScores(tblswatbackgroundinfo tblswatbackgroundinfo)
        {
            var sectionscorevars = db.lkpswatsectionlus.Include(t=>t.lkpswatscorevarslus).Single(section => section.ID == 1);
            
            foreach (var item in sectionscorevars.lkpswatscorevarslus)
            {
                if (item.VarName == "ariditySCORE")
                {
                    if (tblswatbackgroundinfo.AridityID != null)
                    {
                        var scoreIntorder = db.lkpswatmaparidities.Find(tblswatbackgroundinfo.AridityID).intorder;
                        var aridityScore = db.lkpswatscores_aridity.Single(e => e.intorder == scoreIntorder).Description;
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = Double.Parse(aridityScore);
                    }
                    else
                    {
                         db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = null;
                    }
                    
                    db.SaveChanges();
                }
                if (item.VarName == "econPrisSCORE")
                {
                    if (tblswatbackgroundinfo.isEconPris != null)
                    {
                        var scoreIntorder = db.lkpswateconprislus.Find(tblswatbackgroundinfo.isEconPris).intorder;
                        var econPrisScore = db.lkpswatscores_econpris.Single(e => e.intorder == scoreIntorder).Description;
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = Double.Parse(econPrisScore);
                    }
                    else
                    {
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = null;
                    }
                    db.SaveChanges();
                }
                if (item.VarName == "areaBMSCORE")
                {
                    if (tblswatbackgroundinfo.AreaBmID != null)
                    {
                        var scoreIntorder = db.lkpswatareabmlus.Find(tblswatbackgroundinfo.AreaBmID).intorder;
                        var areaBMScore = db.lkpswatscores_areabmlu.Single(e => e.intorder == scoreIntorder).Description;
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = Double.Parse(areaBMScore);
                    }
                    else
                    {
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = null;
                    }
                    db.SaveChanges();
                }
                if (item.VarName == "areaProtSCORE")
                {
                    if (tblswatbackgroundinfo.AreaProtID != null)
                    {
                        var scoreIntorder = db.lkpswatareaprotlus.Find(tblswatbackgroundinfo.AreaProtID).intorder;
                        var areaProtScore = db.lkpswatscores_areaprot.Single(e => e.intorder == scoreIntorder).Description;
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = Double.Parse(areaProtScore);
                    }
                    else 
                    {
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = null;
                    }
                    db.SaveChanges();
                }
                if (item.VarName == "urbanDistanceSCORE")
                {
                    if (tblswatbackgroundinfo.UrbanDistanceID != null)
                    {
                        var scoreIntorder = db.lkpswaturbandistancelus.Find(tblswatbackgroundinfo.UrbanDistanceID).intorder;
                        var urbanDistanceScore = db.lkpswatscores_urbandistance.Single(e => e.intorder == scoreIntorder).Description;
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = Double.Parse(urbanDistanceScore);
                    }
                    else
                    {
                        db.tblswatscores.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID && e.VariableID == item.ID).Value = null;
                    }
                    db.SaveChanges();
                }
            }

            updateSWPrecords(tblswatbackgroundinfo);

            
        }

        // POST: /Background/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,SurveyID,ClimateID,SoilID,EcoregionID,WatershedID,AridityID,UrbanDistanceID,Population,numHouseholds,numChildren,PeoplePerHH,isEconAg,isEconLs,isEconDev,isEconPris,Area,AreaForest,AreaAgC,AreaAgH,AreaPaved,AreaInf,AreaSw,AreaWet,AreaNat,AreaProtID,AreaBmID,PriorQuality,PriorQuan,PriorSeasonal,PriorPolitics,PriorHealth,PriorFinances,PriorAccessible,PriorEquity")] tblswatbackgroundinfo tblswatbackgroundinfo, string submitBtn)
        {
            // Check if the sum of Forest(%), Agriculture(%), Infrastructure(%), Surface Water(%) and Wetlands(%) exceeds 100.
            var totalArea = tblswatbackgroundinfo.AreaForest.GetValueOrDefault(0) 
                            + tblswatbackgroundinfo.AreaAgC.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaAgH.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaPaved.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaInf.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaSw.GetValueOrDefault(0) 
                            + tblswatbackgroundinfo.AreaWet.GetValueOrDefault(0);
            if (totalArea > 100)
            {
                ModelState.AddModelError("AreaForest", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaAgC", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaAgH", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaInf", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaSw", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaWet", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("Paved", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
            }
            
            if (ModelState.IsValid)
            {
                var backgrdIDs = db.tblswatbackgroundinfoes.Where(item => item.SurveyID == tblswatbackgroundinfo.SurveyID).Select(item => item.ID);
                if (backgrdIDs.Any())
                {
                    var backgrdID = backgrdIDs.First();
                    tblswatbackgroundinfo.ID = backgrdID;
                    db.Entry(tblswatbackgroundinfo).State = EntityState.Modified;                
                }
                else
                {
                    db.tblswatbackgroundinfoes.Add(tblswatbackgroundinfo);
                }

                
                db.SaveChanges();
                updateScores(tblswatbackgroundinfo);


                if (submitBtn.Equals("Next"))
                {
                    return RedirectToAction("Create", "WAPrecipitation", new { SurveyID = tblswatbackgroundinfo.SurveyID });
                }
            }

            ViewBag.EcoregionID = new SelectList(db.lkpbiomes, "ID", "Description", tblswatbackgroundinfo.EcoregionID);
            ViewBag.ClimateID = new SelectList(db.lkpclimateclassifications, "ID", "CCType", tblswatbackgroundinfo.ClimateID);
            ViewBag.SoilID = new SelectList(db.lkpsoils, "ID", "Name", tblswatbackgroundinfo.SoilID);
            ViewBag.AreaProtID = new SelectList(db.lkpswatareaprotlus, "id", "Description", tblswatbackgroundinfo.AreaProtID);
            ViewBag.AridityID = new SelectList(db.lkpswatmaparidities, "id", "Description", tblswatbackgroundinfo.AridityID);
            ViewBag.UrbanDistanceID = new SelectList(db.lkpswaturbandistancelus, "id", "Description", tblswatbackgroundinfo.UrbanDistanceID);
            ViewBag.WatershedID = new SelectList(db.lkpswatwatershedslus, "ID", "Description", tblswatbackgroundinfo.WatershedID);
            //ViewBag.SurveyID = new SelectList(db.tblSWATSurveys, "ID", "ID", tblswatbackgroundinfo.SurveyID);
            ViewBag.AreaBmID = new SelectList(db.lkpswatareabmlus, "id", "Description", tblswatbackgroundinfo.AreaBmID);
            ViewBag.isEconPris = new SelectList(db.lkpswateconprislus, "id", "Description", tblswatbackgroundinfo.isEconPris);
            ViewBag.PriorQuality = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuality);
            ViewBag.PriorQuan = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuan);
            ViewBag.PriorSeasonal = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorSeasonal);
            ViewBag.PriorPolitics = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorPolitics);
            ViewBag.PriorHealth = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorHealth);
            ViewBag.PriorFinances = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorFinances);
            ViewBag.PriorAccessible = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorAccessible);
            ViewBag.PriorEquity = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorEquity);
            ViewBag.isEconAg = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconAg);
            ViewBag.isEconLs = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconLs);
            ViewBag.isEconDev = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconDev);
            ViewBag.SurveyID = tblswatbackgroundinfo.SurveyID;
            ViewBag.currentSectionID = 1;
            return View(tblswatbackgroundinfo);
        }

        // GET: /Background/Edit/5
        public ActionResult Edit(int? id, int? SurveyID)
        {
            if (id == null || SurveyID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tblswatbackgroundinfo tblswatbackgroundinfo = db.tblswatbackgroundinfoes.Find(id);
            if (tblswatbackgroundinfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EcoregionID = new SelectList(db.lkpbiomes, "ID", "Description", tblswatbackgroundinfo.EcoregionID);
            ViewBag.ClimateID = new SelectList(db.lkpclimateclassifications, "ID", "CCType", tblswatbackgroundinfo.ClimateID);
            ViewBag.SoilID = new SelectList(db.lkpsoils, "ID", "Name", tblswatbackgroundinfo.SoilID);
            ViewBag.AreaProtID = new SelectList(db.lkpswatareaprotlus, "id", "Description", tblswatbackgroundinfo.AreaProtID);
            ViewBag.AridityID = new SelectList(db.lkpswatmaparidities, "id", "Description", tblswatbackgroundinfo.AridityID);
            ViewBag.UrbanDistanceID = new SelectList(db.lkpswaturbandistancelus, "id", "Description", tblswatbackgroundinfo.UrbanDistanceID);
            ViewBag.WatershedID = new SelectList(db.lkpswatwatershedslus, "ID", "Description", tblswatbackgroundinfo.WatershedID);
            //ViewBag.SurveyID = new SelectList(db.tblSWATSurveys, "ID", "ID", tblswatbackgroundinfo.SurveyID);
            ViewBag.AreaBmID = new SelectList(db.lkpswatareabmlus, "id", "Description", tblswatbackgroundinfo.AreaBmID);
            ViewBag.isEconPris = new SelectList(db.lkpswateconprislus, "id", "Description", tblswatbackgroundinfo.isEconPris);
            ViewBag.PriorQuality = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuality);
            ViewBag.PriorQuan = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuan);
            ViewBag.PriorSeasonal = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorSeasonal);
            ViewBag.PriorPolitics = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorPolitics);
            ViewBag.PriorHealth = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorHealth);
            ViewBag.PriorFinances = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorFinances);
            ViewBag.PriorAccessible = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorAccessible);
            ViewBag.PriorEquity = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorEquity);
            ViewBag.isEconAg = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconAg);
            ViewBag.isEconLs = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconLs);
            ViewBag.isEconDev = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconDev);
            ViewBag.SurveyID = SurveyID;
            ViewBag.currentSectionID = 1;
            return View(tblswatbackgroundinfo);
        }

        // POST: /Background/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,SurveyID,ClimateID,SoilID,EcoregionID,WatershedID,AridityID,UrbanDistanceID,Population,numHouseholds,numChildren,PeoplePerHH,isEconAg,isEconLs,isEconDev,isEconPris,Area,AreaForest,AreaAgC,AreaAgH,AreaPaved,AreaInf,AreaSw,AreaWet,AreaNat,AreaProtID,AreaBmID,PriorQuality,PriorQuan,PriorSeasonal,PriorPolitics,PriorHealth,PriorFinances,PriorAccessible,PriorEquity")] tblswatbackgroundinfo tblswatbackgroundinfo, string submitBtn)
        {
            // Check if the sum of Forest(%), Agriculture(%), Infrastructure(%), Surface Water(%) and Wetlands(%) exceeds 100.
            var totalArea = tblswatbackgroundinfo.AreaForest.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaAgC.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaAgH.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaPaved.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaInf.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaSw.GetValueOrDefault(0)
                            + tblswatbackgroundinfo.AreaWet.GetValueOrDefault(0);
            if (totalArea > 100)
            {
                ModelState.AddModelError("AreaForest", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaAgC", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaAgH", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaInf", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaSw", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("AreaWet", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
                ModelState.AddModelError("Paved", "The sum of Forest(%), Commercial Agriculture(%), Hobby/Subsistence Agriculture(%), Paved Cover(%), Infrastructure(%), Surface Water(%) and Wetlands(%) cannot exceed 100.");
            }

            if (ModelState.IsValid)
            {
                db.Entry(tblswatbackgroundinfo).State = EntityState.Modified;
                db.SaveChanges();
                updateScores(tblswatbackgroundinfo);

                if (submitBtn.Equals("Next"))
                { 
                    // If there is not a WAPreciptation with the current survey (SurveyID) then create one and redirecto to its edit link.
                    var waprecip = db.tblswatwaprecipitations.Where(e => e.SurveyID == tblswatbackgroundinfo.SurveyID);
                    if (!waprecip.Any())
                    {
                        tblswatwaprecipitation tblswatwaprecipitation = new tblswatwaprecipitation();
                        tblswatwaprecipitation.SurveyID = tblswatbackgroundinfo.SurveyID;
                        db.tblswatwaprecipitations.Add(tblswatwaprecipitation);
                        db.SaveChanges();
                        var newWAPreciptationID = tblswatwaprecipitation.ID;
                        return RedirectToAction("Edit", "WAPrecipitation", new { id = tblswatwaprecipitation.ID, SurveyID = tblswatwaprecipitation.SurveyID });
                    }
                    else 
                    {
                        return RedirectToAction("Edit", "WAPrecipitation", new { id = waprecip.Single(e => e.SurveyID == tblswatbackgroundinfo.SurveyID).ID, SurveyID = tblswatbackgroundinfo.SurveyID });
                    }
                }
            }
            ViewBag.EcoregionID = new SelectList(db.lkpbiomes, "ID", "Description", tblswatbackgroundinfo.EcoregionID);
            ViewBag.ClimateID = new SelectList(db.lkpclimateclassifications, "ID", "CCType", tblswatbackgroundinfo.ClimateID);
            ViewBag.SoilID = new SelectList(db.lkpsoils, "ID", "Name", tblswatbackgroundinfo.SoilID);
            ViewBag.AreaProtID = new SelectList(db.lkpswatareaprotlus, "id", "Description", tblswatbackgroundinfo.AreaProtID);
            ViewBag.AridityID = new SelectList(db.lkpswatmaparidities, "id", "Description", tblswatbackgroundinfo.AridityID);
            ViewBag.UrbanDistanceID = new SelectList(db.lkpswaturbandistancelus, "id", "Description", tblswatbackgroundinfo.UrbanDistanceID);
            ViewBag.WatershedID = new SelectList(db.lkpswatwatershedslus, "ID", "Description", tblswatbackgroundinfo.WatershedID);
            //ViewBag.SurveyID = new SelectList(db.tblSWATSurveys, "ID", "ID", tblswatbackgroundinfo.SurveyID);
            ViewBag.AreaBmID = new SelectList(db.lkpswatareabmlus, "id", "Description", tblswatbackgroundinfo.AreaBmID);
            ViewBag.isEconPris = new SelectList(db.lkpswateconprislus, "id", "Description", tblswatbackgroundinfo.isEconPris);
            ViewBag.PriorQuality = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuality);
            ViewBag.PriorQuan = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorQuan);
            ViewBag.PriorSeasonal = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorSeasonal);
            ViewBag.PriorPolitics = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorPolitics);
            ViewBag.PriorHealth = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorHealth);
            ViewBag.PriorFinances = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorFinances);
            ViewBag.PriorAccessible = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorAccessible);
            ViewBag.PriorEquity = new SelectList(db.lkpswatpriorlus, "id", "Description", tblswatbackgroundinfo.PriorEquity);
            ViewBag.isEconAg = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconAg);
            ViewBag.isEconLs = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconLs);
            ViewBag.isEconDev = new SelectList(db.lkpswatyesnolus, "id", "Description", tblswatbackgroundinfo.isEconDev);
            ViewBag.SurveyID = tblswatbackgroundinfo.SurveyID;
            ViewBag.currentSectionID = 1;
            return View(tblswatbackgroundinfo);
        }

        // GET: /Background/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    tblSWATBackgroundinfo tblswatbackgroundinfo = db.tblSWATBackgroundinfoes.Find(id);
        //    if (tblswatbackgroundinfo == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(tblswatbackgroundinfo);
        //}

        // POST: /Background/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    tblSWATBackgroundinfo tblswatbackgroundinfo = db.tblSWATBackgroundinfoes.Find(id);
        //    db.tblSWATBackgroundinfoes.Remove(tblswatbackgroundinfo);
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
