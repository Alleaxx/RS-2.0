using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace RumineSimulator_2._0
{
    class GuiProgressBar : GuiString
    {
        protected StackPanel stackPanel = new StackPanel();
        protected ProgressBar progressBar = new ProgressBar();
        public GuiProgressBar(string Text,int value,int min_value,int max_value,StringProfile prof = StringProfile.Usual): base(Text, value.ToString(), true, prof)
        {
            stackPanel.Orientation = Orientation.Vertical;


            progressBar.Minimum = min_value;
            progressBar.Maximum = max_value;
            progressBar.Value = value;
            progressBar.HorizontalAlignment = HorizontalAlignment.Stretch;
            progressBar.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            progressBar.Width = 125;
            progressBar.Height = 5;
            progressBar.Margin = new Thickness(5, 0, 5, 0);
        }
        public override void SetColor(string background_all, string foreground_all, string f_val = "", string f_text = "",string border_brush = "")
        {
            base.SetColor(background_all, foreground_all, f_val, f_text,border_brush);
            progressBar.Foreground = foreground_brush_value;
        }
        public override void CreateGui()
        {
            stackPanel.Children.Clear();
            stackPanel.Children.Add(Image);
            stackPanel.Children.Add(text_name);
            stackPanel.Children.Add(progressBar);
            item.Content = stackPanel;
        }
    }
}
