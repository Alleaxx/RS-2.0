using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class HistoricEventsControl
    {
        static DateTime date_Wiki = new DateTime(2013, 9, 28);
        static Dictionary<HistoricEventTypes,DateTime> dict = new Dictionary<HistoricEventTypes,DateTime>();

        static public HistoricEvent HistoricEventCheck()
        {
            for (int i = 0; i < dict.Count; i++)
            {
                if (dict.ElementAt(i).Value.ToShortDateString() == Date.current_date.ToShortDateString())
                {
                    switch (dict.ElementAt(i).Key)
                    {
                        case HistoricEventTypes.WikiCreation:
                            return new WikiCreation("Создание вики", EventsEnum.historic,new Reason(Date.current_date,Player.user,Player.user,ReasonsEnum.historic));
                        default:
                            return null;
                    }
                }
            }
            return null;
        }
        enum HistoricEventTypes
        {
            WikiCreation,
        }
    }
}
