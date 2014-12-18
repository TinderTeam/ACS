using ACS.Common;
using ACS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AccessDetail : LogOperable
    {
        public const char SPLIT = '_';
        public const String ACCESS_TYPE = "ACCESS";
        public const String DOORTIME_TYPE = "DOORTIME";
        public const String CONTROL_TYPE = "CONTROL";
        public const String DOOR_TYPE = "DOOR";
        public const String ROOT_ACCESS_ID = "ACCESS_0";
        public const String ROOT_ID = "0";
		public const String ACCESS_DETAIL_ID = "AccessDetailID";
        public const String VALUE_ID = "ValueID";
        public const String TYPE = "Type";
        private int accessDetailID; //门禁权限详细信息ID
        private int accessID;       //门禁权限ID
        private String accessName;  //规则名称
        private int valueID;         //门ID
        private string type;        //类型 Access/DoorTime
        private int createUserID;
        private DateTime createDate;

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

        public virtual int AccessDetailID
        {
            get { return accessDetailID; }
            set { accessDetailID = value; }
        }

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

        public virtual int ValueID
        {
            get { return valueID; }
            set { valueID = value; }
        }

        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual string GetObjType()
        {
            return ServiceConstant.LOG_OBJ_ACCESS;
        }

        public virtual string GetObjName()
        {
            return this.accessName;
        }

        public virtual string GetDesp()
        {
            return null;
        }
    }
}