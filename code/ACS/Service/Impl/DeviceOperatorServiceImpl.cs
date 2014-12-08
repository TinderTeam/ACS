using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Dao;
using ACS.Common.Service;
using ACS.Dao;
using ACS.Models.Model;
using ACS.Models.Po.Business;
using TcpipIntface;

namespace ACS.Service.Impl
{
    public class DeviceOperatorServiceImpl :BasicServiceFrameWork, DeviceOperatorService
    {
        #region Service Define 
            public Dictionary<string, TcpipClass> deviceConnectorMap = new Dictionary<string, TcpipClass>();
            public DeviceService deviceService = ServiceContext.getInstance().getDeviceService();
            public DeviceCache deviceCache = DeviceCache.getInstance();
            public StatusCache statusCache = StatusCache.getInstance();
            CommonDao<AlarmRecord> alarmDao = DaoContext.getInstance().getAlarmRecordDao();
            CommonDao<EventRecord> eventDao = DaoContext.getInstance().getEventRecordDao();
        #endregion

        public DeviceOperatorServiceImpl()
       {

           #region remarks
           /********************************************************************************************
            *  系统初始化时要对已配置的全部设备进行连接,这里需要开一个多线程。
            ********************************************************************************************/
           #endregion

           #region Devcie cache init
           deviceCache.reload();
           //开多线程进行连接deviceCache.getAll();
           #endregion
       }


        #region 事件接口
        void EventHandler(byte EType, byte Second, byte Minute, byte Hour, byte Day, byte Month, int Year, byte DoorStatus,
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
            eventMsg.Ver= Ver;
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
                    statusEventHandel(eventMsg);
                    break;
                case 1:
                    cardEventHandel(eventMsg);
                    break; //card event
                case 2:
                    alarmEventHandel(eventMsg);
                break; // alarm
            }

            Ack = true;
            OpenDoor = false;
            Door = 1;
            
        }

      
       

        //输出Msg
        void ShowMsg(string buffRX)
        {
            log.Info("msg data:" + buffRX);
        }
        #endregion

        #region 事件处理

             #region 状态事件
            /// <summary>
            /// 通过监控界面输出
            /// </summary>
            /// <param name="eventMsg"></param>
             private void statusEventHandel(EventMsg eventMsg)
             {
                log.Info("[StatusEvent] msg:.....");
                EventModel eventModel = ModelConventService.toEventModel(eventMsg);
                statusCache.updateValue(eventModel.Control.ControlID.ToString(), eventModel);
               }
             #endregion

             #region 报警事件
             private void alarmEventHandel(EventMsg eventMsg)
             {
                 log.Info("[AlarmEvent] msg:.....");
                 AlarmRecord alarmRecord = ModelConventService.toAlarmRecord(eventMsg);
                 if (ModelVerificationService.AlarmRecordVirification(alarmRecord))
                 {
                     alarmDao.create(alarmRecord);
                 }
                 else
                 {
                     log.Error("");
                 }
             }
             #endregion

             #region 刷卡事件
             private void cardEventHandel(EventMsg eventMsg)
             {
                 log.Info("[CardEvent] msg:.....");
                 EventRecord eventRecord = ModelConventService.toEventRecord(eventMsg);
                 if (ModelVerificationService.EventRecordVirification(eventRecord))
                 {
                     eventDao.create(eventRecord);
                 }
                 else
                 {
                     log.Error("");
                 }
             }
             #endregion

        #endregion

             #region 设备控制接口功能

             public TcpipClass getConnector(string deviceID)
        {
            TcpipClass connector;
            if (deviceConnectorMap.ContainsKey(deviceID))
            {
                connector = deviceConnectorMap[deviceID];
            }
            else
            {
                connector = new TcpipClass();
                connector.RxDataEvent += ShowMsg;
                connector.OnEventHandler += EventHandler;
                //建立连接
                if(!connector.OpenIP(DeviceCache.getInstance().getValue(deviceID).Ip, 8000)){
                    log.Warn("Connection error: DeviceID=" + deviceID);
                };
                deviceConnectorMap.Add(deviceID, connector);
            }
            return connector;
        }

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