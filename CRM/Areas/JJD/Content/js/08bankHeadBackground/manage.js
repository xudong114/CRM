$(function(){
	$('header span').on('click',function(){
			var picker = new mui.PopPicker({layer:2});
	picker.setData(districts.list);
	picker.show(function(s){
//		console.log(s[1].children[5].text)
	});
		
	})

})
