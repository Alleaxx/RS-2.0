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
        public SolidColorBrush background_brush = new SolidColorBrush();
        public SolidColorBrush foreground_brush = new SolidColorBrush();

        public Interface_String(string Text,string Value,bool IsHited)
        {
            Text_value = Text;
            Text_size = 15;
            this.Value = Value;
            Value_size = 13;
            AddColor("#FFFFFF", "#000000");

            this.IsHited = IsHited;
            Image_path = "";
            ImageSource = null;
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
            }
            this.Tooltip = Tooltip;
            ImageSource = new BitmapImage(new Uri(Image_path));
        }
        public void AddImage(ImageSource image_sourse)
        {
            ImageSource = image_sourse;
        }


        //Добавление цвета через html-строки
        public void AddColor(string background,string foreground)
        {
            if(background != "")
            {
                ColorTranslator.FromHtml(background);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(background);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                background_brush.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
            }
            if(foreground != "")
            {
                ColorTranslator.FromHtml(foreground);
                System.Drawing.Color draf_color = new System.Drawing.Color();
                draf_color = ColorTranslator.FromHtml(foreground);
                System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
                foreground_brush.Color = System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
            }

        }
        //Размер текства свойства и его значения
        public void SetSize(int size_text,int size_value)
        {
            Text_size = size_text;
            Value_size = size_value;
        }
    }
}
