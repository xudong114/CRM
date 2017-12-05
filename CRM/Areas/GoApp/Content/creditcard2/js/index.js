$().ready(function () {
    $('.adds').on('tap', function () {
        var i = $(this).siblings('li.xyk').size();
        $(this).before("<li class='mui-table-view-cell xyk'><a class='mui-navigate-right'><span>信用卡" + (i + 1) + "：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>开卡行：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>额度：</span><input type='text' value='' /></a></li>")
    });

    $('.addHouse').on('tap', function () {
        var h = $(this).siblings('li.house').size();
        $(this).before("<li class='mui-table-view-cell house'><a class='mui-navigate-right'><span>小区名称" + (h + 1) + "：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>房产地址：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>面积：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><span>是否全款：</span><div class='mui-input-row mui-radio'><label>是</label><input value='是' type='radio' name='" + (h + 1) + "houseloan'></div><div class='mui-input-row  mui-radio'><label>否</label><input value='否' type='radio' name='" + (h + 1) + "houseloan'></div></li>");
    });

    $('.addCar').on('tap', function () {
        var c = $(this).siblings('li.car').size();
        $(this).before("<li class='mui-table-view-cell car'><a class='mui-navigate-right'><span>车辆品牌" + (c + 1) + "：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>型号：</span><input type='text' value='' /></a></li><li class='mui-table-view-cell'><a class='mui-navigate-right'><span>市值：</span><input type='text' value=''/></a></li>")
    });

    $('.addFactory').on('tap', function () {
        var y = $(this).siblings('li.factory').size();
        $(this).before("<li class='mui-table-view-cell factory'><a class='mui-navigate-right'><span>地址" + (y + 1) + "：</span><input placeholder='工厂/专卖店地址' type='text' value='' /></a></li>")
    });

});
