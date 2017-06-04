
initialize("mapIframe", false);
loadDrawingManager("longitude", "latitude", resetGeographicFieldsTimer);
initUpdateGeographicFields("longitude", "latitude", "AutoUpdateGeoAttrs", "http://www.comap.ca/wmsserver2013/mapserv.exe?map=HydroSanitas.map&VERSION=1.1.1&LAYERS=COMAP_WMS", new Array(
    new Array("countryID", "RWDB", "AD1_NAME"),
    new Array("SubnationalID", "RWDB", "AD2_NAME"),
    new Array("regionID", "WB", "SBREGNTXT"),
    new Array("Settlement", "RWDB", "BND_NAME"),
    new Array("EcoZoneID", "BIOMES", "BIONAME"),
    new Array("ClimateID", "KG", "KGCLASS"),
    new Array("SoilID", "DDSMW", "DOMSOLITHO")
));