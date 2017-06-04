$(document).ready(function () {
    $(document).ajaxStart(function () {
        $("#wpReport").hide();
        $("#allloading").show();
    });
    $(document).ajaxComplete(function () {
        $("#allloading").hide();
        $("#wpReport").show();
    });

    $.getWPReport = function () {
        var url = "/Survey/_WPBarColumn/" + $("#ID").val() + "?wpID=" + $("#wpList").val();
        $("#wpReport").load(url);
    }

    //$.getWPReport = function () {
    //    var url = "/Survey/_WPBarColumn/" + $("#ID").val() + "?wpID=" + $("#wpList").val();
    //    $.ajax({
    //        url: url,
    //        cache: false,
    //        type: "GET",
    //        dataType: "html",
    //        success: function (data, textStatus, XMLHttpRequest) {
    //            SetWPReportData(data);
    //        }
    //    });
    //    
    //}

    //$.getWPMetricReport = function () {
    //    var url = "/Survey/_WPGroupBarColumn/" + $("#ID").val() + "?wpID=" + $("#wpList").val() + "&metricID=" + $("#metricList").val();
    //    $.ajax({
    //        url: url,
    //        cache: false,
    //        type: "GET",
    //        dataType: "html",
    //        success: function (data, textStatus, XMLHttpRequest) {
    //            SetWPMetricReportData(data);
    //        }
    //    });
    //}

    //function SetWPReportData(data) {
    //    $("#wpReport").html(data); // HTML DOM replace
    //    $.getWPMetricReport();
    //}

    //function SetWPMetricReportData(data) {
    //    $("#wpDetailsReport").html(data);
    //}

    $('#wpList').change($.getWPReport);
    $.getWPReport();
    //$('#metricList').change($.getWPMetricReport);
});