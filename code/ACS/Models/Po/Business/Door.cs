using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po.Business
{
    public class Door
    {
        public const String DOOR_ID = "DoorID";
        private int doorID;//门编号
        private int controlID;//控制器编号
        private String doorName;//
        private String doorSerial;//
        private String doorAddress;//
        private DateTime openTime;//
        private DateTime closeOutTime;//
        private long doorAlerm2Long;//
        private String passBack;//
        private String doorEnable;//
        private String doorVisible;//
        private int mapID;//
        private int xPoint;//
        private int yPoint;//
        private String mapVisible;//
        private String isKQ;//
        private String isRepast;//
        private String alarmMast;//
        private DateTime alarmTime;//
        private String isMainDoor;//
        private String readerIn;//
        private String readerOut;//
        private String mCardsOpen;//
        private String mCardsOpenInOut;//

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

        public virtual String DoorAddress
        {
            get { return doorAddress; }
            set { doorAddress = value; }
        }

        public virtual DateTime OpenTime
        {
            get { return openTime; }
            set { openTime = value; }
        }

        public virtual DateTime CloseOutTime
        {
            get { return closeOutTime; }
            set { closeOutTime = value; }
        }

        public virtual long DoorAlerm2Long
        {
            get { return doorAlerm2Long; }
            set { doorAlerm2Long = value; }
        }

        public virtual String PassBack
        {
            get { return passBack; }
            set { passBack = value; }
        }

        public virtual String DoorEnable
        {
            get { return doorEnable; }
            set { doorEnable = value; }
        }

        public virtual String DoorVisible
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

        public virtual String MapVisible
        {
            get { return mapVisible; }
            set { mapVisible = value; }
        }

        public virtual String IsKQ
        {
            get { return isKQ; }
            set { isKQ = value; }
        }

        public virtual String IsRepast
        {
            get { return isRepast; }
            set { isRepast = value; }
        }

        public virtual String AlarmMast
        {
            get { return alarmMast; }
            set { alarmMast = value; }
        }

        public virtual DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public virtual String IsMainDoor
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

        public virtual String MCardsOpen
        {
            get { return mCardsOpen; }
            set { mCardsOpen = value; }
        }

        public virtual String MCardsOpenInOut
        {
            get { return mCardsOpenInOut; }
            set { mCardsOpenInOut = value; }
        }

    }
}