/**
 * 获取手机验证码
 */
function getSecurityCode() {

    $('.degist span').off('click');
    var i = 60;
    var timer = setInterval(function () {
        i--;
        $('.mui-content .mui-btn').css('background', '#8763F7');
        $('.degist span').html(i + "秒后重新获取");
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
        url: "/jiajudai/user/getsecuritycodecommon",
        data: {
            phoneNo: phoneNo
        },
        success: function (data) {
            if (!data.Status) {
                mui.totast(data.Message);
            }
            console.log(data.Message);
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg1);
        }
    });
}

/**
 * 获取验证码处理事件
 */
function onSecurityCodeClick() {
    var phoneNo = $('#txtPhoneNo').val();
    if (phoneNo == '') {
      return  mui.toast('请输入您的手机号码');
    }

    $.ajax({
        url: '/jiajudai/user/hasusername',
        data: { userName: phoneNo },
        success: function (data) {
            if (data == 'False') {
                getSecurityCode();
            } else {
                mui.toast('手机号码已存在');
            }
            console.log(data);
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg1);
        }

    });
}

function step01() {

    $.ajax({
        url: '/jiajudai/user/checksecuritycode',
        data: $('#form1').serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/user/register02?phoneno=' + data.Message;
            }
            console.log(data);
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg3);
        }
    });
}

/**
 * 下一步
 */
function goNext() {

    var username = $('#txtPhoneNo').val();
    var code = $('.degist input').val();
    var data = {
        phoneNo: username,
        code: code
    };
    if (data.phoneNo === ''
        || data.code === '') {
        return mui.totast('手机号码和验证码为空');
    }
    step01();
    //$('#form1').submit();
}

$().ready(function () {

    /**
	 * 调用手机验证码事件
	 */
    $('.degist span').on('click', function () {
        onSecurityCodeClick();
    });

    /**
	 * 下一步事件
	 */
    $('.mui-btn').on('click', function () {
        goNext();
    });

});