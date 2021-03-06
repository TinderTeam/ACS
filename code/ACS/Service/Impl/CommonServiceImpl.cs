﻿using ACS.Common;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Model;
using ACS.Common.Util;
using ACS.Dao;
using ACS.Service.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service.Impl
{
    public abstract class CommonServiceImpl<E>:CommonService<E>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CommonDao<E> GetDao()
        {
            return DaoContext.getInstance().getDao<E>();
        }
        public CommonDao<T> GetDao<T>()
        {
            return DaoContext.getInstance().getDao<T>();
        }
        public virtual void Validator(E obj)
        {
            log.Debug("the validator is empty ");
        }
        public virtual void Create(E obj)
        {
            Validator(obj);
            DaoContext.getInstance().getDao<E>().create(obj);
        }
        public virtual void Create(int userID , E obj)
        {
            Validator(obj);  
            DaoContext.getInstance().getDao<E>().create(obj);
            CreateOperateLog(userID, ServiceConstant.CREATE_LOG,obj); 
        }
        
        public virtual void Delete(String id)
        {
            if (ValidatorUtil.isEmpty(id))
            {
                log.Warn("the id is empty");
                return;
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, GetObjectInfo().PrimaryName, id);

            DaoContext.getInstance().getDao<E>().delete(condition);
        }
        public virtual void Delete(int userID, List<String> idList)
        {
            if (ValidatorUtil.isEmpty(idList))
            {
                log.Warn("the id list is empty");
                return;
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, GetObjectInfo().PrimaryName, idList);
            List<E> objList = Get(idList);

            DaoContext.getInstance().getDao<E>().delete(condition);

            CreateOperateLog(userID, ServiceConstant.DELETE_LOG, objList);
        }

        public virtual void Modify(E obj)
        {
            //Validator(obj);
            DaoContext.getInstance().getDao<E>().update(obj);
        }
        public virtual void Modify(int userID ,E obj)
        {
            //Validator(obj);
            DaoContext.getInstance().getDao<E>().update(obj);
            CreateOperateLog(userID, ServiceConstant.MODIFY_LOG,obj);
        }
        public virtual void Modify(List<String> idList,String fieldName,Object fieldValue)
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
        public virtual AbstractDataSource<E> GetDataSource(List<QueryCondition> conditionList)
        {
            AbstractDataSource<E> dataSource = new DatabaseSourceImpl<E>(conditionList);
            return dataSource;
        }

        public virtual AbstractDataSource<T> GetDataSource<T>(List<QueryCondition> conditionList)
        {
            AbstractDataSource<T> dataSource = new DatabaseSourceImpl<T>(conditionList);
            return dataSource;
        }

        public virtual E Get(String id)
        {
            if (ValidatorUtil.isEmpty(id))
            {
                log.Error("the id is empty");
                return default(E);
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, GetObjectInfo().PrimaryName, id);
            return DaoContext.getInstance().getDao<E>().getUniRecord(condition);
        }
        public virtual List<E> Get(List<String> idList)
        {
            if (ValidatorUtil.isEmpty(idList))
            {
                log.Error("the id list is empty");
                return new List<E>();
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, GetObjectInfo().PrimaryName, idList);
            return DaoContext.getInstance().getDao<E>().getAll(condition);
        }

        public virtual T Get<T>(String key,String value)
        {
            log.Info("the object is " + typeof(T) +",the key is " + key + ",the value " + value);
            if (ValidatorUtil.isEmpty(key))
            {
                log.Error("the key is empty");
                return default(T);
            }
            if (ValidatorUtil.isEmpty(value))
            {
                log.Error("the value is empty");
                return default(T);
            }
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, key, value);
            return DaoContext.getInstance().getDao<T>().getUniRecord(condition);
        }

        public abstract PersistenceObjInfo GetObjectInfo();

        //创建一条系统操作日志
        public void CreateOperateLog(int userID, String operateType, E obj)
        {
            if (typeof(LogOperable).IsAssignableFrom(typeof(E)))
            {
                ServiceContext.getInstance().getLogService().CreateLog(userID, operateType, (LogOperable)obj, ServiceConstant.SUCCESS);
            }
            else
            {
                log.Warn("the type of " + typeof(E) + " is not a operable class");
            }
        }
        //创建系统操作日志List
        public void CreateOperateLog(int userID, String operateType, List<E> objList)
        {
           
            if (typeof(LogOperable).IsAssignableFrom(typeof(E)))
            {
                List<LogOperable> logList = new List<LogOperable>();
                foreach (E e in objList)
                {
                    logList.Add((LogOperable)e);
                }
                ServiceContext.getInstance().getLogService().CreateLog(userID, operateType, logList, ServiceConstant.SUCCESS);
            }
            else
            {
                log.Warn("the type of " + typeof(E) + " is not a operable class");
            }
        }

    }
}