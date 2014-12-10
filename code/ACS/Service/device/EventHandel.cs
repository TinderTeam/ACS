using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Model;
using ACS.Models.Po.Business;

namespace ACS.Service.device
{
    public class EventHandel
    {
        private EventMsg msg;
        private Control control;

        private static log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public EventHandel(Control control,EventMsg msg)
        {
            this.control = control;
            this.msg=msg;
        }

        public void handel(){
            switch (msg.EType)
            {
                case 0:
                    statusEventHandel(msg);
                    break;
                case 1:
                    cardEventHandel(msg);
                    break; //card event
                case 2:
                    alarmEventHandel(msg);
                    break; // alarm
            }
        }

        public void statusEventHandel(EventMsg eventMsg)
        {
            log.Info("[StatusEvent] Msg: ...");
            OnlineDeviceCache.RefreshStatus(control);
            
        }
        public void alarmEventHandel(EventMsg eventMsg)
        {
            log.Info("[AlarmEvent] msg:.....");


            try
            {
                AlarmRecord alarmRecord = new AlarmRecord();

                alarmRecord.ControlID = this.control.ControlID;

                alarmRecord.DoorID = 9;
                alarmRecord.AlarmType = eventMsg.EventType.ToString();

                alarmRecord.AlarmTime = new DateTime(eventMsg.Year, eventMsg.Month, eventMsg.Day, eventMsg.Hour, eventMsg.Minute, eventMsg.Second);
 
                ServiceContext.getInstance().getAlarmRecordService().Create(alarmRecord);
            }
            catch (Exception e)
            {
                log.Error("handle alarm failed", e);
            }

        }
        public void cardEventHandel(EventMsg eventMsg)
        {
            log.Info("[CardEvent] msg:.....");

                 
            try
            {
                EventRecord eventRecord = new EventRecord();
                eventRecord.ControlID = this.control.ControlID;
                eventRecord.DoorID = 9;
                eventRecord.EventTime = new DateTime(eventMsg.Year, eventMsg.Month, eventMsg.Day, eventMsg.Hour, eventMsg.Minute, eventMsg.Second);

                eventRecord.EventType = eventMsg.EventType;
                eventRecord.EmployeeID = (int)(eventMsg.CardNo);
                ServiceContext.getInstance().getEventRecordService().Create(eventRecord);
            }
            catch (Exception e)
            {
                log.Error("handle alarm failed", e);
            }
        }

        public EventMsg Msg
        {
          get { return msg; }
          set { msg = value; }
        }
    }
}