using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace RumineSimulator_2._0
{
    class UserRelation
    {
        public User user_init { get; private set; }
        public User user_goal { get; private set; }
        public System.Windows.Media.Color color { get; private set; }
        public RelationsEnum relation { get; private set; }
        public string description { get; private set; }

        public string text_form { get; private set; }

        //Значения дружбы и соперничества
        public int friendness { get; private set; }
        public int rivalry { get; private set; }

        //Условия получения определенного отношение(нестрогое сравнение)
        public const int friend_condition = 4;
        public const int comrade_condition = 2;
        public const int unfriends_condition = -1;
        public const int enemy_condition = -4;
        public const int rival_condition = 5;

        //Разница в параметрах
        public int cons_difference { get; private set; }
        public int adeq_difference { get; private set; }
        public int rak_difference { get; private set; }
        public int oldness_difference { get; private set; }
        public int creativity_difference { get; private set; }
        public int sciense_difference { get; private set; }

        Random random = new Random();

        public UserRelation(User init, User Goal)
        {
            //начальные параметры
            user_init = init;
            user_goal = Goal;
            friendness = 0;

            #region Определяем дружбу
            //Сравниваем адекватность
            if (init.character.adeq == Goal.character.adeq)
            {
                adeq_difference = 0;
            }

            else if (init.character.adeq > Goal.character.adeq)
            {
                adeq_difference = init.character.adeq - Goal.character.adeq;
            }
            else
            {
                adeq_difference = -(Goal.character.adeq - init.character.adeq);
            }

            //Сравниваем консервативность
            if (init.character.conservative == Goal.character.conservative)
            {
                cons_difference = 0;
            }

            else if (init.character.conservative > Goal.character.conservative)
            {
                cons_difference = init.character.conservative - Goal.character.conservative;
            }
            else
            {
                cons_difference = -(Goal.character.conservative - init.character.conservative);
            }

            //Сравниваем раковитость
            if (init.character.rakness == Goal.character.rakness)
            {
                rak_difference = 0;
            }

            else if (init.character.rakness > Goal.character.rakness)
            {
                rak_difference = init.character.rakness - Goal.character.rakness;
            }
            else
            {
                rak_difference = -(Goal.character.rakness - init.character.rakness);
            }

            //Сравниваем олдфажность
            if (init.registration.Year == Goal.registration.Year)
            {
                oldness_difference = 0;
            }

            else if (init.registration.Year > Goal.registration.Year)
            {
                oldness_difference = init.registration.Year - Goal.registration.Year;
            }
            else
            {
                oldness_difference = -(Goal.registration.Year - init.registration.Year);
            }

            DiffCompareAdeq();
            DiffCompareCons();
            DiffCompareRakness();
            DiffCompareStereotype();
            DiffCompareOldness();
            DiffCompareGroup();
            #endregion

            #region Определяем соперничество
            //Одинаковые группы
            if (user_init.group == user_goal.group)
                rivalry++;
            //Соперничество журналистов
            if ( (user_init.news + 5 > user_goal.news && user_init.news-5 < user_goal.news) && user_init.group.journ && user_goal.group.journ)
                rivalry += 3;
            //Близкое количество сообщений
            if ( (user_init.messages + 200 > user_goal.messages) && (user_init.messages - 200 < user_goal.messages) && user_init.messages > 500)
                rivalry++;
            //Близкое количество симпатий
            if ( (user_init.likes + 200 > user_goal.likes) && (user_init.likes - 200 < user_goal.likes) && user_init.likes > 500)
                rivalry++;
            //Близкое количество коментов
            if ( (user_init.comments + 25 > user_goal.comments) && (user_init.comments - 25 < user_goal.comments) && user_init.comments > 100)
                rivalry++;
            if (user_init.character.Creativity > 5 && user_goal.character.Sciense > 5)
                rivalry+=2;
            if (user_init.character.Sciense > 5 && user_goal.character.Creativity > 5)
                rivalry+=2;
            #endregion

            if (friendness >= friend_condition)
            {
                relation = RelationsEnum.friend;
                color = System.Windows.Media.Colors.LimeGreen;
            }
                
            else if (friendness < friend_condition && friendness >= comrade_condition)
            {
                relation = RelationsEnum.comrade;
                color = System.Windows.Media.Colors.LightGreen;
            }
                
            else if (friendness > enemy_condition && friendness <= unfriends_condition)
            {
                relation = RelationsEnum.unfriend;
                color = System.Windows.Media.Colors.LightCoral;
            }
                
            else if (friendness <= enemy_condition)
            {
                relation = RelationsEnum.enemy;
                color = System.Windows.Media.Colors.Crimson;
            }
            else
            {
                relation = RelationsEnum.neutral;
                color = System.Windows.Media.Colors.LightGray;
            }
            if(user_init == user_goal)
            {
                relation = RelationsEnum.me;
                color = System.Windows.Media.Colors.CadetBlue;
            }
        }

        //Более подробное сравнение параметров на основе разницы
        private void DiffCompareAdeq()
        {
            if (user_init.character.adeq >= 7 && user_goal.character.adeq >= 7)
            {
                friendness += 2;
            }
            else
            {
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
            if (user_init.character.rakness <= 4 && user_goal.character.rakness <= 4)
            {
                friendness += 1;
            }
            else
            {
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
            int time_oldness_diff = oldness_difference;
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
                    if (user_init.character.tolerance < 5 && user_init.m_oldness > user_goal.m_oldness)
                        friendness -= 1;
                    break;
            }
        }
        private void DiffCompareCreativeSciense()
        {
            if(user_init.character.Creativity > 5 && user_goal.character.Creativity > 5)
            {
                friendness++;
            }
            if (user_init.character.Sciense > 5 && user_goal.character.Sciense > 5)
            {
                friendness++;
            }
        }

        //Прочие сравнения параметров
        private void DiffCompareStereotype()
        {
            if (user_init.stereotype == user_goal.stereotype)
                friendness++;
            else
                friendness--;
        }
        private void DiffCompareGroup()
        {
            if (user_init.group == user_goal.group)
                friendness++;
        }


    }
}
