using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class RsEventArgs : EventArgs
    {
        public string Name { get; }
        public string Description { get; }
        public RsEventArg Type { get; }
        public EventRel Rel { get; }

        public RsEventArgs(string name,string descr,RsEventArg type, EventRel rel)
        {
            Name = name;
            Description = descr;
            Type = type;
            Rel = rel;
        }
        public RsEventArgs()
        {
            Name = "Неопределенное событие";
            Description = "Описание отсутствует";
            Type = RsEventArg.none;
            Rel = EventRel.neutral;
        }
    }
    //Характеристика события 
    enum RsEventArg
    {
        message,comment,none
    }
    enum EventRel
    {
        positive,negative,neutral,
        negativeX,positiveX
    }
}
