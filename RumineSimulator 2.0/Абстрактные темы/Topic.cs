using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    //Возможные темы обсуждения для привнесения рандома
    class Topic
    {
        public string text;

        public TopicType type;

        public Topic(string Text, TopicType Type)
        {
            text = Text;
            type = Type;
        }

    }
}
