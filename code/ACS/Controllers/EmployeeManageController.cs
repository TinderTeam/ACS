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
        //加载用户首页用户信息列表
        public override ActionResult Load(String data)
        {
            return LoadTable<EmployeeView>(GetFilterCondition(data));
        }
        //用于实现条件查询功能
        public override List<QueryCondition> GetFilterCondition(String json)
        {
            
            List<QueryCondition> filterCondition = new List<QueryCondition>();
            EmployeeView employeeFilter = JsonConvert.JsonToObject<EmployeeView>(json);
            if (null != employeeFilter)
            {
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EmployeeView.EMPLOYEENAME, employeeFilter.EmployeeName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EmployeeView.EMPLOYEECODE, employeeFilter.EmployeeCode));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EmployeeView.DEPTNAME, employeeFilter.DeptName));
                filterCondition.Add(new QueryCondition(ConditionTypeEnum.INCLUDLE, EmployeeView.JOBNAME, employeeFilter.JobName));
            }
            
            return filterCondition;
        }
        //上传员工照片
        public ActionResult UploadPhoto(HttpPostedFileBase Fdata,String num)
        {
            //string fileName = System.IO.Path.GetFileName(Fdata.FileName);
            string fileName = DataCreatUtil.getUUID() + ".jpg";
            string filePhysicalPath = ServiceConfigConstants.getUploadPhotoPath() + fileName;
            string fileRelativePath = "/upload/EmployeePhoto/" + fileName;
            List<String> photoObj = new List<string>();
            try
            {
                Fdata.SaveAs(filePhysicalPath);
                photoObj.Add(fileRelativePath);
                photoObj.Add(num);                      //num用来标识上传的是Photo1还是Photo2
                Rsp.Obj = photoObj;
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
        //显示员工权限配置页面
        public ActionResult AccessPage()
        {
            return View();
        }
        //更新员工权限配置
        public ActionResult ModifyAccess(String employeeID, String data)
        {
            try
            {
                employeeService.modifyAccess(employeeID, data);
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
        public ActionResult Leave(String data)
        {
            try
            {
                List<String> idList = JsonConvert.JsonToObject<List<String>>(data);
                employeeService.leave(idList);
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
        public ActionResult Card()
        {
            return View();
        }
        //加载需要发卡的用户列表
        public ActionResult distributeCardList(String data)
        {
            List<String> employeeIDList = JsonConvert.JsonToObject<List<String>>(data);
            List<QueryCondition> conditionList = new List<QueryCondition>();
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, Employee.ID, employeeIDList);
            conditionList.Add(condition);
            return LoadTable(conditionList);
        }

        /// <summary>
        /// 员工发卡提交
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveCard(string data)
        {
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
