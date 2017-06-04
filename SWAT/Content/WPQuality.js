$(document).ready(function () {
    $.showHideQ2= function () {
        var ans = $.trim($('#qualTreated option:selected').text());
        if (ans == "No") {
            $('#userTreated').prop('disabled', false);
        }
        else {
            $('#userTreated').val("");
            $('#userTreated').prop('disabled', true);
            $('#userTreatmentMethod').val("");
            $('#userTreatmentMethod').prop('disabled', true);
        }
    }

    $.showHideQ3 = function () {
        var ans = $.trim($('#userTreated option:selected').text());
        if (ans == "Never" || ans == "") {
            $('#userTreatmentMethod').val("");
            $('#userTreatmentMethod').prop('disabled', true);
        }
        else {
            $('#userTreatmentMethod').prop('disabled', false);
        }
    }

    $('#qualTreated').change($.showHideQ2);
    $('#userTreated').change($.showHideQ3);
    $.showHideQ2();
    $.showHideQ3();
});