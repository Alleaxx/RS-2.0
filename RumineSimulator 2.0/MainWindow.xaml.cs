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
            list_Relations.SelectedIndex = 1;
            Date.InitDate(new DateTime(2011, 07, 27), new DateTime(2013, 07, 19, 11, 59, 0));
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
            exp_info.IsExpanded = false;
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
                    selected_user_beta = selected_user;
                    UserBetaUpdate();
                }

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
            for (int i = 0; i < UserList.Users.Count; i++)
            {
                list_Testing.Items.Add(Interface_Value_Return(UserList.Users[i].InterfaceInfo.interface_basic));
            }
        }
        //Выбор пользователя(бета-версия)
        private void WrapUser_Click(object sender, RoutedEventArgs e)
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
            text_Group1.Text = selected_user_beta.group.Name;
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
            //text_Relation1.Foreground = new SolidColorBrush(selected_user_beta.relations.All[Player.user].color);
            //text_Relation1.Text = selected_user_beta.relations.All[Player.user].ReturnTextRelation();


            //Репутация-карма
            button_ReputationBase1.Content = $"({selected_user_beta.reputation.Base_reputation.ToString()})";
            button_ReputationMinus.Content = $"(-{selected_user_beta.reputation.Otr_reputation.ToString()})";
            button_ReputationPlus.Content = $"(+{selected_user_beta.reputation.Pos_reputation.ToString()})";
            text_KarmaBase.Text = $"Карма: {selected_user_beta.karma.karma.ToString()} ({selected_user_beta.karma.kar_news}Н + {selected_user_beta.karma.kar_reputation}Р + {selected_user_beta.karma.kar_activity}А)";

            //Сайто-форумная информация
            text_Messages.Text = "Сообщений: " + selected_user_beta.messages;
            text_Likes.Text = "Симпатий: " + selected_user_beta.likes;
            text_Warnings.Text = "Предупреждений: " + selected_user_beta.LastBan.Warn_sum;

            text_Comments.Text = "Комментариев: " + selected_user_beta.comments;
            text_CommentsRate.Text = "Рейтинг К.: +" + selected_user_beta.comments_rate;
            text_News.Text = "Новости: " + selected_user_beta.news + " (" + selected_user_beta.news_quality + "%)";
            text_Influence.Text = "Влияние: " + selected_user_beta.forum_influence;

            //Фракции
            text_MainFraction.Text = selected_user_beta.main_fraction.name;
            text_MainFractionMember.Text = "Участник";
            if (selected_user_beta.main_fraction.leader == selected_user_beta)
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
                ListBoxItem item = new ListBoxItem()
                {
                    Content = trait.short_name,
                    ToolTip = trait.full_description,
                    Margin = new Thickness(1, 1, 1, 1),
                    Background = trait.background_brush,
                    Foreground = trait.foreground_brush,
                    Cursor = Cursors.Help
                };
                if (trait.type == TraitType.character)
                {
                    list_TraitsChar.Items.Add(item);
                }
                else
                {
                    list_TraitsStat.Items.Add(item);
                }
            }
            #endregion

            Text_UsersBeta_UserDescription.Text = UserDescription.GetTextDescription(selected_user_beta);

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


        //Спрятать описание пользователей
        private void CheckBox_DescrHide_Click(object sender, RoutedEventArgs e)
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

        private void CheckBox_Testing_Click(object sender, RoutedEventArgs e)
        {

            DP_warningsLevel.Visibility = Visibility.Visible;
            DP_warningsLevel.Height = 21;
            gB_Description.Visibility = Visibility.Visible;
            button_AdminPanel.Visibility = Visibility.Visible;
            button_AdminPanel.Height = 27;
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
        private void List_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void Button_UpdateAll_Click(object sender, RoutedEventArgs e)
        {
            if (tabControlMain.SelectedIndex == 1)
            {
                UserListAllUpdate();
                InfoUserUpdate();
            }
        }
        private void ExpanderListUser_rel_Expanded(object sender, RoutedEventArgs e)
        {
            UserListRelUpdate();
        }
        //Перерисовка списков из-за сортировки
        private void ComboBoxUserSort_all_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListAllUpdate();
        }
        private void comboBoxUserSort_rel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserListRelUpdate();
        }


        //Обновление списки
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
                ListBoxItem user_item = new ListBoxItem()
                {
                    Content = StackPanUserLists(list_sort[i])
                };
                list_UserDetail.Items.Add(user_item);
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
                    ListBoxItem user_item = new ListBoxItem()
                    {
                        Content = StackPanUserLists(list_sort[i], true),
                        Background = new SolidColorBrush(selected_user.relations.All[list_sort[i]].color)
                    };
                    list_Relations.Items.Add(user_item);
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
                gB_MainInfo.Header = header;
                text_Registration.Text = selected_user.registration.ToShortDateString() + $"({selected_user.m_oldness}й месяц)";
                text_LastActivity.Text = selected_user.last_activity.ToShortDateString() + " " + selected_user.last_activity.ToShortTimeString();

                //Цвет группы юзера и сама группа
                text_Group.Foreground = selected_user.group.need_brush;
                text_Group.Text = ($"{selected_user.group.Name}");
                if (selected_user.bans[selected_user.bans.Count - 1].Banned)
                    text_Group.Text = ($"Забанен");




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

                text_warningsLevel.Text = selected_user.bans[selected_user.bans.Count - 1].Warn_sum.ToString();


                //Логические переменные

                checkBox_Mod.IsChecked = selected_user.mod;

                //Слайдера
                #region Благоразумие

                gB_adeq.Header = "Благоразумие";
                slider_adeq.Value = selected_user.character.adeq.Param_value;
                gB_adeq.ToolTip = selected_user.character.adeq.Param_value;
                gB_adeq.Cursor = Cursors.Arrow;
                gB_adeq.Foreground = new SolidColorBrush(Colors.Black);
                gB_adeq.BorderBrush = new SolidColorBrush(Colors.LightGray);
                #endregion
                #region Раковитость
                gB_rakness.Header = "Раковитость";
                slider_rakness.Value = selected_user.character.rakness.Param_value;
                gB_rakness.ToolTip = selected_user.character.rakness.Param_value;
                gB_rakness.Cursor = Cursors.Arrow;
                gB_rakness.Foreground = new SolidColorBrush(Colors.Black);
                gB_rakness.BorderBrush = new SolidColorBrush(Colors.LightGray);

                #endregion

                #region Консервативность

                gB_conservative.Header = "Консервативность";
                slider_conservative.Value = selected_user.character.conservative.Param_value;
                gB_conservative.ToolTip = selected_user.character.conservative.Param_value;
                gB_conservative.Cursor = Cursors.Arrow;
                gB_conservative.Foreground = new SolidColorBrush(Colors.Black);
                gB_conservative.BorderBrush = new SolidColorBrush(Colors.LightGray);

                #endregion
                #region Толерантность

                gB_tolerance.Header = "Толерантность";
                slider_tolerance.Value = selected_user.character.tolerance.Param_value;
                gB_tolerance.ToolTip = selected_user.character.tolerance.Param_value;
                gB_tolerance.Cursor = Cursors.Arrow;
                gB_tolerance.Foreground = new SolidColorBrush(Colors.Black);
                gB_tolerance.BorderBrush = new SolidColorBrush(Colors.LightGray);
                #endregion
                #region Наука

                gB_sciense.Header = "Наука";
                slider_sciense.Value = selected_user.character.sciense.Param_value;
                gB_sciense.ToolTip = selected_user.character.sciense.Param_value;
                gB_sciense.Cursor = Cursors.Arrow;
                gB_sciense.Foreground = new SolidColorBrush(Colors.Black);
                gB_sciense.BorderBrush = new SolidColorBrush(Colors.LightGray);


                #endregion
                #region Креативность

                gB_creative.Header = "Креативность";
                slider_creative.Value = selected_user.character.creativity.Param_value;
                gB_creative.ToolTip = selected_user.character.creativity.Param_value;
                gB_creative.Cursor = Cursors.Arrow;
                gB_creative.Foreground = new SolidColorBrush(Colors.Black);
                gB_creative.BorderBrush = new SolidColorBrush(Colors.LightGray);

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


                    TextBlock txt = new TextBlock();
                    txt.Text = trait.short_name;
                    item.Content = txt;
                    item.IsHitTestVisible = false;
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
            text_GroupDiff2.Text = selected_userRelation.group.Name;
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
        //Характеристики выбранного события
        private void List_passedEvents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

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
            UserList.CheckingAllUserForUpdates();
        }

        private void MinuteInterfaceUpdate()
        {
            //Изменение модификаторa активности
            List_Main_ActivityProperties.Items.Clear();
            InterfaceView_Activity info = Activity.InterfaceInfo;
            foreach (Interface_String inter_string in info.act_properties)
            {
                List_Main_ActivityProperties.Items.Add(Interface_Value_Return(inter_string));
            }
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
            text_GeneratedUsers.Text = UserList.UserAmount.ToString();
            if (users_generated)
            {
                Nicks.NicksInit();
                UserList.Users.Clear();
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
                UserList.GenerateRelations();
                text_log.AppendText("\nОтношения сгенерированы...");
                UserList.ModerChoose();
                relations_generated = true;
                users_generated = true;
                UserList.FractionChoose();
                UserListAllUpdate();
                Activity.Activity_Init();
                text_log.AppendText("\nАктивность инициализирована...");
                list_AverageTemperature.Items.Add($"Раковитость: {UserList.aver_rakness}");
                list_AverageTemperature.Items.Add($"Адекватность: {UserList.aver_adeq}");
                list_AverageTemperature.Items.Add($"Консервативность: {UserList.aver_conservative}");
                list_AverageTemperature.Items.Add($"Толерантность: {UserList.aver_tolerance}");
                list_AverageTemperature.Items.Add($"Креативность: {UserList.aver_creativity}");
                list_AverageTemperature.Items.Add($"Наука: {UserList.aver_sciense}");
                list_AverageTemperature.Items.Add($"Объекты отношений: {GlobalParams.relation_obj}");
            }

            else
            {
                UserList.GenerateUsers(1);
                text_GeneratedUsers.Text = UserList.UserAmount.ToString();
                timer_generated_users++;
            }
        }






        #endregion


        #region Вспомогательные методы


        //Базовое представление интерфейса - строка
        private ListBoxItem Interface_Value_Return(Interface_String interface_info)
        {
            ListBoxItem item = new ListBoxItem();
            StackPanel stackpanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            TextBlock text_value = new TextBlock()
            {
                Margin = new Thickness(2, 2, 2, 1),
                TextAlignment = TextAlignment.Justify,
                Text = interface_info.Text_value,
                FontSize = interface_info.Text_size,
            };
            TextBlock text_value_value = new TextBlock()
            {
                Margin = new Thickness(2, 0, 2, 2),
                TextAlignment = TextAlignment.Justify,
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
            item.Foreground = interface_info.foreground_brush;
            item.Background = interface_info.background_brush;
            item.ToolTip = interface_info.Tooltip;
            return item;
        }


        //Генерация стакпанелей
        private StackPanel StackPanUserLists(User user, bool rel = false)
        {
            #region Главный список
            if (!rel)
            {
                StackPanel stackpanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                TextBlock nick = new TextBlock()
                {
                    Margin = new Thickness(5, 5, 5, 5),
                    TextAlignment = TextAlignment.Justify,
                    Text = user.ToString(),
                    Foreground = user.group.need_brush
                };
                Image ava = new Image()
                {
                    Width = 30,
                    Height = 30,
                    Margin = new Thickness(0, 5, 5, 5)
                };
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
                StackPanel stackpanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
                };
                TextBlock nick = new TextBlock()
                {
                    Margin = new Thickness(5, 5, 5, 5),
                    TextAlignment = TextAlignment.Justify,
                    Text = user.ToString()
                };
                Image ava = new Image()
                {
                    Width = 30,
                    Height = 30,
                    Margin = new Thickness(0, 5, 5, 5)
                };
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
        private ListBoxItem StackPanUser(User user)
        {
            ListBoxItem item = new ListBoxItem()
            {

            };
            StackPanel SPMain = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };
            StackPanel SPInfo = new StackPanel()
            {
                Orientation = Orientation.Vertical
            };
            TextBlock nick = new TextBlock()
            {
                Margin = new Thickness(3, 3, 3, 3),
                FontWeight = text_Group.FontWeight,
                TextAlignment = TextAlignment.Justify,
                FontSize = 12,
                Text = user.ToString()
            };
            SPInfo.Children.Add(nick);
            TextBlock group = new TextBlock()
            {
                Margin = new Thickness(3, 3, 3, 3),
                TextAlignment = TextAlignment.Justify,
                Text = user.group.Name.ToString(),
                FontWeight = text_Group.FontWeight,
                Foreground = user.group.need_brush
            };
            SPInfo.Children.Add(group);

            TextBlock relation = new TextBlock()
            {
                Margin = new Thickness(3, 3, 3, 3),
                TextAlignment = TextAlignment.Justify,
                FontWeight = text_Group.FontWeight
            };
            //switch (Player.user.relations.All[user].relation)
            //{
            //    case RelationsEnum.friend:
            //        relation.Foreground = new SolidColorBrush(Colors.LimeGreen);
            //        relation.Text = "Друг";
            //        break;
            //    case RelationsEnum.comrade:
            //        relation.Foreground = new SolidColorBrush(Colors.Lime);
            //        relation.Text = "Товарищ";
            //        break;
            //    case RelationsEnum.neutral:
            //        relation.Foreground = new SolidColorBrush(Colors.LightSlateGray);
            //        relation.Text = "Нейтрал";
            //        break;
            //    case RelationsEnum.unfriend:
            //        relation.Foreground = new SolidColorBrush(Colors.IndianRed);
            //        relation.Text = "Неприятель";
            //        break;
            //    case RelationsEnum.enemy:
            //        relation.Foreground = new SolidColorBrush(Colors.Red);
            //        relation.Text = "Враг";
            //        break;
            //    default:
            //        relation.Foreground = new SolidColorBrush(Colors.Black);
            //        relation.Text = "Это вы!";
            //        break;
            //}
            SPInfo.Children.Add(relation);
            TextBlock news = new TextBlock()
            {
                Margin = new Thickness(3, 3, 3, 3),
                TextAlignment = TextAlignment.Justify,
                Text = "Новостей: " + user.news.ToString()
            };
            SPInfo.Children.Add(news);
            TextBlock comments = new TextBlock()
            {
                Margin = new Thickness(3, 3, 3, 3),
                TextAlignment = TextAlignment.Justify,
                Text = "Комментариев: " + user.comments.ToString()
            };
            SPInfo.Children.Add(comments);


            Image ava = new Image()
            {
                Width = 100,
                Height = 100,
                Margin = new Thickness(0, 5, 5, 5)
            };
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
            item.MouseDown += WrapUser_Click;
            item.MouseDoubleClick += WrapUser_Click;
            return item;
        }

        private void Text_statusLog_TextChanged(object sender, TextChangedEventArgs e)
        {
            text_logAll.Text = text_logAll.Text + "\n" + text_statusLog.Text;
        }

        #endregion
    }
}
