using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace Web_Music.Models;

public partial class BaiHat
{
    public int MaBaiHat { get; set; }

    public string TenBaiHat { get; set; } = null!;

    public int MaCaSi { get; set; }

    public int? MaAlbum { get; set; }

    public int MaTheLoai { get; set; }

    public TimeOnly ThoiLuong { get; set; }

    public string DuongDanFile { get; set; } = null!;
    public string AnhBia { get; set; } = null!;

    public int? LuotNghe { get; set; }

    public DateTime? NgayThem { get; set; }

    public virtual Album? MaAlbumNavigation { get; set; }
    [ValidateNever]
    public virtual CaSi MaCaSiNavigation { get; set; } = null!;
    [ValidateNever]
    public virtual TheLoai MaTheLoaiNavigation { get; set; } = null!;
}
