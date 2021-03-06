﻿using ACS.Common;
using ACS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class SystemUser : LogOperable
    {
        public const String ID = "UserID";
        public const String NAME = "UserName";

        private int userID;//用户ID
        private String userName;//用户名称
        private String userDesc;//用户描述
        private String pswd;//用户密码
        private int createUserID;//创建用户ID
        private DateTime createDate;//创建时间
        private int modifyUserID;//最后编辑用户
        private DateTime modifyDate;//最后编辑时间

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

        public virtual String UserDesc
        {
            get { return userDesc; }
            set { userDesc = value; }
        }

        public virtual String Pswd
        {
            get { return pswd; }
            set { pswd = value; }
        }

        public virtual int CreateUserID
        {
            get { return createUserID; }
            set { createUserID = value; }
        }

        public virtual DateTime CreateDate
        {
            get { return createDate; }
            set { createDate = value; }
        }

        public virtual int ModifyUserID
        {
            get { return modifyUserID; }
            set { modifyUserID = value; }
        }

        public virtual DateTime ModifyDate
        {
            get { return modifyDate; }
            set { modifyDate = value; }
        }



        public virtual string GetObjType()
        {
            return ServiceConstant.LOG_OBJ_SYSTEMUSER;
        }

        public virtual string GetObjName()
        {
            return this.userName;
        }

        public virtual string GetDesp()
        {
            return null;
        }
    }
}