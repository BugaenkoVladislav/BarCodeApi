using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UsersUI.Models
{
    public class Room
    {
        public Room(long idRoom, long roomNumber, int x, int y)
        {
            IdRoom = idRoom;
            RoomNumber = roomNumber;
            X = x;
            Y = y;
        }

        public long IdRoom { get; set; }

        public long RoomNumber { get; set; }

        public int X { get; set; }

        public int  Y { get; set; }

        public static async void InitializeRoom(List<Room> rooms)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = "https://localhost:7251/ReturnAllRooms";
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    string json = await response.Content.ReadAsStringAsync();       //читает json строку                                
                    JArray arr = JArray.Parse(json);
                    foreach (JObject e in arr.Cast<JObject>())
                    {
                        rooms.Add(new Room(Convert.ToInt32(e["idRoom"]), Convert.ToInt32(e["roomNumber"]), Convert.ToInt32(e["x"]), Convert.ToInt32(e["y"]))); //добавляем новую комнату   
                    }
                }

            }
        }
    }
}
