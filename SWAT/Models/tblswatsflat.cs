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
    
    public partial class tblswatsflat
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public bool LatrineType1 { get; set; }
        public bool LatrineType2 { get; set; }
        public bool LatrineType3 { get; set; }
        public bool LatrineType4 { get; set; }
        public Nullable<float> latrineProb1 { get; set; }
        public Nullable<float> latrineProb2 { get; set; }
        public Nullable<float> latrineProb3 { get; set; }
        public Nullable<int> latrineCondition { get; set; }
        public Nullable<int> latrinecentralPubClean { get; set; }
        public bool latrineAccessGroup1 { get; set; }
        public bool latrineAccessGroup2 { get; set; }
        public bool latrineAccessGroup3 { get; set; }
        public Nullable<int> latrinefeesCharged { get; set; }
        public Nullable<int> latrineFeesLimitAccess { get; set; }
        public Nullable<int> latrineFeesEnsureClean { get; set; }
        public Nullable<int> hhLatrineClean { get; set; }
        public Nullable<int> ltype1 { get; set; }
        public Nullable<int> ltype2 { get; set; }
        public Nullable<int> ltype3 { get; set; }
        public Nullable<int> ltype4 { get; set; }
    
        public virtual lkpswathhcleanlu lkpswathhcleanlu { get; set; }
        public virtual lkpswatlatrineconditionlu lkpswatlatrineconditionlu { get; set; }
        public virtual lkpswatpubcleanlu lkpswatpubcleanlu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu1 { get; set; }
        public virtual lkpswatyesnolu lkpswatyesnolu2 { get; set; }
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}
