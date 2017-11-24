
var currentdate = null;
$(function(){
	//进度条
//	progressBar(2)
	//获取当前时间
function getNowFormatDate() {
    var date = new Date();
    var seperator1 = "-";
    var seperator2 = ":";
    var month = date.getMonth() + 1;
    var strDate = date.getDate();
    if (month >= 1 && month <= 9) {
        month = "0" + month;
    }
    if (strDate >= 0 && strDate <= 9) {
        strDate = "0" + strDate;
    }
 currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
            + " " + date.getHours() + seperator2 + date.getMinutes()
            + seperator2 + date.getSeconds();
//  return currentdate;
}
getNowFormatDate() 	
//	
//	
//		$('.allotOrder,.refuse,.stopSign').on('click','li',function(){
//		$(this).addClass('active').siblings('li').removeClass('active');
//	})
	
	
	$('.allotOrder,.refuse').on('click','li',function(){
		$(this).addClass('active').siblings('li').removeClass('active');
	})
	
	$('.orderBtn .allot').on('tap',function(){
		$('.allotOrder').show();
	})
	$('.orderBtn .check,.checkBtn .check').on('tap',function(){
		$('.checkOrder').show();
	})
	
	$('.off').on('tap',function(){
		$(this).parent().parent().parent().hide();
	})
	
	//审核订单
	$('.agree').on('tap','a',function(){
		if($(this).hasClass('yes')){
			$('.refuse').hide();
			$(this).siblings().removeClass('act');
			$(this).addClass('act');
			$('.choose').show().addClass('act');
			mui.toast('请选择所需材料')
		}else{
			$('.choose').hide().removeClass('act');
			$(this).siblings().removeClass('act');
			$(this).addClass('act');
			$('.refuse').show().addClass('act');
			mui.toast('请填写拒绝原因')
		}
	})
	$('.choose').on('tap','li',function(){
		if($(this).hasClass('active')){
			$(this).removeClass('active')
		}else{
			$(this).addClass('active');
		}
	})
	// 通过审核  之后 向 页面添加元素
	$('.checkOrder .sure').on('tap',function(e){
		//审核通过 之后  隐藏分配 和审核订单按钮   显示签约按钮
		$('.orderBtn').hide();
		$('.signBtn').show();
		progressBar(2);
	//添加银行受理审核通过	
	if($('.act').hasClass('yes')){
		$('.details').prepend("<ul class='mui-table-view bank'><li class='mui-table-view-cell mui-collapse mui-active'><a class='mui-navigate-right'>银行受理</a><div class='mui-collapse-content'><ul class='mui-table-view'><li class='mui-table-view-cell'><p>受理状态</p><span>通过受理</span></li><li class='mui-table-view-cell'><p>受理时间</p><span>2017-6-29 14:35</span></li><li class='mui-table-view-cell'><p>受理客服</p><span>客服姓名</span></li><li class='mui-table-view-cell'><p>手机号码</p><span>0512-8227855</span><img src='img/u3639.png'/></li><li class='mui-table-view-cell remark'><p>备注信息</p><br /><span>通过</span></li></ul></div></li></ul>");
	}else{
		getNowFormatDate()
			//添加银行受理审核未通过	
		$('.details').prepend(`
		<ul class="mui-table-view bank"> 
			<li class="mui-table-view-cell mui-collapse mui-active">
			<a class="mui-navigate-right" href="#">银行受理</a>
			<div class="mui-collapse-content">			
				<ul class="mui-table-view">
				    <li class="mui-table-view-cell"><p>受理状态</p><span class='warning'>未通过受理</span></li>
				    <li class="mui-table-view-cell"><p>受理时间</p><span>${currentdate}</span></li>
				    <li class="mui-table-view-cell"><p>受理客服</p><span>客服姓名</span></li>
				    <li class="mui-table-view-cell"><p>手机号码</p><span>0512-8227855</span><img src="img/u3639.png"/></li>
				    <li class="mui-table-view-cell remark">  
				    	<p>备注信息</p><br />
				    	<span>未通过</span>	
				    </li>
				</ul>
			</div>
			</li>
		</ul>	
		`);
		//平台拒绝  改变签约按钮样式
	$('.stops').addClass('disable')	;
	$('.decide').addClass('disable');	
	}		
	$('.checkOrder').hide();		
})//订单结束

//签约或者不签约
$('.stops').on('tap',function(){

	if($(this).hasClass('disable')){
		mui.toast('签约已拒绝');
		return;
	}	
	$('.stopSign').css('display','block');
	mui.toast('请填写拒绝原因')
})
$('.decide').on('tap',function(){
	if($(this).hasClass('disable')){
		mui.toast('签约已拒绝');
		return;
	}
	$('.decideSign').css('display','block');

})
$('.decideSign').on('tap','.sure',function(e){
	e.stopPropagation();
		progressBar(3)
		//确定签约，添加签约标签
			$('.details').prepend(`
		<ul class="mui-table-view bank"> 
			<li class="mui-table-view-cell mui-collapse mui-active">
			<a class="mui-navigate-right" href="#">银行签约</a>
			<div class="mui-collapse-content">			
				<ul class="mui-table-view">
				    <li class="mui-table-view-cell"><p>签约状态</p><span>已签约</span></li>
				    <li class="mui-table-view-cell"><p>处理时间</p><span>2017-6-29 14:35</span></li>
				    <li class="mui-table-view-cell"><p>受理客服</p><span>客服姓名</span></li>
				    <li class="mui-table-view-cell"><p>手机号码</p><span>0512-8227855</span><img src="img/u3639.png"/></li>
				    <li class="mui-table-view-cell remark">  
				    	<p>签约凭证</p><br />
				    	<div class="pic">
        <div class="getCmr"><img src="img/u2410.jpg"/>
        </div>
        <div class="getCmr"><img src="img/u2410.jpg"/>
        </div>
        <div class="getCmr"><img src="img/u2410.jpg"/>
        </div>
    </div>
				    </li>
				</ul>
			</div>
			</li>
		</ul>	
		`);
		
//	$('.decideSign').css('display','none');
$('.decideSign').css('opacity','0');
	$('.mui-btn').show();
	$('.signBtn').hide();
	setTimeout(function(){
		$('.decideSign').hide();
	},400)
})

$('.pic .getCmr').on('click','span',function(){$(this).parent().remove();})
$('.base').on('tap',function(){
	$(document).scrollTop($(document).height()-$(window).height());
})
});
