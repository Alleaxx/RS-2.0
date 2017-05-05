using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Reason
    {
        public object reason { get; private set; }
        public User initiator { get; private set;}
        public User user_goal { get; private set; }
        public ReasonsEnum type { get; private set; }
        public Reason(object rreason,User Initiator,User User_goal,ReasonsEnum Type)
        {
            reason = rreason;
            type = Type;
            initiator = Initiator;
            user_goal = User_goal;
        }
    }
}
