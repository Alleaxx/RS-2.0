﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class SmallEvent : Event
    {
        public SmallEvent(string Name,EventType Type) : base(Name,Type)
        {
            daysToDelete = 7;
            Duration = 0;
            EventGlobalType = EventType.small;
            EventsControl.AllEvents.Add(this);
            EventsControl.SmallEvents.Add(this);
        }
    }
}
