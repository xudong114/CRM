/**
 * @description 获取安全码
 */
function getSecurityCode() {
    var phoneNo = $('#PersonalPhone').val();
    if (!isPhoneNo(phoneNo)) {
        return mui.toast('手机号码格式错误');
    }

	$('.degist span').off('click');

	$.ajax({
	    type: "get",
	    url: "/jiajudai/user/getsecuritycodecommon",
	    data: {
	        phoneNo: phoneNo,
	        checkUser:false
	    },
        success:function(data){
            if (!data.Status) {
                mui.toast(data.Message);
            } else {
                var i = 60;
                var timer = setInterval(function () {
                    i--;
                    $('.mui-content .mui-btn').css('background', '#8763f7');
                    $('.degist span').html(i + "秒后重新获取");
                    if (i <= 0) {
                        $('.degist span').html("获取验证码");
                        clearInterval(timer);
                        timer = null;
                        $('.degist span').on('click', getSecurityCode);
                    }
                }, 1000);
            }
        },
        error: function (arg1, arg2, arg3) {
            $('.degist span').on('click', getSecurityCode);
	        console.log(arg3);
	    }
	});
}

function updateOrder() {

    $.ajax({
        type:'post',
        url: '/jiajudai/order/step02',
        data: $('#form1').serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/order/step03?orderId='+data.Data;
            } else {
                mui.toast(data.Message);
            }
        },
        error: function (arg1, arg2, arg3) {
            console.log(arg3);
        }

    });
}

/**
 * @description 下一步
 */
$().ready(function () {

	$('.degist span').on('click', function() {
		getSecurityCode();
	});

	$('#btnNext').click(function () {
	   
	    var idImg = $('#IDNoFaceImg').val() + '|' + $('#IDNoBackImg').val();
        $('#IDImg').val(idImg);

        var color = $('.mui-content .mui-btn').css('background-color');
        if (color == 'rgb(215, 215, 215)') {
            return;
	    }

        if ($('#IDImg').val() == ''
            || $('#IDImg').val() == '|') {
            return mui.toast('请上传身份证照片');
        }

        if (!isIDNO($('#IDNo').val())) {
            return mui.toast('身份证号码格式错误');
        }

        if (!isPhoneNo($('#PersonalPhone').val())) {
            return mui.toast('手机号码格式错误');
        }
        updateOrder();
    });

    $('#fileIDNoFaceImg').change(function () {

        var file = $(this)[0].files[0];
        if (!file) {
            mui.toast('未选择任何文件');
            return;
        }
        dialog.hide();
        dialog.show({ title: '正在上传文件...' });
        $('#facePreview').attr('src', File.getObjectURL(file));

        var option = {
            file: file,
            width: 500,
            callback: function (data) {
                if (!data.url) {
                    return;
                }

                $('#IDNoFaceImg').val(data.url);

                dialog.hide();
                dialog.show({ title: '正在识别身份证...' });
                console.log(data.url);
                $.ajax({
                    url: '/jiajudai/ocr/getidcardinfo',
                    data: { filename: data.url },
                    dataType: "json",
                    success: function (data) {
                        dialog.hide();
                        var result = 'kOtherError:Algorithm run failed  - face results are all empty';
                        if (data == result) {
                            return mui.toast('无法识别图片，请重新上传');
                        }

                        data = JSON.parse(data);
                        var cardinfo = JSON.parse(data.outputs[0].outputValue.dataValue);

                        if (cardinfo.num == '') {
                            mui.toast('自动识别失败，请填写身份证号码');
                        } else {
                            mui.toast('身份证识别成功');
                            $('#IDNo').val(cardinfo.num);
                        }

                    },
                    error: function (arg1, arg2, arg3) {
                        dialog.hide();
                    }
                });

            },
            error: function (arg1, arg2, arg3) { }
        };

        File.send(option);

    });

    $('#fileIDNoBackImg').change(function () {

        var file = $(this)[0].files[0];
        if (!file) {
            mui.toast('未选择任何文件');
            return;
        }
        dialog.hide();
        dialog.show({ title: '正在上传文件...' });
        $('#backPreview').attr('src', File.getObjectURL(file));

        var option = {
            file: file,
            width: 500,
            callback: function (data) {

                if (data.url) {
                    $('#IDNoBackImg').val(data.url);
                }
                dialog.hide();

            }, error: function (arg1, arg2, arg3) {
                dialog.hide();
            }
        };

        File.send(option);
    });

});