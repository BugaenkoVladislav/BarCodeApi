using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace UsersUI
{
    internal class User:IMoveUser
    {

        public long IdUser { get; set; }
        public long IdStatus { get; set; }
        private Ellipse el = new Ellipse();
        public User(long idUser, long idStatus, int size) 
        {
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

        public void MoveUser(User user, int x, int y)
        {
            Grid.SetColumn(user.el, y);
            Grid.SetRow(user.el, x);
        }

        public static void DrawUsers(List<User> users, Grid grid)//отрисовывает пользователей
        {
            for (int i = 0; i < users.Count; i++)
            {
                Ellipse ellipse = new Ellipse();
                ellipse.Height = users[i].Ellipse.Height;
                ellipse.Width = users[i].Ellipse.Width;
                ellipse.Fill = users[i].Color;
                ellipse.StrokeThickness = 1;
                Grid.SetRow(ellipse, 3);
                ellipse.Margin = new Thickness((double)i * 5, (double)i * 1, (double)i * 16, (double)i * 10);
                Grid.SetColumn(ellipse, 1);
                Grid.SetZIndex(ellipse, 5);//обязательно указывай
                grid.Children.Insert(i, ellipse);
            }


        }
    }
}
