using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po;

namespace ACS.Service
{
    public class ModelConventService
    {
      

        public static List<UserModel> toUserModelList(List<User> userList){
            List<UserModel> userModelList = new List<UserModel>();
            foreach (User item in userList)
            {
                userModelList.Add(toUserModel(item));
            }
            return userModelList;
        }

        public static UserModel toUserModel(User user)
        {
            UserModel u = new UserModel();
            u.Username = user.UserName;
            u.Pswd = user.Pswd;
            u.UserID = user.UserID;
            return u;
        }

       

    }
}