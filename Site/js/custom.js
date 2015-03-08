jQuery(document).ready(function($){
	//Bx Slider
	if($('.bxslider').length){
		$('.bxslider').bxSlider();
	}
	
	$( "#slider-range" ).slider({
		range: true,
		min: 0,
		max: 500,
		values: [ 75, 300 ],
		slide: function( event, ui ) {
			$( "#amount" ).val( "$" + ui.values[ 0 ] + " - $" + ui.values[ 1 ] );
		}
	});
	$( "#amount" ).val( "$" + $( "#slider-range" ).slider( "values", 0 ) +
		" - $" + $( "#slider-range" ).slider( "values", 1 ) );


	$('a[href="#top"]').click(function(){
		$('html,body').animate({ scrollTop: $("body").offset().top},
		{ duration: 'normal', easing: 'swing'});
		return false;
	});	
  
	//Scrollbar Start
	if($("#content-1").length){
		$(window).load(function(){
			$("#content-1").mCustomScrollbar({
				horizontalScroll:true
			});
			$(".content.inner").mCustomScrollbar({
				scrollButtons:{
					enable:true
				}
			});
		});
	}
	//Scrollbar End
			
	// hide #back-top first Start
	$("#back-top").hide();
	$(window).scroll(function () {
		if ($(this).scrollTop() > 2000) {
			$('#back-top').fadeIn();
		} else {
			$('#back-top').fadeOut();
		}
	});
	// hide #back-top first End
  
	// scroll body to 0px on click start
	$('#back-top a').click(function () {
		$('body,html').animate({
			scrollTop: 0
		}, 800);
		return false;
	});
	
	//SliderShow
	if($('#slideshow').length){
		$('#slideshow').cycle({
			fx:         'fade',
			timeout:     3000,
		   // pager:      '#switcher'
		});
	}
	// scroll body to 0px on click End	
	
	//Circle Slider Start
	if($('#slider1').length){
		var img_url = $('#slider1 .circle_slider_thumb img');
		var tn_array = img_url.map(function() {
			return $(this).attr("src");
		});
			
		$('#slider1').content_slider({
			// bind plugin to div id="slider1"
			map : tn_array,				// pointer to the image map
			max_shown_items: 7,				// number of visible circles
			hv_switch: 0,					// 0 = horizontal slider, 1 = vertical
			active_item: 0,					// layer that will be shown at start, 0=first, 1=second...
			wrapper_text_max_height: 450,	// height of widget, displayed in pixels
			middle_click: 1,				// when main circle is clicked: 1 = slider will go to the previous layer/circle, 2 = to the next
			under_600_max_height: 500,		// if resolution is below 600 px, set max height of content
			border_radius:	-1,				// -1 = circle, 0 and other = radius
			automatic_height_resize: 1,
			border_on_off: 0,
			allow_shadow: 0,
			responsive_by_available_space: 1,
		});
	}
   //Circle Slider End
   
   if($('.container-masonry').length){
       //Masonyr Start
       /*
	   var $container = $('.container-masonry');
	 
	   $container.imagesLoaded( function(){
		 $container.masonry({
		   itemSelector : '.masonryImage'
		 });
	   });*/
	 //Masonyr End
	}
	/*if($('#map_canvas').length){
  		var map;
  		var myLatLng = new google.maps.LatLng(33.987696, -118.472242);
  		//Initialize MAP
  		var myOptions = {
  		  zoom: 16,
  		  center: myLatLng,
  		  disableDefaultUI: true,
  		  zoomControl: true,
  		  mapTypeId: google.maps.MapTypeId.ROADMAP
  		};
  		map = new google.maps.Map(document.getElementById('map_canvas'),myOptions);
  		//End Initialize MAP
  		//Set Marker
  		var marker = new google.maps.Marker({
  		  position: map.getCenter(),
  		  map: map
  		});
  		marker.getPosition();
  		//End marker
  		
  		//Set info window
  		var infowindow = new google.maps.InfoWindow({
  			content: '',
  			position: myLatLng
  		});
  		infowindow.open(map);
  	}*/
  
  //Pretty Photo Start
	if($('.gallery_fun').length){
		$(".gallery_fun:first a[rel^='prettyPhoto']").prettyPhoto({
			animation_speed: 'normal',
			slideshow: 10000,
			autoplay_slideshow: true
		});
		$(".gallery_fun:gt(0) a[rel^='prettyPhoto']").prettyPhoto({
			animation_speed: 'slow',
			slideshow: 10000,
			hideflash: true
		});
	}
	
	
	jQuery(function ($) {
	// Load dialog on page load
	//$('#basic-modal-content').modal();

	// Load dialog on click
	$('#basic-modal .basic').click(function (e) {
		$('#basic-modal-content').modal();

		return false;
	});
});
			
  });
  //Pretty Photo End
  
  
  
	
  
	
	
	
	
		