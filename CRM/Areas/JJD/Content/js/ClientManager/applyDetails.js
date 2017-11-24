
function updateorder(bank,remark) {
    var orderId = $('#orderId').val();
    
    $.ajax({
        url: '/jiajudai/clientmanager/updateorder',
        data: { orderId: orderId, bankCode: bank ,remark:remark},
        success: function (data) {
            mui.toast(data.Message);
            if (data.Status) {
                window.location.href = '/jiajudai/clientmanager';
            }
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
            console.log(arg3);
        }
    });
}

var bankList = [{ value: 'nh', text: "农行" },
    { value: 'jh', text: "建行" },
    { value: 'gh', text: "工行" }];

// 订单分配银行
function assignBank() {
    var BANK_picker = new mui.PopPicker();
    BANK_picker.setData(bankList);
    BANK_picker.pickers[0].setSelectedIndex(2, 1300);
    BANK_picker.show(function (item) {
        console.log(item[0].value);
        updateorder(item[0].value,'');
    });
}

$().ready(function () {

    $('.orderBtn .check').on('tap', function () {
        assignBank();
    });

    $('.orderBtn .allot').on('tap', function () {
        $('.cancelOrder').show();
    });

    $('.off').on('tap', function () {
        $(this).parent().parent().parent().hide();
    });

    $('.sure').on('tap', function () {
        var orderId = $('#orderId').val();
        var remark = $('#txtRemark').text();
        updateorder(orderId, '', remark);
    });
});


