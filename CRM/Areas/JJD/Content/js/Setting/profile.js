//选择地区
function selectDistrict() {
    var PCA_picker = new mui.PopPicker({
        layer: 3
    });
    PCA_picker.setData(districts.list);
    PCA_picker.show(function (s) {
        //console.log(s[0].value);
        if (s[2].text == undefined) {
            s[2].text = ' ';
        }
        if (s[1].text == undefined || s[1].text == null)
            s[1].text = ' ';
        $('.prov input').val(s[0].text + ' ' + s[1].text + ' ' + s[2].text);
        $('.prov .province').val(s[0].text);
        $('.prov .city').val(s[1].text);
        $('.prov .country').val(s[2].text);

    });
}

$().ready(function () {

    $('.prov a').on('click', function () {
        selectDistrict();
    });

    $('.mui-bar-nav span').click(function () {
        $('#form1').submit();
    });

    $('input[name="genderchk"]').click(function () {
        var gender = $(this).hasClass('male') ? 'True' : 'False';

        $('#Gender').val(gender);
    });

});