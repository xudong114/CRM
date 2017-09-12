$().ready(function () {
    $('.messageContent a').on('click', function (e) {
        e.preventDefault();
        var that = this;
        myCollapse(that)
    });
    //增加工厂地址
    $('.addFactory a').on('click', function () {
        var f = $('.factory').size();
        $(this).parent().before('<div class="factory"><span>地址' + (f + 1) + '：</span><input type="text" placeholder="工厂/专卖店地址" /></div>')
    });
    //增加信用卡
    $('.addMyCard a').on('click', function () {
        var c = $('.card').size();
        $(this).parent().before('<div class="card"><span>信用卡' + (c + 1) + '：</span><input type="text" /></div><div><span>开卡行：</span><input type="text"/></div><div><span>额度：</span><input type="text"/></div>')
    });
    //增加房产
    $('.addHouse a').on('click', function () {
        var h = $('.house').size();
        $(this).parent().before('<div class="house"><span>小区名称' + (h + 1) + '：</span><input type="text" /></div><div><span>房产地址：</span><input type="text" /></div><div><span>面积：</span><input type="text" /></div><div><span>是否全款：</span><div class="myRadio"><span>是</span><input name="' + (h + 1) + 'houseloan" type="radio" value="是" /></div><div class="myRadio"><span>否</span><input name="' + (h + 1) + 'houseloan" type="radio" value="否" /></div></div>')
    });
    //增加车辆
    $('.addCar a').on('click', function () {
        var r = $('.car').size();
        $(this).parent().before('<div class="car"><span>车辆品牌' + (r + 1) + '：</span><input type="text" /></div><div><span>型号：</span><input type="text" /></div><div><span>市值：</span><input type="text" /></div>')
    });

});

function myCollapse(that) {
	var collapseId = $(that).attr('href');
	$(collapseId).slideToggle();
	$(that).find('img').toggleClass('imgRotate')
}