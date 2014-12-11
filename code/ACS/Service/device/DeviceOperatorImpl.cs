using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ACS.Common.Dao;
using ACS.Common.Service;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using TcpipIntface;
using ACS.Service.device;
using ACS.Common;
using ACS.Service.Constant;

namespace ACS.Service.Impl
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class DeviceOperatorImpl :  System.ContextBoundObject,DeviceOperator
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Control control;
        private TcpipClass connector;
        private DeviceService deviceService = ServiceContext.getInstance().getDeviceService();

        public Control GetControl()
        {
            return this.control;
        }
   
        public DeviceOperatorImpl(Control control)
       {
           this.control = control;
           connector = new TcpipClass();
           connector.RxDataEvent += ShowMsg;
           connector.OnEventHandler += EventHandler;
          
       }
 
        public void Connect()
        {
            log.Info("Start new Connect on devcie;" + control.ControlID);

            bool result = connector.OpenIP(control.Ip, 8000);
            

            //建立连接
            if (!result)
            {
                log.Error("Connection error: IP=" + control.Ip);
                log.Error("conncet device failed,the control ip address " + control.Ip + ",the port is " + control.Port);
                throw new FuegoException(ExceptionMsg.CONNECT_FAILED);
            }
            OnlineDeviceCache.RefreshStatus(this.control);
            log.Debug("Connect success:IP=" + control.Ip);
 

        }

        #region 事件接口
        public void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
             byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, out byte Door, byte EventType,
             UInt16 CardIndex, byte CardStatus, byte reader, out Boolean OpenDoor, out Boolean Ack)
        {
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

            EventHandel handel = new EventHandel(control,eventMsg);
            handel.handel();

            Ack = true;
            OpenDoor = false;
            Door = 1;
        }

        //输出Msg
        public void ShowMsg(string buffRX)
        {
            log.Info("Receive Msg(DeviceID=" + control.ControlID + ",IP=" + control.Ip+ "):" + buffRX);
        }
        #endregion
        public void Operate(OperateDeviceCmdEnum cmdCode, Door door)
        {
            bool result = true;
            switch (cmdCode)
            {
                case OperateDeviceCmdEnum.OPEN_DOOR:
                    result = connector.Opendoor((byte)door.DoorNum);
                    break;
                case OperateDeviceCmdEnum.CLOSE_DOOR:
                    result = connector.Closedoor((byte)door.DoorNum);
                    break;
                case OperateDeviceCmdEnum.LOCK_DOOR:
                    result = connector.LockDoor((byte)door.DoorNum,true);
                    break;
                case OperateDeviceCmdEnum.UNLOCK_DOOR:
                    result = connector.LockDoor((byte)door.DoorNum, false);
                    break;
                case OperateDeviceCmdEnum.SET_FIRE:
                    result = connector.SetFire(true, false);
                    break;
                case OperateDeviceCmdEnum.CACEL_FIRE:
                    result = connector.SetFire(false, false);
                    break;
                case OperateDeviceCmdEnum.SET_ALARM:
                    result = connector.SetAlarm(true, false);
                    break;
                case OperateDeviceCmdEnum.CACEL_ALARM:
                    result = connector.SetAlarm(false, false);
                    break;
            }
        }
    

    }
}