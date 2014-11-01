using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class Privilege
    {
        private int privilegeID;//权限ID
        private String privilegeMaster;//权限主体类型
        private String privilegeMasterValue;//权限主体名称
        private String privilegeAccess;//权限类型
        private String privilegeAccessValue;//权限名称
        private String privilegeOperation;//操作类型

        public const String MASTER_VALUE = "PrivilegeMasterValue";
        public const String MASTER = "PrivilegeMaster";
        public virtual int PrivilegeID
        {
            get { return privilegeID; }
            set { privilegeID = value; }
        }

        public virtual String PrivilegeMaster
        {
            get { return privilegeMaster; }
            set { privilegeMaster = value; }
        }

        public virtual String PrivilegeMasterValue
        {
            get { return privilegeMasterValue; }
            set { privilegeMasterValue = value; }
        }

        public virtual String PrivilegeAccess
        {
            get { return privilegeAccess; }
            set { privilegeAccess = value; }
        }

        public virtual String PrivilegeAccessValue
        {
            get { return privilegeAccessValue; }
            set { privilegeAccessValue = value; }
        }

        public virtual String PrivilegeOperation
        {
            get { return privilegeOperation; }
            set { privilegeOperation = value; }
        }

    }
}