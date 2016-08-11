using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Reputation
    {
        public User owner { get; private set; }
        public float pos_reputation { get; private set; }
        public float base_reputation { get; private set; }
        public float otr_reputation { get; private set; }
        public List<ReputationHistory> history = new List<ReputationHistory>();
        Random random = new Random();

        public Reputation(User user)
        {
            owner = user;
            switch (Date.current_date.Year - user.registration.Year + 1)
            {
                case 1:
                    base_reputation = random.Next(0, 51);
                    break;
                case 2:
                    base_reputation = random.Next(51, 101);
                    break;
                case 3:
                    base_reputation = random.Next(101, 201);
                    break;
                default:
                    base_reputation = random.Next(0, 101);
                    break;
            }
            pos_reputation += base_reputation;
        }

        public void ReputationRelations(User user)
        {
            User newfag = user;
            for (int i = 0; i < user.relations.All.Count; i++)
            {

                if (user.m_oldness > user.relations.All.ElementAt(i).Key.m_oldness)
                    newfag = user.relations.All.ElementAt(i).Key;

                string rel;
                rel = user.relations.UsersSpace(user.relations.All.ElementAt(i).Key);
                int wish_modif_pos = 0;
                int wish_modif_otr = 0;
                wish_modif_pos += user.group.respect;
                wish_modif_otr += user.group.respect;
                switch (Date.current_date.Year - user.registration.Year + 1)
                {
                    case 1:
                        wish_modif_pos += 5;
                        wish_modif_otr += 5;
                        break;
                    case 2:
                        wish_modif_pos += 0;
                        wish_modif_otr += 0;
                        break;
                    case 3:
                        wish_modif_pos += 5;
                        wish_modif_otr -= 5;
                        break;
                }
                if (user.mod)
                {
                    wish_modif_pos += 15;
                    wish_modif_otr += 10;
                }
                if(user.character.rakness > 5)
                {
                    wish_modif_pos += -30;
                    wish_modif_otr += 46;
                }
                if (user.character.Creativity > 5 || user.character.Sciense > 5)
                {
                    wish_modif_pos += 15;
                }

                for (int a = 0; a < newfag.m_oldness; a++)
                {

                    switch (rel)
                    {
                        case "friends":
                            if (AdvRandom.PersentChanseBool(40 + wish_modif_pos))
                            {
                                pos_reputation += user.relations.All.ElementAt(i).Key.karma.karma;
                            }
                            break;
                        case "enemies":
                            if (AdvRandom.PersentChanseBool(8 + wish_modif_otr))
                            {
                                otr_reputation += user.relations.All.ElementAt(i).Key.karma.karma;
                            }
                            break;
                        case "unfriends":
                            if (AdvRandom.PersentChanseBool(4 + wish_modif_otr))
                            {
                                otr_reputation += user.relations.All.ElementAt(i).Key.karma.karma;
                            }
                            break;
                        case "comrades":
                            if (AdvRandom.PersentChanseBool(20 + wish_modif_pos))
                            {
                                pos_reputation += user.relations.All.ElementAt(i).Key.karma.karma;
                            }
                            break;
                    }

                }
            }
            base_reputation = pos_reputation - otr_reputation;

        }

        public void ChangeReputation(User author, float Value, string reason)
        {
            ReputationHistory log;
            if (Value > 0)
            {
                log = new ReputationHistory(author, Value, reason, false);
                pos_reputation += Value;
            }
            else
            {
                log = new ReputationHistory(author, Value, reason, true);
                otr_reputation -= Value;
            }
            base_reputation = pos_reputation - otr_reputation;
            author.blocked_users_rep[owner] = 14;
            history.Add(log);
        }


    }
}
