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
    
    public partial class tblswatccindig
    {
        public int ID { get; set; }
        public int SurveyID { get; set; }
        public Nullable<int> indigPop { get; set; }
        public Nullable<double> indigPopPerCapita { get; set; }
        public Nullable<int> longtermPop { get; set; }
        public Nullable<double> longtermPopPerCapita { get; set; }
    
        public virtual tblswatsurvey tblswatsurvey { get; set; }
    }
}
