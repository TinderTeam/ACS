using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Log;
using ACS.Common.Model;
using ACS.Common;
using ACS.Common.Util;
using ACS.Service.Constant;

namespace ACS.Service.Impl
{
    public class LogServiceImpl:CommonServiceImpl<SystemLog>,LogService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //获取对象信息
        public override PersistenceObjInfo GetObjectInfo()
        {
            PersistenceObjInfo perObjInfo = new PersistenceObjInfo();
            perObjInfo.PrimaryName = SystemLog.ID;
            return perObjInfo;
        }

        #region LogService 成员

        public void CreateLog(int userID, string logEvent, string msg, string result)
        {
            SystemLog log = new SystemLog();
            log.LogDateTime = DateTime.Now;
            log.LogEvent = logEvent;
            log.Msg = msg;
            log.Result = result;
            log.UserCode = userID.ToString();
            base.Create(log);
        }
        #endregion

        public void CreateLog(int userID, string operType, LogOperable operObj, string operResult)
        {
            try
            {
                SystemLog operlog = new SystemLog();
                operlog.LogDateTime = DateTime.Now;
                operlog.LogEvent = operType;
                operlog.Msg = operObj.GetObjType() + ":" + operObj.GetObjName();
                operlog.Result = operResult;
                operlog.UserCode = userID.ToString();
                GetDao().create(operlog);
            }
            catch (Exception e)
            {
                log.Error("Create operate log Failed", e);
            }
            
        }

        public void CreateLog(int userID, string operType, List<LogOperable> operObjList, string operResult)
        {
            try
            {
                if (!ValidatorUtil.isEmpty<LogOperable>(operObjList))
                {
                    List<SystemLog> logList = new List<SystemLog>();
                    foreach (LogOperable logOperable in operObjList)
                    {
                        SystemLog operlog = new SystemLog();
                        operlog.LogDateTime = DateTime.Now;
                        operlog.LogEvent = operType;
                        operlog.Msg = logOperable.GetObjType() + ":" + logOperable.GetObjName();
                        operlog.Result = operResult;
                        operlog.UserCode = userID.ToString();
                        logList.Add(operlog);
                    }
                    GetDao().create(logList);
                }
                else
                {
                    log.Warn("Create operate log Failed, operate Obj is null");
                }
            }   
            catch(Exception e)
            {
                log.Error("Create operate log Failed",e);
            }
            
        }
    }
   
}