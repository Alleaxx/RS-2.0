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
            selected_event = EventsControl.EventSearch(event_id);
            EventUpdate();
        }
        public void EventUpdate()
        {
            Text_EventName.Text = selected_event.Name;
            if (selected_event is EventStatChange)
            {
                text_EventDescription.Text = selected_event.sel_description;

            }
            List_EventProperties.Items.Clear();
            List_Participants.Items.Clear();
            foreach (GuiString inter_string in selected_event.InterfaceInfo.basicEvent_props)
            {
                try
                {
                    List_EventProperties.Items.Add(inter_string.Item);
                }
                catch
                {
                    List_EventProperties.Items.Add("Здесь что-то потерялось");
                }
            }
            foreach (GuiString inter_string in selected_event.InterfaceInfo.connectedEntities_props)
            {
                List_Participants.Items.Add(inter_string.Item);
            }

        }



        //Базовое представление интерфейса - строка
    }
}
