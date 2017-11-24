 progressBar(2)

function setOrderAudit(order) {
	var list = order.OrderRecordList;

	if(list.length == 0) {
		$('#OrderAuditRecord').hide();
	} else {
		$('#OrderAuditRecord').show();
	}
	var item = null;
	for(var i = 0; i < list.length; i++) {
		item = list[i];

		switch(item.Status) {

			case 4:
				{
					$('#OrderGojiajuStatus').html(item.StatusLabel);
					$('#OrderGojiajuDate').html(item.AuditDateTime);
					$('#OrderGojiajuOfficePhone').html(item.OfficePhone);
					$('#OrderGojiajuRemark').html(item.Remark);
					$('#OrderGojiajuOfficePhone').next().attr('href', 'tel:' + item.OfficePhone);
				}
				break;
			case 8:
				{
					$('#OrderGojiajuStatus').addClass('warning');
					$('#OrderGojiajuStatus').html(item.StatusLabel);
					$('#OrderGojiajuDate').html(item.AuditDateTime);
					$('#OrderGojiajuOfficePhone').html(item.OfficePhone);
					$('#OrderGojiajuRemark').html(item.Remark);
					$('#OrderGojiajuOfficePhone').next().attr('href', 'tel:' + item.OfficePhone);
				}
				break;
			case 16:
				{
					$('#OrderBankStatus').html(item.StatusLabel);
					$('#OrderBankDate').html(item.AuditDateTime);
					$('#OrderBankName').html(order.BankName);
					$('#OrderBankOfficePhone').html(item.OfficePhone);
					$('#OrderBankRemark').html(item.Remark);
					$('#OrderBankOfficePhone').next().attr('href', 'tel:' + item.OfficePhone);
				}
				break;
			case 32:
				{
					$('#OrderBankStatus').addClass('warning');
					$('#OrderBankStatus').html(item.StatusLabel);
					$('#OrderBankDate').html(item.AuditDateTime);
					$('#OrderBankName').html(order.BankName);
					$('#OrderBankOfficePhone').html(item.OfficePhone);
					$('#OrderBankRemark').html(item.Remark);
					$('#OrderBankOfficePhone').next().attr('href', 'tel:' + item.OfficePhone);
				}
				break;
		}
	}
}

function renderOrder(data) {
	defaultOption.province = data.Province;
	defaultOption.city = data.City;
	defaultOption.country = data.Country;

	$('.orderId').html(data.Code);
	$('.applyDate').html(data.CreatedDateLabel);
	$('.applyMony').html(data.LoanAmount);
	$('.userName').val(data.Name);
	$('.phone').html(data.PersonalPhone);
	$('#IDNo').val(data.IDNo);
	$('#community').val(data.Community);
	if(data.Country == undefined) {
		data.Country = '';
	}
	$('#province .province').html(data.Province);
	$('#province .city').html(data.City);
	$('#province .country').html(data.Country);
	$('#landlord').val(data.Landlord);
	var isInstallment = false;
	if(data.IsInstallment) {
		$('.mui-switch').addClass('mui-active');
	} else {
		$('.mui-switch').removeClass('mui-active');
	}
	$('.brankId').html(data.FromBank);
	$('.store').html(data.StoreName);
	$('#StoreCode').val(data.StoreCode);
	$('.clerkCode').html(data.ClerkCode);
	$('#totalAmount').val(data.TotalAmount);
	$('#downpaymentAmount').val(data.DownpaymentAmount);
	$('#Status').val(data.Status);

	if(data.Status == 0) {
		$('#tempTip').show();
	}
}

var defaultOption = {};
var code = null;
var addImdId = null;
var orderId = null;
/**
 * @description 查询订单详情 
 */
function getOrder() {
	mui.ajax2({
		url: '/api/order/getcomplexorder',
		data: {
			orderId: orderId
		},
		success: function(complexOrder) {

			var data = complexOrder.Data;

			renderOrder(data);
			setOrderAudit(data);

		},
		error: function(arg1, arg2, arg3) {
			mui.toast('加载数据失败');
		}
	});
}
/**
 * @description 选择贷款金额
 */
function change() {
	$('.alert>div h1 b').html($('.mui-input-range input').val());
}
/**
 *@description 选择省市区/县 
 */
function selectDistrict() {
	var PCA_picker = new mui.PopPicker({
		layer: 3
	});
	PCA_picker.setData(districts.list);
	PCA_picker.show(function(s) {
		//console.log(s[0].value);
		if(s[2].text == undefined) {
			s[2].text = ' ';
		}
		if(s[1].text == undefined || s[1].text == null)
			s[1].text = ' ';
		$('.areainput').val(s[0].text + ' ' + s[1].text + ' ' + s[2].text); //申请页
		$('.province').html(s[0].text);
		$('.city').html(s[1].text);
		$('.country').html(s[2].text);
	});
}
$(function() {
	$('#province').on('tap', function() {
		selectDistrict()
	})
	$('#oneMone').on('tap', function() {
		$('.alert').show();
	});
	$('.alert .mui-btn').on('tap', function() {
		$('#oneMone span').html($('.mui-input-range input').val());
		$('.alert').css('display', 'none');
	});
	/**
	 * @description 删除图片 
	 */
	$('.pic').on('tap', '.getCmr span', function() {
		var imgId = $(this).attr('data-imgId');
		deleteImg(imgId);
		$(this).parent().remove();
	});
	/**
	 * @description 显示和关闭预览图
	 */
	$('.pic').on('tap', 'img:not(.addImg img)', function() {
		$('#alertPic img').attr('src', $(this).attr('src'));
		$('#alertPic').show();
	});
	$('#alertPic').on('tap', function() {
		$(this).hide();
	});
})