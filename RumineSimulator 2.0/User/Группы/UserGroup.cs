﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;

namespace RumineSimulator_2._0
{
    class UserGroup
    {

        public string name { get; private set; }

        //Параметры группы
        public int respect { get; private set; }
        public int rareness { get; private set; }

        //Цвет группы
        System.Drawing.Color dra_color = new System.Drawing.Color();
        public SolidColorBrush need_brush = new SolidColorBrush();

        //Возможности группы
        public bool mod { get; private set; }
        public bool journ { get; private set; }
        bool mod_bezdn;
        bool aC;
        bool admin;

        //Инициализация группы
        public UserGroup(string Name,int Respect,int rareness,string ColorHTTML,bool Journ = false, bool Mod_bezdn = false, bool Mod = false, bool AC = false,bool Admin = false)
        {
            name = Name;
            this.rareness = rareness;
            respect = Respect;
            mod = Mod;
            journ = Journ;
            mod_bezdn = Mod_bezdn;
            aC = AC;
            admin = Admin;

            //Конвертация цвета из HTML в WPFовский цвет
            ColorTranslator.FromHtml(ColorHTTML);
            dra_color = ColorTranslator.FromHtml(ColorHTTML);
            System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
            need_brush.Color = System.Windows.Media.Color.FromRgb(dra_color.R, dra_color.G, dra_color.B);
        }

    }
}
