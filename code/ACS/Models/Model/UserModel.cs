using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class UserModel
    {
        private String userName;
        private String userDesc;
        private String pswd;
        private int userID;
        private int createUserID;
        private DateTime createDate = DateTime.Now;
        private int modifyUserID;
        private DateTime modifyDate = DateTime.Now;

        public String UserDesc
        {
            get { return userDesc; }
            set { userDesc = value; }
        }
        public DateTime ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }

        public int ModifyUserID
        {
            get { return modifyUserID; }
            set { modifyUserID = value; }
        }
        public DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public int CreateUserID
        {
            get { return createUserID; }
            set { createUserID = value; }
        }

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }

        public String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }
        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public String[] toArray()
        {
            String[] array = { userID.ToString(), userName, pswd };
            return array;
        }
    }
}