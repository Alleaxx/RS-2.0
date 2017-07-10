using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ReactionReason
    {
        public ReasonTypes reason_type { get; private set; }
        public Event event_reason { get; private set; }

        public ReactionReason()
        {
            reason_type = ReasonTypes.nothning;
        }
        public ReactionReason(Event eventt)
        {
            reason_type = ReasonTypes.Event;
            event_reason = eventt;
        }
    }
    enum ReasonTypes
    {
        Event, nothning
    }
}
