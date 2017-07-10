using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class EventStatChange_Preset
    {
        private static Random random = new Random();

        private static User rnd_User;
        private static User rnd_UserAdd;

        //Управляем выдачей событий изменяющих параметры пользователя
        static public EventStatChange returnStatChangeEvent(ReactionReason reason)
        {
            UsersRandomisation();


            //Написание сообщения
            int eventChanse = random.Next(0, 101);
            if (eventChanse < 100)
            {
                return newMessageEvent(reason);
            }

            return new EventStatChange("",EventType.ban);
        }

        //Новое сообщение
        private static EventStatChange newMessageEvent(ReactionReason reason)
        {
            //Создание события
            EventStatChange newMessage = new EventStatChange($"Сообщение {rnd_User.nick}", EventType.message);
            newMessage.EventAdd1_BasicInfo(new Event_Creator(CreatorType.User, rnd_User.nick));
            newMessage.participants.Add(rnd_User,"Автор");
            newMessage.EventAdd3_Mods(random.Next(3), 0, 0, 0);
            newMessage.EventAdd6_Dates(0);
            //Воплощение события
            rnd_User.messages++;
            Activity.curr_day_messages++;
            int likes = 0, chanse = 0;
            chanse = rnd_User.forum_influence / 5;
            if (AdvRnd.PersentChanseBool(chanse))
            {
                likes = random.Next(3);
                rnd_User.likes += likes;
            }
            //Информация в интерфейсе
            newMessage.eventSpec_properties.Add(new GuiString("Сообщение ", $"{rnd_User.nick}", false, StringProfile.Header));
            newMessage.eventSpec_properties.Add(new GuiString("Симпатии: ", likes.ToString(), true));
            return newMessage;
        }
        //Новый комментарий

        //Рандомизация пользователей для событий
        private static void UsersRandomisation()
        {
            rnd_User = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            do
            {
                rnd_UserAdd = UsersControl.Users[random.Next(UsersControl.Users.Count)];
            }
            while (rnd_User != rnd_UserAdd);
        }


    }


}
