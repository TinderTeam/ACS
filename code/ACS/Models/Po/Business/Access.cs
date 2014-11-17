using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Access
    {
        public const String ID = "AccessID";
        public const String UserID = "CreateUserID";
        public const String Name = "AccessName";
        private int accessID;//门禁规则
        private String accessName;//规则名称
        private String accessNote;//门禁规则描述
        private String acsString;//备注
        private int createUserID;
        private DateTime createDate;

        public virtual int AccessID
        {
            get { return accessID; }
            set { accessID = value; }
        }

        public virtual String AccessName
        {
            get { return accessName; }
            set { accessName = value; }
        }

        public virtual String AccessNote
        {
            get { return accessNote; }
            set { accessNote = value; }
        }

        public virtual String AcsString
        {
            get { return acsString; }
            set { acsString = value; }
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
    }
}