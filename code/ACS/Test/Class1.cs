using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ACS.Service;
using TcpipIntface;

namespace ACS.Test
{
    public class Class1
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        #region 事件接口
        public void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
             byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door, byte EventType,
             UInt16 CardIndex, byte CardStatus, byte reader, out Boolean OpenDoor, out Boolean Ack)
        {

            log.Info("New Event;EType=" + EType);
            switch (EType)
            {
                case 0:
                    //statusEventHandel(eventMsg);
                    break;
                case 1:
                  //  cardEventHandel(eventMsg);
                    break; //card event
                case 2:
                   // alarmEventHandel(eventMsg);
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


        
        public void test(){
            newConnect("1");
        }

        public TcpipClass newConnect(string deviceID)
        {
            log.Info("Start new Connect on devcie;" + deviceID);
            TcpipClass connector;
            connector = new TcpipClass();
            connector.RxDataEvent += ShowMsg;
            connector.OnEventHandler += EventHandler;
            DeviceControlThread deviceControlThread =
                new DeviceControlThread(connector, "163.125.163.246", 8000);
            Thread oThread = new Thread(deviceControlThread.connect);
            log.Info("new ConnectionThread Start. -- DeviceID:" + deviceID);
            oThread.Start();
            return connector;

        }
    }
}