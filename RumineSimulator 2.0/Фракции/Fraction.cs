using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class Fraction
    {
        public int id { get; set; }
        public string name { get; private set; }
        public List<Traits> ideology_traits = new List<Traits>();
        public List<Traits> enemy_traits = new List<Traits>();
        public int Influence { get; set;}
        public InterfaceView_Fraction Interface_Info { get; set; }

        public bool active;
        public bool no_cond;
        public bool hard_cond;

        public User leader { get; private set; }
        public Squad squad { get; private set; }


        public string ideology { get; private set; }

        public List<Fraction> enemy_fractions = new List<Fraction>();
        public List<User> members = new List<User>();

        public FractionAggression aggro_type = new FractionAggression();

        public Fraction(string Name, List<Traits> Ideology, List<Traits> EnemyTraits, FractionAggression Type,bool HardCond,bool NoCond = false,bool active = false)
        {
            id = FractionList.id_total++;
            name = Name;
            ideology_traits = Ideology;
            enemy_traits = EnemyTraits;
            aggro_type = Type;
            no_cond = NoCond;
            hard_cond = HardCond;
        }

        public void FractionCreationEnd()
        {
            Influence = 0;
            int leader_influence = 0;
            foreach (User user in members)
            {
                Influence += user.forum_influence;
                if(user.forum_influence > leader_influence)
                {
                    leader = user;
                    leader_influence = user.forum_influence;
                }
            }
            foreach (Fraction fraction in FractionList.AllFractions)
            {
                if(fraction != this)
                {
                    foreach (Traits true_trait in ideology_traits)
                    {
                        foreach (Traits bad_trait in fraction.enemy_traits)
                        {
                            if(bad_trait == true_trait)
                            {
                                enemy_fractions.Add(fraction);
                                break;
                            }
                        }
                    }
                }
            }
            Interface_Info = new InterfaceView_Fraction(this);
        }

        public bool MemberAcceptCheck(User user)
        {
            int need_traits = 0;
            if (no_cond)
                return true;
            foreach (Trait trait in user.traits)
            {
                foreach (Traits t_type in enemy_traits)
                {
                    if (trait.id == t_type)
                    {
                        return false;
                    }
                }
                foreach (Traits t_type in ideology_traits)
                {
                    if (!hard_cond && trait.id == t_type)
                    {
                        return true;
                    }
                    else if (trait.id == t_type)
                    {
                        need_traits++;
                    }
                }
            }
            if (hard_cond && need_traits == ideology_traits.Count)
                return true;
            return false;
        }



    }
    enum FractionAggression
    {
        peacefull,
        light,
        normal,
        ambigious,
        aggressive,
        WAR,
    }
}
