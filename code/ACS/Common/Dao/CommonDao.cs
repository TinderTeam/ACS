using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Dao
{
    public interface CommonDao<E> : ViewDao<E>
    {
        void create(E obj);
		
        void create(List<E> obj);
		
        void update(E obj);

        void update(List<E> obj);

	    void delete(QueryCondition condition);

        void delete(List<QueryCondition> conditionList);

    }
}