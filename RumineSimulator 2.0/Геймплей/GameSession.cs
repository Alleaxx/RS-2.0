using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class GameSession
    {
        public static bool enabled = false;
        private static int playerId;
        public static int PlayerId { get { return playerId; } }

        public static void GameSessionInit()
        {
            enabled = true;
            playerId = AdvRnd.random.Next(1,UsersControl.all_users.Count);
            ActionControl.AddAction(new Action("Тест", 60,ActionType.newMessage));
            ActionControl.AddAction(new Action("Тест намбер ту", 60,ActionType.newMessage));
            ActionControl.ActionQue.Last().SetDescr("Проверка описания");
        }
    }
}
