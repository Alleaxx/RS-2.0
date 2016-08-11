using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class AdvRandom
    {
        static public Random random = new Random();

        static public bool PersentChanseBool(int persent)
        {
            if (random.Next(0, 100) <= persent)
                return true;
            else
                return false;
        }

        static public int PersentChansesCases(int[] chanses)
        {            
            for (int i = 0; i < chanses.Length; i++)
            {
                int persent = random.Next(0, 101);
                if (persent <= chanses[i])
                    return i + 1;
            }
            return 0;
        }
    }
}
