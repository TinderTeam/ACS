﻿using System;
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

        public static Object convertToFieldObject(Type clazz,String fieldName,Object value)  
	    {
		Object obj = null;
        
		try
		{
          
          
            Type fieldClass = clazz.GetProperty(fieldName).PropertyType;
            if (fieldClass == value.GetType())
            {
                obj = value;
            }
            else
            {
                obj = DataTypeConvert.convertStrToObject(value.ToString(), fieldClass);
            }
			

		}
		catch(Exception e)
		{
			log.Error("can not convert to right type.the class is " + clazz + " the field name is " + fieldName + "the value is " + value);
			log.Error("convert data failed",e);
			throw new Exception(CommonMsg.DATA_CONVERT_ERROR);
		}
		
	 
		
		return obj;
	   }
        public static void setObjetField(Object obj, String fieldName, Object value)
        {
            PropertyInfo  field = obj.GetType().GetProperty(fieldName);
            Object objValue = convertToFieldObject(obj.GetType(), fieldName, value);
            field.SetValue(obj, objValue, null);
        }
    }
}