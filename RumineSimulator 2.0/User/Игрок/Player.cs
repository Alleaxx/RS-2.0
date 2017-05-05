using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Player
    {
        public static int achieve_points { get; set; }
        public static User user;
        public static int access_level;
        private static int boredom;
        public static int Boredom
        {
            get
            {
                return boredom;
            }
            set
            {
                boredom = value;
                if (boredom < -20)
                    boredom = -20;
            }
        }
        public static bool enter_rumine = false;
        

        public static List<User> known_users
        {
            get
            {
                List<User> Known_Users = new List<User>();
                for (int i = 0; i < UserList.Users.Count; i++)
                {
                    if (UserList.Users[i].already_known)
                    {
                        Known_Users.Add(UserList.Users[i]);
                    }
                }
                return Known_Users;
            }
        }


        static Random random = new Random();

        public static void PlayerCreation()
        {
            user = UserList.Users[random.Next(UserList.UserAmount)];
            achieve_points = 0;
            user.already_known = true;
            if (user.group != GroupsList.Groups[GroupsEnum.Moderator])
                access_level = 1;
            if (user.mod)
                access_level = 3;
        }

        public static void UserRecognise()
        {
            foreach (CharParam param in user.character.character_params)
            {
                param.unknown = false;

            }
            //Друзья
            for (int i = 0; i < user.relations.friends.Count; i++)
            {
                foreach (CharParam param in user.relations.friends.ElementAt(i).Key.character.character_params)
                {
                    if (AdvRandom.PersentChanseBool(75))
                        param.unknown = false;

                }
            }
            //Товарищи
            for (int i = 0; i < user.relations.comrades.Count; i++)
            {
                foreach (CharParam param in user.relations.comrades.ElementAt(i).Key.character.character_params)
                {
                    if (AdvRandom.PersentChanseBool(35))
                        param.unknown = false;

                }
            }
            //Нейтралы
            for (int i = 0; i < user.relations.neutrals.Count; i++)
            {
                foreach (CharParam param in user.relations.neutrals.ElementAt(i).Key.character.character_params)
                {
                    if (AdvRandom.PersentChanseBool(10))
                        param.unknown = false;

                }
            }
            //Неприятели
            for (int i = 0; i < user.relations.unfriends.Count; i++)
            {
                foreach (CharParam param in user.relations.unfriends.ElementAt(i).Key.character.character_params)
                {
                    if (AdvRandom.PersentChanseBool(25))
                        param.unknown = false;

                }
            }
            //Враги
            for (int i = 0; i < user.relations.enemies.Count; i++)
            {
                foreach (CharParam param in user.relations.enemies.ElementAt(i).Key.character.character_params)
                {
                    if (AdvRandom.PersentChanseBool(50))
                        param.unknown = false;

                }
            }

        }

    }
}
