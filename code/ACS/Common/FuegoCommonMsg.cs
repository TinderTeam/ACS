using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Common
{
    public class FuegoCommonMsg
    {
        public const String SUCCESS = "Success";
        public const String FAIL = "Fail";

        public const String DATABASE_FAIL = "DATABASE_FAIL";


 
        public static String getMessageByErrorCode(String errorCode)
        {
            
            return errorCode;
        }
    }
}