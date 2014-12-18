using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Common.Model;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Service;
using ACS.Test;
using TcpipIntface;
using ACS.Common.Util;
using ACS.Service.device;
using System.Dynamic;
using ACS.Common;
using ACS.Service.Constant;
using System.Threading;
namespace ACS.Controllers
{
    public class MonitorController : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MonitorService monitorService = ServiceContext.getInstance().getMonitorService();
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();

        public ActionResult Monitor()
        {
            return View();
        }

        public ActionResult GetNewEvent(String data)
        {
            String[] attr = JsonConvert.JsonToObject<String[]>(data);
            String doorID=attr[0];
            String eventID = attr[1];
            String alarmID = attr[2];
      

            List<AlarmRecordView> alarmEventList=ServiceContext.getInstance().getAlarmRecordService().GetCurAlarm(alarmID, doorID);
            List<EventRecordView> eventList = ServiceContext.getInstance().getEventRecordService().GetCurEvent(eventID, doorID);


            Result result = new Result();
            List<MonitorEventModel> modelList = new List<MonitorEventModel>();
            foreach(AlarmRecordView e in alarmEventList ){
                MonitorEventModel model = new MonitorEventModel();
                model.DoorID = e.DoorID;
                model.CardNo = "";
                model.ContorID = e.ControlID;
                model.ControlName = e.ControlName;
                model.DoorName = e.DoorName;
                model.EventTime = DateUtil.DateTimeToString(e.AlarmTime);
                model.EventType = e.AlarmType;
                model.Id = e.AlarmID;
                model.Img = ServiceConstant.ALARM_IMG_PATH;
                model.EventModelType = "alarm";
                modelList.Add(model);
        

            }
            foreach (EventRecordView e in eventList)
            {
                MonitorEventModel model = new MonitorEventModel();
                model.DoorID = e.DoorID;
                model.CardNo = e.CardNo;
                model.ContorID = e.ControlID;
                model.ControlName = e.ControlName;
                model.EmployeeName = e.EmployeeName;
                model.DoorName = e.DoorName;
                model.EventTime = DateUtil.DateTimeToString(e.EventTime);
                model.EventType = ServiceContext.getInstance().getEventTypeService().Get(e.EventTypeID.ToString()).EventTypeName;
                model.Id = e.EventID;
                model.EventModelType = "event";
                model.Img = e.Photo1;
                modelList.Add(model);
               
            }
            if (!ValidatorUtil.isEmpty(eventList))
            {
                result.EventIndex = eventList[0].EventID;
            }
            else
            {
                result.EventIndex = System.Convert.ToInt32(eventID);
            }

            if (!ValidatorUtil.isEmpty(alarmEventList))
            {
                result.AlarmIndex = alarmEventList[0].AlarmID;
            }
            else
            {
                result.AlarmIndex = System.Convert.ToInt32(alarmID); ;
            }

            result.DataList = modelList;
            return ReturnJson(result);
        }

        public ActionResult BarStatus(String UUID)
        {
            try
            {
                int p = ProcessManageCache.getProcessByUUID(UUID);

                Rsp.Obj = p;
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

        public ActionResult OperateDevice(String DoorID,int cmdCode)
        {
            try
            {

                OperateDeviceCmdEnum cmd = (OperateDeviceCmdEnum)cmdCode;
                deviceService.OperateDevice(cmd, DoorID);
               
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

        public ActionResult DeviceDownload(String ControlID)
        {
            try
            {
                String uuID = DataCreatUtil.getUUID();
                DeviceOperatorThread thread = new DeviceOperatorThread(ControlID, uuID);
                Thread oThread = new Thread(new ThreadStart(thread.Op));
                oThread.Start();
                Rsp.Obj = uuID;

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

    /// <summary>
    /// 设备控制操作线程
    /// </summary>
    class DeviceOperatorThread
    {

        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
        String uuID;
        String controlID;
       
        public DeviceOperatorThread(String controlID,String uuID)
        {
            this.controlID = controlID;
            this.uuID = uuID;
        }

        public void Op()
        {
   
            //打开进度监控器
            ProcessManageCache.startNewProcession(uuID);
            deviceService.DeviceDownload(controlID, uuID);
           
        }
    }

    class Result
    {

        public int EventIndex { get; set; }
        public int AlarmIndex { get; set; }
        public List<MonitorEventModel> DataList { get; set; }

    }
}
