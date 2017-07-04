using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Interface_User : InterfaceView
    {
        public Interface_String interface_basic { get; private set; }
        public List<Interface_String> basic_properties = new List<Interface_String>();
        public List<Interface_String> numeric_properties = new List<Interface_String>();
        public List<Interface_String> character_properties = new List<Interface_String>();
        public List<Interface_String> skills_properties = new List<Interface_String>();
        public List<Interface_String> traits = new List<Interface_String>();

        public Interface_User(User user) : base()
        {
            //Базовое представление в виде строчки
            interface_basic = new Interface_String(user.nick, "", true);
            interface_basic.AddColor("", user.group.ColorHTML);
            try
            {
                interface_basic.AddImagePathToolTip(Nicks.AvaPath[user.nick].ToString(), "Это пользователь " + user.nick);
            }
            catch
            {
                interface_basic.AddImagePathToolTip("No_ava.png", "Это пользователь " + user.nick);
            }
            //Даты
            basic_properties.Add(new Interface_String("Группа: ", user.group.Name, true, false));
            basic_properties.Last().AddColor("", "",user.group.ColorHTML);
            basic_properties.Last().SetSize(18, 14);
            basic_properties.Last().AddImagePathToolTip("", $"Права журналиста {user.group.Journ}" +
                $" \nПрава модератора {user.mod}" +
                $" \nПрава модератора бездны {user.group.Mod_bezdn}" +
                $" \nРедкость {user.group.Respect}" +
                $" \nУважение {user.group.Respect}");
            basic_properties.Add(new Interface_String("Даты", "", false, true));
            basic_properties.Last().SetSize(18, 14);
            basic_properties.Add(new Interface_String("Дата регистрации: ", user.registration.ToLongDateString(), true));
            basic_properties.Add(new Interface_String("Последнее посещение: ", user.Last_activity.ToLongDateString(), true));
            if(user.main_fraction != null)
            {
                basic_properties.Add(new Interface_String("Фракции", "", false, true));
                basic_properties.Add(new Interface_String("Главная: ", user.main_fraction.name, true));
                foreach (Fraction fraction in user.other_fractions)
                {
                    basic_properties.Add(new Interface_String("Дополнительная: ", fraction.name, true));
                }
            }
            basic_properties.Add(new Interface_String("Дополнительно", "", false, true));
            basic_properties.Last().SetSize(18, 14);
            basic_properties.Add(new Interface_String("Предупреждения: ", user.LastBan.Warn_sum.ToString(), true));
            basic_properties.Add(new Interface_String("Шанс на модератора: ", user.moder_chanse.ToString(), true));
            basic_properties.Add(new Interface_String("Форумное влияние: ", user.forum_influence.ToString(), true));

            //Трейты
            traits.Add(new Interface_String("Черты характера", $"({user.traits.Count})", false, true));
            traits.Add(new Interface_String("Фактические", "", false, true));
            traits.Last().SetSize(18, 14);
            foreach (Trait trait in user.traits)
            {
                if (trait.type != TraitType.character)
                    traits.Add(trait.InterfaceInfo.string_info);
            }
            traits.Add(new Interface_String("Персональные", "", false, true));
            traits.Last().SetSize(18, 14);
            foreach (Trait trait in user.traits)
            {
                if (trait.type == TraitType.character)
                    traits.Add(trait.InterfaceInfo.string_info);
            }

            //Числовые представления
            numeric_properties.Add(new Interface_String("Статистика сайта", "", false, true));
            numeric_properties.Last().SetSize(18, 14);
            numeric_properties.Add(new Interface_String("Комментарии: ", user.comments.ToString(), true));
            numeric_properties.Add(new Interface_String("Рейтинг К.: ", user.comments_rate.ToString(), true));
            numeric_properties.Add(new Interface_String("Новости: ", user.news.ToString(), true));
            numeric_properties.Add(new Interface_String("Качество новостей: ", user.news_quality.ToString(), true));
            numeric_properties.Add(new Interface_String("Форум", "", false, true));
            numeric_properties.Last().SetSize(18, 14);
            numeric_properties.Add(new Interface_String("Сообщения: ", user.messages.ToString(), true));
            numeric_properties.Add(new Interface_String("Симпатии.: ", user.likes.ToString(), true));
            numeric_properties.Add(new Interface_String("Карма", user.karma.karma.ToString(), false,true));
            numeric_properties.Last().SetSize(18, 14);
            numeric_properties.Add(new Interface_String("Активность: ", user.karma.kar_activity.ToString(), true));
            numeric_properties.Add(new Interface_String("Новости: ", user.karma.kar_news.ToString(), true));
            numeric_properties.Add(new Interface_String("Репутация: ", user.karma.kar_reputation.ToString(), true));

            //Прогресс-бары характера
            character_properties.Add(new Interface_ProgressBar("Благоразумие", user.character.adeq.Param_value,0, 10));
            character_properties.Last().AddColor("", "#FFE6E6E6", "#FF4B85FF");
            character_properties.Add(new Interface_ProgressBar("Раковитость", user.character.rakness.Param_value, 0, 10));
            character_properties.Last().AddColor("", "#FFE6E6E6", "#FFFF1919");
            character_properties.Add(new Interface_ProgressBar("Консервативность", user.character.conservative.Param_value, 0, 10));
            character_properties.Last().AddColor("", "#FFE6E6E6", "#FFB98F1D");
            character_properties.Add(new Interface_ProgressBar("Толерантность", user.character.tolerance.Param_value, 0, 10));
            character_properties.Last().AddColor("", "#FFE6E6E6", "#FF27DC15");
            //Прогресс-бары умений
            skills_properties.Add(new Interface_ProgressBar("Креативность", user.character.creativity.Param_value, 0, 10));
            skills_properties.Last().AddColor("", "#FFE6E6E6", "#FF723EFF");
            skills_properties.Add(new Interface_ProgressBar("Наука", user.character.sciense.Param_value, 0, 10));
            skills_properties.Last().AddColor("", "#FFE6E6E6", "#FF3232F9");
            skills_properties.Add(new Interface_ProgressBar("Гуманитаризм", user.character.humanist.Param_value, 0, 10));
            skills_properties.Last().AddColor("", "#FFE6E6E6", "#FFF9AA38");
            try
            {
                character_properties.Add(new Interface_ProgressBar("История", user.character.historic.Param_value, 0, 10));
                character_properties.Last().AddColor("", "#FFE6E6E6", "#FFFFE64B");
            }
            catch
            {

            }

        }
    }
}
