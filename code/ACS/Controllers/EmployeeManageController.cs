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
using ACS.Controllers.Constant;
using ACS.Common;
using ACS.Service.Constant;
using ACS.Common.Dao;
using ACS.Common.Constant;
namespace ACS.Controllers
{
    public class EmployeeManageController : MiniUITableController<Employee>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EmployeeService employeeService = ServiceContext.getInstance().getEmployeeService();


        public override CommonService<Employee> getService()
        {
            return employeeService;
        }

        public ActionResult EmployeeManage()
        {
            return View();
        }

 

        public ActionResult EmployeeEdit(String id)
        {
            ViewBag.Type = "EDIT";
            Employee employee = employeeService.Get(id);
            ViewBag.employee = employee;
            return View();
        }

        public ActionResult EmployeeCreate()
        {
            ViewBag.Type = "CREATE";
            return View("EmployeeEdit");
        }



 
       
        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Cancel(String idList)
        {
            try
            {
                employeeService.cancel(getIDList(idList));
            }
            catch (FuegoException e)
            {
                log.Error("cancel failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("cancel failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
        /// <summary>
        /// 用户离职
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Leave(String idList)
        {
            try
            {
                employeeService.leave(getIDList(idList));
            }
            catch (FuegoException e)
            {
                log.Error("leave failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("leave failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }

          /// <summary>
        /// 显示员工发卡界面
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Card(String idstr)
        {
            ViewBag.idstr = idstr;
            return View();
        }
        //加载需要发卡的用户列表
        public ActionResult distributeCardList(String idList)
        {

            List<QueryCondition> conditionList = new List<QueryCondition>();
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN,Employee.ID,getIDList(idList));
            conditionList.Add(condition);
 
            return LoadTable(conditionList);
        }

        /// <summary>
        /// 员工发卡提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveCard(string data)
        {
            string text = null;
            log.Debug("Save Employee card...");
            List<Employee> employeeList = JsonConvert.JsonToObject<List<Employee>>(data);

            try
            {
                //校验成功
                employeeService.saveEmployeeCard(employeeList);
            }
            catch (FuegoException e)
            {
                log.Error("leave failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("leave failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }
 
            return ReturnJson(Rsp);
        }
 
    }
}
