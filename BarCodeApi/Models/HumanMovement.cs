using System.ComponentModel.DataAnnotations;

namespace BarCodeApi.Models
{
    public class HumanMovement
    {
        public User User { get; set; }
        public Room Room { get; set; }
        public bool Sustain { get; set; }
    }
}
