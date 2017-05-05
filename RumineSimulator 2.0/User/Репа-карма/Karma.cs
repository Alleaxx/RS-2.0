using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Karma
    {
        public float kar_news { get; private set; }
        public float kar_activity { get; private set; }
        public float kar_reputation { get; private set; }
        public float karma { get; private set; }

        public Karma(User user)
        {
            KarmaUpdate(user);

        }

        public void KarmaUpdate(User user)
        {
            #region За новости
            if (user.news == 0)
                kar_news = 0;
            else if (user.news >= 1 && user.news < 10)
                kar_news = 0.1F;
            else if (user.news >= 10 && user.news < 20)
                kar_news = 0.2F;
            else if (user.news >= 20 && user.news < 60)
                kar_news = 0.3F;
            else if (user.news >= 60 && user.news < 101)
                kar_news = 0.4F;
            else if (user.news >= 101 && user.news < 141)
                kar_news = 0.5F;
            else if (user.news >= 141 && user.news < 181)
                kar_news = 0.6F;
            else if (user.news >= 181 && user.news < 221)
                kar_news = 0.7F;
            else if (user.news >= 221 && user.news < 261)
                kar_news = 0.8F;
            else if (user.news >= 261 && user.news < 302)
                kar_news = 0.9F;
            else if (user.news >= 302)
                kar_news = 1F;
            #endregion
            #region За активность
            if (user.comments < 20)
                kar_activity = 0.2F;
            else if ((user.comments >= 20 && user.comments < 100) && user.m_oldness >= 2)
                kar_activity = 0.3F;
            else if ((user.comments >= 100 && user.comments < 200) && user.m_oldness >= 4)
                kar_activity = 0.4F;
            else if ((user.comments >= 200 && user.comments < 300) && user.m_oldness >= 6)
                kar_activity = 0.5F;
            else if ((user.comments >= 300 && user.comments < 500) && user.m_oldness >= 8)
                kar_activity = 0.6F;
            else if ((user.comments >= 500 && user.comments < 700) && user.m_oldness >= 12)
                kar_activity = 0.7F;
            else if ((user.comments >= 700 && user.comments < 900) && user.m_oldness >= 18)
                kar_activity = 0.8F;
            else if ((user.comments >= 900 && user.comments < 1200) && user.m_oldness >= 24)
                kar_activity = 0.9F;
            else if (user.comments >= 1200 && user.m_oldness >= 6)
                kar_activity = 1;
            #endregion
            #region За репутацию
            if (user.reputation.Base_reputation < 1)
                kar_reputation = 0;
            else if (user.reputation.Base_reputation >= 10 && user.reputation.Base_reputation < 20)
                kar_reputation = 0.1F;
            else if (user.reputation.Base_reputation >= 20 && user.reputation.Base_reputation < 50)
                kar_reputation = 0.2F;
            else if (user.reputation.Base_reputation >= 50 && user.reputation.Base_reputation < 100)
                kar_reputation = 0.3F;
            else if (user.reputation.Base_reputation >= 100 && user.reputation.Base_reputation < 200)
                kar_reputation = 0.4F;
            else if (user.reputation.Base_reputation >= 200 && user.reputation.Base_reputation < 400)
                kar_reputation = 0.5F;
            else if (user.reputation.Base_reputation >= 400 && user.reputation.Base_reputation < 800)
                kar_reputation = 0.6F;
            else if (user.reputation.Base_reputation >= 800 && user.reputation.Base_reputation < 1200)
                kar_reputation = 0.7F;
            else if (user.reputation.Base_reputation >= 1200 && user.reputation.Base_reputation < 1600)
                kar_reputation = 0.8F;
            else if (user.reputation.Base_reputation >= 1600 && user.reputation.Base_reputation < 2000)
                kar_reputation = 0.9F;
            else if (user.reputation.Base_reputation >= 2000)
                kar_reputation = 1F;
            #endregion
            karma = kar_activity + kar_news + kar_reputation;
        }
    }
}
