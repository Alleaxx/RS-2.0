﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Presenter
    {
        public static event EventHandler<ActivityEventArgs> EventsListUpdated;

        //Выбранные в интерфейсе объекты
        public static User selected_user;
        public static Ban selected_ban;
        public static Event selected_event;
        public static Group selected_group;
        public static Trait selected_trait;
        public static Relation selected_relation;
        public static Action selectedAction;
        public static bool newEvent;
        public static bool actionUpdate = true;
        public static bool accurateInfo = true;
        public static SortingUserTypes sorting_user = SortingUserTypes.no_sort;


        #region Отображение пользователей
        public static List<User> users_sorted = new List<User>();
        public static List<User> All_Users = new List<User>();
        public static string text_userSearch = "";
        public static bool all_users = true, active_users = true;

        //Список, каким он должен выглядеть в интерфейсе
        public static void UsersListUpdate()
        {
            users_sorted = new List<User>();
            users_sorted.Clear();
            if (all_users && text_userSearch == "")
                users_sorted = UsersControl.UserListReturnSort();
            else
            {
                if (active_users && text_userSearch == "")
                {
                    foreach (User user in UsersControl.UserListReturnSort())
                    {
                        if (user.activity)
                            users_sorted.Add(user);
                    }
                }

            }

            if (text_userSearch != "")
            {
                foreach (User user in UsersControl.reserve)
                {
                    if (user.nick.Contains(text_userSearch))
                        users_sorted.Add(user);
                }
            }

        }

        #endregion

        #region Отображение событий

        //по важности события
        public static bool events_slight = false, events_medium = true,
            events_important = true, events_historic = true, events_unusual = true, events_update = true;
        //Поиск по названию события
        public static string events_search_text = "";
        //Количество показываемых событий
        public static int ShowedEventsCount = 15;
        public static List<Event> events_sorted = new List<Event>();
        //Частота обновлений событий

        //Сортировка показываемых списков
        public static void EventsListUpdate()
        {
            List<Event> eventsSortedPrev = new List<Event>();
            eventsSortedPrev = events_sorted;

            events_sorted.Clear();
            //Сортировка по поиску
            if (events_search_text != "")
            {
                for (int i = 0; i < EventsControl.AllEvents.Count; i++)
                {
                    if (EventsControl.AllEvents[i].Name.Contains(events_search_text))
                        events_sorted.Add(EventsControl.AllEvents[i]);
                }
            }
            //Сортировка по галкам
            if (events_search_text == "")
            {
                foreach (Event eve in EventsControl.AllEvents)
                {
                    if (eve.Rareness == EventImportance.usual && events_slight)
                        events_sorted.Add(eve);
                    if (eve.Rareness == EventImportance.unusual && events_unusual)
                        events_sorted.Add(eve);
                    if (eve.Rareness == EventImportance.rare && events_medium)
                        events_sorted.Add(eve);
                    if (eve.Rareness == EventImportance.epic && events_important)
                        events_sorted.Add(eve);
                    if (eve.Rareness == EventImportance.historical && events_historic)
                        events_sorted.Add(eve);
                }
            }
            events_sorted.Reverse();

            if (events_sorted.Count > ShowedEventsCount)
                events_sorted = events_sorted.GetRange(0, ShowedEventsCount);
            EventsListUpdated(events_sorted, new ActivityEventArgs());

        }

        //Реакция на событие появления нового события
        public static void EventNewCheck(object sender, ActivityEventArgs e)
        {
            EventsListUpdate();
        }

        #endregion


        //Изменение выбранного объекта за счет имени выбранного элемента списка
        public static void SelectionCheck(string name)
        {
            if (name.Length != 0)
            {
                string id_text = name.Substring(0, name.IndexOf('_'));
                int id = Convert.ToInt32(name.Substring(name.IndexOf('_') + 1));
                switch (id_text)
                {
                    case "user":
                        selected_user = UsersControl.UserSearch(id);
                        break;
                    case "group":
                        selected_group = Group.GroupSearch(id);
                        break;
                    case "ban":
                        selected_ban = UsersControl.UserSearch(id).LastBan;
                        break;
                    case "simEvent":
                        selected_event = EventsControl.EventSearch(id);
                        break;
                    case "trait":
                        selected_trait = Trait.SearchTrait(id);
                        break;
                    case "relation":
                        break;
                    case "usualProperty":
                        break;
                    case "action":
                        selectedAction = ActionControl.SearchAction(id);
                        break;
                    default:
                        break;
                }
            }

        }
        //Возвращение необходимой интерфейсной информации
        public static IntView InterfaceInfoReturn(GUITypes type, int id)
        {
            switch (type)
            {
                case GUITypes.user:
                    return UsersControl.UserSearch(id).InterfaceInfo;
                default:
                    return new IntView();
            }
        }
        public static IntView InterfaceInfoReturn(GUITypes type)
        {
            switch (type)
            {
                case GUITypes.user:
                    return selected_user.InterfaceInfo;
                default:
                    return new IntView();
            }
        }
        public static IntView InterfaceInfoReturn(string name)
        {
            if (name != null && name.Length != 0)
            {
                string id_text = name.Substring(0, name.IndexOf('_'));
                int id = Convert.ToInt32(name.Substring(name.IndexOf('_') + 1));
                switch (id_text)
                {
                    case "user":
                        return UsersControl.UserSearch(id).InterfaceInfo;
                    case "group":
                        return Group.GroupSearch(id).InterfaceInfo;
                    case "ban":
                        return UsersControl.UserSearch(id).LastBan.InterfaceInfo;
                    case "simEvent":
                        return EventsControl.EventSearch(id).GetGui();
                    case "trait":
                        return Trait.SearchTrait(id).InterfaceInfo;
                    case "relation":
                        return selected_user.relations.RelationReturn(UsersControl.UserSearch(id)).InterfaceInfo;
                    case "action":
                        return ActionControl.SearchAction(id).GetGui();
                    default:
                        return new IntView();
                }
            }
            else
            {
                return new IntView();
            }

        }

        //Установка сортировки пользователей
        public static void SetUserSort(SortingUserTypes type)
        {
            sorting_user = type;
        }

    }

    enum GUITypes
    {
        user, group, ban, simEvent, fraction, trait, relation, action
    }
    enum SortingUserTypes
    {
        no_sort, registration, groupRareness, messages, reputation, adeq, rakness, moderChanse, influence, warning
    }
}
