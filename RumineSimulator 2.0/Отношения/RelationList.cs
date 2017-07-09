using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class RelationList
    {
        //Список всех пользователей, у которых есть отношения с главным юзером
        public Dictionary<User, Relation> All = new Dictionary<User, Relation>();
        //Более подробные списки с указанием отношений
        public Dictionary<User, Relation> friends = new Dictionary<User, Relation>();
        public Dictionary<User, Relation> comrades = new Dictionary<User, Relation>();
        public Dictionary<User, Relation> enemies = new Dictionary<User, Relation>();
        public Dictionary<User, Relation> unfriends = new Dictionary<User, Relation>();
        public Dictionary<User, Relation> rivals = new Dictionary<User, Relation>();
        public Dictionary<User, Relation> neutrals = new Dictionary<User, Relation>();

        public RelationList()
        {

        }

        public void RelationsGenerate(User main_user)
        {
            //Цикл для генерации отношений со всеми пользователями
            for (int i = 0; i < UsersControl.UserAmount; i++)
            {
                //Если юзера, к которому надо сгенерить отношения самого нету в отношениях - добавляем
                if (!All.ContainsKey(main_user))
                    All.Add(main_user, new Relation(main_user, main_user));
                User pr_user = UsersControl.UsersList[i];
                //Если пользователь еще не добавлен в главный список то генерим отношение, иначе забиваем
                if (!IsUserAlreadyInList(pr_user, main_user))
                {
                    //Собственно, генерация отношения
                    Relation curr_relation = new Relation(main_user, pr_user);
                    #region Итоговое решение в какую группу посылать сравниваемого пользователя
                    //Добавляем пользователя в список "всех"(альтернативному юзеру делаем то же самое)
                    All.Add(pr_user, curr_relation);
                    pr_user.relations.All.Add(main_user, new Relation(pr_user, main_user));

                    //Последняя итерация цикла - распределяем всех из оллов
                    if (i == UsersControl.UserAmount - 1)
                    {
                        for (int a = 0; a < All.Count; a++)
                        {
                            //Просматриваем каждого пользователя в олле и распределяем
                            User adding_user = All.ElementAt(a).Key;
                            Relation perevorot = new Relation(adding_user, main_user);
                            #region Распределение
                            if (!IsUserAlreadyInAdvList(adding_user, main_user))
                            {
                                //Друзья
                                if (All[adding_user].friendness >= GlobalParams.friend_condition)
                                {
                                    friends.Add(adding_user, All[adding_user]);
                                    adding_user.relations.friends.Add(main_user, perevorot);

                                }

                                //Товарищи
                                else if ((All[adding_user].friendness < GlobalParams.friend_condition) && (All[adding_user].friendness >= GlobalParams.comrade_condition))
                                {
                                    comrades.Add(adding_user, All[adding_user]);
                                    adding_user.relations.comrades.Add(main_user, perevorot);
                                }

                                //Неприятели
                                else if ((All[adding_user].friendness > GlobalParams.enemy_condition) && (All[adding_user].friendness <= GlobalParams.unfriends_condition))
                                {
                                    unfriends.Add(adding_user, All[adding_user]);
                                    adding_user.relations.unfriends.Add(main_user, perevorot);
                                }

                                //Враги
                                else if (All[adding_user].friendness <= GlobalParams.enemy_condition)
                                {
                                    enemies.Add(adding_user, All[adding_user]);
                                    adding_user.relations.enemies.Add(main_user, perevorot);
                                }

                                //Нейтралы
                                else
                                {
                                    neutrals.Add(adding_user, All[adding_user]);
                                    adding_user.relations.neutrals.Add(main_user, perevorot);
                                }

                                //Соперники
                                if (All[adding_user].rivalry >= GlobalParams.rival_condition)
                                {
                                    rivals.Add(adding_user, All[adding_user]);
                                    adding_user.relations.rivals.Add(main_user, perevorot);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }

            }
        }

        //Есть ли юзер в общем списке отношений
        private bool IsUserAlreadyInList(User check_user, User m_user)
        {
            if (m_user == check_user)
                return true;
            if (All.ContainsKey(check_user))
                return true;

            return false;
        }
        //Есть ли юзер в более подробных списках отношений
        private bool IsUserAlreadyInAdvList(User check_user, User m_user)
        {
            if (m_user == check_user)
                return true;
            if (friends.ContainsKey(check_user))
                return true;
            if (enemies.ContainsKey(check_user))
                return true;
            if (rivals.ContainsKey(check_user))
                return true;
            if (neutrals.ContainsKey(check_user))
                return true;
            if (comrades.ContainsKey(check_user))
                return true;
            if (unfriends.ContainsKey(check_user))
                return true;

            return false;
        }

        //Где же находится юзер?
        public string UsersSpace(User check_user)
        {
            if (friends.ContainsKey(check_user))
                return "friends";
            else if (enemies.ContainsKey(check_user))
                return "enemies";
            else if (neutrals.ContainsKey(check_user))
                return "neutrals";
            else if (comrades.ContainsKey(check_user))
                return "comrades";
            else if (unfriends.ContainsKey(check_user))
                return "unfriends";
            else
                return "";
        }

        public List<User> ReturnUsersRelSortDesc()
        {
            List<User> users = All.Keys.ToList();
            var sortedGr = from i in users
                           orderby All[i].friendness descending
                           select i;
            return sortedGr.ToList();
        }


    }
}
