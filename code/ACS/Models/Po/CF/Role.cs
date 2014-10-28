using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class Role
    {
        private int roleID;//角色ID
        private String roleName;//角色名称
        private String roleDesc;//角色描述
        private int createUserID;//创建用户ID
        private DateTime createDate;//创建时间
        private int modifyUserID;//最后编辑用户
        private DateTime modifyDate;//最后编辑时间

        public virtual int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
        }

        public virtual String RoleName
        {
            get { return roleName; }
            set { roleName = value; }
        }

        public virtual String RoleDesc
        {
            get { return roleDesc; }
            set { roleDesc = value; }
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

      

    }
}