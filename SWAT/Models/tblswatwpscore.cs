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
    
    public partial class tblswatwpscore
    {
        public long ID { get; set; }
        public long wpID { get; set; }
        public int ScoreID { get; set; }
        public string ScoreName { get; set; }
        public Nullable<double> Value { get; set; }
        public string Description { get; set; }
    
        public virtual lkpswatwpscorelu lkpswatwpscorelu { get; set; }
        public virtual tblswatwpoverview tblswatwpoverview { get; set; }
    }
}
