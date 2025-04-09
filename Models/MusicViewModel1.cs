namespace Web_Music.Models
{
    public class MusicViewModel1
    {
        public IEnumerable<Album> Albums { get; set; }
        public IEnumerable<BaiHat> BaiHats { get; set; }
        public IEnumerable<CaSi> CaSis { get; set; }
        public IEnumerable<NguoiDung> NguoiDungs { get; set; }
        public IEnumerable<TheLoai> TheLoais { get; set; }
    }
}
