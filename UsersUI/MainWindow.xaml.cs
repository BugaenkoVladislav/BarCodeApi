using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace UsersUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public async void Generator()
        {
            int id = Convert.ToInt32(idUser.Text);
            using (HttpClient client = new HttpClient())
            {
                string uri = $"https://localhost:7251/ReturnBarCode?id={id}";//cсылка на http запрос 
                client.BaseAddress = new Uri(uri);//указываем ссылку на запрос
                HttpResponseMessage response = await client.GetAsync(uri);//отправляем запрос 
                
                try
                {
                    response.EnsureSuccessStatusCode();//убеждаеся что результат положительный
                    using (var stream = await response.Content.ReadAsStreamAsync())//создаем отдельный поток который считывает ответ сервера в формате изображения
                    {
                        var bitmapImage = new BitmapImage();// создаем экземпляр bitmap image
                        bitmapImage.BeginInit();//используется для начала процесса инициализации объекта
                        bitmapImage.StreamSource = stream;//указываем streamsource'у(источник в формате потока)  наш поток
                        bitmapImage.EndInit();//окончание процесса инициализации объекта
                        userQR.Source = bitmapImage;// указываем наше изобржение
                    }
                }
                catch(HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
               

            }
            

        }

        private void userIdGenerator_Click(object sender, RoutedEventArgs e)
        {
            Generator();
        }
    }
}
