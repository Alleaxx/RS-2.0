using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    class Relation
    {
        public User user_init { get; private set; }
        public User user_goal { get; private set; }
        public System.Windows.Media.Color color { get; private set; }
        public RelationsEnum relation { get; private set; }
        public string relation_text { get; private set; }

        //Значения дружбы и соперничества
        public int friendness { get; private set; }
        public int rivalry { get; private set; }

        //Разница в параметрах

        Random random = new Random();

        public Relation(User init, User Goal)
        {
            Statistic.relation_obj++;
            //начальные параметры
            user_init = init;
            user_goal = Goal;
            friendness = 0;

            //Сравниваем параметры и определяем дружбу

            DiffCompareAdeq(user_init.character.adeq.Value,user_goal.character.adeq.Value);
            DiffCompareCons();
            DiffCompareRakness();
            DiffCompareOldness();
            DiffCompareGroup(user_init.group,user_goal.group);

            if (friendness >= GlobalParams.friend_condition)
            {
                relation = RelationsEnum.friend;
            }
                
            else if (friendness < GlobalParams.friend_condition && friendness >= GlobalParams.comrade_condition)
            {
                relation = RelationsEnum.comrade;
            }
                
            else if (friendness > GlobalParams.enemy_condition && friendness <= GlobalParams.unfriends_condition)
            {
                relation = RelationsEnum.unfriend;
            }
                
            else if (friendness <= GlobalParams.enemy_condition)
            {
                relation = RelationsEnum.enemy;
            }
            else
            {
                relation = RelationsEnum.neutral;
            }
            if(user_init == user_goal)
            {
                relation = RelationsEnum.me;
                color = Colors.CadetBlue;
            }
            switch (relation)
            {
                case RelationsEnum.friend:
                    color = Colors.LimeGreen;
                    break;
                case RelationsEnum.comrade:
                    color = Colors.Lime;
                    break;
                case RelationsEnum.neutral:
                    color = Colors.LightSlateGray;
                    break;
                case RelationsEnum.unfriend:
                    color = Colors.IndianRed;
                    break;
                case RelationsEnum.enemy:
                    color = Colors.Red;
                    break;
            }
        }

        //Более подробное сравнение параметров на основе разницы
        private void DiffCompareAdeq(int init, int goal)
        {
            if (user_init.character.adeq.Value >= 7 && user_goal.character.adeq.Value >= 7)
            {
                friendness += 2;
            }
            else
            {
                int adeq_difference = user_init.character.adeq.Value - user_goal.character.adeq.Value;
                int time_adeq_difference = adeq_difference;
                if (adeq_difference < 0)
                {
                    time_adeq_difference = -adeq_difference;
                }

                switch (adeq_difference)
                {
                    case 0:
                        friendness += 2;
                        break;
                    case 1:
                        friendness += 2;
                        break;
                    case 2:
                        friendness += 1;
                        break;
                    case 3:
                        friendness += 0;
                        break;
                    case 4:
                        friendness += 0;
                        break;
                    case 5:
                        friendness += 0;
                        break;
                    case 6:
                        friendness += -1;
                        break;
                    case 7:
                        friendness += -1;
                        break;
                    case 8:
                        friendness += -2;
                        break;
                    case 9:
                        friendness += -2;
                        break;
                    case 10:
                        friendness += -3;
                        break;
                }

            }
        }
        private void DiffCompareCons()
        {
            int cons_difference = user_init.character.conservative.Value - user_goal.character.conservative.Value;
            int time_cons_difference = cons_difference;
            if (cons_difference < 0)
            {
                time_cons_difference = -cons_difference;
            }

            switch (cons_difference)
            {
                case 0:
                    friendness += 2;
                    break;
                case 1:
                    friendness += 1;
                    break;
                case 2:
                    friendness += 0;
                    break;
                case 3:
                    friendness += 0;
                    break;
                case 4:
                    friendness += 0;
                    break;
                case 5:
                    friendness += -1;
                    break;
                case 6:
                    friendness += -1;
                    break;
                case 7:
                    friendness += -1;
                    break;
                case 8:
                    friendness += -2;
                    break;
                case 9:
                    friendness += -2;
                    break;
                case 10:
                    friendness += -3;
                    break;
            }
        }
        private void DiffCompareRakness()
        {
            if (user_init.character.rakness.Value <= 4 && user_goal.character.rakness.Value <= 4)
            {
                friendness += 1;
            }
            else
            {
                int rak_difference = user_init.character.rakness.Value - user_goal.character.rakness.Value;
                int time_rak_difference = rak_difference;
                if (rak_difference < 0)
                {
                    time_rak_difference = -rak_difference;
                }
                switch (time_rak_difference)
                {
                    case 0:
                        friendness += 2;
                        break;
                    case 1:
                        friendness += 2;
                        break;
                    case 2:
                        friendness += 1;
                        break;
                    case 3:
                        friendness += 0;
                        break;
                    case 4:
                        friendness += 0;
                        break;
                    case 5:
                        friendness += -2;
                        break;
                    case 6:
                        friendness += -3;
                        break;
                    case 7:
                        friendness += -5;
                        break;
                    case 8:
                        friendness += -6;
                        break;
                    case 9:
                        friendness += -6;
                        break;
                    case 10:
                        friendness += -7;
                        break;
                }
            }

        }
        private void DiffCompareOldness()
        {
            int oldness_difference = user_goal.registration.Year - user_init.registration.Year;
            int time_oldness_diff = user_goal.registration.Year - user_init.registration.Year;
            if (oldness_difference < 0)
            {
                time_oldness_diff = -oldness_difference;
            }
            switch (time_oldness_diff)
            {
                case 0:
                    friendness += 0;
                    break;
                case 1:
                    friendness += 0;
                    break;
                case 2:
                    if (user_init.character.tolerance.Value < 5 && user_init.m_oldness > user_goal.m_oldness)
                        friendness -= 1;
                    break;
            }
        }
        private void DiffCompareCreativeSciense(int init, int goal)
        {
            if(user_init.character.creativity.Value > 5 && user_goal.character.creativity.Value > 5)
            {
                friendness++;
            }
            if (user_init.character.sciense.Value > 5 && user_goal.character.sciense.Value > 5)
            {
                friendness++;
            }
        }

        //Прочие сравнения параметров
        private void DiffCompareGroup(Group init, Group goal)
        {
            if (user_init.group == user_goal.group)
                friendness++;
        }
        public string ReturnTextRelation()
        {
            switch (relation)
            {
                case RelationsEnum.friend:
                    return "Друг";
                case RelationsEnum.comrade:
                    return "Товарищ";
                case RelationsEnum.neutral:
                    return "Нейтрал";
                case RelationsEnum.unfriend:
                    return "Неприятель";
                case RelationsEnum.enemy:
                    return "Враг";
            }
            return "hz";

        }

    }
}
