progressBar(2)
function getOrder() {
	mui.ajax2({
		url: '/api/Order/GetOrder',
		data: {
			id: orderId
		},
		success: function(data) {
//			for(var i in data) {
//				console.log(i + '获得信息---' + data[i])
//			}
			$('.orderId').html(data.Id);
			$('.applyDate').html(data.CreatedDate);
			$('.applyMony').html(data.LoanAmount + '万');
			$('.userName').html(data.Name);
			$('.phone').html(data.PersonalPhone);
			$('#IDNo').html(data.IDNo);
			$('#community').html(data.Community);
			if(data.Country == undefined) {
				data.Country = '';
			}
			$('.pcc').html(data.Province + ' ' + data.City + ' ' + data.Country);
			$('#landlord').html(data.Landlord);
			var isInstallment = '是';
			if(data.IsInstallment) {
				isInstallment = '是';
			} else {
				isInstallment = '否';
			}
			$('.isInstallment').html(isInstallment);
			$('.store').html(data.StoreName);
			$('.clerkCode').html(data.ClerkCode);
			$('#totalAmount').html(data.TotalAmount + '万');
			$('#downpaymentAmount').html(data.DownpaymentAmount + '万');
			switch(data.Status) {
				case 4: //平台通过
					gojiajuPassed();
					progressBar(1);
					break;
				case 8: //平台拒绝
					gojiajuDenied();
					progressBar(1);
					break;
				case 16: //银行通过
					bankPassed();
					gojiajuPassed();
					progressBar(2);
					break;
				case 32: //银行拒绝
					bankDenied();
					gojiajuPassed();
					progressBar(2);
					break;
				case 64: //银行签约
					bankSinged();
					bankPassed();
					gojiajuPassed();
					progressBar(3);
					break;	
				case 128: //取消签约
					singCanceled();
					bankPassed();
					gojiajuPassed();
					progressBar(3);
					break;	
			}

		}
	})
}
/**
 * @description 平台未通过
 */
function gojiajuDenied(goCall, goRemark, goTimes) {
	$('.details').append('<ul class="mui-table-view terrace"><li class="mui-table-view-cell mui-collapse mui-active"><a class="mui-navigate-right">平台受理</a><div class="mui-collapse-content"><ul class="mui-table-view"><li class="mui-table-view-cell"><p>受理状态</p><span id="warning">未通过受理</span><b>02.35 14:35</b></li><li class="mui-table-view-cell"><p>专属客服</p><span>0512-8227855</span><a class="call"><img src="../images/04.user/u3639.png" /></a></li><li class="mui-table-view-cell remark"><p>备注信息</p><br /><span>您好,您的申请通过受理</span></li></ul></div></li></ul>'
	);
}
/**
 *@description 平台通过 
 */
function gojiajuPassed(goCall, goRemark, goTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right" href="#">平台受理</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>受理状态</p><span>通过受理</span><b>02.35 14:35</b></li>
								<li class="mui-table-view-cell">
									<p>专属客服</p><span>0512-8227855</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
								<li class="mui-table-view-cell remark">
									<p>备注信息</p><br/>
									<span>您好您的申请平台已经通过受理，请耐心等待受理平台客服专员会与您取得联系！</span>
								</li>
							</ul>
						</div>
					</li>
				</ul>
				`
	);
}
/**
 *@description 银行拒绝
 */
function bankDenied(bankCall, bankRemark, bankTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right">银行受理</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>受理状态</p><span id="warning">未通过受理</span><b>02.35 14:35</b></li>										   
								<li class="mui-table-view-cell">
									<p>受理银行</p><span>中国银行</span></li>
								<li class="mui-table-view-cell">
									<p>银行专员</p><span>0512-87654321</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
								<li class="mui-table-view-cell remark">
									<p>备注信息</p><br />
									<span>您好您的申请已经被拒绝</span>
									<div>
										<a class='dBtn'>修改申请订单</a>
									</div>
								</li>
							</ul>
						</div>
					</li>
				</ul>`
	)
}
/**
 *@description 银行通过
 */
function bankPassed(bankCall, bankRemark, bankTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right">银行受理</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>受理状态</p><span>通过受理</span><b>02.35 14:35</b></li>										   
								<li class="mui-table-view-cell">
									<p>受理银行</p><span>中国银行</span></li>
								<li class="mui-table-view-cell">
									<p>银行专员</p><span>0512-87654321</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
								<li class="mui-table-view-cell remark">
									<p>备注信息</p><br />
									<span>您好您的申请已经通过银行审核</span>								
								</li>
							</ul>
						</div>
					</li>
				</ul>`
	)
}
/**
 *@description 银行取消签约
 */
function singCanceled(singCall, singRemark, singTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right">银行签约</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>签约状态</p><span id='warning'>取消签约</span><b>02.35 14:35</b>
								</li>
								<li class="mui-table-view-cell">
									<p>银行专员</p><span>0512-87654321</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
								<li class="mui-table-view-cell remark">
									<p>备注信息</p><br />
									<span>取消签约</span>								
								</li>
							</ul>
						</div>
					</li>
				</ul>`
	)
}
/**
 *@description 银行签约
 */
function bankSinged(singCall, singRemark, singTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right">银行签约</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>签约状态</p><span>已签约</span><b>02.35 14:35</b>
								</li>
								<li class="mui-table-view-cell">
									<p>银行专员</p><span>0512-87654321</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
								<li class="mui-table-view-cell remark">
									<p>备注信息</p><br/>
									<span>您的贷款协议已签署完毕,请等待银行通知放款</span>								
								</li>
							</ul>
						</div>
					</li>
				</ul>`
	)
}
/**
 *@description 放款中...
 */
function successed(successedCall,successedTimes) {
	$('.details').append(
		`	<ul class="mui-table-view terrace">
					<li class="mui-table-view-cell mui-collapse mui-active">
						<a class="mui-navigate-right">银行签约</a>
						<div class="mui-collapse-content">
							<ul class="mui-table-view">
								<li class="mui-table-view-cell">
									<p>签约状态</p><span>放款中...</span><b>02.35 14:35</b>
								</li>
								<li class="mui-table-view-cell">
									<p>银行专员</p><span>0512-87654321</span>
									<a class='call'><img src="../images/04.user/u3639.png" /></a>
								</li>
							</ul>
						</div>
					</li>
				</ul>`
	)
}
/**
 *	@description 获取订单图片和征信图片 
 */
function getImgList(code, addImgId) {
	//	var referenceId = app.order.getId() 
	mui.ajax2({
		url: '/api/Files/GetFiles',
		data: {
			referenceId: orderId,
			code: code
		},
		success: function(data) {
			for(var i in data.List) {
				$('#' + addImgId).append(
					`
							<div class='pic'>
        						<img src="${data.List[i].Path}"/>
        					</div>
      					  `
				)
			}
		},
		error: function(a, b, c) {
			mui.toast('获取失败');
		}
	})
}
mui.plusReady(function() {
	getOrder();
	getImgList('3', 'orderImg');
	getImgList('credit', 'creditImg');
	
$('.mui-content').on('tap','.pic img',function(){
	$('#bigImg').fadeIn(300);
	
	$('#bigImg img').attr('src',$(this).attr('src'));
});
$('#bigImg').on('tap',function(){
	$(this).fadeOut(300);
})
});