using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class CharFeature
    {
        public string name { get; private set; }
        private int value;
        public int Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                if (this.value > GlobalParams.charFeature_cap)
                    this.value = GlobalParams.charFeature_cap;
                if (this.value < 0)
                    this.value = 0;
            }
        }
        public CharFeature(string Name)
        {
            name = Name;
        }
        public void SetValue(int in_value)
        {
            Value = in_value;
        }

    }
}
