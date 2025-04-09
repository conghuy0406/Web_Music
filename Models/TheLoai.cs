using System;
using System.Collections.Generic;

namespace Web_Music.Models;

public partial class TheLoai
{
    public int MaTheLoai { get; set; }

    public string TenTheLoai { get; set; } = null!;

    public virtual ICollection<BaiHat> BaiHats { get; set; } = new List<BaiHat>();
}
