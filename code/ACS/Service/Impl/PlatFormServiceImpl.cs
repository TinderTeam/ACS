using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service;
using ACS.Models.Model;
using ACS.Models.Po;
using ACS.Dao;
namespace ACS.Service.Impl
{
    public class PlatFormServiceImpl : PlatFormService
    {
        DbTableDao dbTableDao = DaoContext.getInstance().getDbTableDao();
        public List<ColumnModel> getTableColumn(String tableName)
        {
            return ModelConventService.toColumnList(dbTableDao.getTableByName(tableName));
        }


        List<FormColumn> getFromColumn(String tableName)
        {
            return  ModelConventService.toFormColumnList( dbTableDao.getTableByName(tableName));
        }


    }
}