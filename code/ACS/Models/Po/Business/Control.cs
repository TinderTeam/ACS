using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACS.Models.Po
{
    public class Control
    {
        private int controlID;//编号
        private String netID;//连接号即
        private String code;//编号
        private String controlName;//名称
        private String adress;//
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
        private DateTime fireTime;//
        private DateTime alarmTime;//
        private String alarmMast;//
        private String group;//
        private DateTime timeTamp;//
        private String localSetup;//
        private int mapID;//
        private int xPiont;//
        private int yPoint;//
        private String mapVisible;//
        private String online;//
        private String doorOpen;//
        private String functionMast;//
        private String outPut;//
        private String input;//
        private String is16;//
        private DateTime openTime;//
        private DateTime closeTime;//
        private String floorDelay;//
        private String otherFuction;//
        private String mxOutPort;//
        private String aesPin;//
        private String useAes;//
        private String icAdress;//
        private String icIsSt;//

    }
}