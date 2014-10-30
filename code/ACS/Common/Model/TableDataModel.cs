using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Dao.datasource;
using ACS.Common.Util;
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


        // 从page对象List信息生成Json串
        public string getMiniUIJson()
        {
            String data = JsonConvert.ObjectToJson(this.getCurrentPageData());
            String json = "{\"total\":" + this.getPage().getCount() + ",\"data\":" + data + "}";
            return json;
        }

    }
}