using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Sys
{
    public class Sys_Menu
    {
        private int menuID;//菜单ID  
        private String menuNo;//菜单编号  
        private String applicationCode;//应用编号      
        private String menuParentNo;//父菜单编号     
        private String menuName;//菜单名称      
        private String menuIcon;//菜单按钮       
        private String isVisble;//显示        
        private String isLeaf;//是否有子节点


        public String MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }
        public int MenuID
        {
            get { return menuID; }
            set { menuID = value; }
        }
        public String IsLeaf
        {
            get { return isLeaf; }
            set { isLeaf = value; }
        }
        public String IsVisble
        {
            get { return isVisble; }
            set { isVisble = value; }
        }
        public String MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value; }
        }
        public String MenuNo
        {
            get { return menuNo; }
            set { menuNo = value; }
        }
        public String ApplicationCode
        {
            get { return applicationCode; }
            set { applicationCode = value; }
        }
        public String MenuParentNo
        {
            get { return menuParentNo; }
            set { menuParentNo = value; }
        }
    }
}