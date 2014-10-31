using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class UserRole
    {
        private int userRoleID;//角色关系ID
        private int userID;//用户ID
        private int roleID;//角色ID 
        private int createUserID;//创建用户ID
        private DateTime createDate;//创建时间
        private int modifyUserID;//最后编辑用户
        private DateTime modifyDate;//最后编辑时间

        public const String USER_ID = "UserID";   //封装后的方法名
        public const String Role_ID = "RoleID";   //封装后的方法名

        public virtual int UserRoleID
        {
            get { return userRoleID; }
            set { userRoleID = value; }
        }
        
        public virtual int UserID
        {
            get { return userID; }
            set { userID = value; }
        }
        
        public virtual int RoleID
        {
            get { return roleID; }
            set { roleID = value; }
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