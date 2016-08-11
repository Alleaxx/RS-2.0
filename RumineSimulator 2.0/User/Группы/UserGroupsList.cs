using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserGroupsControler
    {
        //Список уже добавленных групп
        static public Dictionary<GroupsList, UserGroup> Groups = new Dictionary<GroupsList, UserGroup>();

        static Random random = new Random();

        static public void UserGroupsInitCreation()
        {
            #region Объявление стандартных групп
            //Посетители
            UserGroup Poset = new UserGroup("Посетители",2,4, "#9CA1A5");
            Groups.Add(GroupsList.Poset, Poset);

            //Просто хорошие люди
            UserGroup JGP = new UserGroup("ПХЛ", 5,3, "#FFA500");
            Groups.Add(GroupsList.JGP, JGP);

            //XXXL ПХЛ
            UserGroup XXXLJGP = new UserGroup("XXXL ПХЛ", 7,3, "#A0522D");
            Groups.Add(GroupsList.XXXLJGP, XXXLJGP);

            //Модератор бездны
            UserGroup ModeratorBezdna = new UserGroup("Модератор бездны", 6,5, "#999900", false,true);
            Groups.Add(GroupsList.ModeratorBezdna, ModeratorBezdna);

            //Олдфаги
            UserGroup Oldfag = new UserGroup("Олдфаги", 10,4, "#000099");
            Groups.Add(GroupsList.Oldfag, Oldfag);

            //Журналисты
            UserGroup Journalist = new UserGroup("Журналисты",8,4, "#009900", true);
            Groups.Add(GroupsList.Journalist, Journalist);

            //Журналисты-олдфаги
            UserGroup JournalistOldfag = new UserGroup("Журналисты-олдфаги",12, 5, "#2F4F4F", true);
            Groups.Add(GroupsList.JournalistOldfag, JournalistOldfag);

            //Модератор
            UserGroup Moderator = new UserGroup("Модератор",20,9, "#1ca9c9", true,false,true);
            Groups.Add(GroupsList.Moderator, Moderator);
            #endregion
        }

        //Выбираем пользователю группу на основе доступных ему групп
        static public UserGroup ReturnUserGroup(User selected_user)
        {
            UserGroup selected_group = null;
            for (int i = 0; i < selected_user.stereotype.AvailableGroups.Count; i++)
            {
                if (random.Next(selected_user.stereotype.AvailableGroups[i].rareness) == 0)
                {
                    selected_group = selected_user.stereotype.AvailableGroups[i];
                    return selected_group;
                }                
            }
            selected_group = selected_user.stereotype.standartGroup;
            return selected_group;
        }

    }
}
