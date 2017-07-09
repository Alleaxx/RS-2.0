using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Event_Creator
    {
        public CreatorType Type { get; private set; }
        public string Text { get; private set; }

        public Event_Creator(CreatorType Type,string Text)
        {
            this.Type = Type;
            this.Text = Text;
        }
    }
    enum CreatorType
    {
        User,Event,Rumine
    }
}
