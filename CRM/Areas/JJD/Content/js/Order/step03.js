/**
 *@description 选择省市区/县 
 */
function selectDistrict() {
	var PCA_picker = new mui.PopPicker({
		layer: 3
	});

	PCA_picker.setData(districts.list);

	PCA_picker.show(function(s) {

		if(s[2].text == undefined) {
			s[2].text = ' ';
		}

		if(s[1].text == undefined || s[1].text == null)
			s[1].text = ' ';
		$('.prov input').val(s[0].text + ' ' + s[1].text + ' ' + s[2].text); 
		$('.prov .province').val(s[0].text);
		$('.prov .city').val(s[1].text);
		$('.prov .country').val(s[2].text);

	});
}

function updateOrder() {

    $.ajax({
        type: 'post',
        url: '/jiajudai/order/step03',
        data: $('#form1').serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/order/step04?orderId=' + data.Data;
            } else {
                mui.toast(data.Message);
            }
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg3);
        }

    });
}


$().ready(function () {

    $('.prov').on('tap', function () {
        selectDistrict();
    });

    $('.houseBtn div a').on('click', function () {
        $(this).siblings('a').removeClass('active');
        $(this).addClass('active');
        if ($(this).attr('href') === '#no') {
            $('.nameNum').hide();
            $('#HasHouse').val('False');
        } else {
            $('.nameNum').show();
            $('#HasHouse').val('True');
        }
    });

    $('#btnNext').click(function () {
        updateOrder();
    });
});
