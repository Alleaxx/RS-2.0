using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    //Сформированное мнение по поводу какой-то сущности
    class Opinion
    {
        //Параметры мнения
        public string opinion_object { get; private set; }
        public string text_opinion { get; private set; }
        public ReactionTypeMark reactionM { get; private set; }
        public ReactionTypeAct reactionA { get; private set; }
        //Задание мнения
        public Opinion(string op_object, string text, ReactionTypeMark mark,ReactionTypeAct activity)
        {
            opinion_object = op_object;
            text_opinion = text;
            reactionA = activity;
            reactionM = mark;
        }
    }
}
