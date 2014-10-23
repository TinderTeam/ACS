using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Model
{
    public class TableForm
    {
        private int numPerPage = 20;
        private int pageNum = 1;
 
        public PageModel getPage()
        {
            PageModel page = new PageModel();
            page.setPageSize(NumPerPage);
            page.setCurrentPage(PageNum);
            return page;
        }

        public int NumPerPage
        {
            get { return numPerPage; }
            set { numPerPage = value; }
        }

        public int PageNum
        {
            get { return pageNum; }
            set { pageNum = value; }
        }
    }
}