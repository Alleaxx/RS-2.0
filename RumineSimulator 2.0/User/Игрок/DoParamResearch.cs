using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class DoParamResearch : Action
    {
        public User user_goal { get; private set; }
        public Parameter param { get; private set; }
        public DoParamResearch(string name, ActionsEnum id, int Time_sec, User User,Parameter Param) : base(name, id, Time_sec)
        {
            user_goal = User;
            param = Param;
            time_min = param.time_min;
            time_remain = time_min;
        }
        public override bool DoAction()
        {
            param.unknown = false;
            Player.achieve_points += 10;
            result = $"Параметр {param.name} у пользователя {user_goal.nick} изучен! \n" + 
                "\n +10 пунктов к ачивкам!";
            return true;
        }
    }
}
