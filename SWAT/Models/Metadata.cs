﻿using System;
using System.ComponentModel.DataAnnotations;

namespace SWAT.Models
{
    public class WPSwprMetadata
    {
        [Display(Name = "Maintenance")]
        public Nullable<int> pipedCosts1 { get; set; }

        [Display(Name = "Staff")]
        public Nullable<int> pipedCosts2 { get; set; }

        [Display(Name = "Operation (energy, supplies, etc.)")]
        public Nullable<int> pipedCosts3 { get; set; }

        [Display(Name = "Revenue")]
        [Range(0.00001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> pipedRevenue { get; set; }

        [Display(Name = "Expenditure")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> pipedExpenditure { get; set; }

        [Display(Name = "1 Year?")]
        public bool pipedFuture1 { get; set; }

        [Display(Name = "5 Year?")]
        public bool pipedFuture2 { get; set; }

        [Display(Name = "10 Year?")]
        public bool pipedFuture3 { get; set; }

        [Display(Name = "1 Year?")]
        public bool pipedFuturePop1 { get; set; }

        [Display(Name = "5 Year?")]
        public bool pipedFuturePop2 { get; set; }

        [Display(Name = "10 Year?")]
        public bool pipedFuturePop3 { get; set; }
    }

    public class WPSupplyMetadata
    {
        [Display(Name = "% of annual water")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domPercentUse { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectDry1 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectDryAmount1 { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectWet1 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectWetAmount1 { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectDry2 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectDryAmount2 { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectWet2 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectWetAmount2 { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectDry3 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectDryAmount3 { get; set; }

        [Display(Name = "Number of households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domCollectWet3 { get; set; }

        [Display(Name = "Average withdrawal")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> domCollectWetAmount3 { get; set; }

        [Display(Name = "In dry season")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domPipedNumHHDry { get; set; }

        [Display(Name = "In wet season")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> domPipedNumHHWet { get; set; }

        [Display(Name = "Wildlife")]
        public bool domdangerType1 { get; set; }

        [Display(Name = "Assault or rape")]
        public bool domdangerType2 { get; set; }

        [Display(Name = "Terrain")]
        public bool domdangerType3 { get; set; }

        [Display(Name = "Adult Woman")]
        public Nullable<int> domdemoWaterFetch1 { get; set; }

        [Display(Name = "Adult Men")]
        public Nullable<int> domdemoWaterFetch2 { get; set; }

        [Display(Name = "Teenage girls")]
        public Nullable<int> domdemoWaterFetch3 { get; set; }

        [Display(Name = "Teenage boys")]
        public Nullable<int> domdemoWaterFetch4 { get; set; }

        [Display(Name = "Children")]
        public Nullable<int> domdemoWaterFetch5 { get; set; }

        [Display(Name = "Childhood development centres (daycares, elementary schools)")]
        public bool wpaInstitutionHigh1 { get; set; }

        [Display(Name = "Health centre")]
        public bool wpaInstitutionHigh2 { get; set; }

        [Display(Name = "Senior centre")]
        public bool wpaInstitutionHigh3 { get; set; }

        [Display(Name = "Other high risk population")]
        public bool wpaInstitutionHigh4 { get; set; }

        [Display(Name = "Community centre or government building")]
        public bool wpaInstitutionLow1 { get; set; }

        [Display(Name = "Older education centres (secondary school, university, training centre)")]
        public bool wpaInstitutionLow2 { get; set; }

        [Display(Name = "Religious centres (church, mosque, temple)")]
        public bool wpaInstitutionLow3 { get; set; }

        [Display(Name = "Other lower risk population")]
        public bool wpaInstitutionLow4 { get; set; }

        [Display(Name = "Wildlife")]
        public bool indangerType1 { get; set; }

        [Display(Name = "Assault or rape")]
        public bool indangerType2 { get; set; }

        [Display(Name = "Terrain")]
        public bool indangerType3 { get; set; }

        [Display(Name = "Adult Woman")]
        public Nullable<int> indemoWaterFetch1 { get; set; }

        [Display(Name = "Adult Men")]
        public Nullable<int> indemoWaterFetch2 { get; set; }

        [Display(Name = "Teenage girls")]
        public Nullable<int> indemoWaterFetch3 { get; set; }

        [Display(Name = "Teenage boys")]
        public Nullable<int> indemoWaterFetch4 { get; set; }

        [Display(Name = "Children")]
        public Nullable<int> indemoWaterFetch5 { get; set; }

        [Display(Name = "Irrigation")]
        public bool ecWaterUse1 { get; set; }

        [Display(Name = "Livestock watering")]
        public bool ecWaterUse2 { get; set; }

        [Display(Name = "Industry")]
        public bool ecWaterUse3 { get; set; }

        [Display(Name = "Heating/cooling")]
        public bool ecWaterUse4 { get; set; }

        [Display(Name = "Other")]
        public bool ecWaterUse5 { get; set; }

        [Display(Name = "Dry Season")]
        public Nullable<double> ecWaterDemandDry { get; set; }

        [Display(Name = "Wet Season")]
        public Nullable<double> ecWaterDemandWet { get; set; }

        [Display(Name = "Supply restrictions")]
        public bool wpaInterruption1 { get; set; }

        [Display(Name = "Leaks")]
        public bool wpaInterruption2 { get; set; }

        [Display(Name = "High demand")]
        public bool wpaInterruption3 { get; set; }

        [Display(Name = "Breakdowns")]
        public bool wpaInterruption4 { get; set; }

        [Display(Name = "Finances")]
        public bool wpaInterruption5 { get; set; }

        [Display(Name = "Do not know/other")]
        public bool wpaInterruption6 { get; set; }

        [Display(Name = "Natural variation")]
        public bool wpaReliabilityProblem1 { get; set; }

        [Display(Name = "Other competing demands (e.g. agriculture)")]
        public bool wpaReliabilityProblem2 { get; set; }

        [Display(Name = "Periods of high contamination")]
        public bool wpaReliabilityProblem3 { get; set; }

        [Display(Name = "Breakdowns")]
        public bool wpaReliabilityProblem4 { get; set; }

        [Display(Name = "Finances")]
        public bool wpaReliabilityProblem5 { get; set; }

        [Display(Name = "Do not know/other")]
        public bool wpaReliabilityProblem6 { get; set; }
    }

    public class WaterPointMetadata
    {
        [Display(Name = "Water Point Accessing")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public Nullable<int> wpaLoc { get; set; }

        [Display(Name = "Water Point")]
        [StringLength(80, ErrorMessage="Water Point name can be upto 80 characters.")]
        public string wpname { get; set; }

        [Display(Name = "Domestic/household use")]
        public bool wpaUseDom { get; set; }

        [Display(Name = "Institutional use (hospitals, schools, community centres, etc.)")]
        public bool wpaUseInst { get; set; }

        [Display(Name = "Economic Activities (agriculture, industry, etc.)")]
        public bool wpaUseEc { get; set; }
    }

    public class SFLatMetadata
    {
        [Display(Name = "Ventilated improved pit latrine (VIP)")]
        public bool LatrineType1 { get; set; }

        [Display(Name = "Pit latrine with a covering")]
        public bool LatrineType2 { get; set; }

        [Display(Name = "Open pit")]
        public bool LatrineType3 { get; set; }

        [Display(Name = "Composting toilet")]
        public bool LatrineType4 { get; set; }

        [Display(Name = "High numbers of flies or rodents")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> latrineProb1 { get; set; }

        [Display(Name = "Cracked or broken facilities")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> latrineProb2 { get; set; }

        [Display(Name = "Latrine is full or otherwise unuseable")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> latrineProb3 { get; set; }

        [Display(Name = "People with disabilities")]
        public bool latrineAccessGroup1 { get; set; }

        [Display(Name = "Menstruating Women")]
        public bool latrineAccessGroup2 { get; set; }

        [Display(Name = "Children")]
        public bool latrineAccessGroup3 { get; set; }

        [Display(Name = "Ventilated improved pit latrine (VIP)")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> ltype1 { get; set; }

        [Display(Name = "Pit latrine with a covering")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> ltype2 { get; set; }

        [Display(Name = "Open pit")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> ltype3 { get; set; }

        [Display(Name = "Composting toilet")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> ltype4 { get; set; }
    }

    public class SFSepticMetadata
    {
        [Display(Name = "Number of Toilets")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> septicToilets { get; set; }

        [Display(Name = "% of users")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicUnderstand { get; set; }

        [Display(Name = "% of septic systems")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicDistance { get; set; }

        [Display(Name = "% of septic systems")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicPlug { get; set; }

        [Display(Name = "% of septic systems")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicPumped { get; set; }

        [Display(Name = "% of septic systems")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicTrash { get; set; }

        [Display(Name = "% of septic systems")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> septicChemicals { get; set; }

        [Display(Name = "People with disabilities")]
        public bool septicPubAccessGroup1 { get; set; }

        [Display(Name = "Menstruating Women")]
        public bool septicPubAccessGroup2 { get; set; }

        [Display(Name = "Children")]
        public bool septicPubAccessGroup3 { get; set; }

    }

    public class SFCentralMetadata
    {
        [Display(Name = "Number of Toilets")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> centralToilets { get; set; }

        [Display(Name = "People with disabilities")]
        public bool centralAccessGroup1 { get; set; }

        [Display(Name = "Menstruating Women")]
        public bool centralAccessGroup2 { get; set; }

        [Display(Name = "Children")]
        public bool centralAccessGroup3 { get; set; }

    }

    public class SFPointMetadata
    {
        [Display(Name = "Individual households")]
        public bool sanUseInd { get; set; }

        [Display(Name = "Public/shared toilets")]
        public bool sanUsePub { get; set; }

        [Display(Name = "Community institutions (government, school, religious, etc.)")]
        public bool SanUseCom { get; set; }
    }

    public class SFOdMetadata
    {
        [Display(Name = "Families that are very poor are less likely to have toilets")]
        public bool ODdemographic1 { get; set; }

        [Display(Name = "Certain demographic groups are less likely to have toilets")]
        public bool ODdemographic2 { get; set; }

        [Display(Name = "People with disabilities are less likely to have toilets")]
        public bool ODdemographic3 { get; set; }

        [Display(Name = "People of certain ethnic groups, castes, or cliques are less likely to have toilets")]
        public bool ODdemographic4 { get; set; }

        [Display(Name = "None of the above; everybody has a toilet")]
        public bool ODdemographicNONE { get; set; }

        [Display(Name = "# of Households")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<int> hhNoToilet { get; set; }

        [Display(Name = "Use shared or public facilities?")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> sharedFacPercent { get; set; }

        [Display(Name = "Practice open defecation?")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> ODPercent { get; set; }

        [Display(Name = "Women")]
        public bool ODdemoGender1 { get; set; }

        [Display(Name = "Men")]
        public bool ODdemoGender2 { get; set; }

        [Display(Name = "Girls")]
        public bool ODdemoGender3 { get; set; }

        [Display(Name = "Boys")]
        public bool ODdemoGender4 { get; set; }

        [Display(Name = "None of these groups practices open defecation more often than the others")]
        public bool ODdemoGenderNONE { get; set; }

        [Display(Name = "No access to facilities")]
        public bool ODfacilitator1 { get; set; }

        [Display(Name = "Facilities are unclean")]
        public bool ODfacilitator2 { get; set; }

        [Display(Name = "Facilities are dangerous")]
        public bool ODfacilitator3 { get; set; }

        [Display(Name = "Facilities are too far away")]
        public bool ODfacilitator4 { get; set; }

        [Display(Name = "Certain groups are not allowed access")]
        public bool ODfacilitator5 { get; set; }

        [Display(Name = "Cannot afford fees")]
        public bool ODfacilitator6 { get; set; }

        [Display(Name = "Convenience")]
        public bool ODfacilitator7 { get; set; }

        [Display(Name = "Tradition")]
        public bool ODfacilitator8 { get; set; }

        [Display(Name = "Accessibility")]
        public bool ODfacilitator9 { get; set; }

        [Display(Name = "Perception that feces are not dangerous")]
        public bool ODfacilitator10 { get; set; }

        [Display(Name = "Other")]
        public bool ODfacilitator11 { get; set; }
    }

    public class SFSanitationMetadata
    {
        [Display(Name = "In the home")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> toiletHome { get; set; }

        [Display(Name = "In the yard")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> toiletYard { get; set; }

        [Display(Name = "Within 100 m")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> toilet100 { get; set; }

        [Display(Name = "100-500m")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> toilet500 { get; set; }

        [Display(Name = "Over 500m")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> toiletFar { get; set; }
    }

    public class HPPKhpMetadata
    {
        [Display(Name = "% of community members")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> knowledgeQual { get; set; }

        [Display(Name = "Household water treatment")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> trainingAccess1 { get; set; }

        [Display(Name = "Safe water storage and handling")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> trainingAccess2 { get; set; }

        [Display(Name = "Health")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> trainingAccess3 { get; set; }

        [Display(Name = "Hygiene")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> trainingAccess4 { get; set; }

        [Display(Name = "Sanitation")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> trainingAccess5 { get; set; }

        [Display(Name = "% of households")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> handWash { get; set; }

        [Display(Name = "In latrine/toilet")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces1 { get; set; }

        [Display(Name = "In garbage")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces2 { get; set; }

        [Display(Name = "Buried")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces3 { get; set; }

        [Display(Name = "In drain or ditch")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces4 { get; set; }

        [Display(Name = "Left in open")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces5 { get; set; }

        [Display(Name = "Compost")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces6 { get; set; }

        [Display(Name = "Other")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> childFaeces7 { get; set; }
    }

    public class SWPDevMetadata
    {
        [Display(Name = "Mines")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> devSite1 { get; set; }

        [Display(Name = "Landfills")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> devSite2 { get; set; }

        [Display(Name = "Industrial sites")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> devSite3 { get; set; }

        [Display(Name = "Other")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> devSite4 { get; set; }
    }

    public class SWPAgMetadata
    {
        [Display(Name = "Intensive (commercial large-scale farming)")]
        public bool agType1 { get; set; }

        [Display(Name = "Non-intensive (subsistence)")]
        public bool agType2 { get; set; }
    }

    public class CCWaterManagementMetadata
    {
        [DataType(DataType.MultilineText)]
        public string watAdminOther { get; set; }

        [Display(Name = "Administrative")]
        public Nullable<int> watRecords1 { get; set; }

        [Display(Name = "Financial")]
        public Nullable<int> watRecords2 { get; set; }

        [Display(Name = "Water quality")]
        public Nullable<int> watRecords3 { get; set; }

        [Display(Name = "Water quantity")]
        public Nullable<int> watRecords4 { get; set; }

        [Display(Name = "Maintenance")]
        public Nullable<int> watRecords5 { get; set; }

        [Display(Name = "Infrastructure maintenance and operation")]
        public bool watFinPlan1 { get; set; }

        [Display(Name = "Infrastructure expansion")]
        public bool watFinPlan2 { get; set; }
    }

    public class CCExternalMetadata
    {
        [Display(Name = "Technical support")]
        public Nullable<int> extVisitTech { get; set; }

        [Display(Name = "Administrative support")]
        public Nullable<int> extVisitAdmin { get; set; }

        [Display(Name = "Internet resources")]
        public bool expertAccess1 { get; set; }

        [Display(Name = "Consultants")]
        public bool expertAccess2 { get; set; }

        [Display(Name = "Technical documents")]
        public bool expertAccess3 { get; set; }

        [Display(Name = "Local watershed reports")]
        public bool expertAccess4 { get; set; }

        [Display(Name = "Scientific research")]
        public bool expertAccess5 { get; set; }
    }

    public class CCComMetada
    {
        [Display(Name = "Documented policies")]
        public bool comResource1 { get; set; }

        [Display(Name = "Documented future plans and goals")]
        public bool comResource2 { get; set; }

        [Display(Name = "Office space")]
        public bool comResource3 { get; set; }

        [Display(Name = "Elected council")]
        public bool comResource4 { get; set; }

        [Display(Name = "Paid staff")]
        public bool comResource5 { get; set; }
    }

    public class CCSocialMetadata
    {
        [Display(Name = "% of Households")]
        [Range(0, 100, ErrorMessage = "The value is out of range")]
        public Nullable<double> socAttend { get; set; }
    }

    public class CCGenderMetadata
    {
        [Display(Name = "Water collection")]
        public Nullable<int> gRole1 { get; set; }

        [Display(Name = "Cooking")]
        public Nullable<int> gRole2 { get; set; }

        [Display(Name = "Cleaning")]
        public Nullable<int> gRole3 { get; set; }

        [Display(Name = "Keeping water sources safe")]
        public Nullable<int> gRole4 { get; set; }

        [Display(Name = "Child rearing")]
        public Nullable<int> gRole5 { get; set; }

        [Display(Name = "Agriculture")]
        public Nullable<int> gRole6 { get; set; }

        [Display(Name = "% of community-based organizations")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> gWomenRep { get; set; }
    }

    public class CCFinancialMetadata
    {
        [Display(Name = "% of Households")]
        [Range(0, 100, ErrorMessage = "The value is out of range")]
        public Nullable<double> income { get; set; }

        [Display(Name = "Land or real estate")]
        public bool assetsCom1 { get; set; }

        [Display(Name = "Equipment")]
        public bool assetsCom2 { get; set; }

        [Display(Name = "Cash")]
        public bool assetsCom3 { get; set; }

        [Display(Name = "Other investments (stocks, bonds, etc.)")]
        public bool assetsCom4 { get; set; }

        [Display(Name = "Land or real estate")]
        public bool assetsInd1 { get; set; }

        [Display(Name = "Equipment")]
        public bool assetsInd2 { get; set; }

        [Display(Name = "Cash")]
        public bool assetsInd3 { get; set; }

        [Display(Name = "Other investments (stocks, bonds, etc.)")]
        public bool assetsInd4 { get; set; }
    }

    public class CCIndigMetadata
    {
        [Display(Name = "Indigenous Popluation")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> indigPop { get; set; }

        [Display(Name = "Longterm Popluation")]
        [Range(0, int.MaxValue, ErrorMessage = "The value is out of range")]
        public Nullable<int> longtermPop { get; set; }
    }

    public class CCSchoolMetadata
    {
        [Display(Name = "% of children")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> schoolAttend { get; set; }

        [Display(Name = "Elementary School")]
        public bool schoolInstitution1 { get; set; }

        [Display(Name = "Secondary School")]
        public bool schoolInstitution2 { get; set; }

        [Display(Name = "Technical Institute")]
        public bool schoolInstitution3 { get; set; }

        [Display(Name = "College/University")]
        public bool schoolInstitution4 { get; set; }

        [Display(Name = "Access to Health and Hygiene")]
        public Nullable<int> schoolHHAccess { get; set; }
    }

    public class CCTrainMetadata
    {
        [Display(Name = "Administrative supervisor")]
        public bool trainProf1 { get; set; }

        [Display(Name = "Health scientist (nurse, doctor, researcher)")]
        public bool trainProf2 { get; set; }

        [Display(Name = "Engineer")]
        public bool trainProf3 { get; set; }

        [Display(Name = "Lawyer")]
        public bool trainProf4 { get; set; }

        [Display(Name = "Accountant")]
        public bool trainProf5 { get; set; }

        [Display(Name = "Mechanic")]
        public bool trainTech1 { get; set; }

        [Display(Name = "Laboratory technician")]
        public bool trainTech2 { get; set; }

        [Display(Name = "Water system operator")]
        public bool trainTech3 { get; set; }

        [Display(Name = "Administrative Assistant")]
        public bool trainTech4 { get; set; }

        [Display(Name = "IT Technician")]
        public bool trainTech5 { get; set; }
    }

    public class CCEducationMetadata
    {
        [Display(Name = "% of Primary School")]
        [Range(0, 100, ErrorMessage="The value is out of range")]
        public Nullable<double> eduPrim { get; set; }

        [Display(Name = "% in Secondary School")]
        [Range(0, 100, ErrorMessage = "The value is out of range")]
        public Nullable<double> eduSec { get; set; }

        [Display(Name = "% of Women Finished Secondary School")]
        [Range(0, 100, ErrorMessage = "The value is out of range")]
        public Nullable<double> eduGradWomen { get; set; }

        [Display(Name = "% of Men Finished Secondary School")]
        [Range(0, 100, ErrorMessage = "The value is out of range")]
        public Nullable<double> eduGradMen { get; set; }
    }

    public class WAGroundWaterMetadata
    {
        [Display(Name = "Ground Water Availability")]
        public Nullable<int> gwAvailability { get; set; }

        [Display(Name = "Ground Water Reliability")]
        public Nullable<int> gwReliability { get; set; }
    }

    public class WASurfaceWaterMetadata
    {
        [Display(Name = "Surface Water Availability")]
        public Nullable<int> runoff { get; set; }

        [Display(Name = "Suface Water Level")]
        public Nullable<int> surfaceVar { get; set; }
    }

    public class WARiskPrepMetadata
    {
        [Display(Name = "Risk of Fire")]
        public Nullable<int> riskFire { get; set; }

        [Display(Name = "Risk of Flood")]
        public Nullable<int> riskFlood { get; set; }

        [Display(Name = "Risk of Drought")]
        public Nullable<int> riskDrought { get; set; }

        [Display(Name = "Preparedness for Fire")]
        public Nullable<int> prepFire { get; set; }

        [Display(Name = "Preparedness for Flood")]
        public Nullable<int> prepFlood { get; set; }

        [Display(Name = "Preparedness for Drought")]
        public Nullable<int> prepDrought { get; set; }
    }

    public class LocationMetadata
    {
        [Required(ErrorMessage="Settlement Name is required.")]
        [StringLength(50)]
        public string name;
        [Range(Double.MinValue, Double.MaxValue, ErrorMessage="Latitude value is invalid.")]
        public double latitude;
        [Range(Double.MinValue, Double.MaxValue, ErrorMessage="Longitude value is invalid.")]
        public double longitude;
        [Required(ErrorMessage = "Country is required.")]
        public int countryID;
        [Required(ErrorMessage = "Region is required.")]
        public int regionID;
        [Required(ErrorMessage = "Subnation is required.")]
        public int subnationalID;
    }

    public class BackgroundMetadata
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The population has to be an integer and >0.")]
        public Nullable<long> Population;

        [Required]
        [Display(Name = "Number of Households")]
        [Range(1, int.MaxValue, ErrorMessage = "The number of households has to be an integer and >0.")]
        public Nullable<int> numHouseholds;

        [Display(Name = "Number of Children")]
        [Range(1, int.MaxValue, ErrorMessage = "The number of Children has to be an integer and >0.")]
        public Nullable<int> numChildren;

        [Display(Name = "People Per Household")]
        [Range(0.0001, Double.MaxValue, ErrorMessage = "The people per household has to be >0.")]
        public Nullable<double> PeoplePerHH;

        [Display(Name = "Forest (%)")]
        [Range(0, 100, ErrorMessage="The value is out of range.")]
        public Nullable<double> AreaForest;

        [Display(Name = "Commercial Agriculture (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaAgC;

        [Display(Name = "Hobby/Subsistence Agriculture (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaAgH;

        [Display(Name = "Paved Cover (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaPaved;

        [Display(Name = "Infrastructure (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaInf;

        [Display(Name = "Source Water (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaSw;

        [Display(Name = "Wetlands (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaWet;

        [Display(Name = "Native or Natural Lands (%)")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> AreaNat;
    }

    public class WAPrecipitationMetadata
    {
        [Display(Name = "Average precipitation (mm) for January")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> January { get; set; }

        [Display(Name = "Average precipitation (mm) for February")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> February { get; set; }

        [Display(Name = "Average precipitation (mm) for March")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> March { get; set; }

        [Display(Name = "Average precipitation (mm) for April")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> April { get; set; }

        [Display(Name = "Average precipitation (mm) for May")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> May { get; set; }

        [Display(Name = "Average precipitation (mm) for June")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> June { get; set; }

        [Display(Name = "Average precipitation (mm) for July")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> July { get; set; }

        [Display(Name = "Average precipitation (mm) for August")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> August { get; set; }

        [Display(Name = "Average precipitation (mm) for September")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> September { get; set; }

        [Display(Name = "Average precipitation (mm) for October")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> October { get; set; }

        [Display(Name = "Average precipitation (mm) for November")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> November { get; set; }

        [Display(Name = "Average precipitation (mm) for December")]
        [Range(0.0001, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> December { get; set; }
    
    }

    public class waannualprecipMetadata
    {
        [Display(Name = "Standard Deviation of annual precipitation")]
        [Range(0, double.MaxValue, ErrorMessage = "The value is out of range.")]
        public Nullable<double> precipVar { get; set; }

        [Display(Name = "Annual Precipitation")]
        public Nullable<int> precipVarALT { get; set; }
    }

    public class WAClimateChangeMetadata
    {
        [Display(Name = "Drier")]
        public bool climateDryer { get; set; }

        [Display(Name = "Wetter")]
        public bool climateWetter { get; set; }

        [Display(Name = "Colder")]
        public bool climateColder { get; set; }

        [Display(Name = "Hotter")]
        public bool climateHotter { get; set; }

        [Display(Name = "Seasons")]
        public bool climateSeasons { get; set; }

    }

    public class WAExtremeEventMetadata
    {
        [Display(Name="Extreme Dry")]
        public Nullable<int> extremeDry { get; set; }

        [Display(Name = "Extreme Flood")]
        public Nullable<int> extremeFlood { get; set; }

        [Display(Name = "Other Extreme Event(s)")]
        public Nullable<int> extremeOther { get; set; }

        [Display(Name = "Please Specify Event(s)")]
        public string extremeOtherComment { get; set; }
    }

    public class SWPLivestockMetadata
    {
        [Display(Name = "Intensive (commercial livestock)")]
        public bool livestock1 { get; set; }

        [Display(Name = "Non-intensive (household-owned)")]
        public bool livestock2 { get; set; }

        [Display(Name = "Proportion of water sources")]
        [Range(0, 100, ErrorMessage = "The value is out of range.")]
        public Nullable<double> waterFenced { get; set; }
    }
}