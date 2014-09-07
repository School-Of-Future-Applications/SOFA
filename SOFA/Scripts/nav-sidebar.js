
$(document).ready(function () {
    nirs_off_except_active();

    $("ul.nav-sidebar > li.nir").click(function () {
        nirs_off_except(this);
    });
})

function nir_has_active(nir)
{
    return ($(nir).parent().children('li.nis.active').length > 0);
}

function nir_off(nir)
{
    var children = $(nir).parent().children('li.nis');
    var isVisible = $(children).is(":visible");

    if (isVisible) {
        children.toggle('slow', 'swing');
        $(nir).removeClass("active");
    }
}

function nir_on(nir)
{
    var children = $(nir).parent().children('li.nis');
    var isVisible = $(children).is(":visible");

    if (!isVisible)
        children.toggle('slow', 'swing');
    $(nir).addClass("active");
}

function nirs_off_except(nir)
{
    $("ul.nav-sidebar > li.nir").each(function (index, nirFor) {
        if (!$(nirFor).is(nir))
            nir_off(nirFor);
    });
    nir_on(nir);
}

function nirs_off_except_active()
{
    $("ul.nav-sidebar > li.nir").each(function (index, nir) {
        if (nir_has_active(nir) == false) {
            nir_off(nir);
        }
        else
            nir_on(nir);
    });
}