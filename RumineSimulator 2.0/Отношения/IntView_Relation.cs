using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Relation : IntView
    {
        public List<GuiString> relation_infl = new List<GuiString>();

        public IntView_Relation(Relation rel)
        {
            //Представление в виде строки
            classic_string = new GuiString("Отношение: ", $"{EnumToString.RelationToString(rel.relation)}", false);
            //Параметры повлиявшие на дружбу
            Add_RelationInfluence(classic_string);
            Add_RelationInfluence(new GuiString("Значение дружбы: ", $"{rel.friendness}", false));
            Add_RelationInfluence(new GuiString("Влияние на отношения", $"", false, StringProfile.Header));
            for (int i = 0; i < rel.relation_log.Count; i++)
            {
                Add_RelationInfluence(new GuiString($"{rel.relation_log[i]}", $"", 12,12));
            }
        }

        public void Add_RelationInfluence(GuiString info)
        {
            Add_Property(relation_infl, info);
        }

    }
}
