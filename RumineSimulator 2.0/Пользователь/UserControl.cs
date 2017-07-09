using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UsersControl
    {
        //Список пользователей в приложении и их количество
        static public List<User> UsersList = new List<User>();
        static public float ModerAmount { get; set; }
        static public int UserAmount
        {
            get
            {
                return UsersList.Count;
            }
        }
        static public List<string> UpdatesUserLog = new List<string>();

        static Random random = new Random();


        //Генерация пользователей в указанном количестве
        static public void GenerateUsers(int amount)
        {
            ModerAmount = 0;
            for (int i = 0; i < amount; i++)
            {
                User generated_user = new User();
                UsersList.Add(generated_user);
                Statistic.aver_adeq += generated_user.character.adeq.Value;
                Statistic.aver_rakness += generated_user.character.rakness.Value;
                Statistic.aver_conservative += generated_user.character.conservative.Value;
                Statistic.aver_tolerance += generated_user.character.tolerance.Value;
                Statistic.aver_creativity += generated_user.character.creativity.Value;
                Statistic.aver_sciense += generated_user.character.sciense.Value;
            }
        }

        //Генерация отношений для каждого пользователя из списка юзверей
        static public void GenerateRelations()
        {
            Statistic.aver_adeq /= UserAmount;
            Statistic.aver_rakness /= UserAmount;
            Statistic.aver_conservative /= UserAmount;
            Statistic.aver_tolerance /= UserAmount;
            Statistic.aver_creativity /= UserAmount;
            Statistic.aver_sciense /= UserAmount;
            for (int i = 0; i < UserAmount; i++)
            {
                UsersList[i].GenerateRelation();
            }
        }

        //Проверка пользователей на апдейты
        static public void CheckingAllUserForUpdates()
        {
            for (int i = 0; i < UsersList.Count; i++)
            {
                if (Date.current_date_prev.Minute > Date.current_date.Minute)
                {
                }
                if (Date.current_date.Hour == 0 && Date.current_date.Minute < Date.current_date_prev.Minute)
                {
                    UsersList[i].UpdateBeginDay();
                }
                if (Date.current_date.Hour == 23 && Date.current_date.Minute == 59)
                {
                    UsersList[i].UpdateEndDay();
                }
                UsersList[i].CheckingForUpdates();
            }
        }

        //Очистка пользователей
        static public void UsersClear()
        {
            Nicks.NicksInit();
            UsersList.Clear();
        }

        //Выбор модераторов
        static public void ModerChoose()
        {
            int mod_recomend = UserAmount / 20;
            mod_recomend += random.Next(0, 3);
            int need_amount = mod_recomend - (int)ModerAmount;
            int i = 0;
            List<User> poss_mods = ReturnUsersModerChanseDesc();
            do
            {
                if (AdvRnd.PersentChanseBool(poss_mods[i].moder_chanse))
                {
                    poss_mods[i].group = GroupsList.Groups[GroupsEnum.Moderator];
                    poss_mods[i].mod = true;
                    ModerAmount++;
                }
                i++;
            }
            while (ModerAmount != mod_recomend && i != _0.UsersControl.UsersList.Count - 5);


        }

        static public User UserSearch(string nick)
        {
            foreach (User user in UsersList)
            {
                if (user.nick == nick)
                    return user;
            }
            return null;
        }
        static public User UserSearch(int id)
        {
            foreach (User user in UsersList)
            {
                if (user.user_id == id)
                    return user;
            }
            return null;
        }

        static public void FractionChoose()
        {
            foreach (User user in UsersList)
            {
                //Добавляем доступные фракции
                List<Fraction> Av_fracs = new List<Fraction>();
                foreach (Fraction fraction in FractionList.AllFractions)
                {
                    if (fraction.MemberAcceptCheck(user))
                        Av_fracs.Add(fraction);
                }
                //Распределяем доступные фракции
                if (Av_fracs.Count == 0)
                {
                    user.JoinFraction(FractionList.NeutralFraction, true);
                }
                else if(Av_fracs.Count == 1)
                {
                    user.JoinFraction(Av_fracs[0], true);
                }
                else if(Av_fracs.Count > 1)
                {
                    //Шанс остаться нейтральным или все-таки
                    if (AdvRnd.PersentChanseBool(75))
                    {
                        int rnd_id = random.Next(Av_fracs.Count);
                        user.JoinFraction(Av_fracs[rnd_id], true);
                        Av_fracs.RemoveAt(rnd_id);
                    }
                    else
                    {
                        user.JoinFraction(FractionList.NeutralFraction, true);
                    }
                    //Добиваем оставшиеся фракции
                    foreach (Fraction fract in Av_fracs)
                    {
                        if (AdvRnd.PersentChanseBool(50))
                        {
                            user.JoinFraction(fract, false);
                        }
                    }
                }
            }
            foreach (Fraction fr in FractionList.AllFractions)
            {
                fr.FractionCreationEnd();
            }
        }

        #region Сортировки
        static public List<User> ReturnUsersGroupRarenesSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.@group.Respect descending
                           select i;
            return sortedGr.ToList();

        }
        static public List<User> ReturnUsersRegSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.registration
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersMessagesSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.messages descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRepSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.reputation.Base_reputation descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersAdeqSortDesc()
        {
            var sortedGr = from i in UsersList
                           where i.main_fraction.name != "Нет фракции"
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRakSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.character.rakness.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersConsSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.character.conservative descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersCreativeSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.character.creativity.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersScienseSortDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.character.sciense.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersModerChanseDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.moder_chanse descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersForumInfluenceDesc()
        {
            var sortedGr = from i in UsersList
                           orderby i.forum_influence descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersActiveFilter()
        {
            var sortedGr = from i in UsersList
                           where i.activity == true
                           select i;
            return sortedGr.ToList();
        }

        //Сортировка по списку трейтов
        static public List<User> ReturnUserByTraits(List<User> users_to_sort,List<Traits> req_traits,bool and_or = true)
        {
            List<User> sorted = new List<User>();
            foreach (User user in users_to_sort)
            {
                bool sort = false;
                if (and_or)
                {
                    sort = true;
                    foreach (Traits t_type in req_traits)
                    {
                        if (!user.traits.Contains(TraitsList.AllTraits[t_type]))
                            sort = false;
                    }
                }
                else
                {
                    foreach (Traits t_type in req_traits)
                    {
                        if (user.traits.Contains(TraitsList.AllTraits[t_type]))
                            sort = true;
                    }
                }

                if (sort)
                    sorted.Add(user);
            }
            return sorted;
        }
        #endregion
    }
}
