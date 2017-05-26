using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class Interface_String
    {
        //Параметры свойства
        public string Text_value { get; private set; }
        public int Text_size { get; private set; }
        //Значение свойства
        public string Value { get; private set; }
        public int Value_size { get; private set; }
        //Изображение
        public string Image_path { get; private set; }
        public ImageSource ImageSource { get; private set; }
        public string Tooltip { get; private set; }
        public bool IsHited { get; private set; }
        //Цвета
        public SolidColorBrush background_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_value;
        public SolidColorBrush foreground_brush_text;


        public bool header { get; private set; }

        public Interface_String(string Text,string Value,bool IsHited,bool Header = false)
        {
            Text_value = Text;
            Text_size = 15;
            this.Value = Value;
            Value_size = 13;
            AddColor("#FFFFFF", "#000000");
            header = Header;

            this.IsHited = IsHited;
            Image_path = "";
            ImageSource = null;
            SetProfiles();
        }
        //Подсказка и изображение
        public void AddImagePathToolTip(string Path,string Tooltip)
        {
            if(Path != "")
            {
                if (Path.Contains("pack://application:,,,/Resources/"))
                {
                    Image_path = Path;
                }
                else
                {
                    Image_path = "pack://application:,,,/Resources/" + Path;
                }
                ImageSource = new BitmapImage(new Uri(Image_path));
            }
            this.Tooltip = Tooltip;
        }
        public void AddImage(ImageSource image_sourse)
        {
            ImageSource = image_sourse;
        }


        //Добавление цвета через html-строки
        public void AddColor(string background_all,string foreground_all,string f_val = "",string f_text = "")
        {
            if(background_all != "")
            {
                ColorTranslator.FromHtml(background_all);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(background_all);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                background_brush_all.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
            }
            if(foreground_all != "")
            {
                ColorTranslator.FromHtml(foreground_all);
                System.Drawing.Color draf_color = new System.Drawing.Color();
                draf_color = ColorTranslator.FromHtml(foreground_all);
                System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
                foreground_brush_all.Color = System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
            }
            if(f_text != "")
            {
                foreground_brush_text = new SolidColorBrush();
                ColorTranslator.FromHtml(f_text);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(f_text);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                foreground_brush_text.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
            }
            if (f_val != "")
            {
                foreground_brush_value = new SolidColorBrush();
                ColorTranslator.FromHtml(f_val);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(f_val);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                foreground_brush_value.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
            }

        }
        //Размер текства свойства и его значения
        public void SetSize(int size_text,int size_value)
        {
            Text_size = size_text;
            Value_size = size_value;
        }

        public void SetProfiles()
        {
            if (header)
            {
                background_brush_all = new SolidColorBrush(Colors.LightGray);
                Text_size = 16;
            }
        }
    }
}
