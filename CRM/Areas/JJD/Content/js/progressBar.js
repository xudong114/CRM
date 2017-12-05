/**
 * @description n= 1:平台受理; 2: 分配银行；3:银行受理； 4:银行签约； 5:银行放款中  ！
 */
function progressBar(n){
		for(var i=1;i<=n;i++){
			$('.progressBar li:nth-child('+i+')').addClass('actvs').css('background','#8763F7');
		}
		$('.actvs span').css('background','#8763F7');
		$('.actvs').css('border-radius','0');	
		$('.progressBar li:nth-child(1)').css('border-top-left-radius','5px').css('border-bottom-left-radius','5px');
		$('.progressBar li:nth-child('+n+')').css('border-top-right-radius','5px').css('border-bottom-right-radius','5px');
}

