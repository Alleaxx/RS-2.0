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
using System.Windows.Threading;

namespace RumineSimulator_2._0
{
    /// <summary>
    /// Логика взаимодействия для WindowCabinet.xaml
    /// </summary>
    public partial class WindowCabinet : Window
    {
        private DispatcherTimer timer_update = null;
        int selected_index = -1;

        public WindowCabinet()
        {
            InitializeComponent();
            Title = "Личный кабинет пользователя " + Player.user.nick;
            timer_update = new DispatcherTimer();
            timer_update.Tick += new EventHandler(TimerTick);
            timer_update.Interval = new TimeSpan(0, 0, 0, 0, 250);
            InfoUpdate();
            if (Player.user.activity)
            {
                checkBox_Update.IsChecked = true;
                timer_update.IsEnabled = true;
            }
        }

        private void InfoUpdate()
        {
            list_ActionsQue.Items.Clear();
            ListBoxItem item_header = new ListBoxItem();
            item_header.Content = DockPanActionsQueHeader();
            item_header.Background = new SolidColorBrush(Colors.LightGray);
            item_header.BorderThickness = new Thickness(0, 0, 0, 1);
            item_header.BorderBrush = new SolidColorBrush(Colors.Black);
            list_ActionsQue.Items.Add(item_header);
            foreach (Action action in PlayerActions.actions_quie)
            {
                ListBoxItem item = new ListBoxItem();
                item.BorderThickness = new Thickness(0, 1, 0, 0);
                item.BorderBrush = new SolidColorBrush(Colors.Gray);
                item.Content = StackPanActionsQue(action);
                list_ActionsQue.Items.Add(item);
            }
            list_ActionsQue.SelectedIndex = selected_index;
            if(list_ActionsQue.SelectedIndex != -1)
            {
                text_ActionType.Text = PlayerActions.actions_quie.ElementAt(list_ActionsQue.SelectedIndex - 1).short_name;
                text_ActionType.Text = PlayerActions.actions_quie.ElementAt(list_ActionsQue.SelectedIndex - 1).time_min.ToString() + " минут";
            }
            if (Player.user.character.sciense.Param_value < 5)
            {
                checkBox_HTML.IsEnabled = false;
                checkBox_HTML.Content = "HTML(требуется навык науки от 5)";
                checkBox_HTML.Foreground = new SolidColorBrush(Colors.Gray);
            }
            if (!Player.user.traits.Contains(TraitsList.AllTraits[Traits.gnomik]))
            {
                checkBox_ComentJoeks.IsEnabled = false;
                checkBox_ComentJoeks.Content = "Шутки(требуется трейт 'гномик'!)";
                checkBox_ComentJoeks.Foreground = new SolidColorBrush(Colors.Gray);

            }

            text_yourTime.Text = Player.user.Curr_timeForRumine.ToString();
            if(PlayerActions.Curr_action != null)
            {
                text_ActionDateEnd.Text = PlayerActions.Curr_action.end_date.ToShortTimeString();
            }
            slider_newsQuality.Maximum = Player.user.news_quality*2;
            text_maxValueNewsQuality.Text = slider_newsQuality.Maximum.ToString();
            slider_comentQuality.Maximum = (int)(((double)Player.user.comments_rate / (double)Player.user.comments)*100);
            text_maxValueComentsQuality.Text = slider_comentQuality.Maximum.ToString();

            text_achivs.Text = "Ачивки: " + Player.achieve_points;

        }

        private void TimerTick(object sender, EventArgs e)
        {
            InfoUpdate();
        }

        private StackPanel StackPanActionsQue(Action action)
        {
            StackPanel stackpanel = new StackPanel();
            DockPanel dockpanel = new DockPanel();

            ProgressBar progressbar = new ProgressBar();
            progressbar.HorizontalAlignment = HorizontalAlignment.Stretch;
            progressbar.Maximum = action.time_min;
            progressbar.Value = action.time_min - action.time_remain;

            TextBlock action_name = new TextBlock();
            action_name.Width = 200;
            action_name.VerticalAlignment = VerticalAlignment.Center;
            action_name.Margin = new Thickness(2, 5, 12, 5);
            action_name.TextAlignment = TextAlignment.Justify;
            action_name.Text = action.short_name.ToString();

            TextBlock action_time = new TextBlock();
            action_time.Width = 50;
            action_time.VerticalAlignment = VerticalAlignment.Center;
            action_time.Margin = new Thickness(5, 5, 12, 5);
            action_time.TextAlignment = TextAlignment.Justify;
            action_time.Text = action.time_remain.ToString();

            dockpanel.Children.Add(action_name);
            dockpanel.Children.Add(action_time);
            stackpanel.Children.Add(dockpanel);
            stackpanel.Children.Add(progressbar);

            return stackpanel;

        }
        private DockPanel DockPanActionsQueHeader()
        {
            DockPanel dockpanel = new DockPanel();

            TextBlock action_name = new TextBlock();
            action_name.Width = 200;
            action_name.VerticalAlignment = VerticalAlignment.Center;
            action_name.Margin = new Thickness(2, 5, 12, 5);
            action_name.TextAlignment = TextAlignment.Justify;
            action_name.Text = "Действие";

            TextBlock action_time = new TextBlock();
            action_time.Width = 65;
            action_time.VerticalAlignment = VerticalAlignment.Center;
            action_time.Margin = new Thickness(5, 5, 12, 5);
            action_time.TextAlignment = TextAlignment.Justify;
            int sum = 0;
            foreach (Action act in PlayerActions.actions_quie)
            {
                sum += act.time_remain;
            }
            action_time.Text = $"Время({sum})";

            dockpanel.Children.Add(action_name);
            dockpanel.Children.Add(action_time);

            return dockpanel;

        }

