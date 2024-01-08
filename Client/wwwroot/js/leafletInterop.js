var createdMap = null

window.createMap = (id, lat, lng, zoom) => {
    const map = L.map(id).setView([lat, lng], zoom);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png').addTo(map);
    createdMap = map
};

window.addMarker = (lat, lng, title) => {
    const marker = L.marker([lat, lng]).addTo(createdMap);
    marker.bindPopup(title);
};

window.disposeMap = (map) => {
    createdMap.remove();
};
