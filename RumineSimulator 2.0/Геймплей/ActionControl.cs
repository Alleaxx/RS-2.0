using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class ActionControl
    {
        internal static int id_total = 0;
        public static List<Action> ActionQue = new List<Action>();
        public static Action ended_action;



        public static void TimeGoUpdate()
        {
            if (ActionQue.Count > 0)
            {
                if (!ActionQue[0].begin)
                    ActionQue[0].Begin();


                if (ActionQue[0].Procees())
                {
                    ended_action = ActionQue[0];
                    ActionQue.RemoveAt(0);
                    if (ActionQue.Count > 0)
                        ActionQue[0].Begin();
                    Presenter.actionUpdate = true;
                }
                else
                {
                    Presenter.actionUpdate = true;
                }
            }
            else
            {
                Presenter.actionUpdate = false;
            }
        }

        static public void AddAction(Action add)
        {
            ActionQue.Add(add);
            id_total++;
        }
        static public void RemoveAction(int id)
        {
            ActionQue.RemoveAt(id);
        }


        static public Action SearchAction(int id)
        {
            Action act = null;
            for (int i = 0; i < ActionQue.Count; i++)
            {
                if (ActionQue[i].Id == id)
                    return ActionQue[i];
            }
            return act;
        }
    }
    enum ActionType
    {
        newMessage
    }
}
