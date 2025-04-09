using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_Music.Models;
[Table("NguoiDung")]
public partial class NguoiDung
{
    public int MaNguoiDung { get; set; }

    public string TenDangNhap { get; set; } = null!;

    public string MatKhau { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? VaiTro { get; set; }

    public DateTime? NgayTao { get; set; } = DateTime.UtcNow;
}
