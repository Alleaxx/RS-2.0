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
        static public Dictionary<Traits, Trait> AllTraits = new Dictionary<Traits, Trait>();
        public static void TraitsInit()
        {
            #region По хар-кам

            #region Дед
            Dictionary<UserFeaturesEnum, int> ded_conditions = new Dictionary<UserFeaturesEnum, int>();
            ded_conditions.Add(UserFeaturesEnum.m_oldness, 1799);
            AllTraits.Add(Traits.ded, new Trait
                (Traits.ded,100, "Дед", "Бесполезный пердящий кусок олдфажьего мяса",
                ded_conditions, "#9FA3FF", "#000000",TraitType.oldness));
            #endregion
            #region Среднефаг
            Dictionary<UserFeaturesEnum, int> sredn_conditions = new Dictionary<UserFeaturesEnum, int>();
            sredn_conditions.Add(UserFeaturesEnum.m_oldness, 818);
            AllTraits.Add(Traits.srednefag, new Trait
                (Traits.srednefag, 100, "Среднефаг", "Можно было бы назвать и олдфагом, ну да олдфагам совесть не позволяет",
                sredn_conditions, "#AAFF9B","#000000", TraitType.oldness));
            #endregion
            #region Ньюфаг
            Dictionary<UserFeaturesEnum, int> newf_conditions = new Dictionary<UserFeaturesEnum, int>();
            newf_conditions.Add(UserFeaturesEnum.m_oldness, 9);
            AllTraits.Add(Traits.newfag, new Trait
                (Traits.newfag, 100, "Ньюфаг", "Именно таких самодовольные деды любят гнать в хвост и в гриву",
                newf_conditions, "#FFD163","#000000", TraitType.oldness));
            #endregion

            #region Рак
            Dictionary<UserFeaturesEnum, int> rak_conditions = new Dictionary<UserFeaturesEnum, int>();
            rak_conditions.Add(UserFeaturesEnum.rakness, 6);
            AllTraits.Add(Traits.rak, new Trait
                (Traits.rak, 100, "Рак", "Сварился в кастрюле",
                rak_conditions, "#FF4C4C","#000000", TraitType.chRak));
            #endregion

            #region Псих
            Dictionary<UserFeaturesEnum, int> madguy_conditions = new Dictionary<UserFeaturesEnum, int>();
            madguy_conditions.Add(UserFeaturesEnum.adeq, -3);
            AllTraits.Add(Traits.madguy, new Trait
                (Traits.madguy, 75, "Псих", "Их обожают банить модераки, но... какие же они веселые! Впрочем, раков это не касается",
                madguy_conditions, "#A60707", "#FFF259", TraitType.chAdeq));
            #endregion
            #region Интеллигент
            Dictionary <UserFeaturesEnum, int> intel_conditions = new Dictionary<UserFeaturesEnum, int>();
            intel_conditions.Add(UserFeaturesEnum.adeq, 6);
            AllTraits.Add(Traits.inteligent, new Trait
                (Traits.inteligent, 75, "Интеллигент", "Весь мат у него - чьи-то цитаты.",
                intel_conditions, "#3BC3DB","#000000", TraitType.chAdeq));
            #endregion

            #region Революционер
            Dictionary<UserFeaturesEnum, int> revolutioner_conditions = new Dictionary<UserFeaturesEnum, int>();
            revolutioner_conditions.Add(UserFeaturesEnum.conserv, -4);
            AllTraits.Add(Traits.revolutioner, new Trait
                (Traits.revolutioner, 25, "Революционер", "Перемеееен требуют наши сердцаа!",
                revolutioner_conditions, "#FFA869","#000000", TraitType.chCons));
            #endregion
            #region Консерва
            Dictionary<UserFeaturesEnum, int> conserv_conditions = new Dictionary<UserFeaturesEnum, int>();
            conserv_conditions.Add(UserFeaturesEnum.conserv, 6);
            AllTraits.Add(Traits.konserva, new Trait
                (Traits.konserva, 25, "Консерва", "Зачем что-то менять, когда и сейчас все хорошо? Перемены нинужны.",
                conserv_conditions, "#C69B62", "#000000", TraitType.chCons));
            #endregion

            #region Нацист
            Dictionary<UserFeaturesEnum, int> nazi_conditions = new Dictionary<UserFeaturesEnum, int>();
            nazi_conditions.Add(UserFeaturesEnum.tolerance, -5);
            AllTraits.Add(Traits.nazi, new Trait
                (Traits.nazi, 25, "Нацист", "Ненавидит новичков, ньюфагов и им подобных. А еще евреев",
                nazi_conditions, "#000000", "#FFCD01",TraitType.chTolerance));
            #endregion
            #region Добряк
            Dictionary<UserFeaturesEnum, int> kindguy_conditions = new Dictionary<UserFeaturesEnum, int>();
            kindguy_conditions.Add(UserFeaturesEnum.tolerance, 6);
            AllTraits.Add(Traits.kindguy, new Trait
                (Traits.kindguy, 25, "Добряк", "Воплощение толерантности. Наверняка либерал, который защищает всех и каждого!",
                kindguy_conditions, "#DFFFAE", "#000000", TraitType.chTolerance));
            #endregion

            #region Кармодрочер
            Dictionary<UserFeaturesEnum, int> karmodr_conditions = new Dictionary<UserFeaturesEnum, int>();
            karmodr_conditions.Add(UserFeaturesEnum.rakness, 5);
            AllTraits.Add(Traits.karmodroch, new Trait
                (Traits.karmodroch, 15, "Кармодрочер", "Фапает на кармушку каждый день и изрыгает тонны проклятий в адрес тех, кто его минусит.",
                karmodr_conditions, "#FFFFFF", "#176C8E",TraitType.chRak));
            #endregion
            #endregion

            #region Нации
            #region Еврей
            Dictionary<UserFeaturesEnum, int> jew_conditions = new Dictionary<UserFeaturesEnum, int>();
            jew_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.jew, new Trait
                (Traits.jew, 8, "Еврей", "Этот парень очень любит денежки и все с ними связанное",
                jew_conditions, "#0038B8", "#FFFFFF",TraitType.character));
            #endregion
            #region Украинец
            Dictionary<UserFeaturesEnum, int> ucraine_conditions = new Dictionary<UserFeaturesEnum, int>();
            ucraine_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.ucraine, new Trait
                (Traits.ucraine, 5, "Украинец", "Сей пользователь живет в славной Украине!",
                ucraine_conditions, "#0057b8", "#ffd700",TraitType.nation));
            #endregion
            #region Серб
            Dictionary<UserFeaturesEnum, int> serb_conditions = new Dictionary<UserFeaturesEnum, int>();
            serb_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.serb, new Trait
                (Traits.serb, 3, "Серб", "Истинный серб румине!",
                serb_conditions, "#003893", "#bf0a30",TraitType.character));
            #endregion
            #region Кумыс
            Dictionary<UserFeaturesEnum, int> kazah_conditions = new Dictionary<UserFeaturesEnum, int>();
            kazah_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.kazah, new Trait
                (Traits.kazah, 3, "Кумыс", "Живет в юрте",
                kazah_conditions, "#00AFCA", "#FEC50C",TraitType.nation));
            #endregion
            #region Картошка
            Dictionary<UserFeaturesEnum, int> belorus_conditions = new Dictionary<UserFeaturesEnum, int>();
            belorus_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.belorus, new Trait
                (Traits.belorus, 3, "Картошка", "Натуральный белорус! Бацька наше все!",
                belorus_conditions, "#E54E5B", "#008400",TraitType.nation));
            #endregion
            #region Русский
            Dictionary<UserFeaturesEnum, int> russian_conditions = new Dictionary<UserFeaturesEnum, int>();
            russian_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.russian, new Trait
                (Traits.russian, 5, "Русич", "Горжусь Россией!",
                russian_conditions, "#FFFFFF", "#0039A6",TraitType.nation));
            #endregion


            #endregion

            #region Навыки-умения

            #region Художник
            Dictionary<UserFeaturesEnum, int> hud_conditions = new Dictionary<UserFeaturesEnum, int>();
            hud_conditions.Add(UserFeaturesEnum.creativity, 5);
            Dictionary<UserFeaturesEnum, int> hud_buffs = new Dictionary<UserFeaturesEnum, int>();
            hud_buffs.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.arter, new Trait
                (Traits.arter, random.Next(10, 20), "Художник", "Весьма творческий юзверь, пилящий арты.",
                hud_conditions, "#D2B6F7", "#000000", TraitType.proffesional));
            #endregion
            #region Программист
            Dictionary<UserFeaturesEnum, int> program_conditions = new Dictionary<UserFeaturesEnum, int>();
            program_conditions.Add(UserFeaturesEnum.sciense, 5);
            AllTraits.Add(Traits.programmer, new Trait
                (Traits.programmer,random.Next(10,30) , "Программист", "Этот парень дельно(а может и не очень) кодит.",
                program_conditions, "#B6CDF7", "#000000", TraitType.proffesional));
            #endregion
            #region Игродел
            Dictionary<UserFeaturesEnum, int> gaymmaker_conditions = new Dictionary<UserFeaturesEnum, int>();
            gaymmaker_conditions.Add(UserFeaturesEnum.sciense, 4);
            gaymmaker_conditions.Add(UserFeaturesEnum.creativity, 4);
            AllTraits.Add(Traits.gaymmaker, new Trait
                (Traits.gaymmaker, random.Next(5, 10), "Игродел", "Любит делать игры(90% которых он забрасывает еще в самом начале).",
                gaymmaker_conditions, "#B7BBF8", "#000000", TraitType.proffesional));
            #endregion
            #region Композитор
            Dictionary<UserFeaturesEnum, int> composer_conditions = new Dictionary<UserFeaturesEnum, int>();
            composer_conditions.Add(UserFeaturesEnum.sciense, 5);
            composer_conditions.Add(UserFeaturesEnum.creativity, 5);
            AllTraits.Add(Traits.composer, new Trait
                (Traits.composer, random.Next(5, 10), "Композитор", "Редкий подвид чуваков, у которых руки растут не из 5й точки. Пилит крутой саунд и музло.",
                composer_conditions, "#710FA1","#FFFFFF",TraitType.proffesional));
            #endregion
            #region Писатель
            Dictionary<UserFeaturesEnum, int> writer_conditions = new Dictionary<UserFeaturesEnum, int>();
            writer_conditions.Add(UserFeaturesEnum.creativity, 3);
            AllTraits.Add(Traits.writer, new Trait
                (Traits.writer, random.Next(10, 30), "Писатель", "Пишет разные рассказики, а может и книги(которые все равно никто не читает.",
                writer_conditions, "#E7E2DF", "#000000", TraitType.proffesional));
            #endregion

            #region Летсплеер
            Dictionary<UserFeaturesEnum, int> lets_conditions = new Dictionary<UserFeaturesEnum, int>();
            lets_conditions.Add(UserFeaturesEnum.rakness, 5);
            AllTraits.Add(Traits.letsplayer, new Trait
                (Traits.letsplayer, random.Next(5, 15), "Летсплеер", "Этот пользователь пилит летсплаи по всяким играм",
                lets_conditions, "#F8B7B7", "#000000", TraitType.chRak));
            #endregion
            #region Пупер
            Dictionary<UserFeaturesEnum, int> poop_conditions = new Dictionary<UserFeaturesEnum, int>();
            poop_conditions.Add(UserFeaturesEnum.sciense, 3);
            poop_conditions.Add(UserFeaturesEnum.creativity, 5);
            AllTraits.Add(Traits.pooper, new Trait
                (Traits.pooper, random.Next(5, 15), "Пупер", "Пользуется пони-вегасом для создания смищных мемных ритпе.",
                poop_conditions, "#F8E3B7", "#000000", TraitType.proffesional));
            #endregion

            #endregion

            #region Холивары
            #region Линуксоид
            Dictionary<UserFeaturesEnum, int> linux_conditions = new Dictionary<UserFeaturesEnum, int>();
            linux_conditions.Add(UserFeaturesEnum.sciense, 4);
            AllTraits.Add(Traits.linuxoid, new Trait
                (Traits.linuxoid, random.Next(10, 20), "Линуксоид", "Неординарная личность, обожает линух и открытый код, терпеть не может виндавс.",
                linux_conditions,"#FFFFFF","#000000",TraitType.holywar));
            #endregion
            #region Виндузятник
            Dictionary<UserFeaturesEnum, int> wind_conditions = new Dictionary<UserFeaturesEnum, int>();
            wind_conditions.Add(UserFeaturesEnum.sciense, 2);
            AllTraits.Add(Traits.windusiatnik, new Trait
                (Traits.windusiatnik, random.Next(10, 20), "Виндузятник", "Обожает Windows и Microsoft.",
                wind_conditions, "#FFFFFF", "#09749E",TraitType.holywar));
            #endregion
            #endregion

            #region Личные характеристики
            #region Натурал
            Dictionary<UserFeaturesEnum, int> nat_conditions = new Dictionary<UserFeaturesEnum, int>();
            nat_conditions.Add(UserFeaturesEnum.rakness, -5);
            nat_conditions.Add(UserFeaturesEnum.sciense, 4);
            nat_conditions.Add(UserFeaturesEnum.creativity, 4);
            nat_conditions.Add(UserFeaturesEnum.tolerance, -7);
            AllTraits.Add(Traits.natural, new Trait
                (Traits.natural, random.Next(30, 60), "Натурал", "Идеален во всем.",
                nat_conditions, "#FFFFFF", "#A64EA5",TraitType.proffesional));
            #endregion
            #region Геймер
            Dictionary<UserFeaturesEnum, int> gaymer_conditions = new Dictionary<UserFeaturesEnum, int>();
            gaymer_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.gaymer, new Trait
                (Traits.gaymer, random.Next(20, 70), "Геймер", "Любит играть в gayms",
                gaymer_conditions, "#44413C", "#EFBF39",TraitType.character));
            #endregion
            #region Пират
            Dictionary<UserFeaturesEnum, int> pirate_conditions = new Dictionary<UserFeaturesEnum, int>();
            pirate_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.pirate, new Trait
                (Traits.pirate, random.Next(20, 40), "Пират", "Йохохо и репак с торрента!",
                pirate_conditions, "#2C2C2C", "#DFDFDF",TraitType.character));
            #endregion
            #region Властный
            Dictionary<UserFeaturesEnum, int> Wpower_conditions = new Dictionary<UserFeaturesEnum, int>();
            Wpower_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.Wpower, new Trait
                (Traits.Wpower, random.Next(10, 20), "Властный", "Такому лишь бы покомандовать. И да - он ХОЧЕТ стать модератором, ГРом или еще кем похуже!",
                Wpower_conditions, "#FFFFFF", "#450086",TraitType.character));
            #endregion
            #region Мажор
            Dictionary<UserFeaturesEnum, int> major_conditions = new Dictionary<UserFeaturesEnum, int>();
            major_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.major, new Trait
                (Traits.major, random.Next(10, 20), "Мажор", "У него интернет 100 мбит, компьютер за 80к и игры лицензионные",
                major_conditions, "#A1009F", "#F1F100",TraitType.character));
            #endregion
            #region Анимешник
            Dictionary<UserFeaturesEnum, int> anime_conditions = new Dictionary<UserFeaturesEnum, int>();
            anime_conditions.Add(UserFeaturesEnum.rakness, 2);
            AllTraits.Add(Traits.animeshnik, new Trait
                (Traits.animeshnik, random.Next(20, 40), "Анимешник", "Этот юзер балуется визжащими японскими школьницами, опасно!",
                anime_conditions, "#FFFFFF", "#1A8400",TraitType.character));
            #endregion
            #region Любитель новостей
            Dictionary<UserFeaturesEnum, int> newslover_conditions = new Dictionary<UserFeaturesEnum, int>();
            newslover_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.newslover, new Trait
                (Traits.newslover, random.Next(5, 20), "Любитель новостей", "Очень любит пилить НОВОСТИ. Желательно с курсивным комик сансом!",
                newslover_conditions, "#FFFFFF", "#006900",TraitType.character));
            #endregion
            #region Невечеря
            Dictionary<UserFeaturesEnum, int> pit_conditions = new Dictionary<UserFeaturesEnum, int>();
            pit_conditions.Add(UserFeaturesEnum.rakness, 3);
            pit_conditions.Add(UserFeaturesEnum.adeq, -6);
            AllTraits.Add(Traits.nevecherya, new Trait
                (Traits.nevecherya, random.Next(5, 20), "Невечеря", "Ненатурал - и этим все сказано",
                pit_conditions, "#750787", "#FFED00",TraitType.proffesional));
            #endregion
            #region Бидла
            Dictionary<UserFeaturesEnum, int> bidla_conditions = new Dictionary<UserFeaturesEnum, int>();
            bidla_conditions.Add(UserFeaturesEnum.rakness, 3);
            AllTraits.Add(Traits.bidla, new Trait
                (Traits.bidla, random.Next(5, 30), "Бидла", "Родился в трениках и кульком семок в руках. Папаня турник.",
                bidla_conditions, "#000000", "#FFFFFF",TraitType.character));
            #endregion
            #region Качок
            Dictionary<UserFeaturesEnum, int> kach_conditions = new Dictionary<UserFeaturesEnum, int>();
            kach_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.kachok, new Trait
                (Traits.kachok, random.Next(5, 20), "Качок", "Очень любит качаться.",
                kach_conditions,"#FFFFFF","#000000",TraitType.character));
            #endregion
            #region Мемная харя
            Dictionary<UserFeaturesEnum, int> mem_conditions = new Dictionary<UserFeaturesEnum, int>();
            mem_conditions.Add(UserFeaturesEnum.creativity, 3);
            AllTraits.Add(Traits.memguy, new Trait
                (Traits.memguy, random.Next(10, 50), "Мемная харя", "Каждое сообщение у него - мем",
                mem_conditions, "#FFFFFF", "#52541D",TraitType.character));
            #endregion

            #region Фуррифаг
            Dictionary<UserFeaturesEnum, int> furry_conditions = new Dictionary<UserFeaturesEnum, int>();
            furry_conditions.Add(UserFeaturesEnum.rakness, 3);
            furry_conditions.Add(UserFeaturesEnum.adeq, -7);
            AllTraits.Add(Traits.furry, new Trait
                (Traits.furry, random.Next(5, 15), "Фуррифаг", "Больной пользователь, представляющий себя антропоморфным животным.",
                furry_conditions, "#ADA797", "#553821",TraitType.character));
            #endregion
            #region Пане
            Dictionary<UserFeaturesEnum, int> pane_conditions = new Dictionary<UserFeaturesEnum, int>();
            pane_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.pane, new Trait
                (Traits.pane, random.Next(5, 25), "Пане", "Дружба это магия!",
                pane_conditions, "#EC3C95", "#FFFFFF",TraitType.character));
            #endregion

            #region Sadboi
            Dictionary<UserFeaturesEnum, int> sadboi_conditions = new Dictionary<UserFeaturesEnum, int>();
            sadboi_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.sadboi, new Trait
                (Traits.sadboi, random.Next(5, 10), "Сэдboi", "Любит 'уходить' с руминя и хорошенечко поныть о жизни.",
                sadboi_conditions, "#0A005F", "#9BD9FF",TraitType.character));
            #endregion
            #region Крыса
            Dictionary<UserFeaturesEnum, int> vilka_conditions = new Dictionary<UserFeaturesEnum, int>();
            vilka_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.vilka, new Trait
                (Traits.vilka, random.Next(5, 15), "Крыса", "Нехороший человек, хорошего от него не жди.",
                vilka_conditions, "#080A07", "#7AD58C", TraitType.character));
            #endregion
            #region Катлета
            Dictionary<UserFeaturesEnum, int> katleta_conditions = new Dictionary<UserFeaturesEnum, int>();
            katleta_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.katleta, new Trait
                (Traits.katleta, random.Next(5, 10), "Котлета", "Катлетный питар. Или питар из катлет. Или катлета из питара. Черт его разберет.",
                katleta_conditions, "#FFFFFF", "#663A1D", TraitType.character));
            #endregion
            #region Ром
            Dictionary<UserFeaturesEnum, int> rom_conditions = new Dictionary<UserFeaturesEnum, int>();
            rom_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.rom, new Trait
                (Traits.rom, random.Next(5, 10), "Ром", "Ром в каждый дом!",
                rom_conditions, "#D8ADB2", "#732B03", TraitType.character));
            #endregion
            #region Бомж
            Dictionary<UserFeaturesEnum, int> bomj_conditions = new Dictionary<UserFeaturesEnum, int>();
            bomj_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.bomj, new Trait
                (Traits.bomj, random.Next(5, 20), "Бомж", "Живет на задворках форума или и того хуже - в бездне.",
                bomj_conditions, "#A67A6F", "#0F0F1B", TraitType.character));
            #endregion
            #region Алкаш
            Dictionary<UserFeaturesEnum, int> alkash_conditions = new Dictionary<UserFeaturesEnum, int>();
            alkash_conditions.Add(UserFeaturesEnum.adeq, -7);
            AllTraits.Add(Traits.alkash, new Trait
                (Traits.alkash, random.Next(5, 15), "Алкаш", "Пользователь, находящийся на румине по большей части в нетрезвом состоянии.",
                alkash_conditions, "#FAFAFA", "#00549A", TraitType.character));
            #endregion
            #region Коммунист
            Dictionary<UserFeaturesEnum, int> commie_conditions = new Dictionary<UserFeaturesEnum, int>();
            commie_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.commie, new Trait
                (Traits.commie, random.Next(10, 40), "Коммунист", "Союз нерушимых республик свободных...",
                commie_conditions, "#CC0000", "#FFD700", TraitType.character));
            #endregion
            #region Лидер
            Dictionary<UserFeaturesEnum, int> leader_conditions = new Dictionary<UserFeaturesEnum, int>();
            leader_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.leader, new Trait
                (Traits.leader, random.Next(5, 10), "Лидер", "Именно он основывает братвы, возглавляет вастания и ведет народ за собой",
                leader_conditions, "#9C0000", "#DDDAF2", TraitType.character));
            #endregion
            #region Перфекционист
            Dictionary<UserFeaturesEnum, int> accurateguy_conditions = new Dictionary<UserFeaturesEnum, int>();
            accurateguy_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.accurateguy, new Trait
                (Traits.accurateguy, random.Next(5, 20), "Перфекционист", "Сделать что-то не на идеальном уровне - сущий кошмар для этого пользователя",
                accurateguy_conditions, "#0600BF", "#FFFFFF", TraitType.character));
            #endregion
            #region Ремувер кебаба
            Dictionary<UserFeaturesEnum, int> removekebab_conditions = new Dictionary<UserFeaturesEnum, int>();
            removekebab_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.kebabremover, new Trait
                (Traits.kebabremover, random.Next(5, 10), "Ремувер кебаба", "Remove kebab",
                removekebab_conditions, "#E1AD7B", "#170805", TraitType.character));
            #endregion
            #region Тиран
            Dictionary<UserFeaturesEnum, int> tiran_conditions = new Dictionary<UserFeaturesEnum, int>();
            tiran_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.tiran, new Trait
                (Traits.tiran, random.Next(5, 10), "Тиран", "Беспощаден к недругам",
                tiran_conditions,"#FFFFFF","#000000", TraitType.character));
            #endregion
            #region Верун
            Dictionary<UserFeaturesEnum, int> christ_conditions = new Dictionary<UserFeaturesEnum, int>();
            christ_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.christ, new Trait
                (Traits.christ, random.Next(5, 15), "Верун", "Боженька спаси меня от этих грешников!",
                christ_conditions, "#FFFFFF", "#8A7C00", TraitType.character));
            #endregion
            #region Аметист
            Dictionary<UserFeaturesEnum, int> ametist_conditions = new Dictionary<UserFeaturesEnum, int>();
            ametist_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.atheist, new Trait
                (Traits.atheist, random.Next(5, 30), "Аметист", "Глупые веруны! Это все из-за них Россиюшка в бездну катится.",
                ametist_conditions, "#FFFFFF", "#4C3365", TraitType.character));
            #endregion
            #region Краб
            Dictionary<UserFeaturesEnum, int> krab_conditions = new Dictionary<UserFeaturesEnum, int>();
            krab_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.krab, new Trait
                (Traits.krab, random.Next(5, 10), "Краб", "Как Путин",
                krab_conditions, "#BEF5FF", "#AA4630", TraitType.character));
            #endregion
            #region Аскет
            Dictionary<UserFeaturesEnum, int> asket_conditions = new Dictionary<UserFeaturesEnum, int>();
            asket_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.asket, new Trait
                (Traits.asket, 5, "Аскет", "Зачем нужны все эти группы? Зачем нужны понты? Мне ничего не надо",
                asket_conditions, "#BFBFBF", "#000000", TraitType.character));
            #endregion
            #region Гномик
            Dictionary<UserFeaturesEnum, int> gnomik_conditions = new Dictionary<UserFeaturesEnum, int>();
            gnomik_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.gnomik, new Trait
                (Traits.gnomik, random.Next(5, 20), "Гномик", "*Г(*К(*Н)). Ну вы поняли, это была joek!",
                gnomik_conditions, "#FFFFFF", "#667500", TraitType.character));
            #endregion
            #region Чай
            Dictionary<UserFeaturesEnum, int> tea_conditions = new Dictionary<UserFeaturesEnum, int>();
            tea_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.tea, new Trait
                (Traits.tea, random.Next(5, 10), "Чяй", "Нужно больше чая Богу Чая!",
                tea_conditions, "#863C00", "#FFFFFF", TraitType.character));
            #endregion

            #region Рэпер
            Dictionary<UserFeaturesEnum, int> reper_conditions = new Dictionary<UserFeaturesEnum, int>();
            reper_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.reper, new Trait
                (Traits.reper, random.Next(5, 10), "Рэпер", "Рэпчик наше все! еее бейби!",
                reper_conditions,"#FFFFFF","#000000",TraitType.character));
            #endregion
            #region Фашист
            Dictionary<UserFeaturesEnum, int> fashik_conditions = new Dictionary<UserFeaturesEnum, int>();
            fashik_conditions.Add(UserFeaturesEnum.tolerance, -3);
            AllTraits.Add(Traits.fashik, new Trait
                (Traits.fashik, random.Next(5, 15), "Фашист", "На аватарке Гитлер, в подписи цитаты из майнкампфа, в сообщениях heil.",
                fashik_conditions, "#DD0000", "#000000",TraitType.character));
            #endregion
            #region Нигга
            Dictionary<UserFeaturesEnum, int> nigga_conditions = new Dictionary<UserFeaturesEnum, int>();
            nigga_conditions.Add(UserFeaturesEnum.nothing, 0);
            AllTraits.Add(Traits.nigga, new Trait
                (Traits.nigga, random.Next(5, 10), "Нигга", "Обожает KFC, рэпчег и своих братюнь на Грув-Румине-Хоум. Йоу!",
                nigga_conditions, "#54372F", "#FFFFFF",TraitType.character));
            #endregion
            #region Учитель русского языка
            Dictionary<UserFeaturesEnum, int> teacher_conditions = new Dictionary<UserFeaturesEnum, int>();
            teacher_conditions.Add(UserFeaturesEnum.adeq, 5);
            AllTraits.Add(Traits.teacher, new Trait
                (Traits.teacher, 3, "Учитель русского языка", "Пишет с ошибками",
                teacher_conditions,"#FFFFFF","000000",TraitType.character));
            #endregion

            #endregion

            AddBlock();
        }
        private static void AddBlock()
        {
            Trait[] linux_block = { AllTraits[Traits.windusiatnik] };
            Trait[] windows_block = { AllTraits[Traits.linuxoid] };
            AllTraits[Traits.linuxoid].blocked_traits = linux_block.ToList();
            AllTraits[Traits.windusiatnik].blocked_traits = windows_block.ToList();

            //Невечери-натуралы
            Trait[] nevecher_block = { AllTraits[Traits.natural] };
            Trait[] natur_block = { AllTraits[Traits.animeshnik], AllTraits[Traits.nevecherya], AllTraits[Traits.furry], AllTraits[Traits.bidla] };
            Trait[] anime_block = { AllTraits[Traits.natural] };
            AllTraits[Traits.nevecherya].blocked_traits = (nevecher_block.ToList());
            AllTraits[Traits.natural].blocked_traits = (natur_block.ToList());
            AllTraits[Traits.animeshnik].blocked_traits = (anime_block.ToList());
            AllTraits[Traits.furry].blocked_traits = (anime_block.ToList());
            AllTraits[Traits.bidla].blocked_traits = (anime_block.ToList());

            //Нации
            Trait[] nations_block = { AllTraits[Traits.russian], AllTraits[Traits.belorus], AllTraits[Traits.kazah] };
            AllTraits[Traits.ucraine].blocked_traits = (nations_block.ToList());
            AllTraits[Traits.russian].blocked_traits = (nations_block.ToList());
            AllTraits[Traits.kazah].blocked_traits = (nations_block.ToList());
            AllTraits[Traits.belorus].blocked_traits = (nations_block.ToList());
            //Мажор-пират
            Trait[] major_block = { AllTraits[Traits.major], AllTraits[Traits.pirate] };
            AllTraits[Traits.major].blocked_traits = (major_block.ToList());
            AllTraits[Traits.pirate].blocked_traits = (major_block.ToList());
            //Интеллигент-бидла
            Trait[] intel_block = { AllTraits[Traits.inteligent], AllTraits[Traits.bidla], AllTraits[Traits.madguy] };
            Trait[] bidlamad_block = { AllTraits[Traits.inteligent] };

            AllTraits[Traits.inteligent].blocked_traits = (intel_block.ToList());
            AllTraits[Traits.bidla].blocked_traits = (bidlamad_block.ToList());
            AllTraits[Traits.madguy].blocked_traits = (bidlamad_block.ToList());
            //Сэдбои-властный
            Trait[] sad_block = { AllTraits[Traits.sadboi], AllTraits[Traits.Wpower] };
            AllTraits[Traits.sadboi].blocked_traits = (sad_block.ToList());
            AllTraits[Traits.Wpower].blocked_traits = (sad_block.ToList());
            //Фашист-еврей
            Trait[] jew_block = { AllTraits[Traits.jew], AllTraits[Traits.nazi] };
            AllTraits[Traits.jew].blocked_traits = (jew_block.ToList());
            AllTraits[Traits.nazi].blocked_traits = (jew_block.ToList());
            //Ньюфаг-среднефаг-дед
            Trait[] oldness_block = { AllTraits[Traits.newfag], AllTraits[Traits.srednefag], AllTraits[Traits.ded] };
            AllTraits[Traits.newfag].blocked_traits = (oldness_block.ToList());
            AllTraits[Traits.srednefag].blocked_traits = (oldness_block.ToList());
            AllTraits[Traits.ded].blocked_traits = (oldness_block.ToList());
            //Консерва-революционер
            Trait[] conserv_block = { AllTraits[Traits.konserva], AllTraits[Traits.revolutioner] };
            AllTraits[Traits.revolutioner].blocked_traits = (conserv_block.ToList());
            AllTraits[Traits.konserva].blocked_traits = (conserv_block.ToList());
            //Добряк-фашист
            Trait[] kind_block = { AllTraits[Traits.kindguy], AllTraits[Traits.fashik], AllTraits[Traits.nazi] };
            Trait[] nazi_block = { AllTraits[Traits.kindguy], AllTraits[Traits.commie] };
            AllTraits[Traits.kindguy].blocked_traits = (kind_block.ToList());
            AllTraits[Traits.fashik].blocked_traits = (nazi_block.ToList());
            AllTraits[Traits.nazi].blocked_traits = (nazi_block.ToList());
            //Атеист - верун
            Trait[] god_block = { AllTraits[Traits.atheist], AllTraits[Traits.christ] };
            AllTraits[Traits.atheist].blocked_traits = (god_block.ToList());
            AllTraits[Traits.christ].blocked_traits = (god_block.ToList());
        }


        public static List<Trait> ReturnTraits(User user)
        {
            List<Trait> traits_user = new List<Trait>();
            for (int i = 0; i < AllTraits.Count; i++)
            {
                Trait trait = AllTraits.ElementAt(i).Value;
                if (AdvRnd.PersentChanseBool((short)trait.chanse) && !ConditionCheck(user, trait, traits_user))
                {
                    traits_user.Add(trait);
                }
            }
            return traits_user;
        }
        public static bool ConditionCheck(User user, Trait trait, List<Trait> traits_user)
        {
            bool check_failed = false;
            foreach (Trait trait_check in traits_user)
            {
                if (trait_check.blocked_traits.Contains(trait))
                    return true;
            }
            check_failed = CheckUserParams(user, trait, check_failed);
            return check_failed;
        }

        private static bool CheckUserParams(User user, Trait trait, bool check_failed)
        {
            for (int i = 0; i < trait.conditions.Count; i++)
            {
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

                            if (user.m_oldness < min_oldness || user.m_oldness > max_oldness)
                                check_failed = true;
                            break;
                    }
                }
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
                    }
                }

            }

            return check_failed;
        }

        public static Trait SearchTrait(int id)
        {
            Trait trait = AllTraits.ElementAt(0).Value;
            for (int i = 0; i < AllTraits.Count; i++)
            {
                if (AllTraits.ElementAt(i).Value.id_num == id)
                    trait = AllTraits.ElementAt(i).Value;
            }
            return trait;
        }
    }
}
