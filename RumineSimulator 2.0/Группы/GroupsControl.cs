using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class GroupsControl
    {
        //Список уже добавленных групп
        static public List<Group> groups = new List<Group>();

        static Random random = new Random();

        static public void UserGroupsInitCreation()
        {
            //Посетители
            Group Poset = new Group("Посетители", 2, 4, "#9CA1A5", GroupsType.Poset);
            Poset.SetCondition(new GroupCondition(0, 0, 0, 0, -100000));
            Poset.SetPossibilieties(false, false, false, false, false);
            groups.Add(Poset);

            //Просто хорошие люди
            Group JGP = new Group("ПХЛ", 5, 3, "#FFA500", GroupsType.JGP);
            JGP.SetCondition(new GroupCondition(0.6F, 0, 2, 0, 0));
            JGP.SetPossibilieties(false, false, false, false, false);
            groups.Add(JGP);

            //XXXL ПХЛ
            Group XXXLJGP = new Group("XXXL ПХЛ", 7, 3, "#A0522D", GroupsType.XXXLJGP);
            XXXLJGP.SetCondition(new GroupCondition(1, 0, 3, 25, 0));
            XXXLJGP.SetPossibilieties(false, false, false, false, false);
            groups.Add(XXXLJGP);

            //Модератор бездны
            Group ModeratorBezdna = new Group("Модератор бездны", 6, 5, "#999900", GroupsType.ModeratorBezdna);
            ModeratorBezdna.SetCondition(new GroupCondition(0, 0, 8, 30, 0));
            ModeratorBezdna.SetPossibilieties(false, true, false, false, false);
            groups.Add(ModeratorBezdna);

            //Олдфаги
            Group Oldfag = new Group("Олдфаги", 10, 4, "#000099", GroupsType.Oldfag);
            Oldfag.SetCondition(new GroupCondition(1, 11, 0, 0, 0));
            Oldfag.SetPossibilieties(false, false, false, false, false);
            groups.Add(Oldfag);

            //Журналисты
            Group Journalist = new Group("Журналисты", 8, 4, "#009900", GroupsType.Journalist);
            Journalist.SetCondition(new GroupCondition(0F, 0, 8, 40, 0));
            Journalist.SetPossibilieties(true, false, false, false, false);
            groups.Add(Journalist);

            //Журналисты-олдфаги
            Group JournalistOldfag = new Group("Журналисты-олдфаги", 12, 5, "#2F4F4F", GroupsType.JournalistOldfag);
            JournalistOldfag.SetCondition(new GroupCondition(0.9F, 11, 8, 45, 0));
            JournalistOldfag.SetPossibilieties(true, false, false, false, false);
            groups.Add(JournalistOldfag);

            //Модератор
            Group Moderator = new Group("Модератор", 20, 9, "#1ca9c9", GroupsType.Moderator);
            Moderator.SetCondition(new GroupCondition(0, 0, 0, 0, 0, true));
            Moderator.SetPossibilieties(true, false, true, false, false);
            groups.Add(Moderator);

            //Администратор
            Group Admin = new Group("Администратор", 40, 25, "#FFD8131C", GroupsType.Admin);
            Admin.SetCondition(new GroupCondition(0, 0, 0, 0, 0, true));
            Admin.SetPossibilieties(true, true, true, true, true);
            groups.Add(Admin);
        }


        //Возвращает случайную группу
        static public Group ReturnUserRandomGroup()
        {
            List<Group> available_groups = new List<Group>();
            available_groups = groups;
            available_groups = SortByRespect(available_groups);
            available_groups.Reverse();
            foreach (Group group in available_groups)
            {
                if (AdvRnd.PrsChanse(50))
                    return group;
            }
            return available_groups[0];
        }
        //Выдача пользователю группы, включающая в себя все доступные параметры
        static public Group ReturnUserGroup(User user)
        {
            //Заполняем доступные группы
            List<Group> available_groups = new List<Group>();
            for (int i = 0; i < groups.Count; i++)
            {
                if (!CheckGroupConditionFailed(user, groups[i]))
                    available_groups.Add(groups[i]);
            }
            available_groups = SortByRespect(available_groups);

            #region Выдача за счет характера пользователя

            //Властный - выдается лучшая группа из доступных
            if (user.traits.Contains(TraitsList.SearchTrait(TraitsType.Wpower)))
            {
                return available_groups[0];
            }
            //Аскет - выдается худшая группа из доступных
            if (user.traits.Contains(TraitsList.SearchTrait(TraitsType.buddist)))
            {
                return available_groups[available_groups.Count - 1];
            }
            //Любитель новостей - выдается журналист или мб
            if (user.traits.Contains(TraitsList.SearchTrait(TraitsType.newslover)))
            {
                foreach (Group group in available_groups)
                {
                    if (group.Journ)
                        return group;
                    if (group.Mod_bezdn)
                        return group;
                }
            }
            //Чистильщик бездны - выдается МБ, если доступен
            if (user.traits.Contains(TraitsList.SearchTrait(TraitsType.vilka)))
            {
                foreach (Group group in available_groups)
                {
                    if (group.Mod_bezdn)
                        return group;
                }
            }

            #endregion

            #region Рандом под конец, если трейты не влияют
            foreach (Group group in available_groups)
            {
                if (AdvRnd.PrsChanse(20))
                    return group;
            }
            return available_groups[random.Next(available_groups.Count)];
            #endregion

        }

        //Возвращает true, если пользователь не проходит по требованиям группы
        static private bool CheckGroupConditionFailed(User user, Group group)
        {
            if (group.condition.special_condition)
                return true;
            else
            {
                if (user.karma.karma < group.condition.Karma)
                    return true;
                if (user.month_oldness < group.condition.month_oldness)
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

        //Поиск группы по типу и id
        static public Group GroupSearch(int id)
        {
            Group group = groups[0];
            foreach (Group group_s in groups)
            {
                if (group_s.id == id)
                    group = group_s;
            }
            return group;
        }
        static public Group GroupSearch(GroupsType type)
        {
            Group group = groups[0];
            foreach (Group group_s in groups)
            {
                if (group_s.type == type)
                    group = group_s;
            }
            return group;
        }


        //Выдача групп с особыми условиями
        #region Модераторы
        static public void ModerChoose()
        {
            //Число рекомендуемых модераторов - кол-во пользователей / 20 + рандом
            int mod_recomend = UsersControl.act_users.Count / 20;
            mod_recomend += random.Next(0, 3);
            int need_amount = mod_recomend - (int)UsersControl.moderAmount;
            int i = 0;
            //Предполагаемые модератор - список с наибольшим шансом модератора наверху
            List<User> poss_mods = UsersControl.ReturnUsersModerChanseDesc();
            //Проверять шанс пользователей пока число модераторов не достигнет рекомендуемого
            do
            {
                if (AdvRnd.PrsChanse(poss_mods[i].moder_chanse) && poss_mods[i].admin != true)
                {
                    poss_mods[i].group = GroupSearch(GroupsType.Moderator);
                    poss_mods[i].mod = true;
                }
                i++;
                if(i == poss_mods.Count - 2)
                {
                    i = 0;
                }
            }
            while (UsersControl.moderAmount != mod_recomend);
        }
        #endregion


        static private List<Group> SortByRespect(List<Group> list)
        {
            var sortedGr = from i in list
                           orderby i.Respect descending
                           select i;
            return sortedGr.ToList();
        }
    }
}
