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
        public string nick { get; set; }

        public DateTime registration { get; private set; }
        public DateTime last_activity { get; set; }

        public int curr_timeForTime { get;set; }
        public int total_DayActivity { get; set; }
        public int activity_chanse { get; set; }
        public int activity_times { get; set; }
        public int cooldawn { get; set; }
        public int rnd_num { get; set; }

        public int m_oldness { get; private set; }

        public int news { get; set; }
        public int comments { get; set; }
        public int comments_rate { get; set; }

        public UserGroup group { get; private set; }
        public UserStereoType stereotype { get; private set; }
        public UserCharacter character { get; private set; }
        public UserRelationList relations { get; private set; }
        public Reputation reputation { get; }
        public Karma karma { get; }
        public List<UserTrait> traits = new List<UserTrait>();


        public int messages { get;set; }
        public int likes { get; set; }

        public bool ban { get; private set; }
        public bool mod { get; private set; }
        public bool admin { get; private set; }
        public bool activity { get; set; }

        public string description { get; private set; }

        public UserDayLog daylog { get; private set; }
        public List<UserDayLog> last_thirty_Days = new List<UserDayLog>();
        public Dictionary<User, int> blocked_users_rep = new Dictionary<User, int>();

        Random random;

        #endregion

        public User()
        {
            random = new Random();
            //Ник получаем из списка свободных ников
            nick = UserNicks.SelectFreeNick();

            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%), на ее основе комменты и новости
            SetRegistration();
            SetNewsComments();
            SetMessages();
            reputation = new Reputation(this);
            karma = new Karma(this);

            activity = false;

            //Генерируем характер
            character = new UserCharacter(this);
            traits = UserTraitsList.ReturnTraits(this);

            //На основе характера получаем стереотип и доступные группы
            stereotype = UserStereoTypeControl.UserChooseStereType(this);

            //Группу получаем рандомом из списка доступных
            group = UserGroupsControler.ReturnUserGroup(this);
            

            //Устанавливаем симпатии юзера
            SetLikesRatings();

            //Инициализируем отношения                                             
            relations = new UserRelationList();
            SetActivities();

        }

        //Установка даты регистрации
        private void SetRegistration()
        {
            //Устанавливаем рандомную дату регистрации(2011 - 30%, 2012 - 30%, 2013 - 40%)
            if (AdvRandom.PersentChanseBool(40))
            {
                registration = new DateTime(random.Next(Date.found_date.Year, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));

            }
            else if (AdvRandom.PersentChanseBool(50))
            {
                registration = new DateTime(random.Next(Date.found_date.Year+1, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));

            }
            else
            {
                registration = new DateTime(random.Next(Date.found_date.Year+2, Date.current_date.Year + 1), random.Next(1, 13), random.Next(1, 29));
            }
            if (registration.Month <= Date.found_date.Month && registration.Year == Date.found_date.Year)
                registration = new DateTime(registration.Year, random.Next(Date.found_date.Month + 1, 13), registration.Day);
            if (registration.Month >= Date.current_date.Month && registration.Year == Date.current_date.Year)
                registration = new DateTime(registration.Year, random.Next(1, Date.current_date.Month), registration.Day);
            SetOldness();

        }
     
        private void GroupModifier()
        {
            if (group == UserGroupsControler.Groups[GroupsList.Moderator])
                mod = true;
            if (group.journ)
            {
                news *= random.Next(2, 5) + 5;
                comments *= random.Next(1, 4) + 10;
            }
        }
        private void SetOldness()
        {
            if (Date.current_date.Year - registration.Year > 1)
            {
                m_oldness = (13 - registration.Month + ((Date.current_date.Year - registration.Year - 1) * 12) + Date.current_date.Month);
            }
            else if (Date.current_date.Year - registration.Year == 1)
            {
                m_oldness = (13 - registration.Month + Date.current_date.Month);
            }
            else if (Date.current_date.Year - registration.Year == 0)
            {
                m_oldness = Date.current_date.Month - registration.Month + 1;
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

            switch (Date.current_date.Year-registration.Year+1)
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

        #region Установка симпатий и рейтинга
        private void SetLikesRatings()
        {
            //Первые 2 числа - границы процентов симпатий, вторые два - границы рейтинга комментариев
            switch (stereotype.type)
            {
                case StereoTypesEnum.NewfagUsual:
                    LikesRateCounting(10, 70, 0, 2);
                    break;
                case StereoTypesEnum.NewfagCalm:
                    LikesRateCounting(20, 80, 0, 3);
                    break;
                case StereoTypesEnum.NewfagRak:
                    LikesRateCounting(10, 50, 0, 1);
                    break;
                case StereoTypesEnum.NewfagTroll:
                    LikesRateCounting(5, 30, 0, 1);
                    break;
                case StereoTypesEnum.NewfagSchool:
                    LikesRateCounting(5, 60, 0, 3);
                    break;
                case StereoTypesEnum.UserUsual:
                    LikesRateCounting(20, 90, 0, 3);
                    break;
                case StereoTypesEnum.UserAdvanced:
                    LikesRateCounting(40, 130, 0, 4);
                    break;
                case StereoTypesEnum.UserRak:
                    LikesRateCounting(10, 60, 0, 2);
                    break;
                case StereoTypesEnum.UserRakAdvanced:
                    LikesRateCounting(10, 30, 0, 2);
                    break;
                case StereoTypesEnum.UserSchoolAdv:
                    LikesRateCounting(10, 70, 0, 3);
                    break;
                case StereoTypesEnum.OldfagUsual:
                    LikesRateCounting(50, 175, 0, 4);
                    break;
                case StereoTypesEnum.OldfagMad:
                    LikesRateCounting(1, 220, 0, 3);
                    break;
                case StereoTypesEnum.OldfagRak:
                    LikesRateCounting(5, 30, 0, 2);
                    break;
                case StereoTypesEnum.OldfagBad:
                    LikesRateCounting(30, 80, 0, 3);
                    break;
                case StereoTypesEnum.Something:
                    LikesRateCounting(1, 100, 0, 2);
                    break;
            }
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
        #endregion

        //Генерация отношений
        public void GenerateRelation()
        {
            relations.RelationsGenerate(this);
            description = UserDescription.GetTextDescription(this);
            reputation.ReputationRelations(this);
            karma.KarmaUpdate(this);
            for (int i = 0; i < UserControl.UserAmount; i++)
            {
                blocked_users_rep.Add(UserControl.Users[i],0);
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
            if (Date.current_date.Hour == 0 && Date.current_date.Minute < Date.current_date_prev.Minute)
            {
                SetActivities();
                daylog = new UserDayLog(this);
                for (int i = 0; i < UserControl.UserAmount; i++)
                {
                    if (blocked_users_rep[UserControl.Users[i]] > 0)
                        blocked_users_rep[UserControl.Users[i]]--;
                }
            }
            if(Date.current_date.Hour == 23 && Date.current_date.Minute == 59)
            {
                if(daylog != null)
                {
                    daylog.Changes(this);
                    last_thirty_Days.Add(daylog);
                    if (last_thirty_Days.Count > 30)
                        last_thirty_Days.RemoveAt(0);
                }               
            }
            if (Date.current_date.Minute < Date.current_date_prev.Minute)
            {
                if (cooldawn > 0)
                    cooldawn--;
            }
            if (Date.current_date.Day == registration.Day + 1)
            {
                SetOldness();
                karma.KarmaUpdate(this);
            }

        }
        public void SetActivities()
        {
            activity_times = random.Next(0,11-random.Next(character.leaveChanse,character.leaveChanse+2));
            activity_chanse = (11 - character.leaveChanse) * 3;
        }

        public void SetRandomNum()
        {
            rnd_num = random.Next(2);
        }
    }
}
