using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Character
    {
        public User owner { get; private set; }
        public CharFeature adeq { get; private set; }
        public CharFeature rakness { get; private set; }
        public CharFeature creativity { get; private set; }
        public CharFeature sciense { get; private set; }
        public CharFeature humanist { get; private set; }
        public CharFeature historic { get; private set; }
        public CharFeature tolerance { get; private set; }
        public CharFeature conservative { get; private set; }
        public CharFeature leaveChanse { get; private set; }
        public CharFeature[] character_params;

        Random random = new Random();

        public Character(User user)
        {
            owner = user;
            adeq = new CharFeature("Адекватность");
            rakness = new CharFeature("Раковитость");
            creativity = new CharFeature("Креативность");
            sciense = new CharFeature("Наука");
            humanist = new CharFeature("Гуманитарность");
            historic = new CharFeature("Историк");
            tolerance = new CharFeature("Толерантность");
            conservative = new CharFeature("Консервативность");
            leaveChanse = new CharFeature("Шанс ухода");
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {

            #region Адекватность, раковитость, консервативность
                //Адекватность, раковитость и консервативность ньюфагов
                case 1:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(random.Next(0, 6));
                            rakness.SetValue(random.Next(3, 11));
                            conservative.SetValue(random.Next(2, 11));
                            break;
                        case 1:
                            adeq.SetValue(random.Next(2, 7));
                            rakness.SetValue(random.Next(2, 8));
                            conservative.SetValue(random.Next(0, 9));
                            break;
                        case 2:
                            adeq.SetValue(random.Next(3, 11));
                            rakness.SetValue(random.Next(0, 7));
                            conservative.SetValue(random.Next(0, 6));
                            break;
                    }
                    break;
                //Адекватность, раковитость и консервативность среднефагов
                case 2:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(random.Next(4, 8));
                            rakness.SetValue(random.Next(3, 11));
                            conservative.SetValue(random.Next(4, 11));
                            break;
                        case 1:
                            adeq.SetValue(random.Next(3, 11));
                            rakness.SetValue(random.Next(2, 10));
                            conservative.SetValue(random.Next(2, 8));
                            break;
                        case 2:
                            adeq.SetValue(random.Next(0, 5));
                            rakness.SetValue(random.Next(0, 7));
                            conservative.SetValue(random.Next(0, 8));
                            break;
                    }
                    break;
                //Адекватность, раковитость и консервативность олдфагов
                case 3:
                    switch (random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(random.Next(4, 8));
                            rakness.SetValue(random.Next(3, 11));
                            conservative.SetValue(random.Next(5, 11));
                            break;
                        case 1:
                            rakness.SetValue(random.Next(0, 7));
                            adeq.SetValue(random.Next(3, 11));
                            conservative.SetValue(random.Next(0, 11));
                            break;
                        case 2:
                            rakness.SetValue(random.Next(0, 5));
                            adeq.SetValue(random.Next(0, 9));
                            conservative.SetValue(random.Next(2, 9));
                            break;
                    }
                    break;
            }
            #endregion

            //Креативность
            if (AdvRnd.PersentChanseBool(10))
                creativity.SetValue(random.Next(5, 11));
            else
                creativity.SetValue(random.Next(5));

            //Наука
            if (AdvRnd.PersentChanseBool(10))
                sciense.SetValue(random.Next(5, 11));
            else
                sciense.SetValue(random.Next(5));

            //Гуманитаризм
            if (AdvRnd.PersentChanseBool(10))
                humanist.SetValue(random.Next(5, 11));
            else
                humanist.SetValue(random.Next(5));

            if (AdvRnd.PersentChanseBool(10))
                historic.SetValue(random.Next(5, 11));
            else
                historic.SetValue(random.Next(5));

            //Толерантность
            tolerance.SetValue(random.Next(0, 11));

            //Шанс уйти
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                case 1:
                    leaveChanse.SetValue(random.Next(2, 10));
                    break;
                case 2:
                    leaveChanse.SetValue(random.Next(1, 9));
                    break;
                case 3:
                    leaveChanse.SetValue(random.Next(1, 7));
                    tolerance.Value -= random.Next(0, 2);
                    break;
            }

            character_params = new CharFeature[] { adeq,rakness,conservative,tolerance,sciense,creativity};

        }
    }
}
