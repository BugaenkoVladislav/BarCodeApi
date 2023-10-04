using System;
using System.Collections.Generic;
using BarCodeApi.Models;

namespace BarCodeApi;

public partial class UserSustainLogRoom
{
    public long IdUserSustainLogRoom { get; set; }

    public long IdUser { get; set; }

    public long IdRoom { get; set; }

    public bool Sustain { get; set; }

    public virtual Room IdRoomNavigation { get; set; } = null!;

    public virtual User IdUserNavigation { get; set; } = null!;
}
