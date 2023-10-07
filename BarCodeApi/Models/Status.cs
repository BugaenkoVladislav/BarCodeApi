using System;
using System.Collections.Generic;

namespace BarCodeApi.Models;

public partial class Status
{
    public long IdStatus { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
