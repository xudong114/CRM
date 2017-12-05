/**
 * 获取手机验证码
 */
function getSecurityCode(phoneNo) {

    $('.degist span').off('click');
    var i = 60;
    var timer = setInterval(function () {
        i--;
        $('.degist span').html("" + i + "秒后重新获取");
        if (i <= 0) {
            $('.degist span').html("获取验证码");
            clearInterval(timer);
            timer = null;
            $('.degist span').on('click', onSecurityCodeClick);
        }
    }, 1000);

    $.ajax({
        type: "get",
        url: "/jiajudai/user/getsecuritycode",
        data: {
            phoneNo: phoneNo
        }, success: function (data) {
            if (!data.Status) {
                mui.toast(data.Message);
            }
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg3);
        }
    });
}
function login() {
    dialog.show({ title: '登录中...' });
    var username = $('#team1 #txtUserName').val();
    var password = $('#team1 #txtPassword').val();
    var data = { username: username, password: password };
    $.ajax({
        type: 'post',
        url: '/jiajudai/user/login',
        data: data,
        success: function (data) {
            if (data.Status) {
                window.location.href = data.Data;
            } else {
                mui.toast(data.Message);
            }
            dialog.hide();
        },
        error: function (arg1, arg2, arg3) {
            dialog.hide();
            console.log(arg1.responseText);
        }
    });
}

function quickLogin() {

    var username = $('#txtUserName2').val();
    var password = $('#txtPassword2').val();
    var data = { username: username, code: password };

    if (username === '' || password === '') {
        return mui.toast('手机号码或验证码为空');
    }

    dialog.show({ title: '登录中...' });
    $.ajax({
        type: 'post',
        url: '/jiajudai/user/quicklogin',
        data: data,
        success: function (data) {
            if (data.Status) {
                window.location.href = data.Data;
            } else {
                mui.toast(data.Message);
            }
            dialog.hide();
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg1.responseText);
            dialog.hide();
        }
    });
}

/**
 * 获取验证码处理事件
 */
function onSecurityCodeClick() {
    var phoneNo = $('#txtUserName2').val();
    if (phoneNo == '') {
        return mui.totast('请输入您的手机号码');
    }

    $.ajax({
        url: '/jiajudai/user/hasusername',
        data: { userName: phoneNo },
        success: function (data) {
            console.log(data);
            if (data == 'True') {
                getSecurityCode(phoneNo);
            } else {
                mui.toast('账号不存在');
            }
            console.log(data);
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg1);
        }

    });
}

$().ready(function () {

    $('.degist span').click(function () {
        onSecurityCodeClick();
    });

    $('#btnLogin').click(function () {
        var index = $('.loginMethod .tab>a.active').index();

        if (index == 0) {
            login();
        } else {
            quickLogin();
        }
    });

    $('.loginMethod .tab>a').on('click', function (e) {
        e.preventDefault();
        $(this).siblings('a').removeClass('active');
        $(this).addClass('active');
        var showId = $(this).attr('href');
        $(showId).siblings('div:not(.tab)').hide();
        $(showId).show();
    });

});