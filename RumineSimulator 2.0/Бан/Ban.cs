using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Ban :IAdvertisable
    {
        public int id { get; private set; }

        public User Owner { get; set; }

        public bool Banned { get; private set; }

        public List<Warning> warnings = new List<Warning>();
        public int Warn_sum { get; private set; }

        public bool admin_center { get; set; }

        public DateTime ban_start = new DateTime();

        public DateTime ban_end = new DateTime();

        public IntView_Ban InterfaceInfo
        {
            get
            {
                return new IntView_Ban(this);
            }
        }

        public Ban(User Owner,bool AC = false,int days = 0)
        {
            id = Owner.user_id;
            this.Owner = Owner;
            admin_center = AC;
            if (AC)
            {
                Banned = true;
                ban_start = Date.current_date;
                ban_end = ban_start.AddDays(days);
            }
            WarnsCount();
        }

        private void WarnsCount()
        {
            Warn_sum = 0;
            foreach (Warning warn in warnings)
            {
                Warn_sum += warn.amount;
            }
        }

        public bool AddWarnings(User author,int amount, string reason,User goal)
        {
            if ((!goal.mod && author.mod))
            {
                warnings.Add(new Warning(author, amount, reason, goal));
                WarnsCount();
                if (Warn_sum > 99)
                {
                    Banned = true;
                    ban_start = Date.current_date;
                    ban_end = ban_start.AddDays(GlobalParams.warning_durings);
                    if (Warn_sum > 100)
                    {
                        warnings[warnings.Count - 1].amount -= Warn_sum - 100;
                        WarnsCount();
                    } 
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
            if(Warn_sum == 100)
            {
                Banned = false;
                warnings.Add(new Warning(UsersControl.Users[0],-10, "Автоматическое снижение уровня предупреждений после блокировки",user));
            }
            else
            {
                Banned = false;
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

        #region IAdvertisable
        public List<string> Advertisments = new List<string>();
        public void AddAdvertisment(string adv)
        {
            Advertisments.Add(adv);
        }
        #endregion

    }
}
