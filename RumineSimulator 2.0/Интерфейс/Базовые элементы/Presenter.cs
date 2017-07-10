using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class Presenter
    {
        //Выбранные в интерфейсе объекты
        public static User selected_user;
        public static Fraction selected_fraction;
        public static Ban selected_ban;
        public static Event selected_event;
        public static Group selected_group;
        public static Trait selected_trait;
        public static Relation selected_relation;
        public static SortingUserTypes sorting_user = SortingUserTypes.no_sort;

        //Изменение выбранного объекта за счет имени выбранного элемента списка
        public static void SelectionCheck(string name)
        {
            string id_text = name.Substring(0, name.IndexOf('_'));
            int id = Convert.ToInt32(name.Substring(name.IndexOf('_')+1));
            switch (id_text)
            {
                case "user":
                    selected_user = UsersControl.UserSearch(id);
                    break;
                case "group":
                    selected_group = GroupsControl.GroupSearch(id);
                    break;
                case "ban":
                    selected_ban = UsersControl.UserSearch(id).LastBan;
                    break;
                case "simEvent":
                    selected_event = Events_List.EventSearch(id);
                    break;
                case "fraction":
                    selected_fraction = FractionList.SearchFraction(id);
                    break;
                case "trait":
                    selected_trait = TraitsList.SearchTrait(id);
                    break;
                case "relation":
                    break;
                case "usualProperty":
                    break;
                default:
                    break;
            }
        }
        //Возвращение необходимой интерфейсной информации
        public static IntView InterfaceInfoReturn(GUITypes type,int id)
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
            if(name != null && name.Length != 0)
            {
                string id_text = name.Substring(0, name.IndexOf('_'));
                int id = Convert.ToInt32(name.Substring(name.IndexOf('_') + 1));
                switch (id_text)
                {
                    case "user":
                        return UsersControl.UserSearch(id).InterfaceInfo;
                    case "group":
                        return GroupsControl.GroupSearch(id).InterfaceInfo;
                    case "ban":
                        return UsersControl.UserSearch(id).LastBan.InterfaceInfo;
                    case "simEvent":
                        return Events_List.EventSearch(id).InterfaceInfo;
                    case "fraction":
                        return FractionList.SearchFraction(id).Interface_Info;
                    case "trait":
                        return TraitsList.SearchTrait(id).InterfaceInfo;
                    case "relation":
                        return selected_user.relations.RelationReturn(UsersControl.UserSearch(id)).InterfaceInfo;
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
        user,group,ban,simEvent,fraction,trait,relation
    }
    enum SortingUserTypes
    {
        no_sort,registration,groupRareness,messages,reputation,adeq,rakness,moderChanse,influence,warning
    }
}
