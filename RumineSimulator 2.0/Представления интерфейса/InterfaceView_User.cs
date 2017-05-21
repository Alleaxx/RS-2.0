using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_User : InterfaceView
    {
        public Interface_Value interface_basic { get; private set; }

        public InterfaceView_User(User user) : base()
        {
            interface_basic = new Interface_Value(user.nick, "", true);
            interface_basic.AddColor("", user.group.ColorHTML);
            try
            {
                interface_basic.AddImagePathToolTip(Nicks.AvaPath[user.nick].ToString(), "Это пользователь " + user.nick);
            }
            catch
            {
                interface_basic.AddImagePathToolTip("No_ava.png", "Это пользователь " + user.nick);
            }

        }
    }
}
