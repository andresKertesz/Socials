window.getGeolocation = function (dotnetHelper) {
    if ("geolocation" in navigator) {
        navigator.geolocation.getCurrentPosition(function(position) {
            dotnetHelper.invokeMethodAsync('ReceiveGeolocation', position.coords.latitude, position.coords.longitude);
        });
    } else {
        dotnetHelper.invokeMethodAsync('ReceiveGeolocation', null, null);
    }
};
