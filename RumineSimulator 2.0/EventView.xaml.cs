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
                foreach (Interface_String inter_string in info_event.basic_event_properties)
                {
                    List_EventProperties.Items.Add(inter_string.Item);
                }
                foreach (Interface_String inter_string in info_event.connectedEntities_properties)
                {
                    List_Participants.Items.Add(inter_string.Item);
                }
                foreach (Interface_String inter_string in info_event.special_event_properties)
                {
                    List_EventProperties.Items.Add(inter_string.Item);
                }
            }
            catch
            {

            }
        }



        //Базовое представление интерфейса - строка
    }
}
