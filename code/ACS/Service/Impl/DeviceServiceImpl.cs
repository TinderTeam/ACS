using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service.Constant;
using System.Xml;
using ACS.Models.Po.CF;
using ACS.Common.Util;
using ACS.Common;
using ACS.Service.device;
using System.Timers;
namespace ACS.Service.Impl
{
    public class DeviceServiceImpl : CommonServiceImpl<Control>, DeviceService

    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        PrivilegeService privilegeService = ServiceContext.getInstance().getPrivilegeService();

        public override String GetPrimaryName()
        {
            return Control.CONTROL_ID;
        }
        //根据用户ID，获取设备树列表
        public List<TreeModel> getDeviceTreeByID(String userID)
        {
            List<TreeModel> treeModelList = new List<TreeModel>();
            List<String> controlIDList = privilegeService.getPrivilegeValueList(userID, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN);
            QueryCondition IDListCondition = new QueryCondition(ConditionTypeEnum.IN, Control.CONTROL_ID, controlIDList);
            List<Control> controlList = GetDao<Control>().getAll(IDListCondition);
            
            //生成根节点
            TreeModel rootTreeModel = new TreeModel();
            rootTreeModel.Id = Control.CONTROL_TYPE + Control.SPLIT + "0";
            rootTreeModel.Pid = "ROOT0";
            rootTreeModel.MenuName = "所有控制器";
            treeModelList.Add(rootTreeModel);

            foreach (Control control in controlList)
            {
                TreeModel treeModel = new TreeModel();
                treeModel.Id = Control.CONTROL_TYPE + Control.SPLIT + control.ControlID.ToString();
                treeModel.Pid = Control.CONTROL_TYPE + Control.SPLIT + "0";
                treeModel.MenuName = control.ControlName;
                treeModelList.Add(treeModel);
            }
            QueryCondition controlIDListCondition = new QueryCondition(ConditionTypeEnum.IN, Door.CONTROL_ID, controlIDList);
            List<Door> doorList = GetDao<Door>().getAll(controlIDListCondition);
            foreach (Door door in doorList)
            {
                TreeModel treeModel = new TreeModel();
                treeModel.Id = Control.DOOR_TYPE + Control.SPLIT + door.DoorID.ToString();
                treeModel.Pid = Control.CONTROL_TYPE + Control.SPLIT + door.ControlID.ToString();
                treeModel.MenuName = door.DoorName;
                treeModelList.Add(treeModel);
            }
            return treeModelList;
 
        }

