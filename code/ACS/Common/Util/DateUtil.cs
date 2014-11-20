using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common.Util
{
    public class DateUtil
    {
        public static String DateTimeToString(DateTime dateTime)
        {
            string dateString = dateTime.ToString("yyyy-MM-dd hh:mm:ss");
            return dateString;
        }
    }
}