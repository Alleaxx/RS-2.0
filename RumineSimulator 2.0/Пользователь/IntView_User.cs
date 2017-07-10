using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class IntView_User : IntView
    {
        public List<GuiString> basic_props = new List<GuiString>();
        public List<GuiString> numeric_props = new List<GuiString>();
        public List<GuiString> character_props = new List<GuiString>();
        public List<GuiString> skills_props = new List<GuiString>();
        public List<GuiString> traits = new List<GuiString>();

        public IntView_User(User user) : base()
        {
            //Базовое представление в виде строчки
            classic_string = new GuiString(user.nick, "", true);
            classic_string.SetGUIName(GUITypes.user, user.user_id);
            classic_string.SetSize(16, 12);
            classic_string.SetColor("", user.group.ColorHTML);
            try
            {
                classic_string.SetImage(Nicks.AvaPath[user.nick].ToString(),18,18);
            }
            catch
            {
                classic_string.SetImage("No_ava.png",18,18);
            }
            //Даты
            Add_UserBasicProps(classic_string);
            Add_UserBasicProps(new GuiString("Группа: ", user.group.Name, true));
            basic_props.Last().SetColor("", "", user.group.ColorHTML);
            basic_props.Last().SetSize(18, 14);
            basic_props.Last().SetGUIName(GUITypes.group, user.group.id);
            basic_props.Last().SetToolTip($"Права журналиста {user.group.Journ}" +
                $" \nПрава модератора {user.mod}" +
                $" \nПрава модератора бездны {user.group.Mod_bezdn}" +
                $" \nРедкость {user.group.Respect}" +
                $" \nУважение {user.group.Respect}");
            Add_UserBasicProps(new GuiString("Даты", "", false, StringProfile.Header));
            basic_props.Last().SetSize(18, 14);
            Add_UserBasicProps(new GuiString("Дата регистрации: ", user.registration.ToLongDateString()));
            Add_UserBasicProps(new GuiString("Последнее посещение: ", user.Last_activity.ToLongDateString()));
            if (user.main_fraction != null)
            {
                Add_UserBasicProps(new GuiString("Фракции", "", false, StringProfile.Header));
                Add_UserBasicProps(new GuiString("Главная: ", user.main_fraction.name, true));
                basic_props.Last().SetGUIName(GUITypes.fraction, user.main_fraction.id);
                foreach (Fraction fraction in user.other_fractions)
                {
                    Add_UserBasicProps(new GuiString("Дополнительная: ", fraction.name, true));
                    basic_props.Last().SetGUIName(GUITypes.fraction, fraction.id);
                }
            }
            Add_UserBasicProps(new GuiString("Дополнительно", "", false, StringProfile.Header));
            basic_props.Last().SetSize(18, 14);
            Add_UserBasicProps(new GuiString("Предупреждения: ", user.LastBan.Warn_sum.ToString(), true));
            basic_props.Last().SetGUIName(GUITypes.ban, user.LastBan.id);
            Add_UserBasicProps(new GuiString("Шанс на модератора: ", user.moder_chanse.ToString()));
            Add_UserBasicProps(new GuiString("Форумное влияние: ", user.forum_influence.ToString()));
            Add_UserBasicProps(new GuiString("ID пользователя: ", user.user_id.ToString()));

            //Трейты
            Add_UserTraitsProps(new GuiString("Черты характера", $"({user.traits.Count})", false, StringProfile.Header));
            Add_UserTraitsProps(new GuiString("Фактические", "", false, StringProfile.Header));
            traits.Last().SetSize(18, 14);
            foreach (Trait trait in user.traits)
            {
                if (trait.global_type == TraitGlobalType.fact)
                {
                    Add_UserTraitsProps(trait.InterfaceInfo.classic_string);
                    traits.Last().SetGUIName(GUITypes.trait, trait.id_num);
                }


            }
            Add_UserTraitsProps(new GuiString("Персональные", "", false, StringProfile.Header));
            traits.Last().SetSize(18, 14);
            foreach (Trait trait in user.traits)
            {
                if (trait.global_type == TraitGlobalType.personal)
                {
                    Add_UserTraitsProps(trait.InterfaceInfo.classic_string);
                    traits.Last().SetGUIName(GUITypes.trait, trait.id_num);
                }

            }

            //Числовые представления
            Add_UserNumericProps(new GuiString("Статистика сайта", "", false, StringProfile.Header));
            numeric_props.Last().SetSize(18, 14);
            Add_UserNumericProps(new GuiString("Комментарии: ", user.comments.ToString()));
            Add_UserNumericProps(new GuiString("Рейтинг К.: ", user.comments_rate.ToString()));
            Add_UserNumericProps(new GuiString("Новости: ", user.news.ToString()));
            Add_UserNumericProps(new GuiString("Качество новостей: ", user.news_quality.ToString()));
            Add_UserNumericProps(new GuiString("Форум", "", false, StringProfile.Header));
            numeric_props.Last().SetSize(18, 14);
            Add_UserNumericProps(new GuiString("Сообщения: ", user.messages.ToString()));
            Add_UserNumericProps(new GuiString("Симпатии.: ", user.likes.ToString()));
            Add_UserNumericProps(new GuiString("Карма", user.karma.karma.ToString(), false, StringProfile.Header));
            numeric_props.Last().SetSize(18, 18);
            Add_UserNumericProps(new GuiString("Активность: ", user.karma.kar_activity.ToString()));
            Add_UserNumericProps(new GuiString("Новости: ", user.karma.kar_news.ToString()));
            Add_UserNumericProps(new GuiString("Репутация: ", user.karma.kar_reputation.ToString()));

            //Прогресс-бары характера
            Add_UserCharacterProps(new GuiProgressBar("Благоразумие", user.character.adeq.Value, 0, 10));
            character_props.Last().SetColor("", "", "#FF4B85FF");
            character_props.Last().SetToolTip($"{user.character.adeq.Value}");
            Add_UserCharacterProps(new GuiProgressBar("Раковитость", user.character.rakness.Value, 0, 10));
            character_props.Last().SetColor("", "", "#FFFF1919");
            character_props.Last().SetToolTip($"{user.character.rakness.Value}");
            Add_UserCharacterProps(new GuiProgressBar("Консервативность", user.character.conservative.Value, 0, 10));
            character_props.Last().SetColor("", "", "#FFB98F1D");
            character_props.Last().SetToolTip($"{user.character.conservative.Value}");
            Add_UserCharacterProps(new GuiProgressBar("Толерантность", user.character.tolerance.Value, 0, 10));
            character_props.Last().SetColor("", "", "#FF27DC15");
            character_props.Last().SetToolTip($"{user.character.tolerance.Value}");
            Add_UserCharacterProps(new GuiProgressBar("Шанс уйти", user.character.leaveChanse.Value, 0, 10));
            character_props.Last().SetColor("", "", "#FFF7BD8C");
            character_props.Last().SetToolTip($"{user.character.leaveChanse.Value}");
            //Прогресс-бары умений
            Add_UserSkillsProps(new GuiProgressBar("Креативность", user.character.creativity.Value, 0, 10));
            skills_props.Last().SetColor("", "", "#FF723EFF");
            skills_props.Last().SetToolTip($"{user.character.creativity.Value}");
            Add_UserSkillsProps(new GuiProgressBar("Наука", user.character.sciense.Value, 0, 10));
            skills_props.Last().SetColor("", "", "#FF3232F9");
            skills_props.Last().SetToolTip($"{user.character.sciense.Value}");
            Add_UserSkillsProps(new GuiProgressBar("Гуманитаризм", user.character.humanist.Value, 0, 10));
            skills_props.Last().SetColor("", "", "#FFF9AA38");
            skills_props.Last().SetToolTip($"{user.character.humanist.Value}");
            skills_props.Add(new GuiProgressBar("История", user.character.historic.Value, 0, 10));
            skills_props.Last().SetColor("","","#FFFFE64B");
            skills_props.Last().SetToolTip($"{user.character.historic.Value}");


        }

        public void Add_UserBasicProps(GuiString info)
        {
            Add_Property(basic_props, info);
        }
        public void Add_UserNumericProps(GuiString info)
        {
            Add_Property(numeric_props, info);
        }
        public void Add_UserCharacterProps(GuiString info)
        {
            Add_Property(character_props, info);
        }
        public void Add_UserSkillsProps(GuiString info)
        {
            Add_Property(skills_props, info);
        }
        public void Add_UserTraitsProps(GuiString info)
        {
            Add_Property(traits, info);
        }
    }
}
