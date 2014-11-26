using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;
using System.Reflection;

namespace ACS.Common.Util
{
    public class ReflectionUtil
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static Object convertToFieldObject(Type clazz,String fieldName,String value)  
	    {
		Object obj = null;
        
		try
		{
          
            Type fieldClass = clazz.GetProperty(fieldName).PropertyType;
			obj = DataTypeConvert.convertStrToObject(value, fieldClass);

		}
		catch(Exception e)
		{
			log.Error("can not convert to right type.the class is " + clazz + " the field name is " + fieldName + "the value is " + value);
			log.Error("convert data failed",e);
			throw new Exception(CommonMsg.DATA_CONVERT_ERROR);
		}
		
	 
		
		return obj;
	   }
        public static void setObjetField(Object obj, String fieldName, String value)
        {
            PropertyInfo  field = obj.GetType().GetProperty(fieldName);
            field.SetValue(obj,value,null);
        }
    }
}