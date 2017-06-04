$(document).ready(function () {
    $.showHideQuestion2 = function () {
        var toiletAll = $.trim($('#hasData option:selected').text());
        if (toiletAll === "No") {
            $('input[type=text]').val("");
            $('input[type=text]').prop('disabled', true);
            $('#question2').hide();
        }
        else {
            $('input[type=text]').prop('disabled', false);
            $('#question2').show();
        }
    };

    $('#hasData').change($.showHideQuestion2);
    $.showHideQuestion2();
});