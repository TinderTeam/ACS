using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Dao
{
    public interface Dao<E> : ViewDao<E>
    {
        void create(E obj);

        void update(E obj);

        void delete(E obj);
	
	    void delete(QueryCondition condition);

    }
}