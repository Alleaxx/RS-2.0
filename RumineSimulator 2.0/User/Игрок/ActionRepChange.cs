using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionRepChange : Action
    {
        double value;
        string reason;
        User user_goal;


        public ActionRepChange(string name,ActionsEnum id,int Time_sec,double Value,string Reason,User User_goal) : base(name, id, Time_sec)
        {
            value = Value;
            reason = Reason;
            user_goal = User_goal;
        }
        public override bool DoAction()
        {
            bool check;
            check = user_goal.reputation.ChangeReputation(Player.user, (float)value, reason);
            if (!check)
            {
                result = $"Неудача, вы уже изменяли репутацию пользователю {user_goal.nick} ! Либо это вы и есть";
            }
            else
            {
                result = $"Репутация пользователя {user_goal.nick} изменена";
                Player.achieve_points += 2;
            }
            return !check;
        }
    }
}
