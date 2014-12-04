using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TcpipIntface;

namespace ACS.Service.Impl
{
    public class DeviceOperatorServiceImpl : DeviceOperatorService
    {
       private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       public  Dictionary<string, TcpipClass> tcpMap = new Dictionary<string, TcpipClass>();
        /// <summary>
        /// 事物处理方法
        /// </summary>
        /// <param name="EType"></param>
        /// <param name="second"></param>
        /// <param name="minute"></param>
        /// <param name="hour"></param>
        /// <param name="day"></param>
        /// <param name="Month"></param>
        /// <param name="Year"></param>
        /// <param name="DoorStatus"></param>
        /// <param name="Ver"></param>
        /// <param name="FuntionByte"></param>
        /// <param name="Online"></param>
        /// <param name="CardsofPackage"></param>
        /// <param name="CardNo"></param>
        /// <param name="Door"></param>
        /// <param name="EventType"></param>
        /// <param name="CardIndex"></param>
        /// <param name="CardStatus"></param>
        /// <param name="Reader"></param>
        /// <param name="Ack"></param>
        void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
             byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door, byte EventType,
             UInt16 CardIndex, byte CardStatus, byte reader, out Boolean OpenDoor, out Boolean Ack)
        {
            Ack = true;
            OpenDoor = false;
            Door = 1;
            switch (EType)
            {
                 
                case 0: ShowMsg(EType.ToString() + "控制器状态 " + DoorStatus.ToString()); 
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
            log.Info("TCPMsg:"+buffRX);
        }
            
        
        public TcpipClass getControllerTCP(string ip)
        {

           TcpipClass acsTcp;
           if(tcpMap.ContainsKey(ip)){
                acsTcp = tcpMap[ip];
           }else{
                acsTcp = new TcpipClass();
                acsTcp.RxDataEvent += ShowMsg;
               acsTcp.OnEventHandler += EventHandler;
               tcpMap[ip] = acsTcp;
               return acsTcp;
           }
         
           return acsTcp;
        }

        public bool openDoor(String ip,byte doorStatus)
        {
            TcpipClass tcpControl = getControllerTCP(ip);
            if (cennectDevice(ip))
            {
                if (tcpControl.Opendoor(doorStatus))
                {
                    log.Info("Open successful...");
                    return true ;
                }
                log.Info("Can not open the door:" + tcpControl.TCPLastError);             
            }
            return false ;
        }

        public bool closeDoor(String ip, byte doorStatus)
        {
            TcpipClass tcpControl = getControllerTCP(ip);
            if (cennectDevice(ip))
            {
                if (tcpControl.Closedoor(doorStatus))
                {
                    log.Info("Close successful...");
                    return true;
                }
                log.Info("Can not close the door:" + tcpControl.TCPLastError);
            }
            return false;
        }

        public bool reset(String ip)
        {
            TcpipClass tcpControl = getControllerTCP(ip);
            if (cennectDevice(ip))
            {
                if (tcpControl.Reset())
                {
                    log.Info("Reset successful...");
                    return true;
                }
                log.Info("Can not Reset the door:" + tcpControl.TCPLastError);
            }
            return false;
        }

      

        private bool cennectDevice(String ip)
        {
            //Connect
            if (!getControllerTCP(ip).OpenIP(ip, 8000))
            {
                log.Info("Can not connect the device on : " + ip + ":" + getControllerTCP(ip).TCPLastError);
                return false;
            }
            log.Info("Connect successful on : " + ip);
            return true;
        }

    }
}