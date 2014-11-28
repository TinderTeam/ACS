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
using ACS.Common;
namespace ACS.Service.Impl
{
    public class HolidayServiceImpl : CommonServiceImpl<Holiday>, HolidayService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        CommonDao<Holiday> holidayDao = DaoContext.getInstance().getHolidayDao();
 
        //获取对象主键
        public override string GetPrimaryName()
        {
            return Holiday.ID;
        }
    }
}