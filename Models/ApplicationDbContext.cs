using Microsoft.EntityFrameworkCore;

namespace Web_Music.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Album>().HasKey(a => a.MaAlbum);

            // Tương tự cho các entity khác nếu cần:
            modelBuilder.Entity<BaiHat>().HasKey(b => b.MaBaiHat);
            modelBuilder.Entity<CaSi>().HasKey(c => c.MaCaSi);
            modelBuilder.Entity<NguoiDung>().HasKey(n => n.MaNguoiDung);
            modelBuilder.Entity<TheLoai>().HasKey(t => t.MaTheLoai);

            base.OnModelCreating(modelBuilder);

            // Cấu hình mối quan hệ giữa Album và CaSi
            modelBuilder.Entity<Album>()
                .HasOne(a => a.MaCaSiNavigation)
                .WithMany(c => c.Albums)
                .HasForeignKey(a => a.MaCaSi);

            // Cấu hình mối quan hệ giữa BaiHat và Album
            modelBuilder.Entity<BaiHat>()
                .HasOne(b => b.MaAlbumNavigation)
                .WithMany(a => a.BaiHats)
                .HasForeignKey(b => b.MaAlbum);

            // Cấu hình mối quan hệ giữa BaiHat và CaSi
            modelBuilder.Entity<BaiHat>()
                .HasOne(b => b.MaCaSiNavigation)
                .WithMany(c => c.BaiHats)
                .HasForeignKey(b => b.MaCaSi);

            // Cấu hình mối quan hệ giữa BaiHat và TheLoai
            modelBuilder.Entity<BaiHat>()
                .HasOne(b => b.MaTheLoaiNavigation)
                .WithMany(t => t.BaiHats)
                .HasForeignKey(b => b.MaTheLoai);
        }
        public DbSet<Album> Albums { get; set; }
        public DbSet<BaiHat> BaiHats { get; set; }
        public DbSet<NguoiDung>NguoiDungs { get; set; }
        public DbSet<TheLoai>TheLoais { get; set; }
        public DbSet<CaSi>CaSis { get; set; }


    }
}
