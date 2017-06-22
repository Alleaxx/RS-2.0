using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RumineSimulator_2._0
{
    /// <summary>
    /// Логика взаимодействия для EventView.xaml
    /// </summary>
    public partial class EventView : Window
    {
        Event selected_event;
        public EventView(long event_id)
        {
            InitializeComponent();
            selected_event = Events_List.EventSearch(event_id);
            EventUpdate();
        }
        public void EventUpdate()
        {
            Text_EventName.Text = selected_event.Name;
            Text_EventName.ToolTip = selected_event.Tooltip;
            text_EventDescription.Text = selected_event.BigDescription;

            List_EventProperties.Items.Clear();
            List_Participants.Items.Clear();
            try
            {
                InterfaceView_Event info_event = selected_event.InterfaceInfo;
                foreach (Interface_String inter_string in info_event.event_properties)
                {
                    List_EventProperties.Items.Add(Interface_String_Return(inter_string));
                }
                foreach (Interface_String inter_string in info_event.connectedEntities_properties)
                {
                    List_Participants.Items.Add(Interface_String_Return(inter_string));
                }
                foreach (Interface_String inter_string in info_event.special_event_properties)
                {
                    List_EventProperties.Items.Add(Interface_String_Return(inter_string));
                }
            }
            catch
            {

            }
        }



        //Базовое представление интерфейса - строка
        private ListBoxItem Interface_String_Return(Interface_String interface_info)
        {
            ListBoxItem item = new ListBoxItem();
            StackPanel stackpanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock text_value = new TextBlock()
            {
                Margin = new Thickness(2, 2, 2, 1),
                TextAlignment = TextAlignment.Left,
                Text = interface_info.Text_value,
                FontSize = interface_info.Text_size,
            };
            TextBlock text_value_value = new TextBlock()
            {
                Margin = new Thickness(2, 3, 0, 0),
                TextAlignment = TextAlignment.Right,
                Text = interface_info.Value,
                FontSize = interface_info.Value_size,

            };
            //Расшифровка изображения
            if (interface_info.Image_path != "")
            {
                Image image = new Image()
                {
                    Width = 15,
                    Height = 15,
                    Source = new BitmapImage(new Uri(interface_info.Image_path))
                };
                stackpanel.Children.Add(image);
            }
            stackpanel.Children.Add(text_value);
            stackpanel.Children.Add(text_value_value);
            item.Content = stackpanel;
            item.IsHitTestVisible = interface_info.IsHited;
            item.Foreground = interface_info.foreground_brush_all;
            item.Background = interface_info.background_brush_all;
            item.ToolTip = interface_info.Tooltip;
            return item;
        }
    }
}
