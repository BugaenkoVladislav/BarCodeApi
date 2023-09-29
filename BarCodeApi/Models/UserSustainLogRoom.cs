using System;
using System.Collections.Generic;

namespace BarCodeApi.Models;

public partial class UserSustainLogRoom
{
    public long IdSustainLogRoom { get; set; }

    public long IdRoom { get; set; }

    public bool Sustain { get; set; }

    public long IdUser { get; set; }

    public virtual Room IdRoomNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
