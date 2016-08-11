using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class UserTrait
    {
        public int chanse { get; private set; }
        public string short_name { get; private set; }
        public string full_description { get; private set; }
        public UserParamsEnum influence_param { get; private set; }
        public int condition_param { get; private set; }
        public int buff { get; private set; }

        public UserTrait(int Chanse, string s_name, string f_text,UserParamsEnum param,int Condition, int Buff)
        {
            chanse = Chanse;
            short_name = s_name;
            full_description = f_text;
            influence_param = param;
            condition_param = Condition;
            buff = Buff;
        }

        public void UserChange(User user)
        {
            switch (influence_param)
            {
                case UserParamsEnum.creativity:
                    user.character.Creativity += buff;
                    break;
                case UserParamsEnum.sciense:
                    user.character.Sciense += buff;
                    break;
            }
        }
    }
}
