/*------------------------------
 * Copyright 2015 Pixelized
 * http://www.pixelized.cz
 *
 * Supreme theme v1.1
------------------------------*/

/*------------------------------
	SCROLL TOP
------------------------------*/
$(window).scroll(function() {
	if ($(this).scrollTop() > 400) {
		$('#scrolltop').fadeIn(500);
	} else {
		$('#scrolltop').fadeOut(500);
	}		
});

$(document).ready(function() {
	if ($('.content-item').first().hasClass('grey')) {
		$('body').css("background-color","#f0f0f0");
	}
	
	/*------------------------------
		MAGNIFIC POPUP
	------------------------------*/
  	$('.show-image').magnificPopup({type:'image'});
	
	/*------------------------------
		TOGGLE RESET PASSWORD
	------------------------------*/
	$('#reset-password-toggle').click(function() {
        $('#reset-password').slideToggle(500);
    });
	
	/*------------------------------
		SCROLL FUNCTION
	------------------------------*/
	function scrollToObj(target, offset, time) {
		$('html, body').animate({scrollTop: $( target ).offset().top - offset}, time);
	}
	
	$("a.scroll[href^='#']").click(function(){
		scrollToObj($.attr(this, 'href'), 0, 1000);
		return false;
	});
	
	$("#scrolltop").click(function() {
		scrollToObj('body', 0, 1000);
    });
	
	/*------------------------------
		PORTFOLIO - ISOTOPE
	------------------------------*/
	var $container = $('.portfolio-init');
	$container.isotope({
	  	itemSelector: '.portfolio-item',
	});
	
	$('.portfolio-filter .btn-group a').click(function(e) {
		$('.portfolio-filter .btn-group a').removeClass('active');
		$(this).addClass('active');
		
        var category = $(this).attr('data-filter');
		$container.isotope({
			filter: category
		});
    });
	
	/*------------------------------
		OWL CAROUSEL
	------------------------------*/
	$("#homepage-1-carousel").owlCarousel({
    	items : 1,
        loop: true,
		autoplay : true,
		navText : ["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"],
		animateOut: 'fadeOut',
    	animateIn: 'fadeIn',
		responsive:{
			0:{
				nav:false,
			},
			768:{
				nav:true,
			}
		}
  	});
	
	$("#testimonials-carousel").owlCarousel({
    	items : 1,
		loop : true,
		autoplay : true,
		navText : ["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"],
		responsive:{
			0:{
				nav:false,
			},
			768:{
				nav:true,
			}
		}
  	});
	
	$("#reference-carousel").owlCarousel({
		margin : 10,
		dots : false,
		navText : ["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"],
		responsive:{
			0:{
				items:1,
				nav:false,
				loop:false
			},
			360:{
				items:2,
			},
			768:{
				items:2,
				nav:true,
				loop:true
			},
			992:{
				items:3,
				nav:true,
				loop:true
			}
		}
  	});
	
	$('#portfolio-carousel').owlCarousel({
		items: 1,
		loop : true,
		autoplay : true,
		dots : false,
		navText : ["<i class='fa fa-chevron-left'></i>","<i class='fa fa-chevron-right'></i>"],
		responsive:{
			0:{
				nav:false,
			},
			768:{
				nav:true,
			}
		}
	})
	
	/*------------------------------
		REFERENCE DESCRIPTION
	------------------------------*/
	if($(window).width() > 767) {
		$('#reference-carousel .item').mouseenter(function() {
			$(this).find('p').slideDown(400);
		});
		
		$('#reference-carousel .item').mouseleave(function() {
			$(this).find('p').stop().slideUp(400);
		});
	}
	
	/*------------------------------
		TEAM MEMBER SOCIALS
	------------------------------*/	
	if($(window).width() > 767) {
		$('.team-member').mouseenter(function() {
			$(this).find('.overlay').slideDown(400);
		});
		
		$('.team-member').mouseleave(function() {
			$(this).find('.overlay').slideUp(400);
		});
	}
	
	$('.overlay-wrapper').mouseenter(function() {
		$(this).find('.overlay a:first-child').addClass('animated slideInLeft');
		$(this).find('.overlay a:last-child').addClass('animated slideInRight');
    });
	
	$('.overlay-wrapper').mouseleave(function() {
		$(this).find('.overlay a:first-child').removeClass('animated slideInLeft');
		$(this).find('.overlay a:last-child').removeClass('animated slideInRight');
    });
	
	/*------------------------------
		COUNTER UP
	------------------------------*/
	$('.counter').counterUp({
		delay: 100,
		time: 5000
	});
	
	/*------------------------------
		YOUTUBE VIDEO BACKGROUND
	------------------------------*/
	$(".player").YTPlayer();
	
	/*------------------------------
		TYPED
	------------------------------*/
	$(".project-typed").typed({
    	strings: ["Creative.", "Modern."],
		startDelay: 500,
		typeSpeed: 100,
		backDelay: 2000,
		loop: true
    });
	
	/*------------------------------
		COUNTDOWN
	------------------------------*/
	$('.countdown').countdown('2017/01/01', function(event) {
	    var $this = $(this).html(event.strftime(''
	      + '<div><span class="countdown-number">%w</span><span class="countdown-title">weeks</span></div> '
	      + '<div><span class="countdown-number">%d</span><span class="countdown-title">days</span></div> '
	      + '<div><span class="countdown-number">%H</span><span class="countdown-title">hours</span></div> '
	      + '<div><span class="countdown-number">%M</span><span class="countdown-title">minutes</span></div> '
	      + '<div><span class="countdown-number">%S</span><span class="countdown-title">seconds</span></div>'));
	});
		
	/*------------------------------
		GOOGLE MAP
	------------------------------*/	
	//var map;
	
	//var mapInfo = {
	//	'lat' : 40.710968,
	//	'lng' : -74.0084713,
	//	'zoom' : 16
	//};
	
	//var markerInfo = {
	//	'lat' : 40.710968, 
	//	'lng' : -74.0084713,
	//	'title' : 'Our Office'
	//};
	
	//var mapLatLng = new google.maps.LatLng(mapInfo.lat, mapInfo.lng);
	//var markerLatLng = new google.maps.LatLng(markerInfo.lat, markerInfo.lng);
	
	// GOOGLE MAP INIT
	//function initialize($) {
	//	var mapOptions = {
	//	  	zoom: mapInfo.zoom,
	//	  	center: mapLatLng,
	//	  	navigationControl: false,
	//	  	mapTypeControl: false,
	//	  	scaleControl: false,
	//	  	draggable: true,
	//		scrollwheel: false
	//	}
	//	map = new google.maps.Map(document.getElementById("map-canvas"), mapOptions);
		
		
	//	var marker = new google.maps.Marker({
	//		position: markerLatLng,
	//		map: map,
	//		title: markerInfo.title
	//	});
	//}
		
	//if($("#map-canvas").length) {
	//	google.maps.event.addDomListener(window, 'load', initialize);
	//}
	
});