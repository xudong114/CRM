
function updateorder(status) {
    var remark = $('#txtRemark').val();
    var orderId = $('#orderId').val();

    $.ajax({
        url: '/jiajudai/bankmanager/updateorder',
        data: { orderId: orderId,status:status,remark:remark},
        success: function (data) {
            mui.toast(data.Message);
            if (data.Status) {
                window.location.href = '/jiajudai/bankmanager';
            }
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
            console.log(arg3);
        }
    });
}

$().ready(function () {

    $('.orderBtn .check a').click(function () {
        updateorder(true);
    });

    $('.orderBtn .allot').on('click', function () {
        $('.cancelOrder').show();
    });

    $('.off').on('click', function () {
        $(this).parent().parent().parent().hide();
    });

    $('.sure').on('click', function () {
        updateorder(false);
    });
});


