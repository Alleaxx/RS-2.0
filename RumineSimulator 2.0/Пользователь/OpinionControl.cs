using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class OpinionControl
    {
        private User owner { get; set; }
        private string opinion_text = "";
        private ReactionTypeAct reactionA;
        private ReactionTypeMark reactionM;
        private int negative = 0, neutral = 0, positive = 0,passive = 0,actvie = 0;

        public OpinionControl(User user)
        {
            owner = user;
        }

        public Opinion ReturnGroupOpinion(Group group)
        {
            PrepareOpinion();

            return new Opinion(group.Name, opinion_text, reactionM, reactionA);
        }

        private void PrepareOpinion()
        {
            opinion_text = "";
            negative = 0;
            neutral = 0;
            positive = 0;
            passive = 0;
            actvie = 0;
            reactionA = ReactionTypeAct.passive;
            reactionM = ReactionTypeMark.positive;
        }

        private void PrepareFinalOpinion()
        {
            if (negative > positive && negative > neutral)
                reactionM = ReactionTypeMark.negative;
            else if (positive > negative && positive > neutral)
                reactionM = ReactionTypeMark.positive;
            else if (neutral > positive && neutral > negative)
                reactionM = ReactionTypeMark.neutral;
            else
                reactionM = ReactionTypeMark.neutral;

            if (passive > actvie)
                reactionA = ReactionTypeAct.passive;
            else if (actvie > passive)
                reactionA = ReactionTypeAct.active;
            else
                reactionA = ReactionTypeAct.passive;
        }


    }
    //Тип реакции на объект мнения
    enum ReactionTypeMark
    {
        positive,negative,neutral
    }
    //Деятельность по отношению на объект
    enum ReactionTypeAct
    {
        passive,active
    }
}
