using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ACS.Common.Constant;

namespace ACS.Models.Po.Business
{
    public class Control
    {
        public const String CONTROL_ID = "ControlID";
        public const char SPLIT = '_';
        public const String CONTROL_TYPE = "C";
        public const String DOOR_TYPE = "D";
        private int controlID;//编号
        private String netID;//连接号即
        private String code;//编号
        private String controlName;//名称
        private String address;//
        private String serial;//控制器序列号
        private String type;//控制器类型
        private String enable;//状态
        private String duressPin;//DuressPIN挟持密码（未用）
        private int areaID;//BeDuress使用挟持（未用）
        private String www;//
        private String ip;//Ip地址用于网络型控制器
        private int port;//通讯端口
        private String byIP;//ByIP  使用TCPIP通讯（未用）
        private String lockEach;//
        private DateTime fireTime = DefaltValueConstant.getDefaltDateTime();//
        private DateTime alarmTime = DefaltValueConstant.getDefaltDateTime();//
        private String alarmMast;//
        private String controlGroup;//
        private DateTime timeTamp = DefaltValueConstant.getDefaltDateTime();//
        private String localSetup;//
        private int mapID;//
        private int xPoint;//
        private int yPoint;//
        private String mapVisible;//
        private String online;//
        private String doorOpen;//
        private String functionMast;//
        private String output;//
        private String input;//
        private String is16;//
        private DateTime openTime = DefaltValueConstant.getDefaltDateTime();//
        private DateTime closeTime = DefaltValueConstant.getDefaltDateTime();//
        private String floorDelay;//
        private String otherFuction;//
        private String mxOutPort;//
        private String aesPin;//
        private String useAes;//
        private String icAddress;//
        private String icIsSt;//


        public virtual int ControlID
        {
            get { return controlID; }
            set { controlID = value; }
        }
        public virtual String NetID
        {
            get { return netID; }
            set { netID = value; }
        }
        public virtual String Code
        {
            get { return code; }
            set { code = value; }
        }
        public virtual String ControlName
        {
            get { return controlName; }
            set { controlName = value; }
        }

        public virtual String Address
        {
            get { return address; }
            set { address = value; }
        }

        public virtual String Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        public virtual String Type
        {
            get { return type; }
            set { type = value; }
        }

        public virtual String Enable
        {
            get { return enable; }
            set { enable = value; }
        }

        public virtual String DuressPin
        {
            get { return duressPin; }
            set { duressPin = value; }
        }

        public virtual int AreaID
        {
            get { return areaID; }
            set { areaID = value; }
        }

        public virtual String Www
        {
            get { return www; }
            set { www = value; }
        }

        public virtual String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public virtual int Port
        {
            get { return port; }
            set { port = value; }
        }

        public virtual String ByIP
        {
            get { return byIP; }
            set { byIP = value; }
        }

        public virtual String LockEach
        {
            get { return lockEach; }
            set { lockEach = value; }
        }

        public virtual DateTime FireTime
        {
            get { return fireTime; }
            set { fireTime = value; }
        }

        public virtual DateTime AlarmTime
        {
            get { return alarmTime; }
            set { alarmTime = value; }
        }

        public virtual String AlarmMast
        {
            get { return alarmMast; }
            set { alarmMast = value; }
        }

        public virtual String ControlGroup
        {
            get { return controlGroup; }
            set { controlGroup = value; }
        }

        public virtual DateTime TimeTamp
        {
            get { return timeTamp; }
            set { timeTamp = value; }
        }

        public virtual String LocalSetup
        {
            get { return localSetup; }
            set { localSetup = value; }
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

        public virtual String Online
        {
            get { return online; }
            set { online = value; }
        }

        public virtual String DoorOpen
        {
            get { return doorOpen; }
            set { doorOpen = value; }
        }

        public virtual String FunctionMast
        {
            get { return functionMast; }
            set { functionMast = value; }
        }

        public virtual String Output
        {
            get { return output; }
            set { output = value; }
        }

        public virtual String Input
        {
            get { return input; }
            set { input = value; }
        }

        public virtual String Is16
        {
            get { return is16; }
            set { is16 = value; }
        }

        public virtual DateTime OpenTime
        {
            get { return openTime; }
            set { openTime = value; }
        }

        public virtual DateTime CloseTime
        {
            get { return closeTime; }
            set { closeTime = value; }
        }

        public virtual String FloorDelay
        {
            get { return floorDelay; }
            set { floorDelay = value; }
        }

        public virtual String OtherFuction
        {
            get { return otherFuction; }
            set { otherFuction = value; }
        }

        public virtual String MxOutPort
        {
            get { return mxOutPort; }
            set { mxOutPort = value; }
        }

        public virtual String AesPin
        {
            get { return aesPin; }
            set { aesPin = value; }
        }

        public virtual String UseAes
        {
            get { return useAes; }
            set { useAes = value; }
        }

        public virtual String IcAddress
        {
            get { return icAddress; }
            set { icAddress = value; }
        }

        public virtual String IcIsSt
        {
            get { return icIsSt; }
            set { icIsSt = value; }
        }

    }
}