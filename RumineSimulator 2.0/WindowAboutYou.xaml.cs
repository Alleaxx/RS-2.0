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
    /// Логика взаимодействия для WindowAboutYou.xaml
    /// </summary>
    public partial class WindowAboutYou : Window
    {
        public WindowAboutYou()
        {
            InitializeComponent();
            this.Title = $"Вам выпал пользователь {Player.user.nick}!";
            list_Description.SelectedIndex = 1;
        }

        private void list_Description_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            text_coment.Text = "";
            switch (list_Description.SelectedIndex)
            {
                case -1:
                    break;
                case 0:
                    break;
                #region Олдфажность
                case 1:
                    label_paramName1.Text = "Регистрация";
                    text_paramValue1.Text = Player.user.registration.ToShortDateString();
                    label_paramName2.Text = "Олдфажность";
                    text_paramValue2.Text = $"{Player.user.m_oldness}й месяц";
                    label_paramName3.Text = "Ньюфажнее...";
                    int more_nufag = 0;
                    foreach(User user in UserList.Users)
                    {
                        if (user.m_oldness > Player.user.m_oldness)
                            more_nufag++;
                    }
                    text_paramValue3.Text = $"{more_nufag} пользователей";
                    if (Player.user.m_oldness > 0)
                        text_coment.Text = "Вы полный ньюфаг! Впрочем, может все не так уж и плохо";
                    if (Player.user.m_oldness > 4)
                        text_coment.Text = "Вы уже не такой уж и ньюфаг, что-то на сайте вы знаете. Но и среднефагом пока еще не прозваться.";
                    if (Player.user.m_oldness > 8)
                        text_coment.Text = "Вы начинающий среднефаг в рассвете сил";
                    if (Player.user.m_oldness > 12)
                        text_coment.Text = "Вы просидели на сайте целый год! И что вы только делали все это время? Так или иначе вы весьма хорошо знаете румине";
                    if (Player.user.m_oldness > 18)
                        text_coment.Text = "Вы вполне уверенная в себе личность, имеющая право называть себя олдфагом";
                    if (Player.user.m_oldness > 24)
                        text_coment.Text = "Да. Вы - олдфаг.";
                    break;
                #endregion

                #region Группа
                case 2:
                    label_paramName1.Text = "Группа:";
                    text_paramValue1.Text = Player.user.group.name;
                    label_paramName2.Text = "";
                    label_paramName3.Text = "";
                    text_paramValue2.Text = "";
                    text_paramValue3.Text = "";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.Poset])
                        text_coment.Text = "Вы самый обычный, ничем не примечательный посетитель! Не повезло";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.JGP])
                        text_coment.Text = "По крайней мере у вас есть ПХЛ! Не так уж и плохо, ведь он лучшая группа на сайте по мнению 325 пользователей";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.XXXLJGP])
                        text_coment.Text = "В нынешние времена очень редкая группа! С цветом конечно беда, ну да что поделать";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.Journalist])
                        text_coment.Text = "Вы журналист! Небось новости любите? Как же вы до жизни такой докатились? Хотя права очень даже ничего";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.ModeratorBezdna])
                        text_coment.Text = "Вы - чистильщик толчков, или модератор бездны! Что ж, неплохо";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.Oldfag])
                        text_coment.Text = "Вы дед! Старый, пердящий, добротный";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.JournalistOldfag])
                        text_coment.Text = "Прокачанный дедуля с журналистскими правами! Весьма понтово";
                    if (Player.user.group == GroupsList.Groups[GroupsEnum.Moderator])
                        text_coment.Text = "Вы модератор! Очень жесткий паря, карающий всех и вся.";
                    break;
                #endregion

                default:
                    break;
            }
        }
    }
}
