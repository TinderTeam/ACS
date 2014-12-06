using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Service.Constant;
using ACS.Models.Po.CF;
using ACS.Common.Util;
namespace ACS.Service.Impl
{
    public class PrivilegeServiceImpl : PrivilegeService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();

        //根据用户ID，权限类型，获取权限值列表
        public List<String> getPrivilegeValueList(string userID, string PrivilegeType)
        {
            List<String> PrivilegeValueList = new List<string>();
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, PrivilegeType));
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            //判断Privilege列表是否为空
            if (ValidatorUtil.isEmpty<Privilege>(userPrivilegeList))
            {
                log.Warn("There is no Privilege of this user. the user id is " + userID);
                return null;
            }
            foreach (Privilege privilege in userPrivilegeList)
            {
                PrivilegeValueList.Add(privilege.PrivilegeAccessValue);
            }
            return PrivilegeValueList;
        }
        
        /// <summary>
        /// 新增一个用户的某个控制器域权限
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="controlID"></param>
        public void CreateDomainPrivilege(string userID, string controlID)
        {
            Privilege newPrivilege = new Privilege();
            newPrivilege.PrivilegeAccess = ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN;
            newPrivilege.PrivilegeAccessValue = controlID;
            newPrivilege.PrivilegeMaster = ServiceConstant.SYS_MASTER_TYPE_USER;
            newPrivilege.PrivilegeMasterValue = userID;
            newPrivilege.PrivilegeOperation = ServiceConstant.SYS_OPRATION_VALUE_VISIBLE;
            privilegeDao.create(newPrivilege);
        }

    }
}