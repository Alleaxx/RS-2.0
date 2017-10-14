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
        public AdeqCharFeature adeq = new AdeqCharFeature();
        public RakCharFeature rakness = new RakCharFeature();
        public CreativeCharFeature creativity = new CreativeCharFeature();
        public ScienseCharFeature sciense = new ScienseCharFeature();
        public HumanistCharFeature humanist = new HumanistCharFeature();
        public HistoricCharFeature historic = new HistoricCharFeature();
        public ToleranceCharFeature tolerance = new ToleranceCharFeature();
        public ConservativeCharFeature conservative = new ConservativeCharFeature();
        public LeaveChanseCharFeature leaveChanse = new LeaveChanseCharFeature();
        public CharFeature[] character_params;


        public Character(User user)
        {
            owner = user;
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {

            #region Адекватность, раковитость, консервативность
                //Адекватность, раковитость и консервативность ньюфагов
                case 1:
                    switch (AdvRnd.random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(AdvRnd.random.Next(0, 6));
                            rakness.SetValue(AdvRnd.random.Next(3, 11));
                            conservative.SetValue(AdvRnd.random.Next(2, 11));
                            break;
                        case 1:
                            adeq.SetValue(AdvRnd.random.Next(2, 7));
                            rakness.SetValue(AdvRnd.random.Next(2, 8));
                            conservative.SetValue(AdvRnd.random.Next(0, 9));
                            break;
                        case 2:
                            adeq.SetValue(AdvRnd.random.Next(3, 11));
                            rakness.SetValue(AdvRnd.random.Next(0, 7));
                            conservative.SetValue(AdvRnd.random.Next(0, 6));
                            break;
                    }
                    break;
                //Адекватность, раковитость и консервативность среднефагов
                case 2:
                    switch (AdvRnd.random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(AdvRnd.random.Next(4, 8));
                            rakness.SetValue(AdvRnd.random.Next(3, 11));
                            conservative.SetValue(AdvRnd.random.Next(4, 11));
                            break;
                        case 1:
                            adeq.SetValue(AdvRnd.random.Next(3, 11));
                            rakness.SetValue(AdvRnd.random.Next(2, 10));
                            conservative.SetValue(AdvRnd.random.Next(2, 8));
                            break;
                        case 2:
                            adeq.SetValue(AdvRnd.random.Next(0, 5));
                            rakness.SetValue(AdvRnd.random.Next(0, 7));
                            conservative.SetValue(AdvRnd.random.Next(0, 8));
                            break;
                    }
                    break;
                //Адекватность, раковитость и консервативность олдфагов
                case 3:
                    switch (AdvRnd.random.Next(3))
                    {
                        case 0:
                            adeq.SetValue(AdvRnd.random.Next(4, 8));
                            rakness.SetValue(AdvRnd.random.Next(3, 11));
                            conservative.SetValue(AdvRnd.random.Next(5, 11));
                            break;
                        case 1:
                            rakness.SetValue(AdvRnd.random.Next(0, 7));
                            adeq.SetValue(AdvRnd.random.Next(3, 11));
                            conservative.SetValue(AdvRnd.random.Next(0, 11));
                            break;
                        case 2:
                            rakness.SetValue(AdvRnd.random.Next(0, 5));
                            adeq.SetValue(AdvRnd.random.Next(0, 9));
                            conservative.SetValue(AdvRnd.random.Next(2, 9));
                            break;
                    }
                    break;
            }
            #endregion

            //Креативность
            if (AdvRnd.PrsChanse(10))
                creativity.SetValue(AdvRnd.random.Next(5, 11));
            else
                creativity.SetValue(AdvRnd.random.Next(5));

            //Наука
            if (AdvRnd.PrsChanse(10))
                sciense.SetValue(AdvRnd.random.Next(5, 11));
            else
                sciense.SetValue(AdvRnd.random.Next(5));

            //Гуманитаризм
            if (AdvRnd.PrsChanse(10))
                humanist.SetValue(AdvRnd.random.Next(5, 11));
            else
                humanist.SetValue(AdvRnd.random.Next(5));

            if (AdvRnd.PrsChanse(10))
                historic.SetValue(AdvRnd.random.Next(5, 11));
            else
                historic.SetValue(AdvRnd.random.Next(5));

            //Толерантность
            tolerance.SetValue(AdvRnd.random.Next(0, 11));

            //Шанс уйти
            switch (Date.current_date.Year + 1 - user.registration.Year)
            {
                case 1:
                    leaveChanse.SetValue(AdvRnd.random.Next(20, 100));
                    break;
                case 2:
                    leaveChanse.SetValue(AdvRnd.random.Next(10, 90));
                    break;
                case 3:
                    leaveChanse.SetValue(AdvRnd.random.Next(10, 70));
                    tolerance.Value -= AdvRnd.random.Next(0, 2);
                    break;
            }

            character_params = new CharFeature[] { adeq,rakness,conservative,tolerance,sciense,creativity};

        }
    }
}
