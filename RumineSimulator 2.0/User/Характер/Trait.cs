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
        public int chanse { get; private set; }
        public string short_name { get; private set; }
        public string full_description { get; private set; }
        public Dictionary<UserParamsEnum, int> conditions = new Dictionary<UserParamsEnum, int>();
        public List<Trait> blocked_traits = new List<Trait>();
        public Traits id { get; set;}
        public TraitType type { get; private set; }
        public bool known { get; set; }

        System.Drawing.Color dra_color = new System.Drawing.Color();
        public SolidColorBrush background_brush = new SolidColorBrush();
        System.Drawing.Color draf_color = new System.Drawing.Color();
        public SolidColorBrush foreground_brush = new SolidColorBrush();

        public Trait(Traits Id,int Chanse, string s_name, string f_text,Dictionary<UserParamsEnum, int> Conditions,string ColorHTTMLBack = "#FFFFFF", string ColorHTTMLFore = "#000000", TraitType Type = TraitType.notype)
        {
            type = Type;
            id = Id;
            chanse = Chanse;
            short_name = s_name;
            full_description = f_text;
            conditions = Conditions;
            known = false;

            ColorTranslator.FromHtml(ColorHTTMLBack);
            dra_color = ColorTranslator.FromHtml(ColorHTTMLBack);
            System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
            background_brush.Color = System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
            draf_color = ColorTranslator.FromHtml(ColorHTTMLFore);
            System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
            foreground_brush.Color = System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
        }
    }
}
