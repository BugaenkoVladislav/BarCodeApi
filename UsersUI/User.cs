using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
