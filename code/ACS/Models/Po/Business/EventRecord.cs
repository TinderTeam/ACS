using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Log
{
    public class EventRecord
    {
        private int eventID;//时间ID
        private DateTime eventTime;//时间时间
        private int employeeID;//员工ID
        private String cardNo;//卡号
        private int doorID;//门ID
        private String eventType;//事件类型
        private int controlID;//控制器ID
        private String modify;//是否修改过

    }
}