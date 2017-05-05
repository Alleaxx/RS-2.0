using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class EventAnounce : Event
    {
        public string anounce { get; set; }
        public Traits anounceType { get; set; }
        public bool sucees { get; set; }
        public EventAnounce(string Name, EventsEnum Type, Reason rreason): base(Name,Type,rreason)
        {
            creators.Add(reason.initiator);
            foreach (User user in Activity.online)
            {
                participants.Add(user);
            }
            MessageModSet();
            ReasonTextGenerate();
            AnounceGenerate();
            icon = new BitmapImage(new Uri("pack://application:,,,/Resources/attention.png"));

        }
        public override bool EventPassing()
        {
            MessageModSet();
            return base.EventPassing();
        }
        public override void EventEnd()
        {
            result_text = "";
            base.EventEnd();
        }
        public override void MessageModSet()
        {
            message_mod = participants.Count * 2;
        }
        public virtual void AnounceGenerate()
        {
            anounceType = reason.initiator.traits[random.Next(0, reason.initiator.traits.Count)].id;            
            anounce = AnounceType.ReturnAnounce(anounceType);
        }
        public override List<string> ParametersTextGenerate()
        {
            base.ParametersTextGenerate();
            text_parametres.Add($"Объявление:  {anounce}");
            return text_parametres;
        }

    }
}
