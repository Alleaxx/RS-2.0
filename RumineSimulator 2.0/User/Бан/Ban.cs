using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Ban
    {
        public User owner { get; set; }

        public bool banned { get; private set; }

        public List<Warning> warnings = new List<Warning>();
        public int warn_sum { get; private set; }

        public bool ac { get; set; }

        public DateTime ban_start = new DateTime();

        public DateTime ban_end = new DateTime();

        public Ban(User Owner,bool AC = false,int days = 0)
        {
            owner = Owner;
            ac = AC;
            if (AC)
            {
                banned = true;
                ban_start = Date.current_date;
                ban_end = ban_start.AddDays(days);
            }
            WarnsCount();
        }

        private void WarnsCount()
        {
            warn_sum = 0;
            foreach (Warning warn in warnings)
            {
                warn_sum += warn.amount;
            }
        }

        public bool AddWarnings(User author,int amount, string reason,User goal)
        {
            if ((!goal.mod && author.mod) | (author == Player.user && GlobalParams.GodMode))
            {
                warnings.Add(new Warning(author, amount, reason, goal));
                WarnsCount();
                if (warn_sum > 99)
                {
                    banned = true;
                    ban_start = Date.current_date;
                    ban_end = ban_start.AddDays(BanCenter.warning_durings);
                    if (warn_sum > 100)
                    {
                        warnings[warnings.Count - 1].amount -= warn_sum - 100;
                        WarnsCount();
                    }
                    owner.relations.All[author].Chanse_friendness_down += 50;    
                }
                return true;
            }
            else
            {
                return false;
            }

        }
        public Ban BanEnd(User user)
        {
            if(warn_sum == 100)
            {
                banned = false;
                warnings.Add(new Warning(UserList.Users[0],-10, "Автоматическое снижение уровня предупреждений после блокировки",user));
            }
            else
            {
                banned = false;
            }
            return this;
        }

        public List<Warning> WarnSortByDate()
        {
            var sortedGr = from i in warnings
                           orderby i.date descending
                           select i;
            return sortedGr.ToList();
        }

    }
}
