using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_RelationControl : IntView
    {
        public List<GuiString> relation_props = new List<GuiString>();

        public IntView_RelationControl(RelationControl rel)
        {
            classic_string = new GuiString("Связи", $"{rel.owner.nick}", false, StringProfile.Header);
            classic_string.SetSize(15, 15);
            Add_RelationProperty(classic_string);
            Add_RelationProperty(new GuiString("Друзей: ", $"{rel.RelationCountUsersReturn(RelationType.friend).Count}", false));
            relation_props.Last().SetColor("#FF84FF8A", "");
            relation_props.Last().SetSize(14, 14);
            Add_RelationProperty(new GuiString("Товарищей: ", $"{rel.RelationCountUsersReturn(RelationType.comrade).Count}", false));
            relation_props.Last().SetColor("#FFB7FFAC", "");
            relation_props.Last().SetSize(14, 14);
            Add_RelationProperty(new GuiString("Неприятелей: ", $"{rel.RelationCountUsersReturn(RelationType.unfriend).Count}", false));
            relation_props.Last().SetColor("#FFFFB9B9", "");
            relation_props.Last().SetSize(14, 14);
            Add_RelationProperty(new GuiString("Врагов: ", $"{rel.RelationCountUsersReturn(RelationType.enemy).Count}", false));
            relation_props.Last().SetColor("#FFFF7F7F", "");
            relation_props.Last().SetSize(14, 14);
            Add_RelationProperty(new GuiString($"Остальные {rel.RelationCountUsersReturn(RelationType.neutral).Count} юзеров нейтральны", $"", false));
            relation_props.Last().SetColor("#FFEEEEEE", "");
            relation_props.Last().SetSize(14, 14);

        }

        public void Add_RelationProperty(GuiString info)
        {
            Add_Property(relation_props, info);
        }
    }
}
