using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Dao;
using ACS.Common.Dao.impl;
namespace ACS.Common.Dao.datasource
{
    public class DatabaseSourceImpl<E>:AbstractDao<E>,AbstractDataSource<E>
    {
      
        private List<QueryCondition> conditionList = new List<QueryCondition>();

        public DatabaseSourceImpl()
        {

        }
        public DatabaseSourceImpl(List<QueryCondition> conditionList)
        {
            this.conditionList = conditionList;
        }

 
	    public List<E> getCurrentPageData(int startNum, int pageSize)
	    {
 
            return this.getAll(conditionList, startNum, pageSize);
	    }

 
	    public List<E> getAllPageData()
	    {
		    // TODO Auto-generated method stub
		    return  this.getAll(conditionList);
	    }

 
	    public long getDataCount()
	    {
		    // TODO Auto-generated method stub
		    return this.getCount(conditionList);
	    }
  
    }
}