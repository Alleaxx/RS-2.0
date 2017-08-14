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
    /// Логика взаимодействия для WindowReputation.xaml
    /// </summary>
    public partial class WindowReputation : Window
    {
        User selected_user;

        public WindowReputation(string nick)
        {
            InitializeComponent();
            selected_user = UsersControl.UserSearch(nick);
            InformationUpdate();

        }

        private void InformationUpdate()
        {
            List<ReputationHistory> RepHistory = selected_user.reputation.ReturnRepHistorySort();
            list_ReputationHistory.Items.Clear();

            this.Title = $"Репутация пользователя {selected_user.nick}";
            text_userReputationPos.Text = $"(+{selected_user.reputation.Pos_reputation})";
            text_userReputationOtr.Text = $"(-{selected_user.reputation.Otr_reputation})";
            text_userReputation.Text = $"({selected_user.reputation.Base_reputation})";

            ListBoxItem item_header = new ListBoxItem();
            item_header.Content = StackReputationHeader();
            item_header.Background = new SolidColorBrush(Colors.LightGray);
            item_header.BorderThickness = new Thickness(0, 0, 0, 1);
            item_header.BorderBrush = new SolidColorBrush(Colors.Black);
            list_ReputationHistory.Items.Add(item_header);
            foreach (ReputationHistory history in RepHistory)
            {
                ListBoxItem item = new ListBoxItem();
                item.BorderThickness = new Thickness(0, 1, 0, 0);
                item.BorderBrush = new SolidColorBrush(Colors.Gray);
                item.Content = StackPanHistoryRep(history);
                list_ReputationHistory.Items.Add(item);
            }
            ListBoxItem last_item = (ListBoxItem)list_ReputationHistory.Items[list_ReputationHistory.Items.Count - 1];
            last_item.BorderThickness = new Thickness(0, 1, 0, 1);
        }

        private StackPanel StackReputationHeader()
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            TextBlock author = new TextBlock();
            author.Width = 100;
            author.Margin = new Thickness(2, 5, 12, 5);
            author.TextAlignment = TextAlignment.Justify;
            author.Text = "Автор";

            TextBlock Date = new TextBlock();
            Date.Width = 100;
            Date.Margin = new Thickness(5, 5, 12, 5);
            Date.TextAlignment = TextAlignment.Justify;
            Date.Text = "Дата";

            TextBlock Value = new TextBlock();
            Value.Width = 75;

            Value.Margin = new Thickness(2, 5, 5, 5);
            Value.TextAlignment = TextAlignment.Justify;
            Value.Text = "Отношение";

            TextBlock Reason = new TextBlock();
            Reason.Width = 200;
            Reason.TextAlignment = TextAlignment.Center;
            Reason.TextWrapping = TextWrapping.Wrap;
            Reason.Margin = new Thickness(5, 5, 5, 5);
            Reason.TextAlignment = TextAlignment.Justify;
            Reason.Text = "Комментарий";

            stackpanel.Children.Add(author);
            stackpanel.Children.Add(Date);
            stackpanel.Children.Add(Value);
            stackpanel.Children.Add(Reason);
            return stackpanel;

        }
        private StackPanel StackPanHistoryRep(ReputationHistory history)
        {
            StackPanel stackpanel = new StackPanel();
            stackpanel.Orientation = Orientation.Horizontal;

            TextBlock author = new TextBlock();
            author.Width = 100;
            author.VerticalAlignment = VerticalAlignment.Center;
            author.Margin = new Thickness(2, 5, 12, 5);
            author.TextAlignment = TextAlignment.Justify;
            author.Text = history.author.nick;

            TextBlock Date = new TextBlock();
            Date.Width = 100;
            Date.VerticalAlignment = VerticalAlignment.Center;
            Date.Margin = new Thickness(5, 5, 12, 5);
            Date.TextAlignment = TextAlignment.Justify;
            Date.Text = history.date.ToShortDateString() + " " + history.date.ToShortTimeString();

            TextBlock Value = new TextBlock();
            Value.Width = 75;
            Value.VerticalAlignment = VerticalAlignment.Center;
            Value.Margin = new Thickness(2, 5, 5, 5);
            Value.TextAlignment = TextAlignment.Justify;
            Value.Text = "(" + history.set_karma.ToString() + ")";

            TextBlock Reason = new TextBlock();
            Reason.Width = 200;
            Reason.VerticalAlignment = VerticalAlignment.Center;
            Reason.TextWrapping = TextWrapping.WrapWithOverflow;
            Reason.Margin = new Thickness(0, 5, 5, 5);
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

        private void button_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
