using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Models.Po.CF;
using ACS.Service.Constant;
using ACS.Common.Util;
using ACS.Models.Po.Sys;
using ACS.Models.Po.Business;
using ACS.Common;
using ACS.Common.Model;
namespace ACS.Service.Impl
{
    public class UserServiceImpl : CommonServiceImpl<SystemUser>, UserService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = SystemUser.ID;
            return perObjInfo;
        }

        public override void Validator(SystemUser user)
        {
            log.Debug("the validator is empty ");
            QueryCondition IDCondition = new QueryCondition(ConditionTypeEnum.EQUAL,SystemUser.NAME,user.UserName);
            SystemUser oldUser = GetDao<SystemUser>().getUniRecord(IDCondition);
            if((null != oldUser)&&(oldUser.UserID != user.UserID))
            {
                log.Error("create failed, the user name has exist. user name is " + user.UserName);
                throw new FuegoException(ExceptionMsg.USER_EXISTED);
            }
        }
        //创建新用户
        public override void Create(int createUserID, SystemUser user)
        {
            SystemUser newUser = new SystemUser();
            newUser.UserName = user.UserName;
            newUser.CreateDate = DateTime.Now;
            newUser.CreateUserID = createUserID;
            newUser.ModifyDate = DateTime.Now;
            newUser.ModifyUserID = createUserID;
            newUser.UserDesc = user.UserDesc;
            newUser.Pswd = "888888";            //创建用户默认密码为6个8
            base.Create(createUserID,newUser);
        }
        //编辑用户信息
        public override void Modify(int modifyUserID,SystemUser user)
        {

            SystemUser orignalUser = this.Get(user.UserID.ToString());
            orignalUser.UserName = user.UserName;
            orignalUser.UserDesc = user.UserDesc;
            orignalUser.ModifyDate = DateTime.Now;
            orignalUser.ModifyUserID = modifyUserID;
            base.Modify(modifyUserID,orignalUser);

        }
        //删除用户信息
        public override void Delete(int userID, List<String> userIDList)
        {
            foreach (String ID in userIDList)
            {
                if (ID.Equals(userID.ToString()))
                {
                    log.Debug("Delete user failed, can't delete youself");
                    throw new FuegoException(ExceptionMsg.CANT_DELETE_YOURSELF);
                }
            }
            if (!ValidatorUtil.isEmpty(userIDList))
            {
                //删除该用户拥有的菜单权限和设备权限
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, Privilege.MASTER_VALUE, userIDList);
                GetDao<Privilege>().delete(condition);
                //删除该用户
                base.Delete(userID, userIDList);
            }
            else
            {
                log.Warn("Delete user Fail, userIDList is empty");
            }
            

        }
        //用户权限管理
        //获取用户菜单权限树
        public List<TreeModel> getMenuPrivilegeTree(string userID)
        {
            //获取所有权限树列表
            List<Sys_Menu> menuList = GetDao<Sys_Menu>().getAll();
            List<TreeModel> menuTreeList = ModelConventService.toMenuTreeModelList(menuList);
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_APP));
            List<Privilege> userPrivilegeList = GetDao<Privilege>().getAll(conditionList);
            //对用户存在的权限菜单打勾
            foreach (Privilege i in userPrivilegeList)
            {
                foreach (TreeModel j in menuTreeList)
                {
                    if (i.PrivilegeAccessValue.Equals(j.Id))
                    {
                        j.CheckNode = true;
                    }
                }
            }
            return menuTreeList;
        }
        //更改用户菜单权限
        public void saveMenuPrivilege(string userID,List<string> menuIDList)
        {
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_APP));
            GetDao<Privilege>().delete(conditionList); //删除原有菜单权限列表

            if (ValidatorUtil.isEmpty<string>(menuIDList))
            {
                log.Warn("selected menuList is empty,userID is " + userID);
                return;
            }

            List<Privilege> privilegeList = new List<Privilege>();
            
            //新获取到的用户菜单权限加入权限列表
            foreach (string i in menuIDList)
            {
                Privilege privilege = new Privilege();
                privilege.PrivilegeMaster = ServiceConstant.SYS_MASTER_TYPE_USER;
                privilege.PrivilegeMasterValue = userID;
                privilege.PrivilegeAccess = ServiceConstant.SYS_ACCESS_TYPE_APP;
                privilege.PrivilegeAccessValue = i;
                privilege.PrivilegeOperation = ServiceConstant.SYS_OPRATION_VALUE_VISIBLE;

                privilegeList.Add(privilege);
            }

            GetDao<Privilege>().create(privilegeList);

        }
        //用户权限管理
        //获取用户设备权限树（全部+勾选）
        public List<TreeModel> getDevicePrivilegeTree(string userID)
        {
            //获取所有权限树列表
            List<Control> controlList = GetDao<Control>().getAll();
            List<TreeModel> DeviceTreeList = ModelConventService.toDeviceTreeModel(controlList);        
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            List<Privilege> userPrivilegeList =GetDao<Privilege>().getAll(conditionList);

            //对用户存在的权限菜单打勾
            foreach (TreeModel j in DeviceTreeList)
            {
                foreach (Privilege i in userPrivilegeList)
                {
                    if ((i.PrivilegeAccessValue).Equals(j.Id))
                    {
                        j.CheckNode = true;
                       
                    }
                }
            }
            return DeviceTreeList;
        }

        ////更改用户设备权限
        public void saveDevicePrivilege(string userID, List<string> deviceIDList)
        {
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            GetDao<Privilege>().delete(conditionList); //删除原有菜单权限列表

            if (ValidatorUtil.isEmpty<string>(deviceIDList))
            {
                log.Warn("selected deviceIDList is empty,userID is " + userID);
                return;
            }

            List<Privilege> privilegeList = new List<Privilege>();

            //新获取到的用户菜单权限加入权限列表
            foreach (string str in deviceIDList)
            {
                Privilege privilege = new Privilege();
                privilege.PrivilegeMaster = ServiceConstant.SYS_MASTER_TYPE_USER;
                privilege.PrivilegeMasterValue = userID;
                privilege.PrivilegeAccess = ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN;
                privilege.PrivilegeAccessValue = str;
                privilege.PrivilegeOperation = ServiceConstant.SYS_OPRATION_VALUE_VISIBLE;

                privilegeList.Add(privilege);
            }

            GetDao<Privilege>().create(privilegeList);

        }
    }
}