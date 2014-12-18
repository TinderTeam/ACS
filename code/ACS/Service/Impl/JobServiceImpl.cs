using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
using ACS.Common.Dao.datasource;
using ACS.Common.Dao;
using ACS.Common.Constant;
using ACS.Models.Po.CF;
using ACS.Service.Constant;
using ACS.Common.Util;
using ACS.Models.Po.Sys;
using ACS.Models.Po.Business;
using ACS.Common;
using ACS.Common.Model;
namespace ACS.Service.Impl
{
    public class JobServiceImpl :CommonServiceImpl<Job>,JobService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = Job.ID;
            return perObjInfo;
        }

    }
}