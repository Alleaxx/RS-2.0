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
        public RelationType relation { get; private set; }
        public List<string> relation_log = new List<string>();

        //Значения дружбы и соперничества
        public int friendness { get; private set; }
        //Интерфейсное представление
        public IntView_Relation InterfaceInfo
        {
            get
            {
                return new IntView_Relation(this);
            }
        }

        public Relation(User init, User goal)
        {
            //начальные параметры
            friendness = 0;

            //Сравниваем параметры, определяем параметр дружбы
            DiffCompareAdeq(init,goal);
            DiffCompareCons(init, goal);
            DiffCompareRakness(init, goal);
            DiffCompareOldness(init, goal);
            DiffCompareGroup(init.group,goal.group);

            //Выводим тип отношения в зависимости от параметра дружбы
            if (friendness >= GlobalParams.friend_condition)
            {
                relation = RelationType.friend;
            }
                
            else if (friendness < GlobalParams.friend_condition && friendness >= GlobalParams.comrade_condition)
            {
                relation = RelationType.comrade;
            }
                
            else if (friendness > GlobalParams.enemy_condition && friendness <= GlobalParams.unfriends_condition)
            {
                relation = RelationType.unfriend;
            }
                
            else if (friendness <= GlobalParams.enemy_condition)
            {
                relation = RelationType.enemy;
            }

            else
            {
                relation = RelationType.neutral;
            }

            if(init == goal)
            {
                relation = RelationType.me;
            }
        }

        //Более подробное сравнение параметров на основе разницы
        private void DiffCompareAdeq(User init, User goal)
        {
            if (init.character.adeq.Value >= 7 && goal.character.adeq.Value >= 7)
            {
                friendness += 2;
                relation_log.Add($"Оба юзера поразительно адекватны. Это их роднит({2})");
            }
            else
            {
                int adeq_difference = init.character.adeq.Value - goal.character.adeq.Value;
                int time_adeq_difference = adeq_difference;
                if (adeq_difference < 0)
                {
                    time_adeq_difference = -adeq_difference;
                }
                int friend_infl = 0;
                switch (time_adeq_difference)
                {
                    case 0:
                        friend_infl = 2;
                        break;
                    case 1:
                        friend_infl = 2;
                        break;
                    case 2:
                        friend_infl = 1;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        friend_infl = -1;
                        break;
                    case 7:
                        friend_infl = -1;
                        break;
                    case 8:
                        friend_infl = -2;
                        break;
                    case 9:
                        friend_infl = -2;
                        break;
                    case 10:
                        friend_infl = -3;
                        break;
                }
                friendness += friend_infl;
                relation_log.Add($"Разница адекватности {time_adeq_difference}, влияние на дружбу ({friend_infl})");

            }
        }
        private void DiffCompareCons(User init, User goal)
        {
            int cons_difference = init.character.conservative.Value - goal.character.conservative.Value;
            int time_cons_difference = cons_difference;
            if (cons_difference < 0)
            {
                time_cons_difference = -cons_difference;
            }
            int friend_infl = 0;
            switch (time_cons_difference)
            {
                case 0:
                    friend_infl = 1;
                    break;
                case 1:
                    friend_infl = 1;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    friend_infl = -1;
                    break;
                case 7:
                    friend_infl = -1;
                    break;
                case 8:
                    friend_infl = -2;
                    break;
                case 9:
                    friend_infl = -3;
                    break;
                case 10:
                    friend_infl = -3;
                    break;
            }
            friendness += friend_infl;
            relation_log.Add($"Разница консервативности {time_cons_difference}, влияние на дружбу ({friend_infl})");
        }
        private void DiffCompareRakness(User init, User goal)
        {
            if (init.character.rakness.Value <= 4 && goal.character.rakness.Value <= 4)
            {
                friendness += 2;
                relation_log.Add($"Оба пользователя не являются раками, что благотворно на них сказывается({+2})");
            }
            else
            {
                int rak_difference = init.character.rakness.Value - goal.character.rakness.Value;
                int time_rak_difference = rak_difference;
                if (rak_difference < 0)
                {
                    time_rak_difference = -rak_difference;
                }
                int friend_infl = 0;
                switch (time_rak_difference)
                {
                    case 0:
                        friend_infl = 3;
                        break;
                    case 1:
                        friend_infl = 2;
                        break;
                    case 2:
                        friend_infl= 1;
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        friend_infl= -2;
                        break;
                    case 6:
                        friend_infl= -4;
                        break;
                    case 7:
                        friend_infl= -6;
                        break;
                    case 8:
                        friend_infl= -6;
                        break;
                    case 9:
                        friend_infl= -7;
                        break;
                    case 10:
                        friend_infl= -8;
                        break;
                }
                friendness += friend_infl;
                relation_log.Add($"Разница раковитости {time_rak_difference}, влияние на дружбу ({friend_infl})");
            }

        }
        private void DiffCompareOldness(User init, User goal)
        {
            int oldness_difference = goal.registration.Year - init.registration.Year;
            int time_oldness_diff = oldness_difference;
            if (oldness_difference < 0)
            {
                time_oldness_diff = -oldness_difference;
            }
            int friend_infl = 0;
            switch (time_oldness_diff)
            {
                case 0:
                    relation_log.Add($"Разницы в олдфажности почти нет, влияния на отношения тоже");
                    break;
                case 1:
                    relation_log.Add($"Разница в олдфажности средняя, влияния на отношения нет");
                    break;
                case 2:
                    if (init.character.tolerance.Value < 5 && init.month_oldness > goal.month_oldness)
                    {
                        friend_infl = -2;
                        relation_log.Add($"Значительная разница в олдфажности и абсолютное отсутствие терпимости!({-2})");
                    }
                    else
                    {
                        relation_log.Add($"Разница в олдфажности большая, но влияния на отношения нет");
                    }

                    break;
            }
            friendness += friend_infl;
        }
        //Умения
        private void DiffCompareCreativeSciense(User init, User goal)
        {
            if(init.character.creativity.Value > 5 && goal.character.creativity.Value > 5)
            {
                relation_log.Add($"Оба пользователя весьма креативны, что улучшает их отношения ({+2})");
                friendness +=2;
            }
            if (init.character.sciense.Value > 5 && goal.character.sciense.Value > 5)
            {
                relation_log.Add($"Оба пользователя являются технарями, что улучшает их отношения ({+2})");
                friendness +=2;
            }
            if (init.character.humanist.Value > 5 && goal.character.humanist.Value > 5)
            {
                relation_log.Add($"Оба пользователя являются гуманитариями, что улучшает их отношения ({+2})");
                friendness += 2;
            }
            if (init.character.historic.Value > 5 && goal.character.historic.Value > 5)
            {
                relation_log.Add($"Оба пользователя ценят историю румине, что улучшает их отношения ({+1})");
                friendness += 1;
            }
            if (init.character.sciense.Value > 5 && goal.character.humanist.Value > 5)
            {
                relation_log.Add($"Война технарей и гуманитариев не обошла стороной и этих пользователей({-1})");
                friendness -= 1;
            }
            if (init.character.humanist.Value > 5 && goal.character.sciense.Value > 5)
            {
                relation_log.Add($"Война гуманитариев и технарей не обошла стороной и этих пользователей({-1})");
                friendness -= 1;
            }
        }

        //Прочие сравнения параметров
        private void DiffCompareGroup(Group init, Group goal)
        {
            if (init == goal)
            {
                relation_log.Add($"У пользователей одинаковая группа, что слегка улучшает их отношения({+1})");
                friendness++;
            }

        }

    }
}
