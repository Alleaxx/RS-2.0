using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventUsualDay_Preset
    {
        private static Random random = new Random();

        private static User rnd_User;
        private static User rnd_UserAdd;
        private static List<User> rnd_Users = new List<User>();

        public static bool adminPr_newGroup = false;

        //Возвращение обычного события указанного типа
        static public Event returnUsualDayEvent(EventType type)
        {
            UsersRandomisation();
            switch (type)
            {
                case EventType.adminCome:
                    return adminComeEvent();
                case EventType.bigDiskussion:
                    return bigDuskussionEvent();
                case EventType.userLeave:
                    return userLeaveEvent();
                case EventType.userCome:
                    return userReturnEvent();
                case EventType.dayEnd:
                    return dayEndEvent();
                default:
                    return new EventStatChange("", EventType.ban);
            }
        }


        //Приход админа
        static private Event adminComeEvent()
        {
            //Создание события
            Event adminCome = new Event($"Админ в онлайне", EventType.adminCome);
            User MadMoss = UsersControl.UserSearch("MadMoss");
            User south_park = UsersControl.UserSearch("south_park");
            //Кто зашел
            if (AdvRnd.PrsChanse(65))
            {
                adminCome.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, MadMoss.nick));
            }
            else
            {
                adminCome.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, south_park.nick));
            }
            adminCome.Name = $"{adminCome.Creator.Text} в онлайне";
            adminCome.participants.Add(UsersControl.UserSearch(adminCome.Creator.Text), "Зашедший админ");
            adminCome.EventAdd6_Dates(0);

            #region Важность события

            if (AdvRnd.PrsChanse(20))
                adminCome.Importance = EventImportance.usual;
            else if (AdvRnd.PrsChanse(92))
                adminCome.Importance = EventImportance.rare;
            else
                adminCome.Importance = EventImportance.epic;

            #endregion

            //Воплощение события
            int admin_effect = 0;
            UsersControl.UserSearch(adminCome.Creator.Text).last_activity = Date.current_date;
            //Обычный заход, ничего интересного, 80%
            if (AdvRnd.PrsChanse(80))
            {
                adminCome.EventAdd3_Mods(5, 1, 0, 0);
                admin_effect = 1;
            }
            //Админ что-то комментирует, слегка интереснее, небольшой шанс обещания новой группы, 20%
            else
            {
                adminCome.EventAdd3_Mods(10, 2, 0, 0);
                UsersControl.UserSearch(adminCome.Creator.Text).comments++;
                admin_effect = 2;
                //Админ обещает новую группу
                if (AdvRnd.PrsChanse(5))
                    adminPr_newGroup = true;
            }
            //С очень маленьким шансом админ пишет сообщения в ФЧ, историческое событие, с большим шансом новая группа
            if (AdvRnd.PrsChanse(1, 200))
            {
                adminCome.Importance = EventImportance.historical;
                UsersControl.UserSearch(adminCome.Creator.Text).messages += random.Next(10);
                UsersControl.UserSearch(adminCome.Creator.Text).likes += random.Next(30);
                adminCome.EventAdd3_Mods(30, 10, 5, 1);
                admin_effect = 4;
                if (AdvRnd.PrsChanse(75))
                    adminPr_newGroup = true;
            }

            //Информация в интерфейсе
            adminCome.eventSpec_properties.Add(new GuiString("Эффект от онлайна: ", $"{admin_effect}", true));
            if (adminPr_newGroup)
                adminCome.eventSpec_properties.Add(new GuiString("Обещание новой группы: ", $"остается в силе", true));

            #region Объявления
            switch (admin_effect)
            {
                case 1:
                    adminCome.EventAdd7_Description($"В онлайн зашел {adminCome.Creator.Text}. При этом не произошло ничего особо интересного");
                    adminCome.EventAdd7_Description($"{adminCome.Creator.Text} был замечен {adminCome.date}. Судя по всему, он переносил новости на главную и читал ЛС");
                    break;
                case 2:
                    adminCome.EventAdd7_Description($"{adminCome.date} на сайт зашел {adminCome.Creator.Text}, откомментировав парочку новостей. Это дало надежду на то, что админов удастстя разговорить, однако ничего не вышло");
                    adminCome.EventAdd7_Description($"Редкий случай произошел {adminCome.date} - {adminCome.Creator.Text} находился в онлайне подозрительно долго. Что он при этом делал - неясно. Пользователи активно обсуждают это во флудилке");
                    break;
                case 4:
                    adminCome.EventAdd7_Description($"Невероятно! Никто не ожидал этого, однако {adminCome.date} на сайт зашел {adminCome.Creator.Text} и написал в ФЧ несколько постов! Это вызвало небывалой ажиотаж у юзеров, ибо каждый считал своим долгом расспросить его о всяком разном. Народ надолго запомнит этот день");
                    adminCome.EventAdd7_Description($"Редчайшее явление - {adminCome.date} появился в ФЧ {adminCome.Creator.Text}! Такое счастье выпадает нечасто. Пользователи весьма продуктивно пообщались с ним во флудилке");
                    break;


            }
            #endregion
            adminCome.EventEnd_DescrChoose();
            return adminCome;
        }
        //Большая дискуссия
        static private Event bigDuskussionEvent()
        {
            //Создание события
            Event bigDisskusion = new Event($"Большая дискуссия", EventType.bigDiskussion);
            bigDisskusion.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            bigDisskusion.participants.Add(rnd_User, "Создатель обсуждения");
            bigDisskusion.EventAdd6_Dates(0);

            #region Важность события

            if (AdvRnd.PrsChanse(10))
                bigDisskusion.Importance = EventImportance.usual;
            else if (AdvRnd.PrsChanse(90))
                bigDisskusion.Importance = EventImportance.rare;
            else
                bigDisskusion.Importance = EventImportance.epic;

            #endregion

            //Воплощение события
            //Создание дискуссии

            Topic disk_topic = TopicControl.ReturnRndTopic();
            //Жесткость дискуссии
            int disk_violence = random.Next(5);
            string dissk_name = "";
            switch (disk_violence)
            {
                case 0:
                    dissk_name = "Разговор";
                    break;
                case 1:
                    dissk_name = "Дискуссия";
                    break;
                case 2:
                    dissk_name = "Флуд";
                    break;
                case 3:
                    dissk_name = "Спор";
                    break;
                case 4:
                    dissk_name = "Срач";
                    break;
                case 5:
                    dissk_name = "Война";
                    break;

            }

            //Количество людей и масштаб дискуссии
            int people_amount = random.Next(0, rnd_Users.Count);
            string dissk_bignessString = "";
            foreach (User user in rnd_Users)
            {
                user.messages++;
                user.last_activity = Date.current_date;
                if (bigDisskusion.participants.ContainsKey(user) != true)
                    bigDisskusion.participants.Add(user, "Участник дискуссии");
            }
            if (bigDisskusion.participants.Count <= 2 && (disk_violence == 0 || disk_violence == 2 || disk_violence == 3 || disk_violence == 4))
                dissk_bignessString = "Мелкий";
            else if (bigDisskusion.participants.Count <= 2 && (disk_violence == 1 || disk_violence == 5))
                dissk_bignessString = "Мелкая";
            else if (bigDisskusion.participants.Count > 2 && (disk_violence == 0 || disk_violence == 2 || disk_violence == 3 || disk_violence == 4))
                dissk_bignessString = "Типичный";
            else if (bigDisskusion.participants.Count > 2 && (disk_violence == 1 || disk_violence == 5))
                dissk_bignessString = "Типичная";
            else if (bigDisskusion.participants.Count > 5 && (disk_violence == 0 || disk_violence == 2 || disk_violence == 3 || disk_violence == 4))
                dissk_bignessString = "Большой)";
            else if (bigDisskusion.participants.Count > 5 && (disk_violence == 1 || disk_violence == 5))
                dissk_bignessString = "Большая)";


            bigDisskusion.Name = $"{dissk_bignessString} {dissk_name} про {disk_topic.text}";
            bigDisskusion.EventAdd3_Mods(bigDisskusion.participants.Count * 3, bigDisskusion.participants.Count, 0, 0);


            //Информация в интерфейсе
            bigDisskusion.eventSpec_properties.Add(new GuiString("Тема: ", $"{disk_topic.text}", true));
            bigDisskusion.eventSpec_properties.Add(new GuiString("Жесткость: ", $"{disk_violence}", true));
            bigDisskusion.eventSpec_properties.Add(new GuiString("Масштаб: ", $"{bigDisskusion.participants.Count}", true));

            #region Объявления
            bigDisskusion.EventAdd7_Description($"Сегодня, примерно в {Abbrev.date} {rnd_User.nick} вызвал у посетителей ФЧ бурную реакцию, упомянув {disk_topic.text} в не очень приглядном свете!" +
                    $"{rnd_UserAdd.nick} не мог не ответить на это, что повлекло за собой {dissk_bignessString} {dissk_name}. В нем также участвовало еще {bigDisskusion.participants.Count - 1} юзеров. Мы будем следить за развитием событий");

            bigDisskusion.EventAdd7_Description($"День {Abbrev.date} отметился тем, что некий пользователь {rnd_User.nick}, чувствуя себя state,  вызвал обсуждение такого предмета как {disk_topic.text} в своем сообщении." +
        $"К его обсуждению присоединилось еще {bigDisskusion.participants.Count - 1} юзеров, что несколько оживило флудилку. ");

            bigDisskusion.EventAdd7_Description($"{rnd_User.nick}, будучи {rnd_User.traits[rnd_User.traits.Count - 1].name} не мог не сказать про  {disk_topic.text} в своих постах." +
        $"В ответ на это {rnd_UserAdd.traits[rnd_UserAdd.traits.Count - 1].name} {rnd_UserAdd.nick} тоже выразил свою позицию, что привело к такому событию как {dissk_name}, затронув еще {bigDisskusion.participants.Count - 1} пользователей");
            #endregion
            bigDisskusion.EventEnd_DescrChoose();
            return bigDisskusion;
        }

        //Уход пользователя
        static private Event userLeaveEvent()
        {
            //Создание события


            //Определение ушедшего пользователя
            List<User> poss_leavers = UsersControl.ReturnUsersLeaveChanseSortDesc();
            for (int i = 0; i < poss_leavers.Count; i++)
            {
                if (AdvRnd.PrsChanse(poss_leavers[i].character.leaveChanse.Value * 5))
                {
                    rnd_User = poss_leavers[i];
                    break;
                }
            }
            Event leaveUser = new Event($"Уход {rnd_User.nick}", EventType.userLeave);
            leaveUser.EventAdd6_Dates(0);
            #region Важность события

            if (AdvRnd.PrsChanse(10))
                leaveUser.Importance = EventImportance.usual;
            else if (AdvRnd.PrsChanse(30))
                leaveUser.Importance = EventImportance.rare;
            else
                leaveUser.Importance = EventImportance.epic;

            #endregion
            leaveUser.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            leaveUser.participants.Add(rnd_User, "Ушедший пользователь");
            //Отмена активности у ушедшего пользователя, вычисление влияния от его ухода
            rnd_User.activity = false;
            rnd_User.last_activity = Date.current_date;
            int infl = rnd_User.forum_influence / 50;
            if (rnd_User.mod)
            {
                leaveUser.eventSpec_properties.Add(new GuiString("Ушедший является модератором ", $"{infl}", true));
                infl += 5;
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                leaveUser.eventSpec_properties.Add(new GuiString("Ушедший является раком ", $"{infl}", true));
                infl += 5;
            }
            leaveUser.EventAdd3_Mods(infl, infl / 2, 0, 0);

            //Информация в интерфейсе
            leaveUser.eventSpec_properties.Add(new GuiString("Ушедший пользователь: ", $"{rnd_User.nick}", true));
            leaveUser.eventSpec_properties.Add(new GuiString("Влияние от ухода: ", $"{infl}", true));

            #region Объявления
            if (infl <= 5)
            {
                leaveUser.EventAdd7_Description($"Внезапно решил уйти не очень приметный пользователь {rnd_User.nick}. Не сказать, чтобы он был очень известен, но определенную реакцию его уход вызвал");
                leaveUser.EventAdd7_Description($"{Abbrev.date} принял решение об уходе пользователь {rnd_User.nick}. Мало кто это заметил, быть может потому что {rnd_User.nick} никому и не был нужен?");
            }
            else if (infl > 5 && infl < 10)
            {
                leaveUser.EventAdd7_Description($"{Abbrev.date} покинул сайт {rnd_User.nick}. Юзеры активно обсуждают этот поступок, друзья сожалеют, враги ликуют");
                leaveUser.EventAdd7_Description($"{rnd_User.nick} ушел. Жизнь на сайте продолжается, но еще день-другой пользователи, возможно, будут грустить");
            }
            else
            {
                leaveUser.EventAdd7_Description($"{rnd_User.nick} уходит! Флудилка кипит, юзеры орут, как так вышло и почему. Сам юзер сказал, что причина - {TopicControl.topics}");
                leaveUser.EventAdd7_Description($"{Abbrev.date} запомнится как день, когда {rnd_User.nick} в последний раз заходил на сайт. Последним его действием был {Abbrev.diskuss}");
            }
            #endregion

            leaveUser.EventEnd_DescrChoose();
            return leaveUser;
        }
        //Приход пользователя
        static private Event userReturnEvent()
        {
            //Выбор пользователя, который будет приходить
            List<User> poss_comers = new List<User>();
            for (int i = 0; i < UsersControl.all_users.Count; i++)
            {
                if (UsersControl.all_users[i].activity == false)
                    poss_comers.Add(UsersControl.all_users[i]);
            }
            for (int i = 0; i < poss_comers.Count; i++)
            {
                if (AdvRnd.PrsChanse(75))
                {
                    rnd_User = poss_comers[i];
                    break;
                }
            }

            //Создание события
            Event leaveUser = new Event($"{rnd_User.nick} вернулся!", EventType.userCome);
            leaveUser.EventAdd6_Dates(0);
            #region Важность события

            if (AdvRnd.PrsChanse(10))
                leaveUser.Importance = EventImportance.usual;
            else if (AdvRnd.PrsChanse(30))
                leaveUser.Importance = EventImportance.rare;
            else
                leaveUser.Importance = EventImportance.epic;

            #endregion
            leaveUser.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            leaveUser.participants.Add(rnd_User, "Вернувшийся пользователь");

            //Возвращение активности вернувшемуся пользователю и определение влияния
            rnd_User.activity = true;
            rnd_User.last_activity = Date.current_date;
            int infl = rnd_User.forum_influence / 50;
            if (rnd_User.mod)
            {
                leaveUser.eventSpec_properties.Add(new GuiString("Вернувшийся юзер - модератор! ", $"", true));
                infl += 5;
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                leaveUser.eventSpec_properties.Add(new GuiString("Вернувшийся юзер - рак ", $"", true));
                infl += 5;
            }
            leaveUser.EventAdd3_Mods(infl, infl / 2, 0, 0);

            //Информация в интерфейсе
            leaveUser.eventSpec_properties.Add(new GuiString("Пришедший пользователь: ", $"{rnd_User.nick}", true));
            leaveUser.eventSpec_properties.Add(new GuiString("Влияние от возвращения: ", $"{infl}", true));

            #region Объявления
            if (infl <= 5)
            {
                leaveUser.EventAdd7_Description($"На сайт решил вернуться почти забытый {rnd_User.nick}. Больше народу - лучше? Кто знает");
                leaveUser.EventAdd7_Description($"{Abbrev.date} на сайте вновь был замечен {rnd_User.nick}. Мало кто помнит его уход, однако теперь же вот он - здесь, на румайне вновь");
            }
            else if (infl > 5 && infl < 10)
            {
                leaveUser.EventAdd7_Description($"Некогда {rnd_User.nick} покинул сайт, однако сейчас это уже неверно. Пользователь счастливо(или не очень) сидит во флудилке, и все вернулось на круги своя.");
                leaveUser.EventAdd7_Description($"{rnd_User.nick} пришел сюда вновь, потвердив жизненное правило о том, что на румине всегда возвращаются. Добро пожаловать?");
            }
            else
            {
                leaveUser.EventAdd7_Description($"{rnd_User.nick} появлется в онлайне как и раньше! Враги кипят от злости, друзья рады, как так вышло и почему. Сам юзер сказал, что причина возвращения - {TopicControl.topics}");
                leaveUser.EventAdd7_Description($"{Abbrev.date} запомнится как день, когда {rnd_User.nick} вновь стал флудить как и раньше. Первое его действие после возвращения - {Abbrev.diskuss}");
            }
            #endregion

            leaveUser.EventEnd_DescrChoose();
            return leaveUser;
        }

        //Отчет о прошедшем дне
        public static Event dayEndEvent()
        {
            Event dayEnd = new Event($"Отчет за {Date.current_date_prev.ToShortDateString()}", EventType.dayEnd);
            dayEnd.EventAdd1_BasicInfo(new Event_Creator(CreatorType.Rumine, "Румайн"));
            dayEnd.EventAdd6_Dates(0);
            dayEnd.Importance = EventImportance.rare;
            dayEnd.EventAdd3_Mods(random.Next(0), 0, 0, 0);
            dayEnd.eventSpec_properties.Add(new GuiString("Прошедший день", Date.current_date_prev.ToShortDateString(), false, StringProfile.Header));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во событий: ", Activity.day_events.Count.ToString(), true));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во сообщений: ", Activity.day_messages.ToString(), true));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во комментариев: ", Activity.day_comments.ToString(), true));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во новостей: ", Activity.day_news.ToString(), true));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во измененений репутации: ", Activity.day_repChanges.ToString(), true));
            dayEnd.eventSpec_properties.Add(new GuiString("Кол-во попыток забанить: ", Activity.day_bans.ToString(), true));

            dayEnd.EventAdd7_Description("Отчет за прошедший день. Общая статистика по количеству событий и их типу.");
            dayEnd.EventEnd_DescrChoose();
            return dayEnd;
        }


        //Рандомизация пользователей для событий
        private static void UsersRandomisation()
        {
            rnd_Users.Clear();
            rnd_User = UsersControl.act_users[random.Next(UsersControl.act_users.Count)];
            do
            {
                rnd_UserAdd = UsersControl.act_users[random.Next(UsersControl.act_users.Count)];
                //Список рандомных людей для событий. Не повторяют основного рандомного пользователя
                if (rnd_User != rnd_UserAdd)
                    rnd_Users.Add(rnd_UserAdd);
            }
            while (rnd_User == rnd_UserAdd && rnd_Users.Count != 8);
        }

    }
}
