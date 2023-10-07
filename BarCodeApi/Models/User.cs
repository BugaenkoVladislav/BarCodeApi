using System;
using System.Collections.Generic;

namespace BarCodeApi.Models;

public partial class User
{
    public long IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Midname { get; set; } = null!;

    public long IdStatus { get; set; }

    public virtual Status IdStatusNavigation { get; set; } = null!;
}
