using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po.CF;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Service.Constant;
using ACS.Common;
using ACS.Models.Po.Sys;
using ACS.Common.Model;

namespace ACS.Service.Impl
{
    public class LoginServiceImpl : CommonServiceImpl<SystemUser>, LoginService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<SystemUser> userDao = DaoContext.getInstance().getUserDao();
        PrivilegeService privilegeService = ServiceContext.getInstance().getPrivilegeService();

        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = SystemUser.ID;
            return perObjInfo;
        }

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public SystemUser Login(SystemUser loginUser)
        {

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, SystemUser.NAME, loginUser.UserName);
            SystemUser orignalUser = userDao.getUniRecord(condition);

            if ((null == orignalUser) || (!orignalUser.Pswd.Equals(loginUser.Pswd)))
            {
                log.Info("login failed, the password is wrong. user name is " + loginUser.UserName);
                throw new FuegoException(ExceptionMsg.USERNAME_PASSWORD_WRONG);
            }
            SystemUser user = new SystemUser();
            user.UserName = orignalUser.UserName;
            user.UserID = orignalUser.UserID;
            return user;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public void ModifyPswd(string userName, string oldPswd, string newPswd)
        {

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserName", userName);
            SystemUser user = userDao.getUniRecord(condition);
            
            if (null == user)
            {
                log.Error("session has out of time,username is " + userName);
                throw new FuegoException(ExceptionMsg.USERNAME_PASSWORD_WRONG);
            }

            if (!user.Pswd.Equals(oldPswd))
            {
                log.Info("old password is wrong. user name is " + userName);
                throw new FuegoException(ExceptionMsg.USERNAME_PASSWORD_WRONG);
            }
            
            user.Pswd = newPswd;
            userDao.update(user);
        }
        public List<Sys_Menu> getSysMenuListByID(int userID)
        {
            //获取所选用户对应的权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.MASTER_VALUE, userID.ToString()));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_APP));
            List<Privilege> userPrivilegeList = GetDao<Privilege>().getAll(conditionList);
            //对用户存在的权限菜单打勾
            Dictionary<String, String> menuIDListMap = new Dictionary<String, String>();
            List<String> orginalmenuIDList = new List<string>();
            foreach (Privilege i in userPrivilegeList)
            {
                if (!menuIDListMap.ContainsKey(i.PrivilegeAccessValue))
                {
                    menuIDListMap.Add(i.PrivilegeAccessValue, i.PrivilegeAccessValue);
                }
            }
            orginalmenuIDList.AddRange(menuIDListMap.Values.ToList<String>());
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.IN, Sys_Menu.MEMU_ID, orginalmenuIDList);
            List<Sys_Menu> menuList = GetDao<Sys_Menu>().getAll(IDcondition);
            //加入父级按钮
            List<String> menuIDList = new List<string>();
            foreach (Sys_Menu sys_menu in menuList)
            {
                if (!sys_menu.MenuParentNo.Equals("0"))
                {
                    if (!menuIDListMap.ContainsKey(sys_menu.MenuParentNo))
                    {
                        menuIDListMap.Add(sys_menu.MenuParentNo, sys_menu.MenuParentNo);
                    }
                }
                
            }
            menuIDList.AddRange(menuIDListMap.Values.ToList<String>());
            QueryCondition menuIDcondition = new QueryCondition(ConditionTypeEnum.IN, Sys_Menu.MEMU_ID, menuIDList);
            menuList = GetDao<Sys_Menu>().getAll(menuIDcondition);
            return menuList;
        }
    }
}