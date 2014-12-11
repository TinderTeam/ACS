using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Model;
using ACS.Models.Po.CF;
using ACS.Models.Po.Business;
namespace ACS.Test
{
    public class Stub
    {

        //public static TreeModel getTestAccessList()
        //{
        //    TreeModel tree = new TreeModel();
        //    List<TreeItem> list = new List<TreeItem>();
        //    for (int i = 1; i < 10; i++)
        //    {
        //        TreeItem item = new TreeItem();
        //        item.Id = i.ToString();
        //        item.Pid = "";
        //        item.Text = "权限"+i;
        //        list.Add(item);
        //    }
        //    tree.MenuTreeItemList = list;
        //    return tree;
        //}


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
           u.UserID = 1;
           u.UserName = "admin";
           u.Pswd = "1234";
           return u;
       }
        public static List<SystemUser> getUserList()
        {
            List<SystemUser> list = new List<SystemUser>();
            for (int i = 0; i < 100; i++)
            {
                SystemUser t = new SystemUser();
                t.UserID = i;
                t.UserName = "User"+i;
                t.Pswd = "1234";
                list.Add(t);
            }
            return list;
        }

        //public static List<TreeItem> getTreeItemList()
        //{
        //    List<TreeItem> treeList = new List<TreeItem>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        TreeItem item = new TreeItem();
        //        item.Id = "test" + i;
        //        item.Text = "test" + i;
        //        treeList.Add(item);
        //        for (int j = 0; j < 10; j++)
        //        {
        //            TreeItem itemj = new TreeItem();
        //            itemj.Id = "testsub" + j;
        //            itemj.Text = "testsub" + j;
        //            itemj.Pid = "test" + j;
        //            treeList.Add(itemj);
        //        }
        //    }
        //    return treeList;            
        //}

        //public static TreeModel getTestTree()
        //{

        //    List<TreeItem> treeList = new List<TreeItem>();
        //    ///
        //    ///加入测试用的菜单项
        //    ///

        //    treeList.Add(testItem("基本功能", "app", ""));
        //    treeList.Add(testItem("设备监控", "Monitor/Monitor", "app"));
        //    treeList.Add(testItem("刷卡记录", "RecordManage/RecordManage", "app"));
        //    treeList.Add(testItem("报警记录", "Alarmlog/Alarmlog", "app"));
       
        //    treeList.Add(testItem("系统管理", "sysmanage", ""));
        //    treeList.Add(testItem("用户管理", "UserManage/userManage", "sysmanage"));
        //    treeList.Add(testItem("权限管理", "PrivilegeManage/PrivilegeManage", "sysmanage"));
        //    treeList.Add(testItem("后台通信", "ComManage/ComManage", "sysmanage"));
        //    treeList.Add(testItem("日志管理", "LogManage/LogManage", "sysmanage"));

        //    treeList.Add(testItem("数据配置", "datamanage", ""));
        //    treeList.Add(testItem("员工管理", "EmployeeManage/EmployeeManage", "datamanage"));
        //    treeList.Add(testItem("部门管理", "DeptManage/DeptManage", "datamanage"));
        //    treeList.Add(testItem("门禁规则管理", "AccessManage/AccessManage", "datamanage"));
        //    treeList.Add(testItem("设备管理", "DeviceManage/DeviceManage", "datamanage"));
        //    treeList.Add(testItem("假日管理", "HolidayManage/HolidayManage", "datamanage"));
        //    treeList.Add(testItem("事件类型管理", "EventTypeManage/EventTypeManage", "datamanage"));
        //    ///
        //    TreeModel tree = new TreeModel();
        //    tree.MenuTreeItemList = treeList;
        //    return tree;
        //}

        private static TreeModel testItem(String name, String url, String father)
        {
            TreeModel item = new TreeModel();
            item.Id = url;
            item.MenuName = name;
            item.Pid = father;
            return item;
        }


        public static List<TreeModel> getTestDeviceTree()
        {
            List<TreeModel> treeList = new List<TreeModel>();
            treeList.Add(testItem("设备列表", "device", ""));
            for (int i = 0; i < 10; i++)
            {
                treeList.Add(testItem("控制器" + i, "d-" + i, "device"));
            }
            return treeList;
        }
        //public static TreeModel getTestDoorTree()
        //{
        //    List<TreeItem> treeList = new List<TreeItem>();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        treeList.Add(testItem("门" + i, "door-" + i, ""));
        //    }
        //    TreeModel tree = new TreeModel();
        //    tree.MenuTreeItemList = treeList;
        //    return tree;
        //}
        //public static TreeModel getTestContollerDoorTree()
        //{
        //    List<TreeItem> treeList = new List<TreeItem>();
        //    for (int i = 0; i < 4; i++)
        //    {
        //        treeList.Add(testItem("控制器" + i, "ctrl-" + i, ""));
        //        for (int j = 0; j < 4; j++)
        //        {
        //            treeList.Add(testItem("门" + j, "door-" + j, "ctrl-"+i));
        //        }
        //    }       
        //    TreeModel tree = new TreeModel();
        //    tree.MenuTreeItemList = treeList;
        //    return tree;
        //}

        internal static DeviceModel getDevice()
        {
            DeviceModel d = new DeviceModel();
            d.Control = getControl();
            d.DoorList = getDoorList();
            return d;

        }

        private static List<Door> getDoorList()
        {
            List<Door> dLsit = new List<Door>();
            for (int i = 0; i < 8; i++)
            {
                Door d = new Door();
                d.DoorID = 1;
                dLsit.Add(d);
            }
            return dLsit;
        }

        private static Control getControl()
        {
            Control c = new Control();
            c.ControlID = 1;
            c.ControlName = "CName";
            return c;
        }

        internal static DoorModel getDoor()
        {
            DoorModel d = new DoorModel();
            d.Door = getDoorList()[1];
            d.DoorTimeList = getDoorTiemList();
            return d;
        }

        private static List<DoorTime> getDoorTiemList()
        {
            List<DoorTime> list = new List<DoorTime>();

            for (int i = 0; i < 8; i++)
            {
                DoorTime d = new DoorTime();
                list.Add(d);
            }
            return list;
        }

        internal static List<AlarmRecordView> getAlarmEventList()
        {
            List<AlarmRecordView> list = new List<AlarmRecordView>();
            for (int i = 0; i < 1; i++)
            {
                AlarmRecordView v = new AlarmRecordView();
                v.AlarmID = i+1;
                v.AlarmTime = new DateTime();
                v.ControlID = 1;
                v.ControlName = "测试名称";
                v.DoorID = 9;
                v.DoorName = "门";
                v.AlarmType = "火警";
                list.Add(v);
            }
            return list;
        }

        internal static List<EventRecordView> getEventEventList()
        {
            List<EventRecordView> list = new List<EventRecordView>();
            for (int i = 0; i < 1; i++)
            {
                EventRecordView v = new EventRecordView();
                v.EventID = i + 1;
                v.EventTime = new DateTime();
                v.ControlID = 1;
                v.ControlName = "测试名称";
                v.DoorID = 9;
                v.DoorName = "门";
                v.EventTypeID = 1;
                v.CardNo = "1234567";
                v.EmployeeName = "萌妹子";
                list.Add(v);
            }
            return list;
        }
    }
}