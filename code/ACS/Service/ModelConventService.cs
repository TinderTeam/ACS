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
          
        public static List<UserModel> toUserModelList(List<SystemUser> userList){
            List<UserModel> userModelList = new List<UserModel>();
            foreach (SystemUser item in userList)
            {
                userModelList.Add(toUserModel(item));
            }
            return userModelList;
        }

        public static UserModel toUserModel(SystemUser user)
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
        public static SystemUser toUser(UserModel m)
        {
            SystemUser user = new SystemUser();
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
 
        // <summary>
        // 转化为menuTreemodel
        // </summary>
        public static List<TreeModel> toMenuTreeModelList(List<Sys_Menu> sysMenuList)
        {
            //sysMenu转换为menuTree
            List<TreeModel> menuTreeList = new List<TreeModel>();
            foreach (Sys_Menu sysMenu in sysMenuList)
            {
                TreeModel menuTree = new TreeModel();
                menuTree.Id = sysMenu.MenuID.ToString();
                menuTree.Pid = sysMenu.MenuParentNo;
                menuTree.MenuName = sysMenu.MenuName;
                menuTree.Url = sysMenu.MenuURL;
                menuTreeList.Add(menuTree);
            }
            return menuTreeList;
        }

        //用户管理，设备域管理调用
        public static List<TreeModel> toDeviceTreeModel(List<Control> controlList)
        {
            //Control转换为menuTree
            List<TreeModel> menuTreeList = new List<TreeModel>();
            foreach (Control control in controlList)
            {
                TreeModel menuTree = new TreeModel();
                menuTree.Id = control.ControlID.ToString();
                menuTree.MenuName = control.ControlName;
                menuTreeList.Add(menuTree);
            }
            return menuTreeList;
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

        /// <summary>
        /// 根据门获得门序号
        /// </summary>
        /// <param name="door"></param>
        /// <returns></returns>
        internal static byte toDoorIndex(Door door)
        {
            return 1;
        }

        
        internal static AlarmRecord toAlarmRecord(EventMsg eventMsg)
        {
            AlarmRecord r = new AlarmRecord();
            return r;
        }

        internal static EventRecord toEventRecord(EventMsg eventMsg)
        {
            EventRecord r = new EventRecord();
            return r;
        }

        internal static EventModel toEventModel(EventMsg eventMsg)
        {
            throw new NotImplementedException();
        }
    }
}