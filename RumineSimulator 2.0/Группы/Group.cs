using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Group : IAdvertisable
    {
        public int id { get; private set; }
        public string Name { get; private set; }
        public GroupsType type { get; private set; }

        //Параметры группы
        public int Respect { get; private set; }
        public int Rareness { get; private set; }

        //Цвет группы
        System.Drawing.Color dra_color = new System.Drawing.Color();
        public string ColorHTML;
        public SolidColorBrush need_brush = new SolidColorBrush();
        public GroupCondition condition;

        //Возможности группы
        public bool Mod { get; private set; }
        public bool Journ { get; private set; }
        public bool Mod_bezdn { get; private set; }
        public bool aC { get; private set; }
        public bool admin { get; private set; }

        //GUI
        public IntView_Group InterfaceInfo
        {
            get
            {
                return new IntView_Group(this);
            }
        }

        //Инициализация группы
        public Group(string Name, int Respect, int Rareness, string ColorHTTML, GroupsType Type)
        {
            id = GroupsControl.groups.Count + 1;
            this.Name = Name;
            this.Rareness = Rareness;
            this.Respect = Respect;
            ColorHTML = ColorHTTML;
            type = Type;

            //Конвертация цвета из HTML в WPFовский цвет
            ColorTranslator.FromHtml(ColorHTTML);
            dra_color = ColorTranslator.FromHtml(ColorHTTML);
            System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
            need_brush.Color = System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
        }
        public void SetCondition(GroupCondition condition)
        {
            this.condition = condition;
        }
        public void SetPossibilieties(bool Journ, bool Mod_bezdn, bool Mod, bool AC, bool Admin)
        {
            this.Mod = Mod;
            this.Journ = Journ;
            this.Mod_bezdn = Mod_bezdn;

            aC = AC;
            admin = Admin;
        }



        #region IAdvertisable
        public List<string> Advertisments = new List<string>();
        private void AdvertismentInit()
        {
            switch (type)
            {
                case GroupsType.Poset:
                    AddAdvertisment("user_nick: Мне остальные группы и даром не нужны");
                    AddAdvertisment("user_nick: Посетитель для настоящих пацанов!");
                    AddAdvertisment("user_nick падать некуда, разве что в гости");
                    break;
                case GroupsType.JGP:
                    AddAdvertisment("user_nick: Я состою в лучшей группе сайта по мнению 354 пользователей");
                    AddAdvertisment("user_nick: ПХЛ - трушность, проверенная временем");
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
