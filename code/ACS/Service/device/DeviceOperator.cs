﻿using ACS.Models.Po.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service.device
{
    public interface DeviceOperator
    {
         void OpenDoor(Door door);
         void CloseDoor(Door door);
 
    }
}