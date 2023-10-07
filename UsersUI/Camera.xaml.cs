using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UsersUI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersUI
{
    /// <summary>
    /// Логика взаимодействия для Camera.xaml
    /// </summary>
    public partial class Camera : Window , IMoveUser
    {
        List<User> users = new List<User>();
        List<Room> rooms = new List<Room>();


        public Camera()
        {
            InitializeComponent();
            User.InitializeHuman(users,grid);
            Room.InitializeRoom(rooms);
            //запуск перемещения по комнатам
        }
        public void MoveUser(User user, Room room)
        {
            Grid.SetColumn(grid.Children[users.IndexOf(user)], room.X);
            Grid.SetRow(grid.Children[users.IndexOf(user)], room.Y);
        }
        
        
        
        public async void CallUserChanges(int count)//test need
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = $"https://localhost:7251/MoveHuman?count={count}";
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JArray jArray = JArray.Parse(json);
                    foreach (JObject e in jArray.Cast<JObject>()) //Cast<JObject> преобразует наш массив JArray в cписок объектов формата json
                    {
                        MoveUser(users.First(x => x.IdUser == Convert.ToInt32(e["user"]["idUser"])), rooms.First(x => x.IdRoom == Convert.ToInt32(e["room"]["idRoom"])));                       
                    }
                }
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CallUserChanges(1);
        }
    }
    
}
