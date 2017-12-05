/**
 * 前端上传图片压缩 1.0
 * 
 * http://www.gojiaju.com
 * 
 * Copyright 2017, Go佳居
 * 
 * Author: Zhao Jia Hong
 * Updated:
 *          Date: 0000-00-00 Name:xxx
 *
 * Licensed under MIT
 * 
 * Released on: Nov 7, 2017
 */
(function ($, window) {

    /* 
     * 获取上传图片的路径
     * @param file
     *  获取file input上传的文件
     */
    function getObjectURL(file) {
        var url = null;
        if (window.createObjectURL != undefined) {  // basic
            url = window.createObjectURL(file);
        } else if (window.URL != undefined) {       // mozilla(firefox)
            url = window.URL.createObjectURL(file);
        } else if (window.webkitURL != undefined) { // web_kit or chrome
            url = window.webkitURL.createObjectURL(file);
        }
        return url;
    }

    /*
     * 获取图片base64编码
     * @param option 
     *      width:指定图片宽度
     *      height:指定图片高度
     *      rate:按比例压缩率
     * @param callback 回调函数
     *  base64:返回图片经过压缩的base64编码数据
     * @param outputFormat 输出文件的MIME
    */
    function getBase64(option, callback, outputFormat) {
        var canvas = document.createElement('CANVAS');
        var ctx = canvas.getContext('2d');
        var img = new Image;
        img.crossOrigin = 'Anonymous';
        img.onload = function () {
            var width = img.width;
            var height = img.height;
            var rate = 1;
            if (option.rate) {
                rate = (width < height ? width / height : height / width) / parseFloat(option.rate);

            } else if (option.width) {
                if (parseInt(option.width) > width)
                    option.width
                rate = width / parseInt(option.width);
                width = parseInt(option.width);
                height = height / rate;
                if (option.height) {
                    height = parseInt(option.height);
                }
            } else if (option.height) {
                rate = height / parseInt(option.height);
                height = parseInt(option.height);
                width = width / rate;
            }

            canvas.width = width;
            canvas.height = height;
            ctx.drawImage(img, 0, 0, width * rate, height * rate, 0, 0, width, height);

            var dataURL = canvas.toDataURL(option.format || 'image/png');
            callback.call(this, dataURL);
            canvas = null;
        };
        img.src = getObjectURL(option.file);
    }

    /**
     * 将以base64的图片url数据转换为Blob
     * @param urlData
     *            用url方式表示的base64图片数据
     */
    function base64ToBlob(urlData) {

        var bytes = window.atob(urlData.split(',')[1]); //去掉url的头，并转换为byte

        //处理异常,将ascii码小于0的转换为大于0
        var ab = new ArrayBuffer(bytes.length);
        var ia = new Uint8Array(ab);
        for (var i = 0; i < bytes.length; i++) {
            ia[i] = bytes.charCodeAt(i);
        }

        return new Blob([ab], {
            type: 'image/png'
        });
    }

    function process(option) {
        
        getBase64(option, function (base64) {
            var blob = base64ToBlob(base64);
            var formData = new FormData();
            //convertBase64UrlToBlob函数是将base64编码转换为Blob
            formData.append("imageName", blob);
            //append函数的第一个参数是后台获取数据的参数名,
            //和html标签的input的name属性功能相同

            //ajax 提交form
            $.ajax({
                type:'post',
                url: '/jiajudai/files/upload?floder=jiajudai',
                data: formData,
                processData: false, // 告诉jQuery不要去处理发送的数据
                contentType: false, // 告诉jQuery不要去设置Content-Type请求头
                dataType: 'json',
                success: function (data) {
                    //console.log(data);
                    //console.log(data.url);
                    //data.blob = blob;
                    option.callback && option.callback(data);
                },
                error: function (arg1, arg2, arg3) {
                    option.error && option.error(arg1, arg2, arg3);
                    //console.log(arg3);
                }
            });

        });
    }

    function send(option) {
        if (!option.file) {
           // throw  Error("文件为空");
        } else {
            process(option);
        }
    }

    window.File = {
        /*
         * @param option 
         *      file: file input上传的文件
         *      width:指定图片宽度
         *      height:指定图片高度
         *      rate:按比例压缩率
         *      format:输出文件MIME
         * @param callback 文件发送成功后回调函数
         *      data.url 返回上传图片路径
         *      data.message 返回上传信息
         * @param error 文件发送失败后回调函数
         *      arg1,arg2,arg3
         */
        send: send,
        base64ToBlob:base64ToBlob,
        getObjectURL: getObjectURL
    };

})(jQuery, window);

