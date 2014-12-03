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
        public static DateTime StringToDateTime(String str)
        {
            DateTime dateTime = Convert.ToDateTime(str);
            return dateTime;
        }
        public static bool DateTimeIsEmpty(DateTime dateTime)
        {
            if (dateTime.Ticks == 0)
            {
                return true;
            }
            return false;
 
        }
    }
}