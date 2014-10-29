using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Model
{
    public class PswdModel
    {
        private String oldPswd;
        private String newPswd;

        public String OldPswd
        {
            get { return oldPswd; }
            set { oldPswd = value; }
        }
        
        public String NewPswd
        {
            get { return newPswd; }
            set { newPswd = value; }
        }


    }
}