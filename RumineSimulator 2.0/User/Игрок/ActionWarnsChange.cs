using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionWarnsChange : Action
    {
        int amount;
        string reason;
        User user_goal;


        public ActionWarnsChange(string name, ActionsEnum id, int Time_sec, int Amount, string Reason, User User_goal) : base(name, id, Time_sec)
        {
            amount = Amount;
            reason = Reason;
            user_goal = User_goal;
        }
        public override bool DoAction()
        {
            bool check;
            check = user_goal.LastBan.AddWarnings(Player.user,amount,reason,user_goal);
            if (!check)
            {
                result = $"Вы не можете изменить уровень предупреждений, существо более могущественно.";
            }
            else
            {
                result = $"Уровень предупреждений пользователя {user_goal.nick} изменен";
            }
            return !check;
        }
    }
}
