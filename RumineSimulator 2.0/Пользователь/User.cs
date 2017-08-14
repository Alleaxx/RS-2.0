using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class User : IAdvertisable
    {
        #region Свойства пользователя
        public int user_id { get; private set; }
        public string nick { get; set; }

        public DateTime registration { get; private set; }
        public DateTime last_activity { get; set; }
        public bool unknown;
        public bool activity { get; set; }

        public int moder_chanse { get; set; }

        public int month_oldness { get; private set; }

        public int news { get; set; }
        public int news_quality { get; private set; }
        public int comments { get; set; }
        public int comments_rate { get; set; }

        public Group group { get; set; }
        public Character character { get; private set; }
        public RelationControl relations { get; private set; }
        public Reputation reputation { get; }
        public Karma karma { get; }
        public Fraction main_fraction { get; set; }
        public List<Fraction> other_fractions = new List<Fraction>();
        public List<Trait> traits = new List<Trait>();
        public List<Ban> bans = new List<Ban>();
        public Ban LastBan
        {
            get
            {
                return bans[bans.Count - 1];
            }
            set
            {
                bans[bans.Count - 1] = value;
            }
        }
        public int forum_influence;

        //Мнение пользователя
        public OpinionControl Opinion { get; private set; }


        public int messages { get; set; }
        public int likes { get; set; }

        public bool mod { get; set; }
        public bool admin { get; private set; }


        public string description { get; private set; }
        public List<string> oldAdvertisments = new List<string>();

        public UserDayLog daylog { get; private set; }
        public List<UserDayLog> last_thirty_Days = new List<UserDayLog>();
        public Dictionary<User, int> blocked_users_rep = new Dictionary<User, int>();
        //GUI-представление
        public IntView_User InterfaceInfo
        {
            get
            {
                return new IntView_User(this);
            }
        }

        Random random;

        #endregion

        public User(bool Admin = false)
        {
            random = new Random();
            //Ник получаем из списка свободных ников
            user_id = UsersControl.all_users.Count + 1;

            admin = Admin;
            if (!Admin)
                nick = Nicks.SelectFreeNick();
            else
            {
                if(UsersControl.UserSearch("MadMoss") == null)
                {
                    nick = "MadMoss";
                }
                else
                {
                    nick = "south_park";
                }
            }


            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%), на ее основе комменты и новости
            activity = true;
            SetRegistration();

            SetNewsComments();
            SetMessages();
            reputation = new Reputation(this);
            karma = new Karma(this);

            //Генерируем характер
            character = new Character(this);
            traits = TraitsList.ReturnTraits(this);

            //На основе характера получаем стереотип и доступные группы
            if (!admin)
                group = GroupsControl.ReturnUserRandomGroup();
            else
                group = GroupsControl.GroupSearch(GroupsType.Admin);
            SetLikesRatings();

            //Инициализируем отношения                                             
            relations = new RelationControl(this);
            CharacterMod();
            TraitMod();
            bans.Add(new Ban(this));
            //Добавляем мнения
            Opinion = new OpinionControl(this);
        }


        //Установка даты регистрации
        private void SetRegistration()
        {
            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%)
            if (AdvRnd.PrsChanse(40))
            {
                registration = new DateTime(random.Next(Date.found_date.Year, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));

            }
            else if (AdvRnd.PrsChanse(50))
            {
                registration = new DateTime(random.Next(Date.found_date.Year + 1, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));

            }
            else
            {
                registration = new DateTime(random.Next(Date.found_date.Year + 2, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));
            }
            if (registration.Month <= Date.found_date.Month && registration.Year == Date.found_date.Year)
                registration = new DateTime(registration.Year, random.Next(Date.found_date.Month + 1, 13), registration.Day);
            if (registration.Month >= Date.current_date.Month && registration.Year == Date.current_date.Year)
                registration = new DateTime(registration.Year, random.Next(1, Date.current_date.Month), registration.Day);

            unknown = false;
            //Неизвестный для форума пользователь
            if (AdvRnd.PrsChanse(5) && !admin)
            {
                unknown = true;
            }
            activity = true;
            //Неактивный пользователь
            if (AdvRnd.PrsChanse(5) && !admin)
            {
                activity = false;
            }

            if (admin)
            {
                registration = Date.found_date;
            }

            SetOldness();

        }


        //Влияние характера на параметры
        private void CharacterMod()
        {
            news_quality = random.Next(0, 10) + character.creativity.Value * 5 + character.sciense.Value * 5;
            if (news == 0)
            {
                news_quality = 0;
            }
        }

        //Влияние трейтов на все параметры
        private void TraitMod()
        {
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                news_quality += 10;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.newslover)))
            {
                news *= random.Next(1, 3) + 3;
                comments = comments * random.Next(1, 3) + 5;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.leader)))
            {
                forum_influence *= 2;
            }
        }

        //Определяет олдфажность пользователя
        private void SetOldness()
        {
            if (Date.current_date.Year - registration.Year > 1)
            {
                month_oldness = (13 - registration.Month + ((Date.current_date.Year - registration.Year - 1) * 12) + Date.current_date.Month);
            }
            else if (Date.current_date.Year - registration.Year == 1)
            {
                month_oldness = (13 - registration.Month + Date.current_date.Month);
            }
            else if (Date.current_date.Year - registration.Year == 0)
            {
                month_oldness = Date.current_date.Month - registration.Month + 1;
            }

        }

        //Установка кол-ва сообщений, комментариев и новостей в зависимости от реги, а также шанса на модера
        private void SetMessages()
        {
            switch (Date.current_date.Year - registration.Year + 1)
            {
                case 1:
                    messages = 1 * random.Next(10, 150);
                    break;
                case 2:
                    messages = 2 * random.Next(100, 900) + 50;
                    break;
                case 3:
                    messages = 3 * random.Next(300, 2000) + 200;
                    break;
            }
        }
        private void SetNewsComments()
        {
            //Изначальное количество новостей - время на сайте * рандомное число

            switch (Date.current_date.Year - registration.Year + 1)
            {
                case 1:
                    news = random.Next(20);
                    comments = random.Next(1, 75);
                    break;
                case 2:
                    news = 2 * random.Next(15);
                    comments = 4 * random.Next(1, 50) + 25;
                    break;
                case 3:
                    news = 2 * random.Next(10);
                    comments = 8 * random.Next(1, 50) + 75;
                    break;
            }

        }
        public void SetModerChanse()
        {
            moder_chanse = 0;

            if (traits.Contains(TraitsList.SearchTrait(TraitsType.ded)))
            {
                moder_chanse += 10;
            }
            else if (traits.Contains(TraitsList.SearchTrait(TraitsType.newfag)))
            {
                moder_chanse -= 10;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                moder_chanse += 5;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.rak)))
            {
                moder_chanse -= 30;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.madguy)))
            {
                moder_chanse -= 15;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.Wpower)))
            {
                if (random.Next(2) == 0)
                    moder_chanse += 10;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.leader)))
            {
                moder_chanse += 5;
            }
            moder_chanse += relations.RelationCountUsersReturn(RelationType.friend).Count + relations.RelationCountUsersReturn(RelationType.comrade).Count - relations.RelationCountUsersReturn(RelationType.enemy).Count - relations.RelationCountUsersReturn(RelationType.unfriend).Count;
            moder_chanse *= 2;

        }

        //Симпатии и рейтинг
        private void SetLikesRatings()
        {
            //Первые 2 числа - границы процентов симпатий, вторые два - границы рейтинга комментариев
            int min_like = (10 - character.rakness.Value) * 7;
            int max_like = min_like + (10 - character.adeq.Value) * 10;
            int min_coment = 1;
            int max_coment = character.adeq.Value / 2 + 1;
            LikesRateCounting(min_like, max_like, min_coment, max_coment);
        }
        private void LikesRateCounting(int min_pers_likes, int max_pers_likes, int min_rate, int max_rate)
        {
            //У половины комментариев есть шанс получения определенного количества лойсов
            for (int i = 0; i < comments / 2; i++)
            {
                comments_rate += random.Next(min_rate, max_rate);
            }
            //Процент от сообщений
            likes = (int)(Convert.ToDouble(messages) * (Convert.ToDouble(random.Next(min_pers_likes, max_pers_likes))) / 100);
        }

        //Устанавливает форумное влияние
        private void SetForum_influence()
        {
            forum_influence = relations.RelationCountUsersReturn(RelationType.friend).Count * 5 +
                relations.RelationCountUsersReturn(RelationType.comrade).Count * 2 +
                group.Respect * 10 +
                (likes / 100) +
                (int)reputation.Base_reputation;

        }

        //Генерация отношений
        public void GenerateRelation()
        {
            description = UserDescription.GetTextDescription(this);
            //Рандомизирует репутацию на основе отношений
            reputation.ReputationRelations(this);
            karma.KarmaUpdate(this);
            for (int i = 0; i < UsersControl.act_users.Count; i++)
            {
                blocked_users_rep.Add(UsersControl.act_users[i], 0);
            }
            if(!admin)
                group = GroupsControl.ReturnUserGroup(this);
            SetModerChanse();
            SetForum_influence();
        }

        //Присоединение юзера к фракции
        public void JoinFraction(Fraction Fraction, bool main)
        {
            if (main)
            {
                if (Fraction.MemberAcceptCheck(this))
                {
                    main_fraction = Fraction;
                    Fraction.members.Add(this);
                }
            }
            else
            {
                other_fractions.Add(Fraction);
                Fraction.members.Add(this);
            }
        }

        //Обновление юзера, бан и карма
        public void CheckingForUpdates()
        {
            if (Date.current_date.Day == registration.Day + 1)
            {
                SetOldness();
                karma.KarmaUpdate(this);
            }
            if (LastBan.Banned && LastBan.ban_end == Date.current_date)
            {
                Ban ban = new Ban(this);
                ban = bans[bans.Count - 1].BanEnd(this);
                bans.Add(ban);
            }
        }

        public void UpdateBeginDay()
        {
            daylog = new UserDayLog(this);
            //Уменьшение на день доступности проставки репутации заблокированным пользователям
            for (int i = 0; i < blocked_users_rep.Count; i++)
            {
                blocked_users_rep[blocked_users_rep.ElementAt(i).Key]--;
            }
        }

        public void UpdateEndDay()
        {
            if (daylog != null)
            {
                daylog.Changes(this);
                last_thirty_Days.Add(daylog);
                if (last_thirty_Days.Count > 30)
                    last_thirty_Days.RemoveAt(0);
            }
        }

        #region IAdvertisable
        public List<string> Advertisments = new List<string>();
        public void EventAdd7_Description(string adv)
        {
            Advertisments.Add(adv);
        }
        #endregion
    }
}
