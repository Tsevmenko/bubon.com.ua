(function($){

	$.fn.preloader = function(where, callback){
	
		var $where = where;
		var $nimages = $("img").length;
		var $counter = 1;
		
		//get the background-image attribute of each section and add it as an image to 'where'
		$("article").each(function(){
		
			var $background;
		
			if($(this).css("background-image") != 'none' && $(this).css("background-image") != 'undefined' && $(this).css("background-image") != ''){
				
				$background = $(this).css("background-image");
				$background = $background.replace(/"/g,"").replace(/url\(|\)$/ig, "");
				
				$("#"+$where).append("<img src='"+$background+"' alt='sample' />");
			
			}
		
		});
		
		//close each img element to the 'where' argument provided
		$("img").each(function(){
		
			var $clone = $(this).clone();
			$("#"+$where).append($clone);
			
			$counter++;
			
			//when at last element, callback
			if($counter == $nimages)
				callback();
		
		});
			
	};

})(jQuery);


jQuery(document).ready(function($) {
	
	var animationDuration = 1000; //animation speed
	var animated = true; //toggle animation

	//animate the elements within the viewport
	function addEffectInView(){

		var counter = 1;
		animationDelayCount = 0;

		$(".cp_load").each(function(){
		
			var thiss = $(this);
		
			if(isScrolledIntoView(thiss)){
			
				animationDelayCount++;
			
				if(!thiss.hasClass("animated"))
					setTimeout(function(){thiss.addClass("animated").removeClass("cp_load");}, 100 * animationDelayCount);
					
			}
				
		});
		
	}
	
	//check if element is in view
	function isScrolledIntoView(elem)
	{

		var docViewTop = $(window).scrollTop();
		var docViewBottom = docViewTop + $(window).height();

		var elemTop = $(elem).offset().top;
		var elemBottom = elemTop + $(elem).outerHeight(true);

		return ((elemBottom <= docViewBottom) && (elemTop >= docViewTop));
		
	}
	
	//remove the preloader
	function endPreloading(){

		setTimeout(function(){$("#cp-preloader").css("opacity","0");}, animationDuration / 2);
		
		if(animated){
			
			setTimeout(function(){
			
				addEffectInView();
				//add the animation when the content comes into view
				var throttled2 = _.throttle(addEffectInView, 50);
				$(window).scroll(throttled2);
			
			}, animationDuration * 2);
				
		}
		else{
		
			$(".cp_load").removeClass("cp_load");
			
			
		}
		
		setTimeout(function(){
				
			$("#cp-preloader").remove();
			$("#cp-image-preloader").remove();
				
		}, animationDuration * 2);
		
	}
	
	function startPreloader(){
	
		//make sure all images are fully loaded
		var images = $("#cp-image-preloader img");
		var nimages = images.length;
		var interval;
		var counter = 0;
		var prev = 0;
		var percentage;
		
		images.load(function(){updatePreloader();});
		
		function updatePreloader(){
			
			percentage = Math.round(((counter + 1) / nimages) * 100);

			if(percentage >= 100)
				percentage = 100;

			interval = setInterval(function(){
			
				if(prev <= percentage)
					$("#percentage").html((prev++)+"%");
				else{
				
					clearInterval(interval);
					prev = percentage;
				
				}
			
			}, 200);
			
			counter++;
			
			if(counter == nimages)
				endPreloading();
		
		}
	
	}
	//start the preloader plugin
jQuery(document).preloader("cp-image-preloader", startPreloader);
});

