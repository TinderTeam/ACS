using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ACS.Models.Model;
using ACS.Common.Model;
using ACS.Service;
using ACS.Models.Po;
namespace ACM.Controllers
{
    public class EmployeeManageController : Controller
    {
        EmployeeService employeeService = ServiceContext.getInstance().getEmployeeService();
        public ActionResult EmployeeManage(TableForm tableForm, Employee filter)
        {
            TableDataModel<Employee> emolpyeeModelTable = new TableDataModel<Employee>();
            emolpyeeModelTable.setPage(tableForm.getPage());
            emolpyeeModelTable.setDataSource(employeeService.getEmployeeList(filter));
            @ViewBag.EmployeeList = emolpyeeModelTable.getCurrentPageData();
            return View();
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
        /// 删除用户
        /// Ajax调用
        /// </summary>
        /// <returns></returns>
        public ActionResult delete(int employeeModelID)
        {

            if (ModelVerificationService.EmployeeModelIDExist(employeeModelID))
            {
                //校验成功
                employeeService.delete(employeeModelID);
            }
            else
            {
                //校验失败
                //TODO: 
            }
            return View();
        }
    }
}
