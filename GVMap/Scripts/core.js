$(document).ready(function () {
    console.log('Welcome');
    MapHolder.Initialize();
    MapHolder.BindDBClick();
    Buttons.BindEvents();
    MapHolder.BindExitClicksOnInitialization();

    $('#googleMap').css({ height: parseInt($(window).height() - 30), width: parseInt($(window).width() - 30) });

    $(window).resize(function () {
        $('#googleMap').css({ height: parseInt($(this).height() - 30), width: parseInt($(this).width() - 30) });
    });

    $.ajax({
        url: '/Home/GetMarkers',
        success: function (markers) {
            $.each(markers, function (index, value) {
                value.Coordinates = value.Coordinates.replace("(", "");
                value.Coordinates = value.Coordinates.replace(")", "");
                
                var bits = value.Coordinates.split(/,\s*/);
                var point = new google.maps.LatLng(parseFloat(bits[0]),parseFloat(bits[1]));

                var infowindow = new google.maps.InfoWindow({
                    content: "<div><img src=/Home/GetImage/"+ value.Id +"><p>" + value.Description + "</p></div>"
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


Buttons = {
    BindEvents: function() {
        $(document).on('click', '.btn-primary.edit', function() {
            $(this).toggleClass("edit").toggleClass("save").toggleClass("btn-primary").toggleClass("btn-info");
            $(this).text("save");
            $(this).closest('div.innerPopup').find('textarea').prop("disabled", false);
            $(this).closest('div.innerPopup').find('textarea').css('border', "1px solid #36E6F5");
        });

        $(document).on('click', '.btn-info.save', function(e, t, q) {
            $(this).toggleClass("edit").toggleClass("save").toggleClass("btn-primary").toggleClass("btn-info");
            $(this).text("edit")
            $(this).closest('div.innerPopup').find('textarea').prop("disabled", true);
            $('div.gm-style-iw textarea').prop("disabled", true)
            $('div.gm-style-iw textarea').css({ background: "white", border: "none" });

            var tmp = $(this).closest('div.innerPopup').find('input[type=hidden]').val();
            var dataPassed = new Object();
            if (tmp) {
                dataPassed = parseInt(tmp);
            } else dataPassed = null;

            $.ajax({
                url: '/Home/UpsertMarker',
                data: {
                    id: dataPassed,
                    text: $(this).closest('div.innerPopup').find('textarea').val()
                    //imageUrl:$(this).closest('div.innerPopup').find('napravi da se dobavq snimka :D')
                },
                success: function(data) {
                    console.log("success on upsert");
                    //return id na marker i go slojiv i hidden poleto                         
                }
            });
        });
    }
};




