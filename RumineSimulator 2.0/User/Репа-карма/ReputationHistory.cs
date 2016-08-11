using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class ReputationHistory
    {
        public User author { get; private set; }
        public DateTime date { get; private set; }
        public float set_karma { get; private set;}
        public string reason { get; private set; }
        public bool otr { get; private set; }

        public ReputationHistory(User Author,float Value,string Reason,bool Otr)
        {
            author = Author;
            date = new DateTime();
            date = Date.current_date;
            set_karma = Value;
            reason = Reason;
            otr = Otr;
        }


    }
}
