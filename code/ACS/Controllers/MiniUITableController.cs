using ACS.Common;
using ACS.Common.Dao;
using ACS.Common.Model;
using ACS.Common.Util;
using ACS.Service;
using ACS.Service.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ACS.Controllers
{
    public abstract class MiniUITableController<E> : BaseController
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const String  CREATE = "create";
        public const String  MODIFY = "modify";
        public const String  DELETE = "delete";

        /**
         * 该方法绑定了MiniUI 分页数据提交
         */
        public PageModel getPage()
        {

            int currentPage = Convert.ToInt32(Request.Form["pageIndex"]) + 1;
            int pageSize = Convert.ToInt32(Request.Form["pageSize"]);
            PageModel page = new PageModel();
      
            page.setCurrentPage(currentPage);
            page.setPageSize(pageSize);

            return page;
        }

        /**
         * 该方法绑定了MiniUI 表格选择id 数据提交
         */
        public static List<String> getIDList(String str)
        {
            List<String> list = new List<String>();
            foreach (String i in str.Split(','))
            {
                list.Add(i);
            }
            return list;

        }
        public ActionResult LoadTable(List<QueryCondition> conditionList)
        {
            TableDataModel<E> table = new TableDataModel<E>();
            table.setPage(getPage());
            table.setDataSource(getService().GetDataSource());
            return ReturnJson(table.getMiniUIJson());
        }

        public ActionResult LoadTable()
        {
            return LoadTable(null);
        }

        public virtual ActionResult Show(String id)
        {
            try
            {
                getService().Get(id);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        public virtual ActionResult Create(String data)
        {
            E obj = JsonConvert.JsonToObject<E>(data);
            try
            {
                getService().Create(obj);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        public ActionResult Modify(String data)
        {
            E obj = JsonConvert.JsonToObject<E>(data);
            try
            {
                getService().Modify(obj);
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

        public ActionResult Delete(String idList)
        {
            try
            {
                getService().Delete(getIDList(idList));
            }
            catch (FuegoException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("create failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }
           // Response.Write(JsonConvert.ObjectToJson(Rsp));

            return ReturnJson(Rsp);
        }

        public abstract CommonService<E> getService();
    }
}
