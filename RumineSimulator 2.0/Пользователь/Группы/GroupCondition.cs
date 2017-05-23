using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class GroupCondition
    {
        public float Karma { get; private set; }
        public int M_oldness { get;private set; }
        public int News { get;private set; }
        public int News_quality { get; private set; }
        public int Reputation { get; private set; }
        public bool Mod { get; private set; }

        public GroupCondition(float Karma, int M_oldness, int News, int News_quality,int Reputation, bool Mod = false)
        {
            this.Karma = Karma;
            this.M_oldness = M_oldness;
            this.News = News;
            this.News_quality = News_quality;
            this.Reputation = Reputation;
            this.Mod = Mod;
        }
    }
}
