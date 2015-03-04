using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ACS.Common.Dao;
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
            byte Ver, byte FuntionByte, Boolean Online, byte CardsofPackage, UInt64 CardNo, byte Door, byte EventType,
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
            eventMsg.DoorNum= Door;
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
                    log.Info("Device operate command : open door.  Door number=" + door.DoorNum+" controlID="+control.ControlID);
                    result = connector.Opendoor((byte)door.DoorNum);
                    break;
                case OperateDeviceCmdEnum.CLOSE_DOOR:
                    log.Info("Device operate command : close door.  Door number=" + door.DoorNum + " controlID="+control.ControlID);
                    result = connector.Closedoor((byte)door.DoorNum);
                    break;
                case OperateDeviceCmdEnum.LOCK_DOOR:
                    log.Info("Device operate command : lock door.  Door number=" + door.DoorNum + " controlID=" + control.ControlID);
                    result = connector.LockDoor((byte)door.DoorNum, true);
                    break;
                case OperateDeviceCmdEnum.UNLOCK_DOOR:
                    log.Info("Device operate command : unlock door.  Door number=" + door.DoorNum + " controlID=" + control.ControlID);
                    result = connector.LockDoor((byte)door.DoorNum, false);
                    break;
                case OperateDeviceCmdEnum.SET_FIRE:
                    log.Info("Device operate command : set fire alarm.  controlID=" + control.ControlID);
                    result = connector.SetFire(false, false);
                    break;
                case OperateDeviceCmdEnum.CACEL_FIRE:
                    log.Info("Device operate command : cancel fire alarm.  controlID=" + control.ControlID);
                    result = connector.SetFire(true, false);
                    break;
                case OperateDeviceCmdEnum.SET_ALARM:
                    log.Info("Device operate command : set alarm.  controlID=" + control.ControlID);
                    result = connector.SetAlarm(false, false);
                    break;
                case OperateDeviceCmdEnum.CACEL_ALARM:
                    log.Info("Device operate command : cancel fire alarm.  controlID=" + control.ControlID);
                    result = connector.SetAlarm(true, false);
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
            log.Info("TCPControl AddTimeZone: door.DoorNum= " + door.DoorNum + ",doorTime.DoorTimeNum=" + doorTime.DoorTimeNum + ",doorTime.DoorTimeNum=" + doorTime.DoorTimeNum + ",doorTime.StartTime" + doorTime.StartTime + ",doorTime.EndTime= " + doorTime.EndTime + ",week=" + week);
            bool result = connector.AddTimeZone(
                (ushort)door.DoorNum, 
                (byte)doorTime.DoorTimeNum, 
                doorTime.StartTime,
               doorTime.EndTime, 
                week, 
                true, 
                (byte)1, 
                DateTime.Now, 
                0);

            if (result)
            {
                log.Info("TCPControl AddTimeZone:Success...");
            }
            else
            {
                log.Info("TCPControl AddTimeZone: Fail...");
            }

        }





        public void cardInfoDownLoad(Employee employee,List<DoorTimeView> doorTimeList,int employeeIndex)
        {
            log.Info("CardInfo: "+ employeeIndex+ employee + doorTimeList);

            ushort index = (ushort)employeeIndex;
            byte[] TZ = new byte[4];
            int doorCnt = DeviceTypeCache.GetInstance().GetDeviceType(this.control.TypeID).DoorNum;
            switch (doorCnt)
            {
                case 1:
                    foreach(DoorTimeView doorTime in doorTimeList)
                    {
                        int num = doorTime.DoorTimeNum;
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
                        int num = doorTime.DoorTimeNum;
                        int doorNum = doorTime.DoorNum;
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
                        int num = doorTime.DoorTimeNum;
                        int doorNum =doorTime.DoorNum;
                        byte temp = 0x01;
                        for (int i = 0; i < num;i++ )
                        {
                            temp = (byte)(temp << num);
                        }
                        TZ[doorNum] += temp;
                    }
                    break;
            }
            log.Info("TCPControl Addcard: cardno= " + System.Convert.ToUInt64(employee.CardNo) + ",pin=" + employee.Pin + ",EmployeeName=" + employee.EmployeeName + ",TZ[0-3]=" + TZ[0] + " " + TZ[1] + " "+TZ[2]+" "+TZ[3]+",date= "+employee.EndDate);
            bool result = connector.AddCard(
                       index,
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
            if (!result)
            {
                log.Info("TCPControl Addcard: Fail..." + connector.TCPLastError);
            }
        }



        public bool ClearAllCards()
        {
            if (!connector.ClearAllCards())
            {
                log.Info("TCPControl Addcard: Fail..." + connector.TCPLastError);
                return false;
            }
            else
            {
                return true;
            }
        }

        public TcpipClass getConnector()
        {
            return connector;
        }




        #region DeviceOperator 成员
        public void SetDoor(Door door)
        {
            log.Info("TCPControl set door: DoorNum= " + door.DoorNum + ",OpenTime=" + door.OpenTime + ",CloseOutTime=" + (ushort)door.CloseOutTime + ",DoorAlerm2Long" + door.DoorAlerm2Long + ",door.AlarmMast= " + door.AlarmMast + " .AlarmTime=" + door.AlarmTime + ",PassBack= " + door.PassBack + ",MCardsOpen= " + (byte)door.MCardsOpen + "," + (byte)door.MCardsOpenInOut);
            bool result = connector.SetDoor(
                        (byte)door.DoorNum,
                        (ushort)door.OpenTime,
                        (ushort)door.CloseOutTime,
                        door.DoorAlerm2Long,
                        (ushort)door.AlarmMast,
                        (ushort)door.AlarmTime,
                        door.PassBack,
                        (byte)door.MCardsOpen,
                        (byte)door.MCardsOpenInOut
                  );
            if (!result)
            {
                log.Info("TCPControl set door: Fail..." + connector.TCPLastError);
            }
                     
        }

        public void SetDoorTime(DoorTimeView doortime)
        {
            log.Info("TCPControl set door time: DoorNum= " + doortime.DoorNum + ",DoorTimeNum=" + doortime.DoorTimeNum + ",StartTime=" + doortime.StartTime + ",EndTime" + doortime.EndTime + ",WeekByte= " + getWeekByte(doortime) + " ,PassBack=" + true + ",Indetifys= " + doortime.Identify + ",LimitDate= " + doortime.LimitDate + ",group=0");

            /* public Boolean AddTimeZone(
             *  UInt16 Door, 
             *  byte Index, 
             *  DateTime frmtime,
             *  DateTime totime, 
             *  byte Week, 
             *  Boolean PassBack, 
             *  byte Indetify, 
             *  DateTime Enddatetime,
             *  byte Group
             *  )
             */
            DateTime startTime = doortime.StartTime;
            DateTime endTime = doortime.EndTime;
            startTime.AddYears(2000);
            endTime.AddYears(2000);
            bool result = connector.AddTimeZone(
                (byte)doortime.DoorNum,
                (byte)doortime.DoorTimeNum,
                startTime,
                endTime,
                getWeekByte(doortime),
                true,
                (byte)doortime.Identify,
                doortime.LimitDate,
                0 //group
                );
            if (!result)
            {
                log.Info("TCPControl set door: Fail..." + connector.TCPLastError);
            }
            
        }

        //获取星期字
        private byte getWeekByte(DoorTimeView doortime)
        {
            byte weekByte = 0x00;
            if (doortime.Sunday)
            {
                weekByte += 0x01;
            }
            if (doortime.Monday)
            {
                weekByte += 0x02;
            }
            if (doortime.Tuesday)
            {
                weekByte += 0x04;
            }
            if (doortime.Wednesday)
            {
                weekByte += 0x08;
            }
            if (doortime.Thursday)
            {
                weekByte += 0x10;
            }
            if (doortime.Friday)
            {
                weekByte += 0x20;
            }
            if (doortime.Saturday)
            {
                weekByte += 0x40;
            }
            if (doortime.Holiday)
            {
                weekByte += 0x80;
            }
            return weekByte;
        }

        public void DelTimeZone(int DoorNum)
        {
            log.Info("TCPControl DelTimeZone:DoorNum=" + DoorNum);
            bool result = connector.DelTimeZone((byte)DoorNum);
            log.Info("TCPControl DelTimeZone: Success...");
            if (!result)
            {
                log.Info("TCPControl DelTimeZone: Fail..." + connector.TCPLastError);
            }
        }


        public void DelHoliday()
        {
            log.Info("TCPControl DelHoliday:control=" + control.ControlID);
            bool result = connector.DelHoliday();
            log.Info("TCPControl DelHoliday: Success...");
            if (!result)
            {
                log.Info("TCPControl DelHoliday: Fail..." + connector.TCPLastError);
            }
        }

        public void AddHoliday(Holiday holiday)
        {
            log.Info("TCPControl AddHoliday:Holiday=" + JsonConvert.ObjectToJson(holiday));
            bool result = connector.AddHoliday(
                (byte)holiday.HolidayID,
                holiday.StartTime,
                holiday.EndTime);
            log.Info("TCPControl AddHoliday: Success...");
            if (!result)
            {
                log.Info("TCPControl AddHoliday: Fail..." + connector.TCPLastError);
            }
        }

        #endregion
    }
}