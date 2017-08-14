using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class TopicControl
    {
        public static List<Topic> topics = new List<Topic>();
        //Добавление случайных тем
        public static void TopicsInit()
        {
            Topic games = new Topic("Игры", TopicType.games);
            topics.Add(games);
            Topic windows = new Topic("Линукс", TopicType.windows);
            topics.Add(windows);
            Topic linux = new Topic("Виндовс", TopicType.linux);
            topics.Add(linux);
            Topic rumine = new Topic("Румайн", TopicType.rumine);
            topics.Add(rumine);
            Topic tlen = new Topic("Тлен", TopicType.tlen);
            topics.Add(tlen);
            Topic activity = new Topic("Активность", TopicType.activity);
            topics.Add(activity);
            Topic groups = new Topic("Группы", TopicType.groups);
            topics.Add(groups);
            Topic wtfs = new Topic("Ватафаки", TopicType.wtfs);
            topics.Add(wtfs);
            Topic moderators = new Topic("Модераторы", TopicType.moderators);
            topics.Add(moderators);
            Topic admins = new Topic("Админы", TopicType.admins);
            topics.Add(admins);
            Topic minecraft = new Topic("Майнкрафт", TopicType.minecraft);
            topics.Add(minecraft);
        }

        //Возвращение случайной темы
        public static Topic ReturnRndTopic()
        {
            int rnd_num = AdvRnd.random.Next(topics.Count);
            return topics[rnd_num];
        }
    }

    enum TopicType
    {
        games,windows, linux, rumine, tlen, activity, groups, wtfs, moderators, admins, minecraft
    }
}
