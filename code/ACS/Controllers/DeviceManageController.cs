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

        #region 控制器服务      
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
        PrivilegeService privilegeService = ServiceContext.getInstance().getPrivilegeService();
        public override CommonService<Control> getService()
        {
            return deviceService;
        }
        #endregion

        #region 页面载入数据
      
        // 加载设备-门树
        public override ActionResult LoadTree()
        {
            List<TreeModel> deviceTreeList = deviceService.getDeviceTreeByID(this.getSessionUser().UserID.ToString());
            return ReturnJson(deviceTreeList);
        }

        //根据用户ID获取控制器列表
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            List<String> controlIDList = privilegeService.getPrivilegeValueList(this.getSessionUser().UserID.ToString(), ServiceConstant.SYS_ACCESS_TYPE_DEVICE_DOMAIN);
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            if (!ValidatorUtil.isEmpty(controlIDList))
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.IN, Control.CONTROL_ID, controlIDList));
            }
            else
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.FALSE));
            }
            return filterCondition;
        }

        //根据控制器ID加载DoorList
        public ActionResult LoadDoorList(String data)
        {
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            if (ValidatorUtil.isEmpty(data))
            {
                log.Warn("the ControlID is empty");
                return null;
            }
            else
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.EQUAL, Door.CONTROL_ID, data));
                return LoadTable<Door>(filterCondition);
            }
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

        #endregion

        #region 提交

        //更新Door编辑后的信息
        public ActionResult ModifyDoor(String data)
        {
            try
            {
                this.getSessionUser();
                Door door = JsonConvert.JsonToObject<Door>(data);
                deviceService.ModifyDoor(this.getSessionUser().UserID, door);
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

        //更新DoorTime编辑后的信息
        public ActionResult ModifyDoorTime(String data)
        {
            try
            {
                this.getSessionUser();
                DoorTime doorTime = JsonConvert.JsonToObject<DoorTime>(data);
                deviceService.ModifyDoorTime(this.getSessionUser().UserID, doorTime);
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

        //更新设备信息
        public ActionResult updateDeviceInfo(String idList)
        {
            try
            {
                List<String> controlIDList = JsonConvert.JsonToObject<List<String>>(idList);
                deviceService.UpdateDeviceInfo(this.getSessionUser().UserID, controlIDList);
                //待实现
            }
            catch (FuegoException e)
            {
                log.Error("download failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("download failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        #endregion

    }
}
