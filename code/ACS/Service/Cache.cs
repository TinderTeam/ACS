using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po;
using ACS.Models.Po.Sys;
using ACS.Models.Po.CF;
using ACS.Common.Constant;
using ACS.Service;
using ACS.Models.Model;
using ACS.Dao;
using ACS.Common.Dao;
using System.Xml;
using ACS.Service.Constant;
using ACS.Models.Po.Business;
using ACS.Service.device;
namespace ACS.Service
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class OnlineDeviceCache : System.ContextBoundObject
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const int TIME_OUT = 10*1000;
        public const long TIME_OUT_TICKET = TIME_OUT * 10000;
        private static Dictionary<String, long> cache = new Dictionary<String, long>();
        public static  void RefreshStatus(Control control)
        {
            log.Info("refresh control status "+control.Ip);
            if (!cache.ContainsKey(control.Ip))
            {
                cache.Add(control.Ip, DateTime.Now.Ticks);
            }
            else
            {
                cache[control.Ip] = DateTime.Now.Ticks;
            }
        }
        public static bool checkOnline(String ip)
        {
            if (!cache.ContainsKey(ip))
            {
                return false;
            }
            long time = DateTime.Now.Ticks - cache[ip];
            if (time > TIME_OUT_TICKET)
            {
                return false;
            }
            return true;

            
        }
    }
    public class DeviceTypeCache
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static DeviceTypeCache instance = new DeviceTypeCache();

        public List<DeviceTypeModel> TypeList = new List<DeviceTypeModel>();

        public static DeviceTypeCache GetInstance()
        {
            if (null == instance)
            {
                instance = new DeviceTypeCache();
            }
            return instance;
        }
        private DeviceTypeCache()
        {
            load();
        }


        private void load()
        {
           
            XmlDocument xml = new XmlDocument();
            xml.Load(ServiceConfigConstants.getDeviceConfigXmlPath());
            XmlNode root = xml.SelectSingleNode("/root");
            XmlNodeList xnl = root.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement e = (XmlElement)xnl.Item(i);
       
                DeviceTypeModel type = new DeviceTypeModel();
                type.TypeID = Convert.ToInt32(e.GetAttribute("typeID"));
                type.TypeName = e.GetAttribute("typeName");
                type.DoorNum = Convert.ToInt32(e.GetAttribute("door_num"));
                type.TimeNum =  Convert.ToInt32(e.GetAttribute("time_num"));
                TypeList.Add(type);
            }
          
        }
        public DeviceTypeModel GetDeviceType(int deviceTypeID)
        {
            foreach (DeviceTypeModel e in TypeList)
            {
                if (e.TypeID == deviceTypeID)
                {
                    return e;
                }
            }
            log.Warn("can not find device type. the typeID is " + deviceTypeID);
            return null;
        }
    }
    public class DeviceIdentifyTypeCache
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static DeviceIdentifyTypeCache instance = new DeviceIdentifyTypeCache();

        public List<IndetifyTypeType> TypeList = new List<IndetifyTypeType>();

        public static DeviceIdentifyTypeCache GetInstance()
        {
            if (null == instance)
            {
                instance = new DeviceIdentifyTypeCache();
            }
            return instance;
        }
        private DeviceIdentifyTypeCache()
        {
            load();
        }


        private void load()
        {

            XmlDocument xml = new XmlDocument();
            xml.Load(ServiceConfigConstants.getDeviceIdentifyConfigXmlPath());
            XmlNode root = xml.SelectSingleNode("/root");
            XmlNodeList xnl = root.ChildNodes;
            for (int i = 0; i < xnl.Count; i++)
            {
                XmlElement e = (XmlElement)xnl.Item(i);
                IndetifyTypeType type = new IndetifyTypeType();
                type.TypeID = Convert.ToInt32(e.GetAttribute("typeID"));
                type.Name = e.GetAttribute("typeName");
                TypeList.Add(type);
            }
        }
        public IndetifyTypeType GetDeviceType(int identifyTypeID)
        {
            foreach (IndetifyTypeType e in TypeList)
            {
                if (e.TypeID == identifyTypeID)
                {
                    return e;
                }
            }
            log.Warn("can not find device type. the identifyTypeID is " + identifyTypeID);
            return null;
        }
    }

    public class ProcessManageCache
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// 
        /// map for:
        /// | UserID in Session + cmd   | ProcessPro    |
        /// | String                    | int         |
        /// 

        static Dictionary<String, int> map = new Dictionary<String, int>();

        /// <summary>
        /// 获得一个Session用户操作的进度，如果缓存中没有进度则返回0，上级处理时应当对0当做没有进度来进行处理。
        /// 如果读到1（说明进度为100%）则需要将这个进度从缓存中删除掉。
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static int getProcessByUUID(String uuID)
        {
            log.Info("get Procession: ID= " + uuID);

            if (map.ContainsKey(uuID))
            {
                log.Info("get Procession: ID= " + uuID + "p=" + map[uuID]);
                int value = map[uuID];
                if (map[uuID] == 100)
                {
                    removeProcess(uuID);
                }
                return value;
            }
            else
            {
                log.Info("Procession not exist!");
                return 0 ;
            }
        }

        public static void startNewProcession(String uuID)
        {
            log.Info("Start a new Procession: ID= " + uuID);
            if (map.ContainsKey(uuID))
            {
                log.Error("ProcessExcist!!");
                ///这里一般不会出现重复的问题，因为在进程接受后都会清除掉的。所以这里处理的时候打开一个新的进程。
                map[uuID] = 0;
            }
            else{
                map[uuID] =0;
            }
          
        }
        public static void removeProcess(String uuID)
        {
            if (map.ContainsKey(uuID))
            {
                map.Remove(uuID);
            }
            else
            {
                log.Error("ProcessNotExcist!!");
            }
        }

        internal static void Update(string uuID, int p)
        {
            log.Info("Update Procession: ID= " + uuID+"p="+p);
            if (map.ContainsKey(uuID))
            {
                map[uuID] = p;
            }
            else
            {
                map[uuID] = p;
                log.Error("ProcessNotExcist!!");
            }
        }

    }

   
}