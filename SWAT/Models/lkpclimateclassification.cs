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
    
    public partial class lkpclimateclassification
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lkpclimateclassification()
        {
            this.tblswatbackgroundinfoes = new HashSet<tblswatbackgroundinfo>();
        }
    
        public int ID { get; set; }
        public string CCType { get; set; }
        public string CCDescription { get; set; }
        public string CCCriterion { get; set; }
        public Nullable<int> CCOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatbackgroundinfo> tblswatbackgroundinfoes { get; set; }
    }
}