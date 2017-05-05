using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionDoComent : Action
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
        public ActionDoComent(string name, ActionsEnum id,int Time_sec,int QQuality) : base(name, id, Time_sec)
        {
            Quality = QQuality;
            time_min = Quality / 2;
            time_remain = time_min;
        }
        public override bool DoAction()
        {
            int writed_comments = AdvRandom.random.Next(2, 5);
            Player.user.comments += writed_comments;
            int comm_rate_plus = AdvRandom.random.Next(0, quality / 10) * writed_comments;
            Player.user.comments_rate += comm_rate_plus;
            result = $"{writed_comments} комментариев написано, рейтинг повышен на {comm_rate_plus}";
            Player.achieve_points += 5;
            return true;
        }
    }
}
