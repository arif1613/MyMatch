/// <reference path="jquery-1.8.2.js" />
/// <reference path="jquery.validate-vsdoc.js" />
/// <reference path="_references.js" />
(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');

ga('create', 'UA-49383221-1', 'indianbibah.com');
ga('send', 'pageview');

$(function p() {
    $(':input[data-datepicker]').datepicker({
        dateFormat: 'd-M,yy',
        monthNamesShort: ['jan', 'feb', 'mar', 'apr', 'may', 'jun', 'jul', 'aug', 'sep', 'oct', 'nov', 'dec'],
        changeMonth: true,
        changeYear: true,
        yearRange: 'c-60:c+0',
        firstDay: 1 // The first day of the week, Sun = 0, Mon = 1, ...
    });
    $(':input[data-datepicker]').click(function () {
        $(this).blur();
    });
    $(":input[data-autocomplete]").each(function () {
        $(this).autocomplete({ source: $(this).attr("data-autocomplete") });
        });

        


    function moveFloatMenu1() {
        var menuOffset1 = menuYloc.top + $(this).scrollBottom() + "px";
        $("#link").animate({
            top: menuOffset1
        },
        {
            duration: 0,
            queue: false
        });
    }
    menuYloc = $("#link").offset();
    $(window).scroll(moveFloatMenu1);
    moveFloatMenu1();

    
});
