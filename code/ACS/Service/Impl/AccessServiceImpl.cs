using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using ACS.Common.Dao;
using ACS.Common.Dao.datasource;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;

namespace ACS.Service.Impl
{
    public class AccessServiceImpl:AccessService
    {
        #region AccessService 成员

        public Common.Dao.datasource.AbstractDataSource<Models.Po.Business.Access> getAccessList(Models.Po.Business.Access filter)
        {
            List<QueryCondition> conditionList = new List<QueryCondition>();
            AbstractDataSource<Access> dataSource = new DatabaseSourceImpl<Access>(conditionList);
            return dataSource;
        }

        #endregion
    }
}