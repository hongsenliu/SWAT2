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
    
    public partial class lkpcountry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public lkpcountry()
        {
            this.lkpsubnationals = new HashSet<lkpsubnational>();
            this.tblswatlocations = new HashSet<tblswatlocation>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> CurrencyID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public string CommonName { get; set; }
    
        public virtual lkpcurrency lkpcurrency { get; set; }
        public virtual lkpregion lkpregion { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<lkpsubnational> lkpsubnationals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatlocation> tblswatlocations { get; set; }
    }
}