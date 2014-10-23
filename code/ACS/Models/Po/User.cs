using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class User
    {
        private int userID;
        private String userName;
        private String pswd;

        public virtual int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        public virtual String UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        public virtual String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }

    }
}