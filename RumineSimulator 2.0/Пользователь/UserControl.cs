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
        static public List<User> act_users = new List<User>();
        static public List<User> all_users = new List<User>();

        public static List<User> reserve;

        //Считается по статусу пользователя, не группы
        static public int moderAmount
        {
            get
            {
                int mods = 0;
                foreach (User user in act_users)
                {
                    if (user.group.Mod)
                        mods++;
                }
                return mods;
            }
        }

        static public List<string> UpdatesUserLog = new List<string>();

        #region Создание пользователей, генерация отношений
        //Генерация пользователя
        static public void GenerateUser(bool admin = false)
        {
            User generated_user = new User(admin);
            all_users.Add(generated_user);
        }
        //Генерация отношений для каждого пользователя из списка юзверей
        static public void GenerateRelations()
        {
            for (int i = 0; i < all_users.Count; i++)
            {
                all_users[i].GenerateRelation();
            }
            //Во всех пользователя все пользователи, в пользователях лишь активные и известные
            act_users = UsersRet();
            reserve = new List<User>(all_users);
        }
        #endregion

        //Проверка пользователей на апдейты
        static public void CheckingAllUserForUpdates()
        {
            for (int i = 0; i < act_users.Count; i++)
            {
                if (Date.current_date.Hour == 0 && Date.current_date.Minute < Date.current_date_prev.Minute)
                {
                    act_users[i].UpdateBeginDay();
                }
                if (Date.current_date.Hour == 23 && Date.current_date.Minute == 59)
                {
                    act_users[i].UpdateEndDay();
                }
                act_users[i].CheckingForUpdates();
            }
        }
        //Возвращение списка пользователей по их активности и неизвестности
        static public List<User> UsersRet(bool active = true, bool unknown = false, bool admin = false)
        {
            List<User> users_sort = new List<User>();
            for (int i = 0; i < all_users.Count; i++)
            {
                if (all_users[i].activity == active && all_users[i].unknown == unknown && all_users[i].admin == false)
                {
                    users_sort.Add(all_users[i]);
                }
            }
            return users_sort;
        }

        #region Поиск пользователей(по нику и id)
        static public User UserSearch(string nick)
        {
            foreach (User user in all_users)
            {
                if (user.nick == nick)
                    return user;
            }
            return null;
        }
        static public User UserSearch(int id)
        {
            foreach (User user in all_users)
            {
                if (user.user_id == id)
                    return user;
            }
            return null;
        }
        #endregion

        #region Сортировки
        //Возвращение списка на основе заданной сортировки
        static public List<User> UserListReturnSort()
        {
            //if (reserve != null && reserve.Count != 0)
            //    all_users = reserve;
            List<User> ret_list = new List<User>();
            switch (Presenter.sorting_user)
            {
               
                case SortingUserTypes.no_sort:
                    ret_list = all_users;
                    return ret_list;
                case SortingUserTypes.groupRareness:
                    ret_list = ReturnUsersGroupRarenesSortDesc();
                    return ret_list;
                case SortingUserTypes.adeq:
                    ret_list = ReturnUsersAdeqSortDesc();
                    return ret_list;
                case SortingUserTypes.influence:
                    ret_list = ReturnUsersForumInfluenceDesc();
                    return ret_list;
                case SortingUserTypes.messages:
                    ret_list = ReturnUsersMessagesSortDesc();
                    return ret_list;
                case SortingUserTypes.moderChanse:
                    ret_list = ReturnUsersModerChanseDesc();
                    return ret_list;
                case SortingUserTypes.rakness:
                    ret_list = ReturnUsersRakSortDesc();
                    return ret_list;
                case SortingUserTypes.registration:
                    ret_list = ReturnUsersRegSortDesc();
                    return ret_list;
                case SortingUserTypes.reputation:
                    ret_list = ReturnUsersRepSortDesc();
                    return ret_list;
                default:
                    return act_users;
            }
        }


        static public List<User> ReturnUsersGroupRarenesSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.@group.Respect descending
                           select i;
            return sortedGr.ToList();

        }
        static public List<User> ReturnUsersLeaveChanseSortDesc()
        {
            var sortedGr = from i in act_users
                           orderby i.character.leaveChanse.Value descending
                           select i;
            return sortedGr.ToList();

        }
        static public List<User> ReturnUsersRegSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.registration
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersMessagesSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.messages descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRepSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.reputation.Base_reputation descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersAdeqSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.character.adeq.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRakSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.character.rakness.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersCreativeSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.character.creativity.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersScienseSortDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.character.sciense.Value descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersModerChanseDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.moder_chanse descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersForumInfluenceDesc()
        {
            var sortedGr = from i in all_users
                           orderby i.forum_influence descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersActiveFilter()
        {
            var sortedGr = from i in all_users
                           where i.activity == true
                           select i;
            return sortedGr.ToList();
        }

        //Сортировка по списку трейтов
        static public List<User> ReturnUserByTraits(List<User> users_to_sort, List<TraitsType> req_traits, bool and_or = true)
        {
            List<User> sorted = new List<User>();
            foreach (User user in users_to_sort)
            {
                bool sort = false;
                if (and_or)
                {
                    sort = true;
                    foreach (TraitsType t_type in req_traits)
                    {
                        if (!user.traits.Contains(Trait.SearchTrait(t_type)))
                            sort = false;
                    }
                }
                else
                {
                    foreach (TraitsType t_type in req_traits)
                    {
                        if (user.traits.Contains((Trait.SearchTrait(t_type))))
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
