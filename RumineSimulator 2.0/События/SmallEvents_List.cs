using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class SmallEvents_List
    {
        static Random random = new Random();
        static List<string> Roles_Creator = new List<string> {"Изобретал и ваял","Думал чего бы поделать","Внёс свою лепту в развитие сайта",
        "Находился в поиске пути","Валял дурака"};
        static List<string> Roles_moderator = new List<string> {"За справедливость!","Я на страже порядка","Нарушители понесут наказание",
        "Кто  не согласен - тому бан","Мой банхаммер наготове"};
        static List<string> Roles_moderator_victim = new List<string> {"Я хотел сделать как лучше!","Они ничего не смыслят в румине","Я делаю то, что должно",
        "Наше дело правое, победа будет за нами","Голос правды не угаснет никогда"};
    }
}
