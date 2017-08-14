using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RumineSimulator_2._0
{
    static class Nicks
    {
        #region Инициализация ников и их добавление
        static List<string> nicks = new List<string>() {
        "Allexx",
        "naswai",
        "NIGHTDANGER",
        "frokys",
        "Alex G.",
        "3JIou_Kpunep",
        "InFeRnAl_KiD",
        "frendly herobrin rus",
        "dedepete",
        "Пумба :D",
        "CrashBoy01",
        "NeZoX",
        "Wheatley",
        "SuperM",
        "BeZZe",
        "overstalker",
        "Gevorg2012",
        "MegaZerg",
        "CheessteR",
        "Капут-противогаз",
        "senyaiv",
        "Andrej2001",
        "IlyaSidorin",
        "WhiteWolfCraft",
        "(Slime)",
        "dapimex",
        "HerrManelling",
        "Lektorrr",
        "ArtemkaFominLive",
        "CrazyBanana",
        "Anthony Kiedis",
        "Dj_fantom",
        "TheProFinch",
        "raxar",
        "sk909",
        "DjSteve",
        "SirPomidor",
        "Машок",
        "Winlocker",
        "GeXOn",
        "ArtPlayMan",
        "Injustice",
        "BlueMoshka",
        "anatolgol",
        "MesHo4eK",
        "GamerXP",
        "DezmutNour",
        "Stairdeck",
        "vasilek-vitalik",
        "tomkoro",
        "Direct",
        "egor01",
        "minemineminecraft",
        "lolnoob",
        "Me",

        "DXYVAD",
        "SttttttTePaN",
        "MikFreeD",
        "Azik ^3^",
        "NEOevil2",
        "gromda",
        "G3Forse",
        "NestorRoyce",
        "buckij",
        "vision",
        "fenos",
        "KROLant",
        "MrSennator",
        "ЯndomTrash",
        "Arthas_Men",
        "FrayForce",
        "LORDfito",
        "FireStoneeeCraft",
        "Dimalav567",
        "saxalin",
        "Kain",
        "tonchik",
        "The_Faos",
        "Shadowind",
        "Almighty",
        "Лорхан",
        "Laps",
        "Alokey",

        };
        #endregion

        static public Dictionary<string, ImageSource> AvaPath = new Dictionary<string, ImageSource>();

        static Random random = new Random();

        //Добавление аватарок пользователям
        public static void NicksInit()
        {
            nicks = new List<string>() {
        "Allexx",
        "naswai",
        "NIGHTDANGER",
        "frokys",
        "Alex G.",
        "3JIou_Kpunep",
        "InFeRnAl_KiD",
        "frendly herobrin rus",
        "dedepete",
        "Пумба :D",
        "CrashBoy01",
        "NeZoX",
        "Wheatley",
        "SuperM",
        "BeZZe",
        "overstalker",
        "Gevorg2012",
        "MegaZerg",
        "CheessteR",
        "Капут-противогаз",
        "senyaiv",
        "Andrej2001",
        "IlyaSidorin",
        "WhiteWolfCraft",
        "(Slime)",
        "dapimex",
        "HerrManelling",
        "Lektorrr",
        "ArtemkaFominLive",
        "CrazyBanana",
        "Anthony Kiedis",
        "Dj_fantom",
        "TheProFinch",
        "raxar",
        "sk909",
        "DjSteve",
        "SirPomidor",
        "Машок",
        "Winlocker",
        "GeXOn",
        "ArtPlayMan",
        "Injustice",
        "BlueMoshka",
        "anatolgol",
        "MesHo4eK",
        "GamerXP",
        "DezmutNour",
        "Stairdeck",
        "vasilek-vitalik",
        "tomkoro",
        "Direct",
        "egor01",
        "minemineminecraft",
        "lolnoob",
        "Me",

        "DXYVAD",
        "SttttttTePaN",
        "MikFreeD",
        "Azik ^3^",
        "NEOevil2",
        "gromda",
        "G3Forse",
        "NestorRoyce",
        "buckij",
        "vision",
        "fenos",
        "KROLant",
        "MrSennator",
        "ЯndomTrash",
        "Arthas_Men",
        "FrayForce",
        "LORDfito",
        "FireStoneeeCraft",
        "Dimalav567",
        "saxalin",
        "Kain",
        "tonchik",
        "The_Faos",
        "Shadowind",
        "Almighty",
        "Лорхан",
        "Laps",
        "Alokey",
        "SDIR01",
        };

        }
        public static void AvasInit()
        {
            AvaPath.Add("SDIR01", new BitmapImage(new Uri("pack://application:,,,/Resources/sdir.png")));
            AvaPath.Add("Almighty", new BitmapImage(new Uri("pack://application:,,,/Resources/Almighty.png")));
            AvaPath.Add("DXYVAD", new BitmapImage(new Uri("pack://application:,,,/Resources/DXYVAD.png")));
            AvaPath.Add("SttttttTePaN", new BitmapImage(new Uri("pack://application:,,,/Resources/Sttepan.jpg")));
            AvaPath.Add("MikFreeD", new BitmapImage(new Uri("pack://application:,,,/Resources/MikFreed.png")));
            AvaPath.Add("Azik ^3^", new BitmapImage(new Uri("pack://application:,,,/Resources/Azik.jpg")));
            AvaPath.Add("NEOevil2", new BitmapImage(new Uri("pack://application:,,,/Resources/neo.jpg")));
            AvaPath.Add("gromda", new BitmapImage(new Uri("pack://application:,,,/Resources/gromda.jpg")));
            AvaPath.Add("G3Forse", new BitmapImage(new Uri("pack://application:,,,/Resources/g3forse.jpg")));
            AvaPath.Add("NestorRoyce", new BitmapImage(new Uri("pack://application:,,,/Resources/nestor.jpg")));
            AvaPath.Add("buckij", new BitmapImage(new Uri("pack://application:,,,/Resources/buckj.gif")));
            AvaPath.Add("vision", new BitmapImage(new Uri("pack://application:,,,/Resources/vision.jpg")));
            AvaPath.Add("fenos", new BitmapImage(new Uri("pack://application:,,,/Resources/fenos.jpg")));
            AvaPath.Add("KROLant", new BitmapImage(new Uri("pack://application:,,,/Resources/KROLant.png")));
            AvaPath.Add("MrSennator", new BitmapImage(new Uri("pack://application:,,,/Resources/MrSennator.png")));
            AvaPath.Add("ЯndomTrash", new BitmapImage(new Uri("pack://application:,,,/Resources/RandomTrash.png")));
            AvaPath.Add("Arthas_Men", new BitmapImage(new Uri("pack://application:,,,/Resources/Arthas.jpg")));
            AvaPath.Add("FrayForce", new BitmapImage(new Uri("pack://application:,,,/Resources/fray.gif")));
            AvaPath.Add("LORDfito", new BitmapImage(new Uri("pack://application:,,,/Resources/LORDfito.png")));
            AvaPath.Add("FireStoneeeCraft", new BitmapImage(new Uri("pack://application:,,,/Resources/firestoneecraft.png")));
            AvaPath.Add("Dimalav567", new BitmapImage(new Uri("pack://application:,,,/Resources/dimalav567.gif")));
            AvaPath.Add("saxalin", new BitmapImage(new Uri("pack://application:,,,/Resources/saxalin.jpg")));
            AvaPath.Add("Kain", new BitmapImage(new Uri("pack://application:,,,/Resources/kain.jpg")));
            AvaPath.Add("tonchik", new BitmapImage(new Uri("pack://application:,,,/Resources/tonckik.jpg")));
            AvaPath.Add("The_Faos", new BitmapImage(new Uri("pack://application:,,,/Resources/faos.png")));
            AvaPath.Add("Shadowind", new BitmapImage(new Uri("pack://application:,,,/Resources/Shadowind.gif")));
            AvaPath.Add("Лорхан", new BitmapImage(new Uri("pack://application:,,,/Resources/lorhan.gif")));
            AvaPath.Add("Alokey", new BitmapImage(new Uri("pack://application:,,,/Resources/alokey.png")));
            AvaPath.Add("Anthony Kiedis", new BitmapImage(new Uri("pack://application:,,,/Resources/anthony.png")));
            AvaPath.Add("dapimex", new BitmapImage(new Uri("pack://application:,,,/Resources/dapimex.jpg")));
            AvaPath.Add("anatolgol", new BitmapImage(new Uri("pack://application:,,,/Resources/anatol.jpg")));
            AvaPath.Add("MesHo4eK", new BitmapImage(new Uri("pack://application:,,,/Resources/meshok.jpg")));
            AvaPath.Add("GamerXP", new BitmapImage(new Uri("pack://application:,,,/Resources/gamerxp.png")));
            AvaPath.Add("DezmutNour", new BitmapImage(new Uri("pack://application:,,,/Resources/dezmutnour.png")));
            AvaPath.Add("Stairdeck", new BitmapImage(new Uri("pack://application:,,,/Resources/stair.gif")));
            AvaPath.Add("vasilek-vitalik", new BitmapImage(new Uri("pack://application:,,,/Resources/vasvit.jpg")));
            AvaPath.Add("tomkoro", new BitmapImage(new Uri("pack://application:,,,/Resources/tomcoro.jpg")));
            AvaPath.Add("Direct", new BitmapImage(new Uri("pack://application:,,,/Resources/direct.jpg")));
            AvaPath.Add("egor01", new BitmapImage(new Uri("pack://application:,,,/Resources/egor01.png")));
            AvaPath.Add("minemineminecraft", new BitmapImage(new Uri("pack://application:,,,/Resources/minemine.png")));
            AvaPath.Add("lolnoob", new BitmapImage(new Uri("pack://application:,,,/Resources/lolnoob.jpg")));
            AvaPath.Add("Me", new BitmapImage(new Uri("pack://application:,,,/Resources/heassant.jpg")));
            AvaPath.Add("Dj_fantom", new BitmapImage(new Uri("pack://application:,,,/Resources/dj_fantom.jpg")));
            AvaPath.Add("TheProFinch", new BitmapImage(new Uri("pack://application:,,,/Resources/profinch.png")));
            AvaPath.Add("raxar", new BitmapImage(new Uri("pack://application:,,,/Resources/raxar.png")));
            AvaPath.Add("sk909", new BitmapImage(new Uri("pack://application:,,,/Resources/sk909.jpg")));
            AvaPath.Add("DjSteve", new BitmapImage(new Uri("pack://application:,,,/Resources/djsteve.jpg")));
            AvaPath.Add("SirPomidor", new BitmapImage(new Uri("pack://application:,,,/Resources/sirpomidor.jpg")));
            AvaPath.Add("Машок", new BitmapImage(new Uri("pack://application:,,,/Resources/mashok.png")));
            AvaPath.Add("Winlocker", new BitmapImage(new Uri("pack://application:,,,/Resources/winlocker.png")));
            AvaPath.Add("GeXOn", new BitmapImage(new Uri("pack://application:,,,/Resources/gexon.png")));
            AvaPath.Add("ArtPlayMan", new BitmapImage(new Uri("pack://application:,,,/Resources/artplayman.jpg")));
            AvaPath.Add("Injustice", new BitmapImage(new Uri("pack://application:,,,/Resources/injust.png")));
            AvaPath.Add("BlueMoshka", new BitmapImage(new Uri("pack://application:,,,/Resources/bluemosh.png")));
            AvaPath.Add("(Slime)", new BitmapImage(new Uri("pack://application:,,,/Resources/slime.gif")));
            AvaPath.Add("HerrManelling", new BitmapImage(new Uri("pack://application:,,,/Resources/herrmaneling.jpg")));
            AvaPath.Add("Lektorrr", new BitmapImage(new Uri("pack://application:,,,/Resources/lectorr.gif")));
            AvaPath.Add("ArtemkaFominLive", new BitmapImage(new Uri("pack://application:,,,/Resources/artemkafomin.jpg")));
            AvaPath.Add("CrazyBanana", new BitmapImage(new Uri("pack://application:,,,/Resources/crasybanana.png")));
            AvaPath.Add("WhiteWolfCraft", new BitmapImage(new Uri("pack://application:,,,/Resources/wwc.jpg")));
            AvaPath.Add("CheessteR", new BitmapImage(new Uri("pack://application:,,,/Resources/chester.jpg")));
            AvaPath.Add("IlyaSidorin", new BitmapImage(new Uri("pack://application:,,,/Resources/sidorin.png")));
            AvaPath.Add("Andrej2001", new BitmapImage(new Uri("pack://application:,,,/Resources/andrej.png")));
            AvaPath.Add("senyaiv", new BitmapImage(new Uri("pack://application:,,,/Resources/senyaiv.jpg")));
            AvaPath.Add("NeZoX", new BitmapImage(new Uri("pack://application:,,,/Resources/nezox.png")));
            AvaPath.Add("BeZZe", new BitmapImage(new Uri("pack://application:,,,/Resources/bezze.jpg")));
            AvaPath.Add("MegaZerg", new BitmapImage(new Uri("pack://application:,,,/Resources/megazerg.png")));
            AvaPath.Add("frokys", new BitmapImage(new Uri("pack://application:,,,/Resources/frokys.jpg")));
            AvaPath.Add("Gevorg2012", new BitmapImage(new Uri("pack://application:,,,/Resources/gevorg.jpg")));
            AvaPath.Add("Капут-противогаз", new BitmapImage(new Uri("pack://application:,,,/Resources/kaput.jpg")));
            AvaPath.Add("Alex G.", new BitmapImage(new Uri("pack://application:,,,/Resources/alex.g.png")));
            AvaPath.Add("SuperM", new BitmapImage(new Uri("pack://application:,,,/Resources/superm.png")));
            AvaPath.Add("Wheatley", new BitmapImage(new Uri("pack://application:,,,/Resources/wheatley.png")));
            AvaPath.Add("3JIou_Kpunep", new BitmapImage(new Uri("pack://application:,,,/Resources/zloi kpanep.png")));
            AvaPath.Add("frendly herobrin rus", new BitmapImage(new Uri("pack://application:,,,/Resources/frendly.png")));
            AvaPath.Add("InFeRnAl_KiD", new BitmapImage(new Uri("pack://application:,,,/Resources/infernal_kid.gif")));
            AvaPath.Add("NIGHTDANGER", new BitmapImage(new Uri("pack://application:,,,/Resources/night.jpg")));
            AvaPath.Add("overstalker", new BitmapImage(new Uri("pack://application:,,,/Resources/over.jpg")));
            AvaPath.Add("Пумба :D", new BitmapImage(new Uri("pack://application:,,,/Resources/pumba.gif")));
            AvaPath.Add("dedepete", new BitmapImage(new Uri("pack://application:,,,/Resources/dedepete.png")));
            AvaPath.Add("CrashBoy01", new BitmapImage(new Uri("pack://application:,,,/Resources/crash.gif")));
            AvaPath.Add("Allexx", new BitmapImage(new Uri("pack://application:,,,/Resources/allexx.png")));
            AvaPath.Add("naswai", new BitmapImage(new Uri("pack://application:,,,/Resources/naswai.gif")));

        }

        //Выбор свободного ника
        public static string SelectFreeNick()
        {
            //Рандомный выбор ника из хранилища, удаление его из списка, отправление значения обратно
            if (nicks.Count != 0)
            {
                int id = random.Next(nicks.Count);
                string nick = nicks[id];
                nicks.RemoveAt(id);
                return nick;
            }
            else
            {
                return UsersControl.act_users.Count.ToString();
            }

        }

    }
}
