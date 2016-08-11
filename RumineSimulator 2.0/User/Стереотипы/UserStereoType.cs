using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RumineSimulator_2._0
{
    class UserStereoType
    {
        //Письменное название стереотипа
        public string text { get; private set; }
        public StereoTypesEnum type { get; private set; }

        //Необходимые параметры для получения
        public int req_oldness { get;private set; }

        public int req_min_adeq { get; private set; }
        public int req_max_adeq { get; private set; }

        public int req_min_rakness { get; private set; }
        public int req_max_rakness { get; private set; }


        Random random = new Random();
        //Доступные и стандартная группы
        public UserGroup standartGroup { get; private set; }
        public List<UserGroup> AvailableGroups = new List<UserGroup>();
        //Доступные повышения в стереотипах
        public List<UserStereoType> AvailableTypesUp = new List<UserStereoType>();
        public List<UserStereoType> AvailableTypesDown = new List<UserStereoType>();

        //Установка стереотипа
        public UserStereoType(string Text,StereoTypesEnum Type, int m_Oldness, int min_Adekv, int max_Adekv,int min_Rak,int max_Rak,UserGroup StandartGroup)
        {
            req_oldness = m_Oldness;
            req_min_adeq = min_Adekv;
            req_max_adeq = max_Adekv;
            req_min_rakness = min_Rak;
            req_max_rakness = max_Rak;
            type = Type;
            text = Text;
            standartGroup = StandartGroup;            
        }

        public override string ToString()
        {
            return text;
        }
    }
}
