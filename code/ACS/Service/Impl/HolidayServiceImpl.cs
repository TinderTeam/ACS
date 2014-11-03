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
namespace ACS.Service.Impl
{
    public class HolidayServiceImpl : HolidayService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Holiday> holidayDao = DaoContext.getInstance().getHolidayDao();

        public AbstractDataSource<Holiday> getHolidayList(Holiday filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Holiday> dataSource = new DatabaseSourceImpl<Holiday>(conditionList);
            return dataSource;
        }
        //创建新员工
        public void create(HolidayModel holidayModel)
        {
            Holiday holiday = new Holiday();
            holiday = ModelConventService.toHoliday(holiday, holidayModel);
            holidayDao.create(holiday);
        }
        /// <summary>
        ///批量删除员工
        /// </summary>
        /// <returns></returns>
        /*public void delete(List<int> employeeIDList)
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
        }*/
        /*public HolidayModel getHolidayByID(string holidayID)
        {
            QueryCondition condition = new QueryCondition(ConditionTypeEnum.EQUAL, "HolidayID", holidayID);
            Holiday holiday = holidayDao.getUniRecord(condition);
            if (null == holiday)
            {
                log.Error("get holiday failed, the holiday is not exist. holidayID is " + holidayID);
                throw new SystemException(ExceptionMsg.HOLIDAY_NOT_EXIST);
            }
            HolidayModel holidayModel = ModelConventService.toHolidayModel(holiday);
            return holidayModel;
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
        }*/
    }
}