using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Access
    {
        private int accessID;//门禁规则
        private String accessName;//规则名称
        private String accessNote;//门禁规则描述
        private String acsString;//备注
        private int createUserID;
        private DateTime createDate;
    }
}