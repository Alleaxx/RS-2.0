using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventStatChange_Preset
    {
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
            EventStatChange newMessage = new EventStatChange($"Сообщение от {rnd_User.nick}", EventType.message);
            newMessage.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            newMessage.participants.Add(rnd_User, "Автор");
            newMessage.EventAdd6_Dates(0);
            string event_descr = $"{Abbrev.date} пользователь {rnd_User.nick} написал сообщение на форуме румине. ";
            int likes = 0, chanse = rnd_User.forum_influence / 5;

            //Редкость события
            if (AdvRnd.PrsChanse(99))
            {
                newMessage.Importance = EventImportance.usual;
                event_descr = event_descr + $"Оно весьма типично и ничем не отличается от сотен таких же. ";
                newMessage.EventAdd3_Mods(AdvRnd.random.Next(3), 0, 0, 0);
                if (AdvRnd.PrsChanse(chanse))
                {
                    likes = AdvRnd.random.Next(1, 4);
                }
            }
            else if (AdvRnd.PrsChanse(85))
            {
                newMessage.Importance = EventImportance.unusual;
                event_descr = event_descr + $"Содержание его было запоминающимся, а потому оно на какое-то время сохранилось в истории. ";
                newMessage.EventAdd3_Mods(AdvRnd.random.Next(2, 7), 1, 0, 0);
                likes = AdvRnd.random.Next(1, 1+rnd_User.forum_influence / 50);
            }
            else if (AdvRnd.PrsChanse(75))
            {
                newMessage.Importance = EventImportance.rare;
                event_descr = event_descr + $"Является на удивление годным постом, который будут цитировать и через много месяцев. ";
                newMessage.EventAdd3_Mods(AdvRnd.random.Next(2, 7), 1, 0, 0);
                likes = AdvRnd.random.Next(2, 2+rnd_User.forum_influence / 20);
            }
            else if (AdvRnd.PrsChanse(75))
            {
                newMessage.Importance = EventImportance.epic;
                event_descr = event_descr + $"Этот пост стал эпохальным событием, на которые впоследствии ссылались целые годы! ";
                newMessage.EventAdd3_Mods(15, 2, 0, 0);
                likes = AdvRnd.random.Next(5, 2+rnd_User.forum_influence / 10);
            }
            else
            {
                newMessage.Importance = EventImportance.historical;
                event_descr = event_descr + $"Этот пост стал эпохальным событием, на которые впоследствии ссылались целые годы! ";
                newMessage.EventAdd3_Mods(30, 10, 2, 1);
                likes = AdvRnd.random.Next(10, 2 + rnd_User.forum_influence / 5);
            }
            //Статистика и факты
            rnd_User.last_activity = Date.current_date;
            rnd_User.messages++;
            Activity.Hour_messages++;
            rnd_User.likes += likes;

            //Добавление фич в зависимости от редкости события
            if (newMessage.Importance == EventImportance.unusual || newMessage.Importance == EventImportance.rare || newMessage.Importance == EventImportance.epic || newMessage.Importance == EventImportance.historical)
                event_descr = event_descr + $"Тема поста - {TopicControl.ReturnRndTopic().text}. ";
            if (newMessage.Importance == EventImportance.rare || newMessage.Importance == EventImportance.epic || newMessage.Importance == EventImportance.historical)
                event_descr = event_descr + $"Такая черта автора как '{rnd_User.traits[AdvRnd.random.Next(rnd_User.traits.Count)].name}' сильно повлияла на содержание ";
            if (newMessage.Importance == EventImportance.epic || newMessage.Importance == EventImportance.historical)
                event_descr = event_descr + $"Многие юзеры охарактеризовали пост как {Abbrev.quality}. ";
            if (newMessage.Importance == EventImportance.historical)
                event_descr = event_descr + $"{Abbrev.messageFeature} ";

            event_descr = event_descr + $"За него автор получил {likes} симпатий. ";

            //Описание
            newMessage.EventAdd7_Description(event_descr);
            newMessage.EventEnd_DescrChoose();
            return newMessage;
        }
        //Новый комментарий
        private static EventStatChange newCommentEvent()
        {
            //Создание события
            EventStatChange newComment = new EventStatChange($"Комментарий {rnd_User.nick}", EventType.comment);
            newComment.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            newComment.participants.Add(rnd_User, "Автор");
            newComment.EventAdd6_Dates(0);

            string event_descr = $"{Abbrev.date} пользователь {rnd_User.nick} прокомментировал новость на сайте. ";
            int likes = 0, chanse = rnd_User.forum_influence / 5;

            //Важность события
            if (AdvRnd.PrsChanse(99))
            {
                newComment.EventAdd3_Mods(AdvRnd.random.Next(2), 0, 0, 0);
                event_descr = event_descr + $"Ни новость, ни комментарий не достойны подробного описания. ";
                newComment.Importance = EventImportance.usual;
                if (AdvRnd.PrsChanse(chanse))
                {
                    likes = AdvRnd.random.Next(1,4);
                }

            }
            else if (AdvRnd.PrsChanse(75))
            {
                newComment.EventAdd3_Mods(AdvRnd.random.Next(2, 6), 1, 0, 0);
                event_descr = event_descr + $"Этот комментарий вызвал определенную реакцию со стороны читателей, запомнивших его автора. ";
                newComment.Importance = EventImportance.unusual;
                likes = AdvRnd.random.Next(1, 1 + rnd_User.forum_influence / 50);
            }
            else if (AdvRnd.PrsChanse(85))
            {
                newComment.EventAdd3_Mods(AdvRnd.random.Next(3, 8), 3, 0, 0);
                event_descr = event_descr + $"Такой комментарий навсегда врезался в память пользователей и забудут его еще очень нескоро. ";
                newComment.Importance = EventImportance.rare;
                likes = AdvRnd.random.Next(2, 2 + rnd_User.forum_influence / 20);
            }
            else
            {
                newComment.EventAdd3_Mods(20, 5, 2, 0);
                event_descr = event_descr + $"Эпичное содержание комментария, кажется, будут помнить и через 10 лет, если, румине конечно выживет. ";
                newComment.Importance = EventImportance.epic;
                likes = AdvRnd.random.Next(5, 2 + rnd_User.forum_influence / 10);
            }

            //Статистика и факты
            rnd_User.last_activity = Date.current_date;
            rnd_User.comments++;
            rnd_User.likes += likes;
            Activity.day_comments++;

            //Добавление фич в зависимости от редкости события
            if (newComment.Importance == EventImportance.unusual || newComment.Importance == EventImportance.rare || newComment.Importance == EventImportance.epic || newComment.Importance == EventImportance.historical)
                event_descr = event_descr + $"Тема комментируемой новости - {TopicControl.ReturnRndTopic().text}. ";
            if (newComment.Importance == EventImportance.rare || newComment.Importance == EventImportance.epic || newComment.Importance == EventImportance.historical)
                event_descr = event_descr + $"Каждый прочитавший его чувствует себя {Abbrev.state}. ";
            if (newComment.Importance == EventImportance.epic || newComment.Importance == EventImportance.historical)
                event_descr = event_descr + $"Многие юзеры охарактеризовали комментарий как {Abbrev.quality}. ";
            if (newComment.Importance == EventImportance.historical)
                event_descr = event_descr + $"{Abbrev.commentFeature} ";

            event_descr = event_descr + $"Комментарий имеет {likes} рейтинга. ";

            //Описание
            newComment.EventAdd7_Description(event_descr);
            newComment.EventEnd_DescrChoose();
            return newComment;
        }
        //Новость
        private static EventStatChange NewsEvent()
        {
            //Создание события
            EventStatChange news = new EventStatChange($"Новость {rnd_User.nick}", EventType.news, 7);
            news.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            news.participants.Add(rnd_User, "Автор");

            news.EventAdd6_Dates(0);

            string event_descr = $"{Abbrev.date} пользователь {rnd_User.nick} создал новость. ";
            int quality = AdvRnd.random.Next(5, 15) + rnd_User.character.creativity.Value * 5 + rnd_User.character.sciense.Value * 5;

            //Редкость события
            if (AdvRnd.PrsChanse(50))
            {
                event_descr = event_descr + $"Такие новости составляют основу всех публикаций сайта. ";
                news.EventAdd3_Mods(0, 0, 0.01F, 0);
                news.Importance = EventImportance.usual;
            }
            else if (AdvRnd.PrsChanse(95))
            {
                event_descr = event_descr + $"Не совсем типичная новость со своей изюминкой, не дотягивающая, однако до подлинных шедевров ";
                news.Importance = EventImportance.unusual;
                news.EventAdd3_Mods(0, 0, 0.05F, 0);
            }
            else if (AdvRnd.PrsChanse(95))
            {
                event_descr = event_descr + $"Примечательное качество этой новости обеспечивает ей целую кучу комментариев и рейтинга(и не всегда положительного!). ";
                news.Importance = EventImportance.rare;
                news.EventAdd3_Mods(0, 0, 0.15F, 0);
            }
            else
            {
                event_descr = event_descr + $"Настойщий шедевр новостестроения, на который очень любят ссылаться форумные завсегдаи. Под такой всегда можно найти свежий комментарий от какого-нибудь залетного юзера. ";
                news.Importance = EventImportance.epic;
                news.EventAdd3_Mods(0, 0, 0.5F, 0);
            }


            //Статистика и факты
            rnd_User.last_activity = Date.current_date;
            rnd_User.news++;
            Activity.day_news++;

            if (quality < 30)
                event_descr = event_descr + $"Качество новости на редкость отвратительно, {quality}/100. ";
            if (quality > 70)
                event_descr = event_descr + $"С точки зрения мастерства новость является подлинным шедевром, {quality}/100. ";
            if (quality >= 30 && quality <= 70)
                event_descr = event_descr + $"Оформление новости выдержано в привычном, без изысков, стиле, {quality}/100. ";

            //Добавление фич в зависимости от редкости события
            if (news.Importance == EventImportance.unusual || news.Importance == EventImportance.rare || news.Importance == EventImportance.epic || news.Importance == EventImportance.historical)
                event_descr = event_descr + $"Тема, затрагиваемая в новости - {TopicControl.ReturnRndTopic().text}. ";
            if (news.Importance == EventImportance.rare || news.Importance == EventImportance.epic || news.Importance == EventImportance.historical)
                event_descr = event_descr + $"Количество комментариев на данный момент составляет {AdvRnd.random.Next(10, 50)} штук. ";
            if (news.Importance == EventImportance.epic || news.Importance == EventImportance.historical)
                event_descr = event_descr + $"Новость попала в топ рейтинга, и на данный момент находится на {AdvRnd.random.Next(1,50)} месте! ";
            if (news.Importance == EventImportance.historical)
                event_descr = event_descr + $"{Abbrev.newsFeature}";

            //бъявления
            news.EventAdd7_Description(event_descr);
            news.EventEnd_DescrChoose();
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
            string event_descr = $"{Abbrev.date} пользователь {rnd_User.nick} изменил репутацию другому обитателю руминя - {rnd_UserAdd.nick}. ";

            //Важность события
            if (AdvRnd.PrsChanse(95))
            {
                reputationChange.Importance = EventImportance.usual;
                event_descr = event_descr + $"Никого кроме владельца измененной репутации это не заинтересовало. ";
            }
            else if (AdvRnd.PrsChanse(85))
            {
                reputationChange.Importance = EventImportance.unusual;
                event_descr = event_descr + $"Каким-то образом это изменение стало известно в ФЧ, что вызвало некоторые обсуждения. ";
            }
            else if (AdvRnd.PrsChanse(80))
            {
                reputationChange.Importance = EventImportance.rare;
                event_descr = event_descr + $"Немало людей запомнили такой поступок со стороны автора изменения. ";
            }
            else
            {
                reputationChange.Importance = EventImportance.epic;
                event_descr = event_descr + $"Это чрезвычайно взбудоражило форумчан, а в ФЧ началось обсуждение этого действия. ";
            }

            #region Воплощение события
            rnd_User.last_activity = Date.current_date;
            Activity.day_repChanges++;
            float karma_result = 0;
            string change_reason = "";
            //Пользователи друзья, репутация положительна

            if (rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.comrade ||
                rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.friend)
            {
                karma_result = rnd_User.karma.karma;
                change_reason = ReputationReason.ReturnReason(false);
                rnd_UserAdd.reputation.ChangeReputation(rnd_User, rnd_User.karma.karma, change_reason);
                event_descr = event_descr + $"Вследствие дружеский отношений {rnd_User.nick} и {rnd_UserAdd.nick} последнему, конечно же, был поставлен плюс размером {karma_result}. ";
            }
            //Пользователи враги, ставится минус
            else if (rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.unfriend ||
                rnd_User.relations.RelationStateReturn(rnd_UserAdd) == RelationType.enemy)
            {
                karma_result = -rnd_User.karma.karma;
                change_reason = ReputationReason.ReturnReason(true);
                rnd_UserAdd.reputation.ChangeReputation(rnd_User, -rnd_User.karma.karma, change_reason);
                event_descr = event_descr + $"{rnd_User.nick}, будучи врагом пользователя {rnd_UserAdd.nick} влепил ему минусец в {karma_result} ";
            }
            //Пользователи нейтралы, ставится на рандом
            else
            {
                if (AdvRnd.PrsChanse(75))
                {
                    karma_result = rnd_User.karma.karma;
                    change_reason = ReputationReason.ReturnReason(false);
                    rnd_UserAdd.reputation.ChangeReputation(rnd_User, rnd_User.karma.karma, change_reason);
                    event_descr = event_descr + $"{rnd_User.nick} и {rnd_UserAdd.nick} не были друзьями, но и врагами тоже, так что репутация второго юзера увеличилась на {karma_result} ";


                }
                else
                {
                    karma_result = -rnd_User.karma.karma;
                    change_reason = ReputationReason.ReturnReason(true);
                    rnd_UserAdd.reputation.ChangeReputation(rnd_User, -rnd_User.karma.karma, change_reason);
                    event_descr = event_descr + $"Действие {rnd_UserAdd.nick} настолько возмутило юзера {rnd_User.nick}, что он отплатил минусом размером {karma_result} в репутацию провинившегося. ";
                }
            }
            reputationChange.participants[rnd_User] = change_reason;

            #endregion

            reputationChange.EventAdd7_Description(event_descr);
            reputationChange.EventEnd_DescrChoose();

            return reputationChange;
        }
        //Выдача предупреждений
        private static EventStatChange warnsChange()
        {
            //Создание события
            List<User> moders = new List<User>();
            foreach (User moder in UsersControl.act_users)
            {
                if (moder.mod)
                    moders.Add(moder);
            }
            rnd_User = moders[AdvRnd.random.Next(moders.Count)];
            do
            {
                rnd_UserAdd = UsersControl.act_users[AdvRnd.random.Next(UsersControl.act_users.Count)];
            }
            while (rnd_User == rnd_UserAdd);

            EventStatChange warnsChange = new EventStatChange($"Разборки {rnd_User.nick}", EventType.ban);
            warnsChange.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User,
                rnd_User.nick));
            warnsChange.participants.Add(rnd_User, "Модератор");
            warnsChange.participants.Add(rnd_UserAdd, "Нарушитель");
            warnsChange.EventAdd3_Mods(5, 1, 0, 0);
            warnsChange.EventAdd6_Dates(0);
            string event_descr = $"У {rnd_User.nick} проблемы! Модератор {rnd_UserAdd.nick} углядел нечто неправомерное в его активности и теперь активно наседает на виновника! ";

            #region Важность события

            if (AdvRnd.PrsChanse(70))
            {
                warnsChange.Importance = EventImportance.usual;
                event_descr = event_descr + $"Впрочем, ничем серьезным это для сайта не грозит. ";
            }
            else if (AdvRnd.PrsChanse(85))
            {
                warnsChange.Importance = EventImportance.rare;
                event_descr = event_descr + $"Вполне возможно, что данное действие еще аукнется в будущем и модератору, и его жертве. ";
            }
            else
            {
                warnsChange.Importance = EventImportance.epic;
                event_descr = event_descr + $"Такое не простят и не забудут. Этот инцидент будут помнить очень долго, и кое-кто еще пожалеет о том, что он сегодня натворил. ";
            }

            #endregion

            //Воплощение события
            rnd_User.last_activity = Date.current_date;
            rnd_UserAdd.last_activity = Date.current_date;
            Activity.day_bans++;
            int chanse = -10;
            #region Расчет шансов
            if (rnd_UserAdd.relations.RelationStateReturn(rnd_User) == RelationType.comrade ||
                rnd_UserAdd.relations.RelationStateReturn(rnd_User) == RelationType.friend)
            {
                chanse -= 20;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Тем не менее модератор и флудер являются друзьями, что снижает вероятность бана. ";

            }
            else if (rnd_UserAdd.relations.RelationStateReturn(rnd_User) == RelationType.unfriend ||
                rnd_UserAdd.relations.RelationStateReturn(rnd_User) == RelationType.enemy)
            {
                chanse += 20;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"К несчастью флудера, модератор являются его врагом, от чего тот довольно потирает ручонки в ожидании свежей крови. ";
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                chanse += 75;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Жертва модератора является раком, что оправдывает бан если не на все 100, то хоть на 75%! ";

            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.madguy)))
            {
                chanse += 40;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Данный случай не является единичным, ведь {rnd_User.nick} тот еще чокнутый паря. И на чуваков с банхаммерами ему всегда не везло. ";
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                chanse -= 10;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Жертва славится своей опрятностью, прилизанностью и примерным поведением. Может и не стоило... ";
            }
            if (rnd_UserAdd.traits.Contains(TraitsList.SearchTrait(TraitsType.tiran)))
            {
                chanse += 60;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Вот это да! А у {rnd_UserAdd.nick} ручки чешутся кого-нибудь забанить! Неприятный поворот событий. ";
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.ded)))
            {
                chanse -= 20;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Для деда {rnd_User.nick} опасное и неприемлемое поведение считается естественным, и это все понимают. ";
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.kindguy)))
            {
                chanse -= 10;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Да и разве можно забанить такую няшку как {rnd_User.nick}? В обычной ситуации он и мухи не обидел бы! ";
            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.srednefag)))
            {
                chanse -= 10;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Пользователь {rnd_User.nick} уже известен среди обитателей ФЧ и имеет несколько защитников. ";

            }
            if (rnd_User.traits.Contains(TraitsList.SearchTrait(TraitsType.revolutioner)))
            {
                chanse += 25;
                if (AdvRnd.PrsChanse(50))
                    event_descr = event_descr + $"Модератору на крючок попалась опасная рыбка! Революционер! Удивительно, если он выкарабкается. ";
            }
            #endregion
            //Приговор
            if (AdvRnd.PrsChanse(chanse))
            {
                rnd_UserAdd.LastBan.AddWarnings(rnd_User, 100, warnsChange.Name, rnd_UserAdd);
                event_descr = event_descr + $"В конце концов {rnd_User.nick} оказался в бане. Что ж, мы будем скучать. ";
            }
            else
            {
                event_descr = event_descr + $"В конце концов флудера {rnd_User.nick} пронесло, и он отделался лишь легким испугом. ";

            }
            //Информация в интерфейсе
            warnsChange.eventSpec_properties.Add(new GuiString("Модератор: ", rnd_User.nick, true));
            warnsChange.eventSpec_properties.Add(new GuiString("Жертва: ", rnd_UserAdd.nick, true));
            warnsChange.eventSpec_properties.Add(new GuiString("Шанс бана: ", chanse.ToString(), true));
            #region Объявления
            warnsChange.EventAdd7_Description(event_descr);
            #endregion
            warnsChange.EventEnd_DescrChoose();
            return warnsChange;
        }






        //Рандомизация пользователей для событий
        private static void UsersRandomisation()
        {
            rnd_User = UsersControl.act_users[AdvRnd.random.Next(UsersControl.act_users.Count)];
            do
            {
                rnd_UserAdd = UsersControl.act_users[AdvRnd.random.Next(UsersControl.act_users.Count)];
            }
            while (rnd_User == rnd_UserAdd);
        }
    }


}
