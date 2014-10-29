using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Models.Po.CF;
using ACS.Models.Po.Sys;
namespace ACS.Service
{
    public class ModelConventService
    {
        public static List<int> toIDList(String str)
        {
            List<int> list = new List<int>();
            foreach(String  i in str.Split(','))
            {
                list.Add(Convert.ToInt16(i));
            }
            return list;

        }
          
        public static List<UserModel> toUserModelList(List<User> userList){
            List<UserModel> userModelList = new List<UserModel>();
            foreach (User item in userList)
            {
                userModelList.Add(toUserModel(item));
            }
            return userModelList;
        }

        public static UserModel toUserModel(User user)
        {
            UserModel u = new UserModel();
            u.UserName = user.UserName;
            u.Pswd = user.Pswd;
            u.UserID = user.UserID;
            u.CreateUserID = user.CreateUserID;
            u.CreateDate = user.CreateDate;
            u.ModifyUserID = user.ModifyUserID;
            u.ModifyDate = user.ModifyDate;
            u.UserDesc = user.UserDesc;
            return u;
        }
        public static User toUser(UserModel m)
        {
            User user = new User();
            user.UserName = m.UserName;
            user.Pswd = m.Pswd;
            //TODO: 实现转化 方法
            return user;
        }  
        /// <summary>
        /// 转化为menuTreemodel
        /// </summary>
        public static MenuTreeModel toMenuTreeModel(List<Sys_Menu> menuList)
        {
            MenuTreeModel menuTreeModle = new MenuTreeModel();
            List<MenuTreeItem> list = new List<MenuTreeItem>();
            foreach (Sys_Menu sysMenu in menuList)
            {
                if (sysMenu.IsVisble.Equals(ServiceConstant.SYS_VISIABLE))
                {
                    //menu visiable 时
                    MenuTreeItem item = new MenuTreeItem();
                    item.Id = sysMenu.MenuID.ToString();
                    item.Pid = sysMenu.MenuParentNo;
                    item.Text = sysMenu.MenuName;
                    list.Add(item);
                }        
            }
            menuTreeModle.MenuTreeItemList = list;
            return menuTreeModle;
        }

        public static Employee toEmployee(EmployeeModel employeeModel)
        {
            return null;
        }
    }
}