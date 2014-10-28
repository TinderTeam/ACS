using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class UserRoleView
    {
        private int userID;       
        private int roleID;

        public const String USER_ID = "UserID";   //封装后的方法名
        public const String Role_ID = "Role_ID";   //封装后的方法名

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }
        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
    }
}