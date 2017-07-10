using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class HistoricEvent : Event
    {
        public DateTime pre_Date { get; set; }
        public Dictionary<string, string> more_properties = new Dictionary<string, string>();

        public HistoricEvent(string name, EventType type, DateTime pre_Date) : base(name,type)
        {
            this.pre_Date = pre_Date;
            date = pre_Date;
            EventGlobalType = EventType.historic;
            daysToDelete = 2000;
        }

        public bool CheckEvent()
        {
            if (Date.current_date < pre_Date)
                return false;
            else
                return true;
        }
    }
}
