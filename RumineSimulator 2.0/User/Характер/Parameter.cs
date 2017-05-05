using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Parameter
    {
        public User owner { get; private set; }
        public string name { get; private set; }
        private int param_value;
        public int Param_value
        {
            get
            {
                return param_value;
            }
            set
            {
                param_value = value;
                if (param_value > param_gran)
                    param_value = param_gran;
                if (param_value < 0)
                    param_value = 0;
            }
        }
        private int param_gran;
        public bool in_progress
        {
            get
            {
                foreach (Action act in PlayerActions.actions_quie)
                {
                    if (act is DoParamResearch)
                    {
                        DoParamResearch check = (DoParamResearch)act;
                        if (check.user_goal == owner && check.param.name == name)
                            return true;
                    }
                }
                return false;
            }
        }

        public bool unknown { get; set; }
        public bool allParams_toKnow { get; set; }
        public int time_min { get; private set; }

        public Parameter(User Owner,string Name, int value, int gran, bool Unknown, int Time_min, bool AllParam = false)
        {
            owner = Owner;
            name = Name;
            param_gran = gran;
            Param_value = value;
            unknown = Unknown;
            time_min = Time_min;
            allParams_toKnow = AllParam;
        }

    }
}
