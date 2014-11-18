using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AccessDetail
    {
        private int accessDetailID; //门禁权限详细信息ID
        private int accessID;       //门禁权限ID
        private int valueID;         //门ID
        private string type;        //类型 Access/DoorTime

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
    }
}