using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Util
{
    public class DataCreatUtil
    {
        public static String getUUID()
        {
            string UUID = System.Guid.NewGuid().ToString();
            return UUID;
        }
    }
}