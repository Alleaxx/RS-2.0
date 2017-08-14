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
    class GuiString: IGUI
    {
        public ImageSource ImageSource { get; private set; }
        //Цвета
        public SolidColorBrush background_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_all = new SolidColorBrush();
        public SolidColorBrush foreground_brush_value;
        public SolidColorBrush foreground_brush_text;
        public SolidColorBrush border_brush;
        //GUI
        protected ListBoxItem item;
        public ListBoxItem Item { get { CreateGui(); return item; } private set { item = value; } }

        protected StackPanel stackpanel = new StackPanel();
        public TextBlock text_name = new TextBlock(), text_value =  new TextBlock();
        protected System.Windows.Controls.Image Image = null;


        StringProfile profile { get; set; }

        public GuiString(string Text, string Value,bool IsHited = true,StringProfile prof = StringProfile.Usual)
        {
            Statistic.guiString_obj++;
            text_name.Text = Text;
            text_name.FontSize = 14;
            text_name.Margin = new Thickness(2, 2, 2, 2);
            text_name.VerticalAlignment = VerticalAlignment.Center;
            text_name.HorizontalAlignment = HorizontalAlignment.Center;


            text_value.Text = Value;
            text_value.FontSize = 13;
            text_value.Margin = new Thickness(2, 2, 2, 2);
            text_value.VerticalAlignment = VerticalAlignment.Center;
            text_value.HorizontalAlignment = HorizontalAlignment.Center;

            stackpanel.Orientation = Orientation.Horizontal;

            profile = prof;

            item = new ListBoxItem();
            Image = new System.Windows.Controls.Image();
            item.IsHitTestVisible = IsHited;
            Image.Source = null;
            SetProfiles();
            SetGUIName();
        }
        public GuiString(string Text, string Value) : this(Text, Value, false, StringProfile.Usual)
        {

        }
        public GuiString(string Text, string Value,int size_name,int size_value) : this(Text, Value, false, StringProfile.Usual)
        {
            SetSize(size_name, size_value);
        }

        public void SetGUIName()
        {

        }
        public void SetGUIName(GUITypes type,int id)
        {
            try
            {
                string Type = type.ToString();
                string name = $"{type.ToString()}_{id}";
                item.Name = name;
            }
            catch
            {

            }

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
        public void SetToolTip(string tooltip)
        {
            item.ToolTip = tooltip;
        }

        //Редактирование изображения
        public void SetImage(string Path, int height = 15, int weidth = 15)
        {
            if(Path != "")
            {
                if (Path.Contains("pack://application:,,,/Resources/"))
                {
                    SetImage(new BitmapImage(new Uri(Path)));
                }
                else
                {
                    Path = "pack://application:,,,/Resources/" + Path;
                    SetImage(new BitmapImage(new Uri(Path)));
                }
            }
            Image.Width = weidth;
            Image.Height = height;
        }
        public void SetImage(ImageSource image_sourse,int height = 15,int weidth = 15)
        {
            ImageSource = image_sourse;
            Image.Width = weidth;
            Image.Height = height;
            Image.Source = image_sourse;
        }


        //Добавление цвета через html-строки
        public virtual void SetColor(string background_all,string foreground_all,string f_val = "",string f_text = "",string border_Brush ="")
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
            if(border_Brush != "")
            {
                border_brush = new SolidColorBrush();
                ColorTranslator.FromHtml(border_Brush);
                System.Drawing.Color drab_color = new System.Drawing.Color();
                drab_color = ColorTranslator.FromHtml(border_Brush);
                System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                border_brush.Color = System.Windows.Media.Color.FromRgb(drab_color.R, drab_color.G, drab_color.B);
                item.BorderBrush = border_brush;
            }

        }
        public virtual void SetColor(SolidColorBrush back,SolidColorBrush fore)
        {
            item.Background = back;
            text_name.Foreground = fore;
        }

        //Размер текства свойства и его значения
        public void SetSize(int size_text,int size_value)
        {
            text_name.FontSize = size_text;
            text_value.FontSize = size_value;
        }
        
        //Заранее заданные профили
        public void SetProfiles()
        {
            switch (profile)
            {
                case StringProfile.Header:
                    SetColor("#FFB2B2B2", "","","", "#FF5D5D5D");
                    item.BorderBrush =  new SolidColorBrush(Colors.DarkGray);
                    text_name.FontSize = 15;
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
