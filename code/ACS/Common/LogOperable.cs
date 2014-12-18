using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common
{
    public interface LogOperable
    {
         String GetObjType();
         String GetObjName();
         String GetDesp();
    }
}