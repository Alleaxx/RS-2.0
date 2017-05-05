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
    /// Логика взаимодействия для Warnings.xaml
    /// </summary>
    public partial class WindowWarn : Window
    {



        User selected_user;
        Ban last_ban;
        public WindowWarn(string nick)
        {
            //Инициализация переменных      
            InitializeComponent();
            selected_user = UserList.UserSearch(nick);
            last_ban = selected_user.bans[selected_user.bans.Count - 1];
            last_ban.warnings.Reverse();

            //Занесение информации
            this.Title = $"Журнал предупреждений пользователя: {selected_user.nick}";
            WarningsUpdate();
        }

        private void button_WarnsUp_Click(object sender, RoutedEventArgs e)
        {
            if (selected_user.LastBan.warn_sum + Convert.ToInt32(text_warnsAmount.Text) > 100)
            {
                PlayerActions.actions_quie.Add(new ActionWarnsChange($"Повышение предупреждений {selected_user.nick}", ActionsEnum.WarningsUp, 1,
                    100 - selected_user.LastBan.warn_sum, text_reason.Text, selected_user));
            }
            PlayerActions.actions_quie.Add(new ActionWarnsChange($"Повышение предупреждений {selected_user.nick}", ActionsEnum.WarningsUp, 1,
                                Convert.ToInt32(text_warnsAmount.Text), text_reason.Text, selected_user)); WarningsUpdate();
            label_warnwarn.Text = "Изменение уровня предупреждений добавлено в очередь действий!";
        }

        private void button_WarnsDown_Click(object sender, RoutedEventArgs e)
        {
            if (selected_user.LastBan.warn_sum - Convert.ToInt32(text_warnsAmount.Text) < 0)
            {
                PlayerActions.actions_quie.Add(new ActionWarnsChange($"Понижение предупреждений {selected_user.nick}", ActionsEnum.WarningsDown, 1,
                                -selected_user.LastBan.warn_sum, text_reason.Text, selected_user));
            }
            PlayerActions.actions_quie.Add(new ActionWarnsChange($"Понижение предупреждений {selected_user.nick}", ActionsEnum.WarningsDown, 1,
                            -Convert.ToInt32(text_warnsAmount.Text), text_reason.Text, selected_user)); WarningsUpdate();
            label_warnwarn.Text = "Изменение уровня предупреждений добавлено в очередь действий!";
        }

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Перерисовывание списка и полное обновление
        private void WarningsUpdate()
        {
            listBox_warnings.Items.Clear();
            last_ban = selected_user.bans[selected_user.bans.Count - 1];
            last_ban.warnings.Reverse();

            //Занесение информации
            text_currLevel.Text = selected_user.LastBan.warn_sum.ToString();

            //Заголовок
            ListBoxItem item_header = new ListBoxItem();
            item_header.Background = new SolidColorBrush(Colors.LightGray);
            item_header.Content = StackWarningsHeader();
            listBox_warnings.Items.Add(item_header);

            foreach (Warning warn in selected_user.LastBan.warnings)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = StackWarnings(warn);
                item.BorderThickness = new Thickness(0, 1, 0, 0);
                item.BorderBrush = new SolidColorBrush(Colors.Gray);
                listBox_warnings.Items.Add(item);
            }
            ListBoxItem l_item = (ListBoxItem)listBox_warnings.Items[listBox_warnings.Items.Count - 1];
            l_item.BorderThickness = new Thickness(0, 1, 0, 1);

            if (selected_user.LastBan.warnings.Count == 0)
                listBox_warnings.Items.Add("На данный момент у пользователя нету предупреждений.");
        }

        #region Генерация панелей
        private StackPanel StackWarnings(Warning warning)
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            TextBlock author = new TextBlock();
            author.Margin = new Thickness(2, 5, 12, 5);
            author.TextAlignment = TextAlignment.Justify;
            author.Width = 105;
            author.Text = warning.author.ToString();

            TextBlock Reason = new TextBlock();
            Reason.TextAlignment = TextAlignment.Center;
            Reason.TextWrapping = TextWrapping.Wrap;
            Reason.Margin = new Thickness(15, 5, 5, 5);
            Reason.TextAlignment = TextAlignment.Justify;
            Reason.Width = 125;
            Reason.Text = warning.reason;

            TextBlock Value = new TextBlock();
            Value.Margin = new Thickness(2, 5, 5, 5);
            Value.TextAlignment = TextAlignment.Justify;
            Value.Text = warning.amount.ToString();
            Value.Width = 50;
            if (warning.amount >= 0)
                Value.Foreground = new SolidColorBrush(Colors.Red);
            else
                Value.Foreground = new SolidColorBrush(Colors.Green);

            TextBlock Date = new TextBlock();
            Date.Margin = new Thickness(5, 5, 12, 5);
            Date.TextAlignment = TextAlignment.Justify;
            Date.Width = 175;
            Date.Text = warning.date.ToShortDateString() + " " + warning.date.ToShortTimeString();

            stackpanel.Children.Add(author);
            stackpanel.Children.Add(Reason);
            stackpanel.Children.Add(Value);
            stackpanel.Children.Add(Date);
            return stackpanel;

        }
        private StackPanel StackWarningsHeader()
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            TextBlock author = new TextBlock();
            author.Width = 105;
            author.Margin = new Thickness(2, 5, 12, 5);
            author.TextAlignment = TextAlignment.Justify;
            author.Text = "Модератор";

            TextBlock Reason = new TextBlock();
            Reason.Width = 125;
            Reason.TextAlignment = TextAlignment.Center;
            Reason.TextWrapping = TextWrapping.Wrap;
            Reason.Margin = new Thickness(15, 5, 5, 5);
            Reason.TextAlignment = TextAlignment.Justify;
            Reason.Text = "Причина";

            TextBlock Value = new TextBlock();
            Value.Width = 50;
            Value.Margin = new Thickness(2, 5, 5, 5);
            Value.TextAlignment = TextAlignment.Justify;
            Value.Text = "Баллы";

            TextBlock Date = new TextBlock();
            Date.Width = 175;
            Date.Margin = new Thickness(5, 5, 12, 5);
            Date.TextAlignment = TextAlignment.Justify;
            Date.Text = "Предупреждение выдано";

            stackpanel.Children.Add(author);
            stackpanel.Children.Add(Reason);
            stackpanel.Children.Add(Value);
            stackpanel.Children.Add(Date);
            return stackpanel;

        }
        #endregion
    }
}
