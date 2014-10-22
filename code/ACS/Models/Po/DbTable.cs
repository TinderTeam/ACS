using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class DbTable
    {
        private string columnName;
        private string type;
        private string tableName;

        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }
      

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}