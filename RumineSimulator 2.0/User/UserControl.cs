using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserControl
    {
        //Список пользователей в приложении и их количество
        static public List<User> Users = new List<User>();
        static public int UserAmount { get; private set; }
        static public List<string> UpdatesUserLog = new List<string>();

        static Random random = new Random();


        //Генерация пользователей в указанном количестве
        static public void GenerateUsers(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                User generated_user = new User();
                Users.Add(generated_user);
                UserAmount++;
            }
        }

        //Генерация отношений для каждого пользователя из списка юзверей
        static public void GenerateRelations()
        {
            for (int i = 0; i < UserAmount; i++)
            {
                Users[i].GenerateRelation();
            }
        }

        static public void CheckingAllUserForUpdates()
        {
            for (int i = 0; i < Users.Count; i++)
            {
                Users[i].CheckingForUpdates();
            }
        }

        static public List<User> ReturnUsersGroupRarenesSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.@group.respect descending
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
                           orderby i.reputation.base_reputation descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersAdeqSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.adeq descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersRakSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.rakness descending
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
                           orderby i.character.Creativity descending
                           select i;
            return sortedGr.ToList();
        }
        static public List<User> ReturnUsersScienseSortDesc()
        {
            var sortedGr = from i in Users
                           orderby i.character.Sciense descending
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
    }
}
