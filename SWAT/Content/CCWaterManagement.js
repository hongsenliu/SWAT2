$(document).ready(function () {
    $.showHideWaterAdmin = function () {
        var waterCom = $.trim($('#watCom option:selected').text());

        if (waterCom == "There is no Water Committee")
        {
            $('#watAdminOther').prop('disabled', false);
            for (i = 0; i < 5; i++) {
                $('#watRecords' + (i + 1)).val("");
                $('#watRecords' + (i+1)).prop('disabled', true);
            }
        }
        else
        {
            $('#watAdminOther').val("");
            $('#watAdminOther').prop('disabled', true);
            for (i = 0; i < 5; i++) {
                $('#watRecords' + (i+1)).prop('disabled', false);
            }
        }

        $('#watAdminOther').attr('cols', 80);
        $('#watAdminOther').attr('rows', 6);
    }

    $('#watCom').change($.showHideWaterAdmin);
    $.showHideWaterAdmin();
});