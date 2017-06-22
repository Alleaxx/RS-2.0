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
        public static string[] wiki_comment_good = new string[5]
        {"Поддерживаю создателя, эта идея мне по нраву(с)","Готов править статьи(с)","Прекрасное начинание(с)","Плюсовал и лайкал","Махал руками за"
        };
        public static string[] wiki_comment_bad = new string[5]
        {"Ничего не выйдет(с)","Создатель ничего не смыслит в подобных делах(с)","Тьфу(с)","Мертво(с)","Топал ногами и ругался"
        };

        #endregion


        public static void HistoricEvents_Creation(int premade)
        {

            #region Вики по румайну
            int rnd_premade = premade;
            DateTime pre_date = new DateTime(2013, 7, 20, random.Next(9, 24), random.Next(60), 0);

            //Выбор создателя вики
            User creator = UserList.Users[0];
            string wiki_name = "";
            bool sucees = true;
            bool rak = false;
            int firstDay = random.Next(10, 150);

            HistoricEvent wikiCreation = new HistoricEvent("Создание вики по Ру-майну", EventType.historicWiki, pre_date);
            //Содержание исторического события
            switch (rnd_premade)
            {
                //Исторический мод
                case 0:
                    creator = UserList.UserSearch("Andrej2001");
                    wiki_name = "Ru.Ru-minecraft вики";

                    wikiCreation.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, creator.nick), "Создание вики Ру-майна");
                    wikiCreation.EventAdd2_Description($"{intros_good[random.Next(intros_good.Length)]} Сегодня была создана вики по нашему замечательному сайту Ru-minecraft.ru! Она имеет название {wiki_name}, создателем является {creator.nick}. Почти сразу же вики подверглась вандализму со стороны известного senyaiv, но достойным пользователям удалось его остановить. {endings[random.Next(endings.Length)]} ");
                    wikiCreation.EventAdd3_Mods(25, 10, 5, 3, 100);
                    wikiCreation.EventAdd4_Participants(creator, "Создал настоящую, официальную вики Ру-майна. Честь и хвала");
                    wikiCreation.EventAdd4_Participants(UserList.UserSearch("senyaiv"), "Вандал первого дня. Поехавший, впрочем оно и неудивительно");
                    wikiCreation.EventAdd4_Participants(UserList.UserSearch("naswai"), "Поддерживал, наполнял и создавал");
                    wikiCreation.EventAdd4_Participants(UserList.UserSearch("Allexx"), "Сражался за главную страницу вики с Сеней");
                    break;

                //Полностью рандомный мод
                case 1:
                    //Создатель
                    List<User> users = UserList.ReturnUsersForumInfluenceDesc();
                    users = UserList.ReturnUserByTraits(users, new List<Traits>() { Traits.programmer, Traits.accurateguy }, false);
                    creator = users[random.Next(users.Count)];

                    wiki_name = wiki_names[random.Next(wiki_names.Length)];
                    wikiCreation.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, creator.nick), "Создание вики Ру-майна");
                    wikiCreation.EventAdd2_Description($"{intros_good[random.Next(intros_good.Length)]} Сегодня была создана вики описывающая наш Румине! Она имеет название {wiki_name}, создателем является {creator.nick}. Надеемся на то, что вики будет жить и процветать. {endings[random.Next(endings.Length)]}");
                    wikiCreation.EventAdd3_Mods(25, 10, 5, 5, 1000);
                    wikiCreation.EventAdd4_Participants(creator, creator.traits[random.Next(creator.traits.Count)].short_name);
                    //Комментарии на вики
                    List<User> all_users = UserList.Users;
                    foreach (User user in all_users)
                    {
                        if (AdvRandom.PersentChanseBool(15) && user != creator)
                        {
                            if (creator.relations.All[user].relation == RelationsEnum.comrade ||
                                creator.relations.All[user].relation == RelationsEnum.neutral ||
                                creator.relations.All[user].relation == RelationsEnum.friend)
                            {
                                if(random.Next(3) == 1)
                                    wikiCreation.EventAdd4_Participants(user, user.traits[random.Next(creator.traits.Count)].short_name);
                                else
                                    wikiCreation.EventAdd4_Participants(user, wiki_comment_good[random.Next(wiki_comment_good.Length)]);
                            }
                            else
                            {
                                if (random.Next(3) == 1)
                                    wikiCreation.EventAdd4_Participants(user, user.traits[random.Next(creator.traits.Count)].short_name);
                                else
                                    wikiCreation.EventAdd4_Participants(user, wiki_comment_bad[random.Next(wiki_comment_bad.Length)]);
                            }
                        }
                    }
                    //Установка прочих параметров
                    if (creator.traits.Contains(TraitsList.AllTraits[Traits.rak]))
                        rak = true;
                    break;

            }

            wikiCreation.EventAdd5_ImageSource(new BitmapImage(new Uri("pack://application:,,,/Resources/logo_bluemoshka.png")));
            wikiCreation.EventAdd6_Dates(0);

            //Интерфейс
            wikiCreation.EventAddEnd_InterfaceInfo();
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Вики Румайна: ", "", false, true));
            wikiCreation.more_properties.Add("Создатель вики: ", creator.nick);
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Создатель вики: ", creator.nick, false));
            wikiCreation.more_properties.Add("Название: ", wiki_name);
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Название: ", wiki_name, false));
            wikiCreation.more_properties.Add("Правок в первый день: ", firstDay.ToString());
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Правок в первый день: ", firstDay.ToString(), false));
            wikiCreation.more_properties.Add("Успех при создании: ", sucees.ToString());
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Успех при создании: ", sucees.ToString(), false));
            wikiCreation.more_properties.Add("Рако-вики: ", rak.ToString());
            wikiCreation.InterfaceInfo.Add_Property(new Interface_String("Рако-вики: ", rak.ToString(), false));

            future_HistoricEvents.Add(wikiCreation);
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
