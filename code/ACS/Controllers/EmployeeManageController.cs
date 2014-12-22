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
using System.Threading;
namespace ACS.Controllers
{
    public class EmployeeManageController : MiniUITableController<Employee>
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private EmployeeService employeeService = ServiceContext.getInstance().getEmployeeService();



        /**
         *  这里在处理Employee的过程中因为涉及到EmployeeIndex更新的问题，所以在这里要复写增删方法。
         */
        #region 复写 Delete Create 方法
        public override ActionResult Delete(String data)
        {
            log.Debug("Deleting ObjList, ObjIDList is " + data);
            try
            {
                List<String> idList = JsonConvert.JsonToObject<List<String>>(data);
                getService().Delete(this.getSessionUser().UserID, idList);
                employeeService.DeleteIndex(idList); 

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
       
        public override ActionResult Create(String data)
        {
            log.Debug("Creating OBJ ... " + data);

            try
            {
                Employee obj = JsonConvert.JsonToObject<Employee>(data);
                //先查询是否有回收到的Index
                getService().Create(this.getSessionUser().UserID, obj);
                int receiveIndex = employeeService.getReceiveIndex();
                if (receiveIndex == 0)
                {
                    employeeService.CreateIndex(obj);
                }
                else{
                    employeeService.UpdateIndex(receiveIndex, obj);
                }
               
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
        #endregion
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
            log.Debug("Modifying access of employee, employeeID is " + employeeID +". accessID is "+ data );
            try
            {
                employeeService.modifyAccess(this.getSessionUser().UserID, employeeID, data);
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
        public ActionResult Cancel(String data)
        {
            log.Debug("Canceling employee, employee IDList is " + data);
            try
            {
                List<String> idList = JsonConvert.JsonToObject<List<String>>(data);
                employeeService.cancel(this.getSessionUser().UserID, idList);
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
            log.Debug("Leaving employee, employee IDList is " + data);
            try
            {
                List<String> idList = JsonConvert.JsonToObject<List<String>>(data);
                employeeService.leave(this.getSessionUser().UserID, idList);
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
            log.Debug("Saving card of employee, employeeList is " + data);
            try
            {
                //校验成功
                List<Employee> employeeList = JsonConvert.JsonToObject<List<Employee>>(data);
                employeeService.saveEmployeeCard(this.getSessionUser().UserID, employeeList);
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
        /// 下发员工的卡片到所有设备
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        public ActionResult DownCardList(String idList)
        {
            try
            {
                var list = getIDList(idList);
                String uuID = DataCreatUtil.getUUID();
                DownCardListThread thread = new DownCardListThread(list, uuID);
                Thread oThread = new Thread(new ThreadStart(thread.Op));

                oThread.Start();
                Rsp.Obj = uuID;  
            }
            catch (FuegoException e)
            {
                log.Error("download failed", e);
                Rsp.ErrorCode = e.GetErrorCode();
            }
            catch (SystemException e)
            {
                log.Error("download failed", e);
                Rsp.ErrorCode = ExceptionMsg.FAIL;
            }

            return ReturnJson(Rsp);
        }
    }


    class DownCardListThread
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private EmployeeService employeeService = ServiceContext.getInstance().getEmployeeService();
        String uuID;
        List<String> list;

        public DownCardListThread(List<String> list, String uuID)
        {
            this.list = list;
            this.uuID = uuID;
        }

        public void Op()
        {
        
            //打开进度监控器
            ProcessManageCache.startNewProcession(uuID);
            employeeService.DownCardList(list, uuID);
           
        }
    }
}
