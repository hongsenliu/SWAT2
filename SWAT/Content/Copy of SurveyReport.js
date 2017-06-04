$(document).ready(function () {
    
    //$(document).ajaxStop(function () { hideLoading(); });
    $(document).ajaxStart(function () {
        $("#report").hide();
        $("#loading").show();
    });
    $(document).ajaxComplete(function () {
        $("#loading").hide();
        $("#report").show();
    });
    $.getReport = function () {
        var url = "/Survey/_BarColumn/" + $("#ID").val() + "?reportID=" + $("#ReportList").val();
        $("#report").load(url);
    }

    

    //$.getReport = function () {
    //    var url = "/Survey/_BarColumn/" + $("#ID").val() + "?reportID=" + $("#ReportList").val();
    //    $.ajax({
    //        url: url,
    //        cache: false,
    //        type: "GET",
    //        dataType: "html",
    //        success: function (data, textStatus, XMLHttpRequest) {
    //            SetData(data);
    //        }
    //    });
    //}
    
    //function SetData(data) {
    //    $("#report").html(data); // HTML DOM replace
    //}

    $('#ReportList').change($.getReport);
    $.getReport();
});