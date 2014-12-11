using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using ACS.Common.Constant;
using ACS.Common.Util;
namespace ACS.Common.Dao
{
    public class SessionManager
    {
        private static SessionManager instance;
        private readonly ISessionFactory sessionFactory;
        private SessionManager()
        {
            sessionFactory = new Configuration().Configure().BuildSessionFactory();
        }
        public static SessionManager getInstance()
        {
            if (instance == null)
            {
                instance = new SessionManager();
            }
            return instance;
        }
        public ISession GetSession()
        {
            return sessionFactory.OpenSession();
        }

       public static  ICriteria getCriteriaByCondition(Type clazz , List<QueryCondition> conditionList,ISession s)
       {
		ICriteria c  = s.CreateCriteria(clazz);
		if(null != conditionList)
		{
			foreach(QueryCondition condition in conditionList)
			{
 
				Object valueObject = null;
				if(null != condition.FirstValue)
				{
					valueObject = ReflectionUtil.convertToFieldObject(clazz, condition.AttrName, condition.FirstValue);
				}
				switch(condition.ConditionType)
				{
				case ConditionTypeEnum.INCLUDLE:
					c.Add(Restrictions.Like(condition.AttrName,"%"+condition.FirstValue+"%"));
					break;
				case ConditionTypeEnum.EXCLUDLE:
					c.Add(Restrictions.Like(condition.AttrName,"%"+condition.FirstValue+"%"));
					break;
				case ConditionTypeEnum.EQUAL:	
					c.Add(Restrictions.Eq(condition.AttrName,valueObject));
					break;
 
				case ConditionTypeEnum.BIGER:	
					c.Add(Restrictions.Gt(condition.AttrName,valueObject));
					break;	
				case ConditionTypeEnum.BIGER_EQ:	
					c.Add(Restrictions.Ge(condition.AttrName,valueObject));
					break;	
				case ConditionTypeEnum.LOWER:	
					c.Add(Restrictions.Lt(condition.AttrName,valueObject));
					break;	
				case ConditionTypeEnum.LOWER_EQ:	
					c.Add(Restrictions.Le(condition.AttrName,valueObject));
					break;	
				case ConditionTypeEnum.BETWEEN:	
					Object secondValueObject = ReflectionUtil.convertToFieldObject(clazz, condition.AttrName, condition.SecondValue);
					c.Add(Restrictions.Between(condition.AttrName,valueObject,secondValueObject));
					break;	
				case ConditionTypeEnum.IN:
					List<Object> listObject = new List<Object>();
					foreach(String e in  condition.ListValue)
					{
						listObject.Add(ReflectionUtil.convertToFieldObject(clazz, condition.AttrName, e));
					}
					c.Add(Restrictions.In(condition.AttrName,listObject));
					 
					break;
                case ConditionTypeEnum.FALSE:
                    c.SetFirstResult(0);
                    c.SetMaxResults(0);
                    break;
				case ConditionTypeEnum.DESC_ORDER:	
					 c.AddOrder(Order.Desc(condition.AttrName));
					break;	
				case ConditionTypeEnum.ASC_ORDER:	
					c.AddOrder(Order.Asc(condition.AttrName));
					break;	
				default:
				    break;
				  
				}
 			}
		}

		return c;
	}
	
    }
}