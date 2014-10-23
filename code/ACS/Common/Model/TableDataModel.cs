using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Dao.datasource;
namespace ACS.Common.Model
{
    public class TableDataModel<E>
    {
        private PageModel page = new PageModel();

        private AbstractDataSource<E> dataSource;

        public List<E> getCurrentPageData()
        {
            return dataSource.getCurrentPageData(page.getStartNum(), page.getPageSize());
        }

        public AbstractDataSource<E> getDataSource()
        {
            return dataSource;
        }

        public void setDataSource(AbstractDataSource<E> dataSource)
        {
            this.dataSource = dataSource;
            page.setCount(this.dataSource.getDataCount());
            this.dataSource = dataSource;
        }

        public PageModel getPage()
        {
            return page;
        }



        public void setPage(PageModel page)
        {
            this.page = page;
        }

    }
}