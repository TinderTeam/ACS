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
namespace ACM.Controllers
{
    public class EventRecordController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        EventRecordViewService eventRecordViewService = ServiceContext.getInstance().getEventRecordViewService();

        public ActionResult EventRecord()
        {
            return View();
        }
        
        public ActionResult Load(TableForm tableForm, EventRecordView filter)
        {
            log.Debug("Load Employee Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<EventRecordView> eventRecordViewModelTable = new TableDataModel<EventRecordView>();
            eventRecordViewModelTable.setPage(tableForm.getPage());
            eventRecordViewModelTable.setDataSource(eventRecordViewService.getEventRecordViewList(filter));
          
            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);

            Response.Write(eventRecordViewModelTable.getMiniUIJson());
            return null;
        }

    }
}
