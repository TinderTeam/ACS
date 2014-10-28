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
            MenuTreeItem item = new MenuTreeItem();
            item.Id = "test";
            item.Text = "测试";
            treeList.Add(item);
            ///
            ///加入测试用的菜单项
            ///
            treeList.Add(testItem("UserManage/userManage"));
            ///
            MenuTreeModel tree = new MenuTreeModel();
            tree.MenuTreeItemList = treeList;
            return tree;
        }

        private static  MenuTreeItem testItem(String name)
        {
            MenuTreeItem item = new MenuTreeItem();
            item.Id = name;
            item.Text = "测试-" + name;
            item.Pid = "test";
            return item;
        }
    }
}