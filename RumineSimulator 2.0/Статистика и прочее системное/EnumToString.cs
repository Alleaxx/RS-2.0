using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EnumToString
    {
        static public string RelationToString(RelationType type)
        {
            switch (type)
            {
                case RelationType.friend:
                    return "Друг";
                case RelationType.comrade:
                    return "Товарищ";
                case RelationType.neutral:
                    return "Нейтрал";
                case RelationType.unfriend:
                    return "Неприятель";
                case RelationType.enemy:
                    return "Враг";
                case RelationType.me:
                    return "Пользователи равны";
                default:
                    return "Некто непонятный";
            }
        }
    }
}
