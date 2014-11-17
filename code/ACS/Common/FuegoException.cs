using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace ACS.Common
{
    public class FuegoException:SystemException
    {
        private String errorCode;

        public String GetErrorCode()
        {
            return errorCode;
        }
        public FuegoException():base()
        {
            
        }

        public FuegoException(String message)
            : base(message)
        {
            this.errorCode = message;
        }
 
        public FuegoException(String message, Exception e)
            : base(message, e)
        {
            this.errorCode = message;
        }

        public String GetMessage()
        {
            return FuegoCommonMsg.getMessageByErrorCode(base.Message);
        }
     }
}