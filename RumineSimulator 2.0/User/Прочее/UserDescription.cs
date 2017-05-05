using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserDescription
    {
        //Еще не описанные параметры для генерации текста(убираются с добавлением в описание новых фактов)
        static Dictionary<string, bool> descr_available = new Dictionary<string, bool>();

        //Массивы begin обозначают, что содержание относится к началу предложения, middle же посередине предложения
        #region Прозвища юзера за счет олдфажности
        static string[] newfag_text = new string[]
        {
            "пользователь",
            "посетитель сайта",

            "новичок",
            "ньюфаг",
            "ньюфаня",

        };
        static string[] srednefag_text = new string[]
        {
            "руминевец",
            "юзер",

            "пользователь",
            "среднефаг",

        };
        static string[] oldfag_text = new string[]
        {
            "руминевец",

            "завсегдай",
            "дед",
            "олдфаг",
            "старый пердун",
        };
        #endregion
        #region Про адекватность
        static string[] adeq_Begin = new string[] {
            //0
            "Совершенно безумный ",
            "Отбитый напрочь ",
            "Бесшабашный ",
            "Слегка поехавший ",
            "Вменяемый ",
            //5
            "Обычный ",
            "Адекватный ",
            "Исполнительный ",
            "Невероятно вежливый ",
            "Очень предусмотрительный ",
            //10
            "Поразительно адекватный "
        };
        static string[] adeq_Middle = new string[] {
            //0
            ", творящий абсолютно безумные, непредсказуемые вещи",
            ", откалывающий такие номера, что страшно становится",
            ", которому сам Сауз нипочем",
            " со слегка пошатнувшейся психикой",
            ", не имеющий чего-то странного в своих поступках",
            //5
            ", умеющий вовремя остановиться",
            ", никогда не переходящий за границу",
            ", знающий цену себе и окружающим людям",
            ", которому больно смотреть на безумную флудилку",
            ", имеющий кристально чистый разум",
            //10
            ", идеал расчетливого, холодного ума"
        };
        #endregion
        #region Про раковитость
        static string[] rakness_Middle = new string[] {
            //0
            " без единой капли рака",
            " без единой капли рака",
            " без поганых интересов и онкологических болезней",
            ", не имеющий раковых наклонностей",
            ", не имеющий раковых наклонностей",
            //5
            " со слегка повышенным уровнем рака",
            " с явными раковыми наклонностями",
            " с явными раковыми наклонностями",
            ", деградирующий прямо на глазах",
            ", деградирующий прямо на глазах",
            //10
            ", спасти которого от рака мозга уже невозможно"
        };
        static string[] rakness_Begin= new string[] {
            //0
            "Абсолютно не раковитый ",
            "Абсолютно не раковитый ",
            "Не раковитый ",
            "Не раковитый ",
            "Нормальный ",
            //5
            "Слегка побулькивающий в кастрюле ",
            "Слегка побулькивающий в кастрюле ",
            "Страдающий онкологическими болезнями",
            "Неприятный нормальным людям ",
            "Раковитый до невозможности ",
            //10
            "Раковитый до мозга костей "
        };
        #endregion
        #region Про консервативность
        static string[] cons_Middle = new string[] {
            //0
            ", вечно меняющий свои взгляды ",
            ", вечно меняющий свои взгляды ",
            " переменчивый ",
            " переменчивый ",
            ", стабильный в своих взглядах ",
            //5
            " ностальгирующий ",
            " ностальгирующий ",
            " консервированный ",
            ", вечно ноющий про старые времена ",
            ", вечно ноющий про старые времена ",
            //10
            ", вечно ноющий про старые времена "
        };
        #endregion
        #region Про шанс ухода
        static string[] leave_Middle = new string[] {
            //0
            "Этот парень застрял здесь навсегда",
            "Этот парень застрял здесь навсегда",
            "Шанс ухода весьма мал",
            "Шанс ухода весьма мал",
            "Уйдет лишь при сильном желании",
            //5
            "Уйдет лишь при сильном желании",
            "Скорее всего уйдет с румайна далеко и надолго, если выпадет случай",
            "Скорее всего уйдет с румайна далеко и надолго, если выпадет случай",
            "Свалит с сайта при первой же возможности",
            "Свалит с сайта при первой же возможности ",
            //10
            "Свалит с сайта при первой же возможности "
        };
        #endregion


        static Random random = new Random();


        public static string GetTextDescription(User user)
        {
            string descr = "";
            descr_available.Add("Рак", true);
            descr_available.Add("Адекватность", true);

            //первый этап выбора
            switch (random.Next(2))
            {
                case 0:
                    descr = descr + adeq_Begin[user.character.adeq.Param_value];
                    descr_available["Адекватность"] = false;
                    break;
                case 1:
                    descr = descr + rakness_Begin[user.character.rakness.Param_value];
                    descr_available["Рак"] = false;
                    break;
            }
            //добавляем консервированность юзера
            descr = descr + cons_Middle[user.character.conservative.Param_value];
            //Название юзера
            descr = descr + Oldness(Date.current_date.Year-user.registration.Year+1);

            //второй этап выбора
            if (descr_available["Рак"])
            {
                descr = descr + rakness_Middle[user.character.rakness.Param_value];
                descr_available["Рак"] = false;
            }
            else if (descr_available["Адекватность"])
            {
                descr = descr + adeq_Middle[user.character.adeq.Param_value];
                descr_available["Адекватность"] = false;
            }
            //Добавляем про шанс ухода
            descr = descr + ". ";
            descr = descr + leave_Middle[user.character.leaveChanse.Param_value];
            //Количество друзей и так далее
            descr_available.Clear();
            return descr;
        }

        static private string Oldness(int oldness)
        {
            switch (oldness)
            {
                case 1:
                    return newfag_text[oldness];
                case 2:
                    return srednefag_text[oldness];
                case 3:
                    return oldfag_text[oldness];
            }
            return " ";
        }
    }
}
