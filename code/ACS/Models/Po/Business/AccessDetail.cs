using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AccessDetail
    {
        private int accessDetailID;
        private int accessID;       //门禁规则
        private int controlID;      //控制器
        private int doorID;         //门ID
        private string type;        //类型 Access/Device

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

        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }

        public virtual string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}