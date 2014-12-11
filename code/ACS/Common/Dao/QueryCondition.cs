using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;

namespace ACS.Common.Dao
{
    public class QueryCondition
    {
        public QueryCondition(ConditionTypeEnum conditionType)
        {

            this.conditionType = conditionType;
         }

        public QueryCondition(ConditionTypeEnum conditionType, String attrName)
        {
           
            this.conditionType = conditionType;
            this.attrName = attrName;
        }

        public QueryCondition(ConditionTypeEnum conditionType, String attrName, String firstValue)
        {
            
            this.conditionType = conditionType;
            this.attrName = attrName;
            this.firstValue = firstValue;
        }
        public QueryCondition(ConditionTypeEnum conditionType, String attrName, List<String> listValue)
        {
          
            this.conditionType = conditionType;
            this.attrName = attrName;
            this.listValue = listValue;
        }
        public QueryCondition(ConditionTypeEnum conditionType, String attrName, List<int> listValue)
        {

            this.conditionType = conditionType;
            this.attrName = attrName;
            List<String> strValueLIst = new List<String>();
            if (null != listValue)
            {
                foreach(int i in listValue)
                {
                    strValueLIst.Add(i.ToString());
                }
            }
 
            this.listValue = strValueLIst;
        }
        /**
         * @param conditionType
         * @param attrName
         * @param firstValue
         * @param secondValue
         */
        public QueryCondition(ConditionTypeEnum conditionType, String attrName, String firstValue, String secondValue)
        {
           
            this.conditionType = conditionType;
            this.attrName = attrName;
            this.firstValue = firstValue;
            this.secondValue = secondValue;
        }
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