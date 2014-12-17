using ACS.Models.Po.Business;
using ACS.Service.Constant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;

namespace ACS.Service.device
{
    public interface DeviceOperator
    {
        Control GetControl();
 
         void Connect();

         void Operate(OperateDeviceCmdEnum cmdCode, Door door);

         void SetDoorTime(Door door,DoorTime doorTime);

         void deviceCardInfoDownLoad(Dictionary<Employee, List<DoorTimeView>> cardInfoMap);

         void  cardInfoDownLoad(Employee employee, List<DoorTimeView> doorTimeList);
    }
}