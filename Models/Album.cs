using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;

namespace Web_Music.Models;

public partial class Album
{
    public int MaAlbum { get; set; }

    public string TenAlbum { get; set; } = null!;

    public int MaCaSi { get; set; }

    public string? AnhBia { get; set; }

    public DateOnly? NgayPhatHanh { get; set; }

    public virtual ICollection<BaiHat> BaiHats { get; set; } = new List<BaiHat>();

    [ValidateNever]
    public virtual CaSi MaCaSiNavigation { get; set; } = null!;
}
