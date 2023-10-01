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
            BitmapImage bitmapImage = new BitmapImage();
            int id = Convert.ToInt32(idUser.Text);
            using (HttpClient client = new HttpClient())
            {
                string uri = $"https://localhost:7251/ReturnBarCode?id={id}";
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = await client.GetAsync(uri);
                response.EnsureSuccessStatusCode();
                using (var ms = new MemoryStream(await response.Content.ReadAsByteArrayAsync()))
                {
                    
                    bitmapImage.StreamSource = ms;
                    
                }                
            }
            userQR.Source = bitmapImage;//не пашет пофикси 
            userQR.Visibility = Visibility.Visible;

        }

        private void userIdGenerator_Click(object sender, RoutedEventArgs e)
        {
            Generator();
        }
    }
}
