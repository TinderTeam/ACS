﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.CF
{
    public class Privilege
    {
        private int privilegeID;//权限ID
        private String privilegeMaster;//权限主体类型
        private int privilegeMasterValue;//权限主体名称
        private String privilegeAccess;//权限类型
        private String privilegeAccessValue;//权限名称
        private String privilegeOperation;//最后编辑用户

    }
}