using ACS.Models.Po.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class AccessDetailModel
    {
        public String Id { get; set; }                      //树节点ID  随机生成
        public String Pid { get; set; }                     //树节点父本ID  随机生成
        public int AccessDetailID { get; set; }          //
        public int AccessID { get; set; }                //权限的父本ID
        public int ValueID { get; set; }                 //权限ID，可以是权限ID也可以是DoorTimeID
        public String Type { get; set; }                    //权限类型
        public int CreateUserID { get; set; }               //创建者ID
        //public DateTime CreateDate { get; set; }            //创建时间
        public String NodeName { get; set; }                //树节点名称，可以是AccessName、ControlName、DoorName、DoorTimeName
        private DateTime startTime = Convert.ToDateTime("2000-01-01 00:00:00");  //开始时间
        private DateTime endTime = Convert.ToDateTime("2000-01-01 00:00:00");    //截止时间
        public bool CheckNode { get; set; }                 //节点是否勾选
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
        public bool Holiday { get; set; }
        public List<AccessDetailModel> Children { get; set; }    //子节点

        public virtual DateTime StartTime
        {
            get { return startTime; }
            set { startTime = value; }
        }

        public virtual DateTime EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }

        public AccessDetailModel()
        {
        }

        public AccessDetailModel(string pid, string id)
        {
            Pid = pid;
            Id = id;
        }
        
    }
}