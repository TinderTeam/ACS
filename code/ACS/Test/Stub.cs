using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Model;
namespace ACS.Test
{
    public class Stub
    {
       
        public static List<User> getUserList()
        {
            List<User> list = new List<User>();
            for (int i = 0; i < 5; i++)
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
    }
}