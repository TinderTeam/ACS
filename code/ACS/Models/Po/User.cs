using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class User
    {
        private int userID;

        public int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        private String userName;

        public String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private String pswd;

        public String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }

    }
}