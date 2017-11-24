$().ready(function () {
    var classNa = parent.document.getElementById('check').className;
    $('.' + classNa + ">span").css('color', '#8662F8');
    $('.' + classNa + ">span:first-child").css('background-image', "url('/areas/jjd/content/images/navBar/" + classNa + "s.png')");
});
