using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Trait
    {
        public int id_num { get; private set; }
        public int chanse { get; private set; }
        public string name { get; private set; }
        public string Tooltip { get; private set; }
        public Dictionary<UserFeaturesEnum, int> conditions = new Dictionary<UserFeaturesEnum, int>();
        public List<TraitsType> blocked_types = new List<TraitsType>();
        public TraitsType type { get; private set;}
        public TraitGlobalType global_type { get; private set; }

        //Графическое представление
        public IntView_Trait InterfaceInfo
        {
            get
            {
                return new IntView_Trait(this);
            }
        }

        public SolidColorBrush background_brush = new SolidColorBrush();
        public SolidColorBrush foreground_brush = new SolidColorBrush();

        public Trait(string s_name,TraitsType Type, int chanse, TraitGlobalType Type_global = TraitGlobalType.personal)
        {
            id_num = TraitsList.allTraits.Count + 1;
            name = s_name;
            global_type = Type_global;
            type = Type;
            this.chanse = chanse;
            Tooltip = name;

            AddConditions(UserFeaturesEnum.nothing, 0);
            blocked_types.Add(type);
            SetColor("#FFFFFF", "#000000");
        }
        public void AddTooltip(string tooltip)
        {
            Tooltip = tooltip;
        }
        public void AddConditions(UserFeaturesEnum param, int min_value)
        {
            conditions.Add(param, min_value);
        }
        public void AddBlockedTrait(TraitsType bl_type)
        {
            blocked_types.Add(bl_type);
        }
        public void SetColor(string background,string foreground)
        {
            System.Drawing.Color background_color = ColorTranslator.FromHtml(background);
            System.Windows.Media.Color.FromRgb(background_color.R, background_color.G, background_color.B);
            background_brush.Color = System.Windows.Media.Color.FromRgb(background_color.R, background_color.G, background_color.B);

            System.Drawing.Color foreground_color = ColorTranslator.FromHtml(foreground);
            System.Windows.Media.Color.FromRgb(foreground_color.R, foreground_color.G, foreground_color.B);
            foreground_brush.Color = System.Windows.Media.Color.FromRgb(foreground_color.R, foreground_color.G, foreground_color.B);
        }
    }
}
