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
        public String StartTime { get; set; }             //DoorTime的开始时间
        public String EndTime { get; set; }               //DoorTime的结束时间
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