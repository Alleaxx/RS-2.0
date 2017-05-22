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
        public List<Interface_Value> Dates { get; private set; }

        public InterfaceView_User(User user) : base()
        {
            //Базовое представление в виде строчки
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
            //Даты
            Dates.Add(new Interface_Value("Даты", "", false));
            Dates.Last().AddColor("#FFC9C9C9", "");
            Dates.Add(new Interface_Value("Дата регистрации",user.registration.ToLongDateString(),false));
            Dates.Add(new Interface_Value("Последнее посещение", user.last_activity.ToLongDateString(), false));
        }
    }
}
