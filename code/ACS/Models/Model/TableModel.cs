using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class TableModel
    {
        List<ColumnModel> titleList;
        List<List<String>> contentArray;

        public List<List<String>> ContentArray
        {
            get { return contentArray; }
            set { contentArray = value; }
        }
        public List<ColumnModel> TitleList
        {
            get { return titleList; }
            set { titleList = value; }
        }
    }
}