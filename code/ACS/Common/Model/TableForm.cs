using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Model
{
    public class TableForm
    {
        private int pageSize = 20;
        private int pageIndex = 0;
 
        public PageModel getPage()
        {
            PageModel page = new PageModel();
            page.setPageSize(PageSize);
            page.setCurrentPage(PageIndex+1);
            return page;
        }

        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }

        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
    }
}