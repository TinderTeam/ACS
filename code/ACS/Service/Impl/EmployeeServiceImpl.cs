﻿using System;
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
namespace ACS.Service.Impl
{
    public class EmployeeServiceImpl : EmployeeService
    {
        CommonDao<Employee> employeeDao = DaoContext.getInstance().getEmployeeDao();

        public AbstractDataSource<Employee> getEmployeeList(Employee filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Employee> dataSource = new DatabaseSourceImpl<Employee>(conditionList);
            return dataSource;
        }
        public void create(Employee employee)
        {
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

        public void update(Employee employee)
        {
            employeeDao.update(employee);
        }
    }
}