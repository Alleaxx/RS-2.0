using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionDoNews : Action
    {
        private int quality;
        public int Quality
        {
            get
            {
                return quality;
            }
            set
            {
                quality = value;
                if (quality < 1)
                    quality = 1;
                if (quality > 100)
                    quality = 100;
            }
        }
        public ActionDoNews(string name, ActionsEnum id, int Time_Sec, int QQuality) : base(name, id, Time_Sec)
        {
            Quality = QQuality;
            time_min = Quality;
            time_remain = time_min;
        }
        public override bool DoAction()
        {
            Player.user.news++;
            result = $"Новость написана за {time_min} минут, качество {Quality}";
            Player.achieve_points += 15;
            return true;
        }
    }
}
