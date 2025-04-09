using System;
using System.Collections.Generic;

namespace Web_Music.Models;

public partial class CaSi
{
    public int MaCaSi { get; set; }

    public string TenCaSi { get; set; } = null!;

    public string? AnhDaiDien { get; set; }

    public string? TieuSu { get; set; }

    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();

    public virtual ICollection<BaiHat> BaiHats { get; set; } = new List<BaiHat>();
}
