using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class InterfaceView_User : InterfaceView
    {
        public Interface_String interface_basic { get; private set; }
        public List<Interface_String> Dates = new List<Interface_String>();

        public InterfaceView_User(User user) : base()
        {
            //Базовое представление в виде строчки
            interface_basic = new Interface_String(user.nick, "", true);
            interface_basic.AddColor("", user.group.ColorHTML);
            try
            {
                interface_basic.AddImagePathToolTip(Nicks.AvaPath[user.nick].ToString(), "Это пользователь " + user.nick);
            }
            catch
            {
                interface_basic.AddImagePathToolTip("No_ava.png", "Это пользователь " + user.nick);
            }
            //Даты
            Dates.Add(new Interface_String("Даты", "", false));
            Dates.Last().AddColor("#FFC9C9C9", "");
            Dates.Add(new Interface_String("Дата регистрации",user.registration.ToLongDateString(),false));
            Dates.Add(new Interface_String("Последнее посещение", user.last_activity.ToLongDateString(), false));
        }
    }
}
