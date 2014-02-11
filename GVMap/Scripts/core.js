$(document).ready(function () {
	console.log('Welcome');
	MapHolder.Initialize();
	MapHolder.BindDBClick();
	Buttons.BindEvents();

    $(window).resize(function() {
        $('#googleMap').css({height:parseInt($(this).height() - 30), width: parseInt($(this).width() - 30)});
    });
});



MapHolder = {
	map : new Object(),
	
	Initialize : function () {
		var mapProp = {
			center:new google.maps.LatLng(42.690511,23.301144),//Bulgaria
		  	zoom:7,
		  	disableDoubleClickZoom: true,
		  	mapTypeId:google.maps.MapTypeId.ROADMAP
		};
		map	= new google.maps.Map($('#googleMap')[0], mapProp);
	},
	
	BindDBClick : function () {
	
		google.maps.event.addListener(map, "dblclick", function (e) {
			//e.preventDefault();
			var marker=new google.maps.Marker({
				position: e.latLng,
			});

			marker.setMap(map);
			
			
			
			var infowindow = new google.maps.InfoWindow({
		  		content:$('.popupInfo').html()//"Hello World!"
		  	});

			google.maps.event.addListener(marker, 'click', function() {
	  			infowindow.open(map,marker);
				$('div.gm-style-iw textarea').prop("disabled", true)
				$('div.gm-style-iw textarea').css({background: "white", border: "none"});
	  		});
		});	
		
		
		
	}
}


Buttons = {
	BindEvents : function () {
		$(document).on('click', '.btn-primary.edit', function () {
			$(this).toggleClass("edit").toggleClass("save").toggleClass("btn-primary").toggleClass("btn-info");
			$(this).text("save");
			$(this).closest('div.innerPopup').find('textarea').prop("disabled", false);
			$(this).closest('div.innerPopup').find('textarea').css('border',"1px solid #36E6F5");
		});
		
		$(document).on('click', '.btn-info.save', function () {
			$(this).toggleClass("edit").toggleClass("save").toggleClass("btn-primary").toggleClass("btn-info");
			$(this).text("edit")
			$(this).closest('div.innerPopup').find('textarea').prop("disabled", true);
			$('div.gm-style-iw textarea').prop("disabled", true)
			$('div.gm-style-iw textarea').css({background: "white", border: "none"});
		});
	}
}




