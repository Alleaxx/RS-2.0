using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventStatChange_Preset
    {
        private static Random random = new Random();

        private static User rnd_User;
        private static User rnd_UserAdd;

        //Управляем выдачей событий изменяющих параметры пользователя
        static public EventStatChange returnStatChangeEvent(EventType type)
        {
            UsersRandomisation();
            switch (type)
            {
                case EventType.message:
                    return newMessageEvent();
                case EventType.comment:
                    return newCommentEvent();
                case EventType.news:
                    return NewsEvent();
                case EventType.reputation:
                    return reputationChange();
                case EventType.ban:
                    return warnsChange();
                default:
                    return new EventStatChange("", EventType.ban);
            }


        }

        //Новое сообщение
        private static EventStatChange newMessageEvent()
        {
            //Создание события
            EventStatChange newMessage = new EventStatChange($"Сообщение {rnd_User.nick}", EventType.message, 1);
            newMessage.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            newMessage.participants.Add(rnd_User, "Автор");
            newMessage.EventAdd3_Mods(random.Next(3), 0, 0, 0);
            newMessage.EventAdd6_Dates(0);
            //Воплощение события
            rnd_User.messages++;
            Activity.curr_day_messages++;
            int likes = 0, chanse = 0;
            chanse = rnd_User.forum_influence / 5;
            if (AdvRnd.PrsChanse(chanse))
            {
                likes = random.Next(3);
                rnd_User.likes += likes;
            }
            //Информация в интерфейсе
            newMessage.eventSpec_properties.Add(new GuiString("Симпатии: ", likes.ToString(), true));

            #region Объявления
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} страдает херней");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} надеется на активность");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} активно флудит");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} шутит шутки");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} думает, что это сообщение никто не повторит");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} сидит во флудилке");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} счастливо общается");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} выглядит добрым и хорошим");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} хочет умереть, о чем оповещает всех");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} хочет попасть в топ");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} хороший парень и поднимает Румайн с колен");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} написал весьма смешную шутеечку и получил {likes} симпатий за нее!");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} надеется узреть хороший срач");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} подготавливает почву для срача");
            newMessage.AddAdvertisment($"{newMessage.date} {rnd_User.nick} ведет активную дискуссию");
            #endregion
            return newMessage;
        }
        //Новый комментарий
        private static EventStatChange newCommentEvent()
        {
            //Создание события
            EventStatChange newComment = new EventStatChange($"Комментарий {rnd_User.nick}", EventType.comment, 1);
            newComment.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            newComment.participants.Add(rnd_User, "Автор");
            newComment.EventAdd3_Mods(random.Next(2), 0, 0, 0);
            newComment.EventAdd6_Dates(0);
            //Воплощение события
            rnd_User.comments++;
            Activity.curr_day_comments++;
            int likes = 0, chanse = 0;
            chanse = rnd_User.forum_influence / 5;
            if (AdvRnd.PrsChanse(chanse))
            {
                likes = random.Next(3);
                rnd_User.likes += likes;
            }
            //Информация в интерфейсе
            newComment.eventSpec_properties.Add(new GuiString("Рейтинг: ", likes.ToString(), true));

            #region Объявления
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} комментирует");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} свалил с флудилки");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} просматривает сайт");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} набивает рейтинги");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} воротит нос от мерзкой новости в бездне");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} проработал хороший комментарий, получив {likes} рейтинга!");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} участвует в сраче в комментах");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} пошел поглядеть что там в новостях");
            newComment.AddAdvertisment($"{newComment.date} {rnd_User.nick} оценивает новости");
            #endregion
            return newComment;
        }
        //Новость
        private static EventStatChange NewsEvent()
        {
            //Создание события
            EventStatChange news = new EventStatChange($"Новость {rnd_User.nick}", EventType.news, 7);
            news.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            news.participants.Add(rnd_User, "Автор");
            news.EventAdd3_Mods(0, 0, 0.01F, 0);
            news.EventAdd6_Dates(0);
            //Воплощение события
            rnd_User.news++;
            Activity.curr_day_news++;
            int quality = random.Next(5, 15) + rnd_User.character.creativity.Value * 5 + rnd_User.character.sciense.Value * 5;
            //Информация в интерфейсе
            news.eventSpec_properties.Add(new GuiString("Качество новости: ", quality.ToString(), false));
            #region Объявления
            news.AddAdvertisment($"{news.date} {rnd_User.nick} сделал новость! Бежим оценивать!");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} корпел над редактором");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} случайно закрыл вкладку с наполовину сделанной новостью");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} считает, что новости не нужны");
            news.AddAdvertisment($"{news.date} {rnd_User.nick}: я новость выложил, оцените?");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} хочет получить журналиста");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} извернулся и сделал качественную на целых {quality}% новость!");
            news.AddAdvertisment($"{news.date} {rnd_User.nick}: Поднимаем сайт вместе со мной");
            news.AddAdvertisment($"{news.date} {rnd_User.nick} рубит кэш и наяривает рекламу в свою статейку");
            #endregion
            return news;
        }
        //Изменение репутации
        private static EventStatChange reputationChange()
        {
            //Создание события
            EventStatChange reputationChange = new EventStatChange($"Изменение репутации {rnd_UserAdd.nick}", EventType.reputation);
            reputationChange.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            reputationChange.participants.Add(rnd_User, "Проставил репутацию");
            reputationChange.participants.Add(rnd_UserAdd, "Пострадавший");
            reputationChange.EventAdd3_Mods(0, 0, 0, 0);
            reputationChange.EventAdd6_Dates(0);

            #region Воплощение события
            Activity.curr_day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            //Пользователи друзья, репутация положительна

            if (rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.comrade ||
                rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.friend)
            {
                karma_result = rnd_User.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                rnd_UserAdd.reputation.ChangeReputation(rnd_User, rnd_User.karma.karma, change_reason);
            }
            //Пользователи враги, ставится минус
            else if (rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.unfriend ||
                rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.enemy)
            {
                karma_result = -rnd_User.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                rnd_UserAdd.reputation.ChangeReputation(rnd_User, -rnd_User.karma.karma, change_reason);
            }
            //Пользователи нейтралы, ставится на рандом
            else
            {
                if (AdvRnd.PrsChanse(75))
                {
                    karma_result = rnd_User.karma.karma;
                    change_reason = ReputationReason.ReturnReason(false);
                    rnd_UserAdd.reputation.ChangeReputation(rnd_User, rnd_User.karma.karma, change_reason);

                }
                else
                {
                    karma_result = -rnd_User.karma.karma;
                    change_reason = ReputationReason.ReturnReason(true);
                    rnd_UserAdd.reputation.ChangeReputation(rnd_User, -rnd_User.karma.karma, change_reason);
                }
            }
            reputationChange.participants[rnd_User] = change_reason;

            #endregion

            //Информация в интерфейсе
            reputationChange.eventSpec_properties.Add(new GuiString("Кто поставил: ", rnd_User.nick, true));
            reputationChange.eventSpec_properties.Add(new GuiString("Кому поставили: ", rnd_UserAdd.nick, true));
            reputationChange.eventSpec_properties.Add(new GuiString("Проставленная карма: ", karma_result.ToString(), true));

            #region Объявления
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick} дал по еплу своей кармой {rnd_UserAdd.nick}");
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick} проняло до того, что он аж репутацию изменил!");
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick}: {karma_result} то что надо {rnd_UserAdd.nick}");
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick} отметил срач вместе с {rnd_UserAdd.nick}");
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick} {rnd_User.traits[random.Next(rnd_User.traits.Count)]},{rnd_UserAdd.nick} {rnd_UserAdd.traits[random.Next(rnd_UserAdd.traits.Count)]}. Война!");
            reputationChange.AddAdvertisment($"{reputationChange.date} {rnd_User.nick} обласкал {rnd_UserAdd.nick}");
            #endregion
            return reputationChange;
        }
        //Выдача предупреждений
        private static EventStatChange warnsChange()
        {
            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UsersControl.Users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            rnd_User = moders[random.Next(moders.Count)];
            do
            {
                rnd_UserAdd = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            }
            while (rnd_User == rnd_UserAdd);

            EventStatChange warnsChange = new EventStatChange($"Срач с модератором", EventType.ban);
            warnsChange.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User,
                rnd_User.nick));
            warnsChange.participants.Add(rnd_User, "Модератор");
            warnsChange.participants.Add(rnd_UserAdd, "Нарушитель");
            warnsChange.EventAdd3_Mods(random.Next(1, 4), 0, 0, 0);
            warnsChange.EventAdd6_Dates(0);
            //Воплощение события
            Activity.curr_day_bans++;
            int chanse = BanCheck(rnd_User, rnd_UserAdd);
            int warns = 0;
            //Приговор
            if (AdvRnd.PrsChanse(chanse))
            {
                rnd_UserAdd.LastBan.AddWarnings(rnd_User, 100, warnsChange.Name, rnd_UserAdd);
            }
            else
            {
                warns = random.Next(0, 10);
                rnd_UserAdd.LastBan.AddWarnings(rnd_User, warns, warnsChange.Name, rnd_UserAdd);
            }
            //Информация в интерфейсе
            warnsChange.eventSpec_properties.Add(new GuiString("Модератор: ", rnd_User.nick, true));
            warnsChange.eventSpec_properties.Add(new GuiString("Жертва: ", rnd_UserAdd.nick, true));
            warnsChange.eventSpec_properties.Add(new GuiString("Предупреждений: ", warns.ToString(), true));
            warnsChange.eventSpec_properties.Add(new GuiString("Забанен?: ", rnd_UserAdd.LastBan.Banned.ToString(), true));
            warnsChange.eventSpec_properties.Add(new GuiString("Шанс бана: ", chanse.ToString(), true));
            #region Объявления
            warnsChange.AddAdvertisment($"{warnsChange.date} {rnd_User.nick} грозно глядит на флудераста {rnd_UserAdd.nick}");
            warnsChange.AddAdvertisment($"{warnsChange.date} Между {rnd_User.nick} и {rnd_UserAdd.nick} бой. Банхаммер выигрывает ");
            warnsChange.AddAdvertisment($"{warnsChange.date} {rnd_User.nick}: {warns} предупрежденек тебе пригодятся, {rnd_UserAdd.nick}");
            warnsChange.AddAdvertisment($"{warnsChange.date} {rnd_User.nick} отметилил {rnd_UserAdd.nick}");
            warnsChange.AddAdvertisment($"{warnsChange.date}{rnd_User.nick} {rnd_User.traits[random.Next(rnd_User.traits.Count)]},{rnd_UserAdd.nick} {rnd_UserAdd.traits[random.Next(rnd_UserAdd.traits.Count)]}. Война!");
            warnsChange.AddAdvertisment($"{warnsChange.date} {rnd_User.nick} считает, что такое состояние бана как {rnd_UserAdd.LastBan.Banned} для {rnd_UserAdd.nick} оправдано на все сто");
            #endregion

            return warnsChange;
        }






        //Рандомизация пользователей для событий
        private static void UsersRandomisation()
        {
            rnd_User = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            do
            {
                rnd_UserAdd = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            }
            while (rnd_User == rnd_UserAdd);
        }
        //Проверка шанса бана
        static int BanCheck(User user_init, User user_goal)
        {
            int chanse = -10;
            //Расчет шансов
            if (user_init.relations.RelationStateReturn(user_goal) == RelationType.comrade ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.friend)
            {
                chanse -= 20;
            }
            else if (user_init.relations.RelationStateReturn(user_goal) == RelationType.unfriend ||
                user_init.relations.RelationStateReturn(user_goal) == RelationType.enemy)
            {
                chanse += 20;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                chanse += 75;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.madguy)))
            {
                chanse += 40;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                chanse -= 10;
            }
            if (user_init.traits.Contains(TraitsList.SearchTrait(TraitsType.tiran)))
            {
                chanse += 60;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.ded)))
            {
                chanse -= 20;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.kindguy)))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.srednefag)))
            {
                chanse -= 10;
            }
            if (user_goal.traits.Contains(TraitsList.SearchTrait(TraitsType.revolutioner)))
            {
                chanse += 25;
            }
            return chanse;
        }


    }


}
