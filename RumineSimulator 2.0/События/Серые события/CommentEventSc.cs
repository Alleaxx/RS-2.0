using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class MessageEventSl : SlightEvent
    {
        //Свойства события ------------------------------------
        public int likes { get; }
        public User Author { get; }

        //Рандомное сообщение ------------------------------------
        public MessageEventSl() : base()
        {
            Author = EventsControl.rnd_User;
            Name = $"Сообщение от {Author.nick}";
            Rareness = EventImportance.usual;
            participants.Add(Author, "Автор");
            string event_descr = $"{Abbrev.date} пользователь {Author.nick} написал обычное сообщение на форуме румине. ";
            event_descr = event_descr + $"За него автор получил {likes} симпатий. ";
            description = event_descr;
            EventAction();
        }
        //Сообщение с заданными параметрами
        public MessageEventSl(User userMessage,EventImportance rareness, int Likes = 0,string descr = "Сообщение") : base()
        {
            Author = userMessage;
            //Создание события
            Name = $"Сообщение от {Author.nick}";
            Rareness = rareness;
            participants.Add(Author, "Автор");
            description = descr;
            EventAction();
        }


        //Действие события ------------------------------------
        public override void EventAction()
        {
            Author.last_activity = Date.current_date;
            Author.messages++;
            Activity.Hour_messages++;
            Author.likes += likes;

            base.EventAction();
        }
    }

    class CommentEventSl : SlightEvent
    {
        //Свойства события ------------------------------------
        public int Rating { get; }
        public User Author { get; }

        //Случайный комментарий ------------------------------------
        public CommentEventSl() : base()
        {
            Author = EventsControl.rnd_User;
            Name = $"Комментарий {Author.nick}";
            Rareness = EventImportance.usual;
            participants.Add(Author, "Автор");
            description = $"{Abbrev.date} пользователь {Author} прокомментировал новость на сайте. ";
            Rating = AdvRnd.random.Next(3);
            description = description + $"За него автор получил {Rating} рейтинга. ";
            EventAction();
        }
        //Комментарий с заданными параметрами
        public CommentEventSl(User author,EventImportance rareness,int rating,string descr) : base()
        {
            Author = author;
            Name = $"Комментарий {Author.nick}";
            Rareness = rareness;
            participants.Add(Author, "Автор");
            description = descr;
            Rating = rating;
            EventAction();
        }

        //Действие события ------------------------------------
        public override void EventAction()
        {
            Author.last_activity = Date.current_date;
            Author.comments++;
            Author.likes += Rating;
            Activity.day_comments++;
            Author.EventEndedReaction(this, new RsEventArgs());
            base.EventAction();
        }
    }
}
