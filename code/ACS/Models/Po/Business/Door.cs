using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;

namespace ACS.Models.Po.Business
{
    public class Door
    {
        public const String DOOR_ID = "DoorID";
        public const String CONTROL_ID = "ControlID";
        private int doorID;//门编号
        private int controlID;//控制器编号
        private String doorName;//
        private String doorSerial;//
        private int doorAddress;//
        private int openTime;           //开门时长，单位为秒
        private int closeOutTime;       //开门超时时长，单位为秒
        private bool doorAlerm2Long;//
        private bool passBack;//
        private bool doorEnable;//
        private bool doorVisible;//
        private int mapID;//
        private int xPoint;//
        private int yPoint;//
        private bool mapVisible;//
        private bool isKQ;//
        private bool isRepast;//
        private int alarmMast;//
        private int alarmTime;//
        private bool isMainDoor;//
        private String readerIn;//
        private String readerOut;//
        private int mCardsOpen;//
        private int mCardsOpenInOut;//
        public virtual int DoorNum { get; set; }

        public virtual int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }

        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }

        public virtual String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }

        public virtual String DoorSerial
        {
            get { return doorSerial; }
            set { doorSerial = value; }
        }

        public virtual int DoorAddress
        {
            get { return doorAddress; }
            set { doorAddress = value; }
        }

        public virtual int OpenTime
        {
            get { return openTime; }
            set { openTime = value; }
        }

        public virtual int CloseOutTime
        {
            get { return closeOutTime; }
            set { closeOutTime = value; }
        }

        public virtual bool DoorAlerm2Long
        {
            get { return doorAlerm2Long; }
            set { doorAlerm2Long = value; }
        }

        public virtual bool PassBack
        {
            get { return passBack; }
            set { passBack = value; }
        }

        public virtual bool DoorEnable
        {
            get { return doorEnable; }
            set { doorEnable = value; }
        }

        public virtual bool DoorVisible
        {
            get { return doorVisible; }
            set { doorVisible = value; }
        }

        public virtual int MapID
        {
            get { return mapID; }
            set { mapID = value; }
        }

        public virtual int XPoint
        {
            get { return xPoint; }
            set { xPoint = value; }
        }

        public virtual int YPoint
        {
            get { return yPoint; }
            set { yPoint = value; }
        }

        public virtual bool MapVisible
        {
            get { return mapVisible; }
            set { mapVisible = value; }
        }

        public virtual bool IsKQ
        {
            get { return isKQ; }
            set { isKQ = value; }
        }

        public virtual bool IsRepast
        {
            get { return isRepast; }
            set { isRepast = value; }
        }

        public virtual int AlarmMast
        {
            get { return alarmMast; }
            set { alarmMast = value; }
        }

        public virtual int AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public virtual bool IsMainDoor
        {
            get { return isMainDoor; }
            set { isMainDoor = value; }
        }

        public virtual String ReaderIn
        {
            get { return readerIn; }
            set { readerIn = value; }
        }

        public virtual String ReaderOut
        {
            get { return readerOut; }
            set { readerOut = value; }
        }

        public virtual int MCardsOpen
        {
            get { return mCardsOpen; }
            set { mCardsOpen = value; }
        }

        public virtual int MCardsOpenInOut
        {
            get { return mCardsOpenInOut; }
            set { mCardsOpenInOut = value; }
        }

    }
}