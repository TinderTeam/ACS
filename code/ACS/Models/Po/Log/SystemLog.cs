using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Log
{
    public class SystemLog
    {
        public const String ID = "LogID";

        private int logID;//日志ID 
        private DateTime logDateTime;//日志时间
        private int userCode;//用户Code
        private String logEvent;//事件
        private String msg;//信息
        private String result;//结果
        private String computer;//电脑

        public virtual DateTime LogDateTime
        {
            get { return logDateTime; }
            set { logDateTime = value; }
        }

        public virtual int UserCode
        {
            get { return userCode; }
            set { userCode = value; }
        }

        public virtual String LogEvent
        {
            get { return logEvent; }
            set { logEvent = value; }
        }

        public virtual String Msg
        {
            get { return msg; }
            set { msg = value; }
        }

        public virtual String Result
        {
            get { return result; }
            set { result = value; }
        }

        public virtual String Computer
        {
            get { return computer; }
            set { computer = value; }
        }

        public virtual int LogID
        {
            get { return logID; }
            set { logID = value; }
        }

    }
}