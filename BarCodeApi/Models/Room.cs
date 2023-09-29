using System;
using System.Collections.Generic;

namespace BarCodeApi.Models;

public partial class Room
{
    public long IdRoom { get; set; }

    public long RoomNumber { get; set; }

    public virtual ICollection<UserSustainLogRoom> UserSustainLogRooms { get; set; } = new List<UserSustainLogRoom>();
}
