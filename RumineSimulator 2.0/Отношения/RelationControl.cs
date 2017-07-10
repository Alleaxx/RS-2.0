using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class RelationControl
    {
        //Владелец отношений
        public User owner { get; private set; }
        //Интерфейсное представление
        public IntView_RelationControl InterfaceInfo
        {
            get
            {
                return new IntView_RelationControl(this);
            }
        }

        //Инициализация
        public RelationControl(User main_user)
        {
            owner = main_user;
        }

        //Возвращает состояние отношений с заданным пользователем
        public RelationType RelationStateReturn(User user_rel)
        {
            Statistic.relation_obj++;
            Relation relation = new Relation(owner, user_rel);
            //Друзья
            if(user_rel == owner)
            {
                return RelationType.me;
            }
            else if (relation.friendness >= GlobalParams.friend_condition)
            {
                return RelationType.friend;
            }

            //Товарищи
            else if ((relation.friendness < GlobalParams.friend_condition) && (relation.friendness >= GlobalParams.comrade_condition))
            {
                return RelationType.comrade;
            }

            //Неприятели
            else if ((relation.friendness > GlobalParams.enemy_condition) && (relation.friendness <= GlobalParams.unfriends_condition))
            {
                return RelationType.unfriend;
            }

            //Враги
            else if (relation.friendness <= GlobalParams.enemy_condition)
            {
                return RelationType.enemy;
            }
            //Нейтралы
            else
            {
                return RelationType.neutral;
            }
        }
        //Количество пользователей в данной категории состояния
        public List<User> RelationCountUsersReturn(RelationType type)
        {
            List<User> users = new List<User>();
            foreach (User user in UsersControl.Users)
            {
                if(RelationStateReturn(user) == type)
                {
                    users.Add(user);
                }                    
            }
            return users;
        }
        //Возврат отношения
        public Relation RelationReturn(User user_rel)
        {
            return new Relation(owner, user_rel);
        }
    }
}
