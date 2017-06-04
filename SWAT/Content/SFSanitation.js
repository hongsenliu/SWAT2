﻿$(document).ready(function () {
    $.showHideToilet = function () {
        var toiletAll = $.trim($('#toiletsAll option:selected').text());
        if (toiletAll === "Yes") {
            $('input[type=text]').val("");
            $('input[type=text]').prop('disabled', true);
            $('#question2').hide();
        }
        else {
            $('input[type=text]').prop('disabled', false);
            $('#question2').show();
        }
    };

    $('#toiletsAll').change($.showHideToilet);
    $.showHideToilet();
});