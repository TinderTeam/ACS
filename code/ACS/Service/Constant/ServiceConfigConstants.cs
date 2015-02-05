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
            String path = getAppPath() + "DeviceConfig.xml";
            return path;
        }

        public static String getUploadPhotoPath()
        {
            String path = getAppPath() + "/upload/EmployeePhoto/";
            return path;
        }


        internal static String getDeviceIdentifyConfigXmlPath()
        {
            String path = getAppPath() + "DeviceIdentifyConfig.xml";
            return path;
        }
    }
}