        private void button_ActionDelete_Click(object sender, RoutedEventArgs e)
        {
            if(list_ActionsQue.SelectedIndex != -1)
            {
                PlayerActions.actions_quie.RemoveAt(list_ActionsQue.SelectedIndex-1);
                list_ActionsQue.Items.RemoveAt(list_ActionsQue.SelectedIndex);
            }
        }
        private void button_MoveToBegin_Click(object sender, RoutedEventArgs e)
        {
            if (list_ActionsQue.SelectedIndex > 1 && list_ActionsQue.SelectedIndex != -1)
            {
                Action action_up = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1];
                Action action_change = PlayerActions.actions_quie[0];
                Action time = new Action("", ActionsEnum.ReputationDown, 20);
                time = action_change;
                PlayerActions.actions_quie[0] = action_up;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1] = time;
            }
            InfoUpdate();
        }
        private void button_MoveUp_Click(object sender, RoutedEventArgs e)
        {
            if(list_ActionsQue.SelectedIndex > 1 && list_ActionsQue.SelectedIndex != -1)
            {
                Action action_up = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1];
                Action action_change = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 2];
                Action time = new Action("", ActionsEnum.ReputationDown, 20);
                    time = action_change;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 2] = action_up;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1] = time;
            }
            InfoUpdate();
        }
        private void button_MoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (list_ActionsQue.SelectedIndex < list_ActionsQue.Items.Count- 1 && list_ActionsQue.SelectedIndex != -1)
            {
                Action action_up = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1];
                Action action_change = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex];
                Action time = new Action("", ActionsEnum.ReputationDown, 20);
                time = action_change;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex] = action_up;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex] = time;
            }
            InfoUpdate();
        }
        private void button_MoveToEnd_Click(object sender, RoutedEventArgs e)
        {
            if (list_ActionsQue.SelectedIndex < list_ActionsQue.Items.Count - 1 && list_ActionsQue.SelectedIndex != -1)
            {
                Action action_up = PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1];
                Action action_change = PlayerActions.actions_quie[PlayerActions.actions_quie.Count - 1];
                Action time = new Action("", ActionsEnum.ReputationDown, 20);
                time = action_change;
                PlayerActions.actions_quie[PlayerActions.actions_quie.Count - 1] = action_up;
                PlayerActions.actions_quie[list_ActionsQue.SelectedIndex - 1] = time;
            }
            InfoUpdate();
        }

        private void list_ActionsQue_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selected_index = list_ActionsQue.SelectedIndex;
            if (list_ActionsQue.SelectedIndex != -1)
            {
                text_ActionType.Text = PlayerActions.actions_quie.ElementAt(list_ActionsQue.SelectedIndex - 1).short_name;
                text_ActionTime.Text = PlayerActions.actions_quie.ElementAt(list_ActionsQue.SelectedIndex - 1).time_min.ToString() + " минут";
            }
        }

        private void checkBox_Update_MouseDown(object sender, RoutedEventArgs e)
        {
            bool check = (bool)checkBox_Update.IsChecked;
            if (check)
                timer_update.IsEnabled = true;
            else
                timer_update.IsEnabled = false;
        }

        private void button_DoNews_Click(object sender, RoutedEventArgs e)
        {
            PlayerActions.actions_quie.Add(new ActionDoNews("Новость", ActionsEnum.DoNews, 5, (int)slider_newsQuality.Value));
            text_log.Text = "Добавление новости успешно добавлено в очередь действий!";
            InfoUpdate();
        }

        private void button_DoComent_Click(object sender, RoutedEventArgs e)
        {
            PlayerActions.actions_quie.Add(new ActionDoComent("Комментарий", ActionsEnum.DoComent, 5, (int)slider_comentQuality.Value));
            text_log.Text = "Написание комментария успешно добавлено в очередь действий!";
            InfoUpdate();
        }

        private void button_EnterRumine_Click(object sender, RoutedEventArgs e)
        {
            if(Player.enter_rumine == true)
            {
                Activity.online.Add(Player.user);
                button_EnterRumine.Content = "Выйти с румайна";
                Player.enter_rumine = false;
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
                button_EnterRumine.Content = "Зайти на румайн";
                Player.enter_rumine = false;
            }

        }
    }
}
