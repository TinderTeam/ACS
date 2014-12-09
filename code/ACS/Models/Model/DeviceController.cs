using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ACS.Models.Po.Business;
using ACS.Service;
using TcpipIntface;

namespace ACS.Models.Model
{
    public class DeviceController
    {

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
 
        private Control control;
        private TcpipClass tcpIp;

     

        #region 事件接口
        public void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
             byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door, byte EventType,
             UInt16 CardIndex, byte CardStatus, byte reader, out Boolean OpenDoor, out Boolean Ack)
        {

            log.Info("New Event;EType=" + EType);

            EventMsg eventMsg = new EventMsg();
            eventMsg.EType = EType;
            eventMsg.Second = Second;
            eventMsg.Minute = Minute;
            eventMsg.Hour = Hour;
            eventMsg.Day = Day;
            eventMsg.Month = Month;
            eventMsg.Year = Year;
            eventMsg.DoorStatus = DoorStatus;
            eventMsg.Ver = Ver;
            eventMsg.FuntionByte = FuntionByte;
            eventMsg.Online = Online;
            eventMsg.CardsofPackage = CardsofPackage;
            eventMsg.CardNo = CardNo;
            eventMsg.EventType = EventType;
            eventMsg.CardIndex = CardIndex;
            eventMsg.CardStatus = CardStatus;
            //eventMsg.Door= Door;
            eventMsg.Reader = reader;

            switch (EType)
            {
                case 0:
 
                    break;
                case 1:
   
                    break; //card event
                case 2:
   
                    break; // alarm
            }

            Ack = true;
            OpenDoor = false;
            Door = 1;

        }




        //输出Msg
        public void ShowMsg(string buffRX)
        {
            log.Info("msg data:" + buffRX);
        }

        #endregion

 

        public string getDeviceID()
        {
            return control.ControlID.ToString();
        }

        public string getIP()
        {
            return control.Ip;
        }

        public void startConnect()
        {
            log.Info("Start new Connect on devcie;" );
            TcpipClass connector;
            connector = new TcpipClass();
            connector.RxDataEvent += ShowMsg;
            connector.OnEventHandler += EventHandler;
            DeviceControlThread deviceControlThread =
                new DeviceControlThread(connector, "163.125.163.246", 8000);
            Thread oThread = new Thread(deviceControlThread.connect);
            log.Info("new ConnectionThread Start. -- DeviceID:" );
            oThread.Start();

        }


 
        public Control Control
        {
            get { return control; }
            set { control = value; }
        }


        public TcpipClass TcpIp
        {
            get { return tcpIp; }
            set { tcpIp = value; }
        }
 

        public DeviceController(Control control)
        {
            this.Control = control;
 
        }
        
    }
}