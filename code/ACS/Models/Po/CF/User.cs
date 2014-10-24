using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class User
    {
        private int userID;//角色ID

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private String userName;//角色名称

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private String userDesc;//角色描述

        public String UserDesc
        {
            get { return userDesc; }
            set { userDesc = value; }
        }
        private String pswd;//用户密码

        public String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }
        private int createUserID;//创建用户ID

        public int CreateUserID
        {
            get { return createUserID; }
            set { createUserID = value; }
        }
        private DateTime createDate;//创建时间

        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }
        private int modifyUserID;//最后编辑用户

        public int ModifyUserID
        {
            get { return modifyUserID; }
            set { modifyUserID = value; }
        }
        private DateTime modifyDate;//最后编辑时间

        public DateTime ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }


    }
}