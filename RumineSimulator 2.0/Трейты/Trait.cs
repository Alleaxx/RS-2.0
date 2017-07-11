using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Trait : IAdvertisable
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

        #region IAdvertisable
        public List<string> Advertisments = new List<string>();
        private void AdvertismentInit()
        {
            switch (type)
            {
                case TraitsType.ded:
                    AddAdvertisment("user_nick: от ньюфагов хорошего не жди");
                    AddAdvertisment("user_nick: в былые времена было лучше, дааа");
                    AddAdvertisment("user_nick привычно пердит");
                    AddAdvertisment("user_nick вновь прогоняет какого-то ньюфага. Ничего нового");
                    AddAdvertisment("user_nick: И когда этот сайт наконец умрет?");
                    AddAdvertisment("user_nick: Я был в 2012. А вы нет!");
                    AddAdvertisment("user_nick восставал против модеров, все как обычно");
                    AddAdvertisment("user_nick: от ньюфагов хорошего не жди");
                    AddAdvertisment("user_nick: меня угрожают забанить. На кого они вообще наехали, а?");
                    break;
                case TraitsType.memguy:
                    AddAdvertisment("user_nick заразился вирусом Андрежа");
                    AddAdvertisment("user_nick нюхает воду");
                    AddAdvertisment("user_nick состоит из мочи");
                    AddAdvertisment("user_nick: приезжайте погостить в Фарьеград!");
                    break;
            }
        }
        public void AddAdvertisment(string adv)
        {
            Advertisments.Add(adv);
        }
        #endregion

    }
}
