using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class ReputationReason
    {
        static Random random = new Random();

        static List<string> PlusReasons = new List<string>();
        static List<string> MinusReasons = new List<string>();
        public static void ReasonsInit()
        {
            #region Плюсовая репутация
            PlusReasons.Add("Репушка-репутация");
            PlusReasons.Add("Иногда все же надо вспоминать о репушке");
            PlusReasons.Add("Согласен.");
            PlusReasons.Add("Плюсик, мой маленький плюсик.");
            PlusReasons.Add("КОНЕЦ ГЛАВНОГО ЧАТА(((");
            PlusReasons.Add("+репа");
            PlusReasons.Add("Индийские комики.");
            PlusReasons.Add("Этот изумительный школьный рэп.");
            PlusReasons.Add("Пылесос");
            PlusReasons.Add("Вот так вот.");
            PlusReasons.Add("Слава Украине.");
            PlusReasons.Add("И честно!");
            PlusReasons.Add("Домашняя репутация.");
            PlusReasons.Add("Не, ну как тут не лойснуть. Лойсать всегда надо. Будет теперь у тебя лойс.");
            PlusReasons.Add("ывфыы");
            PlusReasons.Add("Графиня Магдебургская изволила одолжить посещением по причине отменного цейлонского чая.");
            PlusReasons.Add("Поднимайся");
            PlusReasons.Add("Удачной работы!");
            PlusReasons.Add("Я хочу сыграть в игру (шахматы).");
            PlusReasons.Add("Добрыня");
            PlusReasons.Add("Сасный сос");
            PlusReasons.Add("Вы жив");
            PlusReasons.Add("Да, это я.");
            PlusReasons.Add("Накрутка саапщений");
            PlusReasons.Add("Я вспомнил про то, что могу ставить пласцы");
            PlusReasons.Add("1666? Во славу Сатане, конечно же.");
            PlusReasons.Add("Давно не повышал.");
            PlusReasons.Add("Курица,мясо,мышьяк!");
            PlusReasons.Add("Покушать принёс.");
            PlusReasons.Add("Добра :3");
            PlusReasons.Add("Мррр-мяу.");
            PlusReasons.Add("Эх плюсики да на репе вертятся");
            PlusReasons.Add("Короче дельфин");
            PlusReasons.Add("Пельмень, уважаю!");
            PlusReasons.Add("Весёлый молочник.");
            PlusReasons.Add("tekst kommentariya");
            PlusReasons.Add("с новым годом");
            PlusReasons.Add("Нет, сударь, это Вы.");
            PlusReasons.Add("Ку-ка-ре-ку.");
            PlusReasons.Add("Проще занимать, чем у кого-то позвонить.");
            PlusReasons.Add("Жи ши пиши от души");
            PlusReasons.Add("Прост");
            PlusReasons.Add("с днёп победы");
            PlusReasons.Add("не ну ты че");
            PlusReasons.Add("Топ разработчег по версии журнала playboy");
            PlusReasons.Add("Значительный прыщ, прям не выдавишь.");
            PlusReasons.Add("хижына в тайге");
            PlusReasons.Add("Расскажи секрет, как поднять репутацию");
            PlusReasons.Add("Боже, спаси култексты и сохрани...");
            PlusReasons.Add("Один из немногих, кто адекватен на этом сайте.");
            PlusReasons.Add("Я живой еще");
            PlusReasons.Add("Спасибо, спасибо.");
            PlusReasons.Add("Большой и злой");
            PlusReasons.Add("танцующие соусы гриль");
            PlusReasons.Add("зато плюс большой!");
            PlusReasons.Add("Пиу-пиу!");
            PlusReasons.Add("Кто не умеет беречь отцовское наследство, тот всё-таки умрёт в нищете, несмотря ни на какие демонские усилия.");
            PlusReasons.Add("Нетрадиционной.");
            PlusReasons.Add("Какой-то школоло помешал нашей священной цели.");
            PlusReasons.Add("? Из фарса, господин Рымов, что ни делай, никогда чушь не выйдет, потому что он сам по себе чушь.");
            PlusReasons.Add("За помощь");
            PlusReasons.Add("Я и не сплю.");
            PlusReasons.Add("СЛОЖНО ДО АДМИНА ИДТИ?7?7");
            PlusReasons.Add("Зачёт...");
            PlusReasons.Add("Скуби-Ду!");
            PlusReasons.Add("PRESS [X] TU POLUCHIT' PLUS V REPU");
            PlusReasons.Add("фраер");
            PlusReasons.Add("++++++++++++++++++++++");
            PlusReasons.Add("Сосиска!");
            PlusReasons.Add("Bobrik");
            PlusReasons.Add("Нацисты-специалисты.");
            PlusReasons.Add("Классный");
            PlusReasons.Add("мммммм вкусно");
            PlusReasons.Add("уточка)))0");
            PlusReasons.Add("без регистрации и смс");
            PlusReasons.Add("майнкрафт лайно ");
            PlusReasons.Add("Физрук новая серия скачать.");
            PlusReasons.Add("чебуреком не подавись.");
            PlusReasons.Add("Метёлка");
            PlusReasons.Add("Эрон-дон-дон");
            PlusReasons.Add("Афанасий Афанасьевич Фет");
            PlusReasons.Add("снг");
            PlusReasons.Add("С Новым годом, чмо.");
            PlusReasons.Add("пошел ты");
            PlusReasons.Add("ееее");
            PlusReasons.Add("ну такое");
            PlusReasons.Add("молодец");
            #endregion
            #region Минусовая репутация
            MinusReasons.Add("Слишком приставучий!");
            MinusReasons.Add("Минусы!ураа");
            MinusReasons.Add("Лол што?");
            MinusReasons.Add("14////88");
            MinusReasons.Add("Взаимно");
            MinusReasons.Add("Спасибо за помощь");
            MinusReasons.Add("ниииииииииииииииииииииииииииит");
            MinusReasons.Add("минус за минус, падла ");
            MinusReasons.Add("><(((°>");
            MinusReasons.Add("А? Что?");
            MinusReasons.Add("пошел ты");
            MinusReasons.Add("неочень");
            MinusReasons.Add("уздарь");
            #endregion
        }
        public static string ReturnReason(bool minus)
        {
            if (minus)
                return MinusReasons[random.Next(MinusReasons.Count)];
            else
                return PlusReasons[random.Next(PlusReasons.Count)];
        }
    }
}
