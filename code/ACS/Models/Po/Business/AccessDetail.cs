﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class AccessDetail
    {
        private int accessID;   //门禁规则
        private int controlID;  //控制器
        private int doorID;
        private DateTime startTime;
        private DateTime endTime;
    }
}