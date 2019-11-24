function initMap(location) {
    var uluru = { lat: -12.9715854, lng: -38.5132827 };
    var map = new google.maps.Map(
        document.getElementById('map'), { zoom: 19, center: uluru });
        placeMarkerAndPanTo(location, map);
}

var marker;

function deleteMarkers() {
    clearMarkers();
    markers = [];
}


function placeMarkerAndPanTo(latLng, map) {
    if (marker) {
        marker.setPosition(latLng);
    } else {
        marker = new google.maps.Marker({
            position: latLng,
            map: map
        });
    }
    map.panTo(latLng);
    var loc = document.getElementById("Location");
    loc.value = latLng;
}