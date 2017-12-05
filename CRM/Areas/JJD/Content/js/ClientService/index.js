
$(function() {
	$('.onOff').on('tap', function() {
		var statusLabel = $('.onOff span').html();
		if(statusLabel == '自动分配关闭') {
			mui.confirm('开启自动分配，订单将自动分配给客户经理', '开启', function(e) {
				if(e.index == 1) {
					$('.onOff span').html('自动分配开启');
					$('.onOff img').attr('src','../images/05.bankManager/u1734.png')
				} else {
					return;
				}
			});
		} else {
			mui.confirm('关闭自动分配，订单将手动分配给客户经理', '关闭', function(e) {
				var btnArray = ['取消', '确定'];
				if(e.index == 1) {
					$('.onOff img').attr('src','../images/05.bankManager/u1720.png')
					$('.onOff span').html('自动分配关闭');
				} else {
					return;
				}
			});
		}
	});
});