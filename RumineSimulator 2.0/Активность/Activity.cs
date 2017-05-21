using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Activity
    {
        static Random random = new Random();

        static public float online_modifier { get; private set; }
        static public float end_online_modifier { get; private set; }
        static public float real_online_modifier { get; private set; }

        static public List<User> online = new List<User>();
        static public List<User> full_online = new List<User>();
        static public List<User> inactive_online = new List<User>();

        static public List<int> last_messages = new List<int>();

        static public int prev_hour_online { get; private set; }
        static public int recomend_online { get; private set; }
        static public int all_online { get; private set; }

        static public string log { get; private set; }
        static public List<int> messages_log = new List<int>();
        static public User last_author;
        static public string activness { get; private set; }
        static public Event curr_event;
        static public Event curr_hist_event;
        static public List<Reason> last_reasons = new List<Reason>();

        static public int messages_per_day { get; private set; }
        static public List<int> messages_Per_month = new List<int>();
        static public List<int> messages_Per_year = new List<int>();

        //Установка модификатора часа
        public static void SetOnlineModifier()
        {
            switch (Date.current_date.Hour)
            {
                case 0:
                    online_modifier = 0.3F;
                    end_online_modifier = 0.08F;
                    real_online_modifier = 0.15F;
                    break;
                case 1:
                    online_modifier = 0.08F;
                    end_online_modifier = 0.06F;
                    real_online_modifier = 0.1F;
                    break;
                case 2:
                    online_modifier = 0.06F;
                    end_online_modifier = 0.04F;
                    real_online_modifier = 0.08F;
                    break;
                case 3:
                    online_modifier = 0.04F;
                    end_online_modifier = 0.03F;
                    real_online_modifier = 0.06F;
                    break;
                case 4:
                    online_modifier = 0.03F;
                    end_online_modifier = 0.04F;
                    real_online_modifier = 0.04F;
                    break;
                case 5:
                    online_modifier = 0.04F;
                    end_online_modifier = 0.08F;
                    real_online_modifier = 0.07F;
                    break;
                case 6:
                    online_modifier = 0.08F;
                    end_online_modifier = 0.1F;
                    real_online_modifier = 0.09F;
                    break;
                case 7:
                    online_modifier = 0.1F;
                    end_online_modifier = 0.12F;
                    real_online_modifier = 0.12F;
                    break;
                case 8:
                    online_modifier = 0.12F;
                    end_online_modifier = 0.13F;
                    real_online_modifier = 0.18F;
                    break;
                case 9:
                    online_modifier = 0.13F;
                    end_online_modifier = 0.15F;
                    real_online_modifier = 0.2F;
                    break;
                case 10:
                    online_modifier = 0.15F;
                    end_online_modifier = 0.17F;
                    real_online_modifier = 0.25F;
                    break;
                case 11:
                    online_modifier = 0.17F;
                    end_online_modifier = 0.2F;
                    real_online_modifier = 0.25F;
                    break;
                case 12:
                    online_modifier = 0.2F;
                    end_online_modifier = 0.23F;
                    real_online_modifier = 0.3F;
                    break;
                case 13:
                    online_modifier = 0.23F;
                    end_online_modifier = 0.25F;
                    real_online_modifier = 0.35F;
                    break;
                case 14:
                    online_modifier = 0.25F;
                    end_online_modifier = 0.3F;
                    real_online_modifier = 0.35F;
                    break;
                case 15:
                    online_modifier = 0.3F;
                    end_online_modifier = 0.3F;
                    real_online_modifier = 0.4F;
                    break;
                case 16:
                    online_modifier = 0.3F;
                    end_online_modifier = 0.33F;
                    real_online_modifier = 0.45F;
                    break;
                case 17:
                    online_modifier = 0.33F;
                    end_online_modifier = 0.34F;
                    real_online_modifier = 0.5F;
                    break;
                case 18:
                    online_modifier = 0.34F;
                    end_online_modifier = 0.4F;
                    real_online_modifier = 0.6F;
                    break;
                case 19:
                    online_modifier = 0.4F;
                    end_online_modifier = 0.5F;
                    real_online_modifier = 0.7F;
                    break;
                case 20:
                    online_modifier = 0.5F;
                    end_online_modifier = 0.6F;
                    real_online_modifier = 0.6F;
                    break;
                case 21:
                    online_modifier = 0.6F;
                    end_online_modifier = 0.5F;
                    real_online_modifier = 0.5F;
                    break;
                case 22:
                    online_modifier = 0.5F;
                    end_online_modifier = 0.4F;
                    real_online_modifier = 0.35F;
                    break;
                case 23:
                    online_modifier = 0.4F;
                    end_online_modifier = 0.3F;
                    real_online_modifier = 0.2F;
                    break;
            }
        }

        //Эффект "плавности"
        public static void ActivityDimishing()
        {
            if (online_modifier < end_online_modifier)
                online_modifier += 0.01F;
            else if (online_modifier > end_online_modifier)
                online_modifier -= 0.01F;
        }

        private static void HourSetFullOnline()
        {
            
        }

        //Проверяем онлайн самого игрока
        private static void PlayerOnline()
        {
        }

        //Полный онлайн - ВСЕ юзеры, у которых еще не кончилось время на румайн
        private static List<User> TechFullOnlineCreation()
        {
            return new List<User>();
        }
        //Обновление пользователей у которых есть время заходить на румайн, но которые в данный момент не активны
        private static void TechInactiveOnlineUpdate()
        {
            inactive_online.Clear();
            foreach (User user in full_online)
            {
                if (online.Contains(user) == false)
                    inactive_online.Add(user);
            }
        }

        //Текстовое представление активности
        private static void SetActivness()
        {
            if (messages_log.Count == 60)
            {
                last_messages = messages_log.GetRange(39, 20);
                if (last_messages.Sum() < 5)
                {
                    activness = "Пустыня";
                }
                else if (last_messages.Sum() >= 5 && last_messages.Sum() < 10)
                {
                    activness = "Тухлячок";
                }
                else if (last_messages.Sum() >= 10 && last_messages.Sum() < 20)
                {
                    activness = "Вялотекущий разговор";
                }
                else if (last_messages.Sum() >= 20 && last_messages.Sum() < 35)
                {
                    activness = "Бодрое обсуждение";
                }
                else if (last_messages.Sum() >= 435)
                {
                    activness = "Аншлаг";
                }
            }
            else
            {
                activness = "Нечто";
            }
        }


        public static void TimeMinutePassing()
        {
            PlayerOnline();
            if (messages_log.Count > 59)
            {
                messages_log.RemoveAt(0);
            }

            messages_log.Add(0);
            recomend_online = full_online.Count * (int)(online_modifier * 10) / 10;
            log = null;
            log = log + "\n" + Date.ReturnCurrDate();
            log = log + "\nДанные об активности";
            log = log + "\n" + $"Онлайн модификатор: {online_modifier}";
            log = log + "\n" + $"Рекомендуемый онлайн на данный момент: {recomend_online}";
            for (int i = 0; i < online.Count; i++)
            {
                //Установка значения прошедших минут
                int passed_minutes;
                passed_minutes = Date.max_minutes_pass;
                //Вычитание прошедших минут
                if (2 < 1)
                {
                    log = log + "\n" + $"Время для {online[i].nick} на румине закончилось";


                    online.RemoveAt(i);
                    if (inactive_online.Count != 0)
                    {
                        int id_inactive_replace = random.Next(inactive_online.Count);
                        log = log + "\n" + $"Зато взамен пришел {inactive_online[id_inactive_replace].nick}";
                        online.Add(inactive_online[id_inactive_replace]);
                        inactive_online.RemoveAt(id_inactive_replace);
                    }
                    else
                    {
                        log = log + "\n" + "Но взамен приходить и некому";
                    }

                }

            }

            //Приход
            #region Рандомный приход и уход пользователей
            int id_in = random.Next(inactive_online.Count);
            {
                log = log + "\n" + $"{inactive_online[id_in].nick} зашел на румине поглядеть чекак";
                online.Add(inactive_online[id_in]);
                inactive_online.RemoveAt(id_in);
            }

            //Уход
            int id_on = random.Next(online.Count);

            #endregion

            //Расставляем флажки активности и последнюю дату посещения
            ActiveUsersUpdate();

            //Определяем липовый "весь" онлайн
            all_online = ((UserList.UserAmount * 3) * (int)(real_online_modifier * 100) / 100) + online.Count + random.Next(3);


            SetActivness();
            if(last_reasons.Count != 0 || curr_event.type != EventsEnum.nothing)
            {
                CheckEvent(last_reasons[random.Next(last_reasons.Count)]);
            }
            TechFullOnlineCreation();
            TechInactiveOnlineUpdate();
        }

        #region Вспомогательные методы для основного тика таймера
        //Обновление информации пользователям в онлайне
        private static void ActiveUsersUpdate()
        {
            for (int i = 0; i < UserList.Users.Count; i++)
            {
                if (online.Contains(UserList.Users[i]))
                {
                    //Если пользователь активен - обновляем его активность
                    DateTime act_date = new DateTime();
                    act_date = Date.current_date;
                    UserList.Users[i].last_activity = act_date;
                    UserList.Users[i].activity = true;

                    if (online.Count != 0)
                    {
                        CheckMessage(i);
                    }

                }
                else
                    UserList.Users[i].activity = false;
            }
            if (online.Count != 0)
            {
                CheckBan();
                CheckNews();
                CheckReputation();
                CheckComments();
            }

        }


        #region Проверки на совершение действия


        //Проверка на совершение пользователем тех или иных действий
        private static void CheckMessage(int i)
        {
            if (AdvRandom.PersentChanseBool(((int)(real_online_modifier * 10) + online.Count / 5) + curr_event.message_mod))
            {
                if(last_author != UserList.Users[i])
                {
                    messages_log[messages_log.Count - 1]++;
                    messages_per_day++;
                    UserList.Users[i].messages++;
                    last_author = UserList.Users[i];
                    Reason reason = new Reason(null, UserList.Users[i], online[random.Next(online.Count)], ReasonsEnum.message);
                    last_reasons.Add(reason);
                    foreach (User user in online)
                    {
                        if (UserList.Users[i].relations.friends.ContainsKey(user))
                        {
                            if (AdvRandom.PersentChanseBool(random.Next(5, 10)))
                                UserList.Users[i].likes++;
                        }
                        else if (UserList.Users[i].relations.comrades.ContainsKey(user))
                        {
                            if (AdvRandom.PersentChanseBool(random.Next(0, 10)))
                                UserList.Users[i].likes++;
                        }
                        else if (UserList.Users[i].relations.neutrals.ContainsKey(user))
                        {
                            if (AdvRandom.PersentChanseBool(random.Next(0, 5)))
                                UserList.Users[i].likes++;
                        }
                        else if (UserList.Users[i].relations.unfriends.ContainsKey(user))
                        {
                            if (AdvRandom.PersentChanseBool(1))
                                UserList.Users[i].likes++;
                        }
                    }
                }
            }
        }
        private static void CheckReputation()
        {
            if (AdvRandom.PersentChanseBool(15))
            {
                int rnd_id = random.Next(online.Count);
                int rnd_ids = random.Next(0, online[rnd_id].relations.All.Count);
                User user_goal = online[rnd_id].relations.All.ElementAt(rnd_ids).Key;
                if (online[rnd_id].blocked_users_rep[user_goal] == 0)
                {
                    if (online[rnd_id].relations.friends.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(4, 8)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, ReputationReason.ReturnReason(false));
                    }
                    else if (online[rnd_id].relations.comrades.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(2, 4)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, ReputationReason.ReturnReason(false));
                    }
                    else if (online[rnd_id].relations.neutrals.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(3))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, ReputationReason.ReturnReason(false));
                    }
                    else if (online[rnd_id].relations.unfriends.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(2, 5)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], -online[rnd_id].karma.karma, ReputationReason.ReturnReason(true));
                    }
                    else if (online[rnd_id].relations.enemies.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(1, 3)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], -online[rnd_id].karma.karma, ReputationReason.ReturnReason(true));
                    }

                }
            }
            if (AdvRandom.PersentChanseBool(5))
            {
                int rnd_id = random.Next(online.Count);
                online[rnd_id].CheckReputation();
            }

        }
        private static void CheckComments()
        {
            int rnd_id = random.Next(online.Count);
            if (AdvRandom.PersentChanseBool(2, 100))
            {
                online[rnd_id].comments++;
                if (AdvRandom.PersentChanseBool(25))
                    online[rnd_id].comments_rate += random.Next(1, 4);
                else
                    online[rnd_id].comments_rate += random.Next(0, 2);
            }

        }
        private static void CheckNews()
        {
            int rnd_id = random.Next(online.Count);
            if (AdvRandom.PersentChanseBool(1, 100))
            {
                online[rnd_id].news++;
            }
        }
        private static void CheckBan()
        {
            List<User> mods = new List<User>();
            foreach (User user in online)
            {
                if (user.mod)
                    mods.Add(user);
            }
            if (mods.Count != 0)
            {
                int rnd_mod = random.Next(mods.Count);
                List<User> mod_goals = new List<User>();
                foreach (User user in online)
                {
                    if (mods[rnd_mod].relations.enemies.ContainsKey(user))
                        mod_goals.Add(user);
                    if (user.traits.Contains(TraitsList.AllTraits[Traits.madguy]))
                        mod_goals.Add(user);
                    if (user.traits.Contains(TraitsList.AllTraits[Traits.rak]))
                        mod_goals.Add(user);
                }
                foreach (User user in mod_goals)
                {
                    int rnd_id = random.Next(mod_goals.Count);
                    if (AdvRandom.PersentChanseBool(2, 1000) && !mod_goals[rnd_id].LastBan.Banned)
                    {
                        mod_goals[rnd_id].LastBan.AddWarnings(mods[rnd_mod], random.Next(1, 101), "Предупрежденьки!", mod_goals[rnd_id]);
                    }
                }
            }
        }
        private static void CheckEvent(Reason reason)
        {
            if (curr_event.type == EventsEnum.nothing)
            {
                if (last_messages.Sum() > 24)
                {
                    if(AdvRandom.PersentChanseBool(4, 1000))
                    {
                        EventSrach srach = new EventSrach("Срач", EventsEnum.Srach, reason);
                        curr_event = srach;
                    }
                }
                else
                {
                    if (last_messages.Sum() > 18)
                    {
                        if(AdvRandom.PersentChanseBool(6, 1000))
                        {
                            EventDusskussion dissk = new EventDusskussion("Обсуждение", EventsEnum.disskussion, reason);
                            curr_event = dissk;
                        }
                        else if(AdvRandom.PersentChanseBool(1, 1200) && reason.type == ReasonsEnum.message)
                        {
                            EventAnounce anounce = new EventAnounce("Объявление!", EventsEnum.anouncement, reason);
                            curr_event = anounce;
                        }
                    }
                }
            }
            else
            {
                bool end = curr_event.EventPassing();
                if (end)
                {
                    Event nothing = new Event("Ничего особенного", EventsEnum.nothing, null);
                    curr_event = nothing;

                }
            }


        }



        #endregion



        #endregion


        //Каждый час задаем новое направление онлайна
        public static void SetTimeForUsersForHour()
        {
            SetOnlineModifier();
            recomend_online = full_online.Count * (int)(online_modifier * 10) / 10;

            //Прибавляем к шансу активности модификатор часа


            //Изменение онлайна
            HourSetFullOnline();
        }

        //Установка новых значений для дня/месяца
        public static void NewDayUpdate()
        {
            messages_Per_month.Add(messages_per_day);
            messages_per_day = 0;
            for (int i = 0; i < UserList.Users.Count; i++)
            {
            }
            Event nothing = new Event("Ничего особенного", EventsEnum.nothing, null);
            curr_event = nothing;
        }
        public static void NewMonthUpdate()
        {
            messages_Per_year.Add((int)messages_Per_month.Average());
            messages_Per_month.Clear();
        }

        #region Получение отсортированных онлайнов
        public static List<User> GetOnline()
        {
            if (online.Count != 0)
            {
                var sortedGr = from i in online
                               orderby i.registration
                               select i;
                return sortedGr.ToList();
            }
            return new List<User>();

        }
        public static List<User> GetFullOnline()
        {
            return new List<User>();
        }
        public static List<User> GetInActiveOnline()
        {
            return new List<User>();
        }
        #endregion
    }
}
