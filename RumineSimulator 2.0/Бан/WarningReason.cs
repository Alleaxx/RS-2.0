using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class WarningReason
    {
        static Random random = new Random();
        public static List<string> warning_reasons = new List<string>() { };

        public static string ReturnReason()
        {
            return warning_reasons[random.Next(warning_reasons.Count)];
        }
    }
}
