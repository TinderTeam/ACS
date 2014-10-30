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
            //UserModel userModel = userService.getUserByID(id);
            //ViewBag.user = userModel;
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
        public ActionResult create(EmployeeModel employeeModel)
        {
            Employee employee = ModelConventService.toEmployee(employeeModel);
            if (ModelVerificationService.EmployeeVerification(employee))
            {
                //校验成功
                employeeService.create(employee);
            }else{
                //校验失败
                //TODO: 
            }
            return View();
        }

        /// <summary>
        /// 修改用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult modify(EmployeeModel employeeModel)
        {
            Employee employee = ModelConventService.toEmployee(employeeModel);
            if (ModelVerificationService.EmployeeVerification(employee))
            {
                //校验成功
                employeeService.create(employee);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            return View();
        }

        /// <summary>
        /// 删除员工
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult Remove(String idstr)
        {
            List<int> idList = ModelConventService.toIDList(idstr);
            log.Debug("Delete User (id=" + idList + ") ...");
            if (ModelVerificationService.UserIDExist(idList))
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
    }
}
