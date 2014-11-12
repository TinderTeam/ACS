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
namespace ACS.Service.Impl
{
    public class UserServiceImpl : UserService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<User> userDao = DaoContext.getInstance().getUserDao();
        CommonDao<Privilege> privilegeDao = DaoContext.getInstance().getPrivilegeDao();
        CommonDao<Sys_Menu> sysMenuDao = DaoContext.getInstance().getSysMenuDao();
        CommonDao<Control> controlDao = DaoContext.getInstance().getControlDao();
        CommonDao<Door> doorDao = DaoContext.getInstance().getDoorDao();
        public AbstractDataSource<User> getUserList(User filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<User> dataSource = new DatabaseSourceImpl<User>(conditionList);  
		    return dataSource;
        }
        //创建新用户
        public void create(UserModel userModel)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserName", userModel.UserName);
            if (null != userDao.getUniRecord(condition))
            {
                log.Error("create failed, the userName has exist. user name is " + userModel.UserName);
                throw new SystemException(ExceptionMsg.USERNAME_EXIST);
            }
            User user = ModelConventService.toUser(userModel);
            user.CreateDate = DateTime.Now;
            user.ModifyDate = DateTime.Now;
            userDao.create(user);
        }
        public void delete(List<int> userIDList)
        {
            foreach (int i in userIDList)
            {
                userDao.delete(
               new QueryCondition(
                  ConditionTypeEnum.EQUAL,
                  User.ID,
                  i.ToString()
               )
           );
            }
           
        }
        public UserModel getUserByID(string userID)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserID", userID);
            User user = userDao.getUniRecord(condition);
            if (null == user)
            {
                log.Error("get user failed, the user is not exist. userID is " + userID);
                throw new SystemException(ExceptionMsg.USER_NOT_EXISTED);
            }
            UserModel userModel = ModelConventService.toUserModel(user);
            return userModel;
        }
        public void update(UserModel userModel)
        {
            //判断用户是否存在
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserID", userModel.UserID.ToString());
            User orignalUser = userDao.getUniRecord(condition);
            if (null == orignalUser)
            {
                log.Error("modify user failed, the user is not exist. userID is " + userModel.UserID);
                throw new SystemException(ExceptionMsg.USER_NOT_EXISTED);
            }
            User user = ModelConventService.toUser(userModel);
            user.UserID = orignalUser.UserID;
            user.Pswd = orignalUser.Pswd;
            user.UserName = orignalUser.UserName;
            user.CreateDate = orignalUser.CreateDate;
            user.ModifyDate = DateTime.Now;
            userDao.update(user);
        }
        //用户权限管理
        //获取用户菜单权限树
        public TreeModel getPrivilegeMenuTree(string userID)
        {
            //获取所有权限树列表
            List<Sys_Menu> menuList = sysMenuDao.getAll();
            TreeModel allTree = ModelConventService.toMenuTreeModel(menuList);
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_APP));
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            //对用户存在的权限菜单打勾
            foreach (Privilege i in userPrivilegeList)
            {
                foreach (TreeItem j in allTree.MenuTreeItemList)
                {
                    if (i.PrivilegeAccessValue.Equals(j.Id))
                    {
                        j.CheckNode = true;
                    }
                }
            }
            return allTree;
        }
        //更改用户菜单权限
        public void updateMenuPrivilege(string userID,List<string> menuIDList)
        {
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_APP));
            privilegeDao.delete(conditionList); //删除原有菜单权限列表

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

            privilegeDao.create(privilegeList);

        }
        //用户权限管理
        //获取用户设备权限树
        public TreeModel getDevicePrivilegeTree(string userID)
        {
            
            //获取所有设备树列表
            List<Control> controlList = controlDao.getAll();
            List<Door> doorList = doorDao.getAll();

            TreeModel allTree = ModelConventService.toDeviceTreeModel(controlList,doorList);
            
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            List<Privilege> userPrivilegeList = privilegeDao.getAll(conditionList);
            //对用户存在的权限菜单打勾
            foreach (Privilege i in userPrivilegeList)
            {
                foreach (TreeItem j in allTree.MenuTreeItemList)
                {
                    if (i.PrivilegeAccessValue.Equals(j.Id))
                    {
                        j.CheckNode = true;
                    }
                }
            }
            return allTree;
        }
        //更改用户设备权限
        public void updateDevicePrivilege(string userID, List<string> deviceIDList)
        {
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            privilegeDao.delete(conditionList); //删除原有菜单权限列表

            if (ValidatorUtil.isEmpty<string>(deviceIDList))
            {
                log.Warn("selected deviceIDList is empty,userID is " + userID);
                return;
            }

            List<Privilege> privilegeList = new List<Privilege>();

            //新获取到的用户菜单权限加入权限列表
            foreach (string i in deviceIDList)
            {
                Privilege privilege = new Privilege();
                privilege.PrivilegeMaster = ServiceConstant.SYS_MASTER_TYPE_USER;
                privilege.PrivilegeMasterValue = userID;
                privilege.PrivilegeAccess = ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN;
                privilege.PrivilegeAccessValue = i;
                privilege.PrivilegeOperation = ServiceConstant.SYS_OPRATION_VALUE_VISIBLE;

                privilegeList.Add(privilege);
            }

            privilegeDao.create(privilegeList);

        }
    }
}