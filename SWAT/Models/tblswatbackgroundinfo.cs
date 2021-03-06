//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SWAT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblswatbackgroundinfo
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public Nullable<int> ClimateID { get; set; }
        public Nullable<int> SoilID { get; set; }
        public Nullable<int> EcoregionID { get; set; }
        public Nullable<int> WatershedID { get; set; }
        public Nullable<int> AridityID { get; set; }
        public Nullable<int> UrbanDistanceID { get; set; }
        public Nullable<long> Population { get; set; }
        public Nullable<int> numHouseholds { get; set; }
        public Nullable<int> numChildren { get; set; }
        public Nullable<float> PeoplePerHH { get; set; }
        public Nullable<int> PriorEquity { get; set; }
        public Nullable<int> isEconAg { get; set; }
        public Nullable<int> isEconLs { get; set; }
        public Nullable<int> isEconDev { get; set; }
        public Nullable<int> isEconPris { get; set; }
        public Nullable<float> Area { get; set; }
        public Nullable<float> AreaForest { get; set; }
        public Nullable<float> AreaAgC { get; set; }
        public Nullable<float> AreaInf { get; set; }
        public Nullable<float> AreaSw { get; set; }
        public Nullable<float> AreaWet { get; set; }
        public Nullable<float> AreaNat { get; set; }
        public Nullable<int> AreaProtID { get; set; }
        public Nullable<int> AreaBmID { get; set; }
        public Nullable<int> PriorQuality { get; set; }
        public Nullable<int> PriorQuan { get; set; }
        public Nullable<int> PriorSeasonal { get; set; }
        public Nullable<int> PriorPolitics { get; set; }
        public Nullable<int> PriorHealth { get; set; }
        public Nullable<int> PriorFinances { get; set; }
        public Nullable<int> PriorAccessible { get; set; }
        public Nullable<float> AreaAgH { get; set; }
        public Nullable<float> AreaPaved { get; set; }
    
        public virtual lkpbiome lkpbiome { get; set; }
        public virtual lkpclimateclassification lkpclimateclassification { get; set; }
        public virtual lkpsoil lkpsoil { get; set; }
        public virtual lkpswatareabmlu lkpswatareabmlu { get; set; }
        public virtual lkpswatareaprotlu lkpswatareaprotlu { get; set; }
        public virtual lkpswateconprislu lkpswateconprislu { get; set; }
        public virtual lkpswatmaparidity lkpswatmaparidity { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu1 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu2 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu3 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu4 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu5 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu6 { get; set; }
        public virtual lkpswatpriorlu lkpswatpriorlu7 { get; set; }
        public virtual lkpswaturbandistancelu lkpswaturbandistancelu { get; set; }
        public virtual lkpswatwatershedslu lkpswatwatershedslu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu1 { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu2 { get; set; }
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}
