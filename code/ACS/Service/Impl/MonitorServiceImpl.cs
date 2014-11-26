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
    public class MonitorServiceImpl : MonitorService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //CommonDao<Employee> employeeDao = DaoContext.getInstance().getEmployeeDao();

        public AbstractDataSource<Control> getControlList(Control filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Control> dataSource = new DatabaseSourceImpl<Control>(conditionList);
            return dataSource;
        }
      
    }
}