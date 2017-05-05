using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class WikiCreation : HistoricEvent
    {
        public string wiki_name { get; private set; }
        List<string> possible_names = new List<string>() { };


        public WikiCreation(string Name, EventsEnum Type, Reason rreason)
            : base(Name,Type,rreason)
        {
            var sortedGr = from i in UserList.Users
                           orderby i.character.historic descending
                           select i;
            List<User> historics = sortedGr.ToList();
            creators.Clear();
            reason = new Reason(this, historics[0], historics[0], ReasonsEnum.historic);
            creators.Add(historics[0]);
            duration = 3600;
        }
    }
}
