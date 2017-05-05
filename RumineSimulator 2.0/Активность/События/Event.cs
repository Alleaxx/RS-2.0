using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class Event
    {
        public string name { get; set; }
        public Reason reason { get; set; }
        public string reason_text { get; set; }
        public string result_text { get; set; }

        public List<User> creators = new List<User>();
        public List<User> participants = new List<User>();
        public List<string> text_parametres = new List<string>();
        public List<string> Text_parametres
        {
            get
            {
                return ParametersTextGenerate();
            }
        }


        public DateTime begin_date = new DateTime();
        public DateTime end_date = new DateTime();
        public int duration { get; set; }

        public EventsEnum type { get; private set; }
        public ImageSource icon { get; set; }

        public Random random = new Random();

        public int message_mod { get; set; }

        public Event(string Name,EventsEnum Type,Reason rreason)
        {
            name = Name;
            type = Type;
            begin_date = Date.current_date;
            message_mod = 0;
            reason = rreason;
            if(reason != null)
            {
                creators.Add(reason.initiator);
                participants.Add(reason.initiator);
            }

            duration = 0;
        }

        public virtual void MessageModSet()
        {

        }

        public virtual bool EventPassing()
        {
            duration++;
            if (AdvRandom.PersentChanseBool(duration / 2 - 5))
            {
                EventEnd();
                return true;
            }

            return false;
        }
        public virtual void EventEnd()
        {
            end_date = Date.current_date;
            ParametersTextGenerate();
            EventsStorage.passed_events.Add(this);
            if (type == EventsEnum.historic)
                EventsStorage.historic_events.Add(this);
        }
        public virtual string ReasonTextGenerate()
        {
            switch (reason.type)
            {
                case ReasonsEnum.message:
                    reason_text = $"Сообщение {reason.initiator}";
                    break;
                default:
                    reason_text = $"Спонтанно";
                    break;
            }

            return reason_text;
        }
        public virtual List<string> ParametersTextGenerate()
        {
            text_parametres.Clear();
            text_parametres.Add(name);
            text_parametres.Add($"Создатель: {creators[0].nick}");
            text_parametres.Add($"Причина: {reason_text}");
            text_parametres.Add($"Модификатор: {message_mod}");
            text_parametres.Add($"Участников: {participants.Count}");
            text_parametres.Add($"Начало: {begin_date.ToShortDateString() + " " + begin_date.ToShortTimeString()}");
            text_parametres.Add($"Конец: {end_date.ToShortDateString() + " " + end_date.ToShortTimeString()}");
            text_parametres.Add($"Длительность: {duration} минут");
            return text_parametres;
        }

    }
}
