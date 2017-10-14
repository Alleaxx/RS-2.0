using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class CharFeature
    {
        //Имя и значение
        public string name { get; set; }
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
                if (value > max)
                    value = max;
                else if (value < min)
                    value = min;
            }
        }
        //Максимальные и минимальные параметры
        protected int min;
        protected int max;

        public CharFeature(string Name)
        {
            name = Name;
        }
        public CharFeature()
        {
            min = 0;
            max = 10;
        }
        public void SetValue(int in_value)
        {
            Value = in_value;
        }
    }
}
