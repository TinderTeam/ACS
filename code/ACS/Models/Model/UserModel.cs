using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class UserModel
    {
        private String userName;
        private String pswd;
        private int userID;

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
        public String Username
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