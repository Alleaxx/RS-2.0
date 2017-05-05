using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Character
    {
        public ChanseParam leaveChanse { get; private set; }


        public User owner { get; private set; }
        public CharParam adeq { get; private set; }
        public CharParam rakness { get; private set; }
        public CharParam creativity { get; private set; }
        public CharParam sciense { get; private set; }
        public CharParam humanist { get; private set; }
        public CharParam historic { get; private set; }
        public CharParam tolerance { get; private set; }
        public CharParam conservative { get; private set; }
        public CharParam[] character_params;

        Random random = new Random();

        public Character(User user)
        {
            owner = user;
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                //Установка рандомных параметров характера(в основном зависят от даты регистрации)
            #region Адекватность
                //Адекватность ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = new CharParam(owner,"Адекватность", random.Next(0, 6),10, true, random.Next(5, 30),false);
                            break;
                        case 1:
                            adeq = new CharParam(owner, "Адекватность", random.Next(2, 7), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            adeq = new CharParam(owner, "Адекватность", random.Next(3, 11), 10, true, random.Next(5, 30), false);

                            break;
                    }
                    break;
                //Адекватность среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = new CharParam(owner, "Адекватность", random.Next(4, 8), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            adeq = new CharParam(owner, "Адекватность", random.Next(3, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            adeq = new CharParam(owner, "Адекватность", random.Next(0, 5), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
                //Адекватность олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = new CharParam(owner, "Адекватность", random.Next(4, 8), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            adeq = new CharParam(owner, "Адекватность", random.Next(2, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            adeq = new CharParam(owner, "Адекватность", random.Next(0, 9), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
            }
            #endregion
            #region Раковитость
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                //Раковитость ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            rakness = new CharParam(owner, "Раковитость", random.Next(3, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            rakness = new CharParam(owner, "Раковитость", random.Next(2, 8), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            rakness = new CharParam(owner, "Раковитость", random.Next(0, 7), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
                //Раковитость среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            rakness = new CharParam(owner, "Раковитость", random.Next(3, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            rakness = new CharParam(owner, "Раковитость", random.Next(2, 10), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            rakness = new CharParam(owner, "Раковитость", random.Next(0, 7), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
                //Раковитость олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            rakness = new CharParam(owner, "Раковитость", random.Next(3, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            rakness = new CharParam(owner, "Раковитость", random.Next(0, 7), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            rakness = new CharParam(owner, "Раковитость", random.Next(0, 5), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
            }
            #endregion
            #region Консервативность
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                //Консервативность ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = new CharParam(owner, "Консервативность", random.Next(2, 11), 10, true, random.Next(5, 30),false);
                            break;
                        case 1:
                            conservative = new CharParam(owner, "Консервативность", random.Next(0, 9), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            conservative = new CharParam(owner, "Консервативность", random.Next(0, 6), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
                //Консервативность среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = new CharParam(owner, "Консервативность", random.Next(4, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            conservative = new CharParam(owner, "Консервативность", random.Next(2, 8), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            conservative = new CharParam(owner, "Консервативность", random.Next(0, 8), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
                //Консервативность олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = new CharParam(owner, "Консервативность", random.Next(5, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 1:
                            conservative = new CharParam(owner, "Консервативность", random.Next(0, 11), 10, true, random.Next(5, 30), false);
                            break;
                        case 2:
                            conservative = new CharParam(owner, "Консервативность", random.Next(2, 9), 10, true, random.Next(5, 30), false);
                            break;
                    }
                    break;
            }
            #endregion
            #region Креативность и науки
            if (AdvRandom.PersentChanseBool(10))
                creativity = new CharParam(owner, "Креативность",random.Next(5, 11),10, true, random.Next(5, 30), false);
            else
                creativity = new CharParam(owner, "Креативность", random.Next(5), 10, true, random.Next(5, 30), false);

            if (AdvRandom.PersentChanseBool(10))
                sciense = new CharParam(owner, "Техник", random.Next(5, 11), 10, true, random.Next(5, 30), false);
            else
                sciense= new CharParam(owner, "Техник",random.Next(5), 10, true, random.Next(5, 30), false);

            if (AdvRandom.PersentChanseBool(10))
                humanist = new CharParam(owner, "Гуманитарность", random.Next(5, 11), 10, true, random.Next(5, 30), false);
            else
                humanist = new CharParam(owner, "Гуманитарность", random.Next(5), 10, true, random.Next(5, 30), false);
            #endregion
            #region Толерантность
            tolerance = new CharParam(owner, "Толерантность", random.Next(0, 11), 10, true, random.Next(5, 30),false);
            #endregion

            #region Параметры зависящие от олдфажности
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                case 1:
                    leaveChanse = new ChanseParam(owner, "Шанс ухода", random.Next(2, 10), 100, true, random.Next(15, 40), true);
                    break;
                case 2:
                    leaveChanse = new ChanseParam(owner, "Шанс ухода", random.Next(1, 9), 100, true, random.Next(15, 40), true);
                    break;
                case 3:
                    leaveChanse = new ChanseParam(owner, "Шанс ухода", random.Next(1, 7), 100, true, random.Next(15, 40), true);

                    tolerance.Param_value -= random.Next(0, 2);
                    break;
            }
            #endregion
            character_params = new CharParam[] { adeq,rakness,conservative,tolerance,sciense,creativity};
        }
    }
}
