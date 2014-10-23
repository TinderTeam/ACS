using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Model
{
    public class PageModel
    {

        private List<int> pages = new List<int>();
        private int[] pageSizeList = { 20, 50, 100, 200 };
        private int pageSize = 20;  //defualt page size
        private int currentPage = 1;
        private long count = 0;



        public int getStartNum()
        {
            return (currentPage - 1) * pageSize;
        }

        public int getEndNum()
        {
            return getStartNum() + pageSize;
        }

        public List<int> getPages()
        {
            pages.Clear();
            int i = 1;
            for (; i <= count / pageSize; i++)
            {
                pages.Add(i);
            }
            if (count % pageSize != 0)
            {
                pages.Add(i);
            }
            return pages;
        }
        public void setPages(List<int> pages)
        {
            this.pages = pages;
        }
        public int getPageSize()
        {
            return pageSize;
        }
        public void setPageSize(int pageSize)
        {
            this.pageSize = pageSize;
        }
        public int getCurrentPage()
        {
            return currentPage;
        }
        public void setCurrentPage(int currentPage)
        {
            this.currentPage = currentPage;
        }
        public long getCount()
        {
            return count;
        }
        public void setCount(long count)
        {
            this.count = count;
        }

        public int[] getPageSizeList()
        {
            return pageSizeList;
        }

        public void setPageSizeList(int[] pageSizeList)
        {
            this.pageSizeList = pageSizeList;
        }



    }

}