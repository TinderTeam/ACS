

using ACS.Models.Po.Business;
using ACS.Service.Impl;
using System;
using System.Collections.Generic;
namespace ACS.Service.device
{
    public class DeviceOperatorFactory 
    {

        private static DeviceOperatorFactory instance;
        private Dictionary<String, DeviceOperator> deviceOperatorMap = new Dictionary<String, DeviceOperator>();

        public DeviceOperator getDeviceOperator(Control control)
        {
            DeviceOperator deviceOperator = null;
            if (!deviceOperatorMap.ContainsKey(control.Ip))
            {
                deviceOperator = new DeviceOperatorImpl(control);
                deviceOperatorMap[control.Ip] = deviceOperator;
            }
            deviceOperator = deviceOperatorMap[control.Ip];
            return deviceOperator;
        }

        public static DeviceOperatorFactory getInstance()
        {
            if (instance == null)
            {
                instance = new DeviceOperatorFactory();
            }
            return instance;
        }
    }
}
