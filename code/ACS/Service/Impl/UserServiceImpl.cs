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
namespace ACS.Service.Impl
{
    public class UserServiceImpl : UserService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<User> userDao = DaoContext.getInstance().getUserDao();

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

    }
}