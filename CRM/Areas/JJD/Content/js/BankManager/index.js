function setAssignPolicy(status) {
    dialog.show();
    $.ajax({
        url: '/jiajudai/bankmanager/setassignpolicy',
        data: {isauto:status},
        success: function (data) {
            mui.toast(data.Message);
            dialog.hide();
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
            dialog.hide();
        }
    });
}

$().ready(function () {

	$('.onOff').on('tap', function() {
		var statusLabel = $('.onOff span').html();
		if(statusLabel == '自动分配关闭') {
			mui.confirm('开启自动分配，订单将自动分配给客户经理', '开启', function(e) {
				if(e.index == 1) {
					$('.onOff span').html('自动分配开启');
					$('.onOff img').attr('src', '~/Areas/JJD/Content/images/05.bankManager/u1734.png')

					setAssignPolicy(true);
				} else {
					return;
				}
			});
		} else {
			mui.confirm('关闭自动分配，订单将手动分配给客户经理', '关闭', function(e) {
				var btnArray = ['取消', '确定'];
				if(e.index == 1) {
				    $('.onOff img').attr('src', '~/Areas/JJD/Content/images/05.bankManager/u1720.png')
				    $('.onOff span').html('自动分配关闭');
				    
				    setAssignPolicy(false);
				} else {
					return;
				}
			});
		}
	});

});