// Lắng nghe sự kiện khi nhấn vào nút play (ở các thẻ <a> với class .play-btn)
document.querySelectorAll('.play-btn').forEach(playButton => {
    playButton.addEventListener('click', function (event) {
        event.preventDefault(); // Ngăn chặn hành động mặc định của thẻ a

        // Lấy phần tử audio và ảnh bìa album
        const audioPlayer = document.getElementById('audio-player');
        const albumCover = document.getElementById('album-cover');

        // Kiểm tra nếu phần tử audio chưa được load thì thực hiện việc tải lại
        if (audioPlayer.paused) {
            // Thêm hiệu ứng quay cho ảnh bìa khi nhạc bắt đầu phát
            audioPlayer.play();  // Chạy nhạc

            // Thêm lớp 'audioplayer-playing' vào phần tử audioplayer
            document.querySelector('.audioplayer').classList.add('audioplayer-playing');

            // Thêm lớp 'rotating' vào ảnh album khi nhạc phát
            albumCover.classList.add('rotating');  // Bắt đầu quay ảnh

            // Khi nhạc dừng, loại bỏ hiệu ứng quay
            audioPlayer.addEventListener('pause', function () {
                albumCover.classList.remove('rotating');
            });

            // Khi nhạc kết thúc, loại bỏ hiệu ứng quay
            audioPlayer.addEventListener('ended', function () {
                albumCover.classList.remove('rotating');
            });
        } else {
            // Nếu nhạc đã được phát, chỉ cần tạm dừng và dừng quay
            audioPlayer.pause();  // Dừng nhạc

            // Loại bỏ lớp 'audioplayer-playing' khi nhạc dừng

            albumCover.classList.remove('rotating');  // Dừng quay ảnh
        }
    });
});




