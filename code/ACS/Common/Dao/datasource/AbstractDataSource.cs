using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Dao.datasource
{
    public interface AbstractDataSource<E>
    {
        List<E> getCurrentPageData(int startNum, int endNum);

        List<E> getAllPageData();

        long getDataCount();
    }
}