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
        static public int prev_hour_online { get; private set; }
        static public int recomend_online { get; private set; }
        static public int all_online { get; private set; }
        static public string log { get; private set; }
        static public List<int> messages_log = new List<int>();
        static public string activness { get; private set; }

        static public int messages_per_day { get; private set; }

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

        public static void ActivityDimishing()
        {
            if (online_modifier < end_online_modifier)
                online_modifier += 0.01F;
            else if (online_modifier > end_online_modifier)
                online_modifier -= 0.01F;
        }

        private static void HourSetFullOnline()
        {
            List<User> rnd_users = TechFullOnlineCreation();
            TechInactiveOnlineUpdate();
            //Выходим на нужную активность путем очистки юзеров с низким шансом активности
            int difference;
            if (rnd_users.Count > recomend_online)
            {
                difference = rnd_users.Count - recomend_online;
                var sortedGr = from i in rnd_users
                               orderby i.activity_chanse
                               select i;
                rnd_users = sortedGr.ToList();
                rnd_users.RemoveRange(0, difference);
            }
            else
            {

            }
            online = rnd_users;
            for (int a = 0; a < online.Count; a++)
            {
                online[a].activity_times--;
                online[a].curr_timeForTime = random.Next(31, 81);
            }
            TechInactiveOnlineUpdate();

        }

        //Полный онлайн - ВСЕ юзеры, у которых еще не кончилось время на румайн
        private static List<User> TechFullOnlineCreation()
        {
            full_online.Clear();
            var sortedGr = from i in UserControl.Users
                           where i.activity_times > 0
                           orderby random.Next(100)
                           select i;
            List<User> rnd_users = sortedGr.ToList();
            full_online = rnd_users.GetRange(0, rnd_users.Count);
            return rnd_users;
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

        private static void SetActivness()
        {
            if (messages_log.Count == 60)
            {
                List<int> last_messages = messages_log.GetRange(39, 20);
                if (last_messages.Sum() < 5)
                {
                    activness = "Пустыня";
                }
                else if (last_messages.Sum() >= 5 && last_messages.Sum() < 15)
                {
                    activness = "Тухлячок";
                }
                else if (last_messages.Sum() >= 15 && last_messages.Sum() < 30)
                {
                    activness = "Вялотекущий разговор";
                }
                else if (last_messages.Sum() >= 30 && last_messages.Sum() < 50)
                {
                    activness = "Бодрое обсуждение";
                }
                else if (last_messages.Sum() >= 50)
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
                online[i].curr_timeForTime -= 1;

                if (online[i].curr_timeForTime < 1)
                {
                    log = log + "\n" + $"Время для {online[i].nick} на румине закончилось";
                    if (online[i].activity_times == 0)
                    {
                        online[i].cooldawn = random.Next(8, 12);
                    }
                    else
                    {
                        online[i].cooldawn = random.Next(0, 4);
                    }
                    online.RemoveAt(i);
                    if (inactive_online.Count != 0)
                    {
                        int id_inactive_replace = random.Next(inactive_online.Count);
                        inactive_online[id_inactive_replace].curr_timeForTime = random.Next(10, 30);
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
            int id_in = random.Next(inactive_online.Count);
            if (inactive_online.Count != 0 && AdvRandom.PersentChanseBool(inactive_online[id_in].activity_chanse) && inactive_online[id_in].cooldawn == 0 && online.Count <= recomend_online + recomend_online / 4)
            {
                log = log + "\n" + $"{inactive_online[id_in].nick} зашел на румине поглядеть чекак";
                inactive_online[id_in].curr_timeForTime = random.Next(10, 30);
                online.Add(inactive_online[id_in]);
                inactive_online.RemoveAt(id_in);
            }

            //Уход
            int id_on = random.Next(online.Count);
            if (online.Count != 0 && AdvRandom.PersentChanseBool(90 - online[id_on].activity_chanse) && (online[id_on].activity_times > 1) && online.Count >= recomend_online - recomend_online / 4)
            {
                online[id_on].cooldawn = random.Next(0, 4);
                online[id_on].curr_timeForTime = 0;
                log = log + "\n" + $"{online[id_on].nick} решил пойти по своим делам";
                inactive_online.Add(online[id_on]);
                online.RemoveAt(id_on);
            }

            //Расставляем флажки активности и последнюю дату посещения
            ActiveUsersUpdate();
            SetReputationCheck();
            SetCommentsCheck();



            //Определяем липовый "весь" онлайн
            all_online = ((UserControl.UserAmount * 3) * (int)(real_online_modifier * 100) / 100) + online.Count + random.Next(3);


            SetActivness();
            TechFullOnlineCreation();
            TechInactiveOnlineUpdate();
        }

        #region Вспомогательные методы для основного тика таймера
        private static void ActiveUsersUpdate()
        {
            for (int i = 0; i < UserControl.Users.Count; i++)
            {
                if (online.Contains(UserControl.Users[i]))
                {
                    DateTime act_date = new DateTime();
                    act_date = Date.current_date;
                    UserControl.Users[i].last_activity = act_date;
                    UserControl.Users[i].activity = true;
                    if (AdvRandom.PersentChanseBool((int)(real_online_modifier * 10) + online.Count / 5))
                    {
                        messages_log[messages_log.Count - 1]++;
                        messages_per_day++;
                        UserControl.Users[i].messages++;
                        foreach (User user in online)
                        {
                            if (UserControl.Users[i].relations.friends.ContainsKey(user))
                            {
                                if (AdvRandom.PersentChanseBool(random.Next(5, 10)))
                                    UserControl.Users[i].likes++;
                            }
                            else if (UserControl.Users[i].relations.comrades.ContainsKey(user))
                            {
                                if (AdvRandom.PersentChanseBool(random.Next(0, 10)))
                                    UserControl.Users[i].likes++;
                            }
                            else if (UserControl.Users[i].relations.neutrals.ContainsKey(user))
                            {
                                if (AdvRandom.PersentChanseBool(random.Next(0, 5)))
                                    UserControl.Users[i].likes++;
                            }
                            else if (UserControl.Users[i].relations.unfriends.ContainsKey(user))
                            {
                                if (AdvRandom.PersentChanseBool(1))
                                    UserControl.Users[i].likes++;
                            }
                        }
                    }
                }
                else
                    UserControl.Users[i].activity = false;
            }
        }
        private static void SetReputationCheck()
        {
            if (online.Count != 0)
            {
                int rnd_id = random.Next(online.Count);
                int rnd_ids = random.Next(0, online[rnd_id].relations.All.Count);
                User user_goal = online[rnd_id].relations.All.ElementAt(rnd_ids).Key;
                if (online[rnd_id].blocked_users_rep[user_goal] == 0)
                {
                    if (online[rnd_id].relations.friends.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(4, 8)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, "Друг");
                    }
                    else if (online[rnd_id].relations.comrades.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(2, 4)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, "Товарищ");
                    }
                    else if (online[rnd_id].relations.neutrals.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(3))
                            user_goal.reputation.ChangeReputation(online[rnd_id], online[rnd_id].karma.karma, "Нейтрал");
                    }
                    else if (online[rnd_id].relations.unfriends.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(2, 5)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], -online[rnd_id].karma.karma, "Неприятель");
                    }
                    else if (online[rnd_id].relations.enemies.ContainsKey(user_goal))
                    {
                        if (AdvRandom.PersentChanseBool(random.Next(1, 3)))
                            user_goal.reputation.ChangeReputation(online[rnd_id], -online[rnd_id].karma.karma, "Враг");
                    }
                }
            }
        }
        private static void SetCommentsCheck()
        {
            if (online.Count != 0)
            {
                int rnd_id = random.Next(online.Count);
                if (AdvRandom.PersentChanseBool(5))
                {
                    online[rnd_id].comments++;
                    if (AdvRandom.PersentChanseBool(25))
                        online[rnd_id].comments_rate += random.Next(1, 4);
                    else
                        online[rnd_id].comments_rate += random.Next(0, 2);
                }
            }
        }
        private static void SetNewsCheck()
        {
            if (online.Count != 0)
            {
                int rnd_id = random.Next(online.Count);
                if (AdvRandom.PersentChanseBool(1))
                {
                    online[rnd_id].news++;
                }
            }
        }
        #endregion


        //Каждый час задаем новое направление онлайна
        public static void SetTimeForUsersForHour()
        {
            SetOnlineModifier();
            recomend_online = full_online.Count * (int)(online_modifier * 10) / 10;

            //Прибавляем к шансу активности модификатор часа
            for (int i = 0; i < UserControl.Users.Count; i++)
            {
                UserControl.Users[i].activity_chanse += UserControl.Users[i].activity_chanse * (int)(online_modifier * 100) / 100;
            }

            //Изменение онлайна
            HourSetFullOnline();
        }

        public static void NewDayUpdate()
        {
            messages_per_day = 0;
            for (int i = 0; i < UserControl.Users.Count; i++)
            {
                UserControl.Users[i].cooldawn = UserControl.Users[i].cooldawn + random.Next(0, 6);
            }
        }

        #region Получение отсортированных онлайнов
        public static List<User> GetOnline()
        {
            var sortedGr = from i in online
                           orderby i.registration
                           select i;
            return sortedGr.ToList();
        }
        public static List<User> GetFullOnline()
        {
            var sortedGr = from i in full_online
                           orderby i.activity_times descending
                           select i;
            return sortedGr.ToList();
        }
        public static List<User> GetInActiveOnline()
        {
            var sortedGr = from i in inactive_online
                           orderby i.cooldawn descending
                           select i;
            return sortedGr.ToList();
        }
        #endregion
    }
}
