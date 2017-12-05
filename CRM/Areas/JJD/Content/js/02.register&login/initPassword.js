function step02() {

    $.ajax({
        url: '/jiajudai/user/register',
        data: $('#form1').serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/';
            }
            console.log(data);
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg3);
        }
    });
}

/**
 * @description 注册用户
 */
function register() {

    var password = $('.ps input').val();
    var confirmPassword = $('.psag input').val();
    if (password.length < 6 || password.length > 18) {
        return mui.toast('密码长度应在6~18个字符之间');
    }
    if (password !== confirmPassword) {
        return mui.toast('两次输入的密码不一致');
    }

    step02();
}

$().ready(function () {

    $('.ps input , .psag input').blur(function () {
        var password = $('.ps input').val();
        var confirmPassword = $('.psag input').val();

        console.log(password);
        console.log(confirmPassword);

        if (password !== ''
            && confirmPassword !== '') {
            $('.mui-content .mui-btn').css('background-color', '#8763F7');
        } else {
            $('.mui-content .mui-btn').removeAttr('style');
        }
    });

    $('.mui-btn').on('click', function () {
        var color = $('.mui-content .mui-btn').css('background-color');//, '#8763F7')
        if (color !== 'rgb(135, 99, 247)') {
            return false;
        }
        register();
    });
});