function searchAlbums() {
    var input = document.getElementById('albumSearch').value.toLowerCase();
    var albumItems = document.querySelectorAll('.single-album-item');

    albumItems.forEach(function (item) {
        var albumName = item.querySelector('h5').textContent.toLowerCase();
        if (albumName.includes(input)) {
            item.style.display = 'block';
        } else {
            item.style.display = 'none';
        }
    });
}

