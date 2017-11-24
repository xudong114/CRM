/**
 * @description 获取银行客户经理
 */
function selectManager() {
    var pickerManager = new mui.PopPicker({
        layer: 1
    });
    var manager = [{
        value: 1,
        text: '经理一'
    }, {
        value: 2,
        text: '经理二'
    }, {
        value: 3,
        text: '经理三'
    }]
    pickerManager.setData(manager);
    pickerManager.show(function (SelectedItem) {
        console.log(SelectedItem);
    })
}

function pickDate() {
    var myDay = new Date();
    var years = myDay.getFullYear();
    var mouth = myDay.getMonth();
    var dtPicker = new mui.DtPicker({
        type: 'month',
        endDate: new Date(years, mouth),
        beginDate: new Date(2010, 1)
    });
    dtPicker.show(function (date) {
        $('.dates li a span').html(date.y.text + '-' + date.m.text);
        $('#Month').val(date.y.text + '-' + date.m.text);
    });
}

$().ready(function () {

    $('.manager').on('click', function () {
        selectManager();
    })

    $('.progress ul li').on('tap', function () {
        $(this).siblings('li').removeClass('active');
        $(this).toggleClass('active');
        if ($(this).hasClass('active')) {
            var code = $(this).attr('code');
            console.log(code);
            $(this).parent().next().val(code);
        } else {
            $(this).parent().next().val('');
        }
    });

    $('.dates li').on('tap', function () {
        pickDate();
    });

    $('body').on('tap', '#AllClerks li', function () {

        $(this).siblings().removeClass('active');
        $(this).toggleClass('active');
        
        $('#productcode').val(code);

    });

});