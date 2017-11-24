jQuery.support.cors = true;


function onFactorySave() {
    $.ajax({
        type: 'post',
        url: '/jiajudai/order/createfactory',
        data: $("#formFactory").serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/order/success';
            }else{
                mui.toast(data.Message);
            }
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
        }
    });
}

function onStoreSave() {
    $.ajax({
        type: 'post',
        url: '/jiajudai/order/createstores',
        data: $("#formStore").serialize(),
        success: function (data) {
            if (data.Status) {
                window.location.href = '/jiajudai/order/success';
            }else{
                mui.toast(data.Message);
            }
        },
        error: function (arg1, arg2, arg3) {
            mui.toast(arg3);
        }
    });
}

function onSave() {
    var code = $('.houseBtn a.active').attr("href");
    if (code == '#factory') {
        onFactorySave();
    } else {
        onStoreSave();
    }
}


//选择经营行业
function selectIndustry(target) {
	var kindList = [{
	    value: '进口家具',
		text: '进口家具'
	}, {
	    value: '北欧',
	    text: '北欧'
	}, {
	    value: '欧式红木',
	    text: '欧式红木'
	}, {
	    value: '定制家具',
	    text: '定制家具'
	}, {
	    value: '实木',
	    text: '实木'
	}, {
	    value: '板式',
	    text: '板式'
	}, {
	    value: '软体',
	    text: '软体'
	}, {
	    value: '户外',
	    text: '户外'
	}, {
	    value: '儿童',
	    text: '儿童'
	}, {
	    value: '办公',
	    text: '办公'
	}, {
	    value: '酒店工程',
	    text: '酒店工程'
	}, {
	    value: '欧美',
	    text: '欧美'
	}, {
	    value: '红木',
	    text: '红木'
	}, {
	    value: '藤竹',
	    text: '藤竹'
	}, {
	    value: '客厅五金',
	    text: '客厅五金'
	}]
	var pickerKind = new mui.PopPicker({
		layer: 1
	});
	pickerKind.setData(kindList);
	pickerKind.show(function(s) {
	    $(target).val(s[0].text);
	});
}

function selectMall(target) {
    var kindList = [{
        value: '进口家具',
        text: '进口家具'
    }];
    var pickerKind = new mui.PopPicker({
        layer: 1
    });
    pickerKind.setData(malls);
    pickerKind.show(function(s) {
        $(target).val(s[0].text);
    });
}

var malls = [];
/**
 *@description 根据地区获取商场
 */
function getMalls( province, city, country) {
    malls = [];
    var data = { pageIndex: 1, pageSize: 100, province: province, city: city, country: country, keyword: "" };
    $.ajax({
        url: 'http://api.gojiaju.com/api/mall/getmalls',
        data: data,
        dataType:'json',
        success: function (data) {
            for(var i=0;i<data.$values.length;i++){
                malls.push({
                    value: data.$values[i].Id,
                    text: data.$values[i].Name
                });
            }
           console.log (data);
        },
        error: function (arg1,arg2,arg3) {
            console.log(arg3);
        }
    });
}

/**
 *@description 选择省市区/县 
 */
function selectDistrictStore(thisObj) {
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
		$(thisObj).val(s[0].text + ' ' + s[1].text + ' ' + s[2].text);
		$(thisObj).parents('.prov').find('.province').val(s[0].text);
		$(thisObj).parents('.prov').find('.city').val(s[1].text);
		$(thisObj).parents('.prov').find('.country').val(s[2].text);
		console.log(s[0].value);
		console.log(s[1].value);
		console.log(s[2].value);

		getMalls(s[0].value,s[1].value,s[2].value);
	});
}
/**
 *@description 选择省市区/县 
 */
function selectDistrictFactory(thisObj) {

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
        $(thisObj).val(s[0].text + ' ' + s[1].text + ' ' + s[2].text);
        $(thisObj).parents('.prov').find('.province').val(s[0].text);
        $(thisObj).parents('.prov').find('.city').val(s[1].text);
        $(thisObj).parents('.prov').find('.country').val(s[2].text);
        console.log(s[0].value);
        console.log(s[1].value);
        console.log(s[2].value);
    });
}

