
// 订单分配银行
var bankList = [{value:'nh',text:"农行"},{value:'jh',text:"建行"},{value:'gh',text:"工行"}] 
function assignBank() {
				var BANK_picker = new mui.PopPicker();
				BANK_picker.setData(bankList);
				BANK_picker.pickers[0].setSelectedIndex(2, 1300);
				BANK_picker.show(function(item) {			
				});
	}
$(function(){
	$('.orderBtn .check').on('tap',function(){
		assignBank();
	})
	$('.orderBtn .allot').on('tap',function(){
		$('.cancelOrder').show();
	})
	$('.off').on('tap',function(){
		$(this).parent().parent().parent().hide();
	})
	})


