using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    static class TraitsList
    {
        static Random random = new Random();
        static public List<Trait> allTraits = new List<Trait>();
        public static void TraitsInit()
        {
            #region Фактические

            #region Дед
            Trait ded = new Trait("Дед", TraitsType.ded, 100, TraitGlobalType.fact);
            ded.AddConditions(UserFeaturesEnum.m_oldness, 1799);
            ded.AddTooltip("Бесполезный кусок пердящего олдфажьего мяса. Для сайта бесполезен");
            ded.SetColor("#9FA3FF", "#000000");
            ded.AddBlockedTrait(TraitsType.newfag);
            ded.AddBlockedTrait(TraitsType.srednefag);
            allTraits.Add(ded);
            #endregion
            #region Среднефаг
            Trait srednefag = new Trait("Среднефаг", TraitsType.srednefag, 100, TraitGlobalType.fact);
            srednefag.AddConditions(UserFeaturesEnum.m_oldness, 818);
            srednefag.AddTooltip("Можно было бы назвать и олдфагом, ну да дедам совесть не позволяет");
            srednefag.SetColor("#AAFF9B", "#000000");
            srednefag.AddBlockedTrait(TraitsType.ded);
            srednefag.AddBlockedTrait(TraitsType.newfag);
            allTraits.Add(srednefag);
            #endregion
            #region Ньюфаг
            Trait newfag = new Trait("Ньюфаг", TraitsType.newfag, 100, TraitGlobalType.fact);
            newfag.AddConditions(UserFeaturesEnum.m_oldness, 9);
            newfag.AddTooltip("Наглый, дерзкий, резкий, может надавать застарелым олдфагушкам на роже. Обычно, правда, все наоборот");
            newfag.SetColor("#FFD163", "#000000");
            newfag.AddBlockedTrait(TraitsType.srednefag);
            newfag.AddBlockedTrait(TraitsType.ded);
            allTraits.Add(newfag);
            #endregion

            #region Рак
            Trait rak = new Trait("Рак", TraitsType.rak, 95, TraitGlobalType.fact);
            rak.AddConditions(UserFeaturesEnum.rakness, 6);
            rak.AddTooltip("Сварился в кастрюле");
            rak.SetColor("#FF4C4C", "#000000");
            allTraits.Add(rak);

            #endregion

            #region Псих
            Trait madguy = new Trait("Псих", TraitsType.madguy, 90, TraitGlobalType.fact);
            madguy.AddConditions(UserFeaturesEnum.adeq, -2);
            madguy.AddTooltip("Их обожают банить модераки, но... какие же они веселые! Впрочем, раков это не касается");
            madguy.SetColor("#A60707", "#FFF259");
            madguy.AddBlockedTrait(TraitsType.inteligent);
            allTraits.Add(madguy);
            #endregion
            #region Интеллигент
            Trait inteligent = new Trait("Интеллигент", TraitsType.inteligent, 90, TraitGlobalType.fact);
            inteligent.AddConditions(UserFeaturesEnum.adeq, 7);
            inteligent.AddTooltip("Весь мат у него - чьи-то цитаты.");
            inteligent.SetColor("#3BC3DB", "#000000");
            inteligent.AddBlockedTrait(TraitsType.madguy);
            allTraits.Add(inteligent);
            #endregion

            #region Революционер
            Trait revolutioner = new Trait("Революционер", TraitsType.revolutioner, 80, TraitGlobalType.fact);
            revolutioner.AddConditions(UserFeaturesEnum.conserv, -3);
            revolutioner.AddTooltip("Перемеееен требуют наши сердцаа!");
            revolutioner.SetColor("#FFA869", "#000000");
            revolutioner.AddBlockedTrait(TraitsType.konserva);
            allTraits.Add(revolutioner);
            #endregion
            #region Консерва
            Trait konserva = new Trait("Консерва", TraitsType.konserva, 80, TraitGlobalType.fact);
            konserva.AddConditions(UserFeaturesEnum.conserv, 7);
            konserva.AddTooltip("Зачем что-то менять, когда и сейчас все хорошо? Перемены нинужны.");
            konserva.SetColor("#C69B62", "#000000");
            konserva.AddBlockedTrait(TraitsType.revolutioner);
            allTraits.Add(konserva);
            #endregion

            #region Нацист
            Trait nazi = new Trait("Нацист", TraitsType.nazi, 70, TraitGlobalType.fact);
            nazi.AddConditions(UserFeaturesEnum.tolerance, -3);
            nazi.AddTooltip("Ненавидит новичков, ньюфагов и им подобных. А еще евреев");
            nazi.SetColor("#000000", "#FFCD01");
            nazi.AddBlockedTrait(TraitsType.kindguy);
            allTraits.Add(nazi);
            #endregion
            #region Добряк
            Trait kindGay = new Trait("Добряк", TraitsType.kindguy, 80, TraitGlobalType.fact);
            kindGay.AddConditions(UserFeaturesEnum.tolerance, 7);
            kindGay.AddTooltip("Воплощение толерантности. Защитник сирых и убогих. Да и просто добрый парень(или девушка)");
            kindGay.SetColor("#DFFFAE", "#000000");
            kindGay.AddBlockedTrait(TraitsType.nazi);
            allTraits.Add(kindGay);
            #endregion

            #endregion

            #region Нации (персональные)

            #region Еврей
            Trait jew = new Trait("Еврей", TraitsType.jew, 8);
            jew.AddTooltip("Этот парень очень любит денежки и все с ними связанное");
            jew.SetColor("#0038B8", "#FFFFFF");
            jew.AddBlockedTrait(TraitsType.nazi);
            allTraits.Add(jew);
            #endregion

            #region Украинец
            Trait ucraine = new Trait("Украiниц", TraitsType.ucraine, 8);
            ucraine.AddTooltip("Сей пользователь живет в славной Украине, чем весьма гордится!");
            ucraine.SetColor("#0057b8", "#ffd700");
            ucraine.AddBlockedTrait(TraitsType.russian);
            ucraine.AddBlockedTrait(TraitsType.belorus);
            ucraine.AddBlockedTrait(TraitsType.kazah);
            allTraits.Add(ucraine);
            #endregion

            #region Серб
            Trait serb = new Trait("Серб", TraitsType.serb, 3);
            serb.AddTooltip("Истинный серб румине!");
            serb.SetColor("#003893", "#bf0a30");
            allTraits.Add(serb);
            #endregion

            #region Кумыс
            Trait kazah = new Trait("Кумыс", TraitsType.kazah, 5);
            kazah.AddTooltip("Живет в юрте");
            kazah.SetColor("#00AFCA", "#FEC50C");
            kazah.AddBlockedTrait(TraitsType.russian);
            kazah.AddBlockedTrait(TraitsType.belorus);
            kazah.AddBlockedTrait(TraitsType.ucraine);
            allTraits.Add(kazah);
            #endregion

            #region Картошка
            Trait belorus = new Trait("Картошка", TraitsType.belorus, 5);
            belorus.AddTooltip("Натуральный продукт");
            belorus.SetColor("#E54E5B", "#008400");
            belorus.AddBlockedTrait(TraitsType.russian);
            belorus.AddBlockedTrait(TraitsType.kazah);
            belorus.AddBlockedTrait(TraitsType.ucraine);
            allTraits.Add(belorus);
            #endregion

            #region Русский
            Trait russian = new Trait("Русич", TraitsType.russian, 5);
            russian.AddTooltip("Горжусь Россией!");
            russian.SetColor("#FFFFFF", "#0039A6");
            russian.AddBlockedTrait(TraitsType.belorus);
            russian.AddBlockedTrait(TraitsType.kazah);
            russian.AddBlockedTrait(TraitsType.ucraine);
            allTraits.Add(russian);
            #endregion

            #endregion

            #region Навыки-умения (фактические)

            #region Художник
            Trait arter = new Trait("Художник", TraitsType.arter, random.Next(10, 30), TraitGlobalType.fact);
            arter.AddConditions(UserFeaturesEnum.creativity, 5);
            arter.AddConditions(UserFeaturesEnum.sciense, 3);
            arter.AddTooltip("Весьма творческий юзверь, пилящий арты");
            arter.SetColor("#D2B6F7", "#000000");
            allTraits.Add(arter);
            #endregion

            #region Программист
            Trait programmer = new Trait("Программист", TraitsType.programmer, random.Next(10, 40), TraitGlobalType.fact);
            programmer.AddConditions(UserFeaturesEnum.sciense, 6);
            programmer.AddTooltip("Этот парень дельно(а может и не очень) кодит. Никогда не доделывает свои проекты");
            programmer.SetColor("#B6CDF7", "#000000");
            allTraits.Add(programmer);
            #endregion

            #region Игродел
            Trait gameMaker = new Trait("Игродел", TraitsType.gaymmaker, random.Next(5, 20), TraitGlobalType.fact);
            gameMaker.AddConditions(UserFeaturesEnum.sciense, 5);
            gameMaker.AddConditions(UserFeaturesEnum.creativity, 5);
            gameMaker.AddConditions(UserFeaturesEnum.humanist, 5);
            gameMaker.AddTooltip("Любит делать игры(90% которых он забрасывает еще в самом начале)");
            gameMaker.SetColor("#B7BBF8", "#000000");
            allTraits.Add(gameMaker);
            #endregion

            #region Композитор
            Trait composer = new Trait("Композитор", TraitsType.composer, random.Next(5, 20), TraitGlobalType.fact);
            composer.AddConditions(UserFeaturesEnum.sciense, 5);
            composer.AddConditions(UserFeaturesEnum.creativity, 5);
            composer.AddTooltip("Редкий подвид пользователей, у которых руки растут не из 5й точки. Пилит крутой саунд и музло");
            composer.SetColor("#710FA1", "#FFFFFF");
            allTraits.Add(composer);
            #endregion

            #region Писатель
            Trait writer = new Trait("Писатель", TraitsType.writer, random.Next(10, 30), TraitGlobalType.fact);
            writer.AddConditions(UserFeaturesEnum.humanist, 5);
            writer.AddConditions(UserFeaturesEnum.creativity, 3);
            writer.AddTooltip("Пишет разные рассказики, а может и книги(которые все равно никто не читает)");
            writer.SetColor("#E7E2DF", "#000000");
            allTraits.Add(writer);
            #endregion

            #region Летсплеер
            Trait lets = new Trait("Летсплеер", TraitsType.letsplayer, random.Next(5, 10), TraitGlobalType.fact);
            lets.AddConditions(UserFeaturesEnum.sciense, 3);
            lets.AddConditions(UserFeaturesEnum.creativity, 3);
            lets.AddTooltip("Господи помилуй");
            lets.SetColor("#F8B7B7", "#000000");
            allTraits.Add(lets);
            #endregion

            #region Пупер
            Trait poopMaker = new Trait("Пупер", TraitsType.pooper, random.Next(10, 20), TraitGlobalType.fact);
            poopMaker.AddConditions(UserFeaturesEnum.sciense, 4);
            poopMaker.AddConditions(UserFeaturesEnum.creativity, 6);
            poopMaker.AddTooltip("Пользуется пони-вегасом для создания смищных мемных ритпе");
            poopMaker.SetColor("#F8E3B7", "#000000");
            allTraits.Add(poopMaker);
            #endregion

            #endregion

            #region Холивары (персональные)
            #region Линуксоид
            Trait linux = new Trait("Линуксоид", TraitsType.linuxoid, random.Next(10, 20));
            linux.AddConditions(UserFeaturesEnum.sciense, 4);
            linux.AddTooltip("Неординарная личность, обожает линух и открытый код, терпеть не может виндавс");
            linux.SetColor("#FFFFFF", "#000000");
            linux.AddBlockedTrait(TraitsType.windusiatnik);
            allTraits.Add(linux);
            #endregion
            #region Виндузятник
            Trait windows = new Trait("Виндузятник", TraitsType.windusiatnik, random.Next(10, 30));
            windows.AddConditions(UserFeaturesEnum.sciense, 3);
            windows.AddTooltip("Обожает Windows и Microsoft");
            windows.SetColor("#FFFFFF", "#09749E");
            windows.AddBlockedTrait(TraitsType.linuxoid);
            allTraits.Add(windows);
            #endregion
            #endregion

            #region Персональные

            #region Кармодрочер
            Trait karma = new Trait("Кармодрочер", TraitsType.karmodroch, 15);
            karma.AddTooltip("Фапает на кармушку каждый день и изрыгает тонны проклятий в адрес тех, кто его минусит");
            karma.SetColor("#FFFFFF", "#176C8E");
            allTraits.Add(karma);
            #endregion
            #region Натурал
            Trait natural = new Trait("Натурал", TraitsType.natural, 50);
            natural.AddConditions(UserFeaturesEnum.rakness, -5);
            natural.AddConditions(UserFeaturesEnum.sciense, 3);
            natural.AddConditions(UserFeaturesEnum.creativity, 3);
            natural.AddTooltip("Идеален во всем. Почти");
            natural.SetColor("#FFFFFF", "#A64EA5");
            natural.AddBlockedTrait(TraitsType.animeshnik);
            natural.AddBlockedTrait(TraitsType.furry);
            natural.AddBlockedTrait(TraitsType.nevecherya);
            allTraits.Add(natural);
            #endregion
            #region Геймер
            Trait gamer = new Trait("Геймер", TraitsType.gaymer, random.Next(20, 70));
            gamer.AddTooltip("Любит играть в gayms");
            gamer.SetColor("#44413C", "#EFBF39");
            allTraits.Add(gamer);
            #endregion
            #region Пират
            Trait pirate = new Trait("Пират", TraitsType.pirate, random.Next(10, 30));
            pirate.AddTooltip("Йохохо и репак с торрента!");
            pirate.SetColor("#2C2C2C", "#DFDFDF");
            allTraits.Add(pirate);
            #endregion
            #region Властный
            Trait wants_power = new Trait("Властный", TraitsType.Wpower, random.Next(10, 20));
            wants_power.AddTooltip("Такому лишь бы покомандовать. И да - он ХОЧЕТ стать модератором, ГРом или еще кем похуже!");
            wants_power.SetColor("#FFFFFF", "#450086");
            allTraits.Add(wants_power);
            #endregion
            #region Мажор
            Trait major = new Trait("Мажор", TraitsType.major, random.Next(10, 20));
            major.AddTooltip("У него интернет 100 мбит, компьютер за 80к и игры лицензионные");
            major.SetColor("#A1009F", "#F1F100");
            allTraits.Add(major);
            #endregion
            #region Анимешник
            Trait anime = new Trait("Анимешник", TraitsType.animeshnik, random.Next(10, 20));
            anime.AddTooltip("Этот юзер балуется визжащими японскими школьницами, опасно!");
            anime.SetColor("#FFFFFF", "#1A8400");
            anime.AddBlockedTrait(TraitsType.natural);
            allTraits.Add(anime);
            #endregion
            #region Любитель новостей
            Trait newslover = new Trait("Любитель новостей", TraitsType.newslover, random.Next(5, 20));
            newslover.AddTooltip("Очень любит пилить НОВОСТИ. Желательно с курсивным комик сансом!");
            newslover.SetColor("#FFFFFF", "#006900");
            allTraits.Add(newslover);
            #endregion
            #region Невечеря
            Trait pitar = new Trait("Невечеря", TraitsType.nevecherya, random.Next(5, 20));
            pitar.AddConditions(UserFeaturesEnum.adeq, -6);
            pitar.AddConditions(UserFeaturesEnum.rakness, 3);
            pitar.AddTooltip("Не натурал - и этим все сказано");
            pitar.SetColor("#750787", "#FFED00");
            pitar.AddBlockedTrait(TraitsType.natural);
            allTraits.Add(pitar);
            #endregion
            #region Бидла
            Trait bidla = new Trait("Бидла", TraitsType.bidla, random.Next(5, 30));
            bidla.AddConditions(UserFeaturesEnum.rakness, 3);
            bidla.AddTooltip("Родился в трениках и кульком семок в руках. Папаня турник");
            bidla.SetColor("#000000", "#FFFFFF");
            bidla.AddBlockedTrait(TraitsType.natural);
            allTraits.Add(bidla);
            #endregion
            #region Мемная харя
            Trait mem = new Trait("Мемный паря", TraitsType.memguy, random.Next(5, 40));
            mem.AddConditions(UserFeaturesEnum.creativity, 3);
            mem.AddTooltip("Каждое сообщение - мем");
            mem.SetColor("#FFFFFF", "#52541D");
            allTraits.Add(mem);
            #endregion


            #region Фуррифаг
            Trait furry = new Trait("Фуррифаг", TraitsType.furry, random.Next(5, 15));
            furry.AddConditions(UserFeaturesEnum.rakness, 3);
            furry.AddConditions(UserFeaturesEnum.adeq, -7);
            furry.AddTooltip("Больной пользователь, представляющий себя антропоморфным животным");
            furry.SetColor("#ADA797", "#553821");
            furry.AddBlockedTrait(TraitsType.natural);
            allTraits.Add(furry);
            #endregion
            #region Пане
            Trait pane = new Trait("Пане", TraitsType.pane, random.Next(5, 25));
            pane.AddTooltip("Дружба это магия!");
            pane.SetColor("#EC3C95", "#FFFFFF");
            allTraits.Add(pane);
            #endregion
            #region Sadboi
            Trait sadboi = new Trait("Сэдboi", TraitsType.sadboi, random.Next(5, 10));
            sadboi.AddTooltip("Любит 'уходить' с руминя и хорошенечко поныть о жизни");
            sadboi.SetColor("#0A005F", "#9BD9FF");
            sadboi.AddBlockedTrait(TraitsType.natural);
            allTraits.Add(sadboi);
            #endregion
            #region Катлета
            Trait katleta = new Trait("Катлета", TraitsType.katleta, random.Next(5, 10));
            katleta.AddTooltip("Катлетный питар. Или питар из катлет. Или катлета из питара. Черт его разберет");
            katleta.SetColor("#FFFFFF", "#663A1D");
            allTraits.Add(katleta);
            #endregion
            #region Ром
            Trait rom = new Trait("Ром", TraitsType.rom, random.Next(5, 10));
            rom.AddTooltip("Ром в каждый дом!");
            rom.SetColor("#D8ADB2", "#732B03");
            allTraits.Add(rom);
            #endregion
            #region Бомж
            Trait bomj = new Trait("Бомж", TraitsType.bomj, random.Next(5, 10));
            bomj.AddTooltip("Живет на задворках форума или и того хуже - в бездне");
            bomj.SetColor("#A67A6F", "#0F0F1B");
            allTraits.Add(bomj);
            #endregion
            #region Алкаш
            Trait alkash = new Trait("Алкаш", TraitsType.alkash, random.Next(5, 10));
            alkash.AddConditions(UserFeaturesEnum.adeq, -6);
            alkash.AddTooltip("Пользователь, находящийся на румине по большей части в нетрезвом состоянии");
            alkash.SetColor("#FAFAFA", "#00549A");
            allTraits.Add(alkash);
            #endregion
            #region Коммунист
            Trait commie = new Trait("Коммунист", TraitsType.commie, random.Next(5, 30));
            commie.AddTooltip("Союз нерушимых республик свободных...");
            commie.SetColor("#CC0000", "#FFD700");
            allTraits.Add(commie);
            #endregion
            #region Лидер
            Trait leader = new Trait("Лидер", TraitsType.leader, random.Next(5, 15));
            leader.AddTooltip("Именно он основывает братвы, возглавляет вастания и ведет народ за собой");
            leader.SetColor("#9C0000", "#DDDAF2");
            allTraits.Add(leader);
            #endregion
            #region Перфекционист
            Trait perfectionist = new Trait("Перфекционист", TraitsType.accurateguy, random.Next(5, 20));
            perfectionist.AddTooltip("Сделать что-то не на идеальном уровне - сущий кошмар для этого пользователя");
            perfectionist.SetColor("#0600BF", "#FFFFFF");
            allTraits.Add(perfectionist);
            #endregion
            #region Ремувер кебаба
            Trait kebab = new Trait("Ремувер кебаба", TraitsType.kebabremover, random.Next(5, 10));
            kebab.AddTooltip("Remove kebab");
            kebab.SetColor("#E1AD7B", "#170805");
            allTraits.Add(kebab);
            #endregion


            #region Тиран
            Trait tiran = new Trait("Тиран", TraitsType.tiran, random.Next(5, 10));
            tiran.AddTooltip("Беспощаден к недругам");
            tiran.SetColor("#FFFFFF", "#000000");
            allTraits.Add(tiran);
            #endregion
            #region Верун
            Trait christ = new Trait("Верун", TraitsType.christ, random.Next(5, 15));
            christ.AddTooltip("Боженька спаси меня от этих грешников!");
            christ.SetColor("#FFFFFF", "#8A7C00");
            christ.AddBlockedTrait(TraitsType.atheist);
            allTraits.Add(christ);
            #endregion
            #region Аметист
            Trait ametist = new Trait("Аметист", TraitsType.atheist, random.Next(5, 30));
            ametist.AddTooltip("Глупые веруны! Это все из-за них Россиюшка в бездну катится");
            ametist.SetColor("#FFFFFF", "#52541D");
            ametist.AddBlockedTrait(TraitsType.christ);
            allTraits.Add(ametist);
            #endregion
            #region Краб
            Trait krab = new Trait("Краб", TraitsType.krab, random.Next(5, 10));
            krab.AddTooltip("Как Путин");
            krab.SetColor("#BEF5FF", "#AA4630");
            allTraits.Add(krab);
            #endregion
            #region Буддист
            Trait buddist = new Trait("Буддист", TraitsType.buddist, random.Next(5));
            buddist.AddTooltip("Группы, симпатии, рейтинг - все это отвлекает от познания истинной сущности бытия");
            buddist.SetColor("#BFBFBF", "#000000");
            allTraits.Add(buddist);
            #endregion
            #region Гномик
            Trait gnomik = new Trait("Гномик", TraitsType.gnomik, random.Next(5, 20));
            gnomik.AddTooltip("*Г(*К(*Н)). Ну вы поняли, это была joek!");
            gnomik.SetColor("#FFFFFF", "#667500");
            allTraits.Add(gnomik);
            #endregion
            #region Чай
            Trait tea = new Trait("Чяй", TraitsType.tea, random.Next(5, 10));
            tea.AddTooltip("Tea Corporation");
            tea.SetColor("#863C00", "#FFFFFF");
            allTraits.Add(tea);
            #endregion
            #region Рэпер
            Trait rap = new Trait("Рэпер", TraitsType.reper, random.Next(5, 10));
            rap.AddTooltip("Рэпчик наше все! еее бейби!");
            rap.SetColor("#FFFFFF", "#000000");
            allTraits.Add(rap);
            #endregion
            #region Фашист
            Trait fashik = new Trait("Фашист", TraitsType.fashik, random.Next(5, 15));
            fashik.AddConditions(UserFeaturesEnum.tolerance, -3);
            fashik.AddTooltip("На аватарке Гитлер, в подписи цитаты из майнкампфа, в сообщениях heil");
            fashik.SetColor("#DD0000", "#000000");
            allTraits.Add(fashik);
            #endregion
            #region Нигга
            Trait nigga = new Trait("Нигга", TraitsType.nigga, random.Next(5, 10));
            nigga.AddTooltip("Обожает KFC, рэпчег и своих братюнь на Грув-Румине-Хоум. Йоу!");
            nigga.SetColor("#54372F", "#FFFFFF");
            allTraits.Add(nigga);
            #endregion
            #region Учитель русского языка
            Trait teacher = new Trait("Учитель русского языка", TraitsType.teacher, 5);
            teacher.AddConditions(UserFeaturesEnum.adeq, 5);
            teacher.AddTooltip("Пишет с ошибками");
            teacher.SetColor("#FFFFFF", "000000");
            allTraits.Add(teacher);
            #endregion

            #endregion
        }

        //Выдача пользователю трейтов
        public static List<Trait> ReturnTraits(User user)
        {
            List<Trait> traits_user = new List<Trait>();
            for (int i = 0; i < allTraits.Count; i++)
            {
                Trait trait = allTraits[i];
                if (AdvRnd.PrsChanse((short)trait.chanse) && !ConditionCheck(user, trait, traits_user))
                {
                    traits_user.Add(trait);
                }
            }
            return traits_user;
        }
        //Возвращение значения доступен ли трейт для пользователя
        public static bool ConditionCheck(User user, Trait trait, List<Trait> traits_user)
        {
            bool check_failed = false;
            foreach (Trait trait_check in traits_user)
            {
                if (trait_check.blocked_types.Contains(trait.type))
                    return true;
            }
            check_failed = CheckUserParams(user, trait, check_failed);
            return check_failed;
        }
        //Проверка условий трейта связанных со значениями параметра
        private static bool CheckUserParams(User user, Trait trait, bool check_failed)
        {
            for (int i = 0; i < trait.conditions.Count; i++)
            {

                //Проверка значений параметра, который должны быть БОЛЬШЕ условия
                if (trait.conditions.ElementAt(i).Value >= 0)
                {
                    switch (trait.conditions.ElementAt(i).Key)
                    {
                        case UserFeaturesEnum.creativity:
                            if (user.character.creativity.Value < trait.conditions[UserFeaturesEnum.creativity])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.sciense:
                            if (user.character.sciense.Value < trait.conditions[UserFeaturesEnum.sciense])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.adeq:
                            if (user.character.adeq.Value < trait.conditions[UserFeaturesEnum.adeq])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.rakness:
                            if (user.character.rakness.Value < trait.conditions[UserFeaturesEnum.rakness])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.tolerance:
                            if (user.character.tolerance.Value < trait.conditions[UserFeaturesEnum.tolerance])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.conserv:
                            if (user.character.conservative.Value < trait.conditions[UserFeaturesEnum.conserv])
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.humanist:
                            int hum_compare = -trait.conditions[UserFeaturesEnum.humanist];
                            if (user.character.humanist.Value > hum_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.historic:
                            int hist_compare = -trait.conditions[UserFeaturesEnum.historic];
                            if (user.character.historic.Value > hist_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.nothing:
                            break;
                        case UserFeaturesEnum.m_oldness:
                            string code = trait.conditions[UserFeaturesEnum.m_oldness].ToString();
                            int min_oldness = 0;
                            int max_oldness = 0;
                            if (code.Length == 1)
                            {
                                max_oldness = Convert.ToInt32(code.Substring(0, 1));
                            }
                            else if (code.Length == 3)
                            {
                                min_oldness = Convert.ToInt32(code.Substring(0, 1));
                                max_oldness = Convert.ToInt32(code.Substring(1, 2));
                            }
                            else if (code.Length == 4)
                            {
                                min_oldness = Convert.ToInt32(code.Substring(0, 2));
                                max_oldness = Convert.ToInt32(code.Substring(2, 2));
                            }

                            if (user.month_oldness < min_oldness || user.month_oldness > max_oldness)
                                check_failed = true;
                            break;
                    }
                }
                //Проверка значений параметра, который должны быть МЕНЬШЕ условия
                else
                {
                    switch (trait.conditions.ElementAt(i).Key)
                    {
                        case UserFeaturesEnum.creativity:
                            int cr_compare = -trait.conditions[UserFeaturesEnum.creativity];
                            if (user.character.creativity.Value > cr_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.sciense:
                            int sc_compare = -trait.conditions[UserFeaturesEnum.sciense];
                            if (user.character.sciense.Value > sc_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.adeq:
                            int adeq_compare = -trait.conditions[UserFeaturesEnum.adeq];
                            if (user.character.adeq.Value > adeq_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.rakness:
                            int rak_compare = -trait.conditions[UserFeaturesEnum.rakness];
                            if (user.character.rakness.Value > rak_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.conserv:
                            int cons_compare = -trait.conditions[UserFeaturesEnum.conserv];
                            if (user.character.conservative.Value > cons_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.tolerance:
                            int toler_compare = -trait.conditions[UserFeaturesEnum.tolerance];
                            if (user.character.tolerance.Value > toler_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.humanist:
                            int hum_compare = -trait.conditions[UserFeaturesEnum.humanist];
                            if (user.character.humanist.Value > hum_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.historic:
                            int hist_compare = -trait.conditions[UserFeaturesEnum.historic];
                            if (user.character.historic.Value > hist_compare)
                                check_failed = true;
                            break;
                        case UserFeaturesEnum.nothing:
                            break;
                    }
                }

            }

            return check_failed;
        }


        //Поиск трейтов по id и типу
        public static Trait SearchTrait(int id)
        {
            Trait trait = allTraits[0];
            for (int i = 0; i < allTraits.Count; i++)
            {
                if (allTraits[i].id_num == id)
                    trait = allTraits[i];
            }
            return trait;
        }
        public static Trait SearchTrait(TraitsType type)
        {
            Trait trait = allTraits[0];
            foreach (Trait trait_s in allTraits)
            {
                if (trait_s.type == type)
                {
                    trait = trait_s;
                }
            }
            return trait;
        }
    }
}
