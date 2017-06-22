using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserList
    {
        //Список пользователей в приложении и их количество
        static public List<User> Users = new List<User>();
        static public int UserAmount
        {
            get
            {
                return Users.Count;
            }
        }

        static public float ModerAmount { get; private set; }
        static public float aver_rakness { get; private set; }
        static public float aver_adeq { get; private set; }
        static public float aver_tolerance { get; private set; }
        static public float aver_sciense { get; private set; }
        static public float aver_creativity { get; private set; }
        static public float aver_conservative { get; private set; }

        static public List<string> UpdatesUserLog = new List<string>();

        static Random random = new Random();


        //Генерация пользователей в указанном количестве
        static public void GenerateUsers(int amount)
        {
            ModerAmount = 0;
            for (int i = 0; i < amount; i++)
            {
                User generated_user = new User();
                Users.Add(generated_user);
                aver_adeq += generated_user.character.adeq.Param_value;
                aver_rakness += generated_user.character.rakness.Param_value;
                aver_conservative += generated_user.character.conservative.Param_value;
                aver_tolerance += generated_user.character.tolerance.Param_value;
                aver_creativity += generated_user.character.creativity.Param_value;
                aver_sciense += generated_user.character.sciense.Param_value;
            }
        }

        //Генерация отношений для каждого пользователя из списка юзверей
        static public void GenerateRelations()
        {
            aver_adeq /= UserAmount;
            aver_rakness /= UserAmount;
            aver_conservative /= UserAmount;
            aver_tolerance /= UserAmount;
            aver_creativity /= UserAmount;
            aver_sciense /= UserAmount;
            for (int i = 0; i < UserAmount; i++)
            {
                Users[i].GenerateRelation();
            }
        }

        //Проверка пользователей на апдейты
        static public void CheckingAllUserForUpdates()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                if (Date.current_date_prev.Minute > Date.current_date.Minute)
                {
                }
                if (Date.current_date.Hour == 0 && Date.current_date.Minute < Date.current_date_prev.Minute)
                {
                    Users[i].UpdateBeginDay();
                }
                if (Date.current_date.Hour == 23 && Date.current_date.Minute == 59)
                {
                    Users[i].UpdateEndDay();
                }
                Users[i].CheckingForUpdates();
            }
        }

        //Очистка пользователей
        static public void UsersClear()
        {
            Nicks.NicksInit();
            Users.Clear();
        }
        static public void CreateInterfaceInfo()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].CreateInterfaceInfo();
            }
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
                if (AdvRandom.PersentChanseBool(poss_mods[i].moder_chanse))
                {
                    poss_mods[i].group = GroupsList.Groups[GroupsEnum.Moderator];
                    poss_mods[i].mod = true;
                    ModerAmount++;
                }
                i++;
            }
            while (ModerAmount != mod_recomend && i != UserList.Users.Count - 5);


        }

        static public User UserSearch(string nick)
        {
            foreach (User user in Users)
            {
                if (user.nick == nick)
                    return user;
            }
            return null;
        }

        static public void FractionChoose()
        {
            foreach (User user in Users)
            {
                //Добавляем доступные фракции
                List<Fraction> Av_fracs = new List<Fraction>();
                foreach (Fraction fraction in FractionList.AllFractions)
                {
                    if (fraction.MemberAccept(user))
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
                    if (AdvRandom.PersentChanseBool(75))
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
                        if (AdvRandom.PersentChanseBool(50))
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
            var sortedGr = from i in Users
                           orderby i.@group.Respect descending
                           select i;
            return sortedGr.ToList();

        }
        static public List<User> ReturnUsersRegSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.registration
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersMessagesSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.messages descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRepSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.reputation.Base_reputation descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersAdeqSortDesc()
        {
            var sortedGr = from i in Users
                           where i.main_fraction.name != "Нет фракции"
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRakSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.rakness.Param_value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersConsSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.conservative descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersCreativeSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.creativity.Param_value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersScienseSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.sciense.Param_value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersModerChanseDesc()
        {
            var sortedGr = from i in Users
                           orderby i.moder_chanse descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersForumInfluenceDesc()
        {
            var sortedGr = from i in Users
                           orderby i.forum_influence descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersActiveFilter()
        {
            var sortedGr = from i in Users
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
