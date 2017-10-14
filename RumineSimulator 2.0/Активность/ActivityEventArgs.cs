using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ActivityEventArgs : EventArgs
    {
        public string reason;
        public ActivityEventArgs()
        {

        }
        public ActivityEventArgs(string sender)
        {
            reason = sender;
        }
    }
}
