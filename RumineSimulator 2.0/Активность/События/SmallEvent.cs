using System;
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
            Days_Delete = 7;
            Duration = 0;
            EventGlobalType = EventType.small;
            Events_List.SmallEvents.Add(this);
        }
        public override void EventAddEnd_InterfaceInfo()
        {
            base.EventAddEnd_InterfaceInfo();
            InterfaceInfo = new InterfaceView_Event(this);
        }
    }
}
