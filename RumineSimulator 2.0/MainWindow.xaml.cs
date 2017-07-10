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
        //Информация для таймера - сколько юзеров генерировать и сколько он уже создал
        int timer_generated_users;
        int timer_total_users;

        bool users_generated = false;

        public MainWindow()
        {
            InitializeComponent();
            //Инициализация базовых объектов
            Nicks.AvasInit();
            GroupsControl.UserGroupsInitCreation();
            TraitsList.TraitsInit();
            ReputationReason.ReasonsInit();
            AnounceType.AnounceInit();
            FractionList.FractionsInit();
            text_log.AppendText($"\nДаты установлены на: ");
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 12, 0, 0));
            text_log.AppendText($"\nДата создания румине: {Date.found_date.ToShortDateString()}");
            text_log.AppendText($"\nНынешняя дата: {Date.ReturnCurrDate()}");
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();
            for (int i = 0; i < TraitsList.allTraits.Count; i++)
            {
                list_TraitsInfo.Items.Add(TraitsList.allTraits[i].InterfaceInfo.classic_string.Item);
            }
            for (int i = 0; i < GroupsControl.groups.Count; i++)
            {
                list_GroupsInfo.Items.Add(GroupsControl.groups[i].InterfaceInfo.classic_string.Item);
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


        #region Время

        //Кнопка начала времени
        private void Button_TimeGo_Click(object sender, RoutedEventArgs e)
        {
            timer_TimeGo = new DispatcherTimer();
            timer_TimeGo.Tick += new EventHandler(TimeGoTick);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 250);

            timer_TimeGo.IsEnabled = true;
            button_TimeGo.IsEnabled = false;
            button_GenerateUsers.IsEnabled = false;
            StatusSpeed_x1.IsEnabled = true;
            StatusSpeed_x2.IsEnabled = true;
            StatusSpeed_x3.IsEnabled = true;
            StatusSpeed_x4.IsEnabled = true;
            statusRadButton_cont.IsEnabled = true;
            statusRadButton_pause.IsEnabled = true;

            button_TimeGo.Margin = new Thickness(0, 0, 0, 0);
            button_TimeGo.Height = 0;
            button_TimeGo.Width = 0;
            button_TimeGo.Visibility = Visibility.Hidden;
            button_TimeGo.IsEnabled = false;

        }

        //Главный тик таймера
        private void TimeGoTick(object sender, EventArgs e)
        {
            timer_TimeGo.IsEnabled = false;
            Activity.Time_Pass();
            MinuteUpdate();
        }

        private void MinuteUpdate()
        {
            MinuteLogicUpdate();
            MinuteInterfaceUpdate();
            if (!(bool)statusRadButton_pause.IsChecked)
                timer_TimeGo.IsEnabled = true;
        }
        private void MinuteLogicUpdate()
        {
            Date.TimeGo();
            UsersControl.CheckingAllUserForUpdates();
        }

        private void MinuteInterfaceUpdate()
        {
            StatusUpdate();

            ActivityProperiesUpdate();
        }

        //Пауза и "продолжить"
        private void StatusRadButton_cont_Checked(object sender, RoutedEventArgs e)
        {
            timer_TimeGo.IsEnabled = true;
        }

        #region Скорость
        //Минута в 500 миллисекунд
        private void StatusSpeed_x1_Click(object sender, RoutedEventArgs e)
        {
            speed = 1;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 500);
            StatusTextSpeed.Text = "x1";
        }
        //Минута в 100 миллисекунд
        private void StatusSpeed_x2_Click(object sender, RoutedEventArgs e)
        {
            speed = 2;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 100);
            StatusTextSpeed.Text = "x2";
        }
        //Минута в 25 миллисекунд
        private void StatusSpeed_x3_Click(object sender, RoutedEventArgs e)
        {
            speed = 3;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 25);
            StatusTextSpeed.Text = "x3";
        }
        //Минута в <25 миллисекунд
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
                text_GeneratedUsers.Text = 50.ToString();
                text_log.AppendText("\nКол-во генерируемых пользователей установлено на 50");
            }
            timer_total_users = GenerateUsersAmount;
            timer_generated_users = 0;
            timer_users = new DispatcherTimer();  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer_users.Tick += new EventHandler(TimerTick);
            timer_users.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(text_GenerateTick.Text));
            text_GeneratedUsers.Text = UsersControl.Users.Count.ToString();
            if (users_generated)
            {
                Nicks.NicksInit();
                UsersControl.Users.Clear();
            }
            //Исчезновение кнопки
            button_GenerateUsers.Margin = new Thickness(0, 0, 0, 0);
            button_GenerateUsers.IsEnabled = false;
            button_GenerateUsers.Visibility = Visibility.Hidden;
            button_GenerateUsers.Width = 0;
            button_GenerateUsers.Height = 0;


            timer_users.Start();
        }
        //Окончание генерации пользователей, инициализация событий и отношений
        private void TimerTick(object sender, EventArgs e)
        {
            if (timer_total_users == timer_generated_users)
            {
                timer_users.Stop();
                UsersControl.GenerateRelations();
                GroupsControl.ModerChoose();
                users_generated = true;
                UsersControl.FractionChoose();
                Activity.Activity_Init();
                HistoricEvents_List.HistoricEvents_Creation(1);

                text_log.AppendText("\nПользователи сгенерированы");
            }

            else
            {
                UsersControl.GenerateUsers(1);
                text_GeneratedUsers.Text = UsersControl.Users.Count.ToString();
                timer_generated_users++;
            }
        }

        #endregion

        //F5, обновление всех списков
        private void status_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            //Обновить события
            list_passedEvents.Items.Clear();
            foreach (Event eve in EventsControl.AllEvents)
            {
                list_passedEvents.Items.Add(eve.InterfaceInfo.classic_string.Item);
            }
            list_EventsProperties.Items.Clear();
            List<GuiString> IntInfo = EventsControl.GetInterfaceInfo();
            foreach (GuiString str in IntInfo)
            {
                list_EventsProperties.Items.Add(str.Item);
            }
            //Обновление пользователей
            if (list_UsersAlpha.Items.Count < 2)
            {
                for (int i = 0; i < UsersControl.Users.Count; i++)
                {
                    list_UsersAlpha.Items.Add(UsersControl.UserListReturnSort()[i].InterfaceInfo.classic_string.Item);
                }
            }
            //Обновление пользователей в отношениях, задание особого события при нажатии строки
            combo_RelationChoose.Items.Clear();
            foreach (User user in UsersControl.UserListReturnSort())
            {
                GuiString user_relation = new GuiString("", "");
                user_relation = user.InterfaceInfo.classic_string;
                user_relation.SetGUIName(GUITypes.relation, user.user_id);
                combo_RelationChoose.Items.Add(user_relation.Item);
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
                IntView_Event info_event = EventsControl.AllEvents[list_passedEvents.SelectedIndex].InterfaceInfo;
                foreach (GuiString inter_string in info_event.basicEvent_props)
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
            WindowEvent = new EventView(EventsControl.AllEvents[list_passedEvents.SelectedIndex].id);
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
                if(sel_user.daylog != null)
                {
                    text_UserChangeLog.Text = sel_user.daylog.text_descr;
                }
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
                list_UserPropertiesRelations.Items.Clear();
                foreach (GuiString info in sel_user.relations.InterfaceInfo.relation_props)
                {
                    list_UserPropertiesRelations.Items.Add(info.Item);
                }


            }
            catch
            {
                text_log.AppendText("При обновлении данных произошла ошибка");
            }
            UserPropsEventsOn();

        }
        private void button_ReputationBeta_Click(object sender, RoutedEventArgs e)
        {
            WindowReputation = new WindowReputation(Presenter.selected_user.nick);
            WindowReputation.Show();
        }
        //Кнопка показа предупреждений
        private void button_ViewWarns_Click(object sender, RoutedEventArgs e)
        {
            if (Presenter.selected_user != null)
            {
                WindowWarnings = new WindowWarn(Presenter.selected_user.nick);
                WindowWarnings.Show();
            }
        }
        //Изменение сортировки пользователей
        private void combo_UserSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (combo_UserSort.SelectedIndex)
            {
                case 0:
                    Presenter.SetUserSort(SortingUserTypes.no_sort);
                    break;
                case 1:
                    Presenter.SetUserSort(SortingUserTypes.registration);
                    break;
                case 2:
                    Presenter.SetUserSort(SortingUserTypes.reputation);
                    break;
                case 3:
                    Presenter.SetUserSort(SortingUserTypes.influence);
                    break;
                case 4:
                    Presenter.SetUserSort(SortingUserTypes.moderChanse);
                    break;
                case 5:
                    Presenter.SetUserSort(SortingUserTypes.groupRareness);
                    break;
                case 6:
                    Presenter.SetUserSort(SortingUserTypes.adeq);
                    break;
                case 7:
                    Presenter.SetUserSort(SortingUserTypes.rakness);
                    break;
                case 8:
                    Presenter.SetUserSort(SortingUserTypes.messages);
                    break;

                default:
                    Presenter.SetUserSort(SortingUserTypes.no_sort);
                    break;
            }
            //Обновление списка пользователей
            list_UsersAlpha.Items.Clear();
            for (int i = 0; i < UsersControl.Users.Count; i++)
            {
                list_UsersAlpha.Items.Add(UsersControl.UserListReturnSort()[i].InterfaceInfo.classic_string.Item);
            }
        }


        //Активность и ее обновление
        private void ActivityProperiesUpdate()
        {
            list_Activity.Items.Clear();
            foreach (GuiString str in Activity.InterfaceInfo.activity_props)
            {
                list_Activity.Items.Add(str.Item);
            }
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
            combo_RelationChoose.SelectionChanged -= PropertyUsersSelection;
        }
        //Включение событий показа дополнительной информации в пользователях
        private void UserPropsEventsOn()
        {
            list_UserPropertiesNumeric.SelectionChanged += PropertyUsersSelection;
            list_TraitsNew.SelectionChanged += PropertyUsersSelection;
            list_UserPropertiesBasic.SelectionChanged += PropertyUsersSelection;
            combo_RelationChoose.SelectionChanged += PropertyUsersSelection;
        }

        //Событие возникающее при выборе свойства пользователя, позволяющее отображать дополнительную информацию
        public void PropertyUsersSelection(object sender, RoutedEventArgs e)
        {
            list_UsersAlpha_SelectedProp.Items.Clear();

            ListBoxItem item = new ListBoxItem();
            //Вызов из комбобокса и листбокса
            try
            {
                ListBox lb = (ListBox)sender;
                item = (ListBoxItem)lb.SelectedItem;
            }
            catch
            {
                ComboBox cb = (ComboBox)sender;
                item = (ListBoxItem)cb.SelectedItem;
            }

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
            if(lb.Items.Count != 0)
            {
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
}
