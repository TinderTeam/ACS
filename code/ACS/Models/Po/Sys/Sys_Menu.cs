using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Sys
{
    public class Sys_Menu
    {
        private int menuID;                     //菜单ID  
        private String menuNo;                  //菜单编号  
        private String applicationCode;         //应用编号      
        private String menuParentNo;            //父菜单编号     
        private String menuName;                //菜单名称      
        private String menuIcon;                //菜单按钮       
        private String isVisible;               //显示        
        private String isLeaf;                  //是否有子节点
        private String menuURL;                 //菜单链接地址

        public const string APP_ID = "ApplicationCode";
        public const string MEMU_ID = "MenuID";
        public virtual String MenuName
        {
            get { return menuName; }
            set { menuName = value; }
        }
        public virtual int MenuID
        {
            get { return menuID; }
            set { menuID = value; }
        }
        public virtual String IsLeaf
        {
            get { return isLeaf; }
            set { isLeaf = value; }
        }
        public virtual String IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }
        public virtual String MenuIcon
        {
            get { return menuIcon; }
            set { menuIcon = value; }
        }
        public virtual String MenuNo
        {
            get { return menuNo; }
            set { menuNo = value; }
        }
        public virtual String ApplicationCode
        {
            get { return applicationCode; }
            set { applicationCode = value; }
        }
        public virtual String MenuParentNo
        {
            get { return menuParentNo; }
            set { menuParentNo = value; }
        }

        public virtual String MenuURL
        {
            get { return menuURL; }
            set { menuURL = value; }
        }

    }
}