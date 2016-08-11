using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserTraitsList
    {
        static List<UserTrait> AllTraits = new List<UserTrait>();
        public static void TraitsInit()
        {
            AllTraits.Add(new UserTrait
                (5,"Художник", "Весьма творческий юзверь, пилящий арты. Получает прибавку к креативности",UserParamsEnum.creativity,3,2));
            AllTraits.Add(new UserTrait
                (5, "Программист", "Этот парень дельно(а может и не очень) кодит. Бафф к науке",UserParamsEnum.sciense,3,2));
        }

        public static List<UserTrait> ReturnTraits(User user)
        {
            List<UserTrait> traits_user = new List<UserTrait>();
            foreach(UserTrait trait in AllTraits)
            {
                if (AdvRandom.PersentChanseBool(trait.chanse) && ConditionChecker(user, trait))
                {
                    trait.UserChange(user);
                    traits_user.Add(trait);
                }                   
            }
            return traits_user;
        }
        public static bool ConditionChecker(User user, UserTrait trait)
        {
            switch (trait.influence_param)
            {
                case UserParamsEnum.creativity:
                    if (user.character.Creativity > trait.condition_param)
                        return true;
                    break;
                case UserParamsEnum.sciense:
                    if (user.character.Sciense > trait.condition_param)
                        return true;
                    break;
            }
            return false;
        }
    }
}
