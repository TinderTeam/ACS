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

namespace ACS.Service.Impl
{
    public class LoginServiceImpl : LoginService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<User> userDao = DaoContext.getInstance().getUserDao();

        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public UserModel Login(string userName, string password)
        {

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "UserName", userName);
            User user = userDao.getUniRecord(condition);

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
            User user = userDao.getUniRecord(condition);
            
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

    }
}