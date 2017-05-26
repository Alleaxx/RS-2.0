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
            Duration = 0;
            SmallEvents_List.SmallEvents.Add(this);
        }
        public override void EventAddEnd_InterfaceInfo()
        {
            base.EventAddEnd_InterfaceInfo();
            InterfaceInfo = new InterfaceView_Event(this);
        }
    }
}
