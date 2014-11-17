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
namespace ACS.Controllers
{
    public class DeviceManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        // GET: /Device/
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
        UserService userService = ServiceContext.getInstance().getUserService();
        /// <summary>
        /// 显示设备控制界面
        /// </summary>
        /// <returns></returns>
        public ActionResult DeviceManage()
        {
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
            TreeModel tree = userService.getDevicePrivilegeTree(loginUser.UserID.ToString());
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
            DeviceModel dmodel=null;
            //dmodel = service.getDeviceByID(DeviceID);
            dmodel = Test.Stub.getDevice();
            ViewBag.DeviceModel = dmodel;
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


        /// <summary>
        /// 设备信息变更
        /// </summary>
        /// <returns></returns>
        public String DeviceEdit(String DeviceID)
        {
            return null;
        }
        /// <summary>
        /// 设备删除
        /// </summary>
        /// <returns></returns>
        public String DeviceDelete(String DeviceID)
        {
            return null;
        }
        /// <summary>
        /// 设备新增
        /// </summary>
        /// <returns></returns>
        public String DeviceAdd()
        {
            return null;
        }
        /// <summary>
        /// 门时间修改
        /// </summary>
        /// <returns></returns>
        public String DoorTimeEdit(String DoorTime)
        {
            return null;
        }


    }
}
