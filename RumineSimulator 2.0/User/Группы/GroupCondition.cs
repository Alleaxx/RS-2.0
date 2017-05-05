using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class GroupCondition
    {
        public float karma { get; private set; }
        public int m_oldness { get;private set; }
        public int news { get;private set; }
        public int news_quality { get; private set; }
        public int reputation { get; private set; }
        public bool mod { get; private set; }
        public GroupCondition(float Karma, int M_oldness, int News, int News_quality,int Reputation, bool Mod = false)
        {
            karma = Karma;
            m_oldness = M_oldness;
            news = News;
            news_quality = News_quality;
            reputation = Reputation;
            mod = Mod;
        }
    }
}
