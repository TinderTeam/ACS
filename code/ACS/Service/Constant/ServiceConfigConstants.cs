using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Service.Constant
{
    public class ServiceConfigConstants
    {
        public static String  getAppPath(){
            return HttpRuntime.AppDomainAppPath.ToString();
        }

        public static String getDeviceConfigXmlPath()
        {
            String path = HttpRuntime.AppDomainAppPath.ToString() + "DeviceConfig.xml";
            return path;
        }

    }
}