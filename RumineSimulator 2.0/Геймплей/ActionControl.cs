using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class ActionControl
    {
        //События на завершение действия и обновление оставшегося времени
        public static event EventHandler<ActionEventArgs> ActionEnded;
        public static event EventHandler<ActionEventArgs> ActionDurChanged;

        internal static int id_total = 0;
        public static List<Action> ActionQue = new List<Action>();
        public static Action ended_action;
        public static List<ActionType> action_presets = new List<ActionType>();


        public static void ActionsInit()
        {
            action_presets.Add(ActionType.newMessage);
        }


        //Вызывается при прохождении одной минуты
        public static void TimeGoUpdate()
        {
            if (ActionQue.Count > 0)
            {
                if (!ActionQue[0].Procees())
                    ActionDurChanged(ActionQue[0],new ActionEventArgs("Изменение длительности"));
            }
        }

        //Вызывается при завершении действия
        public static void ActionEndUpdate(object act_obj,ActionEventArgs args)
        {
            Action act = (Action)act_obj;

            if (!ActionQue[0].begin)
                ActionQue[0].Begin();

            ended_action = act;
            ActionQue.RemoveAt(0);
            if (ActionQue.Count > 0)
                ActionQue[0].Begin();
            Presenter.actionUpdate = true;

            ActionEnded(ended_action,new ActionEventArgs("Завершение действия в контроллере"));
        }

        static public void AddAction(Action add)
        {
            add.ShowEnd += ActionEndUpdate;
            ActionQue.Add(add);
            id_total++;
        }

        static public void AddAction(ActionType act_type)
        {

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
