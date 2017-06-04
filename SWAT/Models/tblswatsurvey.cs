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
    
    public partial class tblswatsurvey
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblswatsurvey()
        {
            this.tblswatbackgroundinfoes = new HashSet<tblswatbackgroundinfo>();
            this.tblswatcccoms = new HashSet<tblswatcccom>();
            this.tblswatccedus = new HashSet<tblswatccedu>();
            this.tblswatccexternalsupports = new HashSet<tblswatccexternalsupport>();
            this.tblswatccfinancials = new HashSet<tblswatccfinancial>();
            this.tblswatccgenders = new HashSet<tblswatccgender>();
            this.tblswatccindigs = new HashSet<tblswatccindig>();
            this.tblswatccschools = new HashSet<tblswatccschool>();
            this.tblswatccsocials = new HashSet<tblswatccsocial>();
            this.tblswatcctrains = new HashSet<tblswatcctrain>();
            this.tblswatccwatermanagements = new HashSet<tblswatccwatermanagement>();
            this.tblswathppcoms = new HashSet<tblswathppcom>();
            this.tblswathppkhps = new HashSet<tblswathppkhp>();
            this.tblswatsfcentrals = new HashSet<tblswatsfcentral>();
            this.tblswatsflats = new HashSet<tblswatsflat>();
            this.tblswatsfods = new HashSet<tblswatsfod>();
            this.tblswatsfpoints = new HashSet<tblswatsfpoint>();
            this.tblswatsfsanitations = new HashSet<tblswatsfsanitation>();
            this.tblswatsfseptics = new HashSet<tblswatsfseptic>();
            this.tblswatswpags = new HashSet<tblswatswpag>();
            this.tblswatswpdevs = new HashSet<tblswatswpdev>();
            this.tblswatswpls = new HashSet<tblswatswpl>();
            this.tblswatwaannualprecips = new HashSet<tblswatwaannualprecip>();
            this.tblswatwaclimatechanges = new HashSet<tblswatwaclimatechange>();
            this.tblswatwaextremeevents = new HashSet<tblswatwaextremeevent>();
            this.tblswatwagroundwaters = new HashSet<tblswatwagroundwater>();
            this.tblswatwamonthlyquantities = new HashSet<tblswatwamonthlyquantity>();
            this.tblswatwaprecipitations = new HashSet<tblswatwaprecipitation>();
            this.tblswatwariskpreps = new HashSet<tblswatwariskprep>();
            this.tblswatwasurfacewaters = new HashSet<tblswatwasurfacewater>();
            this.tblswatwpoverviews = new HashSet<tblswatwpoverview>();
        }
    
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<short> Status { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int LocationID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatbackgroundinfo> tblswatbackgroundinfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatcccom> tblswatcccoms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccedu> tblswatccedus { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccexternalsupport> tblswatccexternalsupports { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccfinancial> tblswatccfinancials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccgender> tblswatccgenders { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccindig> tblswatccindigs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccschool> tblswatccschools { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccsocial> tblswatccsocials { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatcctrain> tblswatcctrains { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatccwatermanagement> tblswatccwatermanagements { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswathppcom> tblswathppcoms { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswathppkhp> tblswathppkhps { get; set; }
        public virtual tblswatlocation tblswatlocation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsfcentral> tblswatsfcentrals { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsflat> tblswatsflats { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsfod> tblswatsfods { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsfpoint> tblswatsfpoints { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsfsanitation> tblswatsfsanitations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatsfseptic> tblswatsfseptics { get; set; }
        public virtual userid userid1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatswpag> tblswatswpags { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatswpdev> tblswatswpdevs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatswpl> tblswatswpls { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwaannualprecip> tblswatwaannualprecips { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwaclimatechange> tblswatwaclimatechanges { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwaextremeevent> tblswatwaextremeevents { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwagroundwater> tblswatwagroundwaters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwamonthlyquantity> tblswatwamonthlyquantities { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwaprecipitation> tblswatwaprecipitations { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwariskprep> tblswatwariskpreps { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwasurfacewater> tblswatwasurfacewaters { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblswatwpoverview> tblswatwpoverviews { get; set; }
    }
}