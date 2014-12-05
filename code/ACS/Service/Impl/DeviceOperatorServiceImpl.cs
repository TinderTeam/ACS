using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Business;
using TcpipIntface;

namespace ACS.Service.Impl
{
    public class DeviceOperatorServiceImpl : DeviceOperatorService
    {
       private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       public Dictionary<string, TcpipClass> deviceConnectorMap = new Dictionary<string, TcpipClass>();
       public Dictionary<string, Control> deviceCache = new Dictionary<string, Control>();
       public DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
       public DeviceOperatorServiceImpl()
       {
           #region Devcie cache init

           #endregion
       }
       public Dictionary<string, TcpipClass> tcpMap = new Dictionary<string, TcpipClass>();

        void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
             byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door, byte EventType,
             UInt16 CardIndex, byte CardStatus, byte reader, out Boolean OpenDoor, out Boolean Ack)
        {
            Ack = true;
            OpenDoor = false;
            Door = 1;
            switch (EType)
            {
                 
                case 0: 
                    //控制器状态处理
                    log.Info("EType:" + EType.ToString());
                    log.Info("DoorStatus:" + DoorStatus.ToString());
                    log.Info("EventType:" + EventType.ToString());
                    ShowMsg(EType.ToString() + "控制器状态 " + DoorStatus.ToString()); 

                    break;
                case 1: ShowMsg(EType.ToString() + "刷卡 " + CardNo.ToString() + " " + EventType.ToString()); 
                break; // card event
                case 2: ShowMsg(EType.ToString() + "报警" + EventType.ToString()); 
                break; // alarm
            }
        }

        void ShowMsg(string buffRX)
        {
           /*
            listBox1.BeginUpdate();
            listBox1.Items.Insert(0, buffRX);
            listBox1.EndUpdate();
            */
            log.Info("data:" + buffRX);
        }

        public TcpipClass getConnector(string deviceID)
        {
            TcpipClass connector;
            if (deviceConnectorMap.ContainsKey(deviceID))
            {
                connector = tcpMap[deviceID];
            }
            else
            {
                connector = new TcpipClass();
                connector.RxDataEvent += ShowMsg;
                connector.OnEventHandler += EventHandler;
                //建立连接
                if(!connector.OpenIP(DeviceCache.getValue(deviceID).Ip, 8000)){
                    log.Warn("Connection error: DeviceID=" + deviceID);
                };
                deviceConnectorMap.Add(deviceID, connector);
            }
            return connector;
        }

        #region DeviceOperatorService 成员

        public bool openDoor(string doorID)
        {
            Door door = deviceService.getDoorByID(doorID).Door;
            String deviceID = door.ControlID.ToString();
            TcpipClass connector = getConnector(deviceID);
            if (connector.Opendoor(ModelConventService.toDoorIndex(door)))
            {
                log.Info("Open successful...");

                return true;
            }
            log.Info("Can not open the door! Error:" + connector.TCPLastError);

            return false;
        }

        public bool closeDoor(string doorID)
        {
            Door door = deviceService.getDoorByID(doorID).Door;
            String deviceID = door.ControlID.ToString();
            TcpipClass connector = getConnector(deviceID);

            if (connector.Closedoor(ModelConventService.toDoorIndex(door)))
            {
                log.Info("Close successful...");
                return true;
            }
            log.Info("Can not close the door:" + connector.TCPLastError);
            return false;
        }

        #endregion
    }
}