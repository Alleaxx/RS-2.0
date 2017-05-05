using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class HistoricEvent : Event
    {
        public bool original_author;
        public string description { get; private set; }

        DateTime early_gran;
        DateTime late_gran;

        public HistoricEvent(string Name, EventsEnum Type, Reason rreason) : base(Name, Type, rreason)
        {
            description = "Заготовка для исторического события. Вы не должны увидеть эту надпись ни при каком случае";
        }
        public override List<string> ParametersTextGenerate()
        {            
            base.ParametersTextGenerate();
            Text_parametres.Add("Описание: " + description);
            return text_parametres;
        }
        public override bool EventPassing()
        {
            duration--;
            if(duration == 0)
            {
                EventEnd();
                return true;
            }
            return false;
        }

    }
}
