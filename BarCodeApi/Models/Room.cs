using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BarCodeApi.Models;

public partial class Room
{
    public long IdRoom { get; set; }

    public long RoomNumber { get; set; }

    public long X { get; set; }

    public long Y { get; set; }

    
}
