using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class DbTable
    {
        private string name;
        private string type;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
      

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}