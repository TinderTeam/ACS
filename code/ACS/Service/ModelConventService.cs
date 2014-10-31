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
            user.CreateUserID = m.CreateUserID;
            user.CreateDate = m.CreateDate;
            user.UserDesc = m.UserDesc;
            user.ModifyUserID = m.ModifyUserID;
            user.ModifyDate = m.ModifyDate;
            //TODO: 实现转化 方法
            return user;
        }  
        /// <summary>
        /// 转化为menuTreemodel
        /// </summary>
        public static TreeModel toMenuTreeModel(List<Sys_Menu> menuList)
        {
            TreeModel menuTreeModle = new TreeModel();
            List<TreeItem> list = new List<TreeItem>();
            Dictionary<String, String> fathermap = new Dictionary<string, string>();
            foreach (Sys_Menu sysMenu in menuList)
            {
                fathermap.Add(sysMenu.MenuID.ToString(), sysMenu.MenuNo);
            }
            foreach (Sys_Menu sysMenu in menuList)
            {
                if (sysMenu.IsVisible.Equals(ServiceConstant.SYS_VISIABLE))
                {
                    //menu visiable 时
                    TreeItem item = new TreeItem();
                    if (sysMenu.IsLeaf != null && sysMenu.IsLeaf.Equals("TRUE"))
                    {
                        item.Id = sysMenu.MenuNo;
                    }
                    else
                    {       
                        item.Id = sysMenu.MenuNo + "/" + sysMenu.MenuNo;
                    }
                    

                    try
                    {
                        if (sysMenu.MenuParentNo != null)
                        {
                            item.Pid = fathermap[sysMenu.MenuParentNo];
                        }
                        else
                        {
                            item.Pid = null;
                        }
                       
                    }
                    catch (KeyNotFoundException ex)
                    {
                        item.Pid = null;
                    }
                   
                    item.Text = sysMenu.MenuName;
                    list.Add(item);
                }        
            }
            menuTreeModle.MenuTreeItemList = list;
            return menuTreeModle;
        }

        public static Employee toEmployee(Employee employee,EmployeeModel employeeModel)
        {
            //TODO: 实现转化 方法
            employee.Birthday = employeeModel.Birthday;
            employee.Car = employeeModel.Car;
            employee.DeptID = employeeModel.DeptID;
            employee.Email = employeeModel.Email;
            employee.EmployeeCode = employeeModel.EmployeeCode;
            employee.EmployeeName = employeeModel.EmployeeName;
            employee.EndDate = employeeModel.EndDate;
            employee.EnglishName = employeeModel.EnglishName;
            employee.Home = employeeModel.Home;
            employee.JobID = employeeModel.JobID;
            employee.LeaveDate = employeeModel.LeaveDate;
            employee.Note1 = employeeModel.Note1;
            employee.Note2 = employeeModel.Note2;
            employee.Note3 = employeeModel.Note3;
            employee.Note4 = employeeModel.Note4;
            employee.PersonCode = employeeModel.PersonCode;
            employee.Phone = employeeModel.Phone;
            employee.Photo = employeeModel.Photo;
            employee.RegDate = employeeModel.RegDate;
            employee.Sex = employeeModel.Sex;
            employee.TimeStamp = employeeModel.TimeStamp;
            employee.TimeStampx = employeeModel.TimeStampx;

            return employee;
        }
        public static EmployeeModel toEmployeeModel(Employee employee)
        {
            //TODO: 实现转化 方法
            EmployeeModel employeeModel = new EmployeeModel();
            employeeModel.EmployeeID = employee.EmployeeID;
            employeeModel.Birthday = employee.Birthday;
            employeeModel.Car = employee.Car;
            employeeModel.DeptID = employee.DeptID;
            employeeModel.Email = employee.Email;
            employeeModel.EmployeeCode = employee.EmployeeCode;
            employeeModel.EmployeeName = employee.EmployeeName;
            employeeModel.EndDate = employee.EndDate;
            employeeModel.EnglishName = employee.EnglishName;
            employeeModel.Home = employee.Home;
            employeeModel.JobID = employee.JobID;
            employeeModel.Note1 = employee.Note1;
            employeeModel.Note2 = employee.Note2;
            employeeModel.Note3 = employee.Note3;
            employeeModel.Note4 = employee.Note4;
            employeeModel.PersonCode = employee.PersonCode;
            employeeModel.Phone = employee.Phone;
            employeeModel.Photo = employee.Photo;
            employeeModel.RegDate = employee.RegDate;
            employeeModel.Sex = employee.Sex;

            return employeeModel;
        }
    }
}