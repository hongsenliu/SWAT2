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
    
    public partial class tblswatsfcentral
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public Nullable<int> centralToilets { get; set; }
        public Nullable<int> centralTreatmentType { get; set; }
        public Nullable<int> centralSludge { get; set; }
        public Nullable<int> centralCondition { get; set; }
        public Nullable<int> centralPubClean { get; set; }
        public bool centralAccessGroup1 { get; set; }
        public bool centralAccessGroup2 { get; set; }
        public bool centralAccessGroup3 { get; set; }
        public Nullable<int> centralfeesCharged { get; set; }
        public Nullable<int> centralFeesLimitAccess { get; set; }
        public Nullable<int> centralFeesEnsureClean { get; set; }
        public Nullable<int> hhCentralClean { get; set; }
    
        public virtual lkpswat5ranklu lkpswat5ranklu { get; set; }
        public virtual lkpswatcentralconditionlu lkpswatcentralconditionlu { get; set; }
        public virtual lkpswatcentraltreatmenttypelu lkpswatcentraltreatmenttypelu { get; set; }
        public virtual lkpswathhcleanlu lkpswathhcleanlu { get; set; }
        public virtual lkpswatpubcleanlu lkpswatpubcleanlu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu1 { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu2 { get; set; }
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}
