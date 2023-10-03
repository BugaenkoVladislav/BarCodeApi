using Microsoft.Win32;
using SixLabors.ImageSharp.Drawing;
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
        public async void UserIdGenerator(int id)
        {
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
                    Console.WriteLine(ex.Message);//!!!Пометь ошибку визуально
                }
                
               

            }
            

        }
        public async void AllQRGenerator(int countOfUsers)
        {
            using(HttpClient client = new HttpClient())
            {
                string uri = $"https://localhost:7251/GenerateAllBarCodes?countOfUsers={countOfUsers}";
                client.BaseAddress = new Uri(uri);
                HttpResponseMessage response = await client.GetAsync(uri);
                try
                {
                    response.EnsureSuccessStatusCode();
                    using(var stream =  await response.Content.ReadAsStreamAsync())
                    {                      
                        SaveFileDialog fileDialog = new SaveFileDialog();
                        fileDialog.DefaultExt = ".pdf";
                        fileDialog.FileName = "Document";
                        fileDialog.Filter = "PDF documents (.pdf)|*.pdf"; // Фильтр файлов по расширению
                        Nullable<bool> result = fileDialog.ShowDialog();//указывает какой результат

                        // Обработка результатов диалогового окна "Сохранить файл"
                        if (result == true && fileDialog.CheckPathExists)
                        {
                            string path = fileDialog.FileName;//указываем путь к файлу 
                            using (FileStream fileStream = new FileStream(path, FileMode.Create))//используем file stream чтобы записать stream в файл
                            {
                                stream.CopyTo(fileStream);//переносим данные из stream в file
                            }



                        }                       

                    }

                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        private void UserIdGenerator_Click(object sender, RoutedEventArgs e)
        {
            UserIdGenerator(Convert.ToInt32(idUser.Text));
        }

        private void AllQRGenerator_Click(object sender, RoutedEventArgs e)
        {
            AllQRGenerator(4);
        }
    }
}
