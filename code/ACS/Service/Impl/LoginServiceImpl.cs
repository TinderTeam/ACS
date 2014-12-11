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

namespace ACS.Service.Impl
{
    public class LoginServiceImpl : CommonServiceImpl<SystemUser>, LoginService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<SystemUser> userDao = DaoContext.getInstance().getUserDao();
        PrivilegeService privilegeService = ServiceContext.getInstance().getPrivilegeService();

        public override String GetPrimaryName()
        {
            return SystemUser.ID;
        }
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserModel Login(string userName, string password)
        {

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserName", userName);
            SystemUser user = userDao.getUniRecord(condition);

            if (null == user)
            {
                log.Error("login failed, the user is not exist. user name is " + userName);
                throw new FuegoException(ExceptionMsg.USERNAME_PASSWORD_WRONG);
            }

            if (null == user.Pswd || !user.Pswd.Equals(password))
            {
                log.Error("login failed, the password is not right. user name is " + userName);
                throw new FuegoException(ExceptionMsg.USERNAME_PASSWORD_WRONG);
            }

            UserModel userModel = new UserModel();
            userModel.UserName = user.UserName;
            userModel.UserID = user.UserID;
            return userModel;
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
                log.Error("old password is wrong. user name is " + userName);
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
            List<String> menuIDList = new List<string>();
            foreach (Privilege i in userPrivilegeList)
            {
                menuIDList.Add(i.PrivilegeAccessValue);
            }
            QueryCondition IDcondition = new QueryCondition(ConditionTypeEnum.IN, Sys_Menu.MEMU_ID, menuIDList);
            List<Sys_Menu> menuList = GetDao<Sys_Menu>().getAll(IDcondition);
            //加入父级按钮
            foreach (Sys_Menu sys_menu in menuList)
            {
                menuIDList.Add(sys_menu.MenuParentNo);
            }
            IDcondition = new QueryCondition(ConditionTypeEnum.IN, Sys_Menu.MEMU_ID, menuIDList);
            menuList = GetDao<Sys_Menu>().getAll(IDcondition);
            return menuList;
        }
    }
}