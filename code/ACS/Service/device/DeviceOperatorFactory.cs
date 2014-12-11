

using ACS.Common;
using ACS.Models.Po.Business;
using ACS.Service.Constant;
using ACS.Service.Impl;
using System;
using System.Collections.Generic;
namespace ACS.Service.device
{
    public class DeviceOperatorFactory 
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static DeviceOperatorFactory instance;
        private Dictionary<String, DeviceOperator> deviceOperatorMap = new Dictionary<String, DeviceOperator>();

        public DeviceOperator getDeviceOperator(Control control)
        {
            DeviceOperator deviceOperator = null;
            if (!deviceOperatorMap.ContainsKey(control.Ip))
            {
                deviceOperator = new DeviceOperatorImpl(control);
                if (deviceOperator.Connect())
                {
                    deviceOperatorMap[control.Ip] = deviceOperator;
                }
                else
                {
                    log.Error("conncet device failed,the control ip address " + control.Ip + ",the port is " + control.Port);
                    throw new FuegoException(ExceptionMsg.CONNECT_FAILED);
                }
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
