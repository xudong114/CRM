/**
 *  @description 选择银行
 */
var bankList = [];
function getBank(){
	mui.ajax2({
		url:'/api/bank/GetBanks',
		success:function(data){
			if(data.Status){
				for(var i=0;i<data.Data.length;i++){
					bankList.push ({
						text:data.Data[i].Name,
						value:data.Data[i].Code						
					})
				}
			}
		},
		error:function(a,b,c){
			mui.toast(c)
		}
	});
}

//getBank()
  function picks(banksValue){

        var BANK_picker = new mui.PopPicker();
        BANK_picker.setData(bankList);
//      BANK_picker.pickers[0].setSelectedIndex(banksValue);
        BANK_picker.show(function(Item){
          var   banker = Item[0].text;
        $('.banks input').val(banker);
        $('.brankId').html(banker);

        })
    }  
mui.ready(function(){ 
	
	getBank();
    $('.banks').on('tap',function(){
    	var banksName = $('.brankId').html();
	  var banksValue =null;
        for(var i = 0; i < bankList.length; i++) {
					if(bankList[i].text === banksName) {
						banksValue = bankList[i].value;
					}
				}
        picks();
    })
});
