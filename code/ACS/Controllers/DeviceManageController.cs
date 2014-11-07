using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Dao.datasource;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Test;

namespace ACS.Controllers
{
    public class DeviceManageController : Controller
    {
        //
        // GET: /Device/
        DeviceService service = ServiceContext.getInstance().getDeviceService();

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
            AbstractDataSource<Control> list = service.getDeviceList(null);
            TreeModel tree = ModelConventService.toDeviceTree(list);
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
            ViewBag.DoorModel = dmodel;
            return View();
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
