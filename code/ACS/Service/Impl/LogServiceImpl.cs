using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Log;

namespace ACS.Service.Impl
{
    public class LogServiceImpl:CommonServiceImpl<SystemLog>,LogService
    {


        public override String GetPrimaryName()
        {
            return SystemLog.ID;
        }

        #region LogService 成员

        public void log(int userID, string logEvent, string msg, string result)
        {
            SystemLog log = new SystemLog();
            log.LogDateTime = DateTime.Now;
            log.LogEvent = logEvent;
            log.Msg = msg;
            log.Result = result;
            log.UserCode = userID;
            base.Create(log);
        }
        #endregion
    }
   
}