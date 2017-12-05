
/**
 * 重置密码
 */
function resetPassWord() {
	var password = $('.ps input').val();
	var confirmPassword = $('.psag input').val();
	if(password !== confirmPassword) {
		return mui.toast('两次输入不一致');
	}

	$('#form1').submit();
}

$().ready(function() {

	$('.ps input').blur(function() {
		$('.mui-content .mui-btn').css('background', '#8763F7');
	});

	$('.mui-btn').on('click', function() {
		resetPassWord();
	});

});