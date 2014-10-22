using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ACS.Common.Dao
{
    public interface ViewDao<E>
    {
        List<E> getAll();
        long getCount(List<QueryCondition> conditionList);
        List<E> getAll(List<QueryCondition> conditionList);
        List<E> getAll(List<QueryCondition> conditionList, int startNum, int pageSize);
        List<E> getAll(QueryCondition condition);

        E getUniRecord(QueryCondition condition);
        E getUniRecord(List<QueryCondition> conditionList);
    }
}