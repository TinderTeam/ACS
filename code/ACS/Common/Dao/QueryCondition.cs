using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;

namespace ACS.Common.Dao
{
    public class QueryCondition
    {
        private ConditionTypeEnum conditionType;
        public ConditionTypeEnum ConditionType
        {
            get { return conditionType; }
            set { conditionType = value; }
        }

        private String attrName;

        public String AttrName
        {
            get { return attrName; }
            set { attrName = value; }
        }
        private String firstValue;

        public String FirstValue
        {
            get { return firstValue; }
            set { firstValue = value; }
        }
        private String secondValue;

        public String SecondValue
        {
            get { return secondValue; }
            set { secondValue = value; }
        }
        private List<String> listValue;

        public List<String> ListValue
        {
            get { return listValue; }
            set { listValue = value; }
        }



    }
}