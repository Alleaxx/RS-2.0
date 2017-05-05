using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class EventSrach : EventDusskussion
    {
        public int hardness { get; set; }
        public EventSrach(string Name,EventsEnum Type,Reason rreason,SrachDiskussion Topic = SrachDiskussion.random): base(Name,Type,rreason)
        {
            hardness = random.Next(1, 11);
            icon = new BitmapImage(new Uri("pack://application:,,,/Resources/sword.jpg"));
            MessageModSet();
        }
        public override bool EventPassing()
        {
            return base.EventPassing();
        }
        public override string ReasonTextGenerate()
        {
            return base.ReasonTextGenerate();
        }
        public override void MessageModSet()
        {
            message_mod = hardness + participants.Count;

        }
        public override void EventEnd()
        {
            int sum_down = 0;
            foreach (User user in first_side)
            {
                foreach (User user_min in opposite_side)
                {
                    int minus = random.Next(0, hardness);
                    user.relations.All[user_min].Chanse_friendness_down += minus;
                    sum_down += minus;
                }

            }
            foreach (User user in opposite_side)
            {
                foreach (User user_min in first_side)
                {
                    int minus = random.Next(0, hardness);
                    user.relations.All[user_min].Chanse_friendness_down += minus;
                    sum_down += minus;
                }
            }
            result_text = $"Срач закончен, итоговый негатив - {sum_down}";
            base.EventEnd();
        }
        public override string TopicTextGenerate()
        {
            switch (topic)
            {
                case SrachDiskussion.gaymssrach:
                    name = "Игросрач";
                    break;
                case SrachDiskussion.osesrach:
                    name = "Осесрач";
                    break;
                case SrachDiskussion.userrak:
                    name = $"Срач про {reason.user_goal.nick}";
                    break;
                default:
                    name = "Махач";
                    break;
            }
            return base.TopicTextGenerate();
        }
        public override List<string> ParametersTextGenerate()
        {
            base.ParametersTextGenerate();
            text_parametres.Add($"Жесткость срача: {hardness}");
            return text_parametres;
        }
    }
}
