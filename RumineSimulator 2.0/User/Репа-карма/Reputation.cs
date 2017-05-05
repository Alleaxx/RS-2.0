﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Reputation
    {
        public User owner { get; private set; }
        private double pos_reputation;
        public double Pos_reputation
        {
            get
            {
                return Math.Round(pos_reputation, 1);
            }
            set
            {
                pos_reputation = value;
            }
        }
        private double base_reputation;
        public double Base_reputation
        {
            get
            {
                return Math.Round(base_reputation, 1);
            }
            set
            {
                base_reputation = value;
            }
        }
        private double otr_reputation;
        public double Otr_reputation
        {
            get
            {
                return Math.Round(otr_reputation, 1);
            }
            set
            {
                otr_reputation = value;
            }
        }

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
                if(user.character.rakness.Param_value > 5)
                {
                    wish_modif_pos += -30;
                    wish_modif_otr += 46;
                }
                if (user.character.creativity.Param_value > 5 || user.character.sciense.Param_value > 5)
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

        public bool ChangeReputation(User author, float Value, string reason)
        {
            if (author.blocked_users_rep[owner] == 0 || (author == Player.user && GlobalParams.GodMode))
            {
                ReputationHistory log;
                if (Value > 0)
                {
                    log = new ReputationHistory(author, Value, reason, false);
                    pos_reputation += Value;
                    owner.relations.All[author].Chanse_friendness_up += 10;
                }
                else
                {
                    log = new ReputationHistory(author, Value, reason, true);
                    otr_reputation -= Value;
                    owner.relations.All[author].Chanse_friendness_down += 10;
                }
                base_reputation = pos_reputation - otr_reputation;
                author.blocked_users_rep[owner] = 14;
                history.Add(log);
                return true;
            }
            else
            {
                return false;
            }

        }

        public List<ReputationHistory> ReturnRepHistorySort()
        {
            var sortedGr = from i in history
                           orderby i.date descending
                           select i;
            return sortedGr.ToList();
        }




    }
}
