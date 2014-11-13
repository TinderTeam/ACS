using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Service.Constant;
using ACS.Common.Util;
namespace ACS.Service.Impl
{
    public class EmployeeServiceImpl : EmployeeService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Employee> employeeDao = DaoContext.getInstance().getEmployeeDao();

        public AbstractDataSource<Employee> getEmployeeList(Employee filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Employee> dataSource = new DatabaseSourceImpl<Employee>(conditionList);
            return dataSource;
        }
        //创建新员工
        public void create(EmployeeModel employeeModel)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeCode", employeeModel.EmployeeCode);
            if (null != employeeDao.getUniRecord(condition))
            {
                log.Error("create failed, the employeeCode has exist. employeeCode is " + employeeModel.EmployeeCode);
                throw new SystemException(ExceptionMsg.EMPLOYEE_CODE_EXIST);
            }
            Employee employee = new Employee();
            employee = ModelConventService.toEmployee(employee,employeeModel);
            employeeDao.create(employee);
        }
        /// <summary>
        ///批量删除员工
        /// </summary>
        /// <returns></returns>
        public void delete(List<int> employeeIDList)
        {
            foreach (int i in employeeIDList)
            {
                employeeDao.delete(
                    new QueryCondition(
                       ConditionTypeEnum.EQUAL,
                       Employee.ID,
                       i.ToString()
                    )
                );
            }
        }
        /// <summary>
        ///批量注销员工
        /// </summary>
        /// <returns></returns>
        public void cancel(List<int> employeeIDList)
        {
            foreach (int i in employeeIDList)
            {
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", i.ToString());
                Employee orignalEmployee = employeeDao.getUniRecord(condition);
                orignalEmployee.EmpEnable = "已注销";
                employeeDao.update(orignalEmployee);
            }
        }
        /// <summary>
        ///批量离职
        /// </summary>
        /// <returns></returns>
        public void leave(List<int> employeeIDList)
        {
            foreach (int i in employeeIDList)
            {
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", i.ToString());
                Employee orignalEmployee = employeeDao.getUniRecord(condition);
                orignalEmployee.Leave = "已离职";
                employeeDao.update(orignalEmployee);
            }
        }
        public EmployeeModel getEmployeeByID(string employeeID)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", employeeID);
            Employee employee = employeeDao.getUniRecord(condition);
            if (null == employee)
            {
                log.Error("get employee failed, the employee is not exist. employeeID is " + employeeID);
                throw new SystemException(ExceptionMsg.EMPLOYEE_NOT_EXIST);
            }
            EmployeeModel employeeModel = ModelConventService.toEmployeeModel(employee);
            return employeeModel;
        }
        public void update(EmployeeModel employeeModel)
        {
            //判断用户是否存在
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", employeeModel.EmployeeID.ToString());
            Employee orignalEmployee = employeeDao.getUniRecord(condition);
            if (null == orignalEmployee)
            {
                log.Error("modify employee failed, the employee is not exist. EmployeeID is " + employeeModel.EmployeeID);
                throw new SystemException(ExceptionMsg.EMPLOYEE_NOT_EXIST);
            }
            QueryCondition codeCondition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeCode", employeeModel.EmployeeCode);
            if ((null != employeeDao.getUniRecord(codeCondition)) && (orignalEmployee.EmployeeID != employeeModel.EmployeeID))
            {
                log.Error("modify employee failed, the EmployeeCode has exist. EmployeeCode is " + employeeModel.EmployeeCode);
                throw new SystemException(ExceptionMsg.EMPLOYEE_CODE_EXIST);
            }
            Employee employee = ModelConventService.toEmployee(orignalEmployee, employeeModel);
            employeeDao.update(employee);
        }
        /// <summary>
        ///批量发卡员工信息加载
        /// </summary>
        /// <returns></returns>
        public string getEmployeeList(List<string> idList)
        {
            
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.IN, Employee.ID, idList);

            List<Employee> employeeList = employeeDao.getAll(condition);
            String data = JsonConvert.ObjectToJson(employeeList);
            String json = "{\"total\":" + idList.Count + ",\"data\":" + data + "}";
            return json;
        }
        /// <summary>
        ///员工批量发卡
        /// </summary>
        /// <returns></returns>
        public void saveEmployeeCard(List<EmployeeModel> employeeModelList)
        {
            //判断卡号是否重复
            for(int i=0;i<employeeModelList.Count;i++)
            {
                //判断提交的卡号是否已存在
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Employee.Card,employeeModelList[i].CardNo);
                Employee employeeTest = employeeDao.getUniRecord(condition);
                if (null != employeeTest)
                {
                    if(employeeModelList[i].EmployeeID != employeeTest.EmployeeID)
                    {
                        log.Error("employee card NO duplicate, " + employeeModelList[i].EmployeeName + " and " + employeeTest.EmployeeName + "are the same");
                        throw new SystemException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
                    }   
                }
                //判断提交的卡号是否存在重复
                for(int j=i+1;j<employeeModelList.Count;j++)
                {
                    if(employeeModelList[i].CardNo==employeeModelList[j].CardNo)
                    {
                        log.Error("employee card NO duplicate, "+employeeModelList[i].EmployeeName+" and " +employeeModelList[j].EmployeeName+"are the same");
                        throw new SystemException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
                    }
                }
            }

            //更新发卡信息
            for (int i = 0; i < employeeModelList.Count; i++)
            {
                QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, Employee.ID, employeeModelList[i].EmployeeID.ToString());
                Employee orignalEmployee = employeeDao.getUniRecord(condition);
                orignalEmployee.CardNo = employeeModelList[i].CardNo;
                employeeDao.update(orignalEmployee);
            }
        }
    }
}