using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    static class HistoricEvents_List
    {
        static Random random = new Random();
        public static List<HistoricEvent> future_HistoricEvents = new List<HistoricEvent>();
        public static List<HistoricEvent> passed_HistoricEvents = new List<HistoricEvent>();

        #region Общие вставки в описание
        public static string[] intros_good = new string[5]
        {"Слава Румине!","Здравствуй, Румине.","Добро пожаловать.","Доброго здравия, народ.","Привет, Румайн."
        };
        public static string[] intros_bad = new string[5]
        {"Печальные новости...","Господа, всё плохо.","Отвратно!","Возмутительно!","Как такое могло произойти?..."
        };
        public static string[] endings = new string[5]
        {"Слава Румине!","Румайну слава!","Были убраны все сведения порочащие сайт.","Утверждено комитетом СМИ РМ","До скорого."
        };
        #endregion

        #region Вики

        public static string[] wiki_names = new string[5]
        {"Вики по Ру-майнкрафт.ру","Румайн-вики","Румине вики","Wiki Ru-minecraft","Mine.ru Wiki"
        };
        public static string[] wiki_comment_good = new string[7]
        {"Поддерживаю автора, мне нравится идея!(с)","Готов править статьи(с)","Прекрасное начинание(с)",
            "Плюсовал и лайкал","Махал руками за","Великолепно(С)","Будем надеяться на лучшее(с)"
        };
        public static string[] wiki_comment_bad = new string[6]
        {"Ничего не выйдет(с)","Аффтар лох и апазорился(с)","Тьфу(с)","Мертво(с)","Топал ногами и ругался","выпей йаду(с)"
        };

        #endregion


        public static void HistoricEvents_Creation(int premade)
        {
            int rnd_premade = premade;
            List<User> all_users = UsersControl.Users;
            #region Вики по румайну
            DateTime wiki_pre_date = new DateTime(2013, 9, random.Next(1, 30), random.Next(9, 24), random.Next(60), 0);

            //Выбор создателя вики
            User wiki_creator = UsersControl.Users[0];
            string wiki_name = "";
            bool wiki_sucees = true;
            bool wiki_rak = false;
            int wiki_firstDay = random.Next(10, 150);

            HistoricEvent wikiCreation = new HistoricEvent("Создание вики по Ру-майну", EventType.historicWiki, wiki_pre_date);
            //Содержание исторического события
            switch (rnd_premade)
            {
                //Исторический мод
                case 0:
                    wiki_creator = UsersControl.UserSearch("Andrej2001");
                    wiki_name = "Ru.Ru-minecraft вики";

                    wikiCreation.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, wiki_creator.nick));
                    wikiCreation.EventAdd7_Description($"{intros_good[random.Next(intros_good.Length)]} Сегодня была создана вики по нашему замечательному сайту Ru-minecraft.ru! Она имеет название {wiki_name}, создателем является {wiki_creator.nick}. Почти сразу же вики подверглась вандализму со стороны известного senyaiv, но достойным пользователям удалось его остановить. {endings[random.Next(endings.Length)]} ");
                    wikiCreation.EventAdd3_Mods(25, 10, 5, 3);
                    wikiCreation.EventAdd4_Participants(wiki_creator, "Создал настоящую, официальную вики Ру-майна. Честь и хвала");
                    wikiCreation.EventAdd4_Participants(UsersControl.UserSearch("senyaiv"), "Вандал первого дня. Поехавший, впрочем оно и неудивительно");
                    wikiCreation.EventAdd4_Participants(UsersControl.UserSearch("naswai"), "Поддерживал, наполнял и создавал");
                    wikiCreation.EventAdd4_Participants(UsersControl.UserSearch("Allexx"), "Сражался за главную страницу вики с Сеней");
                    break;

                //Полностью рандомный мод
                case 1:
                    //Создатель
                    List<User> users = UsersControl.ReturnUsersForumInfluenceDesc();
                    users = UsersControl.ReturnUserByTraits(users, new List<TraitsType>() { TraitsType.programmer, TraitsType.accurateguy }, false);
                    wiki_creator = users[random.Next(users.Count)];

                    wiki_name = wiki_names[random.Next(wiki_names.Length)];
                    wikiCreation.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, wiki_creator.nick));
                    wikiCreation.EventAdd7_Description($"{intros_good[random.Next(intros_good.Length)]} Сегодня была создана вики описывающая наш Румине! Она имеет название {wiki_name}, создателем является {wiki_creator.nick}. Надеемся на то, что вики будет жить и процветать. {endings[random.Next(endings.Length)]}");
                    wikiCreation.EventAdd3_Mods(25, 10, 5, 5);
                    wikiCreation.EventAdd4_Participants(wiki_creator, wiki_creator.traits[random.Next(wiki_creator.traits.Count)].name);
                    //Комментарии на вики
                    foreach (User user in all_users)
                    {
                        if (AdvRnd.PrsChanse(15) && user != wiki_creator)
                        {
                            if (wiki_creator.relations.RelationStateReturn(user) == RelationType.comrade ||
                                wiki_creator.relations.RelationStateReturn(user) == RelationType.neutral ||
                                wiki_creator.relations.RelationStateReturn(user) == RelationType.friend)
                            {
                                if (random.Next(3) == 1)
                                    wikiCreation.EventAdd4_Participants(user, user.traits[random.Next(user.traits.Count)].name);
                                else
                                    wikiCreation.EventAdd4_Participants(user, wiki_comment_good[random.Next(wiki_comment_good.Length)]);
                            }
                            else
                            {
                                if (random.Next(3) == 1)
                                    wikiCreation.EventAdd4_Participants(user, user.traits[random.Next(user.traits.Count)].name);
                                else
                                    wikiCreation.EventAdd4_Participants(user, wiki_comment_bad[random.Next(wiki_comment_bad.Length)]);
                            }
                        }
                    }
                    //Установка прочих параметров
                    if (wiki_creator.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
                        wiki_rak = true;
                    break;

            }

            wikiCreation.EventAdd5_ImageSource(new BitmapImage(new Uri("pack://application:,,,/Resources/logo_bluemoshka.png")));
            wikiCreation.EventAdd6_Dates(0);

            //Интерфейс
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Вики Румайна: ", "", false, StringProfile.Header));
            wikiCreation.more_properties.Add("Создатель вики: ", wiki_creator.nick);
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Создатель вики: ", wiki_creator.nick, false));
            wikiCreation.more_properties.Add("Название: ", wiki_name);
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Название: ", wiki_name, false));
            wikiCreation.more_properties.Add("Правок в первый день: ", wiki_firstDay.ToString());
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Правок в первый день: ", wiki_firstDay.ToString(), false));
            wikiCreation.more_properties.Add("Успех при создании: ", wiki_sucees.ToString());
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Успех при создании: ", wiki_sucees.ToString(), false));
            wikiCreation.more_properties.Add("Рако-вики: ", wiki_rak.ToString());
            wikiCreation.InterfaceInfo.Add_EventProperty(new GuiString("Рако-вики: ", wiki_rak.ToString(), false));

            future_HistoricEvents.Add(wikiCreation);

            #endregion
            #region Нашествие пане
            DateTime paneAttack_pre_date = new DateTime(2013, 7, 20, random.Next(9, 24), random.Next(60), 0);

            //Выбор создателя вики
            User paneAttack_creator = UsersControl.Users[0];
            string paneAttack_name = "Нашествие пане";

            HistoricEvent paneAttack = new HistoricEvent(paneAttack_name, EventType.historicPaneAttack, paneAttack_pre_date);
            //Содержание исторического события

            //Создатели
            Fraction pane_fraction = FractionList.SearchFraction("Броняши");
            pane_fraction.active = true;
            Fraction antiPane_fraction = new Fraction("Антиброни", new List<TraitsType> { }, new List<TraitsType> { TraitsType.pane },
                FractionAggression.ambigious, true,false,true);
            foreach (User user in all_users)
            {
                if (antiPane_fraction.MemberAcceptCheck(user) && AdvRnd.PrsChanse(15))
                {
                    antiPane_fraction.members.Add(user);
                }
            }
            antiPane_fraction.FractionCreationEnd();

            FractionList.AllFractions.Add(antiPane_fraction);

            paneAttack.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Rumine, "Война фракций"));
            paneAttack.EventAdd7_Description($"{intros_good[random.Next(intros_good.Length)]} Было официально объявлено нашествие брони на румине! Все поклонники сериала MLP({pane_fraction.members.Count} юзеров) объединились и вовсю продвигают свою идеологию! Не всем это нравится, и на румине появляется сопротивление. Что же из этого выйдет? {endings[random.Next(endings.Length)]}");
            paneAttack.EventAdd3_Mods(15, 15, 5, 0);
            //Комментарии на вики
            foreach (User user in pane_fraction.members)
            {
                if (AdvRnd.PrsChanse(15))
                    paneAttack.EventAdd4_Participants(user, "За пане!");
            }
            foreach (User user in antiPane_fraction.members)
            {
                if (AdvRnd.PrsChanse(15))
                    paneAttack.EventAdd4_Participants(user, "Сжечь ересь!");
            }
            //Установка прочих параметров

            paneAttack.EventAdd5_ImageSource(new BitmapImage(new Uri("pack://application:,,,/Resources/logo_bluemoshka.png")));
            paneAttack.EventAdd6_Dates(0);

            //Интерфейс
            paneAttack.InterfaceInfo.Add_EventProperty(new GuiString("Нашествие пане: ", "", false, StringProfile.Header));
            paneAttack.more_properties.Add("Группировка брони: ", pane_fraction.name);
            paneAttack.InterfaceInfo.Add_EventProperty(new GuiString("Группировка брони: ", pane_fraction.name, false));
            paneAttack.more_properties.Add("Влияние брони: ", pane_fraction.Influence.ToString());
            paneAttack.InterfaceInfo.Add_EventProperty(new GuiString("Влияние брони: ", pane_fraction.Influence.ToString(), false));
            paneAttack.more_properties.Add("Группировка сопротивления: ", antiPane_fraction.name.ToString());
            paneAttack.InterfaceInfo.Add_EventProperty(new GuiString("Группировка сопротивления: ", antiPane_fraction.name.ToString(), false));
            paneAttack.more_properties.Add("Влияние сопротивления: ", antiPane_fraction.Influence.ToString());
            paneAttack.InterfaceInfo.Add_EventProperty(new GuiString("Влияние сопротивления: ", antiPane_fraction.Influence.ToString(), false));

            future_HistoricEvents.Add(paneAttack);
            #endregion

        }



        public static HistoricEvent EventCheck()
        {
            for (int i = 0; i < future_HistoricEvents.Count; i++)
            {
                if (future_HistoricEvents[i].CheckEvent())
                {
                    HistoricEvent h_event = future_HistoricEvents[i];
                    passed_HistoricEvents.Add(h_event);
                    future_HistoricEvents.RemoveAt(i);
                    return h_event;
                }

            }
            return null;
        }
    }
}
