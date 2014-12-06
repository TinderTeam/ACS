using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class TreeModel
    {
        public String Id { get; set; }              //节点ID
        public String Pid { get; set; }             //父节点ID
        public String MenuName { get; set; }        //节点名称
        public bool CheckNode { get; set; }         //是否勾选
        public String Url { get; set; }             //链接地址
    }
}