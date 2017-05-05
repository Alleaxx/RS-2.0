using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    public enum SrachDiskussion
    {
        random,

        osesrach,
        userrak,
        gaymssrach,
    }
    public static class SrachDiskussions
    {
        public static SrachDiskussion ReturnRndDiskuss()
        {
            List<SrachDiskussion> list = new List<SrachDiskussion>();
            list.Add(SrachDiskussion.osesrach);
            list.Add(SrachDiskussion.userrak);
            list.Add(SrachDiskussion.gaymssrach);
            return list[AdvRandom.random.Next(list.Count)];
        }
    }
}
