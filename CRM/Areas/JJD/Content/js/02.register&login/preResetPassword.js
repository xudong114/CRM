/**
 * 获取手机验证码
 */
function getSecurityCode() {

    $('.degist span').off('click');
    var i = 60;
    var timer = setInterval(function () {
        i--;
        $('.mui-content .mui-btn').css('background', '#0098EE')
        $('.degist span').html("获取验证码（" + i + "）")
        if (i <= 0) {
            $('.degist span').html("获取验证码");
            clearInterval(timer);
            timer = null;
            $('.degist span').on('click', onSecurityCodeClick);
        }
    }, 1000);

    var phoneNo = $('#txtPhoneNo').val();

    if (phoneNo === '')
        return;

    $.ajax({
        type: "get",
        url: "/jiajudai/user/getsecuritycode",
        data: {
            phoneNo: phoneNo
        },
        error: function (arg1, arg2, arg3) {
            console.lgo(arg3);
        }
    });
}

$().ready(function() {

	$('.degist span').on('click', function() {
		var userName = $('.num input').val();
		if(userName == '') {
			return mui.toast('请填写手机号码');
		}else{
			getSecurityCode()
		}
	});

	$('#btnNext').click(function () {
        $('#form1').submit();
	});

});