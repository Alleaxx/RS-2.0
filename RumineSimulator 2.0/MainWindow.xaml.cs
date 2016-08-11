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
        private short speed = 1;
        private short update_time = 5;
        private short updates = 0;
        Dictionary<string, ListBoxItem> UserItems = new Dictionary<string, ListBoxItem>();
        Dictionary<string, ListBoxItem> UserRelItems = new Dictionary<string, ListBoxItem>();

        //Информация для таймера - сколько юзеров генерировать и сколько он уже создал
        int timer_generated_users;
        int timer_total_users;
        bool relations_generated = false;

        //Пользователи, выбранные в списках
        User selected_user;
        User selected_userRelation;

        public MainWindow()
        {
            InitializeComponent();
            //Создание базовых групп
            UserNicks.NicksInit();
            text_log.AppendText("\nИнициализация ников успешна...");
            UserGroupsControler.UserGroupsInitCreation();
            text_log.AppendText("\nГруппы созданы...");
            UserStereoTypeControl.UserStereoTypesInitCreation();
            text_log.AppendText("\nСтереотипы созданы...");
            UserTraitsList.TraitsInit();
            text_log.AppendText("\nТрейты созданы...");
            list_UserDetail.SelectedIndex = 1;
            list_Relations.SelectedIndex = 1;
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 11, 59, 0));
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();

        }
        #endregion

        #region Обновление информации
        private void DateUpdate()
        {
            StatusTextData.Text = Date.ReturnCurrDate();
        }

        //Обновляем любой список какой дадут
        private void UserListAllUpdate()
        {
            UserItems.Clear();
            list_UserDetail.Items.Clear();
            List<User> list_sort = new List<User>();
            #region Сортировка
            switch (comboBox_userSort.SelectedIndex)
            {
                //Без сортировки
                case 0:
                    list_sort = UserControl.Users;
                    break;
                //Ценность группы    
                case 1:
                    list_sort = UserControl.ReturnUsersGroupRarenesSortDesc();
                    break;
                //По дате регистрации
                case 2:
                    list_sort = UserControl.ReturnUsersRegSortDesc();
                    break;
                //По репутации
                case 3:
                    list_sort = UserControl.ReturnUsersRepSortDesc();
                    break;
                //Сообщениям
                case 4:
                    list_sort = UserControl.ReturnUsersMessagesSortDesc();
                    break;
                //Адекватности
                case 5:
                    list_sort = UserControl.ReturnUsersAdeqSortDesc();
                    break;
                //Раковитости
                case 6:
                    list_sort = UserControl.ReturnUsersRakSortDesc();
                    break;
                //Консервативности
                case 7:
                    list_sort = UserControl.ReturnUsersConsSortDesc();
                    break;
                case 8:
                    list_sort = UserControl.ReturnUsersCreativeSortDesc();
                    break;
                case 9:
                    list_sort = UserControl.ReturnUsersScienseSortDesc();
                    break;
                default:
                    list_sort = UserControl.Users;
                    break;
            }
            #endregion



            for (int i = 0; i < list_sort.Count; i++)
            {
                //Добавляем в список идентификатор юзера
                ListBoxItem user_item = new ListBoxItem();
                user_item.Content = StackPanUserLists(list_sort[i]);
                UserItems.Add(list_sort[i].nick, user_item);
                list_UserDetail.Items.Add(user_item);
            }
            list_UserDetail.SelectedIndex = 1;
            SelectedUsersUpdate();
            if (relations_generated)
                UserListRelUpdate();

        }
        private void UserListRelUpdate()
        {
            if (selected_user != null && relations_generated)
            {
                list_Relations.Items.Clear();
                UserRelItems.Clear();
                List<User> list_sort = new List<User>();
                #region Сортировка
                switch (comboBox_SortRel.SelectedIndex)
                {
                    //Без сортировки
                    case 0:
                        list_sort = UserControl.Users;
                        break;
                    case 1:
                        list_sort = selected_user.relations.ReturnUsersRelSortDesc();
                        break;
                    default:
                        list_sort = UserControl.Users;
                        break;

                }
                #endregion
                for (int i = 0; i < list_sort.Count; i++)
                {
                    //Добавляем в список идентификатор юзера
                    ListBoxItem user_item = new ListBoxItem();
                    user_item.Content = StackPanUserLists(list_sort[i], true);
                    UserRelItems.Add(list_sort[i].nick, user_item);
                    user_item.Background = new SolidColorBrush(selected_user.relations.All[list_sort[i]].color);
                    list_Relations.Items.Add(user_item);
                }
            }
        }

        private StackPanel StackPanUserLists(User user, bool rel = false)
        {
            #region Главный список
            if (!rel)
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                TextBlock nick = new TextBlock();
                nick.Margin = new Thickness(5, 5, 5, 5);
                nick.TextAlignment = TextAlignment.Justify;
                nick.Text = user.ToString();
                nick.Foreground = user.group.need_brush;
                Image ava = new Image();
                ava.Width = 30;
                ava.Height = 30;
                ava.Margin = new Thickness(0, 5, 5, 5);
                if (UserNicks.AvaPath.ContainsKey(user.nick))
                {
                    ava.Source = UserNicks.AvaPath[user.nick];
                }
                else
                {
                    ava.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
                }
                stackpanel.Children.Add(ava);
                stackpanel.Children.Add(nick);
                return stackpanel;
            }
            #endregion
            #region Список отношений
            else
            {
                StackPanel stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                TextBlock nick = new TextBlock();
                nick.Margin = new Thickness(5, 5, 5, 5);
                nick.TextAlignment = TextAlignment.Justify;
                nick.Text = user.ToString();
                Image ava = new Image();
                ava.Width = 30;
                ava.Height = 30;
                ava.Margin = new Thickness(0, 5, 5, 5);
                if (UserNicks.AvaPath.ContainsKey(user.nick))
                {
                    ava.Source = UserNicks.AvaPath[user.nick];
                }
                else
                {
                    ava.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
                }
                stackpanel.Children.Add(nick);
                stackpanel.Children.Add(ava);
                return stackpanel;
            }
            #endregion

        }
        private StackPanel StackPanHistoryRep(ReputationHistory history)
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            TextBlock author = new TextBlock();
            author.Margin = new Thickness(2, 5, 12, 5);
            author.TextAlignment = TextAlignment.Justify;
            author.Text = history.author.ToString();

            TextBlock Date = new TextBlock();
            Date.Margin = new Thickness(5, 5, 12, 5);
            Date.TextAlignment = TextAlignment.Justify;
            Date.Text = history.date.ToShortDateString() + " " + history.date.ToShortTimeString();

            TextBlock Value = new TextBlock();
            Value.Margin = new Thickness(2, 5, 5, 5);
            Value.TextAlignment = TextAlignment.Justify;
            Value.Text = "(" + history.set_karma.ToString() + ")";

            TextBlock Reason = new TextBlock();
            Reason.Margin = new Thickness(15, 5, 5, 5);
            Reason.TextAlignment = TextAlignment.Justify;
            Reason.Text = history.reason;

            Image imag = new Image();
            imag.Width = 10;
            imag.Height = 10;
            imag.Margin = new Thickness(5, 5, 2, 5);
            if (history.otr)
            {
                imag.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/reputation_down.png"));
            }
            else
            {
                imag.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/reputation_up.png"));
            }
            stackpanel.Children.Add(author);
            stackpanel.Children.Add(Date);
            stackpanel.Children.Add(imag);
            stackpanel.Children.Add(Value);
            stackpanel.Children.Add(Reason);

            return stackpanel;

        }

        //Обновляем выбранных пользователей в пользователях
        private void SelectedUsersUpdate()
        {
            if (list_UserDetail.SelectedItem != null && UserItems.Count > 0)
            {
                int index = list_UserDetail.SelectedIndex;
                for (int i = 0; i < UserControl.Users.Count; i++)
                {
                    if (UserControl.Users[i].nick == UserItems.ElementAt(index).Key)
                        selected_user = UserControl.Users[i];
                }

            }
            if (list_Relations.SelectedItem != null && UserRelItems.Count > 0 && UserItems.Count > 0)
            {
                int index = list_Relations.SelectedIndex;
                for (int i = 0; i < UserControl.Users.Count; i++)
                {
                    if (UserControl.Users[i].nick == UserRelItems.ElementAt(index).Key)
                        selected_userRelation = UserControl.Users[i];
                }
            }

        }

        //Обновляем абсолютно всю информацию о пользователе, включая отношения
        private void InfoUserUpdate()
        {
            //Вывод аватарки
            if (UserNicks.AvaPath.ContainsKey(selected_user.nick))
            {
                ImageSource ava = UserNicks.AvaPath[selected_user.nick];
                image_Ava.Source = ava;
            }
            else
            {
                image_Ava.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
            }

            //Установка ника на заглавие тексбокса. Если слишком большой - дату удаляем. Все равно большой - удаляем еще и часть ника
            string header = $"{selected_user.nick}";
            if (header.Length > 20)
            {
                header = selected_user.nick;
                if (header.Length > 20)
                {
                    header.Substring(0, 17);
                    header = header + "...";
                }

            }
            gB_MainInfo.Header = header;
            text_Registration.Text = selected_user.registration.ToShortDateString();
            text_LastActivity.Text = selected_user.last_activity.ToShortDateString() + " " + selected_user.last_activity.ToShortTimeString();
            text_oldness.Text = selected_user.m_oldness.ToString();

            //Цвет группы юзера и сама группа
            text_Group.Foreground = selected_user.group.need_brush;
            text_Group.Text = ($"{selected_user.group.name}");
            text_Stereotype.Text = selected_user.stereotype.ToString();

            //Статистика
            text_comments.Text = selected_user.comments.ToString();
            text_news.Text = selected_user.news.ToString();
            text_messages.Text = selected_user.messages.ToString();
            text_likes.Text = selected_user.likes.ToString();
            text_commentsRate.Text = "+" + selected_user.comments_rate.ToString();

            //Карма
            gB_karma.Header = "Карма " + selected_user.karma.karma.ToString();
            text_KarmaActivity.Text = selected_user.karma.kar_activity.ToString();
            text_KarmaNews.Text = selected_user.karma.kar_news.ToString();
            text_KarmaRep.Text = selected_user.karma.kar_reputation.ToString();

            //Репутация
            text_BaseRep.Text = "| " + Math.Round(selected_user.reputation.base_reputation, 1).ToString() + " |";
            text_OtrRep.Text = "-" + Math.Round(selected_user.reputation.otr_reputation, 1).ToString();
            text_PosRep.Text = "+" + Math.Round(selected_user.reputation.pos_reputation, 1).ToString();


            //Логические переменные
            if (selected_user.activity)
            {
                image_online.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/online.png"));
            }

            else
            {
                image_online.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/offline.png.gif"));
            }

            checkBox_Mod.IsChecked = selected_user.mod;
            checkBox_wantsMod.IsChecked = selected_user.character.wantsToBeMod;

            //Слайдера
            slider_adeq.Value = selected_user.character.adeq;
            gB_adeq.ToolTip = selected_user.character.adeq;

            slider_conservative.Value = selected_user.character.conservative;
            gB_conservative.ToolTip = selected_user.character.conservative;

            slider_rakness.Value = selected_user.character.rakness;
            gB_rakness.ToolTip = selected_user.character.rakness;

            slider_creative.Value = selected_user.character.Creativity;
            gB_creative.ToolTip = selected_user.character.Creativity;

            slider_sciense.Value = selected_user.character.Sciense;
            gB_sciense.ToolTip = selected_user.character.Sciense;

            slider_tolerance.Value = selected_user.character.tolerance;
            gB_tolerance.ToolTip = selected_user.character.tolerance;

            //Текстовое описание
            text_UserDescription.Text = selected_user.description;
            valueUser_log.Text = "";
            foreach (UserDayLog log in selected_user.last_thirty_Days)
            {
                valueUser_log.Text = valueUser_log.Text + log.text_descr;
            }
            valueUser_log.ScrollToEnd();
            list_reputationHistory.Items.Clear();
            for (int i = selected_user.reputation.history.Count - 1; i >= 0; i--)
            {
                list_reputationHistory.Items.Add(StackPanHistoryRep(selected_user.reputation.history[i]));
            }
            list_Traits.Items.Clear();
            foreach (UserTrait trait in selected_user.traits)
                list_Traits.Items.Add(trait.short_name);


            //Обновление отношений пользователя
            if (relations_generated && selected_user != selected_userRelation && selected_userRelation != null)
            {
                UserRelationUpdate();
            }


        }
        //Вспомогательный метод для полного обновления информации
        private void UserRelationUpdate()
        {
            #region Обновление основного сравнения
            expanderListUser_rel.Header = ($"{selected_user.ToString()} и {selected_userRelation.ToString()}");
            text_nickDiff2.Text = selected_userRelation.nick;
            text_RegDiff2.Text = selected_userRelation.registration.ToShortDateString();
            text_GroupDiff1.Foreground = selected_user.group.need_brush;
            text_GroupDiff2.Text = selected_userRelation.group.name;
            text_GroupDiff2.Foreground = selected_userRelation.group.need_brush;
            #endregion

            #region Само отношение
            SolidColorBrush brush_rel_text = new SolidColorBrush();
            switch (selected_user.relations.All[selected_userRelation].relation)
            {
                case RelationsEnum.friend:
                    brush_rel_text = new SolidColorBrush(selected_user.relations.All[selected_userRelation].color);
                    text_Relation.Text = "Друзья";
                    break;
                case RelationsEnum.comrade:
                    brush_rel_text = new SolidColorBrush(Colors.Lime);
                    text_Relation.Text = "Товарищи";
                    break;
                case RelationsEnum.neutral:
                    brush_rel_text = new SolidColorBrush(Colors.LightSlateGray);
                    text_Relation.Text = "Нейтралы";
                    break;
                case RelationsEnum.unfriend:
                    brush_rel_text = new SolidColorBrush(Colors.IndianRed);
                    text_Relation.Text = "Неприятели";
                    break;
                case RelationsEnum.enemy:
                    brush_rel_text = new SolidColorBrush(Colors.Red);
                    text_Relation.Text = "Враги";
                    break;
            }
            slider_friendness.Value = selected_user.relations.All[selected_userRelation].friendness;
            text_Relation.Foreground = brush_rel_text;
            //Разницы в сладйерах
            slider_AdeqDiff.Value = selected_user.relations.All[selected_userRelation].adeq_difference;
            slider_RaknessDiff.Value = selected_user.relations.All[selected_userRelation].rak_difference;
            slider_ConsDiff.Value = selected_user.relations.All[selected_userRelation].cons_difference;
            #endregion

            #region Статистика
            text_KarmaDiff1.Text = selected_user.karma.karma.ToString();
            text_RepDiff1.Text = Math.Round(selected_user.reputation.base_reputation, 1).ToString();

            text_KarmaDiff2.Text = selected_userRelation.karma.karma.ToString();
            text_CommentsDiff2.Text = selected_userRelation.comments.ToString();
            text_RepDiff2.Text = Math.Round(selected_userRelation.reputation.base_reputation, 1).ToString();
            text_NewsDiff2.Text = selected_userRelation.news.ToString();
            text_MessagesDiff2.Text = selected_userRelation.messages.ToString();
            text_LikesDiff2.Text = selected_userRelation.likes.ToString();

            if (selected_user.karma.karma > selected_userRelation.karma.karma)
            {
                text_KarmaDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_KarmaDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.karma.karma < selected_userRelation.karma.karma)
            {
                text_KarmaDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_KarmaDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_KarmaDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_KarmaDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (selected_user.comments > selected_userRelation.comments)
            {
                text_CommentsDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_CommentsDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.comments < selected_userRelation.comments)
            {
                text_CommentsDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_CommentsDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_CommentsDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_CommentsDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (selected_user.reputation.base_reputation > selected_userRelation.reputation.base_reputation)
            {
                text_RepDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_RepDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.reputation.base_reputation < selected_userRelation.reputation.base_reputation)
            {
                text_RepDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_RepDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_RepDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_RepDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (selected_user.news > selected_userRelation.news)
            {
                text_NewsDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_NewsDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.news < selected_userRelation.news)
            {
                text_NewsDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_NewsDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_NewsDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_NewsDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (selected_user.messages > selected_userRelation.messages)
            {
                text_MessagesDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_MessagesDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.messages < selected_userRelation.messages)
            {
                text_MessagesDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_MessagesDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_MessagesDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_MessagesDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }

            if (selected_user.likes > selected_userRelation.likes)
            {
                text_LikesDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_LikesDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.likes < selected_userRelation.likes)
            {
                text_LikesDiff1.Foreground = new SolidColorBrush(Colors.DarkRed);
                text_LikesDiff2.Foreground = new SolidColorBrush(Colors.DarkGreen);
            }
            else
            {
                text_LikesDiff1.Foreground = new SolidColorBrush(Colors.Blue);
                text_LikesDiff2.Foreground = new SolidColorBrush(Colors.Blue);
            }
            #endregion
        }
        #endregion

        #region Ход времени
        //Начало отсчета
        private void button_TimeGo_Click(object sender, RoutedEventArgs e)
        {
            timer_TimeGo = new DispatcherTimer();
            timer_TimeGo.Tick += new EventHandler(TimeGoTick);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 250);
            update_time = Convert.ToInt16(value_UpdateTime.Text);
            timer_TimeGo.IsEnabled = true;
            button_TimeGo.IsEnabled = false;
            StatusSpeed_x1.IsEnabled = true;
            StatusSpeed_x2.IsEnabled = true;
            StatusSpeed_x3.IsEnabled = true;
            StatusSpeed_x4.IsEnabled = true;
            statusRadButton_cont.IsEnabled = true;
            statusRadButton_pause.IsEnabled = true;
            Activity.SetOnlineModifier();
            Activity.SetTimeForUsersForHour();
        }

        //Главный тик таймера
        private void TimeGoTick(object sender, EventArgs e)
        {
            Date.TimeGo();
            if (Date.current_date.Hour < Date.current_date_prev.Hour)
            {
                Activity.NewDayUpdate();
            }
            if (Date.current_date.Minute % 10 == 0)
            {
                Activity.ActivityDimishing();
            }
            if (Date.current_date.Minute < Date.current_date_prev.Minute)
            {
                Activity.SetTimeForUsersForHour();
            }
            Activity.TimeMinutePassing();

            #region Обновление инфы
            if (updates >= Convert.ToInt16(value_UpdateTime.Text))
            {
                SelectedUsersUpdate();
                InfoUserUpdate();
                wrapPanel_online.Children.Clear();
                //Заполнение онлайна
                List<User> online = Activity.GetOnline();
                gB_online.Header = "Сейчас онлайн: " + Activity.all_online + $"({Activity.online.Count})";
                try
                {
                    TextBlock text = new TextBlock();
                    text.Text = online[0].nick;
                    text.FontWeight = text_Group.FontWeight;
                    text.FontSize = 12;
                    text.Foreground = online[0].group.need_brush;
                    wrapPanel_online.Children.Add(text);
                }
                catch
                {

                }
                for (int i = 1; i < online.Count; i++)
                {
                    TextBlock text = new TextBlock();
                    text.Text = ", " + online[i].nick;
                    text.FontSize = 12;
                    text.FontWeight = text_Group.FontWeight;
                    text.Foreground = online[i].group.need_brush;
                    wrapPanel_online.Children.Add(text);

                }
                #region Обновление еще миллиона списков
                if ((bool)radio_AllInfoUpdate.IsChecked)
                {
                    UserListAllUpdate();
                }
                #endregion

                //Запись в лог
                text_log.Text = "";
                text_log.Text = text_log.Text + (Activity.log);
                text_log.ScrollToEnd();
                StatusTextActivness.Text = Activity.activness;

                text_messagesDay.Text = Activity.messages_per_day.ToString();

                updates = 0;
            }
            updates += (short)Date.max_minutes_pass;


            UserControl.CheckingAllUserForUpdates();
            DateUpdate();
            #endregion

            #region Смотрим на паузы
            bool? pause = statusRadButton_pause.IsChecked;
            if (!pause.HasValue)
            {

            }
            else if ((bool)pause)
            {
                timer_TimeGo.IsEnabled = false;
            }
            #endregion

        }

        //Пауза и "продолжить"
        private void statusRadButton_cont_Checked(object sender, RoutedEventArgs e)
        {
            timer_TimeGo.IsEnabled = true;
        }

        #region Скорость
        private void StatusSpeed_x1_Click(object sender, RoutedEventArgs e)
        {
            speed = 1;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 150);
            StatusTextSpeed.Text = "Скорость x1";
        }
        private void StatusSpeed_x2_Click(object sender, RoutedEventArgs e)
        {
            speed = 2;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 75);
            StatusTextSpeed.Text = "Скорость x2";
        }
        private void StatusSpeed_x3_Click(object sender, RoutedEventArgs e)
        {
            speed = 3;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 50);
            StatusTextSpeed.Text = "Скорость x3";
        }
        private void StatusSpeed_x4_Click(object sender, RoutedEventArgs e)
        {
            speed = 4;
            Date.SetTimeGoSpeed(speed);
            timer_TimeGo.Interval = new TimeSpan(0, 0, 0, 0, 15);
            StatusTextSpeed.Text = "Скорость x4";
        }
        #endregion

        #endregion

        #region События

        //Меняется юзер в основном списке
        private void list_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedUsersUpdate();
            UserListRelUpdate();
            InfoUserUpdate();
        }
        //Меняется юзер в списке отношений
        private void list_UserRelation_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SelectedUsersUpdate();
            if (selected_user != selected_userRelation && selected_userRelation != null)
            {
                UserRelationUpdate();
            }
        }

        //Кнопка генерации пользователей
        private void button_UserGenerate_Click(object sender, RoutedEventArgs e)
        {
            text_log.AppendText("\nГенерируются юзеры...");
            button_GenerateUsers.IsEnabled = false;
            GenerateUsers();
        }
        //Обновление сортировки
        private void comboBoxUserSort_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListAllUpdate();
        }
        private void comboBoxUserSort_rel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListRelUpdate();
        }

        #endregion

        #region Таймер и генерация пользователей

        private void GenerateUsers()
        {
            int GenerateUsersAmount;
            try
            {
                GenerateUsersAmount = int.Parse(value_GenerateUsers.Text);
                if (GenerateUsersAmount > 500)
                    GenerateUsersAmount = 500;
                else if (GenerateUsersAmount < 1)
                    GenerateUsersAmount = 1;
            }
            catch
            {
                GenerateUsersAmount = 60;
                value_GenerateUsers.Text = 60.ToString();
                text_log.AppendText("\nКол-во генерируемых пользователей установлено на 60");
            }
            timer_total_users = GenerateUsersAmount;
            timer_generated_users = 0;
            timer_users = new DispatcherTimer(DispatcherPriority.Render);  // если надо, то в скобках указываем приоритет, например DispatcherPriority.Render
            timer_users.Tick += new EventHandler(timerTick);
            timer_users.Interval = new TimeSpan(0, 0, 0, 0, 5);
            timer_users.Start();
        }

        private void timerTick(object sender, EventArgs e)
        {
            if (timer_total_users == timer_generated_users)
            {
                timer_users.Stop();
                button_GenerateUsers.IsEnabled = false;
                text_log.AppendText("\n" + timer_total_users + " юзеров сгенерировано...");
                UserListAllUpdate();
                UserControl.GenerateRelations();
                text_log.AppendText("\nОтношения сгенерированы...");
                relations_generated = true;
                button_GenerateUsers.IsEnabled = false;
                ListView_UsersUpdate();

            }

            else
            {
                UserControl.GenerateUsers(1);
                timer_generated_users++;
            }
        }

        private void ListView_UsersUpdate()
        {
            listView_Users.Items.Clear();
            foreach (User user in UserControl.Users)
            {
                listView_Users.Items.Add(new
                {
                    Nick = user.nick,
                    Registration = user.registration.ToShortDateString(),
                    Group = user.group.name,
                    Stereotype = user.stereotype.text,
                    Activity = user.last_activity.ToShortDateString() + " " + user.last_activity.ToShortTimeString(),
                    Comments = user.comments,
                    CommentsRate = user.comments_rate,
                    News = user.news,
                    Messages = user.messages,
                    Likes = user.likes,
                    Karma = user.karma.karma,
                    Reputation = Math.Round(user.reputation.base_reputation, 1)
                });
            }
        }


        #endregion

        private void button_clearLog_Click(object sender, RoutedEventArgs e)
        {
            text_log.Text = "";
        }

        private void button_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            if (tabControlMain.SelectedIndex == 2)
            {
                ListView_UsersUpdate();
            }
            else if (tabControlMain.SelectedIndex == 1)
            {
                UserListAllUpdate();
                InfoUserUpdate();
            }
        }
    }
}
