using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionDo
    {
        public ActionType type = new ActionType();

        public string result { get; set; }
        public User add_user { get; set; }
        private int bonus { get; set; }
        public bool succes { get; set; }
        public ActionTime time = new ActionTime();


        public ActionDo(Action act,ActionType Type)
        {
            type = Type;
        }
        public void SetProps(User Add_user = null,ActionTime Time = ActionTime.medium)
        {
            add_user = Add_user;
            time = Time;
        }
        public void Ending(Action act)
        {
            switch (type)
            {
                case ActionType.newMessage:
                    succes = true;
                    result = $"Вы написали сообщение в {act.ending.ToShortTimeString()} с результатом: {succes}";
                    break;
                default:
                    break;
            }
        }
    }
    enum ActionTime
    {
        Short,medium,Long
    }
}
