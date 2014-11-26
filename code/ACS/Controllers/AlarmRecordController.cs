using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po.Business;
using System.Web.Script.Serialization;
using ACS.Common.Util;
namespace ACS.Controllers
{
    public class AlarmRecordController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        AlarmRecordService alarmRecordService = ServiceContext.getInstance().getAlarmRecordService();

        public ActionResult AlarmRecord()
        {
            return View();
        }

        public ActionResult Load(TableForm tableForm, AlarmRecord filter)
        {
            log.Debug("Load Employee Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<AlarmRecord> alarmRecordModelTable = new TableDataModel<AlarmRecord>();
            alarmRecordModelTable.setPage(tableForm.getPage());
            alarmRecordModelTable.setDataSource(alarmRecordService.getAlarmRecordList(filter));
          
            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);

            Response.Write(alarmRecordModelTable.getMiniUIJson());
            return null;
        }

    }
}
