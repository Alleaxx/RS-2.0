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
        WindowCabinet WindowCabinet;
        WindowAboutYou WindowAboutYou;

        private short speed = 1;
        private short update_time = 5;
        private short updates = 0;
        string log_text;
        //Информация для таймера - сколько юзеров генерировать и сколько он уже создал
        int timer_generated_users;
        int timer_total_users;

        bool relations_generated = false;
        bool users_generated = false;

        //Пользователи, выбранные в списках
        User selected_user;
        User selected_user_beta;
        int selected_index_user;
        int player_index;
        User selected_userRelation;

        ControlTemplate template;
        ControlTemplate templateSec;

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
            list_Relations.SelectedIndex = 1;
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 11, 59, 0));
            StatusTextData.Text = Date.ReturnCurrDate();
            text_foundDate.Text = Date.found_date.ToShortDateString();
            for (int i = 0; i < TraitsList.AllTraits.Count; i++)
            {
                Trait trait = TraitsList.AllTraits.ElementAt(i).Value;
                ListBoxItem item = new ListBoxItem();
                item.Content = trait.short_name;
                item.Background = trait.background_brush;
                item.Foreground = trait.foreground_brush;
                list_AllTraits.Items.Add(item);
            }
            exp_info.IsExpanded = false;
            template = testing.Template;
            templateSec = testingSec.Template;
            PlayerActivity.ActivityInit();
        }



        #endregion

        #region Обновление переменных



        //Просто обновление ВЫБРАННЫХ пользователей
        private void SELECTIONUsersUpdate()
        {
            if (list_UserDetail.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)list_UserDetail.SelectedItem;
                StackPanel panel = (StackPanel)item.Content;
                TextBlock text = (TextBlock)panel.Children[1];
                selected_index_user = list_UserDetail.SelectedIndex;
                string nick = text.Text;
                foreach (User user in UserList.Users)
                {
                    if (user.nick == nick)
                        selected_user = user;
                }
                InterfaceAccesUpdate();

            }
            if (list_Relations.SelectedItem != null)
            {
                ListBoxItem item = (ListBoxItem)list_Relations.SelectedItem;
                StackPanel panel = (StackPanel)item.Content;
                TextBlock text = (TextBlock)panel.Children[0];
                string nick = text.Text;
                foreach (User user in UserList.Users)
                {
                    if (user.nick == nick)
                        selected_userRelation = user;
                }
            }


        }
        //Выбор пользователя(бета-версия)
        private void wrapUser_Click(object sender, RoutedEventArgs e)
        {
            exp_choose.IsExpanded = false;
            exp_info.IsExpanded = true;
            ListBoxItem item = (ListBoxItem)sender;
            StackPanel Spanel0 = (StackPanel)item.Content;
            StackPanel Spanel = (StackPanel)Spanel0.Children[1];
            TextBlock nick = (TextBlock)Spanel.Children[0];
            foreach (User user in UserList.Users)
            {
                if (user.nick == nick.Text)
                {
                    selected_user_beta = user;
                    UserBetaUpdate();
                }
            }
        }
        private void UserBetaUpdate()
        {
            //Ник, группа, аватарка, отношение, посещение
            text_Nick.Text = selected_user_beta.nick;
            text_Group1.Text = selected_user_beta.group.name;
            text_Group1.Foreground = selected_user_beta.group.need_brush;
            if (Nicks.AvaPath.ContainsKey(selected_user_beta.nick))
            {
                ImageSource ava = Nicks.AvaPath[selected_user_beta.nick];
                image_Ava1.Source = ava;
            }
            else
            {
                image_Ava1.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
            }
            text_Dates.Text = selected_user_beta.registration.ToShortDateString() + " - " + selected_user_beta.last_activity.ToShortDateString();
            text_Relation1.Foreground = new SolidColorBrush(selected_user_beta.relations.All[Player.user].color);
            text_Relation1.Text = selected_user_beta.relations.All[Player.user].ReturnTextRelation();


            //Репутация-карма
            button_ReputationBase1.Content = $"({selected_user_beta.reputation.Base_reputation.ToString()})";
            button_ReputationMinus.Content = $"(-{selected_user_beta.reputation.Otr_reputation.ToString()})";
            button_ReputationPlus.Content = $"(+{selected_user_beta.reputation.Pos_reputation.ToString()})";
            text_KarmaBase.Text = $"Карма: {selected_user_beta.karma.karma.ToString()} ({selected_user_beta.karma.kar_news}Н + {selected_user_beta.karma.kar_reputation}Р + {selected_user_beta.karma.kar_activity}А)";

            //Сайто-форумная информация
            text_Messages.Text = "Сообщений: " + selected_user_beta.messages;
            text_Likes.Text = "Симпатий: " + selected_user_beta.likes;
            text_Warnings.Text = "Предупреждений: " + selected_user_beta.LastBan.warn_sum;

            text_Comments.Text = "Комментариев: " + selected_user_beta.comments;
            text_CommentsRate.Text = "Рейтинг К.: +" + selected_user_beta.comments_rate;
            text_News.Text = "Новости: " + selected_user_beta.news + " (" + selected_user_beta.news_quality + "%)";
            text_Influence.Text = "Влияние: " + selected_user_beta.forum_influence;

            //Фракции
            text_MainFraction.Text = selected_user_beta.main_fraction.name;
            text_MainFractionMember.Text = "Участник";
            if(selected_user_beta.main_fraction.leader == selected_user_beta)
                text_MainFractionMember.Text = "Лидер";

            //Трейты
            list_Fractions.Items.Clear();
            list_Fractions.Items.Add(selected_user_beta.main_fraction.name);
            foreach (Fraction fraction in selected_user_beta.other_fractions)
            {
                list_Fractions.Items.Add(fraction.name);
            }
            #region Трейты
            list_TraitsStat.Items.Clear();
            list_TraitsChar.Items.Clear();
            foreach (Trait trait in selected_user_beta.traits)
            {
                ListBoxItem item = new ListBoxItem();
                item.Template = template;
                item.Content = trait.short_name;
                item.ToolTip = trait.full_description;
                item.Margin = new Thickness(1, 1, 1, 1);
                item.Background = trait.background_brush;
                item.Foreground = trait.foreground_brush;
                item.Cursor = Cursors.Help;
                if(trait.type == TraitType.character )
                {
                    list_TraitsChar.Items.Add(item);
                }
                else
                {
                    list_TraitsStat.Items.Add(item);
                }
            }
            #endregion

            text_UserDescr.Text = UserDescription.GetTextDescription(selected_user_beta);

            //Характер
            PB_adeq.Value = selected_user_beta.character.adeq.Param_value;
            PB_rakness.Value = selected_user_beta.character.rakness.Param_value;
            PB_Cons.Value = selected_user_beta.character.conservative.Param_value;
            PB_Tolerance.Value = selected_user_beta.character.tolerance.Param_value;
            //Умения
            PB_Creative.Value = selected_user_beta.character.creativity.Param_value;
            PB_sciense.Value = selected_user_beta.character.sciense.Param_value;
            PB_Humanist.Value = selected_user_beta.character.humanist.Param_value;
            PB_Historic.Value = 0;

        }
        private void InterfaceAccesUpdate()
        {
            if (!GlobalParams.Testing && Player.user != null)
            {
                #region Обычные пользователи
                if (Player.access_level >= 0)
                {
                    if (player_index == list_UserDetail.SelectedIndex)
                    {
                        if (Player.user.LastBan.warn_sum > 0)
                        {
                            DP_warningsLevel.Visibility = Visibility.Visible;
                            DP_warningsLevel.Height = 21;
                        }
                        button_privateCab.Visibility = Visibility.Visible;
                        button_privateCab.Height = 27;
                    }
                    else
                    {
                        DP_warningsLevel.Visibility = Visibility.Hidden;
                        DP_warningsLevel.Height = 0;
                        button_privateCab.Visibility = Visibility.Visible;
                        button_privateCab.Height = 0;
                    }
                    button_AdminPanel.Visibility = Visibility.Hidden;
                    button_AdminPanel.Height = 0;

                }
                #endregion

                #region Пользователи с админпанелью
                if (Player.access_level > 1)
                {
                    if (player_index == list_UserDetail.SelectedIndex)
                    {
                        button_AdminPanel.Visibility = Visibility.Visible;
                        button_AdminPanel.Height = 27;
                    }
                }
                #endregion

                #region Модераторы
                if (Player.access_level > 2)
                {
                    DP_warningsLevel.Visibility = Visibility.Visible;
                    DP_warningsLevel.Height = 21;
                }
                #endregion
            }
        }

        //Галочки
        private void checkBox_DescrHide_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)checkBox_DescrHide.IsChecked)
            {
                gB_Description.Visibility = Visibility.Hidden;
            }
            else
            {
                gB_Description.Visibility = Visibility.Visible;
            }
        }
        private void checkBox_Testing_Click(object sender, RoutedEventArgs e)
        {
            GlobalParams.Testing = (bool)checkBox_Testing.IsChecked;
            if (GlobalParams.Testing)
            {
                DP_warningsLevel.Visibility = Visibility.Visible;
                DP_warningsLevel.Height = 21;
                gB_Description.Visibility = Visibility.Visible;
                button_AdminPanel.Visibility = Visibility.Visible;
                button_AdminPanel.Height = 27;
                button_privateCab.Visibility = Visibility.Visible;
                button_privateCab.Height = 27;
            }
        }

        private void checkBox_GodMode_Click(object sender, RoutedEventArgs e)
        {
            GlobalParams.GodMode = (bool)checkBox_GodMode.IsChecked;
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
            if (Player.user.activity)
            {
                text_playerOnline.Text = $"Онлайн ({PlayerActivity.ReturnActivityText()})";
                text_playerOnline.Foreground = new SolidColorBrush(Colors.Green);
            }
            else
            {
                text_playerOnline.Text = $"Оффлайн ({PlayerActivity.ReturnActivityText()})";
                text_playerOnline.Foreground = new SolidColorBrush(Colors.Gray);
            }

        }

        //Управление обновлением
        private void list_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SELECTIONUsersUpdate();
            if (expanderListUser_rel.IsExpanded)
                UserListRelUpdate();
            if (relations_generated && selected_user != selected_userRelation && selected_userRelation != null)
                UserRelationUpdate();
            InfoUserUpdate();
        }
        private void list_UserRelation_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            SELECTIONUsersUpdate();
            if (selected_user != selected_userRelation && selected_userRelation != null)
                UserRelationUpdate();

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
            EventtPassedListUpdate();
        }
        private void list_Traits_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void expanderListUser_rel_Expanded(object sender, RoutedEventArgs e)
        {
            UserListRelUpdate();
        }
        //Перерисовка списков из-за сортировки
        private void comboBoxUserSort_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListAllUpdate();
        }
        private void comboBoxUserSort_rel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListRelUpdate();
        }


        //Обновление
        //списки
        private void UserListAllUpdate()
        {
            list_UserDetail.Items.Clear();
            List<User> list_sort = new List<User>();
            #region Сортировка
            switch (comboBox_userSort.SelectedIndex)
            {
                //Без сортировки
                case 0:
                    list_sort = UserList.Users;
                    break;
                //Ценность группы    
                case 1:
                    list_sort = UserList.ReturnUsersGroupRarenesSortDesc();
                    break;
                //По дате регистрации
                case 2:
                    list_sort = UserList.ReturnUsersRegSortDesc();
                    break;
                //По репутации
                case 3:
                    list_sort = UserList.ReturnUsersRepSortDesc();
                    break;
                //Сообщениям
                case 4:
                    list_sort = UserList.ReturnUsersMessagesSortDesc();
                    break;
                //Адекватности
                case 5:
                    list_sort = UserList.ReturnUsersAdeqSortDesc();
                    break;
                //Раковитости
                case 6:
                    list_sort = UserList.ReturnUsersRakSortDesc();
                    break;
                //Консервативности
                case 7:
                    list_sort = UserList.ReturnUsersConsSortDesc();
                    break;
                case 8:
                    list_sort = UserList.ReturnUsersCreativeSortDesc();
                    break;
                case 9:
                    list_sort = UserList.ReturnUsersScienseSortDesc();
                    break;
                default:
                    list_sort = UserList.Users;
                    break;
            }
            #endregion



            for (int i = 0; i < list_sort.Count; i++)
            {
                //Добавляем в список идентификатор юзера
                ListBoxItem user_item = new ListBoxItem();
                user_item.Content = StackPanUserLists(list_sort[i]);
                if (list_sort[i] == Player.user)
                {
                    user_item.Background = new SolidColorBrush(Colors.LightBlue);
                    player_index = i;
                }
                list_UserDetail.Items.Add(user_item);
            }
            for (int i = 0; i < Player.known_users.Count; i++)
            {
                WrapListUsers.Children.Add(StackPanUser(Player.known_users[i]));
            }
            list_UserDetail.SelectedIndex = selected_index_user;
            SELECTIONUsersUpdate();
        }
        private void UserListRelUpdate()
        {
            if (selected_user != null && relations_generated)
            {
                list_Relations.Items.Clear();
                List<User> list_sort = new List<User>();
                #region Сортировка
                switch (comboBox_SortRel.SelectedIndex)
                {
                    //Без сортировки
                    case 0:
                        list_sort = UserList.Users;
                        break;
                    case 1:
                        list_sort = selected_user.relations.ReturnUsersRelSortDesc();
                        break;
                    default:
                        list_sort = UserList.Users;
                        break;

                }
                #endregion
                for (int i = 0; i < list_sort.Count; i++)
                {
                    //Добавляем в список идентификатор юзера
                    ListBoxItem user_item = new ListBoxItem();
                    user_item.Content = StackPanUserLists(list_sort[i], true);
                    user_item.Background = new SolidColorBrush(selected_user.relations.All[list_sort[i]].color);
                    list_Relations.Items.Add(user_item);
                }
            }
        }
        private void EventtPassedListUpdate()
        {
            list_passedEvents.Items.Clear();
            if (EventsStorage.passed_events.Count == 0)
            {
                list_passedEvents.Items.Add("Пока никаких событий не произошло");
            }
            else
            {
                foreach (Event eventt in EventsStorage.passed_events)
                {
                    list_passedEvents.Items.Add(StackPanEventLists(eventt));
                }
            }


        }
        //Информация
        private void InfoUserUpdate()
        {
            if (tabControlMain.SelectedIndex == 1 && selected_user != null)
            {
                //Вывод аватарки
                if (Nicks.AvaPath.ContainsKey(selected_user.nick))
                {
                    ImageSource ava = Nicks.AvaPath[selected_user.nick];
                    image_Ava.Source = ava;
                }
                else
                {
                    image_Ava.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
                }

                //Установка ника на заглавие тексбокса. Если слишком большой - дату удаляем. Все равно большой - удаляем еще и часть ника
                string header = $"{selected_user.nick}";
                if (selected_user == Player.user)
                    header = $"{selected_user.nick}(Вы)";
                gB_MainInfo.Header = header;
                text_Registration.Text = selected_user.registration.ToShortDateString() + $"({selected_user.m_oldness}й месяц)";
                text_LastActivity.Text = selected_user.last_activity.ToShortDateString() + " " + selected_user.last_activity.ToShortTimeString();

                //Цвет группы юзера и сама группа
                text_Group.Foreground = selected_user.group.need_brush;
                text_Group.Text = ($"{selected_user.group.name}");
                if (selected_user.bans[selected_user.bans.Count - 1].banned)
                    text_Group.Text = ($"Забанен");


                text_RelationPlayer.Foreground = new SolidColorBrush(selected_user.relations.All[Player.user].color);
                text_RelationPlayer.Text = selected_user.relations.All[Player.user].ReturnTextRelation();

                if (selected_user == Player.user)
                    text_RelationPlayer.Text = "Это вы!";

                //Статистика
                text_comments.Text = selected_user.comments.ToString();
                text_news.Text = selected_user.news.ToString();
                text_newsQuality.Text = $"{selected_user.news_quality}%";
                text_messages.Text = selected_user.messages.ToString();
                text_likes.Text = selected_user.likes.ToString();
                text_commentsRate.Text = "+" + selected_user.comments_rate.ToString();
                text_moderChanse.Text = selected_user.moder_chanse.ToString() + "%";

                //Карма
                gB_karma.Header = "Карма " + selected_user.karma.karma.ToString();
                text_karmaActivity.Text = selected_user.karma.kar_activity.ToString();
                text_karmaNews.Text = selected_user.karma.kar_news.ToString();
                text_karmaRep.Text = selected_user.karma.kar_reputation.ToString();

                //Репутация
                text_BaseRep.Text = "| " + selected_user.reputation.Base_reputation.ToString() + " |";
                text_OtrRep.Text = "-" + selected_user.reputation.Otr_reputation.ToString();
                text_PosRep.Text = "+" + selected_user.reputation.Pos_reputation.ToString();

                text_warningsLevel.Text = selected_user.bans[selected_user.bans.Count - 1].warn_sum.ToString();


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

                //Слайдера
                #region Благоразумие
                if (selected_user.character.adeq.unknown && !GlobalParams.Testing)
                {
                    gB_adeq.Header = "Благоразумие(???)";
                    slider_adeq.Value = 0;
                    gB_adeq.ToolTip = "Неизвестно";
                    gB_adeq.Cursor = Cursors.Hand;
                    gB_adeq.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_adeq.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    gB_adeq.Header = "Благоразумие";
                    slider_adeq.Value = selected_user.character.adeq.Param_value;
                    gB_adeq.ToolTip = selected_user.character.adeq.Param_value;
                    gB_adeq.Cursor = Cursors.Arrow;
                    gB_adeq.Foreground = new SolidColorBrush(Colors.Black);
                    gB_adeq.BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                #endregion
                #region Раковитость
                if (selected_user.character.rakness.unknown && !GlobalParams.Testing)
                {
                    gB_rakness.Header = "Раковитость(???)";
                    slider_rakness.Value = 0;
                    gB_rakness.ToolTip = "Неизвестно";
                    gB_rakness.Cursor = Cursors.Hand;
                    gB_rakness.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_rakness.BorderBrush = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    gB_rakness.Header = "Раковитость";
                    slider_rakness.Value = selected_user.character.rakness.Param_value;
                    gB_rakness.ToolTip = selected_user.character.rakness.Param_value;
                    gB_rakness.Cursor = Cursors.Arrow;
                    gB_rakness.Foreground = new SolidColorBrush(Colors.Black);
                    gB_rakness.BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                #endregion

                #region Консервативность
                if (selected_user.character.conservative.unknown && !GlobalParams.Testing)
                {
                    gB_conservative.Header = "Консервативность(???)";
                    slider_conservative.Value = 0;
                    gB_conservative.ToolTip = "Неизвестно";
                    gB_conservative.Cursor = Cursors.Hand;
                    gB_conservative.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_conservative.BorderBrush = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    gB_conservative.Header = "Консервативность";
                    slider_conservative.Value = selected_user.character.conservative.Param_value;
                    gB_conservative.ToolTip = selected_user.character.conservative.Param_value;
                    gB_conservative.Cursor = Cursors.Arrow;
                    gB_conservative.Foreground = new SolidColorBrush(Colors.Black);
                    gB_conservative.BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                #endregion
                #region Толерантность

                if (selected_user.character.tolerance.unknown && !GlobalParams.Testing)
                {
                    gB_tolerance.Header = "Толерантность(???)";
                    slider_tolerance.Value = 0;
                    gB_tolerance.ToolTip = "Неизвестно";
                    gB_tolerance.Cursor = Cursors.Hand;
                    gB_tolerance.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_tolerance.BorderBrush = new SolidColorBrush(Colors.Black);

                }
                else
                {
                    gB_tolerance.Header = "Толерантность";
                    slider_tolerance.Value = selected_user.character.tolerance.Param_value;
                    gB_tolerance.ToolTip = selected_user.character.tolerance.Param_value;
                    gB_tolerance.Cursor = Cursors.Arrow;
                    gB_tolerance.Foreground = new SolidColorBrush(Colors.Black);
                    gB_tolerance.BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                #endregion
                #region Наука
                if (selected_user.character.sciense.unknown && !GlobalParams.Testing)
                {
                    gB_sciense.Header = "Наука(???)";
                    slider_sciense.Value = 0;
                    gB_sciense.ToolTip = "Неизвестно";
                    gB_sciense.Cursor = Cursors.Hand;
                    gB_sciense.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_sciense.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    gB_sciense.Header = "Наука";
                    slider_sciense.Value = selected_user.character.sciense.Param_value;
                    gB_sciense.ToolTip = selected_user.character.sciense.Param_value;
                    gB_sciense.Cursor = Cursors.Arrow;
                    gB_sciense.Foreground = new SolidColorBrush(Colors.Black);
                    gB_sciense.BorderBrush = new SolidColorBrush(Colors.LightGray);

                }
                #endregion
                #region Креативность
                if (selected_user.character.creativity.unknown && !GlobalParams.Testing)
                {
                    gB_creative.Header = "Креативность(???)";
                    slider_creative.Value = 0;
                    gB_creative.ToolTip = "Неизвестно";
                    gB_creative.Cursor = Cursors.Hand;
                    gB_creative.Foreground = new SolidColorBrush(Colors.Gray);
                    gB_creative.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                else
                {
                    gB_creative.Header = "Креативность";
                    slider_creative.Value = selected_user.character.creativity.Param_value;
                    gB_creative.ToolTip = selected_user.character.creativity.Param_value;
                    gB_creative.Cursor = Cursors.Arrow;
                    gB_creative.Foreground = new SolidColorBrush(Colors.Black);
                    gB_creative.BorderBrush = new SolidColorBrush(Colors.LightGray);
                }
                #endregion


                //Текстовое описание
                text_UserDescription.Text = selected_user.description;
                valueUser_log.Text = "";

                //Лог пользователя
                foreach (UserDayLog log in selected_user.last_thirty_Days)
                {
                    valueUser_log.Text = valueUser_log.Text + log.text_descr;
                }
                valueUser_log.ScrollToEnd();

                //Репутация
                list_Traits.Items.Clear();

                //Трейты
                foreach (Trait trait in selected_user.traits)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Template = template;
                    item.Content = trait.short_name;
                    item.ToolTip = trait.full_description;
                    item.Margin = new Thickness(1, 1, 1, 1);
                    item.Background = trait.background_brush;
                    item.Foreground = trait.foreground_brush;
                    item.Cursor = Cursors.Help;
                    list_Traits.Items.Add(item);
                }

                #region Кол-во друзей и т.д.
                text_friendsAmount.Text = selected_user.relations.friends.Count.ToString();
                text_comradesAmount.Text = selected_user.relations.comrades.Count.ToString();
                text_neutralsAmount.Text = selected_user.relations.neutrals.Count.ToString();
                text_unfriendsAmount.Text = selected_user.relations.unfriends.Count.ToString();
                text_enemiesAmount.Text = selected_user.relations.enemies.Count.ToString();
                #endregion


            }


        }
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
            text_RepDiff1.Text = selected_user.reputation.Base_reputation.ToString();

            text_KarmaDiff2.Text = selected_userRelation.karma.karma.ToString();
            text_CommentsDiff2.Text = selected_userRelation.comments.ToString();
            text_RepDiff2.Text = selected_userRelation.reputation.Base_reputation.ToString();
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

            if (selected_user.reputation.Base_reputation > selected_userRelation.reputation.Base_reputation)
            {
                text_RepDiff1.Foreground = new SolidColorBrush(Colors.DarkGreen);
                text_RepDiff2.Foreground = new SolidColorBrush(Colors.DarkRed);
            }
            else if (selected_user.reputation.Base_reputation < selected_userRelation.reputation.Base_reputation)
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
        private void ListView_UsersUpdate()
        {
            listView_Users.Items.Clear();
            foreach (User user in UserList.Users)
            {
                string group = user.group.name;
                if (user.bans[user.bans.Count - 1].banned)
                    group = group + "(забанен)";
                listView_Users.Items.Add(new
                {
                    Nick = user.nick,
                    Registration = user.registration.ToShortDateString(),
                    Group = group,
                    Activity = user.last_activity.ToShortDateString() + " " + user.last_activity.ToShortTimeString(),
                    Comments = user.comments,
                    CommentsRate = user.comments_rate,
                    News = user.news,
                    Messages = user.messages,
                    Likes = user.likes,
                    Karma = user.karma.karma,
                    Reputation = Math.Round(user.reputation.Base_reputation, 1)
                });
            }
        }
        //Характеристики выбранного события
        private void list_passedEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            list_EventChar.Items.Clear();
            if (list_passedEvents.SelectedIndex != -1)
            {
                Event selected_event = EventsStorage.passed_events[list_passedEvents.SelectedIndex];

                foreach (string eventParam in selected_event.text_parametres)
                {
                    ListBoxItem item = new ListBoxItem();
                    item.Template = templateSec;
                    item.Content = eventParam;
                    item.Margin = new Thickness(1, 1, 1, 1);
                    list_EventChar.Items.Add(item);
                }
            }

        }

        //Все остальное
        private void button_clearLog_Click(object sender, RoutedEventArgs e)
        {
            text_log.Text = "";
        }
        //Ссылка на пользователя игрока
        private void status_playerOnline_Click(object sender, RoutedEventArgs e)
        {
            tabControlMain.SelectedIndex = 1;
            list_UserDetail.SelectedIndex = player_index;
        }
        //Логика экспандеров
        private void exp_choose_Collapsed(object sender, RoutedEventArgs e)
        {
            exp_info.IsExpanded = true;
        }
        private void exp_info_Collapsed(object sender, RoutedEventArgs e)
        {
            exp_choose.IsExpanded = true;
        }
        //Быстрое изменение репутации
        private void text_OtrRep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayerActions.actions_quie.Add(new ActionRepChange($"Понижение репутации {selected_user.nick}", ActionsEnum.ReputationDown, 1,
                -Player.user.karma.karma, ReputationReason.ReturnReason(true), selected_user));
            text_statusLog.Text = "Понижение репутации успешно добавлено в очередь действий!";
        }
        private void text_PosRep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PlayerActions.actions_quie.Add(new ActionRepChange($"Повышение репутации {selected_user.nick}", ActionsEnum.ReputationUp, 1,
                Player.user.karma.karma, ReputationReason.ReturnReason(false), selected_user));
            text_statusLog.Text = "Повышение репутации успешно добавлено в очередь действий!";

        }


        //Личный кабинет, предупреждения, репутация
        private void button_privateCab_Click(object sender, RoutedEventArgs e)
        {
            WindowCabinet = new WindowCabinet();
            WindowCabinet.Show();
        }
        private void text_warningsLevel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowWarnings = new WindowWarn(selected_user.nick);
            WindowWarnings.Show();
        }
        private void text_BaseRep_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            WindowReputation = new WindowReputation(selected_user.nick);
            WindowReputation.Show();
        }

        //Тулбар
        //Зайти на сайт с тулбара
        private void text_playerOnline_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Player.enter_rumine == false)
            {
                Activity.online.Add(Player.user);
                Player.enter_rumine = true;
            }
            else
            {
                for (int i = 0; i < Activity.online.Count; i++)
                {
                    if (Activity.online[i] == Player.user)
                    {
                        Activity.online.RemoveAt(i);
                        break;
                    }
                }

                Player.enter_rumine = false;
            }
        }
        //Скрыть левый тулбар
        private void button_leftToolHide_Click(object sender, RoutedEventArgs e)
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
        private void button_TimeGo_Click(object sender, RoutedEventArgs e)
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
            Activity.curr_event = new Event("Ничего особенного", EventsEnum.nothing, null);
            Activity.SetOnlineModifier();
            Activity.SetTimeForUsersForHour();
        }

        //Главный тик таймера
        private void TimeGoTick(object sender, EventArgs e)
        {
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
            if (Player.user.activity)
            {
                log_text = PlayerActions.ActionsProgress();
            }
            else
            {
                PlayerActions.ActionsStop();
            }

            if (Date.current_date.Hour < Date.current_date_prev.Hour)
            {
                Activity.NewDayUpdate();
                HistoricEvent poss_hist_event = HistoricEventsControl.HistoricEventCheck();
                if (poss_hist_event!= null)
                {
                    Activity.curr_hist_event = poss_hist_event;
                }
            }
            if (Date.current_date.Minute % 10 == 0)
            {
                Activity.ActivityDimishing();
            }

            if (Player.user.activity && Date.current_date.Minute % 4 == 0)
            {
                Player.Boredom += 2;
            }
            else if (Date.current_date.Minute % 4 == 0)
            {
                Player.Boredom--;
            }

            if (Date.current_date.Minute < Date.current_date_prev.Minute)
            {
                Activity.SetTimeForUsersForHour();
            }
            if (Date.current_date.Minute < Date.current_date_prev.Minute && Date.current_date.Month != Date.current_date_prev.Month)
            {
                Activity.NewMonthUpdate();
            }
            Activity.TimeMinutePassing();
            PlayerActivity.UpdateActivity();
            UserList.CheckingAllUserForUpdates();
        }

        private void MinuteInterfaceUpdate()
        {
            if (log_text != null && log_text != "")
            {
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": " + log_text;
            }
            if (Date.current_date.Day != Date.current_date_prev.Day && Date.current_date.Minute < Date.current_date_prev.Minute)
            {
                list_messagesMonth.Items.Clear();
                foreach (int num in Activity.messages_Per_month)
                {
                    list_messagesMonth.Items.Add(num);
                }
            }
            if (Date.current_date.Minute < Date.current_date_prev.Minute && Date.current_date.Month != Date.current_date_prev.Month)
            {
                list_messagesYear.Items.Clear();
                foreach (int num in Activity.messages_Per_month)
                {
                    list_messagesYear.Items.Add(num);
                }
            }
            updates += (short)Date.max_minutes_pass;
            StatusUpdate();
            if (Player.user.activity || (updates >= Convert.ToInt16(text_UpdateTimes.Text) && GlobalParams.Testing))
            {
                SELECTIONUsersUpdate();
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

                //Запись в лог
                text_log.Text = "";
                text_log.Text = text_log.Text + (Activity.log);
                text_log.ScrollToEnd();

                if (Activity.curr_event.type != EventsEnum.nothing)
                {
                    StatusTextEvent.Text = $"{Activity.curr_event.name}!";
                }
                else
                {
                    StatusTextEvent.Text = "Ничего";
                }

                StatusTextActivness.Text = Activity.activness;
                text_messagesDay.Text = Activity.messages_per_day.ToString();
                text_messagesPrevMinute.Text = Activity.last_messages.Sum().ToString();

                updates = 0;
            }
            else
            {
                gB_online.Header = $"Сейчас онлайн: {Activity.all_online}, вы оффлайн";
                text_messagesDay.Text = Activity.messages_per_day.ToString();
                text_messagesPrevMinute.Text = Activity.last_messages.Sum().ToString();
            }
            progBar_Boredom.Value = Player.Boredom;
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
        private void button_UserGenerate_Click(object sender, RoutedEventArgs e)
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
            timer_users.Tick += new EventHandler(timerTick);
            timer_users.Interval = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(text_GenerateTick.Text));
            text_GeneratedUsers.Text = UserList.UserAmount.ToString();
            if (users_generated)
            {
                Nicks.NicksInit();
                UserList.Users.Clear();
            }
            timer_users.Start();
        }
        //Результат
        private void timerTick(object sender, EventArgs e)
        {
            if (timer_total_users == timer_generated_users)
            {
                timer_users.Stop();
                text_log.AppendText("\n" + timer_total_users + " юзеров сгенерировано...");
                UserList.GenerateRelations();
                text_log.AppendText("\nОтношения сгенерированы...");
                UserList.ModerChoose();
                relations_generated = true;
                users_generated = true;
                Player.PlayerCreation();
                text_log.AppendText("\nПользователь выбран! Вы " + $"{Player.user.nick}! Уровень доступа {Player.access_level}");
                Player.UserRecognise();
                UserList.FractionChoose();
                UserListAllUpdate();
                ListView_UsersUpdate();
                list_AverageTemperature.Items.Add($"Раковитость: {UserList.aver_rakness}");
                list_AverageTemperature.Items.Add($"Адекватность: {UserList.aver_adeq}");
                list_AverageTemperature.Items.Add($"Консервативность: {UserList.aver_conservative}");
                list_AverageTemperature.Items.Add($"Толерантность: {UserList.aver_tolerance}");
                list_AverageTemperature.Items.Add($"Креативность: {UserList.aver_creativity}");
                list_AverageTemperature.Items.Add($"Наука: {UserList.aver_sciense}");
                list_AverageTemperature.Items.Add($"Объекты отношений: {GlobalParams.relation_obj}");
                WindowAboutYou = new WindowAboutYou();
                WindowAboutYou.Show();
            }

            else
            {
                UserList.GenerateUsers(1);
                text_GeneratedUsers.Text = UserList.UserAmount.ToString();
                timer_generated_users++;
            }
        }






        #endregion

        #region Изучение параметров
        private void gB_adeq_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selected_user.character.adeq.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.adeq));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение адекватности добавлено в очередь действий!";
                gB_adeq.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_adeq.Cursor = Cursors.Arrow;
            }
        }
        private void gB_rakness_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selected_user.character.rakness.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.rakness));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение раковитости добавлено в очередь действий!";
                gB_rakness.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_rakness.Cursor = Cursors.Arrow;
            }
        }
        private void gB_conservative_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selected_user.character.conservative.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.conservative));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение консервативности добавлено в очередь действий!";
                gB_conservative.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_conservative.Cursor = Cursors.Arrow;
            }
        }
        private void gB_creative_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!selected_user.character.creativity.in_progress && selected_user.character.creativity.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.creativity));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение креативности добавлено в очередь действий!";
                gB_creative.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_creative.Cursor = Cursors.Arrow;
            }
        }
        private void gB_sciense_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selected_user.character.sciense.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.sciense));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение науки добавлено в очередь действий!";
                gB_sciense.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_sciense.Cursor = Cursors.Arrow;
            }
        }
        private void gB_tolerance_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (selected_user.character.tolerance.unknown)
            {
                PlayerActions.actions_quie.Add(new DoParamResearch("Изучение параметра",
                    ActionsEnum.DoResearch, 5, selected_user, selected_user.character.tolerance));
                text_statusLog.Text = Date.current_date.ToShortTimeString() + ": изучение толерантности добавлено в очередь действий!";
                gB_tolerance.BorderBrush = new SolidColorBrush(Colors.Gray);
                gB_tolerance.Cursor = Cursors.Arrow;
            }
        }
        #endregion

        #region Вспомогательные методы




        //Генерация стакпанелей
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
                if (Nicks.AvaPath.ContainsKey(user.nick))
                {
                    ava.Source = Nicks.AvaPath[user.nick];
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
                if (Nicks.AvaPath.ContainsKey(user.nick))
                {
                    ava.Source = Nicks.AvaPath[user.nick];
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
        private ListBoxItem StackPanEventLists(Event eventt)
        {
            ListBoxItem item = new ListBoxItem();
            item.Template = templateSec;
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;
            TextBlock name = new TextBlock();
            name.Margin = new Thickness(5, 5, 5, 5);
            name.TextAlignment = TextAlignment.Justify;
            name.Text = eventt.name;
            Image icon = new Image();
            icon.Width = 20;
            icon.Height = 20;
            icon.Margin = new Thickness(0, 5, 5, 5);
            if (eventt.icon != null)
            {
                icon.Source = eventt.icon;
            }
            stackpanel.Children.Add(icon);
            stackpanel.Children.Add(name);
            item.Content = stackpanel;
            item.Margin = new Thickness(1, 1, 1, 1);
            return item;
        }
        private ListBoxItem StackPanUser(User user)
        {
            ListBoxItem item = new ListBoxItem();
            item.Template = templateSec;
            StackPanel SPMain = new StackPanel();
            SPMain.Orientation = Orientation.Horizontal;

            StackPanel SPInfo = new StackPanel();
            SPInfo.Orientation = Orientation.Vertical;
            TextBlock nick = new TextBlock();
            nick.Margin = new Thickness(3, 3, 3, 3);
            nick.FontWeight = text_Group.FontWeight;
            nick.TextAlignment = TextAlignment.Justify;
            nick.FontSize = 12;
            nick.Text = user.ToString();
            SPInfo.Children.Add(nick);
            TextBlock group = new TextBlock();
            group.Margin = new Thickness(3, 3, 3, 3);
            group.TextAlignment = TextAlignment.Justify;
            group.Text = user.group.name.ToString();
            group.FontWeight = text_Group.FontWeight;
            group.Foreground = user.group.need_brush;
            SPInfo.Children.Add(group);

            TextBlock relation = new TextBlock();
            relation.Margin = new Thickness(3, 3, 3, 3);
            relation.TextAlignment = TextAlignment.Justify;
            relation.FontWeight = text_Group.FontWeight;
            switch (Player.user.relations.All[user].relation)
            {
                case RelationsEnum.friend:
                    relation.Foreground = new SolidColorBrush(Colors.LimeGreen);
                    relation.Text = "Друг";
                    break;
                case RelationsEnum.comrade:
                    relation.Foreground = new SolidColorBrush(Colors.Lime);
                    relation.Text = "Товарищ";
                    break;
                case RelationsEnum.neutral:
                    relation.Foreground = new SolidColorBrush(Colors.LightSlateGray);
                    relation.Text = "Нейтрал";
                    break;
                case RelationsEnum.unfriend:
                    relation.Foreground = new SolidColorBrush(Colors.IndianRed);
                    relation.Text = "Неприятель";
                    break;
                case RelationsEnum.enemy:
                    relation.Foreground = new SolidColorBrush(Colors.Red);
                    relation.Text = "Враг";
                    break;
                default:
                    relation.Foreground = new SolidColorBrush(Colors.Black);
                    relation.Text = "Это вы!";
                    break;
            }
            SPInfo.Children.Add(relation);
            TextBlock news = new TextBlock();
            news.Margin = new Thickness(3, 3, 3, 3);
            news.TextAlignment = TextAlignment.Justify;
            news.Text = "Новостей: " + user.news.ToString();
            SPInfo.Children.Add(news);
            TextBlock comments = new TextBlock();
            comments.Margin = new Thickness(3, 3, 3, 3);
            comments.TextAlignment = TextAlignment.Justify;
            comments.Text = "Комментариев: " + user.comments.ToString();
            SPInfo.Children.Add(comments);


            Image ava = new Image();
            ava.Width = 100;
            ava.Height = 100;
            ava.Margin = new Thickness(0, 5, 5, 5);
            if (Nicks.AvaPath.ContainsKey(user.nick))
            {
                ava.Source = Nicks.AvaPath[user.nick];
            }
            else
            {
                ava.Source = new BitmapImage(new Uri("pack://application:,,,/Resources/No_ava.png"));
            }
            SPMain.Children.Add(ava);
            SPMain.Children.Add(SPInfo);
            item.Content = SPMain;
            SPMain.Width = 225;
            item.Margin = new Thickness(1, 1, 1, 1);
            item.Background = new SolidColorBrush(Colors.GhostWhite);
            item.BorderThickness = new Thickness(1, 1, 1, 1);
            item.BorderBrush = new SolidColorBrush(Colors.LightSlateGray);
            item.MouseDown += wrapUser_Click;
            item.MouseDoubleClick += wrapUser_Click;
            return item;
        }

        private void text_statusLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            text_logAll.Text = text_logAll.Text + "\n" + text_statusLog.Text;
        }

        #endregion
    }
}
