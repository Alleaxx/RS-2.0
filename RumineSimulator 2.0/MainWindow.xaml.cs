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

        bool relations_generated = false;
        bool users_generated = false;

        //Пользователи, выбранные в списках
        User selected_user;
        User selected_user_beta;
        int selected_index_user;
        User selected_userRelation;

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
            selected_index_user = -1;
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 12, 0, 0));
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();
            for (int i = 0; i < TraitsList.AllTraits.Count; i++)
            {
                Trait trait = TraitsList.AllTraits.ElementAt(i).Value;
                ListBoxItem item = new ListBoxItem()
                {
                    Content = trait.short_name,
                    Background = trait.background_brush,
                    Foreground = trait.foreground_brush
                };
                list_AllTraits.Items.Add(item);
            }
        }



        #endregion

        #region Обновление переменных



        //Просто обновление ВЫБРАННЫХ пользователей
        private void SELECTIONUsersUpdate()
        {

        }
        //Выбор пользователя(бета-версия)
        private void WrapUser_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = (ListBoxItem)sender;
            StackPanel Spanel0 = (StackPanel)item.Content;
            StackPanel Spanel = (StackPanel)Spanel0.Children[1];
            TextBlock nick = (TextBlock)Spanel.Children[0];
            foreach (User user in Users.UsersList)
            {
                if (user.nick == nick.Text)
                {
                    selected_user_beta = user;
                }
            }
        }


        //Спрятать описание пользователей
        private void CheckBox_DescrHide_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Testing_Click(object sender, RoutedEventArgs e)
        {
        }

        #endregion

        #region Обновление интерфейса




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

        //Управление обновлением

        //Информация
        //Характеристики выбранного события
        private void List_passedEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list_EventChar.Items.Clear();
            list_EventParticipants.Items.Clear();
            try
            {
                InterfaceView_Event info_event = Events_List.AllEvents[list_passedEvents.SelectedIndex].InterfaceInfo;
                foreach (Interface_String inter_string in info_event.basic_event_properties)
                {
                    list_EventChar.Items.Add(inter_string.Item);
                }
                foreach (Interface_String inter_string in info_event.special_event_properties)
                {
                    list_EventChar.Items.Add(inter_string.Item);
                }
                foreach (Interface_String inter_string in info_event.connectedEntities_properties)
                {
                    list_EventParticipants.Items.Add(inter_string.Item);
                }
            }
            catch
            {

            }
        }
        //Логика экспандеров
        private void exp_choose_Collapsed(object sender, RoutedEventArgs e)
        {
        }
        private void exp_info_Collapsed(object sender, RoutedEventArgs e)
        {
        }
        private void Text_warningsLevel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWarnings = new WindowWarn(selected_user.nick);
            WindowWarnings.Show();
        }
        private void Text_BaseRep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowReputation = new WindowReputation(selected_user.nick);
            WindowReputation.Show();
        }

        //Тулбар
        //Зайти на сайт с тулбара
        //Скрыть левый тулбар
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




        #endregion

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
            Users.CheckingAllUserForUpdates();
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






        //Управление
        private void Button_UserGenerate_Click(object sender, RoutedEventArgs e)
        {
            text_log.AppendText("\nГенерируются юзеры...");
            relations_generated = false;
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
            text_GeneratedUsers.Text = Users.UserAmount.ToString();
            if (users_generated)
            {
                Nicks.NicksInit();
                Users.UsersList.Clear();
            }
            timer_users.Start();
        }
        //Результат
        private void TimerTick(object sender, EventArgs e)
        {
            if (timer_total_users == timer_generated_users)
            {
                timer_users.Stop();
                text_log.AppendText("\n" + timer_total_users + " юзеров сгенерировано...");
                Users.GenerateRelations();
                text_log.AppendText("\nОтношения сгенерированы...");
                Users.ModerChoose();
                relations_generated = true;
                users_generated = true;
                Users.FractionChoose();
                Users.CreateInterfaceInfo();
                Activity.Activity_Init();
                text_log.AppendText("\nАктивность инициализирована...");
                HistoricEvents_List.HistoricEvents_Creation(1);
                text_log.AppendText("\nИсторические события заданы...");
                list_AverageTemperature.Items.Add($"Раковитость: {Users.aver_rakness}");
                list_AverageTemperature.Items.Add($"Адекватность: {Users.aver_adeq}");
                list_AverageTemperature.Items.Add($"Консервативность: {Users.aver_conservative}");
                list_AverageTemperature.Items.Add($"Толерантность: {Users.aver_tolerance}");
                list_AverageTemperature.Items.Add($"Креативность: {Users.aver_creativity}");
                list_AverageTemperature.Items.Add($"Наука: {Users.aver_sciense}");
                list_AverageTemperature.Items.Add($"Объекты отношений: {GlobalParams.relation_obj}");
            }

            else
            {
                Users.GenerateUsers(1);
                text_GeneratedUsers.Text = Users.UserAmount.ToString();
                timer_generated_users++;
            }
        }






        #endregion


        #region Вспомогательные методы(генерация панелей)

        #endregion

        //F5
        private void status_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            //Обновить события
            list_passedEvents.Items.Clear();
            foreach (Event eve in Events_List.AllEvents)
            {
                list_passedEvents.Items.Add(eve.InterfaceInfo.string_info.Item);
            }
            list_EventsProperties.Items.Clear();
            List<Interface_String> IntInfo = Events_List.GetInterfaceInfo();
            foreach (Interface_String str in IntInfo)
            {
                list_EventsProperties.Items.Add(str.Item);
            }
            //Обновление пользователей
            if(list_UsersAlpha.Items.Count < 2)
            {
                for (int i = 0; i < Users.UsersList.Count; i++)
                {
                    list_UsersAlpha.Items.Add(Users.UsersList[i].InterfaceInfo.interface_basic.Item);
                }
            }
            //Обновление фракций
            list_FractionsInfo.Items.Clear();
            foreach (Fraction fraction in FractionList.AllFractions)
            {
                list_FractionsInfo.Items.Add(fraction.Interface_Info.string_info.Item);
            }
        }

        private void button_EventView_Click(object sender, RoutedEventArgs e)
        {
            WindowEvent = new EventView(Events_List.AllEvents[list_passedEvents.SelectedIndex].id);
            WindowEvent.Show();
        }

        private void list_UsersAlpha_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoUserUpdate_Alpha();
        }


        private void button_ReputationBeta_Click(object sender, RoutedEventArgs e)
        {
            WindowReputation = new WindowReputation(selected_user.nick);
            WindowReputation.Show();
        }

        //Изменение выбранной фракции
        private void list_FractionsInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InfoFractionsUpdate_Alpha();
        }
        //Обновление информации о выбранной фракции
        private void InfoFractionsUpdate_Alpha()
        {
            list_FractionDetails.Items.Clear();
            try
            {
                Fraction sel_fraction = FractionList.AllFractions[list_FractionsInfo.SelectedIndex];
                foreach (Interface_String str in sel_fraction.Interface_Info.fraction_properties)
                {
                    list_FractionDetails.Items.Add(str.Item);
                }

            }
            catch
            {

            }
        }
        //Обновление информации о выбранном пользователе
        private void InfoUserUpdate_Alpha()
        {
            try
            {
                User sel_user = Users.UsersList[list_UsersAlpha.SelectedIndex - 1];
                selected_user = sel_user;
                //Аватарка
                Image_Ava.Source = sel_user.InterfaceInfo.interface_basic.ImageSource;
                //Основные св-ва
                list_UserPropertiesBasic.Items.Clear();
                foreach (Interface_String info in sel_user.InterfaceInfo.basic_properties)
                {
                    list_UserPropertiesBasic.Items.Add(info.Item);
                }
                //Трейты
                list_TraitsNew.Items.Clear();
                foreach (Interface_String info in sel_user.InterfaceInfo.traits)
                {
                    list_TraitsNew.Items.Add(info.Item);
                }
                //Описание
                text_Description.Text = sel_user.description;
                //Числовые свойства
                list_UserPropertiesNumeric.Items.Clear();
                foreach (Interface_String info in sel_user.InterfaceInfo.numeric_properties)
                {
                    list_UserPropertiesNumeric.Items.Add(info.Item);
                }
                //Репутация
                button_ReputationBeta.Content = $"({sel_user.reputation.Base_reputation.ToString()})";
                button_ReputationMinusBeta.Content = $"(-{sel_user.reputation.Otr_reputation.ToString()})";
                button_ReputationPlusBeta.Content = $"(+{sel_user.reputation.Pos_reputation.ToString()})";

                //Прогресс-бары
                list_Character.Items.Clear();
                foreach (Interface_String info in sel_user.InterfaceInfo.character_properties)
                {
                    if (info.GetType() == sel_user.InterfaceInfo.interface_basic.GetType())
                        list_Character.Items.Add(info.Item);
                    else
                    {
                        list_Character.Items.Add(info.Item);
                    }
                }
                list_Skills.Items.Clear();
                foreach (Interface_String info in sel_user.InterfaceInfo.skills_properties)
                {
                    if (info.GetType() == sel_user.InterfaceInfo.interface_basic.GetType())
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

        }
    }
}
