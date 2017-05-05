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
        public int Chanse_up
        {
            get
            {
                return chanse_up;
            }
            set
            {
                chanse_up = value;
                if (chanse_up > 100)
                    chanse_up = 100;
                if (chanse_up < 0)
                    chanse_up = 0;
            }
        }
        public int Chanse_down
        {
            get
            {
                return chanse_down;
            }
            set
            {
                chanse_down = value;
                if (chanse_down > 100)
                    chanse_down = 100;
                if (chanse_down < 0)
                    chanse_down = 0;
            }
        }
        public CharParam(User Owner, string name,int value,int gran,bool Unknown,int Time_min,bool AllParam) : base(Owner,name,value,gran,Unknown,Time_min,AllParam)
        {         
            Chanse_down = 0;
            Chanse_up = 0;
        }
    }
}
