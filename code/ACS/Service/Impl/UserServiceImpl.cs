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
namespace ACS.Service.Impl
{
    public class UserServiceImpl : UserService
    {

        CommonDao<User> userDao = DaoContext.getInstance().getUserDao();

        public AbstractDataSource<User> getUserList(User filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<User> dataSource = new DatabaseSourceImpl<User>(conditionList);  
		    return dataSource;
        }
        public void create(User user)
        {
            userDao.create(user);
        }

        public void delete(int userID)
        {
            userDao.delete(
                new QueryCondition(
                   ConditionTypeEnum.EQUAL,
                   User.ID,
                   userID.ToString()
                )
            );
        }

        public void update(User user)
        {
            userDao.update(user);
        }

    }
}