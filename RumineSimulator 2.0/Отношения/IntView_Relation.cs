using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_Relation : IntView
    {
        List<GuiString> relation_props = new List<GuiString>();

        public IntView_Relation(RelationList rel)
        {

        }

        public void Add_RelationProperty( GuiString info)
        {
            Add_Property(relation_props, info);
        }
    }
}
