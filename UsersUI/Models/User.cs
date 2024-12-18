﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace UsersUI.Models
{
    public class User 
    {

        public long IdUser { get; set; }
        public long IdStatus { get; set; }
        public int Size {  get; set; }
        private Ellipse el = new Ellipse();
        public User(long idUser, long idStatus, int size)
        {
            Size = size;
            IdUser = idUser;
            IdStatus = idStatus;
            el.Width = size;
            el.Height = size;
            el.Stroke = System.Windows.Media.Brushes.Black;
            el.StrokeThickness = 1;
            el.Opacity = 1;
            el.Fill = Color;

        }

        public System.Windows.Media.Brush Color
        {
            get
            {
                switch (IdStatus)
                {
                    case 1:
                        return System.Windows.Media.Brushes.Green;
                    case 2:
                        return System.Windows.Media.Brushes.Red;
                }
                throw new NotImplementedException();
            }
        }
        public Ellipse Ellipse
        {
            get
            {
                return el;
            }
        }

        

        public static void DrawUsers(List<User> users, Grid grid)//только отрисовывает пользователей на старте
        {
            int f = 8;
            for (int i = 0; i < users.Count; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Height = users[i].Ellipse.Height;
                ellipse.Width = users[i].Ellipse.Width;
                ellipse.Fill = users[i].Color;
                ellipse.StrokeThickness = 1;
                Grid.SetRow(ellipse, 3);
                if (i < f)
                {
                    Grid.SetColumn(ellipse, i + 1);
                    ellipse.Margin = new Thickness(0, 0, 0, 0);
                }
                else
                {
                    Grid.SetColumn(ellipse, i % f + 1);
                    ellipse.Margin = new Thickness(0, i / f * 50, 0, 0);

                }
                Panel.SetZIndex(ellipse, 5);//обязательно указывай
                grid.Children.Insert(i, ellipse);
            }


        }
        public static async void InitializeHuman(List<User> users,Grid grid)
        {
            using (HttpClient client = new HttpClient())
            {
                string uri = "https://localhost:7251/ReturnAllUsersStatus";
                client.BaseAddress = new Uri(uri);
                using (HttpResponseMessage response = await client.GetAsync(uri))
                {
                    string json = await response.Content.ReadAsStringAsync();       //читает json строку                                
                    JArray arr = JArray.Parse(json);                    //переводит json строку в приемлимый формат
                    //разделяем значения и добавляем в список students
                    foreach (JObject e in arr.Cast<JObject>())
                    {
                        users.Add(new User(Convert.ToInt32(e["idUser"]), Convert.ToInt32(e["idStatus"]), 15));//добавляем нового пользователя    ["idUser"] - берется из поля Json в Swagger'e

                    }
                }

            }
            DrawUsers(users, grid);


        }

    }
}
