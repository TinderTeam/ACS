using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Models.Po.Business;

namespace ACS.Models.Model
{
    public class EventModel
    {
        Control control;
        EventMsg msg;

        #region 封装

        public Control Control
        {
            get { return control; }
            set { control = value; }
        }
        public EventMsg Msg
        {
            get { return msg; }
            set { msg = value; }
        }
        #endregion

    }

    public class EventMsg
    {
        private byte eType;
        private byte second;
        private byte minute;
        private byte hour;
        private byte day;
        private byte month;
        private int year;
        private byte doorStatus;
        private byte ver;
        private byte funtionByte;
        private Boolean online;
        private byte cardsofPackage;
        private UInt64 cardNo;
        private byte eventType;
        private UInt16 cardIndex;
        private byte cardStatus;
        private byte reader;

        #region 封装


        public byte EType
        {
          get { return eType; }
          set { eType = value; }
        }

        public byte Second
        {
            get { return second; }
            set { second = value; }
        }

        public byte Minute
        {
            get { return minute; }
            set { minute = value; }
        }


        public byte Hour
        {
            get { return hour; }
            set { hour = value; }
        }

        public byte Day
        {
            get { return day; }
            set { day = value; }
        }

        public byte Month
        {
            get { return month; }
            set { month = value; }
        }

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public byte DoorStatus
        {
            get { return doorStatus; }
            set { doorStatus = value; }
        }
    
        public byte Ver
        {
            get { return ver; }
            set { ver = value; }
        }
    
        public byte FuntionByte
        {
            get { return funtionByte; }
            set { funtionByte = value; }
        }

        public Boolean Online
        {
            get { return online; }
            set { online = value; }
        }

        public byte CardsofPackage
        {
            get { return cardsofPackage; }
            set { cardsofPackage = value; }
        }

        public UInt64 CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }

        public byte EventType
        {
            get { return eventType; }
            set { eventType = value; }
        }

        public UInt16 CardIndex
        {
            get { return cardIndex; }
            set { cardIndex = value; }
        }

        public byte CardStatus
        {
            get { return cardStatus; }
            set { cardStatus = value; }
        }

        public byte Reader
        {
          get { return reader; }
          set { reader = value; }
        }
        #endregion

        public string toStr()
        {
            String str = "eType=" + eType+";....";
            return str;
        }
    }
}