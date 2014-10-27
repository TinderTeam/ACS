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
namespace ACS.Service.Impl
{
    public class UserServiceImpl : UserService
    {

        UserDao userDao = DaoContext.getInstance().getUserDao();

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
            userDao.delete(userID);
        }

        public void update(User user)
        {
            userDao.update(user);
        }

    }
}