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
    
    public partial class tblswatccexternalsupport
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public Nullable<int> fundApp { get; set; }
        public Nullable<int> fundAppSuccess { get; set; }
        public Nullable<int> govRights { get; set; }
        public Nullable<int> govWatPol { get; set; }
        public Nullable<int> govWatAnal { get; set; }
        public Nullable<int> extVisitTech { get; set; }
        public Nullable<int> extVisitAdmin { get; set; }
        public bool expertAccess1 { get; set; }
        public bool expertAccess2 { get; set; }
        public bool expertAccess3 { get; set; }
        public bool expertAccess4 { get; set; }
        public bool expertAccess5 { get; set; }
    
        public virtual lkpswatextvisitlu lkpswatextvisitlu { get; set; }
        public virtual lkpswatextvisitlu lkpswatextvisitlu1 { get; set; }
        public virtual lkpswatfundapplu lkpswatfundapplu { get; set; }
        public virtual lkpswatfundappsuccesslu lkpswatfundappsuccesslu { get; set; }
        public virtual lkpswatgovrightslu lkpswatgovrightslu { get; set; }
        public virtual lkpswatgovwatanallu lkpswatgovwatanallu { get; set; }
        public virtual lkpswatgovwatpollu lkpswatgovwatpollu { get; set; }
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}