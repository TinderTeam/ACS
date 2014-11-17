﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service.Constant;
using ACS.Common.Util;

namespace ACS.Service.Impl
{
    public class AccessServiceImpl:AccessService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        CommonDao<Access> accessDao = DaoContext.getInstance().getAccessDao();

        //门禁权限管理
        //获取门禁权限树
        public List<Access> getAccessList(string userID)
        {
            //获取所有门禁权限树列表
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Access.UserID, userID);
            List<Access> accessList = accessDao.getAll(condition);
            return accessList;
        }
        //门禁权限管理
        //根据ID获取门禁权限
        public Access getAccessByID(string accessID)
        {
            //获取所有门禁权限树列表
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Access.ID, accessID);
            Access access = accessDao.getUniRecord(condition);
            return access;
        }
        //门禁权限管理
        //新增门禁权限
        public void addAccess(int creatUserID, string accessName)
        {
            //新增门禁权限树列表
            Access newAccess = new Access();
            newAccess.AccessName = accessName;
            newAccess.CreateUserID = creatUserID;
            newAccess.CreateDate = DateTime.Now;
            accessDao.create(newAccess);
        }
        //门禁权限管理
        //更新门禁权限
        public void updateAccess(Access access)
        {
            //更新门禁权限树列表
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.EQUAL, Access.ID, access.AccessID.ToString());
            Access orignalAccess = accessDao.getUniRecord(IDcondition);
            orignalAccess.AccessName = access.AccessName;
            accessDao.update(orignalAccess);
        }
        //门禁权限管理
        //删除门禁权限
        public void deleteAccess(string accessID)
        {
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.EQUAL, Access.ID, accessID);
            Access access = accessDao.getUniRecord(IDcondition);

            if (null == access)
            {
                log.Error("Delete failed, the Access to be deleted is not exit, AccessID is" + accessID);
                throw new SystemException(ExceptionMsg.ACCESS_NOT_EXIST);
            }

            accessDao.delete(IDcondition);
        }
    }
}