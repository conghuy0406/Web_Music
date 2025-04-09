$(document).ready(function () {
    // Khi người dùng nhấn vào thẻ <a> play-btn
    $("a.play-btn").click(function (event) {
        event.preventDefault(); // Ngừng hành động mặc định của thẻ a

        // Lấy dữ liệu từ các data attributes của thẻ <a>
        var tenBaiHat = $(this).data("ten-bai-hat");
        var caSi = $(this).data("ca-si");
        var anhBia = $(this).data("anh-bia");
        var audioSrc = $(this).data("audio");
        var tieusu = $(this).data("tieu-su");

        // Cập nhật thông tin vào phần section
        $("#album-cover").attr("src", "/img/bg-img/" + anhBia); // Cập nhật ảnh bìa
        $("#song-name").text("01. " + tenBaiHat); // Cập nhật tên bài hát
        $(".singer-name").text("Singer: " + caSi); // Cập nhật tên ca sĩ
        $(".singer-Tieusu").text("Tieu su:  " + tieusu);

        // Kiểm tra loại file (audio hay video)
        if (audioSrc.toLowerCase().endsWith(".mp3")) {
            // Nếu là file .mp3, hiển thị phần audio và ẩn video
            $("#audio-player").show();
            $("#video-player").hide();

            // Cập nhật nguồn âm thanh
            $("#audio-source").attr("src", "/" + audioSrc);
            $("audio")[0].load();  // Reload âm thanh mới
            $("audio")[0].play();  // Phát bài hát

            // Hiển thị ảnh bìa
            $("#album-cover").show();
            $('.audioplayer').addClass('audioplayer-playing');
        } else if (audioSrc.toLowerCase().endsWith(".mp4")) {
            // Nếu là file .mp4, hiển thị phần video và ẩn audio
            $("#audio-player").hide();
            $("#video-player").show();

            // Cập nhật nguồn video
            $("#video-source").attr("src", "/" + audioSrc);
            $("video")[0].load();  // Reload video mới
            $("video")[0].play();  // Phát video

            // Ẩn ảnh bìa khi phát video
            $("#album-cover").hide();
            $('.audioplayer').addClass('audioplayer-playing');
        }

        // Xử lý sự kiện khi nhạc/video dừng hoặc kết thúc
        $("audio")[0].onpause = function () {
            $("#album-cover").removeClass("rotating"); // Dừng quay khi nhạc tạm dừng
            $('.audioplayer').removeClass('audioplayer-playing'); // Loại bỏ lớp audioplayer-playing khi nhạc dừng
        };

        $("audio")[0].onended = function () {
            $("#album-cover").removeClass("rotating"); // Dừng quay khi nhạc kết thúc
            $('.audioplayer').removeClass('audioplayer-playing'); // Loại bỏ lớp audioplayer-playing khi nhạc kết thúc
        };

        $("video")[0].onpause = function () {
            $("#album-cover").removeClass("rotating"); // Dừng quay khi video tạm dừng
            $('.audioplayer').removeClass('audioplayer-playing'); // Loại bỏ lớp audioplayer-playing khi video dừng
        };

        $("video")[0].onended = function () {
            $("#album-cover").removeClass("rotating"); // Dừng quay khi video kết thúc
            $('.audioplayer').removeClass('audioplayer-playing'); // Loại bỏ lớp audioplayer-playing khi video kết thúc
        };

        // Cuộn trang đến phần <section id="PhatNhac">
        $('html, body').animate({
            scrollTop: $('#PhatNhac').offset().top
        }, 1000); // Thời gian cuộn trang (1000ms = 1s)
    });
});
