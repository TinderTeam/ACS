using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Util;
using ACS.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service.Impl
{
    public abstract class CommonServiceImpl<E>:CommonService<E>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public virtual void Validator(E obj)
        {
            log.Debug("the validator is empty ");
        }
        public virtual void Create(E obj)
        {
            Validator(obj);
            DaoContext.getInstance().getDao<E>().create(obj);
        }

        public virtual void Delete(String id)
        {
            if (ValidatorUtil.isEmpty(id))
            {
                log.Warn("the id is empty");
                return;
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, GetPrimaryName(), id);

            DaoContext.getInstance().getDao<E>().delete(condition);
        }
        public virtual void Delete(List<String> idList)
        {
            if (ValidatorUtil.isEmpty(idList))
            {
                log.Warn("the id list is empty");
                return;
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, GetPrimaryName(), idList);

            DaoContext.getInstance().getDao<E>().delete(condition);
        }

        public virtual void Modify(E obj)
        {
            Validator(obj);
            DaoContext.getInstance().getDao<E>().update(obj);
        }
        public virtual void Modify(List<String> idList,String fieldName,String fieldValue)
        {
            if (ValidatorUtil.isEmpty(idList))
            {
                log.Warn("the id list is empty");
                return;
            }
            List<E> objList = Get(idList);
            if (ValidatorUtil.isEmpty<E>(objList))
            {
               log.Warn("can not find the object by id list " + idList.ToString());
               return;
            }

            foreach(E e in objList)
            {
                ReflectionUtil.setObjetField(e,fieldName,fieldValue);
            }

            DaoContext.getInstance().getDao<E>().update(objList);
        }
        public virtual AbstractDataSource<E> GetDataSource()
        {
            AbstractDataSource<E> dataSource = new DatabaseSourceImpl<E>();
            return dataSource;
        }

        public virtual AbstractDataSource<E> getDataSource(List<QueryCondition> conditionList)
        {
            AbstractDataSource<E> dataSource = new DatabaseSourceImpl<E>(conditionList);
            return dataSource;
        }

        public virtual E Get(String id)
        {
            if (ValidatorUtil.isEmpty(id))
            {
                log.Error("the id is empty");
                return default(E);
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, GetPrimaryName(), id);
            return DaoContext.getInstance().getDao<E>().getUniRecord(condition);
        }
        public virtual List<E> Get(List<String> idList)
        {
            if (ValidatorUtil.isEmpty(idList))
            {
                log.Error("the id list is empty");
                return new List<E>();
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, GetPrimaryName(), idList);
            return DaoContext.getInstance().getDao<E>().getAll(condition);
        }

        public virtual String GetPrimaryName();
    }
}