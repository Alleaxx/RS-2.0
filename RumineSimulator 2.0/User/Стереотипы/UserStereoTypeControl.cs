using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserStereoTypeControl
    {
        //Список уже созданных стереотипов
        static public Dictionary<StereoTypesEnum, UserStereoType> UserTypes = new Dictionary<StereoTypesEnum, UserStereoType>();
        const short newf_cap = 0;
        const short sredn_cap = 8;
        const short old_cap = 20;

        static Random random = new Random();

        
        static public void UserStereoTypesInitCreation()
        {
            #region Объявление стандартных характеров

            #region Ньюфаги
            #region Обыкновенный ньюфаг
            UserStereoType NewfagUsual = new UserStereoType("Типичный ньюфаг",StereoTypesEnum.NewfagUsual,newf_cap,3,7,0,6, UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagUsual.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagUsual.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            NewfagUsual.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            NewfagUsual.AvailableGroups = RarenesSort(NewfagUsual.AvailableGroups);           
            UserTypes.Add(StereoTypesEnum.NewfagUsual, NewfagUsual);
            #endregion
            #region Очень разумный ньюфаг
            UserStereoType NewfagCalm = new UserStereoType("Рассчетливый ньюфаг", StereoTypesEnum.NewfagCalm, newf_cap+2, 7, 10, 0, 4, UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagCalm.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagCalm.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            NewfagCalm.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            NewfagCalm.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            NewfagCalm.AvailableGroups = RarenesSort(NewfagCalm.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.NewfagCalm, NewfagCalm);
            #endregion
            #region Ньюфаг-рак
            UserStereoType NewfagNoob = new UserStereoType("Нуб", StereoTypesEnum.NewfagRak, newf_cap, 0, 4,6,10, UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagNoob.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            NewfagNoob.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            NewfagNoob.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            NewfagNoob.AvailableGroups = RarenesSort(NewfagNoob.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.NewfagRak, NewfagNoob);
            #endregion
            #region Ньюфаг-тролль
            UserStereoType NewfTroll = new UserStereoType("Тролль", StereoTypesEnum.NewfagTroll, newf_cap, 0, 6,6,8, UserGroupsControler.Groups[GroupsList.Poset]);
            NewfTroll.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            NewfTroll.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            NewfTroll.AvailableGroups = RarenesSort(NewfTroll.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.NewfagTroll, NewfTroll);
            #endregion
            #region Школолоша
            UserStereoType NewSchool = new UserStereoType("Школолоша", StereoTypesEnum.NewfagSchool, newf_cap, 5, 11, 5, 11, UserGroupsControler.Groups[GroupsList.Poset]);
            NewSchool.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            NewSchool.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            NewSchool.AvailableGroups = RarenesSort(NewSchool.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.NewfagSchool, NewSchool);
            #endregion
            #endregion
            #region Среднефаги
            #region Обычный пользователь
            UserStereoType UsualUser = new UserStereoType("Обычный пользователь", StereoTypesEnum.UserUsual, sredn_cap, 3, 6,0,6, UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UsualUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            UsualUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            UsualUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UsualUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            UsualUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            UsualUser.AvailableGroups = RarenesSort(UsualUser.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.UserUsual, UsualUser);
            #endregion
            #region Карьерист
            UserStereoType UsualAdvUser = new UserStereoType("Карьерист", StereoTypesEnum.UserAdvanced, sredn_cap+4, 7, 10, 0, 4, UserGroupsControler.Groups[GroupsList.Journalist]);
            UsualAdvUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UsualAdvUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            UsualAdvUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            UsualAdvUser.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Moderator]);
            UsualAdvUser.AvailableGroups = RarenesSort(UsualAdvUser.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.UserAdvanced, UsualAdvUser);
            #endregion
            #region Обычный рак
            UserStereoType UsualRak = new UserStereoType("Рак", StereoTypesEnum.UserRak, sredn_cap, 0, 5,6,10, UserGroupsControler.Groups[GroupsList.Poset]);
            UsualRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            UsualRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            UsualRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UsualRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            UsualRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            UsualRak.AvailableGroups = RarenesSort(UsualRak.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.UserRak, UsualRak);
            #endregion
            #region Рьяный рак
            UserStereoType UserRakAdv = new UserStereoType("Рьяный рак", StereoTypesEnum.UserRakAdvanced, sredn_cap, 2, 6, 6, 10, UserGroupsControler.Groups[GroupsList.Poset]);
            UserRakAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            UserRakAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            UserRakAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UserRakAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            UserRakAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            UserRakAdv.AvailableGroups = RarenesSort(UserRakAdv.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.UserRakAdvanced, UserRakAdv);
            #endregion
            #region Навязчивый рачок
            UserStereoType UserSchoolAdv = new UserStereoType("Навязчивый рачок", StereoTypesEnum.UserSchoolAdv, sredn_cap, 5, 11, 5, 11, UserGroupsControler.Groups[GroupsList.Journalist]);
            UserSchoolAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            UserSchoolAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            UserSchoolAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.XXXLJGP]);
            UserSchoolAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Journalist]);
            UserSchoolAdv.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.ModeratorBezdna]);
            UserSchoolAdv.AvailableGroups = RarenesSort(UserSchoolAdv.AvailableGroups);
            UserTypes.Add(StereoTypesEnum.UserSchoolAdv, UserSchoolAdv);
            #endregion
            #endregion
            #region Олдфаги
            #region Хороший олдфаг
            UserStereoType Oldfag = new UserStereoType("Олдфаг", StereoTypesEnum.OldfagUsual, old_cap, 4, 9,0,5, UserGroupsControler.Groups[GroupsList.Oldfag]);
            Oldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            Oldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JournalistOldfag]);
            Oldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Oldfag]);
            Oldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Moderator]);
            Oldfag.AvailableGroups = RarenesSort(Oldfag.AvailableGroups);

            UserTypes.Add(StereoTypesEnum.OldfagUsual, Oldfag);
            #endregion
            #region Скатившийся олдфаг
            UserStereoType OldfagBad = new UserStereoType("Скатившийся олдфаг", StereoTypesEnum.OldfagBad, old_cap, 4, 11, 6, 11, UserGroupsControler.Groups[GroupsList.Oldfag]);
            OldfagBad.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            OldfagBad.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JournalistOldfag]);
            OldfagBad.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Oldfag]);
            OldfagBad.AvailableGroups = RarenesSort(OldfagBad.AvailableGroups);

            UserTypes.Add(StereoTypesEnum.OldfagBad, OldfagBad);
            #endregion
            #region Чокнутый олдфаг
            UserStereoType MadOldfag = new UserStereoType("Чокнутый олдфаг", StereoTypesEnum.OldfagMad, old_cap, 0, 6,0,6, UserGroupsControler.Groups[GroupsList.JGP]);
            MadOldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            MadOldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            MadOldfag.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Oldfag]);
            MadOldfag.AvailableGroups = RarenesSort(MadOldfag.AvailableGroups);

            UserTypes.Add(StereoTypesEnum.OldfagMad, MadOldfag);
            #endregion
            #region Матерый рак
            UserStereoType TrueRak = new UserStereoType("Матерый рак", StereoTypesEnum.OldfagRak, old_cap, 0, 5,7,10, UserGroupsControler.Groups[GroupsList.Poset]);
            TrueRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            TrueRak.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            TrueRak.AvailableGroups = RarenesSort(TrueRak.AvailableGroups);

            UserTypes.Add(StereoTypesEnum.OldfagRak, TrueRak);
            #endregion
            #endregion

            #region Нечто
            UserStereoType SomethingHZ = new UserStereoType("Нечто", StereoTypesEnum.Something, 0, 0, 11, 0, 11, UserGroupsControler.Groups[GroupsList.Poset]);
            SomethingHZ.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.Poset]);
            SomethingHZ.AvailableGroups.Add(UserGroupsControler.Groups[GroupsList.JGP]);
            SomethingHZ.AvailableGroups = RarenesSort(SomethingHZ.AvailableGroups);

            UserTypes.Add(StereoTypesEnum.Something, SomethingHZ);
            #endregion

            #endregion
        }

        //Выдача стереотипов в зависимости от параметров
        static public UserStereoType UserChooseStereType(User user)
        {
            List<UserStereoType> sorted_list = OldnessSort(UserTypes);
            for (int i = 0; i < UserTypes.Count; i++)
            {
                if(user.m_oldness >= sorted_list[i].req_oldness)
                {
                    if( (user.character.adeq >= sorted_list[i].req_min_adeq) && (user.character.adeq <= sorted_list[i].req_max_adeq) )
                    {
                        if ( (user.character.rakness >= sorted_list[i].req_min_rakness) && (user.character.rakness <= sorted_list[i].req_max_rakness) )
                        {
                            return sorted_list[i];
                        }
                    }
                }
            }
            return UserTypes[StereoTypesEnum.Something];
        }

        //Сортировка групп по редкости(при рандомном выборе групп сначала будут проверяться наиболее "редкие")
        static List<UserGroup> RarenesSort(List<UserGroup> list)
        {
            var sortedGr = from i in list
                           orderby i.rareness descending
                           select i;
            return sortedGr.ToList();
        }
        static List<UserStereoType> OldnessSort(Dictionary<StereoTypesEnum, UserStereoType> dict)
        {
            List<UserStereoType> types = dict.Values.ToList();
            var sortedGr = from i in types
                           orderby i.req_oldness descending
                           select i;
            return sortedGr.ToList();

        }
    }
}
