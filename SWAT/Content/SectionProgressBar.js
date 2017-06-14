$(document).ready(function () {
    $.showProgress = function () {
        //var toiletAll = $.trim($('#hasData option:selected').text());
        //if (toiletAll === "No") {
        //    $('input[type=text]').val("");
        //    $('input[type=text]').prop('disabled', true);
        //    $('#question2').hide();
        //}
        //else {
        //    $('input[type=text]').prop('disabled', false);
        //    $('#question2').show();
        //}\\
        for (i = 1; i < currentSectionID; i++) {
            
            if (i == 4) {
                if (hasSection4 === 'true') {
                    $('#sec' + i).show();
                }
            } else {
                $('#sec' + i).show();
            }
            $('#sec' + i + ' .progress-bar').addClass('progress-bar-success');
        }
        $('#sec' + currentSectionID).show();
        $('#sec' + currentSectionID + ' .progress-bar').addClass('progress-bar-info');
    };

    //$('#hasData').change($.showHideQuestion2);
    if (typeof currentSectionID != 'undefined') 
    {
        $.showProgress();
    }
    
});