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
using ACS.Common.Util;

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

        public void SetDoorTime(Door door,DoorTime doorTime)
        {
            byte week = 0;
            if(doorTime.Holiday)
            {
                week |= 0x80;
            }
            if(doorTime.Sunday)
            {
                week |= 0x40;
            }
            if(doorTime.Saturday)
            {
                week |= 0x20;

            }
            if(doorTime.Friday)
            {
                week |= 0x10;
            }
            if(doorTime.Thursday)
            {
                week |= 0x08;
            }
            if(doorTime.Wednesday)
            {
                week |= 0x04;
            }
            if(doorTime.Tuesday)
            {
                week |= 0x02;
            }
            if(doorTime.Monday)
            {
                week |= 0x01;
            }
            bool result = connector.AddTimeZone(
                (ushort)door.DoorNum, 
                (byte)doorTime.DoorTimeNum, 
                DateUtil.StringToDateTime(doorTime.StartTime),
                 DateUtil.StringToDateTime(doorTime.EndTime), 
                week, 
                true, 
                (byte)1, 
                DateTime.Now, 
                0);
        }


        public void deviceCardInfoDownLoad(Dictionary<Employee, List<DoorTimeView>> cardInfoMap)
        {
            log.Info("cardInfoMap is :" + cardInfoMap);
            List<Employee> list = new List<Employee>(cardInfoMap.Keys);
            foreach (Employee e in list)
            {
                if (cardInfoMap.ContainsKey(e))
                {
                    cardInfoDownLoad(e, cardInfoMap[e]);
                }
            }
        }


        public void cardInfoDownLoad(Employee employee,List<DoorTimeView> doorTimeList)
        {
            log.Info("CardInfo: " + employee + doorTimeList);
            byte[] TZ = new byte[4];
            int doorCnt = DeviceTypeCache.GetInstance().GetDeviceType(this.control.TypeID).DoorNum;
            switch (doorCnt)
            {
                case 1:
                    foreach(DoorTimeView doorTime in doorTimeList)
                    {
                        int num = doorTime.DoorTimeID;
                        byte temp = 0x01;
                        if (num < 8)
                        {
                            TZ[0] += (byte)(temp << num);
                        }
                        else
                        {
                            TZ[1] += (byte)(temp << (num -8));
                        }
                    }

                    break;
                case 2:
                    foreach (DoorTimeView doorTime in doorTimeList)
                    {
                        int num = doorTime.DoorTimeID;
                        int doorNum = doorTime.DoorID;
                        byte temp = 0x01;
                        for (int i = 0; i < TZ.Length; i++)
                        {
                            if (num < 8)
                            {
                                TZ[doorNum] += (byte)(temp << num);
                            }
                            else
                            {
                                TZ[doorNum] += (byte)(temp << (num - 8));
                            }
                        }
 

                    }
                    break;
                case 4:
                    foreach (DoorTimeView doorTime in doorTimeList)
                    {
                        int num = doorTime.DoorTimeID;
                        int doorNum =doorTime.DoorID;
                        byte temp = 0x01;
                        for (int i = 0; i < TZ.Length;i++ )
                        {
                            TZ[doorNum] += (byte)(temp << num);
                        }
                    }
                    break;
            }

            connector.AddCard(
                       0,
                       System.Convert.ToUInt64(employee.CardNo),
                       employee.Pin,
                       employee.EmployeeName,
                       TZ[0],
                       TZ[1],
                       TZ[2],
                       TZ[3],
                       1,
                       employee.EndDate
                  );
        }
     
    }
}