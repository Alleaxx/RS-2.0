using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class UserDescription
    {
        public static string GetTextDescription(User user)
        {
            string descr = "";
            //Рандомизация описания
            //List<string> quals = new List<string>() { "\noldness", "\nrak", "\nadeq", "\ncons", "\nleave" };
            //do
            //{
            //    int rnd = AdvRnd.random.Next(quals.Count);
            //    descr = descr + quals[rnd];
            //    quals.RemoveAt(rnd);
            //}
            //while (quals.Count != 0);
            descr = "oldness \nadeq \nrak \ntolerance \ncons \nleave \ncreative \nscience \nhumanist \nhistoric";
            //Замена ключевых слов на текст
            descr = descr.Replace("adeq", Adeq(user.character.adeq.Value));
            descr = descr.Replace("leave", Leave(user.character.leaveChanse.Value));
            descr = descr.Replace("oldness", Oldness(user.month_oldness));
            descr = descr.Replace("rak", Rak(user.character.rakness.Value));
            descr = descr.Replace("cons", Cons(user.character.conservative.Value));
            descr = descr.Replace("tolerance", Tolerance(user.character.tolerance.Value));
            descr = descr.Replace("creative", Creative(user.character.creativity.Value));
            descr = descr.Replace("science", Science(user.character.sciense.Value));
            descr = descr.Replace("humanist", Humanist(user.character.humanist.Value));
            descr = descr.Replace("historic", Historic(user.character.historic.Value));
            return descr;
        }

        //Описание в зависимости от параметра
        static private string Adeq(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Совершенно безумный ");
                    strs.Add("-Творит абсолютно безумные, непредсказуемые вещи ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Отбитый напрочь ");
                    strs.Add("-Откалывает такие номера, что страшно становится ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Бесшабашный ");
                    strs.Add("-Сам Сауз ему нипочем! ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Слегка поехавший ");
                    strs.Add("-Слегка пошатнувшаяся психика ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Вменяемый ");
                    strs.Add("-Не имеет чего-то странного в своих поступках ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("-Обычный ");
                    strs.Add("-Умеет вовремя остановиться ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Адекватный ");
                    strs.Add("-Никогда не переходит за границу ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Исполнительный ");
                    strs.Add("-Знает цену себе и окружающим людям ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Невероятно вежливый ");
                    strs.Add("-Больно смотреть на безумную флудилку,");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Очень предусмотрительный ");
                    strs.Add("-Кристально чистый разум ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Поразительно адекватный ");
                    strs.Add("-Идеал расчетливого, холодного ума ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Tolerance(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Дьявол во плоти");
                    strs.Add("-Готов уничтожить всех несогласных");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Ненавидит всех вокруг");
                    strs.Add("-Терпеть не может неподобающие элементы");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Очень недобрый человек");
                    strs.Add("-Ужасно нетолерантнен");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Явно не добряк");
                    strs.Add("-Мимими и няняня не для него");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Несколько груб по отношению к миру ");
                    strs.Add("-Недолюбливает чужаков и нетру персон");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("Нейтрал");
                    strs.Add("-Уравновешен и спокоен");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Незлопамятный");
                    strs.Add("-Готов терпеть несовершенство окружающих");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Добрый");
                    strs.Add("-Любит людей в любых проявлениях");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Несет добро и свет в мир");
                    strs.Add("-Щедрый и великодушный");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Сама толерантность ");
                    strs.Add("-Не умеет злиться");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Ангел во плоти");
                    strs.Add("-Воплощение добра");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Rak(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Без единой капли рака ");
                    strs.Add("-Абсолютно не раковитый ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Без единой капли рака ");
                    strs.Add("-Абсолютно не раковитый ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Без поганых интересов и онкологических болезней ");
                    strs.Add("-Не раковитый ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Не имеет раковых наклонностей ");
                    strs.Add("-Не раковитый ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Не имеет раковых наклонностей ");
                    strs.Add("-Нормальный ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("-Слегка повышенный уровень рака");
                    strs.Add("-Слегка побулькивающий в кастрюле ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Явные раковые наклонности");
                    strs.Add("-Слегка побулькивающий в кастрюле ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Прямиком с мида");
                    strs.Add("-Страдает онкологическими болезнями ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Деградирует прямо на глазах ");
                    strs.Add("-Неприятен нормальным людям ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Деградирует прямо на глазах ");
                    strs.Add("-Раковит до невозможности ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Спасение невозможно, лечение одно - уничтожение");
                    strs.Add("-Раковит до мозга костей ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Cons(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Постоянно меняет свои взгляды ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Постоянно меняет свои взгляды ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Переменчивый");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Переменчивый");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Стабильный в своих взглядах");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("-Стабильный в своих взглядах");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Ностальгирующий");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Ностальгирующий");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Консервированный");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Вечно ноет о старых временах");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Застрял в прошлом");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Leave(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Застрял здесь навсегда ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Застрял здесь навсегда ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-С сайта просто так не уйдет");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-С сайта просто так не уйдет");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Уйдет лишь при сильном желании");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("-Уйдет лишь при сильном желании");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Скорее всего уйдет с румайна далеко и надолго");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Скорее всего уйдет с румайна далеко и надолго");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Свалит с сайта при первой же возможности");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Свалит с сайта при первой же возможности");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Свалит с сайта при первой же возможности");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }

        static private string Oldness(int val)
        {
            List<string> strs = new List<string>();

            if (val <= 6)
            {
                strs.Add("-Новичок ");
                strs.Add("-Ньюфаг ");
                strs.Add("-Ньюфаня ");
                strs.Add("-Новенький ");
                return strs[AdvRnd.random.Next(strs.Count)];
            }
            else if (val > 6 && val <= 12)
            {
                strs.Add("-Руминевец ");
                strs.Add("-Юзер ");
                strs.Add("-Среднефаг ");
                strs.Add("-Пользователь ");
                return strs[AdvRnd.random.Next(strs.Count)];
            }
            else if (val > 12)
            {
                strs.Add("-Завсегдай ");
                strs.Add("-Дед ");
                strs.Add("-Олдфаг ");
                strs.Add("-Старый пердун ");
                return strs[AdvRnd.random.Next(strs.Count)];
            }
            else
            {
                return "Пользователь";
            }

        }

        static private string Creative(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Скучная личность");
                    strs.Add("-Совершенно не имеет фантазии");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Креативность - явно не про него");
                    strs.Add("-Типичный потребитель");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Плохой фантазер");
                    strs.Add("-Не способен в высокие материи");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Может выдумать нечто");
                    strs.Add("-Не совсем плох, но и вовсе не хорош в творческом деле");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Имеет зачатки креативщика");
                    strs.Add("-Способен достичь большего");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("Творческий человек");
                    strs.Add("-Имеет немало интересных идей на уме");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Переполнен проектами и энергией для их осуществления");
                    strs.Add("-Прекрасный фантазер");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Умеет в разнообразие");
                    strs.Add("-Редко кто обладает такой выдумкой");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Овладел искусством творить и ваять");
                    strs.Add("-Может создавать шедевры");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Великого ума человек");
                    strs.Add("-Почти современный Леонардо да Винчи");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Грац искусства");
                    strs.Add("-Идеал");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Science(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Удивительно, что он знает про интернет");
                    strs.Add("- 2 + 2 = 5 для него");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Терпеть не может точные науки");
                    strs.Add("-Микроволновку точно не починит");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Имеет общие представления об окружающем мире");
                    strs.Add("-Ученые почти не является колдунами для него");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Знает отличие монитора от компьютера");
                    strs.Add("-Осведомлен о физических законах этого мира");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Может в науку, но не очень хочет");
                    strs.Add("-Крепкий середнячок");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("Имеет многогранные знания в точных науках");
                    strs.Add("-Осознает важность математики во всех ее проявлениях");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Мечтает стать ученым");
                    strs.Add("-Любит алгебру, геометрию, физику и химию");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Способен производить точные рассчеты");
                    strs.Add("-Владеет навыками математического анализа");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Знает и продвигает, что математика - царица всех наук");
                    strs.Add("-В свободное время кодит великолепные симуляции");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Истинно математический склад ума");
                    strs.Add("-Вычисляет что угодно и когда угодно");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Доктор технических наук");
                    strs.Add("-Получил(бы?) ученую степень");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Humanist(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-2 слова связать не может");
                    strs.Add("-Весьма ограниченная личность");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Возможно, знает Пушкина");
                    strs.Add("-Говорит короткими фразами, чтобы было понятнее");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Общие и очень отдаленные знания о литературе");
                    strs.Add("-Может составлять сложные посты(редко)");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Книги не читает, кроме, возможно, школьной программы");
                    strs.Add("-Не понимает в чем смысл чтения классиков");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Проявляет несильный интерес к саморазвитию");
                    strs.Add("-Осознает себя как личность, знающая своё 'Я' ");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("Начитан и умён");
                    strs.Add("-Легко назовет десяток фамилий известных авторов");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Уважает и ценит чужие письменные труды");
                    strs.Add("-Осознаёт важность развития человечества в социальном направлении");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Великолепный оратор и собеседник");
                    strs.Add("-Осведомлен о множестве социальных процессов мира");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Читал Ницше еще в детстве");
                    strs.Add("-Читать его посты - одно удовольствие");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Тонко чувствует любого человека");
                    strs.Add("-Твёрдо знает свое место в мире. А также место других людей");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Не иначе как великий писатель");
                    strs.Add("-Обаятелен, воспитан, прекрасен");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
        static private string Historic(int val)
        {
            List<string> strs = new List<string>();
            switch (val)
            {
                case 0:
                    strs.Add("-Живет настоящим");
                    strs.Add("-Обречен повторять ошибки прошлого");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 1:
                    strs.Add("-Мало что знает и помнит о прошлом");
                    strs.Add("-Плохая память? Или нежелание что-либо знать?");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 2:
                    strs.Add("-Имеет некоторые представления о древнем");
                    strs.Add("-Кажется, помнит что было в прошлом месяце");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 3:
                    strs.Add("-Различает Великуюю Отечественную и 2-ю Мировую");
                    strs.Add("-Некоторые знания истории есть");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 4:
                    strs.Add("-Помнит многое, хотя и не всё");
                    strs.Add("-В меру историчен");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 5:
                    strs.Add("-Интересуется прошлым");
                    strs.Add("-Иногда вспоминает и вздыхает");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 6:
                    strs.Add("-Готов проводить многочисленные исследования прошлого");
                    strs.Add("-Ошибок прошлого явно не повторит");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 7:
                    strs.Add("-Знает почти все что можно в истории, румине в особенности");
                    strs.Add("-Восторгается былыми деяниями");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 8:
                    strs.Add("-Мечтает повторить великие свершения прошлого");
                    strs.Add("-Возможно, ему стоит работать археологом?");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 9:
                    strs.Add("-Знает От и До в истории румайна и реальности");
                    strs.Add("-Шикарно реконструирует и рассказывает истории");
                    return strs[AdvRnd.random.Next(strs.Count)];
                case 10:
                    strs.Add("-Порой живет прошлым и потому великолепно его знает");
                    strs.Add("-Доктор исторических наук(румайна)");
                    return strs[AdvRnd.random.Next(strs.Count)];
                default:
                    strs.Add("");
                    strs.Add("");
                    return strs[AdvRnd.random.Next(strs.Count)];
            }
        }
    }
}
