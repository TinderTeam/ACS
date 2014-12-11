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
namespace ACS.Controllers
{
    public class MonitorController : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MonitorService monitorService = ServiceContext.getInstance().getMonitorService();
        DeviceService deviceService = ServiceContext.getInstance().getDeviceService();

        // GET: /Monitor/

        public ActionResult Monitor()
        {
            return View();
        }


        public ActionResult Load(TableForm tableForm)
        {
            log.Debug("Load Control Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<Control> controlModelTable = new TableDataModel<Control>();
            controlModelTable.setPage(tableForm.getPage());
            controlModelTable.setDataSource(monitorService.getControlList(null));
            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);
            Response.Write(controlModelTable.getMiniUIJson());
            return null;
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
                modelList.Add(model);
                result.AlarmIndex = e.AlarmID;

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
                model.Img = e.Photo1;
                modelList.Add(model);
                result.EventIndex = e.EventID;
            }
            result.DataList = modelList;
            return ReturnJson(result);
        }

        class Result{

            public int EventIndex { get; set; }
            public int AlarmIndex { get; set; }
            public List<MonitorEventModel> DataList { get; set; }

        }

        public String Timer(String data)
        {
            String[] attr = JsonConvert.JsonToObject<String[]>(data);
            return null;
        }

        public String OpenDoor(String DoorID)
        {
            deviceService.OpenDoor(DoorID);   
            return null;
        }

        public String CloseDoor(String DoorID)
        {
            deviceService.CloseDoor(DoorID);
            return null;
        }
        /*
        public String Alarm(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }

        public String AlarmCancel(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }

        public String unLock(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }

        public String Lock(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }

        public String FireAlarm(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }

        public String FireAlarmCancel(String ID)
        {
            if (!tcpService.openDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
            return null;
        }
         */
    }
}
