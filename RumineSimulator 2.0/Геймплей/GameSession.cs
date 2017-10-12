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
        static Random random = new Random();

        public static void GameSessionInit()
        {
            enabled = true;
            playerId = random.Next(1,UsersControl.all_users.Count);
            ActionControl.AddAction(new Action("Тест", 360));
        }
    }
}
