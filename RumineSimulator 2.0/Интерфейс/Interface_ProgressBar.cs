using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Interface_ProgressBar : Interface_String
    {
        public int min_value { get; private set; }
        public int max_value { get; private set; }
        public Interface_ProgressBar(string Text,int value,int min_value,int max_value,bool header = false): base(Text, value.ToString(), false, header)
        {
            this.min_value = min_value;
            this.max_value = max_value;
        }
    }
}
