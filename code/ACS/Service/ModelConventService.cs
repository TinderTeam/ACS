using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Models.Po.CF;
using ACS.Models.Po.Sys;

using ACS.Common.Util;
using ACS.Common.Dao;
using ACS.Common.Constant;
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

        public static List<string> toIDStrList(String str)
        {
            List<string> list = new List<string>();
            foreach (String i in str.Split(','))
            {
                list.Add(i);
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
        /// 转化为DeptTreemodel
        /// </summary>
        public static TreeModel toDeptTreeModel(List<Dept> deptList)
        {
            TreeModel deptTreeModel = new TreeModel();
            List<TreeItem> list = new List<TreeItem>();

            foreach (Dept dept in deptList)
            {
                TreeItem item = new TreeItem();
                item.Id = dept.DeptID.ToString();
                item.Text = dept.DeptName;
                item.Pid = dept.FatherDeptID;
                list.Add(item);
            }

            deptTreeModel.MenuTreeItemList = list;
            return deptTreeModel;
        }
        //转化为Dept
        public static Dept toDept(Dept dept, DeptModel deptModel)
        {
            dept.DeptName = deptModel.DeptName;
            dept.FatherDeptID = deptModel.FatherDeptID;
            dept.DeptCode = deptModel.DeptCode;
            dept.Note = deptModel.Note;
            return dept;
        }
        //转化为Dept Model
        public static DeptModel toDeptModel(Dept dept, Dept fatherDept)
        {
            DeptModel deptModel = new DeptModel();
            if (dept != null)
            {
                deptModel.DeptID = dept.DeptID;
                deptModel.DeptName = dept.DeptName;
                deptModel.DeptCode = dept.DeptCode;
                deptModel.FatherDeptID = dept.FatherDeptID;
                deptModel.Note = dept.Note;
            }
            if (fatherDept != null)
            {
                deptModel.FatherDeptID = fatherDept.DeptID.ToString();
                deptModel.FatherDeptName = fatherDept.DeptName;
            }

            return deptModel;
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
                  
                    try
                    {
                        if (sysMenu.MenuParentNo != null)
                        {
                            item.Pid = sysMenu.MenuParentNo;
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
                    item.Id = sysMenu.MenuID.ToString();
                    item.Url = sysMenu.MenuURL;
                    item.Text = sysMenu.MenuName;
                    list.Add(item);
                }        
            }
            menuTreeModle.MenuTreeItemList = list;
            return menuTreeModle;
        }
   

        public static TreeModel toUserTreeModel(List<User> userList)
        {
            TreeModel userTreeModle = new TreeModel();
            List<TreeItem> list = new List<TreeItem>();
            foreach (User user in userList)
            {

                TreeItem item = new TreeItem();
                item.Id = user.UserID.ToString();
                item.Text = user.UserName;    
                list.Add(item);
            }
            userTreeModle.MenuTreeItemList = list;
            return userTreeModle;
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
        public static Holiday toHoliday(Holiday holiday, HolidayModel holidayModel)
        {
            //TODO: 实现转化 方法
            holiday.HolidayName = holidayModel.HolidayName;
            holiday.StartTime = holidayModel.StartTime;
            holiday.EndTime = holidayModel.EndTime;
            holiday.HolidayNote = holidayModel.HolidayNote;

            return holiday;
        }
        public static HolidayModel toHolidayModel(Holiday holiday)
        {
            //TODO: 实现转化 方法
            HolidayModel holidayModel = new HolidayModel();
            holidayModel.HolidayID = holiday.HolidayID;
            holidayModel.HolidayName = holiday.HolidayName;
            holidayModel.StartTime = holiday.StartTime;
            holidayModel.EndTime = holiday.EndTime;
            holidayModel.HolidayNote = holiday.HolidayNote;

            return holidayModel;
        }

        /// <summary>
        /// 通过控制器获取控制器-树列表
        /// </summary>
        /// <param name="datasource"></param>
        /// <returns></returns>
        public static TreeModel toDeviceTree(Common.Dao.datasource.AbstractDataSource<Control> datasource)
        {
           //获取所有控制器
            TreeModel tree = new TreeModel();
            List<TreeItem> itemList = new List<TreeItem>();
            List<Control> List = datasource.getAllPageData();

            foreach (Control device in List)
            {
                TreeItem i = new TreeItem();
                i.Id = "C"+device.ControlID.ToString();
                i.Text = device.ControlName;
                i.Pid = "";
                itemList.Add(i);
            }
            //获取所有门
            List<Door> doorList=ServiceContext.getInstance().getDeviceService().getDoorList(null).getAllPageData();
            foreach (Door door in doorList)
            {
                TreeItem i = new TreeItem();
                i.Id = "D"+door.DoorID.ToString();
                i.Text = door.DoorName;
                i.Pid = "C" + door.ControlID.ToString();
                itemList.Add(i);
            }
            tree.MenuTreeItemList = itemList;
            return tree;
        }

        internal static TreeModel toDeviceTreeModel(List<Control> controlList)
        {
            TreeModel tree = new TreeModel();
            List<TreeItem> itemList = new List<TreeItem>();
            foreach (Control device in controlList)
            {
                TreeItem i = new TreeItem();
                i.Id = device.ControlID.ToString();
                i.Text = device.ControlName;
                i.Pid = "";
                itemList.Add(i);
            }
            tree.MenuTreeItemList = itemList;
            return tree;
        }

        internal static List<DoorTime> toDoorTimeList(List<DoorTimeModel> modelList)
        {

            List<DoorTime> list = new List<DoorTime>();
            foreach(DoorTimeModel model in modelList){
                list.Add(toDoorTime(model));
            }
            return list;
        }

        internal static DoorTime toDoorTime(DoorTimeModel model)
        {

            DoorTime doortime = new DoorTime();
            doortime.DoorID = model.DoorID;
            doortime.DoorTimeID = model.DoorTimeID;
            doortime.DoorTimeName = model.DoorTimeName;
            doortime.EndTime = model.EndTime;
            doortime.StartTime = model.StartTime;
            doortime.Enable = model.Enable;
            return doortime;

        }

        internal static Control toControl(ControllerModel m)
        {
            Control control = new Control();
            control.Address = m.Address;
            control.ControlName = m.ControlName;
            control.ControlID = m.ControlID;
            control.Ip = m.Ip;
            return control;
        }

        internal static List<QueryCondition> getAccessDetailIDConditionByViewList(List<AccessDetailView> viewList)
        {
            List<QueryCondition> list = new List<QueryCondition>();
            foreach (AccessDetailView view in viewList)
            {
                QueryCondition condition = new QueryCondition(
                       ConditionTypeEnum.EQUAL,
                           AccessDetail.ACCESS_DETAIL_ID,
                           view.AccessDetailID.ToString()
                   );
                list.Add(condition);
            }
            return list;
        }
    }
}