using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Log
{
    public class SystemLog
    {
        private int logID ;//日志ID 
        private DateTime logDateTime ;//日志时间
        private int userCode ;//用户Code
        private String logEvent;//事件
        private String msg ;//信息
        private String result ;//结果
        private String computer ;//电脑

    }
}