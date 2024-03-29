﻿using System;
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
    /// 


    public partial class MainWindow : Window
    {

        #region Начальные параметры, инициализация
        private DispatcherTimer timer_TimeGo = null;

        private short speed = 1;
        //Инициализация базовых объектов
        public MainWindow()
        {
            InitializeComponent();
            Nicks.AvasInit();
            Group.GroupsCreation();
            Trait.TraitsInit();
            TopicControl.TopicsInit();
            ReputationReason.ReasonsInit();
            text_log.AppendText($"\nДаты установлены на: ");
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 12, 0, 0));
            text_log.AppendText($"\nДата создания румине: {Date.found_date.ToShortDateString()}");
            text_log.AppendText($"\nНынешняя дата: {Date.ReturnCurrDate()}");
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();
            for (int i = 0; i < Trait.allTraits.Count; i++)
            {
                list_TraitsInfo.Items.Add(Trait.allTraits[i].InterfaceInfo.classic_string.Item);
            }
            for (int i = 0; i < Group.groups.Count; i++)
            {
                list_GroupsInfo.Items.Add(Group.groups[i].InterfaceInfo.classic_string.Item);
            }

            UserPropsEventsOn();

            list_FractionsInfo.SelectionChanged += PropertyViewListSelection;
            list_TraitsInfo.SelectionChanged += PropertyViewListSelection;
            list_GroupsInfo.SelectionChanged += PropertyViewListSelection;
        }

        #endregion

        #region Время

        //Главный тик таймера c обновлением логики
        private void TimeGoTick(object sender, EventArgs e)
        {
            timer_TimeGo.IsEnabled = false;
            Date.TimeGo();
            Activity.Time_Pass();

            UsersControl.CheckingAllUserForUpdates();
            ActionControl.TimeGoUpdate();

            if (!(bool)statusRadButton_pause.IsChecked)
                timer_TimeGo.IsEnabled = true;
        }

        //Пауза и "продолжить"
        private void StatusRadButton_cont_Checked(object sender, RoutedEventArgs e)
        {
            timer_TimeGo.IsEnabled = true;
        }
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

        #region Активность
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
            //Исчезновение кнопки
            button_TimeGo.Margin = new Thickness(0, 0, 0, 0);
            button_TimeGo.Height = 0;
            button_TimeGo.Width = 0;
            button_TimeGo.Visibility = Visibility.Hidden;
            button_TimeGo.IsEnabled = false;

            Date.DateChanged += Date_DateChanged;

        }
        //Обработчик события изменения даты
        private void Date_DateChanged(object sender, EventArgs e)
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

        //Обработчики событий из активности, изменение параметров
        private void Activity_ActivityInfoChanged(object sender, ActivityEventArgs e)
        {
            if (Presenter.accurateInfo)
            {
                list_Activity.Items.Clear();
                foreach (GuiString str in Activity.InterfaceInfo.activity_props)
                {
                    list_Activity.Items.Add(str.Item);
                }
            }
        }

        #endregion

        #region События

        //Метод обновления информации о выбранном событии
        private void EventInfoUpdate()
        {
            text_EventName.Text = Presenter.selected_event.Name;
            text_EventDescr.Text = Presenter.selected_event.description;
            list_EventProperties.Items.Clear();

            IntView info = Presenter.selected_event.GetGui();
            foreach (GuiString str in info.all_properties)
            {
                list_EventProperties.Items.Add(str.Item);
            }

        }
        //Обновление событий, галка
        private void check_EventsUpdate_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_update = (bool)check_EventsUpdate.IsChecked;

        }
        //Изменение количества показываемых событий
        private void text_EventsShowed_TextChanged(object sender, TextChangedEventArgs e)
        {
            Presenter.ShowedEventsCount = Convert.ToInt32(text_EventsShowed.Text);
        }
        //Сортировка показываемых событий по тексту
        private void text_EventSearch_SelectionChanged(object sender, RoutedEventArgs e)
        {
            list_passedEvents.Items.Clear();
            Presenter.events_search_text = text_EventSearch.Text;
            Presenter.EventsListUpdate();
            for (int i = 0; i < Presenter.events_sorted.Count; i++)
            {
                list_passedEvents.Items.Add(Presenter.events_sorted[i].GetGui().classic_string.Item);
            }
        }
        //Изменение галок на сортировку событий
        private void check_EventsSlight_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_slight = (bool)check_EventsSlight.IsChecked;
        }
        private void check_EventsMedium_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_medium = (bool)check_EventsMedium.IsChecked;
        }
        private void check_EventsImportant_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_important = (bool)check_EventsImportant.IsChecked;
        }
        private void check_EventsHistoric_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_historic = (bool)check_EventsHistoric.IsChecked;
        }
        private void check_EventsUnusual_Click(object sender, RoutedEventArgs e)
        {
            Presenter.events_unusual = (bool)check_EventsUnusual.IsChecked;
        }

        //Событие возникающее при выборе события, позволяющее отображать дополнительную информацию
        private void PropertyEventSelection(object sender, SelectionChangedEventArgs e)
        {
            if (list_passedEvents.SelectedItem != null)
            {
                list_EventProperties.Items.Clear();
                ListBoxItem item = (ListBoxItem)list_passedEvents.SelectedItem;
                Presenter.SelectionCheck(item.Name);
                if (Presenter.InterfaceInfoReturn(item.Name) != null)
                {
                    IntView info = Presenter.InterfaceInfoReturn(item.Name);
                    foreach (GuiString str in info.all_properties)
                    {
                        list_EventProperties.Items.Add(str.Item);
                    }
                    text_EventDescr.Text = Presenter.selected_event.description;
                }
            }
        }
        //Обработчик событий из активности, новое событие
        private void Presenter_EventsListUpdated(object sender, ActivityEventArgs e)
        {
            list_passedEvents.Items.Clear();
            List<Event> ev_list = (List<Event>)sender;
            foreach (Event eve in ev_list)
            {
                list_passedEvents.Items.Add(eve.GetGui().classic_string.Item);
            }
        }

        #endregion

        #region Пользователи
        //Кнопка генерации пользователей
        private void Button_UserGenerate_Click(object sender, RoutedEventArgs e)
        {
            int GenerateUsersAmount;
            try
            {
                GenerateUsersAmount = int.Parse(text_GeneratedUsers.Text);
            }
            catch
            {
                GenerateUsersAmount = 48;
                text_GeneratedUsers.Text = 50.ToString();
                text_log.AppendText("\nКол-во генерируемых пользователей установлено на 50");
            }
            //Собственно, генерация
            UsersControl.GenerateUser(true);
            UsersControl.GenerateUser(true);
            for (int i = 2; i < GenerateUsersAmount; i++)
            {
                UsersControl.GenerateUser();
            }

            //Исчезновение кнопки
            button_GenerateUsers.Margin = new Thickness(0, 0, 0, 0);
            button_GenerateUsers.IsEnabled = false;
            button_GenerateUsers.Visibility = Visibility.Hidden;
            button_GenerateUsers.Width = 0;
            button_GenerateUsers.Height = 0;

            //Инициализация после создания пользователей
            UsersControl.GenerateRelations();
            Group.ModerChoose();
            Activity.Activity_Init();
            Activity.NewEventAdded += Presenter.EventNewCheck;
            Presenter.EventsListUpdated += Presenter_EventsListUpdated;
            Activity.ActivityInfoChanged += Activity_ActivityInfoChanged;

            //HistoricEvents_List.HistoricEvents_Creation(1);
            text_log.AppendText("\nПользователи сгенерированы");
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
            if (lb.Items.Count != 0)
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
                //Описание и недавняя активность
                text_Description.Text = sel_user.description;
                list_UserLastEvents.Items.Clear();
                List<Event> user_events = EventsControl.EventSearch(Presenter.selected_user);
                if (user_events.Count != 0)
                {
                    foreach (Event eve in user_events)
                    {
                        list_UserLastEvents.Items.Add(eve.GetGui().classic_string.Item);
                    }
                }
                else
                {
                    ListBoxItem lb = new ListBoxItem();
                    lb.IsHitTestVisible = false;
                    lb.Content = "Никаких последних событий не найдено";
                    list_UserLastEvents.Items.Add(lb);
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
                text_log.AppendText("\nПри обновлении данных произошла ошибка");
            }
            UserPropsEventsOn();

        }
        private void button_ReputationBeta_Click(object sender, RoutedEventArgs e)
        {
            WindowReputation WindowReputation = new WindowReputation();
            try
            {
                WindowReputation.Show();
            }
            catch
            {
                text_log.Text = text_log.Text + "\nОшибка при выводе окна";
            }

        }
        //Кнопка показа предупреждений
        private void button_ViewWarns_Click(object sender, RoutedEventArgs e)
        {
            if (Presenter.selected_user != null)
            {
                WindowWarn WindowWarnings = new WindowWarn();
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
            UsersListUpdate();
        }
        //Обновление списка пользователей
        private void UsersListUpdate()
        {
            Presenter.UsersListUpdate();
            list_UsersAlpha.Items.Clear();
            if (Presenter.users_sorted != null)
            {
                for (int i = 0; i < Presenter.users_sorted.Count; i++)
                {
                    list_UsersAlpha.Items.Add(Presenter.users_sorted[i].InterfaceInfo.classic_string.Item);
                }
            }

        }
        //Галки сортировки пользователей
        private void check_UsersAll_Click(object sender, RoutedEventArgs e)
        {
            Presenter.all_users = (bool)check_UsersAll.IsChecked;
            UsersListUpdate();
        }
        private void check_UsersActive_Click(object sender, RoutedEventArgs e)
        {
            Presenter.active_users = (bool)check_UsersActive.IsChecked;
            UsersListUpdate();
        }
        //Поиск пользователя по нику
        private void text_UserSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Presenter.text_userSearch = text_UserSearch.Text;
            UsersListUpdate();
        }


        //Изменение фракции в списке и ее обновление
        private void list_FractionsInfo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
        private void InfoFractionsUpdate_Alpha()
        {
        }
        #endregion

        #region Функционал

        //Изменение отображения точности информации
        private void checkAccurateInfo_Click(object sender, RoutedEventArgs e)
        {
            Presenter.accurateInfo = (bool)checkAccurateInfo.IsChecked;
        }
        //F5, обновление всех списков
        private void status_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            UserPropsEventsOff();
            //Обновление пользователей
            UsersListUpdate();
            //Обновление пользователей в отношениях, задание особого события при нажатии строки
            combo_RelationChoose.Items.Clear();
            foreach (User user in UsersControl.UserListReturnSort())
            {
                GuiString user_relation = new GuiString("", "");
                user_relation = user.InterfaceInfo.classic_string;
                user_relation.SetGUIName(GUITypes.relation, user.user_id);
                combo_RelationChoose.Items.Add(user_relation.Item);
            }

            //Обновление событий

            UserPropsEventsOn();
        }

        //Выключение событий показа дополнительной информации в пользователях
        private void UserPropsEventsOff()
        {
            list_UserPropertiesNumeric.SelectionChanged -= PropertyUsersSelection;
            list_TraitsNew.SelectionChanged -= PropertyUsersSelection;
            list_UserPropertiesBasic.SelectionChanged -= PropertyUsersSelection;
            combo_RelationChoose.SelectionChanged -= PropertyUsersSelection;
            list_PlayerActions.SelectionChanged -= ActionSelection;
        }
        //Включение событий показа дополнительной информации в пользователях
        private void UserPropsEventsOn()
        {
            list_UserPropertiesNumeric.SelectionChanged += PropertyUsersSelection;
            list_TraitsNew.SelectionChanged += PropertyUsersSelection;
            list_UserPropertiesBasic.SelectionChanged += PropertyUsersSelection;
            combo_RelationChoose.SelectionChanged += PropertyUsersSelection;
            list_PlayerActions.SelectionChanged += ActionSelection;
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

        #endregion

        #region Геймплей

        //Кнопка создания игровой сессии
        private void button_SessionCreate_Click(object sender, RoutedEventArgs e)
        {
            //Исчезновение кнопки
            button_SessionCreate.Margin = new Thickness(0, 0, 0, 0);
            button_SessionCreate.IsEnabled = false;
            button_SessionCreate.Visibility = Visibility.Hidden;
            button_SessionCreate.Width = 0;
            button_SessionCreate.Height = 0;

            GameSession.GameSessionInit();
            //Добавление доступных на данный момент действий
            status_textUser.Text = UsersControl.UserSearch(GameSession.PlayerId).nick;

            ActionControl.ActionEnded += ActionFinished;

            ActionControl.ActionDurChanged += ActionControl_ActionDurUpdated;

            UpdateActionList();
        }


        //Полное обновление списка
        private void UpdateActionList()
        {
            list_PlayerActions.Items.Clear();
            foreach (Action act in ActionControl.ActionQue)
            {
                IntView view = act.GetGui();
                list_PlayerActions.Items.Add(act.GetClassicString().Item);
            }
        }

        //Завершение действие, обновление интерфейса
        private void ActionFinished(object Act, ActionEventArgs e)
        {
            Action act = (Action)Act;
            text_ActionEndDescr.Text = act.result;
            list_PlayerActions.Items.RemoveAt(0);

            status_textEvent.Text = act.result;
        }
        //Изменение длительности действия, обновление интерфейса
        private void ActionControl_ActionDurUpdated(object Dur_action, ActionEventArgs e)
        {
            Action dur_action = (Action)Dur_action;
            list_PlayerActions.Items[0] = dur_action.GetClassicString().Item;
        }


        //Выбор элемента списка запланированных событий
        public void ActionSelection(object sender, RoutedEventArgs e)
        {
            if (sender is ListBox)
            {
                ListBoxItem item = (ListBoxItem)list_PlayerActions.SelectedItem;
                if (item != null && item.Name.Length != 0)
                {
                    Presenter.SelectionCheck(item.Name);
                    list_PlayerActionProps.Items.Clear();
                    text_ActionDescr.Text = Presenter.selectedAction.Description;
                    IntView view = Presenter.selectedAction.GetGui();
                    foreach (GuiString str in view.all_properties)
                    {
                        if (str.Item.Parent == null)
                            list_PlayerActionProps.Items.Add(str.Item);
                    }
                }
            }
        }
        //Нажатие на кнопку, добавление нового типа события
        private void button_ActionGo_Click(object sender, RoutedEventArgs e)
        {
            if (list_AvalActions.SelectedItem != null)
            {
            }
        }




        #endregion


    }
}
