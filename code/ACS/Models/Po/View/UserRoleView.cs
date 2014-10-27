using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.View
{
    public class UserRoleView
    {
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private int roleID;

        public int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }
    }
}