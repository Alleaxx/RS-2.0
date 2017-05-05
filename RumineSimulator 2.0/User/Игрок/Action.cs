using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Action
    {
        public string short_name { get; set;}
        public ActionsEnum id { get; set; }
        public int time_min { get; set;}
        public int time_remain { get; set; }
        public DateTime end_date = new DateTime();
        public bool started = false;
        public string result { get; set; }


        public Action(string name, ActionsEnum Id,int Time_sec)
        {
            short_name = name;
            id = Id;
            time_min = Time_sec;
            time_remain = time_min;
        }
        public void ActionStart()
        {
            started = true;
            time_remain = time_min + (int)(time_min * ((double)Player.Boredom/100));
            end_date = Date.current_date.AddMinutes(time_min);
        }
        public void ActionMinutePass()
        {
            time_remain--;
            end_date = Date.current_date.AddMinutes(time_remain);
        }
        public virtual bool DoAction()
        {
            return true;
        }
    }
}
