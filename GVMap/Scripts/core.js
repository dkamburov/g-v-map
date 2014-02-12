$(document).ready(function () {
    console.log('Welcome');
    MapHolder.Initialize();
    MapHolder.BindDBClick();
    MapHolder.BindExitClicksOnInitialization();

    $('#googleMap').css({ height: parseInt($(window).height() - 30), width: parseInt($(window).width() - 30) });

    $(window).resize(function () {
        $('#googleMap').css({ height: parseInt($(this).height() - 30), width: parseInt($(this).width() - 30) });
    });

    $.ajax({
        url: '/Home/GetMarkers',
        success: function (markers) {
            $.each(markers, function (index, value) {
                var coords = value.Coordinates;
                value.Coordinates = value.Coordinates.replace("(", "");
                value.Coordinates = value.Coordinates.replace(")", "");
                
                var bits = value.Coordinates.split(/,\s*/);
                var point = new google.maps.LatLng(parseFloat(bits[0]),parseFloat(bits[1]));

                var infowindow = new google.maps.InfoWindow({
                    content: "<div class='markerContent'><input class='markerId' type='hidden' value='"+ value.Id +"'><input class='markerCoords' type='hidden' value='"+ coords +"'><img src=/Home/GetImage/"+ value.Id +"><p>" + value.Description + "</p><button class='editMarker btn btn-default'>Редакция</button></div>"
                });

                var marker = new google.maps.Marker({
                    position: point,
                    map: MapHolder.map,
                });
                
                google.maps.event.addListener(marker, 'click', function () {
                    infowindow.open(MapHolder.map, marker);
                });
            });
        },
        error: function () {
            alert("Error getting the markers!");
        }
    });

    $(document).on("click", ".editMarker", function() {
        var markerContent = $(this).closest(".markerContent");
        
        $("#coordinates").val(markerContent.find(".markerCoords").val());
        $("#id").val(markerContent.find(".markerId").val());
        $("#description").val(markerContent.find("p").html());
    });

});


MapHolder = {
    map: new Object(),

    Initialize: function () {
        var mapProp = {
            center: new google.maps.LatLng(42.690511, 23.301144), //Bulgaria
            zoom: 7,
            disableDoubleClickZoom: true,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        MapHolder.map = new google.maps.Map($('#googleMap')[0], mapProp);
    },

    BindDBClick: function () {
        var marker = new Object();

        google.maps.event.addListener(MapHolder.map, "dblclick", function (e) {
            //e.preventDefault();
            marker = new google.maps.Marker({
                position: e.latLng
            });

            marker.setMap(MapHolder.map);

            $("#coordinates").val(e.latLng);
        });
    },

    BindExitClicksOnInitialization: function () {
        $(document).on('click', '.popupInfo.empty ', function () {

        });
    }
};
