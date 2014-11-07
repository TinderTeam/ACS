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

        public int DoorID
        {
            get { return doorID; }
            set { doorID = value; }
        }
        private int controlID;//控制器编号

        public int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }
        private String doorName;//

        public String DoorName
        {
            get { return doorName; }
            set { doorName = value; }
        }
        private String doorSerial;//

        public String DoorSerial
        {
            get { return doorSerial; }
            set { doorSerial = value; }
        }
        private String doorAddress;//

        public String DoorAddress
        {
            get { return doorAddress; }
            set { doorAddress = value; }
        }
        private DateTime openTime;//

        public DateTime OpenTime
        {
            get { return openTime; }
            set { openTime = value; }
        }
        private DateTime closeOutTime;//

        public DateTime CloseOutTime
        {
            get { return closeOutTime; }
            set { closeOutTime = value; }
        }
        private long doorAlerm2Long;//

        public long DoorAlerm2Long
        {
            get { return doorAlerm2Long; }
            set { doorAlerm2Long = value; }
        }
        private String passBack;//

        public String PassBack
        {
            get { return passBack; }
            set { passBack = value; }
        }
        private String doorEnable;//

        public String DoorEnable
        {
            get { return doorEnable; }
            set { doorEnable = value; }
        }
        private String doorVisible;//

        public String DoorVisible
        {
            get { return doorVisible; }
            set { doorVisible = value; }
        }
        private int mapID;//

        public int MapID
        {
            get { return mapID; }
            set { mapID = value; }
        }
        private int xPoint;//

        public int XPoint
        {
            get { return xPoint; }
            set { xPoint = value; }
        }
        private int yPoint;//

        public int YPoint
        {
            get { return yPoint; }
            set { yPoint = value; }
        }
        private String mapVisible;//

        public String MapVisible
        {
            get { return mapVisible; }
            set { mapVisible = value; }
        }
        private String isKQ;//

        public String IsKQ
        {
            get { return isKQ; }
            set { isKQ = value; }
        }
        private String isRepast;//

        public String IsRepast
        {
            get { return isRepast; }
            set { isRepast = value; }
        }
        private String alarmMast;//

        public String AlarmMast
        {
            get { return alarmMast; }
            set { alarmMast = value; }
        }
        private DateTime alarmTime;//

        public DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }
        private String isMainDoor;//

        public String IsMainDoor
        {
            get { return isMainDoor; }
            set { isMainDoor = value; }
        }
        private String readerIn;//

        public String ReaderIn
        {
            get { return readerIn; }
            set { readerIn = value; }
        }
        private String readerOut;//

        public String ReaderOut
        {
            get { return readerOut; }
            set { readerOut = value; }
        }
        private String mCardsOpen;//

        public String MCardsOpen
        {
            get { return mCardsOpen; }
            set { mCardsOpen = value; }
        }
        private String mCardsOpenInOut;//

        public String MCardsOpenInOut
        {
            get { return mCardsOpenInOut; }
            set { mCardsOpenInOut = value; }
        }

    }
}