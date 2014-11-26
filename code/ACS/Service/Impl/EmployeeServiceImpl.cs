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
using ACS.Common;
namespace ACS.Service.Impl
{
    public class EmployeeServiceImpl : CommonServiceImpl<Employee>, EmployeeService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Employee> employeeDao = DaoContext.getInstance().getEmployeeDao();

        public override String GetPrimaryName()
        {
            return Employee.ID;
        }

        public override void Validator(Employee obj)
        {
            if(null != obj)
            {
                log.Info("the obj is empty");
                throw new FuegoException(ExceptionMsg.EMPLOYEE_NOT_EXIST);
            }

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeCode", obj.EmployeeCode);
            if (null != employeeDao.getUniRecord(condition))
            {
                log.Error("create failed, the employeeCode has exist. employeeCode is " + obj.EmployeeCode);
                throw new FuegoException(ExceptionMsg.EMPLOYEE_CODE_EXIST);
            }
        }
 
        public override void Modify(Employee newEmployee)
        {
            Validator(newEmployee);

            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "EmployeeID", newEmployee.EmployeeID.ToString());
            Employee oldEmployee = employeeDao.getUniRecord(condition);

            oldEmployee.EmployeeName = newEmployee.EmployeeName;
            oldEmployee.EnglishName = newEmployee.EnglishName;
            oldEmployee.Sex = newEmployee.Sex;
            oldEmployee.Home = newEmployee.Home;
            oldEmployee.Birthday = newEmployee.Birthday;
            oldEmployee.Phone = newEmployee.Phone;
            oldEmployee.PersonCode = newEmployee.PersonCode;
            oldEmployee.Home = newEmployee.Home;
            oldEmployee.Car = newEmployee.Car;

            oldEmployee.DeptID = newEmployee.DeptID;
            oldEmployee.Email = newEmployee.Email;

            oldEmployee.EmployeeCode = newEmployee.EmployeeCode;
 
            oldEmployee.JobID = newEmployee.JobID;
 
            oldEmployee.RegDate = newEmployee.RegDate;
            oldEmployee.EndDate = newEmployee.EndDate;
            oldEmployee.Note1 = newEmployee.Note1;
            oldEmployee.Note2 = newEmployee.Note2;
            oldEmployee.Note3 = newEmployee.Note3;
            oldEmployee.Note4 = newEmployee.Note4;
            oldEmployee.Photo = newEmployee.Photo;
            employeeDao.update(oldEmployee);
 
             
        }
 
 
        /// <summary>
        ///批量注销员工
        /// </summary>
        /// <returns></returns>
        public void cancel(List<String> employeeIDList)
        {
            Modify(employeeIDList, "EmpEnable", "已注销");
 
        }
        /// <summary>
        ///批量离职
        /// </summary>
        /// <returns></returns>
        public void leave(List<String> employeeIDList)
        {
            Modify(employeeIDList, "Leave", "已离职");
        }
 
 
        /// <summary>
        ///员工批量发卡
        /// </summary>
        /// <returns></returns>
        public void saveEmployeeCard(List<Employee> employeeModelList)
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
                        throw new FuegoException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
                    }   
                }
                //判断提交的卡号是否存在重复
                for(int j=i+1;j<employeeModelList.Count;j++)
                {
                    if(employeeModelList[i].CardNo==employeeModelList[j].CardNo)
                    {
                        log.Error("employee card NO duplicate, "+employeeModelList[i].EmployeeName+" and " +employeeModelList[j].EmployeeName+"are the same");
                        throw new FuegoException(ExceptionMsg.EMPLOYEE_CARDNO_DUPLICATE);
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