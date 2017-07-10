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
        public List<TraitsType> ideology_traits = new List<TraitsType>();
        public List<TraitsType> enemy_traits = new List<TraitsType>();
        public int Influence
        {
            get
            {
                int infl = 0;
                foreach (User user in members)
                {
                    infl += user.forum_influence;
                }
                return infl;
            }
        }
        public IntView_Fraction Interface_Info
        {
            get
            {
                return new IntView_Fraction(this);
            }
        }

        public bool active;
        public bool no_cond;
        public bool hard_cond;

        public User leader { get; private set; }

        public string ideology { get; private set; }

        public List<Fraction> enemy_fractions = new List<Fraction>();
        public List<User> members = new List<User>();

        public FractionAggression aggro_type = new FractionAggression();

        public Fraction(string Name, List<TraitsType> Ideology, List<TraitsType> EnemyTraits, FractionAggression Type, bool HardCond, bool NoCond = false, bool active = false)
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
            //Выбор лидера фракции
            int leader_influence = 0;
            foreach (User user in members)
            {
                if (user.forum_influence > leader_influence)
                {
                    leader = user;
                    leader_influence = user.forum_influence;
                }
            }
            //Вражеские фракции
            foreach (Fraction fraction in FractionList.AllFractions)
            {
                if (fraction != this)
                {
                    foreach (TraitsType bad_trait in enemy_traits)
                    {
                        if (fraction.ideology_traits.Contains(bad_trait))
                        {
                            enemy_fractions.Add(fraction);
                        }

                    }
                }
            }
        }

        //Проверка на принятие во фракцию
        public bool MemberAcceptCheck(User user)
        {
            int need_traits = 0;
            if (no_cond)
                return true;
            foreach (Trait trait in user.traits)
            {
                foreach (TraitsType t_type in enemy_traits)
                {
                    if (trait.type == t_type)
                    {
                        return false;
                    }
                }
                foreach (TraitsType t_type in ideology_traits)
                {
                    if (!hard_cond && trait.type == t_type)
                    {
                        return true;
                    }
                    else if (trait.type == t_type)
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
