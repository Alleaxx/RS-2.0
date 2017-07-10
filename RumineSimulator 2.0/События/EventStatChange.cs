using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class EventStatChange : Event
    {
        public EventStatChange(string name,EventType type, int days_delete = 2) : base(name,type)
        {
            daysToDelete = days_delete;
        }
    }
}
