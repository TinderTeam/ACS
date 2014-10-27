using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using NHibernate;
using NHibernate.Criterion;
using ACS.Test;
namespace ACS.Dao.impl
{
    public class EmployeeDaoImpl : EmployeeDao
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public List<Employee> getAll()
        {
            return null;
            //To be Implemented
        }

        public void create(Employee employee)
        {
            return ;
            //To be Implemented
        }
        public void update(Employee employee)
        {
            return;
            //To be Implemented
        }

        public void delete(int employeeID)
        {
            return ;
            //To be Implemented
        }

    }
}