$('.reset').on('tap',function(){
	mui.confirm('默认密码为123456您确定重置吗','温馨提示',function(e){
	if (e.index == 1) {
		mui.toast('重置成功')
	} else {
		return;
		}
	})
})
//删除账户
$('.del').on('tap',function(){
	mui.confirm('您确定要删除该账户吗','温馨提示',function(e){
	if (e.index == 1) {
		mui.toast('用户删除成功')
	} else {
		return;
		}
	})
})