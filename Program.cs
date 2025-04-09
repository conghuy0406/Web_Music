using Microsoft.EntityFrameworkCore;
using Web_Music.Models;
using Web_Music.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình kết nối database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Thêm dịch vụ cho Controllers và Views
builder.Services.AddControllersWithViews();

// Cấu hình các repository
builder.Services.AddScoped<IUserRepositories, EFNguoiDungRepository>();
builder.Services.AddScoped<IAlbumRepository, EFAlbumRepository>();
builder.Services.AddScoped<ICasiRepository, EFCasiRepository>();
builder.Services.AddScoped<IBaiHatRepository, EFBaiHatRepository>();
builder.Services.AddScoped<ITheLoaiRepository, EFTheLoaiRepository>();

// Thêm dịch vụ Session
builder.Services.AddDistributedMemoryCache(); // Sử dụng bộ nhớ để lưu trữ session
builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian hết hạn session
    options.Cookie.HttpOnly = true; // Chỉ truy cập cookie từ HTTP
    options.Cookie.IsEssential = true; // Chỉ định cookie là bắt buộc
});

var app = builder.Build();

// Cấu hình các middleware trong request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt session
app.UseSession();

app.UseAuthorization();

// Cấu hình route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
