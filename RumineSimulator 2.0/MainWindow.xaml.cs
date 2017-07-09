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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Threading;

namespace RumineSimulator_2._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Инициализация


        Random random = new Random();

        private DispatcherTimer timer_users = null;
        private DispatcherTimer timer_TimeGo = null;
        WindowWarn WindowWarnings;
        WindowReputation WindowReputation;
        EventView WindowEvent;

        private short speed = 1;
        private short update_time = 5;
        //Информация для таймера - сколько юзеров генерировать и сколько он уже создал
        int timer_generated_users;
        int timer_total_users;

        bool users_generated = false;

        public MainWindow()
        {
            InitializeComponent();
            //Создание базовых групп
            Nicks.AvasInit();
            text_log.AppendText("\nИнициализация ников успешна...");
            GroupsList.UserGroupsInitCreation();
            text_log.AppendText("\nГруппы созданы...");
            TraitsList.TraitsInit();
            text_log.AppendText("\nТрейты созданы...");
            ReputationReason.ReasonsInit();
            text_log.AppendText("\nПричины созданы...");
            AnounceType.AnounceInit();
            text_log.AppendText("\nОбъявления созданы...");
            FractionList.FractionsInit();
            text_log.AppendText("\nФракции созданы...");
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 12, 0, 0));
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();
            for (int i = 0; i < TraitsList.AllTraits.Count; i++)
            {
                list_TraitsInfo.Items.Add(TraitsList.AllTraits.ElementAt(i).Value.InterfaceInfo.classic_string.Item);
            }
            for (int i = 0; i < GroupsList.Groups.Count; i++)
            {
                list_GroupsInfo.Items.Add(GroupsList.Groups.ElementAt(i).Value.InterfaceInfo.classic_string.Item);
                list_TraitsInfo.Items.Add(TraitsList.AllTraits.ElementAt(i).Value.InterfaceInfo.classic_string.Item);
            }

            UserPropsEventsOn();

            list_FractionsInfo.SelectionChanged += PropertyViewListSelection;
            list_TraitsInfo.SelectionChanged += PropertyViewListSelection;
            list_GroupsInfo.SelectionChanged += PropertyViewListSelection;

        }




        #endregion

        //Дата в статусе
        private void StatusUpdate()
        {
            StatusTextData.Text = Date.ReturnCurrDate();
            if (Date.current_date.Hour == 21 && Date.current_date.Minute == 0)
                StatusTextData.Foreground = new SolidColorBrush(Colors.MidnightBlue);
            if (Date.current_date.Hour == 17 && Date.current_date.Minute == 0)
                StatusTextData.Foreground = new SolidColorBrush(Colors.Maroon);
            if (Date.current_date.Hour == 10 && Date.current_date.Minute == 0)
                StatusTextData.Foreground = new SolidColorBrush(Colors.Black);
            if (Date.current_date.Hour == 5 && Date.current_date.Minute == 0)
                StatusTextData.Foreground = new SolidColorBrush(Colors.DodgerBlue);

        }
        private void Button_leftToolHide_Click(object sender, RoutedEventArgs e)
        {
            if (toolbarTrayLeft.Visibility == Visibility.Hidden)
            {
                button_leftToolHide.Content = "<";
                toolbarTrayLeft.Visibility = Visibility.Visible;
                toolbarTrayLeft.Width = 45;
            }
            else
            {
                button_leftToolHide.Content = ">";
                toolbarTrayLeft.Visibility = Visibility.Hidden;
                toolbarTrayLeft.Width = 0;
            }
        }


        #region Ход времени






        //Начало отсчета
        private void Button_TimeGo_Click(object sender, RoutedEventArgs e)
        {
            timer_TimeGo = new DispatcherTimer();
            timer_TimeGo.Tick += new EventHandler(TimeGoTick);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 250);
            update_time = Convert.ToInt16(text_UpdateTimes.Text);

            timer_TimeGo.IsEnabled = true;
            button_TimeGo.IsEnabled = false;
            button_GenerateUsers.IsEnabled = false;
            StatusSpeed_x1.IsEnabled = true;
            StatusSpeed_x2.IsEnabled = true;
            StatusSpeed_x3.IsEnabled = true;
            StatusSpeed_x4.IsEnabled = true;
            statusRadButton_cont.IsEnabled = true;
            statusRadButton_pause.IsEnabled = true;
        }

        //Главный тик таймера
        private void TimeGoTick(object sender, EventArgs e)
        {
            Activity.Time_Pass();
            MinuteUpdate();
        }

        private void MinuteUpdate()
        {
            MinuteLogicUpdate();
            MinuteInterfaceUpdate();
            if ((bool)statusRadButton_pause.IsChecked)
                timer_TimeGo.IsEnabled = false;
        }
        private void MinuteLogicUpdate()
        {
            Date.TimeGo();
            UsersControl.CheckingAllUserForUpdates();
        }

        private void MinuteInterfaceUpdate()
        {
            //Изменение модификаторa активности
            StatusUpdate();
        }




        //Пауза и "продолжить"
        private void StatusRadButton_cont_Checked(object sender, RoutedEventArgs e)
        {
            timer_TimeGo.IsEnabled = true;
        }

        #region Скорость
        private void StatusSpeed_x1_Click(object sender, RoutedEventArgs e)
        {
            speed = 1;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 500);
            StatusTextSpeed.Text = "x1";
        }
        private void StatusSpeed_x2_Click(object sender, RoutedEventArgs e)
        {
            speed = 2;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 100);
            StatusTextSpeed.Text = "x2";
        }
        private void StatusSpeed_x3_Click(object sender, RoutedEventArgs e)
        {
            speed = 3;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 25);
            StatusTextSpeed.Text = "x3";
        }
        private void StatusSpeed_x4_Click(object sender, RoutedEventArgs e)
        {
            speed = 4;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(text_MaxSpeed.Text));
            StatusTextSpeed.Text = "x4";
        }
        #endregion





        #endregion

        #region Таймер и генерация пользователей

        //Кнопка генерации пользователей
        private void Button_UserGenerate_Click(object sender, RoutedEventArgs e)
        {
            text_log.AppendText("\nГенерируются юзеры...");
            int GenerateUsersAmount;
            try
            {
                GenerateUsersAmount = int.Parse(text_GeneratedUsers.Text);
            }
            catch
            {
                GenerateUsersAmount = 50;
                text_GeneratedUsers.Text = 60.ToString();
                text_log.AppendText("\nКол-во генерируемых пользователей установлено на 50");
            }
            timer_total_users = GenerateUsersAmount;
            timer_generated_users = 0;
            timer_users = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer_users.Tick += new EventHandler(TimerTick);
            timer_users.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(text_GenerateTick.Text));
            text_GeneratedUsers.Text = UsersControl.UserAmount.ToString();
            if (users_generated)
            {
                Nicks.NicksInit();
                UsersControl.UsersList.Clear();
            }
            timer_users.Start();
        }
        //Окончание генерации пользователей, инициализация событий и отношений
        private void TimerTick(object sender, EventArgs e)
        {
            if (timer_total_users == timer_generated_users)
            {
                timer_users.Stop();
                text_log.AppendText("\n" + timer_total_users + " юзеров сгенерировано...");
                UsersControl.GenerateRelations();
                text_log.AppendText("\nОтношения сгенерированы...");
                UsersControl.ModerChoose();
                users_generated = true;
                UsersControl.FractionChoose();
                Activity.Activity_Init();
                text_log.AppendText("\nАктивность инициализирована...");
                HistoricEvents_List.HistoricEvents_Creation(1);
                text_log.AppendText("\nИсторические события заданы...");
            }

            else
            {
                UsersControl.GenerateUsers(1);
                text_GeneratedUsers.Text = UsersControl.UserAmount.ToString();
                timer_generated_users++;
            }
        }

        #endregion

        //F5, обновление всех списков
        private void status_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            //Обновить события
            list_passedEvents.Items.Clear();
            foreach (Event eve in Events_List.AllEvents)
            {
                list_passedEvents.Items.Add(eve.InterfaceInfo.classic_string.Item);
            }
            list_EventsProperties.Items.Clear();
            List<GuiString> IntInfo = Events_List.GetInterfaceInfo();
            foreach (GuiString str in IntInfo)
            {
                list_EventsProperties.Items.Add(str.Item);
            }
            //Обновление пользователей
            if (list_UsersAlpha.Items.Count < 2)
            {
                for (int i = 0; i < UsersControl.UsersList.Count; i++)
                {
                    list_UsersAlpha.Items.Add(UsersControl.UsersList[i].InterfaceInfo.classic_string.Item);
                }
            }
            //Обновление фракций
            list_FractionsInfo.Items.Clear();
            foreach (Fraction fraction in FractionList.AllFractions)
            {
                list_FractionsInfo.Items.Add(fraction.Interface_Info.classic_string.Item);
            }
            //Обновление статистики
            list_Statistics.Items.Clear();
            foreach (GuiString str in Statistic.InterfaceInfo.statistics)
            {
                list_Statistics.Items.Add(str.Item);
            }


        }

        //Изменение события и его обновление, показ окна
        private void List_passedEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list_EventChar.Items.Clear();
            list_EventParticipants.Items.Clear();
            try
            {
                IntView_Event info_event = Events_List.AllEvents[list_passedEvents.SelectedIndex].InterfaceInfo;
                foreach (GuiString inter_string in info_event.basicEvent_props)
                {
                    list_EventChar.Items.Add(inter_string.Item);
                }
                foreach (GuiString inter_string in info_event.specialEvent_props)
                {
                    list_EventChar.Items.Add(inter_string.Item);
                }
                foreach (GuiString inter_string in info_event.connectedEntities_props)
                {
                    list_EventParticipants.Items.Add(inter_string.Item);
                }
            }
            catch
            {

            }
        }
        private void button_EventView_Click(object sender, RoutedEventArgs e)
        {
            WindowEvent = new EventView(Events_List.AllEvents[list_passedEvents.SelectedIndex].id);
            WindowEvent.Show();
        }

        //Изменение пользователя в списке и его обновление, показ окна репутации
        private void list_UsersAlpha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoUserUpdate_Alpha();
        }
        private void InfoUserUpdate_Alpha()
        {
            UserPropsEventsOff();
            try
            {
                ListBoxItem item = (ListBoxItem)list_UsersAlpha.SelectedItem;
                Presenter.SelectionCheck(item.Name);
                User sel_user = Presenter.selected_user;

                //Аватарка
                Image_Ava.Source = sel_user.InterfaceInfo.classic_string.ImageSource;
                //Основные св-ва
                list_UserPropertiesBasic.Items.Clear();
                foreach (GuiString info in sel_user.InterfaceInfo.basic_props)
                {
                    list_UserPropertiesBasic.Items.Add(info.Item);
                }
                //Трейты
                list_TraitsNew.Items.Clear();
                foreach (GuiString info in sel_user.InterfaceInfo.traits)
                {
                    list_TraitsNew.Items.Add(info.Item);
                }
                //Описание
                text_Description.Text = sel_user.description;
                //Числовые свойства
                list_UserPropertiesNumeric.Items.Clear();
                foreach (GuiString info in sel_user.InterfaceInfo.numeric_props)
                {
                    list_UserPropertiesNumeric.Items.Add(info.Item);
                }
                //Репутация
                button_ReputationAlpha.Content = $"({sel_user.reputation.Base_reputation.ToString()})";
                button_ReputationMinusAlpha.Content = $"(-{sel_user.reputation.Otr_reputation.ToString()})";
                button_ReputationPlusAlpha.Content = $"(+{sel_user.reputation.Pos_reputation.ToString()})";

                //Прогресс-бары
                list_Character.Items.Clear();
                foreach (GuiString info in sel_user.InterfaceInfo.character_props)
                {
                    if (info.GetType() == sel_user.InterfaceInfo.classic_string.GetType())
                        list_Character.Items.Add(info.Item);
                    else
                    {
                        list_Character.Items.Add(info.Item);
                    }
                }
                list_Skills.Items.Clear();
                foreach (GuiString info in sel_user.InterfaceInfo.skills_props)
                {
                    if (info.GetType() == sel_user.InterfaceInfo.classic_string.GetType())
                        list_Skills.Items.Add(info.Item);
                    else
                    {
                        list_Skills.Items.Add(info.Item);
                    }

                }
            }
            catch
            {

            }
            UserPropsEventsOn();

        }
        private void button_ReputationBeta_Click(object sender, RoutedEventArgs e)
        {
            WindowReputation = new WindowReputation(Presenter.selected_user.nick);
            WindowReputation.Show();
        }

        //Изменение фракции в списке и ее обновление
        private void list_FractionsInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoFractionsUpdate_Alpha();
        }
        private void InfoFractionsUpdate_Alpha()
        {
            list_ViewListDetails.Items.Clear();
            try
            {
                ListBoxItem item = (ListBoxItem)list_FractionsInfo.SelectedItem;
                Presenter.SelectionCheck(item.Name);
                Fraction sel_fraction = Presenter.selected_fraction;
                foreach (GuiString str in sel_fraction.Interface_Info.fraction_properties)
                {
                    list_ViewListDetails.Items.Add(str.Item);
                }

            }
            catch
            {

            }
        }




        //Выключение событий показа дополнительной информации в пользователях
        private void UserPropsEventsOff()
        {
            list_UserPropertiesNumeric.SelectionChanged -= PropertyUsersSelection;
            list_TraitsNew.SelectionChanged -= PropertyUsersSelection;
            list_UserPropertiesBasic.SelectionChanged -= PropertyUsersSelection;
        }
        //Включение событий показа дополнительной информации в пользователях
        private void UserPropsEventsOn()
        {
            list_UserPropertiesNumeric.SelectionChanged += PropertyUsersSelection;
            list_TraitsNew.SelectionChanged += PropertyUsersSelection;
            list_UserPropertiesBasic.SelectionChanged += PropertyUsersSelection;
        }

        //Событие возникающее при выборе свойства пользователя, позволяющее отображать дополнительную информацию
        public void PropertyUsersSelection(object sender, RoutedEventArgs e)
        {
            list_UsersAlpha_SelectedProp.Items.Clear();
            ListBox lb = (ListBox)sender;
            ListBoxItem item = (ListBoxItem)lb.SelectedItem;
            if (Presenter.InterfaceInfoReturn(item.Name) != null)
            {
                IntView info = Presenter.InterfaceInfoReturn(item.Name);
                list_UsersAlpha_SelectedProp.Items.Add(new GuiString("Подробнее о выбранном свойстве", "", false, StringProfile.Header).Item);
                foreach (GuiString str in info.all_properties)
                {
                    list_UsersAlpha_SelectedProp.Items.Add(str.Item);
                }
            }
        }
        //Событие возникающее при выборе списка сущностей, позволяющее отображать дополнительную информацию
        public void PropertyViewListSelection(object sender, RoutedEventArgs e)
        {
            list_ViewListDetails.Items.Clear();
            ListBox lb = (ListBox)sender;
            ListBoxItem item = (ListBoxItem)lb.SelectedItem;
            if (Presenter.InterfaceInfoReturn(item.Name) != null)
            {
                IntView info = Presenter.InterfaceInfoReturn(item.Name);
                foreach (GuiString str in info.all_properties)
                {
                    list_ViewListDetails.Items.Add(str.Item);
                }
            }
        }


    }
}