        //新建控制器
        public override void Create(int userID, Control control)
        {
            //创建控制器
            base.Create(userID, control);

            DeviceOperatorFactory.getInstance().getDeviceOperator(control);

            //根据控制器创建门
            DeviceTypeModel deviceType = DeviceTypeCache.GetInstance().GetDeviceType(control.TypeID);
            if (null == deviceType)
            {
                log.Error("Control create Failed,deviceType not exist, the type is " + control.TypeID);
                throw new FuegoException(ExceptionMsg.OPERATE_FAILED);
            }
            List<DoorTime> doorTimeList = new List<DoorTime>();
            //根据控制器创建时间
            for (int i = 0; i < deviceType.DoorNum; i++)
            {

                Door door = new Door();
                door.ControlID = control.ControlID;
                door.DoorName = "Door" + i.ToString();
                door.DoorNum = i;
                GetDao<Door>().create(door);

                for (int j = 0; j < deviceType.TimeNum; j++)
                {
                    DoorTime doorTime = new DoorTime();
                    doorTime.DoorID = door.DoorID;
                    doorTime.DoorTimeName = "Time" + j;
                    doorTime.DoorTimeNum = i;
                    doorTimeList.Add(doorTime);
                }
            }
            GetDao<DoorTime>().create(doorTimeList);
            //增加设备权限
            privilegeService.CreateDomainPrivilege(userID.ToString(), control.ControlID.ToString());
        }
        //删除控制器
        public override void Delete(List<String> idList)
        {
            //删除该控制器
            base.Delete(idList);
            //删除控制器所属的Door\DoorTime及DoorTime类型的门禁权限
            deleteDooTimeByControlId(idList);

            //删除该控制器的域管理权限
            List<QueryCondition> conditionList = new List<QueryCondition>();
            conditionList.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Privilege.ACCESS_TYPE, ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN));
            conditionList.Add(new QueryCondition(ConditionTypeEnum.IN, Privilege.ACCESS_VALUE, idList));
            GetDao<Privilege>().delete(conditionList);

        }
        //根据控制器ID删除Door\DoorTime\DoorTimeAccess
        private void deleteDooTimeByControlId(List<string> idList)
        {
            QueryCondition doorCondition = new QueryCondition(ConditionTypeEnum.IN, Door.CONTROL_ID, idList);
            List<Door> doorList = GetDao<Door>().getAll(doorCondition);
            List<String> doorIDList = new List<String>();
            List<DoorTime> doorTimeList = new List<DoorTime>();
            List<String> doorTimeIDList = new List<String>();
            //获取DoorTime列表
            foreach (Door door in doorList)
            {
                doorIDList.Add(door.DoorID.ToString());
            }
            QueryCondition doorTimeCondition = new QueryCondition(ConditionTypeEnum.IN, DoorTime.DOOR_ID, doorIDList);
            doorTimeList.AddRange(GetDao<DoorTime>().getAll(doorTimeCondition));
            //删除所有DoorTime权限
            foreach (DoorTime doorTime in doorTimeList)
            {
                doorTimeIDList.Add(doorTime.DoorTimeID.ToString());
            }
            List<QueryCondition> doorTimeAccessCondition = new List<QueryCondition>();
            doorTimeAccessCondition.Add(new QueryCondition(ConditionTypeEnum.IN, AccessDetail.VALUE_ID, doorTimeIDList));
            doorTimeAccessCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, AccessDetail.TYPE, AccessDetail.DOORTIME_TYPE));
            GetDao<AccessDetail>().delete(doorTimeAccessCondition);
            //删除所有DoorTime
            GetDao<DoorTime>().delete(doorTimeCondition);
            //删除所有Door
            GetDao<Door>().delete(doorCondition);

        }
        //更新DoorTime信息
        public void ModifyDoorTime(int userID, DoorTime doorTime)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL,DoorTime.DOOR_TIME_ID,doorTime.DoorTimeID.ToString());
            DoorTime orignalDoorTime = GetDao<DoorTime>().getUniRecord(condition);
            if (null == orignalDoorTime)
            {
                log.Error("DoorTime Modify Failed, DoorTime is  not exist, the DoorTimeID is " + doorTime.DoorTimeID);
                throw new FuegoException(ExceptionMsg.OPERATE_FAILED);
            }
            orignalDoorTime.DoorTimeName = doorTime.DoorTimeName;
            orignalDoorTime.Enable = doorTime.Enable;
            orignalDoorTime.StartTime = doorTime.StartTime;
            orignalDoorTime.EndTime = doorTime.EndTime;
            orignalDoorTime.Monday = doorTime.Monday;
            orignalDoorTime.Tuesday = doorTime.Tuesday;
            orignalDoorTime.Wednesday = doorTime.Wednesday;
            orignalDoorTime.Thursday = doorTime.Thursday;
            orignalDoorTime.Friday = doorTime.Friday;
            orignalDoorTime.Saturday = doorTime.Saturday;
            orignalDoorTime.Sunday = doorTime.Sunday;

            Door door = GetDao<Door>().getUniRecord(new QueryCondition(ConditionTypeEnum.EQUAL, Door.DOOR_ID, orignalDoorTime.DoorID.ToString()));
            Control control = GetDao<Control>().getUniRecord( new QueryCondition(ConditionTypeEnum.EQUAL, Control.CONTROL_ID, door.ControlID.ToString()));
            DeviceOperatorFactory.getInstance().getDeviceOperator(control).SetDoorTime(door, orignalDoorTime);


            GetDao<DoorTime>().update(orignalDoorTime);
        }
        //更新Door信息
        public void ModifyDoor(int userID, Door door)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Door.DOOR_ID, door.DoorID.ToString());
            Door orignalDoor = GetDao<Door>().getUniRecord(condition);
            if (null == orignalDoor)
            {
                log.Error("Door Modify Failed, Door is  not exist, the DoorID is " + door.DoorID);
                throw new FuegoException(ExceptionMsg.OPERATE_FAILED);
            }
            orignalDoor.DoorName = door.DoorName;
            orignalDoor.OpenTime = door.OpenTime;
            orignalDoor.CloseOutTime = door.CloseOutTime;
            GetDao<Door>().update(orignalDoor);
        }
 
        

      

        public void StartMonitorAll()
        {
            log.Info("now start conncet all device");
            List<Control> controlList = GetDao().getAll();
            foreach (Control control in controlList)
            {
                try
                {
                    DeviceOperatorFactory.getInstance().getDeviceOperator(control);
                    control.Online = true;
                }
                catch (Exception e)
                {
                    log.Error("the control connect failed when started server",e);
                    control.Online = false;
                }
            }
            GetDao().update(controlList);

            log.Info("start the time to check device online or not ");
            Timer timer = new System.Timers.Timer(OnlineDeviceCache.TIME_OUT);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(checkOnline);
            timer.AutoReset = true;
            timer.Start();
        }

        public void OnlineStatus(Control control,bool status)
        {
            control.Online = status;
            GetDao().update(control);
        }

        private void checkOnline(object sender, System.Timers.ElapsedEventArgs e)
        {
            DeviceOperatorFactory.getInstance().CheckOnline();
        }

        public void StartMonitor(List<String> idList)
        {
            List<Control> controlList = base.Get(idList);
            foreach (Control control in controlList)
            {
                log.Info("start monitor contorl,contorl ip is " + control.Ip);
                DeviceOperatorFactory.getInstance().getDeviceOperator(control);
            }
        }

 
        public void OperateDevice(OperateDeviceCmdEnum cmdCode,String doorID)
        {

            Door door = Get<Door>(Door.DOOR_ID, doorID);
            if (null == door)
            {
                log.Error("can not find the door by id, the door id is " + doorID);
                throw new FuegoException(ExceptionMsg.DOOR_NOT_EXIST);
            }
            Control control = Get(door.ControlID.ToString());
            if (null == control)
            {
                log.Error("can not find the control by id, the control id is " + door.ControlID.ToString());
                throw new FuegoException(ExceptionMsg.CONTROL_NOT_EXIST);
            }
            DeviceOperator deviceOperator = DeviceOperatorFactory.getInstance().getDeviceOperator(control);
            deviceOperator.Operate(cmdCode,door);
        }
 
 
    }
}