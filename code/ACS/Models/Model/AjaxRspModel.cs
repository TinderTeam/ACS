using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Service.Constant;

namespace ACS.Models.Model
{
    public class AjaxRspModel
    {
        private String errorCode = ExceptionMsg.SUCCESS;

        public String ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }
        private String message;

        public String Message
        {
            get 
            {
                return ExceptionMsg.getMessageByErrorCode(errorCode); 
            }
            set { message = value; }
        }
        private Object obj;

        public Object Obj
        {
            get { return obj; }
            set { obj = value; }
        }



         
    }
}