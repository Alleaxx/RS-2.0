using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class GroupsList
    {
        //Список уже добавленных групп
        static public Dictionary<GroupsEnum, Group> Groups = new Dictionary<GroupsEnum, Group>();

        static Random random = new Random();

        static public void UserGroupsInitCreation()
        {
            //Посетители
            Group Poset = new Group("Посетители",2,4, "#9CA1A5",new GroupCondition(0,0,0,0,-100000));
            Groups.Add(GroupsEnum.Poset, Poset);

            //Просто хорошие люди
            Group JGP = new Group("ПХЛ", 5,3, "#FFA500", new GroupCondition(0.6F, 0, 2, 0, 0));
            Groups.Add(GroupsEnum.JGP, JGP);

            //XXXL ПХЛ
            Group XXXLJGP = new Group("XXXL ПХЛ", 7,3, "#A0522D", new GroupCondition(1, 0, 3, 25, 0));
            Groups.Add(GroupsEnum.XXXLJGP, XXXLJGP);

            //Модератор бездны
            Group ModeratorBezdna = new Group("Модератор бездны", 6,5, "#999900", new GroupCondition(0, 0, 8, 30, 0), false,true);
            Groups.Add(GroupsEnum.ModeratorBezdna, ModeratorBezdna);

            //Олдфаги
            Group Oldfag = new Group("Олдфаги", 10,4, "#000099", new GroupCondition(1, 11, 0, 0, 0));
            Groups.Add(GroupsEnum.Oldfag, Oldfag);

            //Журналисты
            Group Journalist = new Group("Журналисты",8,4, "#009900", new GroupCondition(0F, 0, 8, 40, 0), true);
            Groups.Add(GroupsEnum.Journalist, Journalist);

            //Журналисты-олдфаги
            Group JournalistOldfag = new Group("Журналисты-олдфаги",12, 5, "#2F4F4F", new GroupCondition(0.9F,11, 8, 45, 0), true);
            Groups.Add(GroupsEnum.JournalistOldfag, JournalistOldfag);

            //Модератор
            Group Moderator = new Group("Модератор",20,9, "#1ca9c9", new GroupCondition(0, 0, 0, 0, 0,true), true,false,true);
            Groups.Add(GroupsEnum.Moderator, Moderator);
        }

        static public Group ReturnRandomGroup()
        {
            List<Group> available_groups = new List<Group>();
            for (int i = 0; i < Groups.Count; i++)
            {
                available_groups.Add(Groups.ElementAt(i).Value);
            }
            available_groups = SortByRareness(available_groups);
            available_groups.Reverse();
            foreach (Group group in available_groups)
            {
                if (AdvRandom.PersentChanseBool(50))
                    return group;
            }
            return available_groups[0];
        }
        static public Group ReturnUserGroup(User user)
        {
            //Заполняем доступные группы
            List<Group> available_groups = new List<Group>();
            for (int i = 0; i < Groups.Count; i++)
            {
                if (!CheckGroupConditionFailed(user, Groups.ElementAt(i).Value))
                    available_groups.Add(Groups.ElementAt(i).Value);
            }
            available_groups = SortByRareness(available_groups);

            //Выдаем
            #region Проверка трейтов
            if (user.traits.Contains(TraitsList.AllTraits[Traits.Wpower]))
            {
                return available_groups[0];
            }
            if (user.traits.Contains(TraitsList.AllTraits[Traits.asket]))
            {
                return available_groups[available_groups.Count-1];
            }
            if (user.traits.Contains(TraitsList.AllTraits[Traits.newslover]))
            {
                foreach (Group group in available_groups)
                {
                    if (group.Journ)
                        return group;
                }
            }
            if (user.traits.Contains(TraitsList.AllTraits[Traits.vilka]))
            {
                foreach (Group group in available_groups)
                {
                    if (group.Mod_bezdn)
                        return group;
                }
            }
            #endregion

            #region Рандом
            foreach (Group group in available_groups)
            {
                if (AdvRandom.PersentChanseBool(50))
                    return group;
            }
            return available_groups[random.Next(available_groups.Count)];
            #endregion

        }

        static private bool CheckGroupConditionFailed(User user,Group group)
        {
            if (group.condition.Mod)
                return true;
            else
            {
                if (user.karma.karma < group.condition.Karma)
                    return true;
                if (user.m_oldness < group.condition.M_oldness)
                    return true;
                if (user.news < group.condition.News)
                    return true;
                if (user.news_quality < group.condition.News_quality)
                    return true;
                if (user.reputation.Base_reputation < group.condition.Reputation)
                    return true;
            }
            return false;
        }

        static private List<Group> SortByRareness(List<Group> list)
        {
            var sortedGr = from i in list
                           orderby i.Respect descending
                           select i;
            return sortedGr.ToList();
        }
    }
}
