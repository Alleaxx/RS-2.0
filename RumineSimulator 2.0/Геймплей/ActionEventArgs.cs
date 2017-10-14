using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActionEventArgs : EventArgs
    {
        public Action action;
        public string explaining;
        public ActionEventArgs(Action act)
        {
            action = act;
        }
        public ActionEventArgs(string expl)
        {
            explaining = expl;
        }
        public ActionEventArgs()
        {
            explaining = "Без аргументов";
        }
    }
}
