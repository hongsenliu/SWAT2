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
    
    public partial class tblswatcctrain
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public bool trainProf1 { get; set; }
        public bool trainProf2 { get; set; }
        public bool trainProf3 { get; set; }
        public bool trainProf4 { get; set; }
        public bool trainProf5 { get; set; }
        public bool trainTech1 { get; set; }
        public bool trainTech2 { get; set; }
        public bool trainTech3 { get; set; }
        public bool trainTech4 { get; set; }
        public bool trainTech5 { get; set; }
    
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}
