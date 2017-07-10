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
        public int month_oldness { get;private set; }
        public int News { get;private set; }
        public int News_quality { get; private set; }
        public int Reputation { get; private set; }
        public bool special_condition { get; private set; }

        public GroupCondition(float Karma, int M_oldness, int News, int News_quality,int Reputation, bool Special_condition = false)
        {
            this.Karma = Karma;
            this.month_oldness = M_oldness;
            this.News = News;
            this.News_quality = News_quality;
            this.Reputation = Reputation;
            special_condition = Special_condition;
        }
    }
}
