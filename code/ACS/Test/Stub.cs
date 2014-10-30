using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Model;
using ACS.Models.Po.CF;
namespace ACS.Test
{
    public class Stub
    {

        public static List<Privilege> getPrivilegeList()
        {
            List<Privilege> list = new List<Privilege>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(getPrivilege(i));
            }
            return list;
        }

        public static Privilege getPrivilege(int i)
        {
            Privilege p = new Privilege();
            p.PrivilegeID = i;
            p.PrivilegeMaster = "master";
            p.PrivilegeAccess = "access";
            p.PrivilegeAccessValue = "svalue";
            p.PrivilegeMasterValue = "value";
            return p;
        }
       public static UserModel getUser(){
           UserModel u = new UserModel();
           u.UserID = 0;
           u.UserName = "test";
           u.Pswd = "1234";
           return u;
       }
        public static List<User> getUserList()
        {
            List<User> list = new List<User>();
            for (int i = 0; i < 100; i++)
            {
                User t = new User();
                t.UserID = i;
                t.UserName = "User"+i;
                t.Pswd = "1234";
                list.Add(t);
            }
            return list;
        }

        public static List<MenuTreeItem> getTreeItemList()
        {
            List<MenuTreeItem> treeList = new List<MenuTreeItem>();
            for (int i = 0; i < 10; i++)
            {
                MenuTreeItem item = new MenuTreeItem();
                item.Id = "test" + i;
                item.Text = "test" + i;
                treeList.Add(item);
                for (int j = 0; j < 10; j++)
                {
                    MenuTreeItem itemj = new MenuTreeItem();
                    itemj.Id = "testsub" + j;
                    itemj.Text = "testsub" + j;
                    itemj.Pid = "test" + j;
                    treeList.Add(itemj);
                }
            }
            return treeList;            
        }

        public static MenuTreeModel getTestTree()
        {

            List<MenuTreeItem> treeList = new List<MenuTreeItem>();
            ///
            ///加入测试用的菜单项
            ///

            treeList.Add(testItem("基本功能", "app", ""));
            treeList.Add(testItem("设备监控", "Monitor/Monitor", "app"));
            treeList.Add(testItem("刷卡记录", "RecordManage/RecordManage", "app"));
            treeList.Add(testItem("报警记录", "Alarmlog/Alarmlog", "app"));
       
            treeList.Add(testItem("系统管理", "sysmanage", ""));
            treeList.Add(testItem("用户管理", "UserManage/userManage", "sysmanage"));
            treeList.Add(testItem("权限管理", "PrivilegeManage/PrivilegeManage", "sysmanage"));
            treeList.Add(testItem("后台通信", "ComManage/ComManage", "sysmanage"));
            treeList.Add(testItem("日志管理", "LogManage/LogManage", "sysmanage"));

            treeList.Add(testItem("数据配置", "datamanage", ""));
            treeList.Add(testItem("部门管理", "DeptManage/DeptManage", "datamanage"));
            treeList.Add(testItem("门禁规则管理", "AccessManage/AccessManage", "datamanage"));
            treeList.Add(testItem("设备管理", "DeviceManage/DeviceManage", "datamanage"));
            treeList.Add(testItem("假日管理", "HolidayManage/HolidayManage", "datamanage"));
            treeList.Add(testItem("事件类型管理", "EventTypeManage/EventTypeManage", "datamanage"));
            ///
            MenuTreeModel tree = new MenuTreeModel();
            tree.MenuTreeItemList = treeList;
            return tree;
        }

        private static  MenuTreeItem testItem(String name,String url,String father)
        {
            MenuTreeItem item = new MenuTreeItem();
            item.Id = url;
            item.Text =name;
            item.Pid = father;
            return item;
        }
    }
}