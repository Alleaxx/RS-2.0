using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class PlayerActions
    {
        public static List<Action> actions_quie = new List<Action>();
        public static List<Action> sucess_actions = new List<Action>();
        public static List<Action> failed_actions = new List<Action>();
        private static Action curr_action;
        public static Action Curr_action
        {
            get
            {
                return curr_action;
            }
            set
            {
                curr_action = value;
            }
        }
        public static bool restart = false;
        public static string log = null;

        public static string ActionsProgress()
        {
            if (actions_quie.Count != 0)
            {
                if (Curr_action == null)
                {
                    restart = false;
                    Curr_action = actions_quie[0];
                    Curr_action.ActionStart();
                }
                if (Curr_action != null)
                {
                    Curr_action.ActionMinutePass();
                }
                else if (Curr_action != null)
                {
                    Curr_action.ActionMinutePass();
                }

                if (CheckActionEnd())
                {
                    if (Curr_action.DoAction())
                    {
                        failed_actions.Add(Curr_action);
                    }
                    else
                    {
                        sucess_actions.Add(Curr_action);
                    }
                    log = Curr_action.result;
                    Curr_action = null;
                    actions_quie.RemoveAt(0);
                }
                else
                {
                    log = null;
                }
            }
            else
            {
                log = null;
            }
            return log;
        }
        public static bool CheckActionEnd()
        {
            if (Curr_action.end_date == Date.current_date)
                return true;
            else
                return false;

        }

        public static void ActionsStop()
        {
            restart = true;
        }
    }
}
