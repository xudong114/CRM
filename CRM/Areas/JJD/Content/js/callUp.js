/**
 *	@description 打电话 
 */
function getPhoneNum(that) {
	$('.call').each(function() {
		var phoneNum = $(that).prev('span').html();
		$(that).attr('href', 'tel:' + phoneNum);
	});
}

$('.details').on('tap', '.call', function() {
	var that = this;
	getPhoneNum(that);
})