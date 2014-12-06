using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Dao.datasource;
using ACS.Common.Util;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Test;
using System.Web.Script.Serialization;
using NHibernate.Mapping;
using ACM.Controllers;
using ACS.Common;
using ACS.Service.Constant;
using ACS.Models.Po.CF;
using ACS.Common.Dao;
using ACS.Common.Constant;
namespace ACS.Controllers
{
    public class DeviceManageController : MiniUITableController<Control>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();

        public override CommonService<Control> getService()
        {
            return deviceService;
        }

        // 加载设备-门树
        public override ActionResult LoadTree()
        {
            List<TreeModel> deviceTreeList = deviceService.getDeviceTreeByID(this.getSession().UserID.ToString());
            return ReturnJson(deviceTreeList);
        }
        //加载控制器类型列表
        public ActionResult LoadTypeList()
        {
            List<DeviceTypeModel> typeList = DeviceTypeCache.GetInstance().TypeList;
            return ReturnJson(typeList);
        }
        //根据DoorID加载DoorTimeList
        public ActionResult LoadDoorTimeList(String data)
        {
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            if (ValidatorUtil.isEmpty(data))
            {
                log.Warn("the DoorID is empty");
                return null;
            }
            else
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, DoorTime.DOOR_ID, data));
                return LoadTable<DoorTime>(filterCondition);
            }
        
        }
        //打开门编辑窗口
        public ActionResult DoorPage()
        {
            return View();
        }
        //加载门信息
        public ActionResult ShowDoor(String data)
        {
           return base.Show<Door>(Door.DOOR_ID,data);
        }
        //更新DoorTime编辑后的信息
        public ActionResult ModifyDoor(String data)
        {

            try
            {
                this.getSession();
                Door door = JsonConvert.JsonToObject<Door>(data);
                deviceService.ModifyDoor(this.getSession().UserID, door);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
        //打开时间段编辑窗口
        public ActionResult DoorTimePage()
        {
            return View();
        }
        //加载时间段信息
        public ActionResult ShowDoorTime(String data)
        {
            return base.Show<DoorTime>(DoorTime.DOOR_TIME_ID, data);
        }
        //更新DoorTime编辑后的信息
        public ActionResult ModifyDoorTime(String data)
        {

            try
            {
                this.getSession();
                DoorTime doorTime = JsonConvert.JsonToObject<DoorTime>(data);
                deviceService.ModifyDoorTime(this.getSession().UserID, doorTime);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (Exception e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

    }
}
