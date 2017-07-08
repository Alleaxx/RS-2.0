using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;

namespace RumineSimulator_2._0
{
    class Interface_String: IGUI
    {
        public ImageSource ImageSource { get; private set; }
        //Цвета
        public SolidColorBrush background_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_value;
        public SolidColorBrush foreground_brush_text;
        //GUI
        protected ListBoxItem item;
        public ListBoxItem Item { get { CreateGui(); return item; } private set { item = value; } }

        protected StackPanel stackpanel = new StackPanel();
        protected TextBlock text_name = new TextBlock(), text_value =  new TextBlock();
        protected System.Windows.Controls.Image Image = null;


        StringProfile profile { get; set; }

        public Interface_String(string Text, string Value,bool IsHited = true,StringProfile prof = StringProfile.Usual)
        {
            text_name.Text = Text;
            text_name.FontSize = 15;
            text_name.Margin = new Thickness(2, 2, 2, 1);
            text_name.TextAlignment = TextAlignment.Left;


            text_value.Text = Value;
            text_value.FontSize = 13;
            text_value.Margin = new Thickness(2, 3, 0, 0);
            text_value.TextAlignment = TextAlignment.Right;

            stackpanel.Orientation = Orientation.Horizontal;

            profile = prof;

            item = new ListBoxItem();
            Image = new System.Windows.Controls.Image();
            item.IsHitTestVisible = IsHited;
            Image.Source = null;
            SetProfiles();
        }
        public Interface_String(string Text, string Value) : this(Text, Value, false, StringProfile.Usual)
        {

        }

        public virtual void CreateGui()
        {
            stackpanel.Children.Clear();
            stackpanel.Children.Add(Image);
            stackpanel.Children.Add(text_name);
            stackpanel.Children.Add(text_value);
            item.Content = stackpanel;
        }

        //Подсказка
        public void AddToolTip(string tooltip)
        {
            item.ToolTip = tooltip;
        }

        //Редактирование изображения
        public void AddImage(string Path)
        {
            if(Path != "")
            {
                if (Path.Contains("pack://application:,,,/Resources/"))
                {
                    AddImage(new BitmapImage(new Uri(Path)));
                }
                else
                {
                    Path = "pack://application:,,,/Resources/" + Path;
                    AddImage(new BitmapImage(new Uri(Path)));
                }

            }
        }
        public void AddImage(ImageSource image_sourse,int height = 15,int weidth = 15)
        {
            Image.Width = weidth;
            Image.Height = height;
            Image.Source = image_sourse;
        }


        //Добавление цвета через html-строки
        public virtual void AddColor(string background_all,string foreground_all,string f_val = "",string f_text = "")
        {
            if(background_all != "")
            {
                ColorTranslator.FromHtml(background_all);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(background_all);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                background_brush_all.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                item.Background = background_brush_all;
            }
            if(foreground_all != "")
            {
                ColorTranslator.FromHtml(foreground_all);
                System.Drawing.Color draf_color = new System.Drawing.Color();
                draf_color = ColorTranslator.FromHtml(foreground_all);
                System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
                foreground_brush_all.Color = System.Windows.Media.Color.FromRgb(draf_color.R, draf_color.G, draf_color.B);
                item.Foreground = foreground_brush_all;
            }
            if(f_text != "")
            {
                foreground_brush_text = new SolidColorBrush();
                ColorTranslator.FromHtml(f_text);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(f_text);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                foreground_brush_text.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                text_name.Foreground = foreground_brush_text;
            }
            if (f_val != "")
            {
                foreground_brush_value = new SolidColorBrush();
                ColorTranslator.FromHtml(f_val);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(f_val);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                foreground_brush_value.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                text_value.Foreground = foreground_brush_value;
            }

        }
        //Размер текства свойства и его значения
        public void SetSize(int size_text,int size_value)
        {
            text_name.FontSize = size_text;
            text_value.FontSize = size_value;
        }

        public void SetProfiles()
        {
            switch (profile)
            {
                case StringProfile.Header:
                    item.Background =  new SolidColorBrush(Colors.LightGray);
                    text_name.FontSize = 16;
                    break;
                case StringProfile.Quote:
                    text_value.TextAlignment = TextAlignment.Right;
                    text_name.TextAlignment = TextAlignment.Right;
                    break;
                default:
                    break;
            }

        }
    }
    enum StringProfile
    {
        Header,Usual,Quote
    }
}
