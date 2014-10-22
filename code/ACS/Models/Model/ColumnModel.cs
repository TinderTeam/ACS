using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class ColumnModel
    {
        private String title;
        private String name;
        private String type;

        public String Type
        {
            get { return type; }
            set { type = value; }
        }

        public String Title
        {
            get { return title; }
            set { title = value; }
        }
        public String Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}