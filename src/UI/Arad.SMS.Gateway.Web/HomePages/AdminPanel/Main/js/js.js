$(document).ready(function () {
	$(".div_main").css({ height: $(window).height() - 30 + 'px' });
	$(".div_header .right").css({ width: ($(window).width() - $(".div_header .right").width()) - 10 + 'px' });

	$(".left_column").css(
	{
		width: $(window).width() - $(".right_column").width() - 10 + 'px',
		height: $(".div_footer").offset().top - 100 + 'px'
	});

	$(".right_column").css({ height: $(".left_column").height() + 'px' });
	$(".tabs-bottom").css({ height: $(".right_column").height() + 'px' });
	$("#tabs-1").css({ height: $(".left_column").height() - 40 + 'px' });
	$("#tabs-2").css({ height: $(".left_column").height() - 40 + 'px' });

	$(window).resize(function () {
		$(".div_main").css({ height: $(window).height() - 30 + 'px' });
		$(".div_header .right").css({ width: ($(window).width() - $(".div_header .right").width()) - 10 + 'px' });

		$(".left_column").css(
	{
		width: $(window).width() - $(".right_column").width() - 10 + 'px',
		height: $(".div_footer").offset().top - 100 + 'px'
	});

		$(".right_column").css({ height: $(".left_column").height() + 'px' });
		$(".tabs-bottom").css({ height: $(".right_column").height() + 'px' });
		$("#tabs-1").css({ height: $(".left_column").height() - 40 + 'px' });
		$("#tabs-2").css({ height: $(".left_column").height() - 40 + 'px' });
	});

	$('.div_popup').css(
	{
		left: (($(window).width() - $('.div_popup').width()) / 2) + 'px',
		top: $('.div_footer').offset().top + 'px',
		bottom: $('.div_footer').offset().top + 'px'
	});

	var popup_status = false;
	$('.div_footer .middle_orange').click(function () {
		if (popup_status) {
			popup_status = false;
			$('.div_popup').stop().animate(
					{
						top: $('.div_footer').offset().top + 'px',
						height: '0px',
						opacity: '0'
					}, 'slow');
		}
		else {
			popup_status = true;
			$('.div_popup').stop().animate(
					{
						top: $(window).height() / 2 + 'px',
						height: (($(window).height() / 2) - $('.div_footer').height() - 13) + 'px',
						opacity: '1'
					}, 'slow');
		}
	});
	//tab
	$("#tabs").tabs({ fx: { opacity: 'toggle' }, selected: 2 });
	$(".tabs-bottom .ui-tabs-nav, .tabs-bottom .ui-tabs-nav > *").removeClass("ui-corner-all ui-corner-top").addClass("ui-corner-bottom");
	//end tab
	//menu
	$(".accordion").accordion({ active: false, autoHeight: false, collapsible: true });
	//end menu
});
	