
using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;

namespace SWAT.Models
{
    [MetadataType(typeof(WPSwprMetadata))]
    public partial class tblswatwpswpr
    {
    }

    [MetadataType(typeof(WPSupplyMetadata))]
    public partial class tblswatwpsupply
    {
    }

    [MetadataType(typeof(WaterPointMetadata))]
    public partial class tblswatwpoverview
    {
    }

    [MetadataType(typeof(SFLatMetadata))]
    public partial class tblswatsflat
    {
    }

    [MetadataType(typeof(SFSepticMetadata))]
    public partial class tblswatsfseptic
    {
    }

    [MetadataType(typeof(SFCentralMetadata))]
    public partial class tblswatsfcentral
    {
    }

    [MetadataType(typeof(SFPointMetadata))]
    public partial class tblswatsfpoint
    {
    }

    [MetadataType(typeof(SFOdMetadata))]
    public partial class tblswatsfod
    {
    }

    [MetadataType(typeof(SFSanitationMetadata))]
    public partial class tblswatsfsanitation
    { 
    }

    [MetadataType(typeof(HPPKhpMetadata))]
    public partial class tblswathppkhp
    { 
    }

    [MetadataType(typeof(SWPDevMetadata))]
    public partial class tblswatswpdev
    {
    }

    [MetadataType(typeof(SWPAgMetadata))]
    public partial class tblswatswpag
    { 
    }

    [MetadataType(typeof(SWPLivestockMetadata))]
    public partial class tblswatswpl
    { 
    }

    [MetadataType(typeof(CCWaterManagementMetadata))]
    public partial class tblswatccwatermanagement
    { 
    }

    [MetadataType(typeof(CCExternalMetadata))]
    public partial class tblswatccexternalsupport
    { 
    }

    [MetadataType(typeof(CCComMetada))]
    public partial class tblswatcccom
    { 
    }

    [MetadataType(typeof(CCSocialMetadata))]
    public partial class tblswatccsocial
    { 
    }

    [MetadataType(typeof(CCGenderMetadata))]
    public partial class tblswatccgender
    { 
    }

    [MetadataType(typeof(CCFinancialMetadata))]
    public partial class tblswatccfinancial
    { 
    }

    [MetadataType(typeof(CCIndigMetadata))]
    public partial class tblswatccindig
    {
    }

    [MetadataType(typeof(CCSchoolMetadata))]
    public partial class tblswatccschool
    { 
    }

    [MetadataType(typeof(CCTrainMetadata))]
    public partial class tblswatcctrain
    {
    }

    [MetadataType(typeof(CCEducationMetadata))]
    public partial class tblswatccedu
    { 
    }

    [MetadataType(typeof(WAGroundWaterMetadata))]
    public partial class tblswatwagroundwater
    {
    }

    [MetadataType(typeof(WASurfaceWaterMetadata))]
    public partial class tblswatwasurfacewater
    { 
    }

    [MetadataType(typeof(WARiskPrepMetadata))]
    public partial class tblswatwariskprep
    { 
    }

    [MetadataType(typeof(LocationMetadata))]
    public partial class tblswatlocation
    {
    }

    [MetadataType(typeof(BackgroundMetadata))]
    public partial class tblswatbackgroundinfo
    { 
    }

    [MetadataType(typeof(WAPrecipitationMetadata))]
    public partial class tblswatwaprecipitation
    {
    }

    [MetadataType(typeof(waannualprecipMetadata))]
    public partial class tblswatwaannualprecip
    { 
    }

    [MetadataType(typeof(WAClimateChangeMetadata))]
    public partial class tblswatwaclimatechange
    { 
    }

    [MetadataType(typeof(WAExtremeEventMetadata))]
    public partial class tblswatwaextremeevent
    { 
    }

    
    //public partial class swatEntities
    //{
    //    // TODO uncomment this part to deploy
    //    //private void setDBpw(string password)
    //    //{
    //    //    var settings = System.Configuration.ConfigurationManager.ConnectionStrings[2];
    //    //    var fi = typeof(System.Configuration.ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
    //    //    fi.SetValue(settings, false);
    //    //    string connStr = settings.ConnectionString;
    //    //    int insertIndex = connStr.IndexOf("databasename");
    //    //    connStr = connStr.Insert(insertIndex, "password=" + password + ";");
    //    //    settings.ConnectionString = connStr;
    //    //    fi.SetValue(settings, true);
    //    //}
    //}
}