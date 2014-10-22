using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
 
namespace ACS.Common.Util
{
    public class DataTypeConvert
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Object convertStrToObject(String value,Type fieldClass)
	   {
		 Object obj = null;
         if (fieldClass == typeof(int) || fieldClass == typeof(Int32) || fieldClass == typeof(Int16))
		{
 			obj = Convert.ToInt32(value);
		}
         else if (fieldClass == typeof(float))
		{
			obj = Convert.ToDouble(value);
		}
         else if (fieldClass == typeof(double) || fieldClass == typeof(Double))
		{
			obj = Convert.ToDouble(value);
		}
         else if (fieldClass == typeof(Boolean) || fieldClass == typeof(Byte))
		{
			obj = Convert.ToBoolean(value);
		}
         else if (fieldClass == typeof(byte) || fieldClass == typeof(Byte))
		{
			obj = Convert.ToByte(value);
		}
         else if (fieldClass == typeof(string) || fieldClass == typeof(String))
		{
			obj = value;
		}
		else if(fieldClass == typeof(DateTime))
		{
           // obj = DateUtil.stringToDate(value);
		}
		else
		{
			log.Error("can not convert to right type.the type is " + fieldClass + "the value is " + value);
		}
		return obj;
	}
    }
}