$().ready(function() {

    $(document).on('click','.provstore',function() {
        console.log('选择店铺地区');
        selectDistrictStore(this);
    });

    $('.provfactory').click(function() {
        console.log('选择工厂地区');
        selectDistrictFactory(this);
	});

	$('.houseBtn div a').on('click', function(e) {
	    e.preventDefault();
	    $(this).siblings('a').removeClass('active');
	    $(this).addClass('active');
	    var showId = $(this).attr('href');
	    $(showId).siblings().hide();
	    $(showId).show();
	});

    /*
     * @desc 选择经营行业
     */
	$(document).on('click','input[code="Industry"]',function() {

	    selectIndustry(this);
	});

	$(document).on('click','input[code="Mall"]',function(){
	    
	    selectMall(this);
	});

	$('.addStore').on('click', function() {

	    $(this).before(`
					<ul class="mui-table-view">
						<li class="mui-table-view-cell mui-collapse">
							<a class="mui-navigate-right" href="#" storename>门店一</a>
							<div class="mui-collapse-content">
								<!--经销商门店-->
                                <input type="hidden" name="Stores[0].Id" code="Id" value="00000000-0000-0000-0000-000000000000" />
								<div class="facName">
									<label for="facName">经营品牌</label><input placeholder="填写所经营的品牌" type="text"  code="Brand"  value="" /><span style="float: right;"> &gt;</span>
								</div>
								<div class="saleList">
									<label for="saleList">经营行业</label><input placeholder="选择经营行业" type="text" code="Industry" value="" /><span style="float: right;"> &gt;</span>
								</div>
								<div class="prov">
									<label for="provstore">所在地区</label><input type="text" value="" placeholder="请选择所在区域" id='provstore' class="provstore"/><span style="float: right;"> &gt;</span>
                                    <input type="hidden" class='province'  code="Province" value="" />
                                    <input type="hidden" class='city'  code="City" value="City" />
                                    <input type="hidden" class='country'  code="Country" value="" />
								</div>
								<div class="saleList">
									<label for="saleList">所属商场</label><input placeholder="选择门店所属商场" type="text" code="Mall" value="" /><span style="float: right;"> &gt;</span>
								</div>
								<div class="adds landlord">
									<label for="codes">工厂面积</label><input type="text" code="StoreArea" value="" /><span style="float: right;">m2</span>
								</div>
                                
								<div class="pic">
									<p>上传营业执照</p>
									<div class='getCmr'>
										<div class='hid'>
											<a data-fancybox href='/Areas/JJD/Content/images/01.index/u454.png'>
                                                <img preview style="width:75px;height:75px;" src='/Areas/JJD/Content/images/01.index/u454.png' />
                                            </a>
										</div>
										<span class='data'></span>
									</div>
                                    <div class="upload">
                                        <input type="file" code="fileBusinessLicenseImg" />
									    <div id="getCmrOrder" class="addImg getCmrOrder">
										    <img upload src="/Areas/JJD/Content/images/03.personalCenter/jia.jpg" class='add' />
										    <input type="hidden" code="BusinessLicenseImg"  />
                                            <span >添加  </span>
									    </div>
                                    </div>
								</div>
                                <label for="facName">营业执照编号</label><input placeholder="注册号/统一社会信用代码" type="text" name="Stores[0].BusinessLicenseNo" code="BusinessLicenseNo" id="Stores[0].BusinessLicenseNo" value="" /><span style="float: right;"> &gt;</span>
							</div>
						</li>
					</ul>
		`);
        //重置索引
		$('.dealer ul').each(function(i,ele){
		    $(this).find('input').each(function(j,thisObj){
		        $(thisObj).attr('name', 'Stores['+i+'].'+$(thisObj).attr('code'));
		    });
		    console.log(i);
		    $(ele).find('a[storename]').text('门店'+(i+1));
		});
	});

	$(document).on('change', 'input[type="file"]', function () {
	    var thisObj = this;
	    var file = $(this)[0].files[0];
	    if (!file) {
	        mui.toast('未选择任何文件');
	        return;
	    }
	    dialog.hide();
	    dialog.show({ title: '正在上传文件...' });

	    $(this).parents('.pic').find('img[preview]').attr('src', File.getObjectURL(file));
	    $(this).parents('.pic').find('a[data-fancybox]').attr('href', File.getObjectURL(file));

	    var option = {
	        file: file,
	        width: 800,
	        callback: function (data) {
	            if (!data.url) {
	                return;
	            }

	            $(thisObj).parent().find('input[code="BusinessLicenseImg"]').val(data.url);

	            dialog.hide();
	            dialog.show({ title: '正在识别营业执照...' });
	            console.log(data.url);
                //门店照片不需要识别
	            if($(thisObj).attr('code')!=='storeImg'){
	                $.ajax({
	                    url: '/jiajudai/ocr/getbusinesslicenseinfo',
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

	                        console.log(cardinfo.success);
	                        if (cardinfo.success) {
	                            if (cardinfo.reg_num == '') {
	                                mui.toast('自动识别失败，请填写注册号/统一社会信用代码！');
	                            } else {
	                                mui.toast('营业执照识别成功');
	                                $(thisObj).parents('.mui-table-view').find('input[code="BusinessLicenseNo"]').val(cardinfo.reg_num);
	                                $(thisObj).parents('.factory').find('#BusinessLicenseNo').val(cardinfo.reg_num);
	                            }
	                        } else {
	                            mui.toast('自动识别失败，请填写注册号/统一社会信用代码！');
	                        }
	                        dialog.hide();

	                    },
	                    error: function (arg1, arg2, arg3) {
	                        dialog.hide();
	                    }
	                });
	            }else{
                    //...
	            }
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

	$('#btnNext').click(function () {
	    onSave();
	});

    //激活选项卡
	$('.houseBtn a.active').click();
});
