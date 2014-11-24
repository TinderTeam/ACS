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
namespace ACS.Controllers
{
    public class DeviceManageController : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Device/
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
        UserService userService = ServiceContext.getInstance().getUserService();
        PrivilegeService privilegeService = ServiceContext.getInstance().getPrivilegeService();

        /// <summary>
        /// 显示设备控制界面
        /// </summary>
        /// <returns></returns>
        public ActionResult DeviceManage()
        {
            return View();
        }

 

        public ActionResult DeviceCreatePanel()
        {
            List<String> list = deviceService.getDeviceTypeList();
            ViewBag.TypeList = list;
            return View();
        }


        /// <summary>
        /// 加载设备-门树
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public String DeviceTree(String key)
        {
            //获取当前登录用户
            UserModel loginUser = (UserModel)Session["SystemUser"];
            //根据用户获取权限门树
            TreeModel tree = userService.getUserDevicePrivilegeTree(loginUser.UserID.ToString());
            String str = tree.ToJsonStr();
            return str;
        }
        
       /// <summary>
       /// 选择设备
       /// </summary>
       /// <param name="key">传回设备ID</param>
       /// <returns></returns>
        public ActionResult DeviceView(String DeviceID)
        {
            ViewBag.deviceID = DeviceID;
            return View();
        }

        /// <summary>
        /// 选择门
        /// </summary>
        /// <param name="key">传回设备ID</param>
        /// <returns></returns>
        public ActionResult DoorView(String DoorID)
        {
            DoorModel dmodel;
            //dmodel   = service.getDoorByID(DoorID);

            dmodel = Test.Stub.getDoor();
            ViewBag.DoorID = DoorID;
            return View();
        }

        public String  LoadDoorTime(String DoorID)
        {
            log.Debug("Load Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            List<DoorTime> doorTimeList=deviceService.getDoorTimeListByDoorID(DoorID);
            return JsonConvert.ObjectToJson(doorTimeList);
        }


        public String  DoorTimeSave(String data)
        {

            List<DoorTimeModel> modelList = JsonConvert.JsonToObject<List<DoorTimeModel>>(data);
            deviceService.updateDoorTimeList(ModelConventService.toDoorTimeList(modelList));
            Response.Write(data);
            return null;

        }

        /// <summary>
        /// 设备信息变更
        /// </summary>
        /// <returns></returns>
        public String DeviceInfoEdit(String submitData)
        {
            ControllerModel controller = JsonConvert.JsonToObject<ControllerModel>(submitData);
            Control control = ModelConventService.toControl(controller);
            if (ModelVerificationService.ControllerVerification(control))
            {
                deviceService.updateControl(control);
            }
            Response.Write(submitData);
            return null;
        }

        /// <summary>
        /// 设备信息变更
        /// </summary>
        /// <returns></returns>
        public String DeviceEdit(String DeviceID)
        {
            return null;
        }

        /// <summary>
        /// 设备新增
        /// </summary>
        /// <returns></returns>
        public String DeviceAdd(String type,String data)
        {
            try
            {

                Control c= JsonConvert.JsonToObject<Control>(data);
                c.Type = type;
                //校验成功
                Control control = deviceService.addControl(c);
                //新增当前用户权限
                UserModel loginUser = (UserModel)Session["SystemUser"];
                privilegeService.addDomainPrivilege(loginUser.UserID.ToString(), control.ControlID.ToString());
            }
            catch (FuegoException e)
            {
                Rsp.ErrorCode = e.GetErrorCode();
                log.Error("add control failed", e);
            }
            catch (SystemException ex)
            {
                Rsp.ErrorCode = ExceptionMsg.FAIL;
                log.Error("add control failed", ex);
            }
            Response.Write(getRspJson());
            return null;
        }
        //加载控制器的参数
        public String LoadControllerInfo(String controllerID){
            DeviceModel deviceModel = deviceService.getDeviceByID(controllerID);
            String json = JsonConvert.ObjectToJson(deviceModel.Control);         
            return json;
        }


        //删除控制器
        public override void basicDelete(string id)
        {
            deviceService.deleteControlById(id);   
        }
    }
}
