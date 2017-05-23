using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class CharParam : Parameter
    {
        private int chanse_up;
        private int chanse_down;

        public CharParam(User Owner, string name,int value,int gran,bool Unknown,int Time_min,bool AllParam) : base(Owner,name,value,gran,Unknown,Time_min,AllParam)
        {         

        }
    }
}
