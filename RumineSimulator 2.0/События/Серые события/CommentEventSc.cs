using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class MessageEventSl : SlightEvent
    {
        private int likes = 0;
        public MessageEventSl() : base()
        {
            //Создание события
            Name = $"Сообщение от {EventsControl.rnd_User.nick}";
            Rareness = EventImportance.usual;
            participants.Add(EventsControl.rnd_User, "Автор");
            string event_descr = $"{Abbrev.date} пользователь {EventsControl.rnd_User.nick} написал обычное сообщение на форуме румине. ";
            event_descr = event_descr + $"За него автор получил {likes} симпатий. ";
            description = event_descr;
            EventAction();

        }
        //Действие события
        public override void EventAction()
        {
            EventsControl.rnd_User.last_activity = Date.current_date;
            EventsControl.rnd_User.messages++;
            Activity.Hour_messages++;
            EventsControl.rnd_User.likes += likes;
            base.EventAction();
        }
    }
    class CommentEventSl : SlightEvent
    {
        private int rating = 0;

        public CommentEventSl() : base()
        {
            Name = $"Комментарий {EventsControl.rnd_User.nick}";
            Rareness = EventImportance.usual;
            participants.Add(EventsControl.rnd_User, "Автор");
            description = $"{Abbrev.date} пользователь {EventsControl.rnd_User.nick} прокомментировал новость на сайте. ";
            rating = AdvRnd.random.Next(3);
            description = description + $"За него автор получил {rating} рейтинга. ";
            EventAction();

        }
        //Действие события
        public override void EventAction()
        {
            EventsControl.rnd_User.last_activity = Date.current_date;
            EventsControl.rnd_User.comments++;
            EventsControl.rnd_User.likes += rating;
            Activity.day_comments++;
            base.EventAction();
        }
    }
}
