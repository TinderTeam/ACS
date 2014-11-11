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
    public class EmployeeManageController : Controller
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        EmployeeService employeeService = ServiceContext.getInstance().getEmployeeService();
        public ActionResult EmployeeManage()
        {
            return View();
        }
        public ActionResult EmployeeEdit(String id)
        {
            ViewBag.Type = "EDIT";
            EmployeeModel employeeModel = employeeService.getEmployeeByID(id);
            ViewBag.employee = employeeModel;
            return View();
        }

        public ActionResult EmployeeCreate()
        {
            ViewBag.Type = "CREATE";
            return View("EmployeeEdit");
        }
        public ActionResult Load(TableForm tableForm, Employee filter)
        {
            log.Debug("Load Employee Data...");
            //数据库操作：使用查询条件、分页、排序等参数进行查询
            TableDataModel<Employee> employeeModelTable = new TableDataModel<Employee>();
            employeeModelTable.setPage(tableForm.getPage());
            employeeModelTable.setDataSource(employeeService.getEmployeeList(filter));
          
            log.Debug("pageIndex = " + tableForm.PageIndex + ";pageSize=" + tableForm.PageSize);

            Response.Write(employeeModelTable.getMiniUIJson());
            return null;
        }
        /// <summary>
        /// 新增员工
        /// 可以改成用Ajax调用的响应
        /// </summary>
        /// <returns></returns>
        public string create(string data)
        {
            string text = null;
            log.Debug("Create Employee...");
            EmployeeModel employeeModel = JsonConvert.JsonToObject<EmployeeModel>(data);
            employeeModel.TimeStamp = DateTime.Now;
            employeeModel.TimeStampx = DateTime.Now;
            employeeModel.LeaveDate = DateTime.Now;
            if (ModelVerificationService.EmployeeVerification(employeeModel))
            {
                
                try
                {
                    //校验成功
                    employeeService.create(employeeModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;
                }
                text = "Success";
                Response.Write(text);
                return null;
            }else{
                //校验失败
                //TODO: 
            }
            return null;
        }

        /// <summary>
        /// 修改用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(string data)
        {
            string text = null;
            log.Debug("Modify Employee...");
            EmployeeModel employeeModel = JsonConvert.JsonToObject<EmployeeModel>(data);

            if (ModelVerificationService.EmployeeVerification(employeeModel))
            {
                try
                {
                    //校验成功
                    employeeService.update(employeeModel);
                }
                catch (SystemException ex)
                {
                    text = ex.Message;
                    Response.Write(text);
                    return null;
                }
                text = "Success";
                Response.Write(text);
                return null;

            }
            else
            {
                //校验失败
                //TODO: 
            }
            return null;
        }

        /// <summary>
        /// 删除员工
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Remove(String idstr)
        {
            List<int> idList = ModelConventService.toIDList(idstr);
            log.Debug("Delete employee (id=" + idList + ") ...");
            if (ModelVerificationService.EmployeeIDExist(idList))
            {
                //校验成功
                employeeService.delete(idList);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            Response.Write("ok");
            return null;
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Cancel(String idstr)
        {
            List<int> idList = ModelConventService.toIDList(idstr);
            log.Debug("Cancel employee (id=" + idList + ") ...");
            if (ModelVerificationService.EmployeeIDExist(idList))
            {
                //校验成功
                //TODO
                employeeService.cancel(idList);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            Response.Write("ok");
            return null;
        }
        /// <summary>
        /// 用户离职
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Leave(String idstr)
        {
             List<int> idList = ModelConventService.toIDList(idstr);
            log.Debug("Delete employee (id=" + idList + ") ...");
            if (ModelVerificationService.EmployeeIDExist(idList))
            {
                //校验成功
                //TODO
                employeeService.leave(idList);
            }           
            else
            {
                //校验失败
                //TODO: 
            }
            Response.Write("ok");
            return null;
        }

          /// <summary>
        /// 员工发卡
        /// </summary>
        /// <param name="idstr"></param>
        /// <returns></returns>
        public ActionResult Card(String idstr)
        {
            //
            return View();
        }


        /// <summary>
        /// 发卡保存
        /// </summary>
        /// <returns></returns>
        public ActionResult SaveCard()
        {
            ///
            Response.Write("ok");
            return null;
        }
    }
}
