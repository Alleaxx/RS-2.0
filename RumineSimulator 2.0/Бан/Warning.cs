using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Warning
    {
        public User author { get; private set; }

        public DateTime date = new DateTime();

        public User goal { get; private set; }

        public int amount { get; set; }

        public string reason { get; private set; }

        public Warning(User Author, int Amount, string Reason,User Goal)
        {
            author = Author;
            amount += Amount;
            reason = Reason;
            goal = Goal;
            date = Date.current_date;
        }
    }
}
