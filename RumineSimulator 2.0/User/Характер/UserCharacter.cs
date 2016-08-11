using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class UserCharacter
    {
        public int leaveChanse { get; private set; }
        public int conservative { get; private set; }
        public int adeq { get; private set; }
        public int rakness { get; private set; }

        private int creativity;
        public int Creativity
        {
            get
            {
                return creativity;

            }
            set
            {
                if (value > 10)
                    creativity = 10;
                else
                    creativity = value;
            }
        }

        private int sciense;
        public int Sciense
        {
            get
            {
                return sciense;

            }
            set
            {
                if (value > 10)
                    sciense = 10;
                else
                    sciense = value;
            }
        }

        public int tolerance { get; private set; }

        public bool wantsToBeMod { get; private set; }
        Random random = new Random();

        public UserCharacter(User user)
        {
            switch (Date.current_date.Year+1-user.registration.Year)
            {
            //Установка рандомных параметров характера(в основном зависят от даты регистрации)
            #region Адекватность
                //Адекватность ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = random.Next(0, 7);
                            break;
                        case 1:
                            adeq = random.Next(2, 8);
                            break;
                        case 2:
                            adeq = random.Next(3, 11);
                            break;
                    }
                    break;
                //Адекватность среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = random.Next(4, 9);
                            break;
                        case 1:
                            adeq = random.Next(3, 11);
                            break;
                        case 2:
                            adeq = random.Next(0, 5);
                            break;
                    }
                    break;
                //Адекватность олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq = random.Next(4, 9);
                            break;
                        case 1:
                            adeq = random.Next(2, 10);
                            break;
                        case 2:
                            adeq = random.Next(0, 11);
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
                            rakness = random.Next(3, 11);
                            break;
                        case 1:
                            rakness = random.Next(2, 8);
                            break;
                        case 2:
                            rakness = random.Next(0, 7);
                            break;
                    }
                    break;
                //Раковитость среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            rakness = random.Next(3, 11);
                            break;
                        case 1:
                            rakness = random.Next(2, 10);
                            break;
                        case 2:
                            rakness = random.Next(0, 7);
                            break;
                    }
                    break;
                //Раковитость олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            rakness = random.Next(4, 11);
                            break;
                        case 1:
                            rakness = random.Next(4, 11);
                            break;
                        case 2:
                            rakness = random.Next(0, 6);
                            break;
                    }
                    break;
            }
            rakness -= adeq / 4;
            if (rakness < 0)
                rakness = 0;
            #endregion
            #region Консервативность
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                //Консервативность ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = random.Next(2, 11);
                            break;
                        case 1:
                            conservative = random.Next(0, 9);
                            break;
                        case 2:
                            conservative = random.Next(0, 6);
                            break;
                    }
                    break;
                //Консервативность среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = random.Next(4, 11);
                            break;
                        case 1:
                            conservative = random.Next(2, 8);
                            break;
                        case 2:
                            conservative = random.Next(0, 8);
                            break;
                    }
                    break;
                //Консервативность олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            conservative = random.Next(5, 11);
                            break;
                        case 1:
                            conservative = random.Next(0, 11);
                            break;
                        case 2:
                            conservative = random.Next(2, 9);
                            break;
                    }
                    break;
            }
            #endregion
            #region Креативность и наука
            if (AdvRandom.PersentChanseBool(10))
                creativity = random.Next(5, 11);
            else
                creativity = random.Next(7);

            if (AdvRandom.PersentChanseBool(10))
                sciense = random.Next(5, 11);
            else
                sciense = random.Next(7);
            #endregion
            #region Толерантность
            tolerance = random.Next(0, 11);
            #endregion

            #region Параметры зависящие от олдфажности
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                case 1:
                    leaveChanse = random.Next(2, 10);
                    wantsToBeMod = AdvRandom.PersentChanseBool(10);
                    break;
                case 2:
                    leaveChanse = random.Next(1, 9);
                     
                    wantsToBeMod = AdvRandom.PersentChanseBool(20);
                    break;
                case 3:
                    leaveChanse = random.Next(1,7);
                    tolerance -= random.Next(0, 2);
                    wantsToBeMod = AdvRandom.PersentChanseBool(25);
                    break;
            }
            #endregion
        }
    }
}
