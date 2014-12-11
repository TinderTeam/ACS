

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

        public void CheckOnline()
        {
            log.Info("now check device status. the time is " + DateTime.Now);

            List<String> keyList = new List<String>();
            keyList.AddRange(deviceOperatorMap.Keys);

            foreach (var item in keyList)
            {
 
                bool status = OnlineDeviceCache.checkOnline(item);
                Control control = deviceOperatorMap[item].GetControl();
                if(status != control.Online)
                {

                    log.Warn("the control ip is " + control.Ip+ "the control status change,now status is " + status);
                     ServiceContext.getInstance().getDeviceService().OnlineStatus(control, status);
                }
 
            }
        }

        public DeviceOperator getDeviceOperator(Control control)
        {
            DeviceOperator deviceOperator = null;
            if (!deviceOperatorMap.ContainsKey(control.Ip))
            {
                deviceOperator = new DeviceOperatorImpl(control);
                deviceOperator.Connect();
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
