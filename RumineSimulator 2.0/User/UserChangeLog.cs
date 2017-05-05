using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class UserDayLog
    {
        DateTime create_date = new DateTime();

        int old_messages;
        int new_messages;

        int old_likes;
        int new_likes;

        int old_comments;
        int new_comments;

        int old_ratecomments;
        int new_ratecomments;

        int old_news;
        int new_news;

        double old_reputation;
        double new_reputation;

        Group old_group;
        Group new_group;

        Karma old_karma;
        Karma new_karma;

        public string text_descr { get; private set; }
        public string addings { get; set; }

        public UserDayLog(User user)
        {
            old_likes = user.likes;
            old_messages = user.messages;
            old_reputation = user.reputation.Base_reputation;
            old_karma = user.karma;
            old_group = user.group;
            old_comments = user.comments;
            old_ratecomments = user.comments_rate;
            old_news = user.news;
        }
        public void Changes(User user)
        {
            new_group = user.group;
            new_reputation = user.reputation.Base_reputation;
            new_messages = user.messages;
            new_likes = user.likes;
            new_karma = user.karma;
            new_comments = user.comments;
            new_ratecomments = user.comments_rate;
            new_news = user.news;

            create_date = Date.current_date;
            text_descr = "\n";
            text_descr = text_descr + "\nИзменения юзера " + $"{user.nick} за {create_date.ToShortDateString()}" ;
            if(old_group != new_group)
            {
                text_descr = text_descr + "\n" + $"Группа: {old_group.name} --> {new_group.name}";
            }
            if (old_reputation != new_reputation)
            {
                text_descr = text_descr + "\n" + $"Репутация: {Math.Round(old_reputation, 1)} --> {Math.Round(new_reputation, 1)}";
            }
            if (old_karma != new_karma)
            {
                text_descr = text_descr + "\n" + $"Карма: {old_karma.karma} --> {new_karma.karma}";
            }
            if (old_comments != new_comments)
            {
                text_descr = text_descr + "\n" + $"Комментарии: {old_comments} --> {new_comments}";
            }
            if (old_ratecomments != new_ratecomments)
            {
                text_descr = text_descr + "\n" + $"Рейтинг: {old_ratecomments} --> {new_ratecomments}";
            }
            if (old_news != new_news)
            {
                text_descr = text_descr + "\n" + $"Новости: {old_news} --> {new_news}";
            }
            if (old_messages != new_messages)
            {
                text_descr = text_descr + "\n" + $"Сообщения: {old_messages} --> {new_messages}";
            }
            if (old_likes != new_likes)
            {
                text_descr = text_descr + "\n" + $"Симпатии: {old_likes} --> {new_likes}";
            }
            if (addings != "")
            {
                text_descr = text_descr + "\n" + $"Примечания: {addings}";
            }
        }
    }
}
