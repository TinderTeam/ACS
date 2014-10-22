using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
namespace ACS.Service.Impl
{
    public class UserServiceImpl : UserService
    {

        UserDao userDao = DaoContext.getInstance().getUserDao();
       
        public List<UserModel>  getAllUsers(){
            return ModelConventService.toUserModelList(userDao.getAll());
        }


    }
}