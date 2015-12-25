$(document).ready(function () {
    var elementName = $('#menu_active').attr("menuItem");
    elementName = "#" + elementName + "-menu-item";
    $(elementName).addClass("active");
});