using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class FCHActivity
    {
        ActivitiesEnum type = new ActivitiesEnum();
        string short_description;
        string full_description;


        int activity_chanse { get; set; }
        int message_chanse { get; set; }
        int likes_chanse { get; set; }
        int reputation_chanse { get; set; }
        int event_chanse { get; set; }

        public FCHActivity(ActivitiesEnum a_enum, string sh_descr, int act_chanse, int mess_chanse, int like_chanse,int rep_chanse)
        {
            type = a_enum;
            short_description = sh_descr;
            activity_chanse = act_chanse;
            message_chanse = mess_chanse;
            likes_chanse = like_chanse;
            reputation_chanse = rep_chanse;
        }

    }
}
