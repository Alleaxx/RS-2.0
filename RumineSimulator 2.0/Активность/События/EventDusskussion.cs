using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class EventDusskussion : Event
    {
        public SrachDiskussion topic { get; private set; }
        public string topic_text { get; private set; }
        public List<User> first_side = new List<User>();
        public List<User> opposite_side = new List<User>();
        public bool random_topic { get; private set; }
        public EventDusskussion(string Name, EventsEnum Type, Reason rreason, SrachDiskussion Topic = SrachDiskussion.random): base(Name,Type,rreason)
        {
            creators.Add(reason.initiator);
            first_side.Add(reason.initiator);
            if (reason.user_goal != reason.initiator)
            {
                opposite_side.Add(reason.user_goal);
                participants.Add(reason.user_goal);
            }
            MessageModSet();
            if (Topic == SrachDiskussion.random)
            {
                if (reason.type == ReasonsEnum.message)
                {
                    topic = SrachDiskussions.ReturnRndDiskuss();
                }
            }
            else
            {
                topic = Topic;
            }
            ReasonTextGenerate();
            TopicTextGenerate();
            icon = new BitmapImage(new Uri("pack://application:,,,/Resources/beer.jpg"));
        }
        public override bool EventPassing()
        {
            if (AdvRandom.PersentChanseBool(10))
            {
                User rnd_user = Activity.online[random.Next(Activity.online.Count)];
                if (!participants.Contains(rnd_user))
                {
                    participants.Add(rnd_user);
                    if (creators[0].relations.friends.ContainsKey(rnd_user) || creators[0].relations.comrades.ContainsKey(rnd_user))
                    {
                        first_side.Add(rnd_user);
                    }
                    else if (creators[0].relations.enemies.ContainsKey(rnd_user) || creators[0].relations.unfriends.ContainsKey(rnd_user))
                    {
                        opposite_side.Add(rnd_user);
                    }
                    else
                    {
                        if (random.Next(2) == 0)
                            first_side.Add(rnd_user);
                        else
                            opposite_side.Add(rnd_user);
                    }
                }
            }
            MessageModSet();
            return base.EventPassing();
        }
        public override void MessageModSet()
        {
            message_mod = participants.Count;
        }
        public virtual string TopicTextGenerate()
        {
            switch (topic)
            {
                case SrachDiskussion.gaymssrach:
                    topic_text = "Обсуждение игр";
                    break;
                case SrachDiskussion.osesrach:
                    topic_text = "Обсуждение ОС";
                    break;
                case SrachDiskussion.userrak:
                    topic_text = $"Обсуждение {reason.user_goal.nick}";
                    break;
                default:
                    topic_text = "Деятельный разговор";
                    break;
            }
            return topic_text;
        }
        public override List<string> ParametersTextGenerate()
        {
            base.ParametersTextGenerate();
            text_parametres.Add($"Тема:  {topic_text}");
            return text_parametres;
        }

    }
}
