using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class User
    {
        #region Свойства пользователя
        public int user_id { get; private set; }
        public string nick { get; set; }

        public DateTime registration { get; private set; }
        private DateTime last_activity;
        public DateTime Last_activity
        {
            get
            {
                return last_activity;
            }
            set
            {
                last_activity = value;
            }
        }

        public int rnd_num { get; set; }
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


        public int messages { get; set; }
        public int likes { get; set; }

        public bool mod { get;set; }
        public bool admin { get; private set; }
        public bool activity { get; set; }

        public string description { get; private set; }

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

        public User()
        {
            random = new Random();
            //Ник получаем из списка свободных ников
            user_id = UsersControl.UserAmount + 1;
            nick = Nicks.SelectFreeNick();

            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%), на ее основе комменты и новости
            SetRegistration();

            SetNewsComments();
            SetMessages();
            reputation = new Reputation(this);
            karma = new Karma(this);

            activity = false;

            //Генерируем характер
            character = new Character(this);
            traits = TraitsList.ReturnTraits(this);

            //На основе характера получаем стереотип и доступные группы
            group = GroupsControl.ReturnUserRandomGroup();
            SetLikesRatings();

            //Инициализируем отношения                                             
            relations = new RelationControl(this);
            CharacterMod();
            TraitMod();
            SetActivities();
            bans.Add(new Ban(this));
        }

        //Установка даты регистрации
        private void SetRegistration()
        {
            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%)
            if (AdvRnd.PersentChanseBool(40))
            {
                registration = new DateTime(random.Next(Date.found_date.Year, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));

            }
            else if (AdvRnd.PersentChanseBool(50))
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
            SetOldness();

        }

        private void CharacterMod()
        {
            news_quality = random.Next(0,10) + character.creativity.Value * 5 + character.sciense.Value * 5;
            if(news == 0)
            {
                news_quality = 0;
            }
        }

        private void TraitMod()
        {
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.newslover)))
            {
                news *= random.Next(1, 3) + 3;
                comments = comments * random.Next(1, 3) + 5;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.vilka)))
            {
                comments = comments * random.Next(1, 4);
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.accurateguy)))
            {
                news_quality += 10;
            }
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.leader)))
            {
                forum_influence *= 2;
            }
        }

        private void GroupMod()
        {
            if (group.Mod)
                mod = true;
        }

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

        //Установка кол-ва сообщений, комментариев и новостей в зависимости от реги
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

        #region Установка симпатий и рейтинга
        private void SetLikesRatings()
        {
            //Первые 2 числа - границы процентов симпатий, вторые два - границы рейтинга комментариев
            int min_like = (10 - character.rakness.Value) * 7;
            int max_like = min_like + (10 - character.adeq.Value) * 10;
            int min_coment = 1;
            int max_coment = character.adeq.Value / 2 + 1;
            LikesRateCounting(min_like,max_like,min_coment,max_coment);
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

        private void SetForum_influence()
        {
            forum_influence = relations.RelationCountUsersReturn(RelationType.friend).Count * 5 +
                relations.RelationCountUsersReturn(RelationType.comrade).Count * 2 +
                group.Respect * 10 +
                (likes / 100) + 
                (int)reputation.Base_reputation;
            if (traits.Contains(TraitsList.SearchTrait(TraitsType.leader))) ;
            forum_influence = forum_influence + (forum_influence / 2);
        }
        #endregion

        //Генерация отношений
        public void GenerateRelation()
        {
            description = UserDescription.GetTextDescription(this);
            reputation.ReputationRelations(this);
            karma.KarmaUpdate(this);
            for (int i = 0; i < UsersControl.UserAmount; i++)
            {
                blocked_users_rep.Add(UsersControl.Users[i], 0);
            }
            group = GroupsControl.ReturnUserGroup(this);
            GroupMod();
            SetModerChanse();
            SetForum_influence();
        }

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

        //Пользователь в строку
        public override string ToString()
        {
            return nick;
        }

        //Обновление юзера со временем
        public void CheckingForUpdates()
        {
            if (Date.current_date.Day == registration.Day + 1)
            {
                SetOldness();
                karma.KarmaUpdate(this);
            }
            if(LastBan.Banned && LastBan.ban_end == Date.current_date)
            {
                Ban ban = new Ban(this);
                ban = bans[bans.Count - 1].BanEnd(this);
                bans.Add(ban);
            }
        }
        public void UpdateBeginDay()
        {
            SetActivities();
            daylog = new UserDayLog(this);
            for (int i = 0; i < UsersControl.UserAmount; i++)
            {
                if (blocked_users_rep[UsersControl.Users[i]] > 0)
                    blocked_users_rep[UsersControl.Users[i]]--;
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
        public void SetActivities()
        {

        }

        public void CheckReputation()
        {
            //Dictionary<User, bool> last_changes = new Dictionary<User, bool>();
            //for (int i = reputation.history.Count-random.Next(1,4); i < reputation.history.Count; i++)
            //{
            //    if (i < 0)
            //        i = 0;
            //    last_changes.Add(reputation.history[i].author, reputation.history[i].otr);
            //}
            //int choise = 0;
            //for (int i = 0; i < last_changes.Count; i++)
            //{
            //    if (relations.friends.ContainsKey(last_changes.ElementAt(i).Key) || relations.comrades.ContainsKey(last_changes.ElementAt(i).Key))
            //        choise += 50;
            //    else if (relations.enemies.ContainsKey(last_changes.ElementAt(i).Key) || relations.unfriends.ContainsKey(last_changes.ElementAt(i).Key))
            //        choise -= 50;
            //    if (last_changes.ElementAt(i).Value)
            //        choise -= 25;
            //    else if (last_changes.ElementAt(i).Value)
            //        choise += 25;

            //    if(choise > 0)
            //    {
            //        if (AdvRandom.PersentChanseBool(choise))
            //        {
            //            last_changes.ElementAt(i).Key.reputation.ChangeReputation(this, karma.karma, ReputationReason.ReturnReason(false) + "(Убийца - садовник!)");
            //        }
            //    }
            //    else if(choise == 0)
            //    {
            //        if(random.Next(2) == 0)
            //            last_changes.ElementAt(i).Key.reputation.ChangeReputation(this, karma.karma, ReputationReason.ReturnReason(false) + "(Убийца - садовник!)");
            //        else
            //            last_changes.ElementAt(i).Key.reputation.ChangeReputation(this, -karma.karma, ReputationReason.ReturnReason(true) + "(Убийца - садовник!)");
            //    }
            //    else
            //    {
            //        if (AdvRandom.PersentChanseBool(choise))
            //        {
            //            last_changes.ElementAt(i).Key.reputation.ChangeReputation(this, -karma.karma, ReputationReason.ReturnReason(true) + "(Убийца - садовник!)");
            //        }
            //    }

            //}

        }
    }
}
