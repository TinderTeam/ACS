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
namespace ACS.Controllers
{
    public class MonitorController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        MonitorService monitorService = ServiceContext.getInstance().getMonitorService();
        DeviceOperatorService tcpService = ServiceContext.getInstance().getDeviceOperatorService();
        // GET: /Monitor/

        public ActionResult Monitor()
        {
            return View();
        }


        public String DeviceTree(String key)
        {
            TreeModel tree = Stub.getTestDeviceTree();
            String str = tree.ToJsonStr();
            return str;
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

        public String OpenDoor(String ID)
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

        public String CloseDoor(String ID)
        {
            if (!tcpService.closeDoor("163.125.218.203", 1))
            {
                Response.Write("false");
            }
            else
            {
                Response.Write("true");
            }
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
