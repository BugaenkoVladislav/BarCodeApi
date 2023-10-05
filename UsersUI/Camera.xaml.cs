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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UsersUI
{
    /// <summary>
    /// Логика взаимодействия для Camera.xaml
    /// </summary>
    public partial class Camera : Window 
    {
        List<User> users = new List<User>();
        public Camera()
        {
            InitializeComponent();
            InitializeHuman();
        }
        public async void InitializeHuman()
        {
            using(HttpClient client = new HttpClient())
            {
                string uri = "https://localhost:7251/ReturnAllUsersStatus";
                client.BaseAddress = new Uri(uri);
                using(HttpResponseMessage  response = await client.GetAsync(uri))
                {
                    string json = await response.Content.ReadAsStringAsync();       //читает json строку                                
                    JArray arr = JArray.Parse(json);                    //переводит json строку в приемлимый формат
                    //разделяем значения и добавляем в список students
                    foreach (JObject e in arr.Cast<JObject>())
                    {
                        users.Add(new User(Convert.ToInt32(e["idUser"]), Convert.ToInt32(e["idStatus"]),30));//добавляем нового пользователя    ["idUser"] - берется из поля Json в Swagger'e
                    }
                }
                
            }
            User.DrawUsers(users,grid);
        }
        
    }
    
}
