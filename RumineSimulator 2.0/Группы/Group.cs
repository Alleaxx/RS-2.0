using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class Group
    {
        public int id { get; private set; }
        public string Name { get; private set; }

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
        public Group(string Name,int Respect,int rareness,string ColorHTTML, GroupCondition Condition, bool Journ = false, bool Mod_bezdn = false, bool Mod = false, bool AC = false,bool Admin = false)
        {
            id = GroupsList.Groups.Count + 1;
            this.Name = Name;
            this.Rareness = rareness;
            this.Respect = Respect;
            this.Mod = Mod;
            this.Journ = Journ;
            this.Mod_bezdn = Mod_bezdn;
            this.ColorHTML = ColorHTTML;
            aC = AC;
            admin = Admin;
            condition = Condition;

            //Конвертация цвета из HTML в WPFовский цвет
            ColorTranslator.FromHtml(ColorHTTML);
            dra_color = ColorTranslator.FromHtml(ColorHTTML);
            System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
            need_brush.Color = System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
        }

    }
}
