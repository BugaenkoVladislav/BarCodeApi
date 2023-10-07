using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsersUI.Models;

namespace UsersUI
{
    interface IMoveUser
    {
        void MoveUser(User user,Room room);
        void CallUserChanges(int count);
    }
}
