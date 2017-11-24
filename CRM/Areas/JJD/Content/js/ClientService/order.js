function updateOrderStatus(status) {

    $.ajax({
        url: '/jiajudai/clientservice/updateorder',
        data: { orderId: $('#orderId').val(), remark: $('#txtRemark').val(), status: status },
        success: function (data) {
            
            if (data.Status) {
                $('#txtRemark').val('');
                $('.off').parent().parent().parent().hide();
                window.location.href = "/jiajudai/clientservice";
            } 

            mui.toast(data.Message);
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
        }
    });
}

$().ready(function () {

    $('.orderBtn .check').on('tap', function () {
        updateOrderStatus(true);
    });

    $('.orderBtn .allot').on('tap', function () {
        $('.cancelOrder').show();
    });

    $('.off').on('tap', function () {
        $(this).parent().parent().parent().hide();
    });

    $('.sure').on('tap', function () {
        updateOrderStatus(false);
    });
});


